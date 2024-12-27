using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Test.Config;
using TS.FW.Wpf.Core;

namespace TS.FW.Test.Models
{
    public class ParamModel : ModelBase
    {
        [DataMember]
        public int ParamInt { get => this.GetValue<int>(); set => this.SetValue(value); }
        [DataMember]
        public double ParamDouble { get => this.GetValue<double>(); set => this.SetValue(value); }
        [DataMember]
        public bool ParamBool { get => this.GetValue<bool>(); set => this.SetValue(value); }
        [DataMember]
        public string ParamString { get => this.GetValue<string>(); set => this.SetValue(value); }


        public static implicit operator ParamDb(ParamModel item)
        {
            if (item == null) return null;

            return new ParamDb()
            {
                ParamInt = item.ParamInt,
                ParamDouble = item.ParamDouble,
                ParamBool = item.ParamBool,
                ParamString = item.ParamString,
            };
        }
        public static implicit operator ParamModel(ParamDb item)
        {
            if (item == null) return null;

            return new ParamModel()
            {
                ParamInt = item.ParamInt,
                ParamDouble = item.ParamDouble,
                ParamBool = item.ParamBool,
                ParamString = item.ParamString,
            };
        }
    }
}
