using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin;
using TS.FW.Device.Dto.DInOut;
using TS.FW.Device.Simulation;
using TS.FW.Diagnostics;
using TS.FW.Utils;

namespace TS.FW.Device.Test.Managers
{
    public partial class InOutManager
    {
        public const int MODULE_COUNT = 3;
        public const int MODULE_BIT_COUNT = 32;

        public static InOutManager Ins { get; private set; }

        static InOutManager()
        {
            Ins = new InOutManager();
        }

        private readonly string FILE_PATH = "";
        private readonly object _dbLock = new object();

        private BackgroundTimer _trUpdate = new BackgroundTimer(System.Threading.ApartmentState.MTA);

        private IDInOut IO => DeviceManager.Ins.IO;

        private readonly Dictionary<string, InOutModel> _InList = new Dictionary<string, InOutModel>();
        private readonly Dictionary<string, InOutModel> _OutList = new Dictionary<string, InOutModel>();

        public List<InOutModel> InList => this._InList.Values.ToList();

        public List<InOutModel> OutList => this._OutList.Values.ToList();

        public InOutManager()
        {
            this.FILE_PATH = Path.Combine(Environment.CurrentDirectory, "Asstes", "InOut.ini");

            this._trUpdate.SleepTimeMsc = 50;
            this._trUpdate.DoWork += _trUpdate_DoWork;
        }

