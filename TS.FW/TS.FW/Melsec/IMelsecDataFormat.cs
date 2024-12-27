using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Melsec
{
    public abstract class IMelsecDataFormat
    {
        protected readonly static Dictionary<Type, Dictionary<PropertyInfo, MelsecAttribute>> _infoList = new Dictionary<Type, Dictionary<PropertyInfo, MelsecAttribute>>();

        static IMelsecDataFormat()
        {
            LoadAssemlby();
        }

        public bool CheckDataChanged(IMelsecDataFormat item)
        {
            if (this.GetType() != item.GetType()) return false;

            var list = _infoList[this.GetType()];

            foreach (var info in list)
            {
                var a = info.Key.GetValue(this);
                var b = info.Key.GetValue(item);

                switch (info.Value.Format)
                {
                    case MDataFormat.Bool:
                        {
                            var temp1 = (bool)Convert.ChangeType(a, typeof(bool));
                            var temp2 = (bool)Convert.ChangeType(b, typeof(bool));

                            if (temp1 != temp2) return true;
                        }
                        break;
                    case MDataFormat.Shot:
                        {
                            var temp1 = (short)Convert.ChangeType(a, typeof(short));
                            var temp2 = (short)Convert.ChangeType(b, typeof(short));

                            if (temp1 != temp2) return true;
                        }
                        break;
                    case MDataFormat.Int:
                        {
                            var temp1 = (int)Convert.ChangeType(a, typeof(int));
                            var temp2 = (int)Convert.ChangeType(b, typeof(int));

                            if (temp1 != temp2) return true;
                        }
                        break;
                    case MDataFormat.Long:
                        {
                            var temp1 = (long)Convert.ChangeType(a, typeof(long));
                            var temp2 = (long)Convert.ChangeType(b, typeof(long));

                            if (temp1 != temp2) return true;
                        }
                        break;
                    case MDataFormat.Flat:
                        {
                            var temp1 = (float)Convert.ChangeType(a, typeof(float));
                            var temp2 = (float)Convert.ChangeType(b, typeof(float));

                            if (temp1 != temp2) return true;
                        }
                        break;
                    case MDataFormat.Double:
                        {
                            var temp1 = (double)Convert.ChangeType(a, typeof(double));
                            var temp2 = (double)Convert.ChangeType(b, typeof(double));

                            if (temp1 != temp2) return true;
                        }
                        break;
                    case MDataFormat.String:
                        {
                            var temp1 = (string)Convert.ChangeType(a, typeof(string));
                            var temp2 = (string)Convert.ChangeType(b, typeof(string));

                            if (string.Equals(temp1, temp2, StringComparison.OrdinalIgnoreCase) == false) return true;
                        }
                        break;
                    case MDataFormat.Binary:
                        {
                            var temp1 = (short[])Convert.ChangeType(a, typeof(short[]));
                            var temp2 = (short[])Convert.ChangeType(b, typeof(short[]));

                            if (temp1.SequenceEqual(temp2) == false) return true;
                        }
                        break;
                    default: return false;
                }
            }

            return false;
        }

        public virtual void SetData(MelsecDataList list, int start)
        {
            var type = this.GetType();

            if (_infoList.ContainsKey(type) == false) return;

            foreach (var item in _infoList[type])
            {
                switch (item.Value.Format)
                {
                    case MDataFormat.Bool:
                        {
                            var data = list.ToConvert<bool>(start);
                            item.Key.SetValue(this, data);

                            start += item.Value.Lenght;
                        }
                        break;
                    case MDataFormat.Shot:
                        {
                            var data = list.ToConvert<short>(start);
                            item.Key.SetValue(this, data);

                            start += item.Value.Lenght;
                        }
                        break;
                    case MDataFormat.Int:
                        {
                            var data = list.ToConvert<int>(start);
                            item.Key.SetValue(this, data);

                            start += item.Value.Lenght;
                        }
                        break;
                    case MDataFormat.Long:
                        {
                            var data = list.ToConvert<long>(start);
                            item.Key.SetValue(this, data);

                            start += item.Value.Lenght;
                        }
                        break;
                    case MDataFormat.Flat:
                        {
                            var data = list.ToConvert<float>(start);
                            item.Key.SetValue(this, data);

                            start += item.Value.Lenght;
                        }
                        break;
                    case MDataFormat.Double:
                        {
                            var data = list.ToConvert<float>(start);
                            item.Key.SetValue(this, data);

                            start += item.Value.Lenght;
                        }
                        break;
                    case MDataFormat.String:
                        {
                            var data = list.ToConvert<string>(start, item.Value.Lenght);
                            item.Key.SetValue(this, data);

                            start += item.Value.Lenght;
                        }
                        break;                        
                    case MDataFormat.Binary:
                        {
                            var data = list.ToData(start, item.Value.Lenght);

                            item.Key.SetValue(this, data.Select(t => t.Data).ToArray());
                            start += item.Value.Lenght;
                        }
                        break;
                    default:
                        {
                            start += item.Value.Lenght;
                        }
                        break;
                }
            }
        }

        public int ToLenght()
        {
            var type = this.GetType();

            if (_infoList.ContainsKey(type) == false) return 0;

            return _infoList[type].Values.Select(t => t.Lenght).Sum();
        }

        public short[] ToBuffer()
        {
            var type = this.GetType();

            if (_infoList.ContainsKey(type) == false) return new short[0];

            var list = new List<short>();

            foreach (var item in _infoList[type])
            {
                switch (item.Value.Format)
                {
                    case MDataFormat.Bool:
                        {
                            var data = (bool)item.Key.GetValue(this);

                            list.Add((short)(data ? 1 : 0));
                        }
                        break;
                    case MDataFormat.Shot:
                        {
                            var data = (short)item.Key.GetValue(this);

                            list.Add(data);
                        }
                        break;
                    case MDataFormat.Int:
                        {
                            var data = (int)item.Key.GetValue(this);

                            foreach (var temp in data.ToByte().ToInt16List(0, 2))
                            {
                                list.Add(temp);
                            }
                        }
                        break;
                    case MDataFormat.Long:
                        {
                            var data = (long)item.Key.GetValue(this);

                            foreach (var temp in data.ToByte().ToInt16List(0, 4))
                            {
                                list.Add(temp);
                            }
                        }
                        break;
                    case MDataFormat.Flat:
                        {
                            var data = (float)item.Key.GetValue(this);

                            foreach (var temp in data.ToByte().ToInt16List(0, 2))
                            {
                                list.Add(temp);
                            }
                        }
                        break;
                    case MDataFormat.Double:
                        {
                            var data = (double)item.Key.GetValue(this);

                            foreach (var temp in data.ToByte().ToInt16List(0, 4))
                            {
                                list.Add(temp);
                            }
                        }
                        break;
                    case MDataFormat.String:
                        {
                            var data = (string)item.Key.GetValue(this);

                            if (data == null) data = string.Empty;

                            var buffe = Encoding.ASCII.GetBytes(data.PadRight(item.Value.Lenght * 2, ' '));

                            foreach (var temp in buffe.ToInt16List(0, buffe.Length / 2))
                            {
                                list.Add(temp);
                            }
                        }
                        break;
                    case MDataFormat.Binary:
                        {
                            var data = (short[])item.Key.GetValue(this);

                            foreach (var temp in data)
                            {
                                list.Add(temp);
                            }
                        }
                        break;
                    default:
                        {
                            for (int i = 0; i < item.Value.Lenght; i++)
                            {
                                list.Add(0);
                            }
                        }
                        break;
                }
            }

            return list.ToArray();
        }

        public static void LoadAssemlby()
        {
            foreach (var type in Assembly.GetEntryAssembly().GetTypes()
                .Where(t => t.IsAbstract == false && t.BaseType == typeof(IMelsecDataFormat))
                .Select(t => t))
            {
                if (_infoList.ContainsKey(type) == true) continue;

                var list = new Dictionary<PropertyInfo, MelsecAttribute>();

                foreach (var info in type.GetProperties())
                {
                    var at = info.GetCustomAttribute<MelsecAttribute>();
                    if (at == null) continue;

                    list.Add(info, at);
                }

                _infoList.Add(type, list.OrderBy(t => t.Value.No).ToDictionary(t => t.Key, t => t.Value));
            }
        }

        public static int ToLenght<T>() => _infoList[typeof(T)].Values.Select(t => t.Lenght).Sum();

        public static Dictionary<PropertyInfo, MelsecAttribute> GetInfo<T>() => _infoList[typeof(T)];
    }

    public enum MDataFormat
    {
        None,
        Bool,
        Shot,
        Int,
        Long,
        Flat,
        Double,
        String,
        Binary,
    }
}
