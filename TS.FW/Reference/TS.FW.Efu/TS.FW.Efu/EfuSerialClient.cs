using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Net.Serial;

namespace TS.FW.Efu
{
    public class EfuSerialClient : ISerialClient
    {
        public event EventHandler<string> OnDpsMessage;
        public override int DeleyTime => 300; //base.DeleyTime;
        private object _locker = new object();
        /// <summary>
        /// Callback 기준
        /// </summary>
        /// <param name="retval"></param>
        public EfuSerialClient(enReturnType retval = enReturnType.SendRecv) : base()
        {

        }

        public override bool Open(NetSerialData item, double readTimeout = 1, double sendTimeout = 1)
        {
            //this.DeleyTime = 100;
            //if (_trUpdate.IsBusy == false)
            //    this._trUpdate.Start();
            var ret = base.Open(item, readTimeout, sendTimeout);

            if (ret)
            {
                return true;
            }

            return false;
        }

        public override void Close()
        {
            try
            {
                base.Close();

                //this._trUpdate.Stop();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SendCommand(string cmd, bool isdsp = false)
        {
            try
            {
                lock (this._locker)
                {
                    var strcmd = this.ToCommand(cmd);

                    if (isdsp) this.DpsMessage(true, strcmd);

                    this.SendData(strcmd);

                    var recv = base.ReadData();
                    //if(base.CheckSum(recv))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private byte[] ToCommand(string cmd)
        {
            var str = string.Empty;
            byte[] bytes = new byte[9];

            switch (cmd)
            {
                case "Status":
                    bytes[1] = (byte)EFU_TYPE.REQ_MODE1; //MODE1
                    bytes[2] = (byte)EFU_TYPE.REQ_MODE2; //MODE2
                    bytes[3] = (byte)EFU_TYPE.LV32; //LV32
                    bytes[4] = (byte)EFU_TYPE.DPU; //DPU
                    bytes[5] = (byte)EFU_TYPE.MIN_LFU_COUNT; //START
                    bytes[6] = (byte)EFU_TYPE.MAX_LFU_COUNT; //EDN
                    bytes[7] = base.CheckSum(bytes).First();// base.CheckSum(bytes); // ETX

                    bytes[0] = (byte)EFU_TYPE.STX; // STX
                    bytes[8] = (byte)EFU_TYPE.ETX; // ETXbytes[1]
                    break;


                default:
                    return bytes;

            }
            return bytes;
        }

        private void DpsMessage(bool isSend, byte[] buffer)
        {
            try
            {
                var msg = string.Format("{0} >> {1}", isSend ? "TX" : "RX", string.Join(" ", buffer.Select(t => this.ToHex(t, 2))));

                this.OnDpsMessage?.Invoke(this, msg);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
