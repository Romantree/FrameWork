using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Net.Serial
{
    public class NetSerialData
    {
        public string PortName { get; set; }
        public int PortNo => PortNoEx();

        private int PortNoEx()
        {
            var ret = 0;

            var no = PortName.Replace("COM", "");

            if (int.TryParse(no, out ret) == false)
                return -1;

            return ret;
        }


        public int BaudRate { get; set; }

        public Parity Parity { get; set; }

        public int DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public override string ToString()
        {
            return $"COM:{this.PortName}, BaudRate:{this.BaudRate}, Parity:{this.Parity}, DataBits:{this.DataBits}, StopBits:{this.StopBits}";
        }
    }
}
