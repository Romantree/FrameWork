using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device.Ajin.Dto.Axis
{
    public class GantrySettingInfo
    {
        public bool Enable { get; internal set; }

        public bool HomeUse { get; internal set; }

        public double OffSet { get; internal set; }

        public double Range { get; internal set; }

        public static implicit operator bool(GantrySettingInfo info)
        {
            return info.Enable;
        }
    }
}
