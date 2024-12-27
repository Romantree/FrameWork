using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi
{
    public class PlcHeader
    {
        public const ushort STX_REQUEST = 0x5000;
        public const ushort STX_RESPONSE = 0xD000;
        public const ushort INOUT_NO = 0xFF03;

        public ushort STX { get; private set; }

        public byte NetworkNo { get; private set; }

        public byte PlcNo { get; private set; }

        public ushort InoutNo { get; private set; }

        public byte StationNo { get; private set; }

        internal PlcHeader(byte networkNo = 0x02, byte plcNo = 0x01, byte stationNo = 0x01)
        {
            this.STX = PlcHeader.STX_REQUEST;
            this.NetworkNo = networkNo;
            this.PlcNo = plcNo;
            this.InoutNo = PlcHeader.INOUT_NO;
            this.StationNo = stationNo;
        }

        internal PlcHeader(byte[] buffer, ref int index)
        {
            if (buffer.Length < 11) throw new IndexOutOfRangeException(string.Format("PLC 수신 데이터 파싱 중 에러가 발생하였습니다. 헤더 크기가 맞지 않습니다. Size = {0}", buffer.Length));

            this.STX = buffer.ToUInt16(ref index, BitHandler.ByteOrder.BigEndian);
            this.NetworkNo = buffer[index++];
            this.PlcNo = buffer[index++];
            this.InoutNo = buffer.ToUInt16(ref index, BitHandler.ByteOrder.BigEndian);
            this.StationNo = buffer[index++];
        }

        public virtual IEnumerable<byte> ToByte()
        {
            foreach (var item in this.STX.ToByte(BitHandler.ByteOrder.BigEndian))
            {
                yield return item;
            }

            yield return this.NetworkNo;
            yield return this.PlcNo;

            foreach (var item in this.InoutNo.ToByte(BitHandler.ByteOrder.BigEndian))
            {
                yield return item;
            }

            yield return this.StationNo;
        }

        public virtual string ToHexString()
        {
            return string.Join(" ", this.ToByte().Select(t => t.ToHex()));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("HEADER :");
            sb.AppendLine(string.Format("- STX[{0}]", this.STX.ToHex()));
            sb.AppendLine(string.Format("- NetworkNo[{0}] : {1}", this.NetworkNo.ToHex(), this.NetworkNo));
            sb.AppendLine(string.Format("- PlcNo[{0}]     : {1}", this.PlcNo.ToHex(), this.PlcNo));
            sb.AppendLine(string.Format("- InoutNo[{0}]", this.InoutNo.ToHex()));
            sb.AppendLine(string.Format("- StationNo[{0}] : {1}", this.StationNo.ToHex(), this.StationNo));

            return sb.ToString();
        }

        public static implicit operator byte[] (PlcHeader header)
        {
            if (header == null) throw new NullReferenceException("Plc Header 정보가 Null 입니다.");

            return header.ToByte().ToArray();
        }
    }
}
