using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Wpf.Core;

namespace TS.FW.Test.Config
{
    public class RecipeModel : ModelBase, ICloneable
    {
        [DataMember]
        public int No { get => this.GetValue<int>(); set => this.SetValue(value); }
        [DataMember]
        public string Name { get => this.GetValue<string>(); set => this.SetValue(value); }
        [DataMember]
        public bool SetValueBool { get => this.GetValue<bool>(); set => this.SetValue(value); }
        [DataMember]
        public int SetValueInt { get => this.GetValue<int>(); set => this.SetValue(value); }
        [DataMember]
        public double SetValueDouble { get => this.GetValue<double>(); set => this.SetValue(value); }
        [DataMember]
        public string SetValueString { get => this.GetValue<string>(); set => this.SetValue(value); }


        public RecipeModel()
        {
            Name = string.Empty;
            SetValueString = string.Empty;
        }
        public object Clone()
        {
            var ret = new RecipeModel();
            ret.No = No;
            ret.Name = Name;
            ret.SetValueBool = SetValueBool;
            ret.SetValueInt = SetValueInt;
            ret.SetValueDouble = SetValueDouble;
            ret.SetValueString = SetValueString;

            return ret;
        }
    }
}
