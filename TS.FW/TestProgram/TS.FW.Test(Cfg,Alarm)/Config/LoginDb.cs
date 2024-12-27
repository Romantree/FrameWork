using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Cfg;

namespace TS.FW.Test.Config
{
    public class LoginDb : IConfigDb
    {
        public bool IsLock { get => this.GetValueBool(); set => this.SetValue(value); }

        public string Name { get => this.GetValue(); set => this.SetValue(value); }

        public int Age { get => GetValueInt(); set => this.SetValue(value); }

        public string Password { get => GetValue(); set => this.SetValue(value); }

        public double Data { get => this.GetValueDouble(); set => this.SetValue(value); }
    }
}
