using ActSupportMsgLib;
using ActUtlTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Core;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Utils;

namespace TS.FW.Plc.Communication
{
    public class PlcMxControl : IPlcControl
    {
        private PlcMxConfig _Config = null;
        private PlcMxClient _Client = null;
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
            this._Client.ConfigChange(this._Config.IpAddress, this._Config.StationNo);
        }

        public void Connect(IPlcConfigBase config)
        {
            if (this._Client != null) return;

            this.SetConfig(config);

            this._Client = new PlcMxClient(this._Config.IpAddress, this._Config.StationNo);
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

                return this._Client.ReadBit(bit.ToAddressMxComponent());
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
                    var res = this._Client.ReadBit(bit.ToAddressMxComponent());
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

        // TODO : 사용되고 있지 않음 구조개선 필요.
        private ResponseList<PlcBit, PlcBit.Signal> _ReadBitList(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Address);
                var length = (int)Math.Ceiling(orderByList.Count() / 16.0D);
                var first = orderByList.First();

                var res = this._Client.ReadBitList(first.ToAddressMxComponent(), length);
                if (res == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, res.Comment);

                var result = new Dictionary<PlcBit, PlcBit.Signal>();

                foreach (var item in res.Result.Select((Data, Index) => new { Index, Data }))
                {
                    var bit = list.FirstOrDefault(t => t.Address == (first.Address + item.Index));
                    if (bit == null) continue;

                    result.Add(bit, item.Data);
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
                    var res = this._Client.ReadBit(bit.ToAddressMxComponent());
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

        // TODO : 사용되고 있지 않음 구조개선 필요.
        private ResponseList<PlcBit, PlcBit.Signal> _ReadBitFromWordCommand(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Address);
                var length = (int)Math.Ceiling(orderByList.Count() / 16.0D);
                var first = orderByList.First();

                var res = this._Client.ReadBitList(first.ToAddressMxComponent(), length);
                if (res == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, res.Comment);

                var result = new Dictionary<PlcBit, PlcBit.Signal>();

                foreach (var item in res.Result.Select((Data, Index) => new { Index, Data }))
                {
                    var bit = list.FirstOrDefault(t => t.Address == (first.Address + item.Index));
                    if (bit == null) continue;

                    result.Add(bit, item.Data);
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
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

                var wordList = list.Where(t => t.Length == 1).OrderBy(t => t.Address).ToList();
                var dWordList = list.Where(t => t.Length == 2).OrderBy(t => t.Address).ToList();

                var result = new Dictionary<PlcWord, object>();

                if (wordList.Count > 0)
                {
                    var address = wordList.Select(t => string.Format("{0}{1}", t.Code.ToCodeString(), t.ToAddressMxComponent()));

                    var res = this._Client.ReadRandomWord(address);
                    if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);

                    foreach (var item in res.Result.Select((Data, Index) => new { Index, Data }))
                    {
                        var word = wordList[item.Index];
                        var index = 0;

                        result.Add(word, word.ToDataParse(item.Data, ref index));
                    }
                }

                if (dWordList.Count > 0)
                {
                    var address = dWordList.Select(t => string.Format("{0}{1}", t.Code.ToCodeString(), t.ToAddressMxComponent()));

                    var res = this._Client.ReadRandomDWord(address);
                    if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);

                    foreach (var item in res.Result.Select((Data, Index) => new { Index, Data }))
                    {
                        var word = dWordList[item.Index];
                        var index = 0;

                        result.Add(word, word.ToDataParse(item.Data, ref index));
                    }
                }

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

                var address = string.Format("{0}{1}", word.Code.ToCodeString(), word.ToAddressMxComponent());

                var res = this._Client.ReadWord(address, word.Length);
                if (res == false) return new Response<object>(false, res.Comment);

                var index = 0;
                var result = word.ToDataParse(res.Result, ref index);

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

                var orderByList = list.OrderBy(t => t.Address).ToList();
                var result = new Dictionary<PlcWord, object>();

                foreach (var word in list)
                {
                    var address = string.Format("{0}{1}", word.Code.ToCodeString(), word.ToAddressMxComponent());

                    var res = this._Client.ReadWord(address, word.Length);
                    if (res == false) continue;

                    var index = 0;
                    result.Add(word, word.ToDataParse(res.Result, ref index));
                }

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        // TODO : 사용되고 있지 않음 구조개선 필요.
        private ResponseList<PlcWord, object> _ReadWordList(IEnumerable<PlcWord> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Address).ToList();
                var first = orderByList.FirstOrDefault();

                var address = string.Format("{0}{1}", first.Code.ToCodeString(), first.ToAddressMxComponent());
                var length = orderByList.Sum(t => t.Length);

                var res = this._Client.ReadWordList(address, length);
                if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);

