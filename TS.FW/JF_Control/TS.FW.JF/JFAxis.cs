using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.FW.JF.Core;
using TS.FW.JF.DTO;

namespace TS.FW.JF
{
    public class JFAxis
    {
        internal double PositionScale = 0.001;
        //internal double SpeedScale = 0.001;

        public static event EventHandler OnResetPosition;

        public int No { get; private set; }

        public void SetNo(int no)
        {
            this.No = no;
            this.Setting.No = no;
        }

        public bool IsEnable { get; set; }
        public bool IsPosLimit => this.GetPosLimit();
        public bool IsNegLimit => this.GetNegLimit();
        public bool IsHome => this.GetHome();
        public bool IsInPos => this.MoveDone();
        public bool IsServoOn { get => this.GetAmpEnable(); set => this.SetAmpEnable(value); }
        public bool IsBusy => (this.MotionDone() == false);
        public bool IsAlarm => this.GetFault();
        public bool IsHomeEnd { get; set; }
        public bool IsLatchOn => this.GetLatchState();
        public double Enc { get { return this.GetEncoderEx(); } }

        //public double TableREnc => (this.GetEncoder() / PositionScale) * this.GearRatio;

        private double GetEncoderEx()
        {
            if (this.Setting.Name == "TABLE_R") //todo : 회전축인지 비교 하는 데이터 필요 네임으로 픽스 할수는 없음. 
            {
                var enc = this.GetEncoder() / PositionScale;

                return Math.Abs(enc * this.GearRatio) % 360.0D;
            }
            else
            {
                return this.GetEncoder() / PositionScale;
            }
        }

        public double LatchPos
        {
            get { return this.GetPosLatch() / PositionScale; }
        }

        public double EncOrg => this.GetEncoder();

        public double Cmd => this.GetCommand() * PositionScale;
        public int Rpm => this.GetCommandRPM();
        /// <summary>
        /// 모터 분해능
        /// </summary>
        public int Rstn { get => this.GetAmpResolution(); set => this.SetAmpResolution(value); }
        /// <summary>
        /// 기어비 / 감속비
        /// </summary>
        public double GearRatio { get => this.GetElectricGear(); set => this.SetElectricGear(value); }

        public enDir Dir { get => this.GetEncoderDir(); set => this.SetEncoderDir(value); }
        public double Speed { get => this.Setting.InitMove.Speed; set => this.Setting.InitMove.Speed = value; }
        public int Accel { get => this.Setting.InitMove.Accel; set => this.Setting.InitMove.Accel = value; }
        public int Decel { get => this.Setting.InitMove.Decel; set => this.Setting.InitMove.Decel = value; }
        public double JogSpeed { get => this.Setting.InitMove.JogSpeed; set => this.Setting.InitMove.JogSpeed = value; }
        public int JogAccel { get => this.Setting.InitMove.JogAccel; set => this.Setting.InitMove.JogAccel = value; }

        public int MpgMode { get => this.GetMPGMode(); set => this.SetMPGMode(value); }
        public int MpgDirect { get => this.GetMPGDir(); set => this.SetMPGDir(value); }
        public int MpgRatio { get => this.GetMPGRatio(); set => this.SetMPGRatio(value); }
        //public short MpgFilter { get => this.GetMPGFilter(); set => this.SetMPGFilter(value); }

        public bool MpgFilter { get => this.GetMPGFilter(); set => this.SetMPGFilter(value); }

        public double MpgCounter { get => this.GetMPGCount(); set => this.SetMPGCount(value); }

        public SettingInfo Setting { get; set; }

        /// <summary>
        /// 분해능
        /// </summary>
        //public int MotorResoution { get; set; }
        //public double MotorReduction { get; set; } //Rtns가 감속비 임. 
        public double MotorBallPitch { get; set; }

        public JFAxis(int no) //, bool enable = true
        {
            this.No = no;
            this.IsHomeEnd = false;
            this.Setting = new SettingInfo(no);
            this.Setting.LoadSetting();

            //IsEnable = enable;
            //var setcal = (iRes / dPtch) / dRdct;
        }

