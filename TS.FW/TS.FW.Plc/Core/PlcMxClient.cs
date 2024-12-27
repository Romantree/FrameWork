using ActSupportMsgLib;
using ActUtlTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Utils;

namespace TS.FW.Plc.Core
{
    public class PlcMxClient
    {
        private IActUtlType _Client = null;
        private readonly IActSupportMsg _Support = null;

        private System.Timers.Timer _trReconnect = null;
        private readonly object _lock = new object();

        /// <summary>
        /// IP 주소
        /// </summary>
        public string IpAddress { get; private set; }

        /// <summary>
        /// 스테이션 번호
        /// </summary>
        public int StationNo { get; private set; }

        /// <summary>
        /// 연결 알림 이벤트
        /// </summary>
        public event EventHandler OnConnectedEvent;

        /// <summary>
        /// 연결 해제 이벤트
        /// </summary>
        public event EventHandler OnDisconnectedEvent;

        /// <summary>
        /// 연결 여부
        /// </summary>
        public bool Connected
        {
            get
            {
                return this.IsPingTest(IPAddress.Parse(this.IpAddress));
            }
        }

        private PlcMxClient()
        {
            this._Support = new ActSupportMsgClass();
        }

        public PlcMxClient(string ipAddress, int stationNo) : this()
        {
            this.IpAddress = ipAddress;
            this.StationNo = stationNo;
        }

        #region 함수

        /// <summary>
        /// 통신 연결 정보 수정
        ///  - 기존에 연결된 통신은 중지
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void ConfigChange(string ipAddress, int stationNo)
        {
            if (this.IpAddress == ipAddress) return;

            this.InitializeComponent();

            this.IpAddress = ipAddress;
            this.StationNo = stationNo;
        }

        /// <summary>
        /// 통신 연결 시작
        /// </summary>
        public void Start()
        {
            if (this._Client != null) return;
            this.Connect(this.IpAddress, this.StationNo);
        }

        /// <summary>
        /// 통신 연결 중지
        /// </summary>
        public void Stop()
        {
            this._trReconnect.Stop();
            this.Close();
        }

        #region 명령

