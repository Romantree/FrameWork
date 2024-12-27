using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Cfg;

namespace TS.FW.Test.Config
{
    public class ParamDb : IConfigDb
    {
        public int ParamInt { get => this.GetValueInt(); set => this.SetValue(value); }
        public double ParamDouble { get => this.GetValueDouble(); set => this.SetValue(value); }
        public bool ParamBool { get => this.GetValueBool(); set => this.SetValue(value); }
        public string ParamString { get => this.GetValue(); set => this.SetValue(value); }

    }
}