        //Load Motor Parameter
        public bool LoadMotionFile()
        {
            var result = this.GetParameter();
            return result.IsOK();
        }
        //Save Motor Parameter
        public bool SaveMotionFile()
        {
            var result = this.SetParameter();
            Thread.Sleep(100);
            return result.IsOK();
        }
        // Motor 감속 정지
        public bool Stop()
        {
            var result = this.DecStop();
            return result.IsOK();
        }
        // Motor 긴급 정지
        public bool EStop()
        {
            var result = this.EmgStop();
            return result.IsOK();
        }
        // Error 인터럽트 레지스터 클리어
        public void Reset()
        {
            this.ResetClear();
        }
        // Motor Reset Alarm
        public bool ResetAlarm()
        {
            var result1 = this.SetAmpReset(true);

            System.Threading.Thread.Sleep(500);

            var result2 = this.SetAmpReset(false);

            System.Threading.Thread.Sleep(500);

            if (result2.IsError()) return false;

            return true;
        }

        // Position Initialize
        public bool ResetPosition()
        {
            //Logger.DebugWrite(this, $"Encoder Value :{this.Enc:f3}");
            var result1 = this.SetCommand(0);
            if (result1.IsError()) return false;

            //Thread.Sleep(300);

            var result2 = this.SetEncoder(0);
            if (result2.IsError()) return false;

            OnResetPosition?.Invoke(this, new EventArgs());

            return true;
        }

        // Set Gear Ratio
        public bool CalGearRatio(int iRes, double dPtch, double dRdct)
        {
            //기어비 = (Motor Resolution / 기구물 1회전 이동 Pitch) / 감속비
            var setcal = (iRes / dPtch) / dRdct;
            this.SetElectricGear(setcal);

            System.Threading.Thread.Sleep(100); //
            var getCal = this.GetElectricGear();

            return (setcal == getCal);
        }

        private Task _homeTask = null;

