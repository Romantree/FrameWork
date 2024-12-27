using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Plc;
using TS.FW.Plc.Config;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;

namespace TS.FW.Plc.Test.Managers
{
    public class PlcManager
    {
        public static PlcManager Ins { get; private set; }

        static PlcManager() => Ins = new PlcManager();

        private readonly BackgroundTimer _trProcess = new BackgroundTimer();

        private readonly Dictionary<IPlcConfigBase, List<IPlcDataBase>> _changeList = new Dictionary<IPlcConfigBase, List<IPlcDataBase>>();

        public event EventHandler<IPlcConfigBase> OnConnectedChanged;

        public event EventHandler<BitChangedEventArg> OnBitChanged;
        public event EventHandler<WordChangedEventArg> OnWordChanged;

        public event EventHandler<BitChangedEventArg> OnOutBitChanged;
        public event EventHandler<WordChangedEventArg> OnOutWordChanged;

        public bool IsInitOutData { get; set; } = true;

        public PlcControlManager Control => PlcControlManager.Ins;

        private PlcManager()
        {
            this.Control.OnConnectedEvent += Control_OnConnectedEvent;
            this.Control.OnDisconnectedEvent += Control_OnDisconnectedEvent;
            this.Control.OnTickEvent += Control_OnTickEvent;

            this._trProcess.SleepTimeMsc = 100;
            this._trProcess.DoWork += _trProcess_DoWork;
            this._trProcess.Start();
        }

