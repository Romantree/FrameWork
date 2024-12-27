using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Core;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet.LS;
using TS.FW.Plc.Dto.Packet.LS.Command;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;
using TS.FW.Utils;

namespace TS.FW.Plc.Communication
{
    public class PlcLSControl : IPlcControl
    {
        private const int TIMEOUT_SEC = 10;

        private PlcLSConfig _Config = null;
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

        /// <summary>
        /// 비트 개별 읽기
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public Response<PlcBit.Signal> ReadBit(PlcBit bit)
        {
            try
            {
                if (bit == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new ILSReadEachCommand(bit);

                Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (LSResponse)buffer.Result;
                res.CheckPacket();

                Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                foreach (var item in res.LSReadBitCommandProcess(req.Command as ILSReadEachCommand))
                {
                    if (bit.Address == item.Key) return new Response<PlcBit.Signal>(item.Value);
                }

                throw new Exception(string.Format("PLC BIt 데이터 읽는데 실패하였습니다. BIT : {0}", bit));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 비트 연속 읽기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResponseList<PlcBit, PlcBit.Signal> ReadBitList(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new ILSReadContinueCommand(list);

                Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (LSResponse)buffer.Result;
                res.CheckPacket();

                Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                var process = res.LSReadBitListCommandProcess(req.Command as ILSReadContinueCommand);
                var result = list.Join(process, x => x.Address, y => y.Key, (x, y) => new { Bit = x, Single = y.Value }).ToDictionary(t => t.Bit, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcBit, PlcBit.Signal>(result);

            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 비트 연속 읽기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResponseList<PlcBit, PlcBit.Signal> ReadBitFromWordCommand(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new ILSReadContinueCommand(list);

                Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (LSResponse)buffer.Result;
                res.CheckPacket();

                Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                var process = res.LSReadBitListCommandProcess(req.Command as ILSReadContinueCommand);
                var result = list.Join(process, x => x.Address, y => y.Key, (x, y) => new { Bit = x, Single = y.Value }).ToDictionary(t => t.Bit, t => t.Single);

                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcBit, PlcBit.Signal>(result);

            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 워드 개별 연속 읽기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResponseList<PlcWord, object> ReadRandomWord(IEnumerable<PlcWord> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var result = new Dictionary<PlcWord, object>();

                foreach (var plcList in list.ToPageList(16))
                {
                    var req = this.CreatePlcRequest();
                    req.Command = new ILSReadEachCommand(plcList);

                    Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                    var buffer = this._Client.Request(req, TIMEOUT_SEC);
                    if (buffer == false) throw new Exception(buffer.Comment);

                    var res = (LSResponse)buffer.Result;
                    res.CheckPacket();

                    Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                    var process = res.LSReadWordCommandProcess(req.Command as ILSReadEachCommand, plcList);

                    foreach (var item in process)
                    {
                        result.Add(item.Key, item.Value);
                    }
                }

                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 워드 개별 읽기
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public Response<object> ReadWord(PlcWord word)
        {
            try
            {
                if (word == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new ILSReadEachCommand(word);

                Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (LSResponse)buffer.Result;
                res.CheckPacket();

                Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                foreach (var item in res.LSReadWordCommandProcess(req.Command as ILSReadEachCommand, new PlcWord[] { word }))
                {
                    if (word.Address == item.Key.Address) return new Response<object>(item.Value);
                }

                throw new Exception(string.Format("PLC BIt 데이터 읽는데 실패하였습니다. WORD : {0}", word));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 워드 연속 읽기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResponseList<PlcWord, object> ReadWordList(IEnumerable<PlcWord> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new ILSReadContinueCommand(list);

                Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (LSResponse)buffer.Result;
                res.CheckPacket();

                Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                var process = res.LSReadWordListCommandProcess(req.Command as ILSReadContinueCommand, list);
                var result = process.ToDictionary(t => t.Key, t => t.Value);

                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 비트 개별 쓰기
        /// </summary>
        /// <param name="bit"></param>
        /// <param name="signal"></param>
        /// <returns></returns>
        public Response WriteBit(PlcBit bit, PlcBit.Signal signal)
        {
            try
            {
                if (bit == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new ILSWriteEachCommandBase(bit, signal);

                Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (LSResponse)buffer.Result;
                res.CheckPacket();

                Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 비트 개별 연속 쓰기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Response WriteBitList(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var plcList in list.ToPageList(16))
                {
                    var req = this.CreatePlcRequest();
                    req.Command = new ILSWriteEachCommandBase(plcList.ToDictionary(t => t.Key, t => t.Value));

                    Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                    var buffer = this._Client.Request(req, TIMEOUT_SEC);
                    if (buffer == false) throw new Exception(buffer.Comment);

                    var res = (LSResponse)buffer.Result;
                    res.CheckPacket();

                    Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));
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
        /// 비트 개별 연속 쓰기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Response WriteBitFromWordCommand(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var plcList in list.ToPageList(16))
                {
                    var req = this.CreatePlcRequest();
                    req.Command = new ILSWriteEachCommandBase(plcList.ToDictionary(t => t.Key, t => t.Value));

                    Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                    var buffer = this._Client.Request(req, TIMEOUT_SEC);
                    if (buffer == false) throw new Exception(buffer.Comment);

                    var res = (LSResponse)buffer.Result;
                    res.CheckPacket();

                    Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));
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
        /// 비트 개별 연속 쓰기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Response WriteRandomBit(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var plcList in list.ToPageList(16))
                {
                    var req = this.CreatePlcRequest();
                    req.Command = new ILSWriteEachCommandBase(plcList.ToDictionary(t => t.Key, t => t.Value));

                    Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                    var buffer = this._Client.Request(req, TIMEOUT_SEC);
                    if (buffer == false) throw new Exception(buffer.Comment);

                    var res = (LSResponse)buffer.Result;
                    res.CheckPacket();

                    Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));
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
        /// 워드 개별 연속 쓰기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Response WriteRandomWord(Dictionary<PlcWord, object> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var plcList in list.ToPageList(16))
                {
                    var req = this.CreatePlcRequest();
                    req.Command = new ILSWriteEachCommandBase(plcList.ToDictionary(t => t.Key, t => t.Value));

                    Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                    var buffer = this._Client.Request(req, TIMEOUT_SEC);
                    if (buffer == false) throw new Exception(buffer.Comment);

                    var res = (LSResponse)buffer.Result;
                    res.CheckPacket();

                    Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));
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
        /// 워드 개별 쓰기
        /// </summary>
        /// <param name="word"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Response WriteWord(PlcWord word, object value)
        {
            try
            {
                if (word == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                var req = this.CreatePlcRequest();
                req.Command = new ILSWriteEachCommandBase(word, value);

                Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                var buffer = this._Client.Request(req, TIMEOUT_SEC);
                if (buffer == false) throw new Exception(buffer.Comment);

                var res = (LSResponse)buffer.Result;
                res.CheckPacket();

                Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 워드 개별 연속 쓰기
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Response WriteWordList(Dictionary<PlcWord, object> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var plcList in list.ToPageList(16))
                {
                    var req = this.CreatePlcRequest();
                    req.Command = new ILSWriteEachCommandBase(plcList.ToDictionary(t => t.Key, t => t.Value));

                    Logger.Write(this, string.Format("PLC 데이터 전송 : {0}", req));

                    var buffer = this._Client.Request(req, TIMEOUT_SEC);
                    if (buffer == false) throw new Exception(buffer.Comment);

                    var res = (LSResponse)buffer.Result;
                    res.CheckPacket();

                    Logger.Write(this, string.Format("PLC 데이터 수신 : {0}", res));
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
            if (config is PlcLSConfig == false) throw new TypeAccessException(string.Format("PLC 이더넷 통신 설정 정보가 잘못되었습니다. {0}", config));

            this._Config = config as PlcLSConfig;
        }

        private void IsInitializeCheck()
        {
            if (this._Config == null) throw new Exception("PLC 통신 환경설정 정보가 존재하지 않습니다.");
            if (this._Client == null) throw new Exception(string.Format("PLC 통신 컨트롤이 초기화 되지 않았습니다. {0}", this._Config));
            if (this._Client.Connected == false) throw new Exception(string.Format("PLC 통신이 연결 되어 있지 않습니다. {0}", this._Config));
        }

        private LSRequest CreatePlcRequest()
        {
            return new LSRequest();
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