        public bool Start()
        {
            try
            {
                if (this.LoadDatabase() == false) return false;

                this.UpdateOutList();

                this._trUpdate.Start();

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool ReadX([CallerMemberName] string key = null)
        {
            var item = this.GetInData(key);
            if (item == null || item.BitNo == -1) throw new Exception(string.Format("[IN] = '{0}'", key));

            return item.OnOff;
        }

        public bool ReadY([CallerMemberName] string key = null)
        {
            var item = this.GetOutData(key);
            if (item == null || item.BitNo == -1) throw new Exception(string.Format("[OUT] = '{0}'", key));

            return item.OnOff;
        }

        public void WriteX(bool onOff, [CallerMemberName] string key = null)
        {
            var item = this.GetInData(key);
            if (item == null || item.BitNo == -1) throw new Exception(string.Format("[IN] = '{0}'", key));

            this.IO.WriteBit(item.ModuleNo, item.BitNo, item.IsAType ? (onOff ? eSignal.ON : eSignal.OFF) : (onOff ? eSignal.OFF : eSignal.ON));
        }

        public void WriteY(bool onOff, [CallerMemberName] string key = null)
        {
            var item = this.GetOutData(key);
            if (item == null || item.BitNo == -1) throw new Exception(string.Format("[OUT] = '{0}'", key));

            if (item.OnOff == onOff) return;

            this.IO.WriteBit(item.ModuleNo, item.BitNo, item.IsAType ? (onOff ? eSignal.ON : eSignal.OFF) : (onOff ? eSignal.OFF : eSignal.ON));
        }

        public List<InOutModel> ReadDataBase(enInOutType type)
        {
            if (File.Exists(FILE_PATH) == false) return new List<InOutModel>();

            var iniList = IniFileHandler.GetIniFile_ToDictionary(FILE_PATH, type == enInOutType.IN ? "X" : "Y", string.Empty);
            if (iniList == false) throw new Exception(iniList.Comment);

            return this.ToInOutModel(iniList.Result, type);
        }

        public IEnumerable<InOutModel> ToInOutModelList(enInOutType type, int port, int portNum, int bitCount)
        {
            var totalNum = port + portNum;

            for (; port < totalNum; port++)
            {
                for (int bitNo = 0; bitNo < bitCount; bitNo++)
                {
                    var no = bitNo + (port * bitCount);
                    var name = string.Format("Spare {0}", no);

                    yield return new InOutModel(name)
                    {
                        ModuleNo = port,
                        BitNo = bitNo,
                        DisplayName = "Spare",
                        Type = type,
                        OutputType = enOutputType.A,
                        OnOff = false,
                    };
                }
            }
        }

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                this.UpdateInList();
                this.UpdateOutList();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private bool LoadDatabase()
        {
            try
            {
                this.InitDataBase();

                this.LoadDatabase(this._InList, enInOutType.IN, 0);
                this.LoadDatabase(this._OutList, enInOutType.OUT, 3);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        private void InitDataBase()
        {
            try
            {
                var inList = this.ReadDataBase(enInOutType.IN);
                var outList = this.ReadDataBase(enInOutType.OUT);

                if (File.Exists(FILE_PATH)) File.Delete(FILE_PATH);

                foreach (var info in this.GetType().GetProperties())
                {
                    this.CallbackPropertyInfo(info, inList, outList);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void LoadDatabase(Dictionary<string, InOutModel> list, enInOutType type, int sPortNo)
        {
            lock (this._dbLock)
            {
                list.Clear();

                var temp = this.ReadDataBase(type);

                foreach (var spare in this.ToInOutModelList(type, sPortNo, MODULE_COUNT, MODULE_BIT_COUNT))
                {
                    var item = temp.FirstOrDefault(t => t.ModuleNo == spare.ModuleNo && t.BitNo == spare.BitNo);
                    if (item == null)
                    {
                        list.Add(spare.Key, spare);
                    }
                    else
                    {
                        list.Add(item.Key, item);
                    }
                }
            }
        }

        private List<InOutModel> ToInOutModel(Dictionary<string, string> list, enInOutType type)
        {
            var temp = new List<InOutModel>();

            foreach (var item in list)
            {
                try
                {
                    var data = new InOutModel(item.Key);
                    data.Type = type;
                    data.SetContents(item.Value);

                    temp.Add(data);
                }
                catch (Exception ex)
                {
                    Logger.Write(this, ex);
                }
            }

            return temp;
        }

        private void CallbackPropertyInfo(PropertyInfo info, List<InOutModel> inList, List<InOutModel> outList)
        {
            var at = info.GetCustomAttribute<InOutAttribute>();
            if (at == null) return;

            switch (at.InOutType)
            {
                case enInOutType.IN:
                    {
                        var item = inList.FirstOrDefault(t => t.Key == info.Name);
                        if (item != null)
                        {
                            item.DisplayName = at.DisplayName;
                            this.SetDatabase(item);
                        }
                        else
                        {
                            var no = at.BitNo;
                            var moduleNo = no / MODULE_BIT_COUNT;
                            var bitNo = no % MODULE_BIT_COUNT;

                            this.SetDatabase(new InOutModel(info.Name)
                            {
                                Type = enInOutType.IN,
                                DisplayName = at.DisplayName,

                                ModuleNo = moduleNo,
                                BitNo = bitNo,
                                OutputType = at.OutputType,
                            });
                        }

                    }
                    break;
                case enInOutType.OUT:
                    {
                        var item = outList.FirstOrDefault(t => t.Key == info.Name);
                        if (item != null)
                        {
                            item.DisplayName = at.DisplayName;
                            this.SetDatabase(item);
                        }
                        else
                        {
                            var no = at.BitNo;
                            var moduleNo = no / MODULE_BIT_COUNT;
                            var bitNo = no % MODULE_BIT_COUNT;

                            this.SetDatabase(new InOutModel(info.Name)
                            {
                                Type = enInOutType.OUT,
                                DisplayName = at.DisplayName,

                                ModuleNo = moduleNo,
                                BitNo = bitNo,
                                OutputType = at.OutputType,
                            });
                        }
                    }
                    break;
            }
        }

        public void SetDatabase(InOutModel data)
        {
            if (string.IsNullOrEmpty(FILE_PATH)) return;
            if (Path.GetExtension(FILE_PATH) != ".ini") return;

            if (File.Exists(FILE_PATH) == false) File.WriteAllText(FILE_PATH, "");

            var key = data.Key;//this.ToKey(data.Key);
            var contents = data.GetContents();

            var res = IniFileHandler.SetIniFile_Value(FILE_PATH, data.Type == enInOutType.IN ? "X" : "Y", key, contents);
            if (res == false) throw new Exception(res.Comment);

            if (data.Type == enInOutType.IN)
            {
                this.SetDatabase(this._InList, data);
            }
            else
            {
                this.SetDatabase(this._OutList, data);
            }
        }

        private void SetDatabase(Dictionary<string, InOutModel> list, InOutModel data)
        {
            lock (this._dbLock)
            {
                if (list.ContainsKey(data.Key) == false)
                {
                    if (list.Any(t => t.Value.BitNo == data.BitNo))
                    {
                        var item = list.FirstOrDefault(t => t.Value.BitNo == data.BitNo);
                        list.Remove(item.Key);
                    }

                    list.Add(data.Key, data);
                }
                else
                {
                    list[data.Key] = data;
                }
            }
        }

        private InOutModel GetInData(string key)
        {
            if (this._InList.ContainsKey(key) == false) return null;

            return this._InList[key];
        }

        private InOutModel GetOutData(string key)
        {
            if (this._OutList.ContainsKey(key) == false) return null;

            return this._OutList[key];
        }

        private void UpdateInList()
        {
            if (this.IO == null) return;

            foreach (var list in this.InList.GroupBy(t => t.ModuleNo))
            {
                var readItem = this.ReadIn(list.Key);
                if (readItem == null) continue;

                foreach (var item in list)
                {
                    var bit = readItem[item.ModuleNo, item.BitNo];
                    if (bit == null) continue;

                    if (item.Signal != bit.Signal)
                    {
                        item.OnOff_Org = bit.Signal == eSignal.ON;
                    }
                }
            }
        }

        private void UpdateOutList()
        {
            if (this.IO == null) return;

            foreach (var list in this.OutList.GroupBy(t => t.ModuleNo))
            {
                var readItem = this.ReadOut(list.Key);
                if (readItem == null) continue;

                foreach (var item in list)
                {
                    var bit = readItem[item.ModuleNo, item.BitNo];
                    if (bit == null) continue;

                    if (item.Signal != bit.Signal)
                    {
                        item.OnOff_Org = bit.Signal == eSignal.ON;
                    }
                }
            }
        }

        /// <summary>
        /// 전체 Out 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        private IDInOutBase ReadOut(int moduleNo)
        {
            switch (MODULE_BIT_COUNT)
            {
                case 8: return this.ReadOut8Bit(moduleNo);
                case 16: return this.ReadOut16Bit(moduleNo);
                case 32: return this.ReadOut32Bit(moduleNo);
                default: return null;
            }
        }

        /// <summary>
        /// 전체 In 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        private IDInOutBase ReadIn(int moduleNo)
        {
            switch (MODULE_BIT_COUNT)
            {
                case 8: return this.ReadIn8Bit(moduleNo);
                case 16: return this.ReadIn16Bit(moduleNo);
                case 32: return this.ReadIn32Bit(moduleNo);
                default: return null;
            }
        }

        /// <summary>
        /// 8점 in 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        public DInOutByte ReadIn8Bit(int moduleNo, int offset = 0)
        {
            var res = this.IO.ReadByteIn(moduleNo, offset);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Warning);
                return null;
            }

            return res.Result;
        }

        /// <summary>
        /// 16점 In 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        public DInOutWord ReadIn16Bit(int moduleNo, int offset = 0)
        {
            var res = this.IO.ReadWordIn(moduleNo, offset);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Warning);
                return null;
            }

            return res.Result;
        }

        /// <summary>
        /// 32점 In 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        public DInOutDWord ReadIn32Bit(int moduleNo)
        {
            var res = this.IO.ReadDWordIn(moduleNo);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Warning);
                return null;
            }

            return res.Result;
        }

        /// <summary>
        /// 8점 in 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        public DInOutByte ReadOut8Bit(int moduleNo, int offset = 0)
        {
            var res = this.IO.ReadByteOut(moduleNo, offset);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Warning);
                return null;
            }

