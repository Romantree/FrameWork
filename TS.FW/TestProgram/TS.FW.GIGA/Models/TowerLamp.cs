using System;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.GIGA.Models
{
    public class TowerLamp : IModel
    {
        private int blnk = 0;

        public bool Red { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Yellow { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Green { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public void Update()
        {
            try
            {
                blnk++;
                if (blnk < 5) return;

                blnk = 0;

                if(!Red && !Yellow && !Green)
                {
                    Red = true;
                }
                else if (Red && !Yellow && !Green)
                {
                    Yellow = true;
                }
                else if (Red && Yellow && !Green)
                {
                    Green = true;
                }
                else if (Red && Yellow && Green)
                {
                    Red = false;
                }
                else if (!Red && Yellow && Green)
                {
                    Yellow = false;
                }
                else if (!Red && !Yellow && Green)
                {
                    Green = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
