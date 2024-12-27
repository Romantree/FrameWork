using System;
using System.Threading.Tasks;
using TS.FW.Device.Ajin.Core;
using TS.FW.Device.Ajin.Dto.Axis;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin
{
    public class AjinAxis : IAxis
    {
        private Task _tkHome = null;

        public DeviceType DeviceType { get; set; } = DeviceType.N804;

        public bool Netowrk { get; set; } = false;

        public double Speed { get => this.Setting.InitMove.Speed * this.Setting.SCALE; set => this.Setting.InitMove.Speed = value / this.Setting.SCALE; }

        public double Accel { get => this.Setting.InitMove.Accel * this.Setting.SCALE; set => this.Setting.InitMove.Accel = value / this.Setting.SCALE; }

        public double Decel { get => this.Setting.InitMove.Decel * this.Setting.SCALE; set => this.Setting.InitMove.Decel = value / this.Setting.SCALE; }

        public int No { get; private set; }

        public bool IsServoOn { get => this.GetServoOn(); set => this.SetServoOn(value); }

        public bool IsAlarm => this.GetAlarm();

        public bool IsBusy => this.GetBusy();

        public bool HomeSensor => this.GetHomeSensor();

        public bool LimitPlus => this.GetLimitPlus();

        public bool LimitMinus => this.GetLimitMinus();

        public double ActPosition => this.GetActPosition() * this.Setting.SCALE;

        public double ComPosition => this.GetComPosition() * this.Setting.SCALE;

        public double LoadRatio => this.GetLoadRatio();

        public double ActSpeed => this.GetSpeed() * this.Setting.SCALE;

        public AxisSettingInfo Setting { get; private set; }

        public GantrySettingInfo Gantry => this.AxmGantryGetEnable();

        public AjinAxis(int no)
        {
            this.No = no;
            this.Setting = new AxisSettingInfo(no);
            this.Setting.LoadSetting();
        }

        public Response EStop()
        {
            return this.AxmMoveEStop();
        }

        private bool IsStop = false; 

        public Response HomeAsync(out HomeAsyncResult result)
        {
            result = new HomeAsyncResult() { Success = false };

            try
            {
                if (this._tkHome != null && this._tkHome.IsCompleted == false)
                {
                    result.Success = false;
                    result.Comment = "Home 진행 중 정지 후 다시 진행 해주세요.";
                    result.Complete = true;

                    this.IsStop = true;

                    return new Response(false, "Home 진행 중 정지 후 다시 진행 해주세요.");
                }

                this.IsStop = false;

                this._tkHome = Task.Factory.StartNew(this.Home_DoWork, result, TaskCreationOptions.None);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response MoveABS(double position)
        {
            return this.MoveABS(position, this.Speed, this.Accel, this.Decel);
        }

        public Response MoveABS(double position, double speed, double accel, double decel)
        {
            var mode = this.AxmMotGetAbsRelMode();
            if (mode == false || mode.Result != AXT_MOTION_ABSREL.POS_ABS_MODE)
            {
                var res = this.AxmMotSetAbsRelMode(AXT_MOTION_ABSREL.POS_ABS_MODE);
                if (res == false) return res;
            }

            return this.AxmMoveStartPos(position / this.Setting.SCALE, speed / this.Setting.SCALE, accel / this.Setting.SCALE, decel / this.Setting.SCALE);
        }

        public Response MoveREL(double position)
        {
            return this.MoveREL(position, this.Speed, this.Accel, this.Decel);
        }

        public Response MoveREL(double position, double speed, double accel, double decel)
        {
            var mode = this.AxmMotGetAbsRelMode();
            if (mode == false || mode.Result != AXT_MOTION_ABSREL.POS_REL_MODE)
            {
                var res = this.AxmMotSetAbsRelMode(AXT_MOTION_ABSREL.POS_REL_MODE);
                if (res == false) return res;
            }

            return this.AxmMoveStartPos(position / this.Setting.SCALE, speed / this.Setting.SCALE, accel / this.Setting.SCALE, decel / this.Setting.SCALE);
        }

        public Response MoveVEL(eDirection dir)
        {
            return this.MoveVEL(dir, this.Speed, this.Accel, this.Decel);
        }

        public Response MoveVEL(eDirection dir, double speed, double accel, double decel)
        {
            return this.AxmMoveVel(dir, speed / this.Setting.SCALE, accel / this.Setting.SCALE, decel / this.Setting.SCALE);
        }

        public Response SpeedChange(eDirection dir, double speed, double accel, double decel)
        {
            return this.AxmOverrideAccelVelDecel(dir, speed / this.Setting.SCALE, accel / this.Setting.SCALE, decel / this.Setting.SCALE);
        }

        public Response OverrideSetMaxVel(double speed)
        {
            return this.AxmOverrideSetMaxVel(speed);
        }

        public Response ResetAlarm()
        {
            if (this.DeviceType == DeviceType.A5N)
            {
                var res1 = this.AxmSignalServoAlarmReset(true);
                if (res1 == false) return res1;

                System.Threading.Thread.Sleep(500);

                var res2 = this.AxmSignalServoAlarmReset(false);
                if (res2 == false) return res2;
            }
            else
            {
                var res1 = this.AxmSignalWriteOutputBit(AXT_MOTION_UNIV_OUTPUT.UIO_OUT1, true);
                if (res1 == false) return res1;

                System.Threading.Thread.Sleep(500);

                var res2 = this.AxmSignalWriteOutputBit(AXT_MOTION_UNIV_OUTPUT.UIO_OUT1, false);
                if (res2 == false) return res2;
            }

            return new Response();
        }

        public Response ResetPosition()
        {
            //if (this.ServoOnLevel != AXT_MOTION_LEVEL_MODE.UNUSED)
            //{
            //    this.IsServoOn = false;
            //    System.Threading.Thread.Sleep(500);
            //}

            if (this.DeviceType == DeviceType.PM || this.DeviceType == DeviceType.A5N)
            {
                var res = this.AxmStatusSetPosMatch(0);
                if (res == false) return res;
            }
            else
            {
                var res1 = this.AxmStatusSetActPos(0);
                if (res1 == false) return res1;

                var res2 = this.AxmStatusSetCmdPos(0);
                if (res2 == false) return res2;
            }

            //if (this.ServoOnLevel != AXT_MOTION_LEVEL_MODE.UNUSED)
            //{
            //    this.IsServoOn = true;
            //}

            return new Response();
        }

        public Response Stop()
        {
            return this.AxmMoveSStop();
        }

        public Response GantryEnable(AjinAxis slave)
        {
            try
            {
                if (this.Gantry == true) return new Response();

                return this.AxmGantrySetEnable(slave);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response GantryDisable(AjinAxis slave)
        {
            try
            {
                if (this.Gantry == false) return new Response();

                return this.AxmGantrySetDisable(slave);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response SetSoftwareLimit()
        {
            try
            {
                this.Setting.AxmSignalGetSoftLimit();

                var limit = this.Setting.SoftwareLimit;

                return this.SetSoftwareLimit(limit.Enable, limit.StopMode, limit.Selection, limit.PositivePos * this.Setting.SCALE, limit.NegativePos * this.Setting.SCALE);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response SetSoftwareLimit(bool enable, AXT_MOTION_STOPMODE stopMode, AXT_MOTION_SELECTION selection, double positivePos, double negativePos)
        {
            var res = this.AxmSignalSetSoftLimit(enable, stopMode, selection, positivePos / this.Setting.SCALE, negativePos / this.Setting.SCALE); ;

            if(res == true) this.Setting.AxmSignalGetSoftLimit();

            return res;
        }

        private void Home_DoWork(object state)
        {
            var item = state as HomeAsyncResult;

            try
            {
                var res = this.AxmHomeSetStart();
                if (res == false) throw new Exception(res.Comment);

                System.Threading.Thread.Sleep(500);

                do
                {
                    if (item.IsStop)
                    {
                        throw new Exception(this.AxmHomeGetResultString());
                    }

                    if (this.IsStop == true) return;

                    System.Threading.Thread.Sleep(10);

                } while (this.AxmHomeGetResult() != AXT_MOTION_HOME_RESULT.HOME_SUCCESS);

                item.Success = true;
            }
            catch (Exception ex)
            {
                item.Success = false;
                item.Comment = ex.Message;
                Logger.Write(this, ex);
            }
            finally
            {
                item.Complete = true;
            }
        }
    }
}
