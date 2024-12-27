using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS.FW.Inverter.Mitsubishi
{
    public class FR_A800
    {
        private const int ACK = 0x06;
        private const int ENQ = 0x05;
        private const int CR = 0x0D;
        private const int LF = 0x0A;
        private const int DELEY = 0x31;
        private const int RESET = 0x9966;
        private const int ALARM_CLEAR = 0x9696;

        private readonly Encoding Encoding = Encoding.ASCII;

        #region Send

        public byte[] Reset(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.Reset, no, RESET, 4).ToArray();
        }

        public byte[] AlarmClear(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.AlarmClear, no, ALARM_CLEAR, 4).ToArray();
        }

        public byte[] Home(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.SetCommand, no, eFR_A800_Action.HOME).ToArray();
        }

        public byte[] Stop(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.SetCommand, no, eFR_A800_Action.STOP).ToArray();
        }

        public byte[] MoveCW(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.SetCommand, no, eFR_A800_Action.FWD).ToArray();
        }

        public byte[] MoveCCW(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.SetCommand, no, eFR_A800_Action.BWD).ToArray();
        }

        public byte[] SetSpeed(int no, int rpm)
        {
            return this.ToCommand(eFR_A800_Cmd.SetSpeed, no, rpm, 4).ToArray();
        }

        public byte[] PositionShift(int no, int angle)
        {
            var value = Convert.ToInt32(this.ToDigit(angle, 0, 360, 4096));

            return this.ToCommand(eFR_A800_Cmd.PositionShift, no, value, 4).ToArray();
        }

        public byte[] SetExtendMode(int no, int mode)
        {
            return this.ToCommand(eFR_A800_Cmd.ExtendMode, no, mode, 2).ToArray();
        }

        public byte[] ReadStatus(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.ReadStatus, no).ToArray();
        }

        public byte[] ReadRevolution(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.ReadRevolution, no).ToArray();
        }

        public byte[] ReadOutputA(int no)
        {
            return this.ToCommand(eFR_A800_Cmd.ReadOutputA, no).ToArray();
        }

        #endregion

        #region Recv

        public bool IsAck(byte[] buffer)
        {
            if (buffer == null || buffer.Length < 1) return false;

            return buffer[0] == ACK;
        }

        public FR_A800_Status ToStatus(byte[] buffer)
        {
            return new FR_A800_Status(buffer);
        }

        #endregion

        private IEnumerable<byte> ToCommand(eFR_A800_Cmd cmd, int no, eFR_A800_Action action)
        {
            var data = (int)action;

            foreach (var item in this.ToCommand(cmd, no, data, 2))
            {
                yield return item;
            }
        }

        private IEnumerable<byte> ToCommand(eFR_A800_Cmd cmd, int no, int data, int len)
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

        private IEnumerable<byte> ToCommand(eFR_A800_Cmd cmd, int no)
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

        private double ToDigit(double voltage, double minVoltage, double maxVoltage, double maxDigit = ushort.MaxValue)
        {
            return maxDigit * ((voltage - minVoltage) / (maxVoltage - minVoltage));
        }

        private string ToHex(int value, int len)
        {
            return Convert.ToString(value, 16).PadLeft(len, '0').ToUpper();
        }
    }

    public class FR_A800_Status
    {
        public bool RUN { get; private set; }

        public bool CW { get; private set; }

        public bool CCW { get; private set; }

        public bool HOME { get; private set; }

        public bool SU { get; private set; }

        public bool OL { get; private set; }

        public bool IPF { get; private set; }

        public bool FU { get; private set; }

        public FR_A800_Status() { }

        internal FR_A800_Status(byte[] buffer)
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
    }

    internal enum eFR_A800_Cmd
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

    internal enum eFR_A800_Action
    {
        STOP = 0,
        FWD = 2,
        BWD = 4,
        HOME = 8,
    }
}
