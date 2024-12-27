using System;
using TS.FW.Device.Ajin.Core;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin.Dto.Axis
{
    public class AxisSettingInfo
    {
        public double SCALE { get; set; } = 0.001;

        public int No { get; private set; }

        public AxisLimitInfo Limit { get; internal set; } = new AxisLimitInfo();

        public AXT_MOTION_LEVEL_MODE ServoAlarmResetLevel { get; internal set; }

        public AXT_MOTION_ABSREL AbsRelMode { get; internal set; }

        public AXT_MOTION_PROFILE_MODE ProfileMode { get; internal set; }

        public AXT_ENCODE_TYPE EncodeType { get; internal set; }

        public AXT_MOTION_PULSE_OUTPUT? OutputPulse { get; internal set; }

        public AXT_MOTION_EXTERNAL_COUNTER_INPUT? EncInput { get; internal set; }

        public AxisInitMoveInfo InitMove { get; internal set; } = new AxisInitMoveInfo();

        public HomeSettingInfo Home { get; internal set; } = new HomeSettingInfo();

        public SoftwareLimitInfo SoftwareLimit { get; internal set; } = new SoftwareLimitInfo();

        public AxisSettingInfo(int no)
        {
            this.No = no;
        }

        public void SaveIniMove()
        {
            this.AxmMotSetParaLoad();
        }

        public void SetOutputPulse(AXT_MOTION_PULSE_OUTPUT item)
        {
            try
            {
                this.OutputPulse = item;

                this.AxmMotSetPulseOutMethod();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetEncInput(AXT_MOTION_EXTERNAL_COUNTER_INPUT item)
        {
            try
            {
                this.EncInput = item;

                this.AxmMotSetEncInputMethod();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
