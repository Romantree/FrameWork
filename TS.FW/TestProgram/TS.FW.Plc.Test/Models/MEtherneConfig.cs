using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Plc.Test.Models
{
    public class MEtherneConfig : IPlcConfigModel
    {
        public string IpAddress { get => this.GetValue<string>(); set => this.SetValue(value); }

        public int Port { get => this.GetValue<int>(); set => this.SetValue(value); }

        public byte NetworkNo { get => this.GetValue<byte>(); set => this.SetValue(value); }

        public byte StationNo { get => this.GetValue<byte>(); set => this.SetValue(value); }

        public MEtherneConfig()
        {
            this.IpAddress = "127.0.0.1";
            this.Port = 5000;
            this.NetworkNo = 1;
            this.StationNo = 2;
        }
    }
}
