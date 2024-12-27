using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Cfg
{
    public abstract class IConfigDb
    {
        private static readonly Dictionary<string, ConfigData> _dataList = new Dictionary<string, ConfigData>();
        private static readonly object _locker = new object();

        protected string GetValue([CallerMemberName] string name = "")
        {
            try
            {
                var group = this.GetType().Name;

                return this.GetValue(group, name);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        protected T GetEnumType<T>([CallerMemberName] string name = "") where T : struct
        {
            try
            {
                var value = this.GetValue(name);
                if (string.IsNullOrWhiteSpace(value)) return default(T);

                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        protected int GetValueInt([CallerMemberName] string name = "")
        {
            var value = this.GetValue(name);
            if (string.IsNullOrWhiteSpace(value)) return 0;

            return Convert.ToInt32(value);
        }

        protected bool GetValueBool([CallerMemberName] string name = "")
        {
            var value = this.GetValue(name);
            if (string.IsNullOrWhiteSpace(value)) return false;

            return this.ToBool(value);
        }

        protected double GetValueDouble([CallerMemberName] string name = "")
        {
            var value = this.GetValue(name);
            if (string.IsNullOrWhiteSpace(value)) return 0;

            return Convert.ToDouble(value);
        }

        protected T GetValueClass<T>([CallerMemberName] string name = "") where T : class
        {
            var value = this.GetValue(name);
            if (string.IsNullOrWhiteSpace(value)) return default(T);

            var res = TS.FW.Serialization.Serialization.JsonDeserializer<T>(value);
            if (res == false) return default(T);

            return res.Result;
        }

        protected void SetValue(object value, [CallerMemberName] string name = "")
        {
            try
            {
                var group = this.GetType().Name;
                var tyep = value != null ? value.GetType() : typeof(string);

                if (tyep.IsValueType == true || tyep == typeof(string))
                {
                    this.Upsert(group, name, value);
                }
                else
                {
                    var json = TS.FW.Serialization.Serialization.JsonSerializer(value);
                    if (json == false) throw new Exception(json.Comment);

                    this.Upsert(group, name, json.Result);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected string GetValue(string group, string name)
        {
            lock (_locker)
            {
                var key = string.Format("{0}_{1}", group, name);

                if (_dataList.ContainsKey(key) && _dataList[key].IsUpdate == false)
                {
                    return _dataList[key].Value;
                }

                using (var biz = new ConfigNTx())
                {
                    var res = biz.GetConfigByName(group, name);
                    if (res == false || res.Result == null)
                    {
                        if (_dataList.ContainsKey(key) == false) _dataList.Add(key, new ConfigData(false, null));

                        _dataList[key].IsUpdate = false;
                        _dataList[key].Value = string.Empty;

                        return null;
                    }

                    if (_dataList.ContainsKey(key) == false) _dataList.Add(key, new ConfigData(false, res.Result.Value));

                    _dataList[key].IsUpdate = false;
                    _dataList[key].Value = res.Result.Value;

                    return res.Result.Value;
                }
            }
        }

        protected void Upsert(string group, string name, object value)
        {
            try
            {
                lock (_locker)
                {
                    var key = string.Format("{0}_{1}", group, name);

                    var data = value == null ? string.Empty : value.ToString();

                    if (_dataList.ContainsKey(key) == false)
                    {
                        _dataList.Add(key, new ConfigData(true, data));
                    }
                    else
                    {
                        if (_dataList[key].Value == data) return;
                    }

                    _dataList[key].IsUpdate = true;

                    using (var biz = new ConfigRTx())
                    {
                        var res = biz.Upsert(group, name, value);
                        if (res == false) Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private bool ToBool(string value)
        {
            if (value == "0") return false;
            else if (value == "1") return true;

            if (bool.TryParse(value, out bool result) == false) return false;

            return result;
        }
    }

    internal class ConfigData
    {
        public bool IsUpdate { get; set; }

        public string Value { get; set; }

        public ConfigData(bool isUpdate, object value)
        {
            this.IsUpdate = isUpdate;
            this.Value = value == null ? string.Empty : value.ToString();
        }
    }
}
