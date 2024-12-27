using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TS.FW.Wpf.Core;
using TS.FW.Test.Config;

namespace TS.FW.Test.Models
{
    public class LoginModel : ModelBase
    {
        [DataMember]
        public bool IsLock { get => this.GetValue<bool>(); set => this.SetValue(value); }
        [DataMember]
        public string Name { get => this.GetValue<string>(); set => this.SetValue(value); }
        [DataMember]
        public int Age { get => this.GetValue<int>(); set => this.SetValue(value); }
        [DataMember]
        public string Password { get => this.GetValue<string>(); set => this.SetValue(value); }
        [DataMember]
        public double Data { get => this.GetValue<double>(); set => this.SetValue(value); }

        public static implicit operator LoginDb(LoginModel item)
        {
            if (item == null) return null;

            return new LoginDb()
            {
                IsLock = item.IsLock,
                Name = item.Name,
                Age = item.Age,
                Password = item.Password,
                Data = item.Data,
            };
        }
        public static implicit operator LoginModel(LoginDb item)
        {
            if (item == null) return null;

            return new LoginModel()
            {
                IsLock = item.IsLock,
                Name = item.Name,
                Age = item.Age,
                Password = item.Password,
                Data = item.Data,
            };
        }
    }
}
