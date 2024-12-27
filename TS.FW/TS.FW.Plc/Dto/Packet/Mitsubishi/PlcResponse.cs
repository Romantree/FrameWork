using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi
{
    public class PlcResponse : PlcHeader
    {
        class PlcErrorResponse
        {
            public byte NetworkNo { get; private set; }

            public byte PlcNo { get; private set; }

            public ushort InoutNo { get; private set; }

            public byte StationNo { get; private set; }

            public PlcCommandType CommandType { get; private set; }

            private PlcErrorResponse(byte[] buffer, ref int index)
            {
                this.NetworkNo = buffer[index++];
                this.PlcNo = buffer[index++];
                this.InoutNo = buffer.ToUInt16(ref index, BitHandler.ByteOrder.BigEndian);
                this.StationNo = buffer[index++];
                this.CommandType = (PlcCommandType)buffer.ToUInt32(ref index, BitHandler.ByteOrder.Mid_BigEndian);
            }

            public static implicit operator PlcErrorResponse(byte[] buffer)
            {
                var index = 0;

                return new PlcErrorResponse(buffer, ref index);
            }

            public override string ToString()
            {
                return string.Format("NetworkNo({0}), PlcNo({1}), StationNo({2}), CommandType({3})", this.NetworkNo, this.PlcNo, this.StationNo, this.CommandType);
            }
        }

        public ushort Length { get; private set; }

        public ushort EndCode { get; private set; }

        public byte[] Buffer { get; private set; }

        public PlcResponse(byte[] buffer, ref int index) : base(buffer, ref index)
        {
            this.Length = buffer.ToUInt16(ref index);
            this.EndCode = buffer.ToUInt16(ref index);
            this.Buffer = buffer.Skip(index).ToArray();
        }

        public void CheckPacket()
        {
            if (this.STX != PlcHeader.STX_RESPONSE) throw new Exception(string.Format("PLC 수신 데이터 파싱 도중 오류가 발생하였습니다. 시작 코드가 맞지 않습니다. {0}", this.STX.ToHex()));
            if (this.EndCode != 0x00)
                throw new Exception(string.Format("PLC 수신 데이터 파싱 도중 오류가 발생하였습니다. 오류 코드가 발생하였습니다. EndCod({0}) {1}", this.EndCode, (PlcErrorResponse)this.Buffer));
        }

        public IEnumerable<KeyValuePair<int, PlcBit.Signal>> ReadBitCommandProcess(PlcReadBitCommand command)
        {
            if (command != null && this.Buffer != null && this.Buffer.Length > 0)
            {
                var buffer = this.Buffer.Select(t => new byte[] { (byte)((t & 0x10) >> 4), (byte)(t & 0x01) }).SelectMany(t => t).ToArray();

                foreach (var item in buffer.Select((t, i) => new { Index = i, Value = t }))
                {
                    yield return new KeyValuePair<int, PlcBit.Signal>(command.StartAddress + item.Index, (PlcBit.Signal)item.Value);
                }
            }
        }

        public IEnumerable<KeyValuePair<int, PlcBit.Signal>> ReadBitFromWordCommandProcess(PlcReadWordCommand command)
        {
            if (command != null && this.Buffer != null && this.Buffer.Length > 0)
            {
                var buffer = this.Buffer.Select(t => Convert.ToString(t, 2).PadLeft(8, '0').Reverse()).SelectMany(t => t).Select(t => t == '1' ? PlcBit.Signal.ON : PlcBit.Signal.OFF);

                foreach (var item in buffer.Select((t, i) => new { Index = i, Value = t }))
                {
                    yield return new KeyValuePair<int, PlcBit.Signal>(command.StartAddress + item.Index, (PlcBit.Signal)item.Value);
                }
            }
        }

        public IEnumerable<KeyValuePair<PlcWord, object>> ReadWordCommandProcess(PlcReadWordCommand command, IEnumerable<PlcWord> wordList)
        {
            if (command != null && this.Buffer != null && this.Buffer.Length > 0)
            {
                for (int index = 0; index < this.Buffer.Length;)
                {
                    var word = wordList.FirstOrDefault(t => t.Address == (command.StartAddress + (index / 2)));
                    if (word == null)
                    {
                        index += 2;
                        continue;
                    }

                    yield return new KeyValuePair<PlcWord, object>(word, word.ToDataParse(this.Buffer, ref index));
                }
            }
        }

        public IEnumerable<KeyValuePair<int, byte[]>> ReadWordCommandProcess(PlcReadWordCommand command)
        {
            if (command != null && this.Buffer != null && this.Buffer.Length > 0)
            {
                for (int index = 0; index < this.Buffer.Length; index += 2)
                {
                    var address = command.StartAddress + (index / 2);

                    yield return new KeyValuePair<int, byte[]>(address, new byte[] { this.Buffer[index], this.Buffer[index + 1] });
                }
            }
        }

        public IEnumerable<KeyValuePair<PlcWord, object>> ReadRandomCommandProcess(PlcRandomReadCommand command, IEnumerable<PlcWord> wordList)
        {
            if (command != null && this.Buffer != null && this.Buffer.Length > 0)
            {
                var index = 0;

                foreach (var item in command.WordList)
                {
                    var word = wordList.FirstOrDefault(t => t.Address == item.Address);
                    if (word == null) continue;

                    yield return new KeyValuePair<PlcWord, object>(word, word.ToDataParse(this.Buffer, ref index));
                }

                foreach (var item in command.DWordList)
                {
                    var word = wordList.FirstOrDefault(t => t.Address == item.Address);
                    if (word == null) continue;

                    yield return new KeyValuePair<PlcWord, object>(word, word.ToDataParse(this.Buffer, ref index));
                }
            }
        }

        public override string ToHexString()
        {
            return string.Join(" ", this.ToByte().Concat(this.Buffer).Select(t => t.ToHex()));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Format("PACKET [{0}]", this.ToHexString()));
            sb.AppendLine(base.ToString());
            sb.AppendLine("BODY :");
            sb.AppendLine(string.Format("- Lenght[{0}]  : {1}", this.Length.ToHex(), this.Length));
            sb.AppendLine(string.Format("- EndCode[{0}]", this.EndCode.ToHex()));

            sb.AppendLine();
            sb.AppendLine("DATA :");
            sb.AppendLine(this.Buffer.ToHex());

            return sb.ToString();
        }
        
        public static implicit operator PlcResponse(byte[] buffer)
        {
            var index = 0;
            return new PlcResponse(buffer, ref index);
        }
    }
}
