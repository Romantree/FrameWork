using Microsoft.VisualStudio.TestTools.UnitTesting;
using S7.Net;
using S7.Net.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Core;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;

namespace TS.FW.Plc.Communication
{
    public class PlcSiemensControl : IPlcControl
    {
        private const int TIMEOUT_SEC = 10;

        private PlcSiemensConfig _Config = null;
        private S7.Net.Plc plc = null;
        private BackgroundTimer _trTick = null;

        public event EventHandler<bool> OnConnectedChangedEvent;

        public event EventHandler<IPlcConfigBase> OnTickEvent;

        /// <summary>
        /// 연결 알림 이벤트
        /// </summary>
        public event EventHandler OnConnectedEvent;

        /// <summary>
        /// 연결 해제 이벤트
        /// </summary>
        public event EventHandler OnDisconnectedEvent;

        public bool Connected
        {
            get
            {
                if (this.plc == null) return false;

                return this.plc.IsConnected;
            }
        }

        public void ChangeConfig(IPlcConfigBase config)
        {
            Logger.Write(this);

            if (this._Config == null) return;

            this.SetConfig(config);
            this._Config = config as PlcSiemensConfig;
        }

        public void Connect(IPlcConfigBase config)
        {
            Logger.Write(this);

            if (this.plc != null) return;

            this.SetConfig(config);

            this.plc = new S7.Net.Plc(_Config.Cpu, _Config.IpAddress, _Config.Rack, _Config.Slot);

            this.OnConnectedEvent?.Invoke(this, new EventArgs());

            this.OnConnectedEvent += _Client_OnConnectedEvent;
            this.OnDisconnectedEvent += _Client_OnDisconnectedEvent;

            this.plc.Open();

            this.IsInitializeCheck();

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
            Logger.Write(this);

            if (this.plc != null)
            {
                this.plc.Close();

                this.OnDisconnectedEvent?.Invoke(this, new EventArgs());

                this.OnConnectedEvent += _Client_OnConnectedEvent;
                this.OnDisconnectedEvent += _Client_OnDisconnectedEvent;
            }

            this._Config = null;
            this.plc = null;

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

                object boolVariable;

                string[] bitAddress = (bit.Address * 0.1).ToString().Split('.');

                if (bitAddress.Count() != 2)
                {
                    boolVariable = plc.Read(string.Format("DB{0}.DBX{1}.0", _Config.DataBlock, bitAddress[0]));
                }
                else
                {
                    boolVariable = plc.Read(string.Format("DB{0}.DBX{1}.{2}", _Config.DataBlock, bitAddress[0], bitAddress[1]));
                }

                PlcBit.Signal bitSignal;

                if (boolVariable == null)
                {
                    bitSignal = boolVariable == null ? PlcBit.Signal.OFF : PlcBit.Signal.ON;
                }
                else
                {
                    bitSignal = (bool)boolVariable == false ? PlcBit.Signal.OFF : PlcBit.Signal.ON;
                }

                return new Response<PlcBit.Signal>(bitSignal);
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

                var dataItems = new List<DataItem>();

                foreach (var item in list)
                {
                    string[] bitAddress = (item.Address * 0.1).ToString().Split('.');

                    if (bitAddress.Count() != 2)
                    {
                        dataItems.Add(new DataItem()
                        {
                            Count = 1,
                            DataType = DataType.DataBlock,
                            DB = _Config.DataBlock,
                            StartByteAdr = Convert.ToInt32(bitAddress[0]),
                            VarType = VarType.Bit,
                            BitAdr = 0,
                        });
                    }
                    else
                    {
                        dataItems.Add(new DataItem()
                        {
                            Count = 1,
                            DataType = DataType.DataBlock,
                            DB = _Config.DataBlock,
                            StartByteAdr = Convert.ToInt32(bitAddress[0]),
                            VarType = VarType.Bit,
                            BitAdr = Convert.ToByte(bitAddress[1]),
                        });
                    }
                }

                plc.ReadMultipleVars(dataItems);

                foreach (var item in dataItems)
                {
                    item.StartByteAdr = Convert.ToInt32(string.Format("{0}{1}", item.StartByteAdr, item.BitAdr));
                }

                var result = list.Join(dataItems, x => x.Address, y => y.StartByteAdr, (x, y) => new { Bit = x, Single = y.Value }).ToDictionary(t => t.Bit, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                Dictionary<PlcBit, PlcBit.Signal> plcBitSignal = new Dictionary<PlcBit, PlcBit.Signal>();

                foreach (var item in result)
                {
                    if (item.Value == null)
                    {
                        plcBitSignal.Add(item.Key, item.Value == null ? PlcBit.Signal.OFF : PlcBit.Signal.ON);
                    }
                    else
                    {
                        plcBitSignal.Add(item.Key, (bool)item.Value == false ? PlcBit.Signal.OFF : PlcBit.Signal.ON);
                    }
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(plcBitSignal);
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

                var dataItems = new List<DataItem>();

                foreach (var item in list)
                {
                    string[] bitAddress = (item.Address * 0.1).ToString().Split('.');

                    if (bitAddress.Count() != 2)
                    {
                        dataItems.Add(new DataItem()
                        {
                            Count = 1,
                            DataType = DataType.DataBlock,
                            DB = _Config.DataBlock,
                            StartByteAdr = Convert.ToInt32(bitAddress[0]),
                            VarType = VarType.Bit,
                            BitAdr = 0,
                        });
                    }
                    else
                    {
                        dataItems.Add(new DataItem()
                        {
                            Count = 1,
                            DataType = DataType.DataBlock,
                            DB = _Config.DataBlock,
                            StartByteAdr = Convert.ToInt32(bitAddress[0]),
                            VarType = VarType.Bit,
                            BitAdr = Convert.ToByte(bitAddress[1]),
                        });
                    }
                }

                plc.ReadMultipleVars(dataItems);

                foreach (var item in dataItems)
                {
                    item.StartByteAdr = Convert.ToInt32(string.Format("{0}{1}", item.StartByteAdr, item.BitAdr));
                }

                var result = list.Join(dataItems, x => x.Address, y => y.StartByteAdr, (x, y) => new { Bit = x, Single = y.Value }).ToDictionary(t => t.Bit, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                Dictionary<PlcBit, PlcBit.Signal> plcBitSignal = new Dictionary<PlcBit, PlcBit.Signal>();

                foreach (var item in result)
                {
                    if (item.Value == null)
                    {
                        plcBitSignal.Add(item.Key, item.Value == null ? PlcBit.Signal.OFF : PlcBit.Signal.ON);
                    }
                    else
                    {
                        plcBitSignal.Add(item.Key, (bool)item.Value == false ? PlcBit.Signal.OFF : PlcBit.Signal.ON);
                    }
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(plcBitSignal);
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

                var dataItems = new List<DataItem>();

                foreach (var item in list)
                {
                    dataItems.Add(new DataItem()
                    {
                        Count = 1,
                        DataType = DataType.DataBlock,
                        DB = _Config.DataBlock,
                        StartByteAdr = item.Address,
                        VarType = VarType.Word
                    });
                }

                plc.ReadMultipleVars(dataItems);

                var result = list.Join(dataItems, x => x.Address, y => y.StartByteAdr, (x, y) => new { Word = x, Single = y.Value }).ToDictionary(t => t.Word, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
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

                ushort result = (ushort)plc.Read(string.Format("DB{0}.DBW{1}", _Config.DataBlock, word.Address));

                return new Response<object>(result);
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

                var dataItems = new List<DataItem>();

                foreach (var item in list)
                {
                    dataItems.Add(new DataItem()
                    {
                        Count = 1,
                        DataType = DataType.DataBlock,
                        DB = _Config.DataBlock,
                        StartByteAdr = item.Address,
                        VarType = VarType.Word
                    });
                }

                plc.ReadMultipleVars(dataItems);

                var result = list.Join(dataItems, x => x.Address, y => y.StartByteAdr, (x, y) => new { Word = x, Single = y.Value }).ToDictionary(t => t.Word, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

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

                string[] bitAddress = (bit.Address * 0.1).ToString().Split('.');

                bool bitSignal = signal == PlcBit.Signal.OFF ? false : true;

                string dataBlock;

                if (bitAddress.Count() != 2)
                {
                    dataBlock = string.Format("DB{0}.DBX{1}.0", _Config.DataBlock, bitAddress[0]);
                    plc.Write(dataBlock, bitSignal);
                }
                else
                {
                    dataBlock = string.Format("DB{0}.DBX{1}.{2}", _Config.DataBlock, bitAddress[0], bitAddress[1]);
                    plc.Write(dataBlock, bitSignal);
                }

                return new Response();
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

                foreach (var item in list)
                {
                    string[] bitAddress = (item.Key.Address * 0.1).ToString().Split('.');

                    bool bitSignal = item.Value == PlcBit.Signal.OFF ? false : true;

                    string dataBlock;

                    if (bitAddress.Count() != 2)
                    {
                        dataBlock = string.Format("DB{0}.DBX{1}.0", _Config.DataBlock, bitAddress[0]);
                        plc.Write(dataBlock, bitSignal);
                    }
                    else
                    {
                        dataBlock = string.Format("DB{0}.DBX{1}.{2}", _Config.DataBlock, bitAddress[0], bitAddress[1]);
                        plc.Write(dataBlock, bitSignal);
                    }
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

                foreach (var item in list)
                {
                    string[] bitAddress = (item.Key.Address * 0.1).ToString().Split('.');

                    bool bitSignal = item.Value == PlcBit.Signal.OFF ? false : true;

                    string dataBlock;

                    if (bitAddress.Count() != 2)
                    {
                        dataBlock = string.Format("DB{0}.DBX{1}.0", _Config.DataBlock, bitAddress[0]);
                        plc.Write(dataBlock, bitSignal);
                    }
                    else
                    {
                        dataBlock = string.Format("DB{0}.DBX{1}.{2}", _Config.DataBlock, bitAddress[0], bitAddress[1]);
                        plc.Write(dataBlock, bitSignal);
                    }
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

                foreach (var item in list)
                {
                    string[] bitAddress = (item.Key.Address * 0.1).ToString().Split('.');

                    bool bitSignal = item.Value == PlcBit.Signal.OFF ? false : true;

                    string dataBlock;

                    if (bitAddress.Count() != 2)
                    {
                        dataBlock = string.Format("DB{0}.DBX{1}.0", _Config.DataBlock, bitAddress[0]);
                        plc.Write(dataBlock, bitSignal);
                    }
                    else
                    {
                        dataBlock = string.Format("DB{0}.DBX{1}.{2}", _Config.DataBlock, bitAddress[0], bitAddress[1]);
                        plc.Write(dataBlock, bitSignal);
                    }
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

                foreach (var item in list)
                {
                    plc.Write(string.Format("DB{0}.DBW{1}", _Config.DataBlock, item.Key.Address), item.Value);
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
                if (word == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                plc.Write(string.Format("DB{0}.DBW{1}", _Config.DataBlock, word.Address), value);

                return new Response();
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

                foreach (var item in list)
                {
                    plc.Write(string.Format("DB{0}.DBW{1}", _Config.DataBlock, item.Key.Address), item.Value);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        private void SetConfig(IPlcConfigBase config)
        {
            if (config == null) throw new NullReferenceException("PLC 이더넷 통신 설정 정보가 NULL 입니다.");
            if (config is PlcSiemensConfig == false) throw new TypeAccessException(string.Format("PLC 이더넷 통신 설정 정보가 잘못되었습니다. {0}", config));

            this._Config = config as PlcSiemensConfig;
        }

        private void IsInitializeCheck()
        {
            if (this._Config == null) throw new Exception("PLC 통신 환경설정 정보가 존재하지 않습니다.");
            if (this.plc == null) throw new Exception(string.Format("PLC 통신 컨트롤이 초기화 되지 않았습니다. {0}", this._Config));
            if (this.plc.IsConnected == false) throw new Exception(string.Format("PLC 통신이 연결 되어 있지 않습니다. {0}", this._Config));
        }

        //private PlcRequest CreatePlcRequest()
        //{
        //    return new PlcRequest(this._Config.NetworkNo, (byte)this._Config.PlcNo, this._Config.StationNo);
        //}

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
