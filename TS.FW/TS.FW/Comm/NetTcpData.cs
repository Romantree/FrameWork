using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Comm
{
    public class NetTcpData
    {
        public int IpAdd1 { get; set; }
        public int IpAdd2 { get; set; }
        public int IpAdd3 { get; set; }
        public int IpAdd4 { get; set; }
        public int Port { get; set; }
        public int TimeoutMs { get; set; }
        public int RecvBufferSize { get; set; }
        public int RecvTimeout { get; set; }

        public NetTcpData()
        {
            TimeoutMs = 1000;
            RecvBufferSize = 4096;
            RecvTimeout = 1000;
        }

        public string ToIPAddress()
        {
            return string.Format("{0}.{1}.{2}.{3}", this.IpAdd1, this.IpAdd2, this.IpAdd3, this.IpAdd4);
        }
    }
}
