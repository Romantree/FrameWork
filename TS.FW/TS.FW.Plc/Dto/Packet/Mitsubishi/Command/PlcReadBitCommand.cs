using System.Collections.Generic;
using System.Linq;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public class PlcReadBitCommand : IPlcReadWriteCommandBase
    {
        public PlcReadBitCommand(PlcBit bit) : base(PlcCommandType.ReadBit)
        {
            this.StartAddress = bit.Address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Count = 1;
        }

        public PlcReadBitCommand(IEnumerable<PlcBit> list) : base(PlcCommandType.ReadBit)
        {
            this.StartAddress = list.OrderBy(t => t.Address).First().Address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Count = (ushort)list.Count();
        }

        public PlcReadBitCommand(int address, int count) : base(PlcCommandType.ReadBit)
        {
            this.StartAddress = address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Count = (ushort)count;
        }
    }
}
