using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public class PlcRandomWriteBitCommand : IPlcCommandBase
    {
        public byte Count { get; private set; }

        public List<RandomAddress> BitList { get; protected set; }

        public PlcRandomWriteBitCommand(Dictionary<PlcBit, PlcBit.Signal> list) : base(PlcCommandType.RandomWriteBit)
        {
            var orderByList = list.OrderBy(t => t.Key.Address);

            this.Count = (byte)orderByList.Count();
            this.BitList = new List<RandomAddress>(orderByList.Select(t => new RandomAddress(t.Key, t.Value)));
        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }

            yield return this.Count;

            if (this.BitList != null && this.BitList.Count > 0)
            {
                foreach (var item in this.BitList.SelectMany(t => t.ToByte()))
                {
                    yield return item;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine(string.Format("- Count[{0}] : {1}", this.Count.ToHex(), this.Count));

            sb.AppendLine();

            sb.AppendLine("BIT LIST :");
            sb.Append(this.ToString(this.BitList));

            return sb.ToString();
        }
    }
}