        public Response<PlcBit.Signal> ReadBit(string address)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.GetDevice2(string.Format("B{0}", address), out short buffer);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response<PlcBit.Signal>(buffer == 0 ? PlcBit.Signal.OFF : PlcBit.Signal.ON);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<PlcBit.Signal> ReadBitList(string address, int length)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                var buffer = new short[length];

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.ReadDeviceBlock2(string.Format("B{0}", address), length, out buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    var result = buffer.Select(t => Convert.ToString(t, 2).PadLeft(16, '0').Reverse()).SelectMany(t => t).Select(t => t == '0' ? PlcBit.Signal.OFF : PlcBit.Signal.ON);

                    return new ResponseList<PlcBit.Signal>(result);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<byte[]> ReadRandomWord(IEnumerable<string> addressList)
        {
            try
            {
                if (addressList == null || addressList.Count() <= 0) throw new Exception("읽을 Word 주소가 존재하지 않습니다.");
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var address = string.Join("\n", addressList);
                    var count = addressList.Count();
                    var buffer = new short[count];

                    var res = this._Client.ReadDeviceRandom2(address, count, out buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    var result = buffer.Select(t => t.ToByte());

                    return new ResponseList<byte[]>(result);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<byte[]> ReadRandomDWord(IEnumerable<string> addressList)
        {
            try
            {
                if (addressList == null || addressList.Count() <= 0) throw new Exception("읽을 Word 주소가 존재하지 않습니다.");
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var address = string.Join("\n", addressList);
                    var count = addressList.Count();
                    var buffer = new int[count];

                    var res = this._Client.ReadDeviceRandom(address, count, out buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    var result = buffer.Select(t => t.ToByte());

                    return new ResponseList<byte[]>(result);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<byte[]> ReadWord(string address, int length)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var buffer = new short[length];

                    var res = this._Client.ReadDeviceBlock2(address, buffer.Length, out buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    var result = buffer.Select(t => t.ToByte()).SelectMany(t => t).ToArray();

                    return new Response<byte[]>(result);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<byte> ReadWordList(string address, int length)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                var buffer = new short[length];

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.ReadDeviceBlock2(address, buffer.Length, out buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    var result = buffer.Select(t => t.ToByte()).SelectMany(t => t);

                    return new ResponseList<byte>(result);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteBit(string address, PlcBit.Signal signal)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.SetDevice2(address, (short)signal);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteBitList(string address, int length, params PlcBit.Signal[] values)
        {
            try
            {
                // TODO : 예제 코드가 존재하지 않음.... 테스트 확실히 해야됨

                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                var buffer = PlcBit.ToShortList(values).ToArray();

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.WriteDeviceBlock2(string.Format("B{0}", address), length, ref buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteRandomBit(IEnumerable<PlcWirteBitModel> list)
        {
            try
            {
                // TODO : 예제 코드가 존재하지 않음.... 테스트 확실히 해야됨

                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                var orderByList = list.OrderBy(t => t.Address).ToList();
                var length = orderByList.Count;

                var address = string.Join("\n", orderByList.Select(t => string.Format("B{0}", t.ToAddressMxComponent())));
                var buffer = orderByList.Select(t => (short)t.WirteSignal).ToArray();

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.WriteDeviceRandom2(address, length, ref buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteRandomWord(IEnumerable<PlcWirteWordModel> list)
        {
            try
            {
                // TODO : 예제 코드가 존재하지 않음.... 테스트 확실히 해야됨

                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var address = string.Join("\n", list.Select(t => string.Format("{0}{1}", t.Code.ToCodeString(), t.ToAddressMxComponent())));
                    var buffer = list.Select(t => Convert.ToInt16(t.WirteValue)).ToArray();

                    var res = this._Client.WriteDeviceRandom2(address, buffer.Length, ref buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteRandomDWord(IEnumerable<PlcWirteWordModel> list)
        {
            try
            {
                // TODO : 예제 코드가 존재하지 않음.... 테스트 확실히 해야됨

                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var address = string.Join("\n", list.Select(t => string.Format("{0}{1}", t.Code.ToCodeString(), t.ToAddressMxComponent())));
                    var buffer = list.Select(t => Convert.ToInt32(t.WirteValue)).ToArray();

                    var res = this._Client.WriteDeviceRandom(address, buffer.Length, ref buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteWord(string address, short[] buffer)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.WriteDeviceBlock2(address, buffer.Length, ref buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteWordList(IEnumerable<PlcWirteWordModel> list)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                var orderByList = list.OrderBy(t => t.Address).ToList();
                var first = orderByList.FirstOrDefault();

                var address = string.Format("{0}{1}", first.Code.ToCodeString(), first.ToAddressMxComponent());
                var buffer = orderByList.Select(t => t.ToByte(t.WirteValue).ToInt16List(0, t.Length)).SelectMany(t => t).ToArray();

                lock (this._lock)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var res = this._Client.ReadDeviceBlock2(address, buffer.Length, out buffer[0]);
                    if (res != 0) throw new Exception(this.GetErrorMessage(res));

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        #endregion

        #region private

        private void InitializeComponent()
        {
            this.IpAddress = string.Empty;

            this._trReconnect = this._trReconnect ?? new System.Timers.Timer();
            this._trReconnect.AutoReset = false;

            // TODO : 잦은 연결시 PLC 쪽에서 막힘.
            this._trReconnect.Interval = 5 * 1000;
            this._trReconnect.Elapsed += Reconnect_DoWork;
            this._trReconnect.Stop();

            this.Stop();
        }

        private string GetErrorMessage(int code)
        {
            if (this._Support.GetErrorMessage(code, out string msg) != 0)
            {
                return string.Format("정의 되지 않은 에러 코드 입니다. {0}", code);
            }

            return msg;
        }

        private void PingTest(string ipAddress)
        {
            var list = this.PingTest(IPAddress.Parse(ipAddress)).ToList();
            var message = string.Join("\r\n", list);

            Logger.Write(this, string.Format("PING 테스트\r\n{0}", message));
        }

        private void Connect(string ipAddress, int stationNo)
        {
            this.PingTest(ipAddress);

            this._Client = this._Client ?? new ActUtlTypeClass();
            this._Client.ActLogicalStationNumber = stationNo;

            var res = this._Client.Open();
            if (res != 0) throw new Exception(string.Format("통신 연결도 중 에러가 발생하였습니다. '{0}'", this.GetErrorMessage(res)));

            // 연결 이벤트 푸시
            this.OnConnectedEvent?.Invoke(this, new EventArgs());
        }

        private void Close()
        {
            if (this._Client == null) return;

            var res = this._Client.Close();
            if (res != 0) throw new Exception(string.Format("통신 연결 해제도 중 에러가 발생하였습니다. '{0}'", this.GetErrorMessage(res)));

            this._Client = null;

            this.OnDisconnectedEvent?.Invoke(this, new EventArgs());
        }

        private void Reconnect_DoWork(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (this.Connected) return;

                this.Connect(this.IpAddress, this.StationNo);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this._trReconnect.Start();
            }
        }

        public bool IsPingTest(IPAddress ipAddress, int byteLength = 32)
        {
            var ping = new Ping();
            var buffer = new byte[byteLength];

            for (int i = 0; i < 5; i++)
            {
                var reply = ping.Send(ipAddress, 100, buffer);
                if (reply.Status == IPStatus.Success) return true;
            }

            return false;
        }

        public IEnumerable<string> PingTest(IPAddress ipAddress, int count = 5, int byteLength = 32)
        {
            var ping = new Ping();

            for (int i = 0; i < count; i++)
            {
                var buffer = new byte[byteLength];
                var reply = ping.Send(ipAddress, 100, buffer);

                if (reply.Status != IPStatus.Success) throw new Exception(string.Format("Ping 테스트에 실패하였습니다. IP : {0}", ipAddress));

                var loss = ((double)(buffer.Length - reply.Buffer.Length) / buffer.Length) * 100;

                yield return string.Format("{0}의 응답: 바이트={1} 시간<{2}ms 손실 = {3}%", ipAddress, byteLength, reply.RoundtripTime, loss);
            }
        }

        #endregion

        #endregion
    }
}
