using System;
using System.Collections.Generic;
using System.Linq;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public class PlcReadWordCommand : IPlcReadWriteCommandBase
    {
        public PlcReadWordCommand(int address, int count, PlcDeviceCode code) : base(PlcCommandType.ReadWord)
        {
            this.StartAddress = address;
            this.Code = code;
            this.Count = (ushort)count;
        }

        public PlcReadWordCommand(PlcWord word, PlcDeviceCode code = PlcDeviceCode.W_LINK_REGISTER) : base(PlcCommandType.ReadWord)
        {
            this.StartAddress = word.Address;
            this.Code = code;
            this.Count = (ushort)word.Length;
        }

        public PlcReadWordCommand(IEnumerable<PlcWord> list, PlcDeviceCode code = PlcDeviceCode.W_LINK_REGISTER) : base(PlcCommandType.ReadWord)
        {
            this.StartAddress = list.OrderBy(t => t.Address).First().Address;
            this.Code = code;
            this.Count = (ushort)list.Sum(t => t.Length);
        }

        public PlcReadWordCommand(IEnumerable<PlcBit> list) : base(PlcCommandType.ReadWord)
        {
            var orderByList = list.OrderBy(t => t.Address);
            var count = list.Max(t => t.Address) - list.Min(t => t.Address);

            // TODO : 단일 Bit 읽기 실행시 count가 0이 됨
            if (count <= 0) count++;

            this.StartAddress = orderByList.FirstOrDefault().Address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Count = (ushort)Math.Ceiling(count / 16.0D);
        }
    }
}