                var result = new Dictionary<PlcWord, object>();
                var buffer = res.Result.ToArray();
                var index = 0;

                foreach (var word in orderByList)
                {
                    result.Add(word, word.ToDataParse(buffer, ref index));
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

                var address = string.Format("B{0}", bit.ToAddressMxComponent());

                return this._Client.WriteBit(address, signal);
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
                    var address = string.Format("B{0}", bit.Key.ToAddressMxComponent());

                    var res = this._Client.WriteBit(address, bit.Value);
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

        // TODO : 사용되고 있지 않음 구조개선 필요.
        private Response _WriteBitList(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Key.Address).ToList();
                var length = orderByList.Count();
                var first = orderByList.First();

                return this._Client.WriteBitList(first.Key.ToAddressMxComponent(), length, orderByList.Select(t => t.Value).ToArray());
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
                    var address = string.Format("B{0}", bit.Key.ToAddressMxComponent());

                    var res = this._Client.WriteBit(address, bit.Value);
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

        // TODO : 사용되고 있지 않음 구조개선 필요.
        private Response _WriteBitFromWordCommand(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var orderByList = list.OrderBy(t => t.Key.Address).ToList();
                var length = orderByList.Count();
                var first = orderByList.First();

                return this._Client.WriteBitList(first.Key.ToAddressMxComponent(), length, orderByList.Select(t => t.Value).ToArray());
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
                    var address = string.Format("B{0}", bit.Key.ToAddressMxComponent());

                    var res = this._Client.WriteBit(address, bit.Value);
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

        // TODO : 사용되고 있지 않음 구조개선 필요.
        private Response _WriteRandomBit(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var req = list.Select(t => t.Key.ToPlcWirteBitModel(t.Value));

                return this._Client.WriteRandomBit(req);
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

                var temp = list.Select(t => t.Key.ToPlcWirteWordModel(t.Value)).OrderBy(t => t.Address).ToList();

                var wordList = temp.Where(t => t.Length == 1).ToList();
                var dWordList = temp.Where(t => t.Length == 2).ToList();

                if (wordList.Count > 0)
                {
                    var res = this._Client.WriteRandomWord(wordList);
                    if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);
                }

                if (dWordList.Count > 0)
                {
                    var res = this._Client.WriteRandomDWord(dWordList);
                    if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);
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

                var address = string.Format("{0}{1}", word.Code.ToCodeString(), word.ToAddressMxComponent());
                var buffer = word.ToByte(value).ToInt16List(0, word.Length).ToArray();

                return this._Client.WriteWord(address, buffer);
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
                    var address = string.Format("{0}{1}", word.Key.Code.ToCodeString(), word.Key.ToAddressMxComponent());
                    var buffer = word.Key.ToByte(word.Value).ToInt16List(0, word.Key.Length).ToArray();

                    var res = this._Client.WriteWord(address, buffer);
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

        // TODO : 사용되고 있지 않음 구조개선 필요.
        private Response _WriteWordList(Dictionary<PlcWord, object> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var req = list.Select(t => t.Key.ToPlcWirteWordModel(t.Value));

                return this._Client.WriteWordList(req);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        private void SetConfig(IPlcConfigBase config)
        {
            if (config == null) throw new NullReferenceException("PLC MX 통신 설정 정보가 NULL 입니다.");
            if (config is PlcMxConfig == false) throw new TypeAccessException(string.Format("PLC MX 통신 설정 정보가 잘못되었습니다. {0}", config));

            this._Config = config as PlcMxConfig;
        }

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
