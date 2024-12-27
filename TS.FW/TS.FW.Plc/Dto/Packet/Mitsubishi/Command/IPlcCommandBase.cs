using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public abstract class IPlcCommandBase
    {
        public PlcCommandType CommandType { get; private set; }

        public IPlcCommandBase(PlcCommandType commandType)
        {
            this.CommandType = commandType;
        }

        public virtual IEnumerable<byte> ToByte()
        {
            foreach (var item in ((uint)this.CommandType).ToByte(BitHandler.ByteOrder.Mid_BigEndian))
            {
                yield return item;
            }
        }

        public override string ToString()
        {
            return string.Format("COMMAND :\r\n- CommandType[{0}] : {1}", ((uint)this.CommandType).ToHex(), this.CommandType);
        }

        protected string ToString(List<RandomAddress> list)
        {
            if (list == null || list.Count <= 0) return string.Empty;

            var sb = new StringBuilder();

            var temp = list.Count / 10;

            for (int i = 0; i <= temp; i++)
            {
                sb.AppendLine(string.Join(" ", list.Skip(i * 10).Take(10)));
            }

            return sb.ToString();
        }
    }

    public abstract class IPlcReadWriteCommandBase : IPlcCommandBase
    {
        public int StartAddress { get; protected set; }

        public PlcDeviceCode Code { get; protected set; }

        public ushort Count { get; protected set; }

        public IPlcReadWriteCommandBase(PlcCommandType commandType) : base(commandType)
        {

        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.StartAddress.ToByte().Take(3))
            {
                yield return item;
            }

            yield return (byte)this.Code;

            foreach (var item in this.Count.ToByte())
            {
                yield return item;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine(string.Format("- StartAddress[{0}] : {1}", this.StartAddress.ToHex(), this.StartAddress));
            sb.AppendLine(string.Format("- Code[{0}] : {1}", ((byte)this.Code).ToHex(), this.Code));
            sb.AppendLine(string.Format("- Count[{0}] : {1}", this.Count.ToHex(), this.Count));

            return sb.ToString();
        }
    }

    public abstract class IPlcRandomWordCommandBase : IPlcCommandBase
    {
        public byte WordCount { get; protected set; }

        public byte DWordCount { get; protected set; }

        public List<RandomAddress> WordList { get; protected set; }

        public List<RandomAddress> DWordList { get; protected set; }

        public IPlcRandomWordCommandBase(PlcCommandType commandType) : base(commandType)
        {
            this.WordList = new List<RandomAddress>();
            this.DWordList = new List<RandomAddress>();
        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }

            yield return WordCount;
            yield return DWordCount;

            if (this.WordList != null && this.WordList.Count > 0)
            {
                foreach (var item in this.WordList.SelectMany(t => t.ToByte()))
                {
                    yield return item;
                }
            }

            if (this.DWordList != null && this.DWordList.Count > 0)
            {
                foreach (var item in this.DWordList.SelectMany(t => t.ToByte()))
                {
                    yield return item;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine(string.Format("- WordCount[{0}] : {1}", this.WordCount.ToHex(), this.WordCount));
            sb.AppendLine(string.Format("- DWordCount[{0}] : {1}", ((byte)this.DWordCount).ToHex(), this.DWordCount));

            sb.AppendLine();

            sb.AppendLine("WORD LIST :");
            sb.AppendLine(this.ToString(this.WordList));

            sb.AppendLine("DWORD LIST :");
            sb.Append(this.ToString(this.DWordList));

            return sb.ToString();
        }
    }
}
