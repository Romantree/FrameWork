using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.JF.DTO
{
    public class SettingInfo
    {
        //public AXT_ENCODE_TYPE EncodeType { get; internal set; }
        public int No { get; internal set; }
        public string Name { get; set; }

        public LimitInfo Limit { get; internal set; } = new LimitInfo();
        public InitMoveInfo InitMove { get; internal set; } = new InitMoveInfo();
        public HomeSettingInfo Home { get; set; } = new HomeSettingInfo();
        public SoftwareLimitInfo SoftwareLimit { get; internal set; } = new SoftwareLimitInfo();

        public SettingInfo(int no)
        {
            this.No = no;
            Name = string.Empty;
        }
    }
}
