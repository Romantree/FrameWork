using System;
using System.Collections.Generic;
using System.Linq;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto
{
    public class PlcBit : IPlcDataBase
    {
        public enum Signal
        {
            OFF,
            ON,
            ERR = 0xFF,
        }

        public Signal OnOff { get; set; }

        public Signal? WriteOnOff { get; set; } = null;
        
        public PlcBit(int address, string name) : base(address, name)
        {
            this.OnOff = Signal.OFF;
        }

        public PlcBit(int address, string name, Signal onOff) : this(address, name)
        {
            OnOff = onOff;
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", base.ToString(), this.OnOff);
        }

        public override string ToValueString()
        {
            return this.OnOff.ToString();
        }

        public string ToAddressMxComponent()
        {
            return this.Address.ToString("X");
        }

        public PlcBit ToPlcBitAddressOffset(int offset)
        {
            return new PlcBit(this.Address + offset, this.Name, this.OnOff);
        }

        public PlcWirteBitModel ToPlcWirteBitModel(PlcBit.Signal signal)
        {
            return new PlcWirteBitModel(this, signal);
        }

        public static IEnumerable<short> ToShortList(IEnumerable<PlcBit.Signal> list)
        {
            return list.Select(t => t == Signal.ON ? "1" : "0")
                .ToPageList(16) // 16개씩 짜르기
                .Select(t => string.Join("", t.Reverse()).PadLeft(16, '0')) // 16개가 아닌 경우 나머지 '0'으로 채우기
                .Select(t => Convert.ToInt16(t, 2)); //  Word 유형으로 변경
        }
    }

    public class PlcWirteBitModel :PlcBit
    {
        public Signal WirteSignal { get; set; }

        public PlcWirteBitModel(PlcBit bit, PlcBit.Signal signal) : base(bit.Address, bit.Name, bit.OnOff)
        {
            this.WirteSignal = signal;
        }
    }
}
