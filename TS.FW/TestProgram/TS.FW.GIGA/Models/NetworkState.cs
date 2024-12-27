using System;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.GIGA.Models
{
    public class NetworkState : IModel
    {
        public bool LeftLD { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool RightLD { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool UvLamp { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool LeftRG { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool RightRG { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public void Update()
        {
            try
            {
                this.LeftLD = true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
