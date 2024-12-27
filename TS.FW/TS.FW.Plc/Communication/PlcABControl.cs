using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Core;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;
using TS.FW.Utils;

namespace TS.FW.Plc.Communication
{
    public class PlcABControl : IPlcControl
    {
        private PlcABConfig _Config = null;
        private PlcOpcClient _Client = null;
        private BackgroundTimer _trTick = null;

        public event EventHandler<bool> OnConnectedChangedEvent;
        public event EventHandler<IPlcConfigBase> OnTickEvent;

        public List<PlcOpcData> PlcOpcBitDataList { get; private set; } = new List<PlcOpcData>();

        public List<PlcOpcData> PlcOpcWordDataList { get; private set; } = new List<PlcOpcData>();

        public bool Connected
        {
            get
            {
                return _Client.Server.IsConnected;
            }
        }

        public void ChangeConfig(IPlcConfigBase config)
        {
            if (this._Config == null) return;

            this.SetConfig(config);
            this._Client.ConfigChange(this._Config.OpcName);
        }

        public void Connect(IPlcConfigBase config)
        {
            if (this._Client != null) return;

            this.SetConfig(config);

            this._Client = new PlcOpcClient(this.PlcOpcBitDataList, this.PlcOpcWordDataList, this._Config.OpcName);
            this._Client.OnConnectedEvent += _Client_OnConnectedEvent;
            this._Client.OnDisconnectedEvent += _Client_OnDisconnectedEvent;

            this._Client.Start();

            if (this._trTick == null)
            {
                this._trTick = new BackgroundTimer();
                this._trTick.DoWork += _trTick_DoWork;
                this._trTick.SleepTimeMsc = IPlcConfigBase.TICK_INTERVAL;
            }

            this._trTick.Start();
        }

        public void DisConnect()
        {
            if (this._Client != null)
            {
                this._Client.Stop();

                this._Client.OnConnectedEvent -= _Client_OnConnectedEvent;
                this._Client.OnDisconnectedEvent -= _Client_OnDisconnectedEvent;
            }

            this._Config = null;
            this._Client = null;

            if (this._trTick != null)
            {
                this._trTick.Stop();
            }

            this._trTick = null;
        }

