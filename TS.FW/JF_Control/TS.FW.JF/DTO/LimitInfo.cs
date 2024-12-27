using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.JF.DTO
{
    public class LimitInfo
    {
        public enStopAct HwLimitAct { get; internal set; }
        public enStopAct AlarmAct { get; internal set; }
        public enLevel LimitLevel { get; internal set; }
        public enLevel HomeLevel { get; internal set; }
        public enLevel LatchLevel { get; internal set; }
        public enLevel InPosLevel { get; internal set; }
        public enLevel AmpLevel { get; internal set; }
    }
}