        private void Control_OnConnectedEvent(object sender, EventArgs e)
        {
            try
            {
                var config = sender as IPlcConfigBase;
                if (config == null) return;

                this.OnConnectedChanged?.Invoke(this, config);

                if(this.IsInitOutData == true)
                {
                    this.InitOutBitList(config);
                    this.InitOutWordList(config);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Control_OnDisconnectedEvent(object sender, EventArgs e)
        {
            try
            {
                var config = sender as IPlcConfigBase;
                if (config == null) return;

                this.OnConnectedChanged?.Invoke(this, config);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Control_OnTickEvent(object sender, IPlcConfigBase e)
        {
            try
            {
                if (this.Control.IsConnected(e.PlcNo) == false) return;

                this.ReadPlcData(e); // 기본
                //this.ReadPlcDataAsync(e); // 고성능 필요할때...(데이터가 많으면 더 느려짐)
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void ReadPlcData(IPlcConfigBase config)
        {
            try
            {
                if (config == null) return;

                this.BitChanged(config);
                this.WordChanged(config);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private async void ReadPlcDataAsync(IPlcConfigBase config)
        {
            try
            {
                if (config == null) return;

                await Task.Delay(10);

                lock (config)
                {
                    this.BitChanged(config);
                    this.WordChanged(config);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void BitChanged(IPlcConfigBase config)
        {
            var dt = DateTime.Now;

            try
            {
                var cList = this.ReadBitAll(config);
                if (cList == null || cList.Count <= 0) return;

                if (this._changeList.ContainsKey(config) == false) this._changeList.Add(config, new List<IPlcDataBase>());

                lock (this._changeList[config])
                {
                    this._changeList[config].AddRange(cList.Select(t => t.Key));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                config.ReadBitTime = DateTime.Now - dt;
            }
        }

        private List<KeyValuePair<PlcBit, PlcBit.Signal>> ReadBitAll(IPlcConfigBase config)
        {
            var list = config.InBitList.Values.ToList();

            var res = this.Control.ReadBitAll(config.PlcNo, list);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                return new List<KeyValuePair<PlcBit, PlcBit.Signal>>(); ;
            }

            if (res.Result == null || res.Result.Count <= 0) return new List<KeyValuePair<PlcBit, PlcBit.Signal>>();

            var cList = res.Result.Where(t => t.Key.OnOff != t.Value).ToList();
            if (cList.Count <= 0) return new List<KeyValuePair<PlcBit, PlcBit.Signal>>();

            foreach (var item in cList)
            {
                item.Key.OnOff = item.Value;
            }

            return cList;
        }

        private void WordChanged(IPlcConfigBase config)
        {
            var dt = DateTime.Now;

            try
            {
                var cList = this.ReadWordAll(config);
                if (cList == null || cList.Count <= 0) return;

                if (this._changeList.ContainsKey(config) == false) this._changeList.Add(config, new List<IPlcDataBase>());

                lock (this._changeList[config])
                {
                    this._changeList[config].AddRange(cList.Select(t => t.Key));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                config.ReadWordTime = DateTime.Now - dt;
            }
        }

        private List<KeyValuePair<PlcWord, object>> ReadWordAll(IPlcConfigBase config)
        {
            var list = config.InWordList.Values.Where(t => t.Code == PlcDeviceCode.W_LINK_REGISTER).ToList();

            var res = this.Control.ReadWordAll(config.PlcNo, PlcDeviceCode.W_LINK_REGISTER, list);
            if (res == false)
            {
                Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                return new List<KeyValuePair<PlcWord, object>>();
            }

            if (res.Result == null || res.Result.Count <= 0) return new List<KeyValuePair<PlcWord, object>>();

            var cList = res.Result.Where(t => this.WordDataEquals(t.Key.Value, t.Value) == false).ToList();
            if (cList.Count <= 0) return new List<KeyValuePair<PlcWord, object>>();

            foreach (var word in cList)
            {
                word.Key.Value = word.Value;
            }

            return cList;
        }

        private void _trProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var cList = this.Control.GetPlcConfig();

                foreach (var config in cList)
                {
                    var list = this.GetPlcData(config);
                    if (list == null || list.Count <= 0) continue;

                    this.BitProcess(config, list.Where(t => t is PlcBit).Select(t => t as PlcBit).ToList());
                    this.WordProcess(config, list.Where(t => t is PlcWord).Select(t => t as PlcWord).ToList());
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void BitProcess(IPlcConfigBase config, List<PlcBit> list)
        {
            try
            {
                if (list.Count <= 0) return;

                this.OnBitChanged?.Invoke(this, new BitChangedEventArg(config, list));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void WordProcess(IPlcConfigBase config, List<PlcWord> list)
        {
            try
            {
                if (list.Count <= 0) return;

                this.OnWordChanged?.Invoke(this, new WordChangedEventArg(config, list));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private List<IPlcDataBase> GetPlcData(IPlcConfigBase config)
        {
            if (this._changeList.ContainsKey(config) == false) return new List<IPlcDataBase>();

            lock (this._changeList[config])
            {
                var list = this._changeList[config].ToList();
                this._changeList[config].Clear();

                return list;
            }
        }

        private bool WordDataEquals(object a, object b)
        {
            if (object.Equals(a, null) && object.Equals(b, null)) return true;
            if (object.Equals(a, null)) return false;
            if (object.Equals(b, null)) return false;

            return a.GetHashCode() == b.GetHashCode();
        }

        public bool IsConnected(IPlcConfigBase config)
        {
            if (config == null) return false;

            return this.Control.IsConnected(config.PlcNo);
        }

        public void Start(IPlcConfigBase config)
        {
            if (config == null) return;
            if (this.IsConnected(config)) this.Control.Stop(config);

            this.Control.Start(config);
        }

        public void Stop(IPlcConfigBase config)
        {
            if (config == null) return;
            if (this.IsConnected(config) == false) return;

            this.Control.Stop(config);
        }

        public void WriteBitProcess(IPlcConfigBase config)
        {
            try
            {
                if (this.IsConnected(config) == false) return;

                lock (config.OutBitList)
                {
                    var outBitList = config.OutBitList.Values.Where(t => t.WriteOnOff.HasValue).ToList();
                    if (outBitList == null || outBitList.Count <= 0) return;

                    var res = this.Control.WriteBitList(config.PlcNo, 0, outBitList.Select(t => t.ToPlcWirteBitModel(t.WriteOnOff.Value)).ToArray());
                    if (res == false) return;

                    foreach (var item in outBitList)
                    {
                        item.OnOff = item.WriteOnOff.Value;
                        item.WriteOnOff = null;
                    }

                    this.OnOutBitChanged?.Invoke(this, new BitChangedEventArg(config, outBitList));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void WriteWordProcess(IPlcConfigBase config)
        {
            try
            {
                if (this.IsConnected(config) == false) return;

                lock (config.OutWordList)
                {
                    var outWordList = config.OutWordList.Values.Where(t => t.WriteData != null).ToList();
                    if (outWordList == null || outWordList.Count <= 0) return;

                    var res = this.Control.WriteWordList(config.PlcNo, 0, outWordList.Select(t => t.ToPlcWirteWordModel(t.WriteData)).ToArray());
                    if (res == false) return;

                    foreach (var item in outWordList)
                    {
                        item.Value = item.WriteData;
                        item.WriteData = null;
                    }

                    this.OnOutWordChanged?.Invoke(this, new WordChangedEventArg(config, outWordList));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void InitOutBitList(IPlcConfigBase config)
        {
            try
            {
                if (this.IsConnected(config) == false) return;

                var list = config.OutBitList.Values.ToList();

                var res = this.Control.ReadBitAll(config.PlcNo, list);
                if (res == false)
                {
                    Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                    return;
                }

                if (res.Result == null || res.Result.Count <= 0) return;

                foreach (var item in res.Result)
                {
                    item.Key.OnOff = item.Value;
                }

                this.OnOutBitChanged?.Invoke(this, new BitChangedEventArg(config, res.Result.Keys.ToList()));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void InitOutWordList(IPlcConfigBase config)
        {
            try
            {
                if (this.IsConnected(config) == false) return;

                var list = config.OutWordList.Values.Where(t => t.Code == PlcDeviceCode.W_LINK_REGISTER).ToList();

                var res = this.Control.ReadWordAll(config.PlcNo, PlcDeviceCode.W_LINK_REGISTER, list);
                if (res == false)
                {
                    Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                    return;
                }

                if (res.Result == null || res.Result.Count <= 0) return;

                foreach (var word in res.Result)
                {
                    word.Key.Value = word.Value;
                }

                this.OnOutWordChanged?.Invoke(this, new WordChangedEventArg(config, res.Result.Keys.ToList()));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class BitChangedEventArg
    {
        public IPlcConfigBase Config { get; private set; }

        public List<PlcBit> List { get; private set; }

        public BitChangedEventArg(IPlcConfigBase config, List<PlcBit> list)
        {
            this.Config = config;
            this.List = list;
        }
    }

    public class WordChangedEventArg
    {
        public IPlcConfigBase Config { get; private set; }

        public List<PlcWord> List { get; private set; }

        public WordChangedEventArg(IPlcConfigBase config, List<PlcWord> list)
        {
            this.Config = config;
            this.List = list;
        }
    }
}
