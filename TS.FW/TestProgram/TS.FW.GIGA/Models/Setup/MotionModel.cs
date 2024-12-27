using System;
using System.Collections.Generic;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.GIGA.Models.Setup
{
    public class MotionModel : IModel
    {
        public List<AxisModel> Axis { get; set; } = new List<AxisModel>();

        public AxisModel SelectAxis { get => this.GetValue<AxisModel>(); set => this.SetValue(value); }

        public NormalCommand OnSelectAxisCmd => new NormalCommand(SelectAxisCmd);

        public void Update() => this.Axis.ForEach(t => t.Update());

        private void SelectAxisCmd(object param)
        {
            try
            {
                if (this.SelectAxis != null) this.SelectAxis.IsSeleted = false;

                this.SelectAxis = (param as AxisModel);

                if (this.SelectAxis != null)
                {
                    this.SelectAxis.IsSeleted = true;
                    this.SelectAxis.UpdateLimit();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
