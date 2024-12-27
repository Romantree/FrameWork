using System.Collections.Generic;
using System.Linq;

namespace TS.FW.Plc.Dto.Packet.Mitsubishi.Command
{
    public class PlcRandomWriteWordCommand : IPlcRandomWordCommandBase
    {
        public PlcRandomWriteWordCommand(Dictionary<PlcWord, object> list) : base(PlcCommandType.RandomWriteWord)
        {
            // TODO : 랜덤 쓰기는 Word(1), DWord(2) 사이즈 이상 되는 것을 사용할 수 없음.
            var wordList = list.Where(t => t.Key.Length == 1).OrderBy(t => t.Key.Address);
            var dWordList = list.Where(t => t.Key.Length == 2).OrderBy(t => t.Key.Address);

            this.WordCount = (byte)wordList.Count();
            this.DWordCount = (byte)dWordList.Count();

            this.WordList.AddRange(wordList.Select(t => new RandomAddress(t.Key, t.Key.ToByte(t.Value))));
            this.DWordList.AddRange(dWordList.Select(t => new RandomAddress(t.Key, t.Key.ToByte(t.Value))));
        }
    }
}
