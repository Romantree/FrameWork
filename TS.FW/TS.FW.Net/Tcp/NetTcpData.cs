using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Net.Tcp
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
        
        public void SetIpAddress(string str)
        {
            var lists = str.Split('.');
            if (lists.Count() < 3) return;
            IpAdd1 = int.Parse(lists[0]);
            IpAdd2 = int.Parse(lists[1]);
            IpAdd3 = int.Parse(lists[2]);
            IpAdd4 = int.Parse(lists[3]);
        }
        public string IPAddress => string.Format("{0}.{1}.{2}.{3}", this.IpAdd1, this.IpAdd2, this.IpAdd3, this.IpAdd4);

        public NetTcpData()
        {
            TimeoutMs = 1000;
            RecvBufferSize = 1024;
            RecvTimeout = 1000;
        }
    }
}
