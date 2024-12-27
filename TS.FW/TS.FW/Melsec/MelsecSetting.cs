using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Melsec
{
    public class MelsecSetting
    {
        public int NetworkNo { get; set; } = 0;

        public int StationNo { get; set; } = 255;

        public MDevType DevType { get; set; } = MDevType.LB;

        /// <summary>
        /// Melsec 시작 주소
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Melsec 마지막 주소
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Melsec 시작 주소부터 마지막 주소까지의 길이
        /// </summary>
        public int Lenght => (this.End - this.Start) + 1;

        public override string ToString()
        {
            return string.Format("{2}[{0:X} - {1:X}]({3})", this.Start, this.End, this.DevType, this.Lenght);
        }

        /// <summary>
        /// Melsec Mapping 정보에 대한 format 값으로 Melsec Setting
        /// </summary>
        /// <param name="format"> 예시 : netNo,stnNo,devType,start(시작주소),end(끝주소) => 0,255,LB,0x5000,0x6000 </param>
        public static implicit operator MelsecSetting(string format)
        {
            try
            {
                var tokens = format.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 5) throw new Exception(string.Format("Format 에러 '{0}'", format));

                var setting = new MelsecSetting();

                setting.NetworkNo = Convert.ToInt32(tokens[0]);
                setting.StationNo = Convert.ToInt32(tokens[1]);
                setting.DevType = (MDevType)Enum.Parse(typeof(MDevType), tokens[2]);
                setting.Start = Convert.ToInt32(tokens[3], 16);
                setting.End = Convert.ToInt32(tokens[4], 16);

                return setting;
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(MelsecSetting), ex);
            }

            return null;
        }
    }
}