        // Motor Home
        public Response HomeAsync(out HomeAsyncResult result)
        {
            result = new HomeAsyncResult();

            try
            {
                if (this._homeTask != null)
                {
                    if (this._homeTask.IsCompleted == false)
                    {
                        result.Comment = "Home Fail.";
                        result.Success = false;
                        result.Complete = true;

                        return new Response(false, result.Comment);
                    }
                }

                this._homeTask = Task.Factory.StartNew(this.Home_DoWork, result, TaskCreationOptions.None);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private void Home_DoWork(object state)
        {
            var item = state as HomeAsyncResult;
            this.IsHomeEnd = false;
            try
            {
                var Home = this.Setting.Home;


                var StartVel = this.ToValue(Home.StartVel);
                var MaxVel = this.ToValue(Home.MaxVel);
                var HomeVel = this.ToValue(Home.HomeVel);
                
                this.Stop();
                Thread.Sleep(500);

                if (this.IsHome != false)
                {
                    this.MoveVEL(
                        Home.Dir, // != 1 ? enDir.CCW : enDir.CW
                        Home.StartVel,
                        Home.Acc);

                    while (this.IsHome != false)
                    {
                        if (this.IsBusy == false || this.IsInPos == true)
                        {
                            item.Success = false;
                            item.Comment = "Motion Stop Signal Detect";

                            item.Complete = true;
                            return;
                        }

                        Thread.Sleep(1);
                    }

                    this.EStop();
                    Thread.Sleep(500);
                }

                //var mode = IsHome ? 13 : 1;// 13; //5, 11, 9, 10
                var mode = 1;
                if (IsHome) // Home Serach Speed Change
                {
                    StartVel = HomeVel / 10;
                    HomeVel = HomeVel / 20;
                }

                var res = this.MoveHome((int)Home.Dir, StartVel, MaxVel, HomeVel, Home.Acc, mode);// Home.Mode);
                //var res = this.MoveHome(Home.Dir, Home.StartVel, Home.MaxVel, Home.HomeVel, Home.Acc, Home.Mode);
                if (res.IsError()) throw new Exception("Motion Home Fail");

                Thread.Sleep(500);

                do
                {
                    if (item.IsStop)
                    {
                        throw new Exception("Motion Home TimeOut Stop");
                    }

                    System.Threading.Thread.Sleep(10);

                } while (this.IsInPos != true); //JF Control은 실시간으로 Result를 받지 못하므로.. IsInPos = motion_done && in_position 

                if (this.IsHome == false)
                {
                    item.Success = false;
                    item.Comment = "Home Signal no detect";

                    return;
                }

                this.IsHomeEnd = true;
                item.Success = true;

                Thread.Sleep(500);
                ResetPosition();
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

        #region Move Function.
        // Motor 절대위치 이동
        public bool MoveABS(double Pos, bool SCurve = false)
        {
            if (IsAlarm) return false;

            return this.MoveABS(Pos, this.Speed, this.Accel, this.Decel, SCurve);
        }

        public bool MoveABS(double pos, double Speed, int Acc, int Dec, bool SCurve = false)
        {
            if (IsAlarm) return false;

            MMC_STAT result;

            var position = this.ToValue(pos);
            var speed = this.ToValue(Speed);


            if (SCurve)
            {
                result = this.Start_sa_iMove(position, 0, speed, Acc, Dec);
            }
            else
            {
                result = this.Start_ta_iMove(position, 0, speed, Acc, Dec);
            }
            return result.IsOK();
        }

        // Motor 상대위치 이동
        public bool MoveREL(double Pos, bool SCurve = false)
        {
            if (IsAlarm) return false;

            return this.MoveREL(Pos, this.Speed, this.Accel, this.Decel, SCurve);
        }

        public bool MoveREL(double pos, double Speed, int Acc, int Dec, bool SCurve = false)
        {
            if (IsAlarm) return false;

            MMC_STAT result;

            var position = this.ToValue(pos);
            var speed = this.ToValue(Speed);

            if (SCurve)
            {
                result = this.Start_sr_iMove(position, 0, speed, Acc, Dec);
            }
            else
            {
                result = this.Start_tr_iMove(position, 0, speed, Acc, Dec);
            }
            return result.IsOK();
        }

        // Motor Jog 이동 
        public void MoveVEL(enDir Dir)
        {
            if (IsAlarm) return;

            this.MoveVEL(Dir, this.JogSpeed, this.JogAccel);
        }

        public void MoveVEL(enDir Dir, double Speed, int Acc)
        {
            if (IsAlarm) return;

            //참고사항//tv_move, sv_move 인 경우만 maxvel 의 부호가 회전 방향을 나타낸다.
            //var DirSpeed = (double)Dir * Speed;

            var speed = this.ToValue(Speed);
            speed = (double)Dir * speed;

            var acc = Acc;// (int)(((Acc / MotorBallPitch) * Rstn) / GearRatio);
            this.Move_tv(0, speed, acc);
        }
        #endregion

        #region Setting Limit Action.
        // Set Software Positive Limit Action 
        public bool SetSWPosLimitAct()
        {
            try
            {
                var Swlimit = this.Setting.SoftwareLimit;

                return this.SetSWPosLimitAct(Swlimit.PosLimitPos, Swlimit.PosLimitAct);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SetSWPosLimitAct(double PosLimitPos, enStopAct Act)
        {
            var pos = this.ToValue(PosLimitPos);

            this.Setting.SoftwareLimit.PosLimitPos = PosLimitPos;
            this.Setting.SoftwareLimit.PosLimitAct = Act;

            var result = this.SetPosLimit(pos, Act);

            return result.IsOK();
        }

        // Set Software Positive Limit Action
        public bool SetSWNegLimitAct()
        {
            try
            {
                var Swlimit = this.Setting.SoftwareLimit;

                return this.SetSWNegLimitAct(Swlimit.NegLimitPos, Swlimit.NegLimitAct);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetSWNegLimitAct(double NegLimitPos, enStopAct Act)
        {
            var pos = this.ToValue(NegLimitPos);

            this.Setting.SoftwareLimit.NegLimitPos = NegLimitPos;
            this.Setting.SoftwareLimit.NegLimitAct = Act;

            var result = this.SetNegLimit(pos, Act);

            return result.IsOK();
        }

        // Set Hardware Postive / Negtive Limit Action
        public bool SetHWLimitAct()
        {
            try
            {
                var limit = this.Setting.Limit;
                return this.SetHWLimitAct(limit.HwLimitAct);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetHWLimitAct(enStopAct Act)
        {
            this.Setting.Limit.HwLimitAct = Act;
            var result = this.SetLimitAct(Act);
            return result.IsOK();
        }

        // Set Alarm Action
        public bool SetAlarmLimitAct()
        {
            try
            {
                var limit = this.Setting.Limit;
                return this.SetAlarmLimitAct(limit.AlarmAct);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetAlarmLimitAct(enStopAct Act)
        {
            this.Setting.Limit.AlarmAct = Act;
            var result = this.SetAlarmLimit(Act);
            return result.IsOK();
        }

        #endregion

        #region Setting Limit 

        // Set Pos / Neg Limit Level
        public bool SetLimitLevel()
        {
            try
            {
                var limit = this.Setting.Limit;
                return this.SetLimitLevel(limit.LimitLevel);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetLimitLevel(enLevel Act)
        {
            this.Setting.Limit.LimitLevel = Act;
            var result = this.SetLimit_Level(Act);
            return result.IsOK();
        }

        // Set Home Level
        public bool SetHomeLevel()
        {
            try
            {
                var limit = this.Setting.Limit;
                return this.SetHomeLevel(limit.HomeLevel);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetHomeLevel(enLevel Act)
        {
            this.Setting.Limit.HomeLevel = Act;
            var result = this.SetHome_Level(Act);
            return result.IsOK();
        }

        // Set Latch Level
        public bool SetLatchLevel()
        {
            try
            {
                var limit = this.Setting.Limit;
                return this.SetLatchLevel(limit.LatchLevel);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetLatchLevel(enLevel Act)
        {
            this.Setting.Limit.LatchLevel = Act;
            var result = this.SetLatch_Level(Act);
            return result.IsOK();
        }

        //public bool GetLatchLevel()
        //{
        //    var result = this.GetLatch_Level();
        //    return result;
        //}

        // Set In Position Level

        public bool SetInPosLevel()
        {
            try
            {
                var limit = this.Setting.Limit;
                return this.SetInPosLevel(limit.InPosLevel);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetInPosLevel(enLevel Act)
        {
            this.Setting.Limit.InPosLevel = Act;
            var result = this.SetInPos_Level(Act);
            return result.IsOK();
        }

        // Set Amp Fault Level
        public bool SetAmpFaultLevel()
        {
            try
            {
                var limit = this.Setting.Limit;
                return this.SetAmpFaultLevel(limit.AmpLevel);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetAmpFaultLevel(enLevel Act)
        {
            this.Setting.Limit.AmpLevel = Act;
            var result = this.SetAmpFault_Level(Act);
            return result.IsOK();
        }

        public bool SetMPGSpeed(double speed)
        {
            var sp = this.ToValue(speed);

            this.vMoveMPG(sp);
            Thread.Sleep(100);
            this.pMoveMPG(this.MpgRatio, sp);
            return true;
        }


        /// <summary>
        /// 이동 포지션, 속도 Scale 변경 시 사용(기본 : 0.001)
        /// </summary>
        /// <param name="resoution">분해능</param>
        /// <param name="reduction">감속비</param>
        /// <param name="pitch">모터 볼 피치</param>
        public void SetScaleParamter(int resoution, double reduction, double pitch)
        {
            Rstn = resoution;
            GearRatio = reduction;
            MotorBallPitch = pitch;

            PositionScale = this.ToScale();
        }

        private double ToValue(double value)
        {
            if (this.Setting.Name == "TABLE_R")
            {
                return (value * this.ToScale()) / this.GearRatio;
            }
            else
            {
                return value * this.ToScale();
            }
        }

        private double ToScale()
        {
            return (Rstn) / MotorBallPitch;
        }

        #endregion

        public void SetEncMode(int mode)
        {
            JFAxisCore.SetEncorderMode(this.No, mode);
        }
    }
}
