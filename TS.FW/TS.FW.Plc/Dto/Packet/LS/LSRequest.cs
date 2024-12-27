using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Plc.Dto.Packet.LS.Command;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.LS
{
    public class LSRequest : LSHeader
    {
        /// <summary>
        /// Header Frame 뒤에 오는 Data들의 총 Byte 길이
        /// 직접변수 연속쓰기시 최대 16 byte
        /// 이에따라 16byte로 고정값 사용(Manual Spec)
        /// </summary>
        
        /// <summary>
        /// 예약영역
        /// PLC에서 사용하는 영역
        /// 우리는 zero값으로 셋팅
        /// Bit 0 ~ 3 : Ethernet I/F module Slot Number
        /// Bit 4 ~ 7 : Ehternet I/F module Base Number
        /// </summary>
        public const byte ETHERNET_RESERVED = 0x00;

        /// <summary>
        /// 예약영역
        /// Application Header의 Byte Sum
        /// PLC에서 사용하는 영역으로 zero값으로 셋팅
        /// </summary>
        public const byte BCC_RESERVED = 0x00;

        public ushort Length
        {
            get
            {
                return (ushort)this.ToData().Count();
            }
        }

        public byte FenetPosition { get; private set; }

        public byte BCC { get; private set; }

        public ILSCommandBase Command { get; set; }

        public LSRequest(byte fenetPosition = LSRequest.ETHERNET_RESERVED, byte bcc = LSRequest.BCC_RESERVED)
        {
            this.FenetPosition = fenetPosition;
            this.BCC = bcc;
        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
               yield return item;
            }

            foreach (var item in this.ToPacket())
            {
                yield return item;
            }
        }

        public override string ToHexString()
        {
            return string.Join(" ", this.ToByte().Select(t => t.ToHex()));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Format("PACKET [{0}]", this.ToHexString()));
            sb.AppendLine(base.ToString());
            sb.AppendLine("BODY :");
            sb.AppendLine(string.Format("- Lenght[{0}]  : {1}", this.Length.ToHex(), this.Length));

            if (this.Command != null)
            {
                sb.AppendLine();
                sb.Append(this.Command.ToString());
            }

            return sb.ToString();
        }

        private IEnumerable<byte> ToPacket()
        {
            foreach (var item in this.Length.ToByte(BitHandler.ByteOrder.LittleEndian))
            {
                yield return item;
            }

            yield return this.FenetPosition;
            yield return this.BCC;

            foreach (var item in this.ToData())
            {
                yield return item;
            }
        }

        private IEnumerable<byte> ToData()
        {
            if (this.Command != null)
            {
                foreach (var item in this.Command.ToByte())
                {
                    yield return item;
                }
            }
        }

        public static implicit operator byte[] (LSRequest packet)
        {
            if (packet == null) throw new NullReferenceException("PLC Request 정보가 NULL 입니다. ");

            return packet.ToByte().ToArray();
        }
    }
}
