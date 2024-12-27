using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi
{
    public class RandomAddress
    {
        public int Address { get; set; }

        public PlcDeviceCode Code { get; set; }

        public byte[] Data { get; private set; }

        public RandomAddress(PlcWord word)
        {
            this.Address = word.Address;
            this.Code = word.Code;
        }

        public RandomAddress(PlcWord word, byte[] data) : this(word)
        {
            this.Data = data;
        }

        public RandomAddress(PlcBit bit, PlcBit.Signal signal)
        {
            this.Address = bit.Address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Data = new byte[] { (byte)(signal == PlcBit.Signal.ON ? 0x01 : 0x00) };
        }

        public IEnumerable<byte> ToByte()
        {
            foreach (var item in this.Address.ToByte().Take(3))
            {
                yield return item;
            }

            yield return (byte)this.Code;

            if (this.Data != null && this.Data.Length > 0)
            {
                foreach (var item in this.Data)
                {
                    yield return item;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format("[{0}]", ((int)this.Address).ToHex()));

            if (this.Data != null && this.Data.Length > 0)
            {
                sb.AppendLine(string.Format(" : {0}", this.Data.ToHex()));
            }

            return sb.ToString();
        }
    }
}