        public Response<PlcBit.Signal> ReadBit(PlcBit bit)
        {
            try
            {
                if (bit == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                return this._Client.ReadBit(bit);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcBit, PlcBit.Signal> ReadBitList(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Address);
                var result = new Dictionary<PlcBit, PlcBit.Signal>();

                foreach (var bit in orderByList)
                {
                    var res = this._Client.ReadBit(bit);
                    if (res == false) continue;

                    result.Add(bit, res.Result);
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcBit, PlcBit.Signal> ReadBitFromWordCommand(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Address);
                var result = new Dictionary<PlcBit, PlcBit.Signal>();

                foreach (var bit in orderByList)
                {
                    var res = this._Client.ReadBit(bit);
                    if (res == false) continue;

                    result.Add(bit, res.Result);
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<object> ReadWord(PlcWord word)
        {
            try
            {
                if (word == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                var res = this._Client.ReadWord(word);
                if (res == false) return new Response<object>(false, res.Comment);

                return new Response<object>(res.Result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcWord, object> ReadWordList(IEnumerable<PlcWord> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Address).ToList();
                var result = new Dictionary<PlcWord, object>();

                foreach (var word in list)
                {
                    var res = this._Client.ReadWord(word);
                    if (res == false) continue;

                    result.Add(word, res.Result);
                }

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcWord, object> ReadRandomWord(IEnumerable<PlcWord> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Address).ToList();
                var result = new Dictionary<PlcWord, object>();

                foreach (var word in list)
                {
                    var res = this._Client.ReadWord(word);
                    if (res == false) continue;

                    result.Add(word, res.Result);
                }

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteBit(PlcBit bit, PlcBit.Signal signal)
        {
            try
            {
                if (bit == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                return this._Client.WriteBit(bit, signal);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteBitList(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Key.Address).ToList();

                foreach (var bit in orderByList)
                {
                    var res = this._Client.WriteBit(bit.Key, bit.Value);
                    if (res == false) return new Response(false, res.Comment);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteBitFromWordCommand(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Key.Address).ToList();

                foreach (var bit in orderByList)
                {
                    var res = this._Client.WriteBit(bit.Key, bit.Value);
                    if (res == false) return new Response(false, res.Comment);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteRandomBit(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Key.Address).ToList();

                foreach (var bit in orderByList)
                {
                    var res = this._Client.WriteBit(bit.Key, bit.Value);
                    if (res == false) return new Response(false, res.Comment);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteRandomWord(Dictionary<PlcWord, object> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Key.Address).ToList();

                foreach (var word in orderByList)
                {
                    var buffer = word.Key.ToByte(word.Value).ToInt16List(0, word.Key.Length).ToArray();

                    var res = this._Client.WriteWord(word.Key, buffer);
                    if (res == false) return new Response(false, res.Comment);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteWord(PlcWord word, object value)
        {
            try
            {
                if (word == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                var buffer = word.ToByte(value).ToInt16List(0, word.Length).ToArray();

                return this._Client.WriteWord(word, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteWordList(Dictionary<PlcWord, object> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Key.Address).ToList();

                foreach (var word in orderByList)
                {
                    var buffer = word.Key.ToByte(word.Value).ToInt16List(0, word.Key.Length).ToArray();

                    var res = this._Client.WriteWord(word.Key, buffer);
                    if (res == false) return new Response(false, res.Comment);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// TODO :  굳이 컨트롤에서 해 줄 필요가 없음. 클라이언트나 OpcData에서 따로 생성.
        /// </summary>
        /// <param name="config"></param>
        private void SetConfig(IPlcConfigBase config)
        {
            if (config == null) throw new NullReferenceException("PLC AB 통신 설정 정보가 NULL 입니다.");
            if (config is PlcABConfig == false) throw new TypeAccessException(string.Format("PLC AB 통신 설정 정보가 잘못되었습니다. {0}", config));

            this._Config = config as PlcABConfig;

            foreach (var item in this._Config.TotalBitList)
            {
                if (item.Key.StartsWith("ACK_GLASS_IN_EVENT_"))
                {
                    this.ConvertUnitInfo(item.Key, out int sectionNo, out int unitNo);
                    PlcOpcBitDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{3}]{0}{1}[0].{2}", item.Key.Substring(0, "ACK_GLASS_IN_EVENT_".Length), sectionNo, unitNo - 1, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
                else if (item.Key.StartsWith("ACK_GLASS_OUT_EVENT_"))
                {
                    this.ConvertUnitInfo(item.Key, out int sectionNo, out int unitNo);
                    PlcOpcBitDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{3}]{0}{1}[0].{2}", item.Key.Substring(0, "ACK_GLASS_OUT_EVENT_".Length), sectionNo, unitNo - 1, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
                else if (item.Key.StartsWith("IS_GLASS_CONTAIN_"))
                {
                    this.ConvertUnitInfo(item.Key, out int sectionNo, out int unitNo);
                    PlcOpcBitDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{3}]{0}{1}[0].{2}", item.Key.Substring(3, "GLASS_CONTAIN_".Length), sectionNo, unitNo - 1, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
                else if (item.Key.StartsWith("GLASS_IN_EVENT_"))
                {
                    this.ConvertUnitInfo(item.Key, out int sectionNo, out int unitNo);
                    PlcOpcBitDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{3}]{0}{1}[0].{2}", item.Key.Substring(0, "GLASS_IN_EVENT_".Length), sectionNo, unitNo - 1, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
                else if (item.Key.StartsWith("GLASS_OUT_EVENT_"))
                {
                    this.ConvertUnitInfo(item.Key, out int sectionNo, out int unitNo);
                    PlcOpcBitDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{3}]{0}{1}[0].{2}", item.Key.Substring(0, "GLASS_OUT_EVENT_".Length), sectionNo, unitNo - 1, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
                else
                {
                    PlcOpcBitDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{1}]{0}", item.Value.Name, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
            }

            foreach (var item in this._Config.TotalWordList)
            {
                if (item.Key.StartsWith("PROC_STATE_UNIT_"))
                {
                    this.ConvertUnitInfo(item.Key, out int sectionNo, out int unitNo);
                    PlcOpcWordDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{3}]W_{0}{1}[{2}]", item.Key.Substring(0, "PROC_STATE_UNIT_".Length), sectionNo, unitNo - 1, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
                else if (item.Key.StartsWith("EQP_STATE_UNIT_"))
                {
                    this.ConvertUnitInfo(item.Key, out int sectionNo, out int unitNo);
                    PlcOpcWordDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{3}]W_{0}{1}[{2}]", item.Key.Substring(0, "EQP_STATE_UNIT_".Length), sectionNo, unitNo - 1, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
                else if (!(item.Key.StartsWith("GLASS_")))
                {
                    PlcOpcWordDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = string.Format("[{1}]W_{0}", item.Value.Name, this._Config.OpcName),
                        DataName = item.Key,
                    });
                }
            }
        }

        private void ConvertUnitInfo(string name, out int sectionNo, out int unitNo)
        {
            var temp = Regex.Replace(name, @"\D", "_").Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
            sectionNo = Convert.ToInt32(temp[0]);
            unitNo = Convert.ToInt32(temp[1]);
        }

        /// <summary>
        /// PlcOpcDataList가 null인지 확인.
        /// </summary>
        private void IsInitializeCheck()
        {
            if (this._Config == null) throw new Exception("PLC 통신 환경설정 정보가 존재하지 않습니다.");
            if (this._Client == null) throw new Exception(string.Format("PLC 통신 컨트롤이 초기화 되지 않았습니다. {0}", this._Config));
            if (this._Client.Connected == false) throw new Exception(string.Format("PLC 통신이 연결 되어 있지 않습니다. {0}", this._Config));
        }

        private void _Client_OnConnectedEvent(object sender, EventArgs e)
        {
            try
            {
                this.OnConnectedChangedEvent?.Invoke(this._Config, true);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _Client_OnDisconnectedEvent(object sender, EventArgs e)
        {
            try
            {
                this.OnConnectedChangedEvent?.Invoke(this._Config, false);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _trTick_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                this.OnTickEvent?.Invoke(this, this._Config);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

    }
}