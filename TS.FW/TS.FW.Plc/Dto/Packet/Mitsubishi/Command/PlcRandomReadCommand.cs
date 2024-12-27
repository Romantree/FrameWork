using System.Collections.Generic;
using System.Linq;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public class PlcRandomReadCommand : IPlcRandomWordCommandBase
    {
        public PlcRandomReadCommand(IEnumerable<PlcWord> list) : base(PlcCommandType.RandomRead)
        {
            // TODO : 랜덤 읽기는 Word(1), DWord(2) 사이즈 이상 되는 것을 사용할 수 없음.
            var wordList = list.Where(t => t.Length == 1).OrderBy(t => t.Address);
            var dWordList = list.Where(t => t.Length == 2).OrderBy(t => t.Address);

            this.WordCount = (byte)wordList.Count();
            this.DWordCount = (byte)dWordList.Count();

            this.WordList.AddRange(wordList.Select(t => new RandomAddress(t)));
            this.DWordList.AddRange(dWordList.Select(t => new RandomAddress(t)));
        }
    }
}
