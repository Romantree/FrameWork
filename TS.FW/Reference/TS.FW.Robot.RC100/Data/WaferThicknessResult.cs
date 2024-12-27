using System.Collections.Generic;

namespace TS.FW.Robot.RC100.Data
{
    public class WaferThicknessResult
    {
        public bool Complete { get; internal set; } = false;

        public List<int> WaferThickness { get; internal set; } = new List<int>();
    }
}
