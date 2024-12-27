using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Core;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;

namespace TS.FW.Plc.Communication
{
    public class PlcEthernetControl : IPlcControl
    {
        private const int TIMEOUT_SEC = 10;

        private PlcEthernetConfig _Config = null;
        private PlcTcpClient _Client = null;
        private BackgroundTimer _trTick = null;

        public event EventHandler<bool> OnConnectedChangedEvent;

        public event EventHandler<IPlcConfigBase> OnTickEvent;

        public bool Connected
        {
            get
            {
                if (this._Client == null) return false;

                return this._Client.Connected;
            }
        }

        public void ChangeConfig(IPlcConfigBase config)
        {
            Logger.Write(this);

            if (this._Config == null) return;

            this.SetConfig(config);
            this._Client.ConfigChange(this._Config.IpAddress, this._Config.Port);
        }

        public void Connect(IPlcConfigBase config)
        {
            if (this._Client != null) return;

            this.SetConfig(config);

            this._Client = new PlcTcpClient(this._Config.IpAddress, this._Config.Port);
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

        public ResponseList<int, PlcBit.Signal> ReadBitList(int address, int count)
        {
            try
            {
                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new PlcReadBitCommand(address, count);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                var process = res.ReadBitCommandProcess(req.Command as PlcReadBitCommand);
                var result = process.ToDictionary(t => t.Key, t => t.Value);

                return new ResponseList<int, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
                    Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcWord, object> ReadWordList(int address, int count, PlcDeviceCode code, List<PlcWord> list)
        {
            try
            {
                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new PlcReadWordCommand(address, count, code);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                var process = res.ReadWordCommandProcess(req.Command as PlcReadWordCommand, list);
                var result = process.ToDictionary(t => t.Key, t => t.Value);

                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
                    Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteWord(int address, byte[] data)
        {
            try
            {
                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new PlcWriteWordCommand(address, data, PlcDeviceCode.W_LINK_REGISTER);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
                    Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<PlcBit.Signal> ReadBit(PlcBit bit)
        {
            try
            {
                if (bit == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new PlcReadBitCommand(bit);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                foreach (var item in res.ReadBitCommandProcess(req.Command as PlcReadBitCommand))
                {
                    if (bit.Address == item.Key) return new Response<PlcBit.Signal>(item.Value);
                }

                throw new Exception(string.Format("PLC BIt 데이터 읽는데 실패하였습니다. BIT : {0}", bit));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcReadBitCommand(list);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                var process = res.ReadBitCommandProcess(req.Command as PlcReadBitCommand);
                var result = list.Join(process, x => x.Address, y => y.Key, (x, y) => new { Bit = x, Single = y.Value }).ToDictionary(t => t.Bit, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcReadWordCommand(list);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                var process = res.ReadBitFromWordCommandProcess(req.Command as PlcReadWordCommand);
                var result = list.Join(process, x => x.Address, y => y.Key, (x, y) => new { Bit = x, Single = y.Value }).ToDictionary(t => t.Bit, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcRandomReadCommand(list);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                var process = res.ReadRandomCommandProcess(req.Command as PlcRandomReadCommand, list);
                var result = process.ToDictionary(t => t.Key, t => t.Value);

                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcReadWordCommand(word, word.Code);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                foreach (var item in res.ReadWordCommandProcess(req.Command as PlcReadWordCommand, new PlcWord[] { word }))
                {
                    if (word.Address == item.Key.Address) return new Response<object>(item.Value);
                }

                throw new Exception(string.Format("PLC BIt 데이터 읽는데 실패하였습니다. WORD : {0}", word));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcReadWordCommand(list, list.First().Code);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                var process = res.ReadWordCommandProcess(req.Command as PlcReadWordCommand, list);
                var result = process.ToDictionary(t => t.Key, t => t.Value);

                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcWriteBitCommand(bit, signal);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcWriteBitCommand(list);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcWriteWordCommand(list);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcRandomWriteBitCommand(list);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcRandomWriteWordCommand(list);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcWriteWordCommand(word, value, word.Code);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
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

                var req = this.CreatePlcRequest();
                req.Command = new PlcWriteWordCommand(list, list.First().Key.Code);

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (PlcResponse)buffer.Result;
                res.CheckPacket();

                return new Response();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PLC 통신이 연결 되어 있지 않습니다") == false)
                    Logger.Write(this, ex);
                return ex;
            }
        }

        private void SetConfig(IPlcConfigBase config)
        {
            if (config == null) throw new NullReferenceException("PLC 이더넷 통신 설정 정보가 NULL 입니다.");
            if (config is PlcEthernetConfig == false) throw new TypeAccessException(string.Format("PLC 이더넷 통신 설정 정보가 잘못되었습니다. {0}", config));

            this._Config = config as PlcEthernetConfig;
        }

        private void IsInitializeCheck()
        {
            if (this._Config == null) throw new PlcStateCheckException("PLC 통신 환경설정 정보가 존재하지 않습니다.");
            if (this._Client == null) throw new PlcStateCheckException(string.Format("PLC 통신 컨트롤이 초기화 되지 않았습니다. {0}", this._Config));
            if (this._Client.Connected == false) throw new PlcStateCheckException(string.Format("PLC 통신이 연결 되어 있지 않습니다. {0}", this._Config));
        }

        private PlcRequest CreatePlcRequest()
        {
            return new PlcRequest(this._Config.NetworkNo, (byte)this._Config.PlcNo, this._Config.StationNo);
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

    public class PlcStateCheckException : Exception
    {
        public PlcStateCheckException(string msg) : base(msg) { }
    }
}
