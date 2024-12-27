using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.LS
{
    public class LSHeader
    {

        /// <summary>
        /// ASCII 문자열
        /// LSIS-XGT 값으로 고정
        /// </summary>
        public const ulong COMPANY_ID = 0x4C5349532d584754;

        /// <summary>
        /// companyID가 LGIS-GLOFA인 module 사용시 필요한 영역
        /// 우리한테 필요 없는 영역으로 0x0000으로 고정
        /// </summary>
        public const ushort RESERVED_ID = 0x0000;

        /// <summary>
        /// cpuInfo
        /// PC to PLC : 0x0000
        /// Dont't Care
        /// PLC to PC : Read from PLC
        /// </summary>
        public const ushort PLC_INFO = 0x0000;

        /// <summary>
        /// cpuInfo
        /// 우리가 사용하는 CPU Module : XGI-CPUH
        /// 따라서 우리는 XGI 시리즈 값인 0xA4로 고정 값 사용
        /// </summary>
        public const byte CPU_INFO = 0xA4;

        /// <summary>
        /// Source of Frame
        /// 통신에 대한 방향성 영역
        /// PC to PLC : 0x33
        /// PLC to PC : 0x11
        /// PC to PLC 값을 기본값으로 사용
        /// </summary>
        public const byte H33_PCtoPLC = 0x33;

        /// <summary>
        /// PLC에서 사용하는 영역
        /// PC to PLC의 경우 zero값으로 고정
        /// </summary>
        public const ushort INVOKE_ID = 0x0000;

        public ulong CompanyID { get; private set; }

        public ushort ReservedID { get; private set; }

        public ushort PLCInfo { get; private set; }

        public byte CPUInfo { get; private set; }

        public byte H33 { get; private set; }

        public ushort InvokeID { get; private set; }

        internal LSHeader(ushort plcInfo = LSHeader.PLC_INFO, byte direction = LSHeader.H33_PCtoPLC,
                             ushort invokeID = LSHeader.INVOKE_ID)
        {
            this.CompanyID = LSHeader.COMPANY_ID; 
            this.ReservedID = LSHeader.RESERVED_ID;
            this.PLCInfo = plcInfo;
            this.CPUInfo = LSHeader.CPU_INFO;
            this.H33 = direction;
            this.InvokeID = invokeID;
        }

        internal LSHeader(byte[] buffer, ref int index)
        {
            if (buffer.Length < 16) throw new IndexOutOfRangeException(string.Format("PLC 수신 데이터 파싱 중 에러가 발생하였습니다.헤더 크기가 맞지 않습니다.Size = {0}", buffer.Length));
            this.CompanyID = buffer.ToUInt64(ref index, BitHandler.ByteOrder.BigEndian);
            this.ReservedID = buffer.ToUInt16(ref index, BitHandler.ByteOrder.BigEndian);
            this.PLCInfo = buffer.ToUInt16(ref index, BitHandler.ByteOrder.BigEndian);
            this.CPUInfo = buffer[index++];
            this.H33 = buffer[index++];
            this.InvokeID = buffer.ToUInt16(ref index, BitHandler.ByteOrder.BigEndian);
        }

        public virtual IEnumerable<byte> ToByte()
        {
            foreach (var item in this.CompanyID.ToByte(BitHandler.ByteOrder.BigEndian))
            {
                yield return item;
            }

            foreach (var item in this.ReservedID.ToByte(BitHandler.ByteOrder.BigEndian))
            {
                yield return item;
            }
            foreach (var item in this.PLCInfo.ToByte(BitHandler.ByteOrder.BigEndian))
            {
                yield return item;
            }

            yield return this.CPUInfo;
            yield return this.H33;

            foreach (var item in this.InvokeID.ToByte(BitHandler.ByteOrder.BigEndian))
            {
                yield return item;
            }
        }

        public virtual string ToHexString()
        {
            return string.Join(" ", this.ToByte().Select(t => t.ToHex()));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("HEADER :");
            sb.AppendLine(string.Format("- CompanyID[{0}]", this.CompanyID.ToHex()));
            sb.AppendLine(string.Format("- Reserved[{0}]", this.ReservedID.ToHex()));
            sb.AppendLine(string.Format("- PLCInfo[{0}]", this.PLCInfo.ToHex()));
            sb.AppendLine(string.Format("- CPUInfo[{0}]", this.CPUInfo.ToHex()));
            sb.AppendLine(string.Format("- H33[{0}]", this.H33.ToHex()));
            sb.AppendLine(string.Format("- InvokeID[{0}]", this.InvokeID.ToHex()));
            return sb.ToString();
        }

        public static implicit operator byte[] (LSHeader header)
        {
            if (header == null) throw new NullReferenceException("Plc Header 정보가 Null 입니다.");

            return header.ToByte().ToArray();
        }
    }
}
