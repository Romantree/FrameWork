using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Wpf.Core;

namespace TS.FW.Plc.Test.Models
{
    public abstract class IPlcConfigModel : DataModelBase
    {
        public int PlcNo { get => this.GetValue<int>(); set => this.SetValue(value); }

        public string IniFilePath { get => this.GetValue<string>(); set => this.SetValue(value); }

        public IPlcConfigModel()
        {
            this.PlcNo = 1;
            this.IniFilePath = string.Empty;
        }
    }
}
