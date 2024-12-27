using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public class PlcWriteBitCommand : IPlcReadWriteCommandBase
    {
        public byte[] Data { get; private set; }

        public PlcWriteBitCommand(PlcBit bit, PlcBit.Signal signal) : base(PlcCommandType.WriteBit)
        {
            this.StartAddress = bit.Address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Count = 1;
            this.Data = new byte[] { this.ToByte(signal) };
        }

        public PlcWriteBitCommand(Dictionary<PlcBit, PlcBit.Signal> list) : base(PlcCommandType.WriteBit)
        {
            var orderByList = list.OrderBy(t => t.Key.Address);

            this.StartAddress = orderByList.First().Key.Address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Count = (ushort)orderByList.Count();
            this.Data = this.ToByte(list.Values.ToList()).ToArray();
        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.Data)
            {
                yield return item;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("DATA : ");

            var list = new List<string>();

            for (int i = 0; i < this.Count; i += 2)
            {
                list.Add(string.Format("[{0}]({1})", ((int)(this.StartAddress + i)).ToHex(), (PlcBit.Signal)((this.Data[i / 2] & 0x10) >> 4)).PadRight(17, ' '));
                if ((i + 1) < this.Count)
                {
                    list.Add(string.Format("[{0}]({1})", ((int)(this.StartAddress + (i + 1))).ToHex(), (PlcBit.Signal)((this.Data[i / 2] & 0x01))).PadRight(17, ' '));
                }
            }

            var count = list.Count / 10;

            for (int i = 0; i <= count; i++)
            {
                sb.AppendLine(string.Join(" ", list.Skip(i * 10).Take(10)));
            }

            return sb.ToString();
        }

        private byte ToByte(PlcBit.Signal signal)
        {
            return (byte)(signal == PlcBit.Signal.ON ? 0x10 : 0x00);
        }

        private IEnumerable<byte> ToByte(List<PlcBit.Signal> list)
        {
            for (int i = 0; i < list.Count; i += 2)
            {
                yield return (byte)(this.ToByte(list[i])
                    | (((i + 1) < list.Count) ? (byte)list[i + 1] : 0x00));
            }
        }
    }
}
