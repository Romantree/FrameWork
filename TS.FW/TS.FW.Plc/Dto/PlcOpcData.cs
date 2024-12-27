using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Plc.Dto
{
    public class PlcOpcData
    {
        public PlcBit.Signal OnOff { get; set; }

        public string DataName { get; set; }

        public string OpcItemName { get; set; }

        public object Value { get; set; }

        public PlcOpcData()
        {
            this.DataName = "";
            this.OpcItemName = "";
            this.Value = "";
            this.OnOff = PlcBit.Signal.OFF;
        }
    }
}
