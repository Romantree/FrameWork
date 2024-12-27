using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device;
using TS.FW.Device.Ajin;
using TS.FW.Device.Ajin.Lib;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.GIGA.Models.Setup
{
    public class AxisModel : IModel
    {
        protected readonly IAxis _axis;
        protected readonly IAxis _axisGantry;

        private eAxis _AxisType => (eAxis)_axis.No;

        private eAxis _AxisGantryType => (eAxis)_axisGantry.No;

        public AxisModel(eAxis type)
        {
            this._axis = AP.Device[type];

            this.Name = this.ToName(type);
            this.GantryModel = false;

            this.State.Update(_axis);
            this.Limit.Update(_axis);
        }

        public AxisModel(eAxis master, eAxis slave) : this(master)
        {
            this._axisGantry = AP.Device[slave];
            this.GantryModel = true;
        }

        public bool IsSeleted { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public string Name { get => this.GetValue<string>(); set => this.SetValue(value); }

        public bool GantryModel { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsGantry { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public AxisStateModel State { get; set; } = new AxisStateModel();

        public AxisLimitModel Limit { get; set; } = new AxisLimitModel();

        public NormalCommand OnAxisCmd => new NormalCommand(AxisCmd);

        public NormalCommand OnAxisMoveCmd => new NormalCommand(AxisMoveCmd);

        public NormalCommand OnAxisStopCmd => new NormalCommand(AxisStopCmd);

        public void Update()
        {
            try
            {
                this.State.Update(_axis);

                if (GantryModel)
                {
                    this.State.IsServoOn = this.State.IsServoOn && _axisGantry.IsServoOn;
                    this.State.IsAlarm = this.State.IsAlarm || _axisGantry.IsAlarm;
                    this.IsGantry = AP.Device.Gantry(_AxisType, true);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void UpdateLimit() => this.Limit.Update(_axis);

        private void AxisCmd(object param)
        {
            try
            {
                switch (param as string)
                {
                    case "Servo":
                        {
                            _axis.IsServoOn = !this.State.IsServoOn;
                        }
                        break;
                    case "Alarm":
                        {
                            _axis.ResetAlarm();
                            if (GantryModel) _axisGantry.ResetAlarm();
                        }
                        break;
                    case "Reset":
                        {
                            _axis.ResetPosition();
                            if (GantryModel) _axisGantry.ResetPosition();
                        }
                        break;
                    case "SetLimit":
                        {
                            this.Limit.SetLimit(_axis);
                        }
                        break;
                    case "UpdateLimit":
                        {
                            UpdateLimit();
                        }
                        break;
                    case "GantryOn":
                        {
                            if (GantryModel == false || State.IsBusy) return;

                            AP.Device.GantryEnable(_AxisType, _AxisGantryType);
                        }
                        break;
                    case "GantryOff":
                        {
                            if (GantryModel == false || State.IsBusy) return;

                            AP.Device.GantryDisable(_AxisType, _AxisGantryType);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void AxisMoveCmd(object param)
        {
            try
            {
                if (this.MoveCheck()) return;

                switch (param as string)
                {
                    case "HOME":
                        {
                            _axis.HomeAsync(out HomeAsyncResult result);
                        }
                        break;
                    case "JOG+":
                        {
                            _axis.MoveVEL(eDirection.Plus, State.Speed, State.Speed * 4, State.Speed * 4);
                        }
                        break;
                    case "JOG-":
                        {
                            _axis.MoveVEL(eDirection.Minus, State.Speed, State.Speed * 4, State.Speed * 4);
                        }
                        break;
                    case "REL+":
                        {
                            _axis.MoveREL(State.RelPos, State.Speed, State.Speed * 4, State.Speed * 4);
                        }
                        break;
                    case "REL-":
                        {
                            _axis.MoveREL(-State.RelPos, State.Speed, State.Speed * 4, State.Speed * 4);
                        }
                        break;
                    case "ABS":
                        {
                            _axis.MoveABS(State.AbsPos, State.Speed, State.Speed * 4, State.Speed * 4);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void AxisStopCmd(object param)
        {
            try
            {
                _axis.Stop();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private bool MoveCheck()
        {
            if (this.State.IsAlarm || this.State.IsBusy) return true;
            if (this.GantryModel && IsGantry == false)
            {
                this.AxisCmd("GantryOn");
                return true;
            }

            return false;
        }

        protected string ToName(eAxis type)
        {
            switch (type)
            {

            }

            return $"{type}".Replace("_", " ");
        }
    }

    public class AxisStateModel : IModel
    {
        public bool IsServoOn { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsAlarm { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsBusy { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsHome { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsPlus { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsMinus { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public double ActPos { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double CmdPos { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double Speed { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double AbsPos { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double RelPos { get => this.GetValue<double>(); set => this.SetValue(value); }

        public AxisStateModel()
        {
            this.Speed = 1;
            this.AbsPos = 0;
            this.RelPos = 1;
        }

        public void Update(IAxis axis)
        {
            try
            {
                if (axis == null)
                {
                    this.IsServoOn = false;
                    this.IsAlarm = true;
                    this.IsBusy = false;
                    this.IsHome = false;
                    this.IsPlus = false;
                    this.IsMinus = false;
                    this.ActPos = 0;
                    this.CmdPos = 0;
                }
                else
                {
                    this.IsServoOn = axis.IsServoOn;
                    this.IsAlarm = axis.IsAlarm;
                    this.IsBusy = axis.IsBusy;
                    this.IsHome = axis.HomeSensor;
                    this.IsPlus = axis.LimitPlus;
                    this.IsMinus = axis.LimitMinus;
                    this.ActPos = axis.ActPosition;
                    this.CmdPos = axis.ComPosition;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class AxisLimitModel : IModel
    {
        public bool Enable { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool StopMode { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Type { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public double Plus { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double Minus { get => this.GetValue<double>(); set => this.SetValue(value); }

        public void Update(IAxis axis)
        {
            try
            {
                var item = axis as AjinAxis;
                if (item == null) return;

                this.Enable = item.Setting.SoftwareLimit.Enable;
                this.StopMode = item.Setting.SoftwareLimit.StopMode == AXT_MOTION_STOPMODE.SLOWDOWN_STOP;
                this.Type = item.Setting.SoftwareLimit.Selection == AXT_MOTION_SELECTION.ACTUAL;
                this.Plus = item.Setting.SoftwareLimit.PositivePos * item.Setting.SCALE;
                this.Minus = item.Setting.SoftwareLimit.NegativePos * item.Setting.SCALE;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetLimit(IAxis axis)
        {
            try
            {
                var item = axis as AjinAxis;
                if (item == null) return;

                var enable = this.Enable;
                var stopMode = this.StopMode ? AXT_MOTION_STOPMODE.SLOWDOWN_STOP : AXT_MOTION_STOPMODE.EMERGENCY_STOP;
                var type = this.Type ? AXT_MOTION_SELECTION.ACTUAL : AXT_MOTION_SELECTION.COMMAND;
                var plus = this.Plus;
                var minus = this.Minus;

                var res = item.SetSoftwareLimit(enable, stopMode, type, plus, minus);
                if (res == true)
                {
                    AP.Device.Save();
                    return;
                }

                Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);

                AP.System.InterlockMsgEvent($"{(eAxis)axis.No} Limit Setting Fail.");
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this.Update(axis);
            }
        }
    }
}
