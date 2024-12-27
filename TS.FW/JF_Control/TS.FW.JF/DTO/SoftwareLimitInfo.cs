using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.JF.DTO
{
    public class SoftwareLimitInfo
    {
        public bool Enable { get; internal set; }

        public double PosLimitPos { get; set; }
        public double NegLimitPos { get; set; }

        public enStopAct PosLimitAct { get; set; }
        public enStopAct NegLimitAct { get; set; }
    }
}
