using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public class PlcWriteWordCommand : IPlcReadWriteCommandBase
    {
        public byte[] Data { get; private set; }

        public PlcWriteWordCommand(Dictionary<PlcBit, PlcBit.Signal> list) : base(PlcCommandType.WriteWord)
        {
            var orderByList = list.OrderBy(t => t.Key.Address);

            this.StartAddress = orderByList.First().Key.Address;
            this.Code = PlcDeviceCode.B_LINK_RELAY;
            this.Count = (ushort)Math.Ceiling(orderByList.Count() / 16.0D);
            this.Data = this.ToByte(orderByList.Select(t => t.Value), this.Count);
        }

        public PlcWriteWordCommand(PlcWord word, object value, PlcDeviceCode code = PlcDeviceCode.W_LINK_REGISTER) : base(PlcCommandType.WriteWord)
        {
            this.StartAddress = word.Address;
            this.Code = code;
            this.Count = (ushort)word.Length;
            this.Data = word.ToByte(value);
        }

        public PlcWriteWordCommand(Dictionary<PlcWord, object> list, PlcDeviceCode code = PlcDeviceCode.W_LINK_REGISTER) : base(PlcCommandType.WriteWord)
        {
            var orderByList = list.OrderBy(t => t.Key.Address);

            this.StartAddress = orderByList.First().Key.Address;
            this.Code = code;
            this.Count = (ushort)orderByList.Sum(t => t.Key.Length);
            this.Data = orderByList.Select(t => t.Key.ToByte(t.Value)).SelectMany(t => t).ToArray();
        }

        public PlcWriteWordCommand(int address, byte[] data, PlcDeviceCode code = PlcDeviceCode.W_LINK_REGISTER) : base(PlcCommandType.WriteWord)
        {
            this.StartAddress = address;
            this.Code = code;
            this.Count = 1;
            this.Data = data.ToArray();
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

            if (this.Code == PlcDeviceCode.M_INSIDE_RELAY || this.Code == PlcDeviceCode.B_LINK_RELAY)
            {
                sb.Append(this.ToPlcBitString(this.StartAddress, this.Data));
            }
            else
            {
                sb.Append(this.ToPlcWordString(this.StartAddress, this.Data, this.Count));
            }

            return sb.ToString();
        }

        private string ToPlcBitString(int startAddress, byte[] buffer)
        {
            var sb = new StringBuilder();
            var list = new List<string>();

            foreach (var item in buffer.Select((t, i) => new { Index = i, Value = t }))
            {
                var temp = Convert.ToString(item.Value, 2).PadLeft(8, '0').Reverse().Select((t, i) => string.Format("[{0}]({1})"
                     , ((int)(this.StartAddress + ((item.Index * 8) + i))).ToHex()
                     , (t == '1') ? PlcBit.Signal.ON : PlcBit.Signal.OFF).PadRight(17, ' '));

                list.AddRange(temp);
            }

            var count = list.Count / 10;

            for (int i = 0; i <= count; i++)
            {
                sb.AppendLine(string.Join(" ", list.Skip(i * 10).Take(10)));
            }

            return sb.ToString();
        }

        private string ToPlcWordString(int startAddress, byte[] buffer, int count)
        {
            var sb = new StringBuilder();
            var list = new List<string>();

            foreach (var item in buffer.ToInt16List(0, count).Select((t, i) => new { Index = i, Value = t }))
            {
                list.Add(string.Format("[{0}] : {1}"
                    , ((int)(this.StartAddress + item.Index)).ToHex()
                    , item.Value.ToHex()
                    ).PadRight(21, ' '));
            }

            var temp = list.Count / 5;

            for (int i = 0; i <= temp; i++)
            {
                sb.AppendLine(string.Join(" ", list.Skip(i * 5).Take(5)));
            }

            return sb.ToString();
        }

        private byte[] ToByte(IEnumerable<PlcBit.Signal> list, int count)
        {
            var buffer = new List<byte>();

            for (int i = 0; i < count; i++)
            {
                var first = string.Format("{1}{0}"
                    , string.Join("", list.Skip(i * 4).Take(4).Reverse().Select(t => t == PlcBit.Signal.ON ? "1" : "0")).PadLeft(4, '0')
                    , string.Join("", list.Skip((i + 1) * 4).Take(4).Reverse().Select(t => t == PlcBit.Signal.ON ? "1" : "0")).PadLeft(4, '0'));

                var second = string.Format("{1}{0}"
                    , string.Join("", list.Skip((i + 2) * 4).Take(4).Reverse().Select(t => t == PlcBit.Signal.ON ? "1" : "0")).PadLeft(4, '0')
                    , string.Join("", list.Skip((i + 3) * 4).Take(4).Reverse().Select(t => t == PlcBit.Signal.ON ? "1" : "0")).PadLeft(4, '0'));

                buffer.Add(Convert.ToByte(first, 2));
                buffer.Add(Convert.ToByte(second, 2));
            }

            return buffer.ToArray();
        }
    }
}
