using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Controls.UI
{
    public class InOutModel : ModelBase
    {
        public string Key { get => this.GetValue<string>(); set => this.SetValue(value); }

        public int BitNo { get => this.GetValue<int>(); set => this.SetValue(value); }

        public string DisplayName { get => this.GetValue<string>(); set => this.SetValue(value); }

        public enInOutType Type { get => this.GetValue<enInOutType>(); set => this.SetValue(value); }

        public enOutputType OutputType { get => this.GetValue<enOutputType>(); set => this.SetValue(value); }

        public bool OnOff { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsAType { get => this.OutputType == enOutputType.A; }

        public InOutModel(string key)
        {
            this.Key = key;
            this.OnOff = false;
            this.OutputType = enOutputType.A;
        }

        public string GetContents()
        {
            return string.Format("{0}|{1}|{2}", this.BitNo, string.IsNullOrEmpty(this.DisplayName) ? this.Key : this.DisplayName, this.OutputType);
        }

        public void SetContents(string contents)
        {
            var tokens = contents.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 3) throw new Exception(contents);

            this.BitNo = Convert.ToInt32(tokens[0].Trim());
            this.DisplayName = tokens[1].Trim();
            this.OutputType = (enOutputType)Enum.Parse(typeof(enOutputType), tokens[2].Trim());
        }

        protected override void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                base.SetValue(value, propertyName);
            }
            finally
            {
                if (string.Equals(propertyName, nameof(this.OutputType)))
                {
                    this.OnNotifyPropertyChanged(nameof(this.IsAType));
                }
            }
        }
    }
}
