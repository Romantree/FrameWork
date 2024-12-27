using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Helper;
using TS.FW.Net.Serial;

namespace TS.FW.Net.Inverter
{
    /// <summary>
    /// 미쯔비시 제품 모델 (Home 기능 적용) 
    /// FR_A700 모델과 동일함
    /// </summary>
    public class FR_A800_Client : ISerialClient
    {
        private const int RESET = 0x9966;
        private const int ALARM_CLEAR = 0x9696;

        private readonly object _locker = new object();
        private readonly BackgroundTimer _trUpdate = new BackgroundTimer();

        public event EventHandler<string> OnDpsMessage;

        public override int DeleyTime => 100;

        public InverterState State { get; private set; } = new InverterState();

        public FR_A800_Client()
        {
            this._trUpdate.SleepTimeMsc = 100;
            this._trUpdate.DoWork += _trUpdate_DoWork;
        }

        public override bool Open(SerialSetting item, double readTimeout = 1, double sendTimeout = 1, bool issim = false)
        {
            try
            {
                if (base.Open(item, readTimeout, sendTimeout) == false) return false;

                this.SetExtendMode();

                this._trUpdate.Start();

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public override void Close()
        {
            try
            {
                base.Close();

                if (this._trUpdate.IsBusy)
                    this._trUpdate.Stop();
            }
            catch (Exception)
            {
            }
        }

        public string ToSendMessage(enDpsInverterCmd cmd)
        {
            var buffer = this.ToCommand(cmd);
            if (buffer == null || buffer.Count() <= 0) return string.Empty;

            var temp = this.CheckSum(buffer.ToArray());
            return string.Join(" ", temp.Select(t => this.ToHex(t, 2)));
        }

        public bool Reset(int no = 1, bool isDpsMsg = false)
        {
            try
            {
                try
                {
                    if (this.IsSimulation) return true;

                    lock (this._locker)
                    {
                        var cmd = this.ToCommand(enInverterCmd.Reset, no, RESET, 4);

                        return this.SetCommand(cmd, isDpsMsg);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(this, ex);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool AlarmClear(int no = 1, bool isDpsMsg = false)
        {
            try
            {
                try
                {
                    if (this.IsSimulation) return true;

                    lock (this._locker)
                    {
                        var cmd = this.ToCommand(enInverterCmd.AlarmClear, no, ALARM_CLEAR, 4);

                        return this.SetCommand(cmd, isDpsMsg);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(this, ex);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool Home(int no = 1, bool isDpsMsg = false)
        {
            try
            {
                return this.SetCommand(enInverterAction.HOME, no, isDpsMsg);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool Stop(int no = 1, bool isDpsMsg = false)
        {
            try
            {
                return this.SetCommand(enInverterAction.STOP, no, isDpsMsg);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool MovwCW(int no = 1, bool isDpsMsg = false)
        {
            try
            {
                return this.SetCommand(enInverterAction.FWD, no, isDpsMsg);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool MovwCCW(int no = 1, bool isDpsMsg = false)
        {
            try
            {
                return this.SetCommand(enInverterAction.BWD, no, isDpsMsg);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool SetSpeed(int rpm, int no = 1, bool isDpsMsg = false)
        {
            try
            {
                if (this.IsSimulation)
                {
                    this.State.ReadRPM = rpm;
                    return true;
                }

                lock (this._locker)
                {
                    var cmd = this.ToCommand(enInverterCmd.SetSpeed, no, rpm, 4);

                    return this.SetCommand(cmd, isDpsMsg);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public bool PositionShift(int angle, int no = 1, bool isDpsMsg = false)
        {
            try
            {
                if (this.IsSimulation) return true;

                lock (this._locker)
                {
                    var value = Convert.ToInt32(DataHelper.ToDigit(angle, 0, 360, 4096));

                    var cmd = this.ToCommand(enInverterCmd.PositionShift, no, value, 4);

                    var ack = this.SetCommand(cmd, isDpsMsg);

                    //if (ack == true) AP.DB.System.CurrentWheelPositionShift = angle;
                    //해당 데이터는 외부에서 처리 해야함. 21.12.06 by Jp 

                    return ack;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (this.IsSimulation == false && this.Connected == false) return;

                this.ReadStatus();

                if (this.State.RUN == false)
                {
                    this.State.ReadOutputA = 0;
                    this.State.ReadRPM = 0;
                    return;
                }

                this.ReadRevolution();
                this.ReadOutputA();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void SetExtendMode(int no = 1, int mode = 3)
        {
            var cmd = this.ToCommand(enInverterCmd.ExtendMode, no, mode, 2);

            this.SendData(cmd.ToArray());

            this.ReadData();
        }

        private bool SetCommand(enInverterAction action, int no, bool isDpsMsg)
        {
            try
            {
                if (this.IsSimulation) return true;

                lock (this._locker)
                {
                    var cmd = this.ToCommand(enInverterCmd.SetCommand, no, action);

                    return this.SetCommand(cmd, isDpsMsg);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        private bool SetCommand(IEnumerable<byte> cmd, bool isDpsMsg)
        {
            try
            {
                if (this.IsSimulation) return true;

                var msg = cmd.ToArray();
                this.SendData(cmd.ToArray(), isDpsMsg);

                var buffer = this.ReadData();
                if (buffer == null || buffer.Length < 1) return false;

                if (isDpsMsg == true) this.DpsMessage(false, buffer);

                return buffer[0] == ACK;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        private void ReadStatus(int no = 1)
        {
            try
            {
                if (this.IsSimulation) return;

                lock (this._locker)
                {
                    var cmd = this.ToCommand(enInverterCmd.ReadStatus, no);

                    this.SendData(cmd.ToArray());

                    var buffer = this.ReadData();
                    if (buffer == null || buffer.Length <= 7) return;

                    this.State.SetStatus(buffer);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void ReadRevolution(int no = 1)
        {
            try
            {
                if (this.IsSimulation) return;

                lock (this._locker)
                {
                    var cmd = this.ToCommand(enInverterCmd.ReadRevolution, no);

                    this.SendData(cmd.ToArray());

                    var buffer = this.ReadData();
                    if (buffer == null || buffer.Length <= 7) return;

                    this.State.SetRPM(buffer);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void ReadOutputA(int no = 1)
        {
            try
            {
                if (this.IsSimulation)                    
                {
                    this.State.ReadOutputA = this.State.ReadRPM != 0 ? 7.5 : 0;
                    return;
                }

                lock (this._locker)
                {
                    var cmd = this.ToCommand(enInverterCmd.ReadOutputA, no);

                    this.SendData(cmd.ToArray());

                    var buffer = this.ReadData();
                    if (buffer == null || buffer.Length <= 7) return;

                    this.State.SetOutputA(buffer);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void SendData(byte[] buffer, bool isDpsMsg)
        {
            try
            {
                var checkSum = this.CheckSum(buffer).ToArray();

                base.SendData(checkSum);

                if (isDpsMsg == false) return;

                this.DpsMessage(true, checkSum);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected override void SendData(byte[] buffer)
        {
            try
            {
                var checkSum = this.CheckSum(buffer).ToArray();

                base.SendData(checkSum);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private IEnumerable<byte> ToCommand(enDpsInverterCmd cmd)
        {
            switch (cmd)
            {
                case enDpsInverterCmd.RESET_01: return this.ToCommand(enInverterCmd.Reset, 1, RESET, 4);
                case enDpsInverterCmd.ALARM_CLEAR_01: return this.ToCommand(enInverterCmd.AlarmClear, 1, ALARM_CLEAR, 4);
                case enDpsInverterCmd.STOP_01: return this.ToCommand(enInverterCmd.SetCommand, 1, enInverterAction.STOP);
                case enDpsInverterCmd.HOME_01: return this.ToCommand(enInverterCmd.SetCommand, 1, enInverterAction.HOME);
                case enDpsInverterCmd.MOVE_FWD_01: return this.ToCommand(enInverterCmd.SetCommand, 1, enInverterAction.FWD);
                case enDpsInverterCmd.MOVE_BWD_01: return this.ToCommand(enInverterCmd.SetCommand, 1, enInverterAction.BWD);
                case enDpsInverterCmd.SET_SPEED_10_01: return this.ToCommand(enInverterCmd.SetSpeed, 1, 10, 4);
                case enDpsInverterCmd.SET_SPEED_100_01: return this.ToCommand(enInverterCmd.SetSpeed, 1, 100, 4);

                case enDpsInverterCmd.RESET_02: return this.ToCommand(enInverterCmd.Reset, 2, RESET, 4);
                case enDpsInverterCmd.ALARM_CLEAR_02: return this.ToCommand(enInverterCmd.AlarmClear, 2, ALARM_CLEAR, 4);
                case enDpsInverterCmd.STOP_02: return this.ToCommand(enInverterCmd.SetCommand, 2, enInverterAction.STOP);
                case enDpsInverterCmd.HOME_02: return this.ToCommand(enInverterCmd.SetCommand, 2, enInverterAction.HOME);
                case enDpsInverterCmd.MOVE_FWD_02: return this.ToCommand(enInverterCmd.SetCommand, 2, enInverterAction.FWD);
                case enDpsInverterCmd.MOVE_BWD_02: return this.ToCommand(enInverterCmd.SetCommand, 2, enInverterAction.BWD);
                case enDpsInverterCmd.SET_SPEED_10_02: return this.ToCommand(enInverterCmd.SetSpeed, 2, 10, 4);
                case enDpsInverterCmd.SET_SPEED_100_02: return this.ToCommand(enInverterCmd.SetSpeed, 2, 100, 4);
            }

            return new byte[0];
        }

        private IEnumerable<byte> ToCommand(enInverterCmd cmd, int no, enInverterAction action)
        {
            var data = (int)action;

            foreach (var item in this.ToCommand(cmd, no, data, 2))
            {
                yield return item;
            }
        }

        private IEnumerable<byte> ToCommand(enInverterCmd cmd, int no, int data, int len)
        {
            foreach (var item in this.ToCommand(cmd, no))
            {
                yield return item;
            }

            var value = this.ToHex(data, len);

            foreach (var item in this.Encoding.GetBytes(value))
            {
                yield return item;
            }
        }

        private IEnumerable<byte> ToCommand(enInverterCmd cmd, int no)
        {
            var number = this.ToHex(no, 2);

            foreach (var item in this.Encoding.GetBytes(number))
            {
                yield return item;
            }

            var command = this.ToHex((int)cmd, 2);

            foreach (var item in this.Encoding.GetBytes(command))
            {
                yield return item;
            }

            yield return DELEY;
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

    public class InverterState
    {
        public bool RUN { get; private set; }

        public bool CW { get; private set; }

        public bool CCW { get; private set; }

        public bool HOME { get; private set; }

        public bool SU { get; private set; }

        public bool OL { get; private set; }

        public bool IPF { get; private set; }

        public bool FU { get; private set; }

        public long ReadRPM { get; set; }

        public double ReadOutputA { get; set; }

        public void SetStatus(byte[] buffer)
        {
            try
            {
                var msg = Encoding.ASCII.GetString(buffer.Skip(3).Take(2).ToArray());
                var status = Convert.ToString(Convert.ToByte(msg, 16), 2).PadLeft(8, '0').Reverse().ToArray();

                this.RUN = status[0] == '1';
                this.CW = status[1] == '1';
                this.CCW = status[2] == '1';
                this.SU = status[3] == '1';
                this.OL = status[4] == '1';
                this.IPF = status[5] == '1';
                this.FU = status[6] == '1';
                this.HOME = status[7] == '1';
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetRPM(byte[] buffer)
        {
            try
            {
                var msg = Encoding.ASCII.GetString(buffer.Skip(3).Take(4).ToArray());
                this.ReadRPM = Convert.ToInt32(msg, 16);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetOutputA(byte[] buffer)
        {
            try
            {
                var msg = Encoding.ASCII.GetString(buffer.Skip(3).Take(4).ToArray());
                this.ReadOutputA = Convert.ToInt32(msg, 16) * 0.01D;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetSimulation(enInverterAction action)
        {
            switch (action)
            {
                case enInverterAction.STOP:
                    {
                        this.CCW = false;
                        this.CW = false;
                        this.RUN = false;
                        this.HOME = false;
                    }
                    break;
                case enInverterAction.FWD:
                    {
                        this.CCW = false;
                        this.CW = true;
                        this.RUN = true;
                        this.HOME = false;
                    }
                    break;
                case enInverterAction.BWD:
                    {
                        this.CCW = true;
                        this.CW = false;
                        this.RUN = true;
                        this.HOME = false;
                    }
                    break;
                case enInverterAction.HOME:
                    {
                        this.CCW = false;
                        this.CW = false;
                        this.RUN = false;
                        this.HOME = true;
                    }
                    break;
            }
        }
    }

    public enum enInverterAction
    {
        STOP = 0,
        FWD = 2,
        BWD = 4,
        HOME = 8,
    }

    public enum enInverterCmd
    {
        None = 0x00,

        Reset = 0xFD,
        AlarmClear = 0xF4,

        ExtendMode = 0xFF,

        PositionShift = 0xBD,

        ReadStatus = 0x7A,
        ReadRevolution = 0x6F,
        ReadOutputA = 0x70,

        SetSpeed = 0xED,
        SetCommand = 0xFA,

        //OperationMode = 0xFB,
        //SpeciaMonitor = 0xF3,
        //SetLoadFactor = 0x72,
    }

    public enum enDpsInverterCmd
    {
        RESET_01,
        ALARM_CLEAR_01,

        STOP_01,
        HOME_01,

        MOVE_FWD_01,
        MOVE_BWD_01,

        SET_SPEED_10_01,
        SET_SPEED_100_01,

        RESET_02,
        ALARM_CLEAR_02,

        STOP_02,
        HOME_02,

        MOVE_FWD_02,
        MOVE_BWD_02,

        SET_SPEED_10_02,
        SET_SPEED_100_02,
    }

}
