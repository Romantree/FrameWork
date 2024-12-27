using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Plc.Dto.Packet.LS.Command;
using TS.FW.Plc.Dto.Packet.Mitsubishi.Command;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.LS
{
    public class LSResponse : LSHeader
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

        public ushort CommandID { get; private set; }

        public ushort DataType { get; private set; }

        public ushort Reserved { get; private set; }

        public ushort ErrState { get; private set; }

        public ushort ErrCode { get; private set; }

        public ushort VariablesCount { get; private set; }

        public ushort DataByteSize { get; private set; }

        public ushort DataValue { get; private set; }
        
        public LSResponse(byte[] buffer, ref int index) : base(buffer, ref index)
        {
            this.Length = buffer.ToUInt16(ref index);
            this.EndCode = buffer.ToUInt16(ref index);

            this.CommandID = buffer.ToUInt16(ref index);
            this.DataType = buffer.ToUInt16(ref index);
            this.Reserved = buffer.ToUInt16(ref index);
            this.ErrState = buffer.ToUInt16(ref index);

            if (this.ErrState != 0)
            {
                this.ErrCode = buffer.ToUInt16(ref index);
                this.Buffer = buffer.Skip(index).ToArray();
            }
            else
            {
                this.VariablesCount = buffer.ToUInt16(ref index);
                this.Buffer = buffer.Skip(index).ToArray();
            }
        }

        public void CheckPacket()
        {
            if (this.CompanyID != LSHeader.COMPANY_ID) throw new Exception(string.Format("PLC 수신 데이터 파싱 도중 오류가 발생하였습니다. 시작 코드가 맞지 않습니다. {0}", this.CompanyID.ToHex()));
            if (this.ErrState != 0x0000)
                throw new Exception(string.Format("PLC 수신 데이터 파싱 도중 오류가 발생하였습니다. 오류 코드가 발생하였습니다. EndCod({0}) {1}", this.EndCode, (PlcErrorResponse)this.Buffer));
        }

        /// <summary>
        /// 비트 1개 읽기
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<int, PlcBit.Signal>> LSReadBitCommandProcess(ILSReadEachCommand command)
        {
            if(this.VariablesCount > 0 && command != null)
            {
                var index = 0;

                for (int i = 0; i < this.VariablesCount; i++)
                {
                    var dataSize = this.Buffer.ToUInt16(ref index);
                    yield return new KeyValuePair<int, PlcBit.Signal>(command.StartAddress + i, this.Buffer[index++] == 1 ? PlcBit.Signal.ON : PlcBit.Signal.OFF);
                }
            }
        }

        /// <summary>
        /// 비트 연속 읽기
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<int, PlcBit.Signal>> LSReadBitListCommandProcess(ILSReadContinueCommand command)
        {
            if (this.VariablesCount > 0 && command != null)
            {
                var index = 0;
                var dataSize = this.Buffer.ToUInt16(ref index);
                var dataBuffer = this.Buffer.Skip(index).Take(dataSize);

                foreach (var item in dataBuffer.SelectMany(t => Convert.ToString(t, 2).PadLeft(8, '0').Reverse())
                    .Select((t, i) => new { address = i, Data = t == '0' ? PlcBit.Signal.OFF : PlcBit.Signal.ON }))
                {
                    yield return new KeyValuePair<int, PlcBit.Signal>(command.StartAddress + item.address, item.Data);
                }
            }
        }

        /// <summary>
        /// 워드 개별 1개
        /// </summary>
        /// <param name="command"></param>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<PlcWord, object>> LSReadWordCommandProcess(ILSReadEachCommand command, IEnumerable<PlcWord> wordList)
        {
            if (command != null && this.VariablesCount > 0)
            {
                var dataBuffer = this.GetDataBuffer().ToArray();
                var index = 0;

                foreach (var word in wordList)
                {
                    yield return new KeyValuePair<PlcWord, object>(word, word.ToDataParse(dataBuffer, ref index));
                }
            }
        }

        public IEnumerable<byte> GetDataBuffer()
        {
            for (int i = 0; i < this.Buffer.Length;)
            {
                var dataSize = this.Buffer.ToUInt16(ref i);

                foreach (var data in this.Buffer.Skip(i).Take(dataSize))
                {
                    yield return data;
                }

                i += dataSize;
            }
        }

        /// <summary>
        /// 워드 연속 읽기
        /// </summary>
        /// <param name="command"></param>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<PlcWord, object>> LSReadWordListCommandProcess(ILSReadContinueCommand command, IEnumerable<PlcWord> wordList)
        {
            if (command != null && this.VariablesCount > 0)
            {
                int SizeIndex = 0;
                int SizeValue = 0;
                int ValueIndex = 0;

                for (int i = 0; i < VariablesCount; i++)
                {
                    SizeIndex = SizeValue + ValueIndex;
                    ValueIndex = SizeIndex + 2;
                    this.DataByteSize = this.Buffer.ToUInt16(SizeIndex);
                    SizeValue = this.DataByteSize;
                    var dataBuffer = this.Buffer.Skip(2).Take(SizeValue).ToArray();

                    for (int index = 0; index < this.Buffer.Length - 2;)
                    {
                        var word = wordList.FirstOrDefault(t => t.Address == (command.StartAddress + ((index) / 2)));
                        if (word == null) break; // continue 처리 할 경우.. 함수에서 나올수가 없음...

                        yield return new KeyValuePair<PlcWord, object>(word, word.ToDataParse(dataBuffer, ref index));
                    }
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

        public static implicit operator LSResponse(byte[] buffer)
        {
            var index = 0;
            return new LSResponse(buffer, ref index);
        }
    }
}
