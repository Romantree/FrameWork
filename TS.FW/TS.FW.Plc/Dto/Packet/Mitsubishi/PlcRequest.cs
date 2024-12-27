using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi
{
    public class PlcRequest : PlcHeader
    {
        public const ushort REQUEST_CPUTIMER = 0x1000;

        /// <summary>
        /// 데이터 통신시 Send Buffer 길이는 2(16)승 + 9 길이 만큼 전송 가능 65544
        /// </summary>
        public ushort Length
        {
            get
            {
                return (ushort)this.ToData().Count();
            }
        }

        public ushort CpuTimer { get; private set; }

        public IPlcCommandBase Command { get; set; }

        public PlcRequest(byte networkNo = 0x02, byte plcNo = 0x01, byte stationNo = 0x01) : base(networkNo, plcNo, stationNo)
        {
            this.CpuTimer = REQUEST_CPUTIMER;
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

        // 예) 0x50 0x00 0x01 .......
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
            sb.AppendLine(string.Format("- CpuTimer[{0}]", this.CpuTimer.ToHex()));

            if (this.Command != null)
            {
                sb.AppendLine();
                sb.Append(this.Command.ToString());
            }

            return sb.ToString();
        }

        private IEnumerable<byte> ToPacket()
        {
            foreach (var item in this.Length.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.ToData())
            {
                yield return item;
            }
        }

        private IEnumerable<byte> ToData()
        {
            foreach (var item in this.CpuTimer.ToByte(BitHandler.ByteOrder.BigEndian))
            {
                yield return item;
            }

            if (this.Command != null)
            {
                foreach (var item in this.Command.ToByte())
                {
                    yield return item;
                }
            }
        }

        public static implicit operator byte[] (PlcRequest packet)
        {
            if (packet == null) throw new NullReferenceException("PLC Request 정보가 NULL 입니다. ");

            return packet.ToByte().ToArray();
        }
    }
}