            return res.Result;
        }

        /// <summary>
        /// 16점 In 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        public DInOutWord ReadOut16Bit(int moduleNo, int offset = 0)
        {
            var res = this.IO.ReadWordOut(moduleNo, offset);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Warning);
                return null;
            }

            return res.Result;
        }

        /// <summary>
        /// 32점 In 읽기
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        public DInOutDWord ReadOut32Bit(int moduleNo)
        {
            var res = this.IO.ReadDWordOut(moduleNo);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Warning);
                return null;
            }

            return res.Result;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class InOutAttribute : Attribute
    {
        public int BitNo { get; private set; }

        public string DisplayName { get; private set; }

        public enInOutType InOutType { get; private set; }

        public enOutputType OutputType { get; private set; }

        public InOutAttribute(string displayName, enInOutType type, int no = -1, enOutputType outPutType = enOutputType.A)
        {
            this.DisplayName = displayName;
            this.InOutType = type;
            this.BitNo = no;
            this.OutputType = outPutType;
        }
    }

    public class InOutModel : Wpf.Core.ModelBase
    {
        public string Key { get => this.GetValue<string>(); set => this.SetValue(value); }

        public int DisplayNo => this.BitNo + (this.ModuleNo * InOutManager.MODULE_BIT_COUNT);

        public int ModuleNo { get => this.GetValue<int>(); set => this.SetValue(value); }

        public int BitNo { get => this.GetValue<int>(); set => this.SetValue(value); }

        public string DisplayName { get => this.GetValue<string>(); set => this.SetValue(value); }

        public enInOutType Type { get => this.GetValue<enInOutType>(); set => this.SetValue(value); }

        public enOutputType OutputType { get => this.GetValue<enOutputType>(); set => this.SetValue(value); }

        public bool OnOff { get => this.GetOnOff(); set => this.SetValue(value); }

        public bool OnOff_Org { get => this.GetValue<bool>(nameof(this.OnOff)); set => this.OnOff = value; }

        public bool IsAType { get => this.OutputType == enOutputType.A; }

        public eSignal Signal => this.OnOff ? eSignal.ON : eSignal.OFF;

        public InOutModel(string key)
        {
            this.Key = key;
            this.OnOff = false;
            this.OutputType = enOutputType.A;
        }

        private bool GetOnOff()
        {
            var onOff = this.OnOff_Org;

            return this.IsAType ? onOff : !onOff;
        }

        public string GetContents()
        {
            return string.Format("{0}|{1}|{2}", this.BitNo + (this.ModuleNo * InOutManager.MODULE_BIT_COUNT), string.IsNullOrEmpty(this.DisplayName) ? this.Key : this.DisplayName, this.OutputType);
        }

        public void SetContents(string contents)
        {
            var tokens = contents.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 3) throw new Exception(contents);

            var no = Convert.ToInt32(tokens[0].Trim());

            this.ModuleNo = no / InOutManager.MODULE_BIT_COUNT;
            this.BitNo = no % InOutManager.MODULE_BIT_COUNT;
            this.DisplayName = tokens[1].Trim();
            this.OutputType = (enOutputType)Enum.Parse(typeof(enOutputType), tokens[2].Trim());
        }

        public override string ToString()
        {
            return string.Format("{0}/{1} = {2}", this.ModuleNo, this.BitNo, this.DisplayName);
        }

        protected override void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                base.SetValue(value, propertyName);
            }
            finally
            {
                if (string.Equals(propertyName, nameof(this.OutputType)))
                {
                    this.OnNotifyPropertyChanged(nameof(this.IsAType));
                    this.OnNotifyPropertyChanged(nameof(this.OnOff));
                }
            }
        }
    }

    public enum enInOutType
    {
        IN,
        OUT,
    }

    public enum enOutputType
    {
        A,
        B,
    }
}
