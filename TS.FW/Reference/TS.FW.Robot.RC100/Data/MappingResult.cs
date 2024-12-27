using System.Collections.Generic;

namespace TS.FW.Robot.RC100.Data
{
    public class MappingResult
    {
        public bool Complete { get; internal set; } = false;

        public List<WaferState> Wafers { get; internal set; } = new List<WaferState>();
    }
}
