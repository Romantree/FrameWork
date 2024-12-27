using System;
using System.Linq;
using TS.FW.Robot.RC100.Data;
using TS.FW.Robot.RC100.State;

namespace TS.FW.Robot.RC100
{
    public partial class RobotClient
    {
        public Response Initial()
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.Alarm | RobotStateCheck.ProgramRun)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.INITIAL;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response Stop()
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.None)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.STOP;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response AlarmReseet()
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.None)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.RESET;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response ServoOnOff(bool isServoOn)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.Alarm)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = isServoOn ? RobotCmdType.SERVOON : RobotCmdType.SERVOOF;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response Pause()
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.None)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.PAUSE;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response Resum()
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.Alarm)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.RESUM;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response Home()
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.HOME;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response ArmFold()
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.ARMFOLD;

                    var cmd = this.Send(type);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response RobotGetReady(RobotActionData data)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.FGREADY;

                    var cmd = this.Send(type, data.ToData());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response RobotGet(RobotActionData data)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.GETFROM;

                    var cmd = this.Send(type, data.ToData());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response RobotPutReady(RobotActionData data)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.FPREADY;

                    var cmd = this.Send(type, data.ToData());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response RobotPut(RobotActionData data)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.PUTINTO;

                    var cmd = this.Send(type, data.ToData());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response RobotGetTeach(RobotActionData data)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.GETTPOS;

                    var cmd = this.Send(type, data.ToData());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response RobotGetFlod(RobotActionData data)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.PUTAFLD;

                    var cmd = this.Send(type, data.ToData());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response VacuumOnOff(RobotArm arm, bool isVacuumOn)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = isVacuumOn ? RobotCmdType.VACUMON : RobotCmdType.VACUMOF;

                    var cmd = this.Send(type, (int)arm);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response MoveABS(int axisNo, double position, int speed)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.MOVEABS;

                    var cmd = this.Send(type, axisNo, position, speed);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response MoveREL(int axisNo, double position, int speed)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.MOVEREL;

                    var cmd = this.Send(type, axisNo, position, speed);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<RobotPosition> GetTeachPos(RobotTeachData teach)
        {
            try
            {
                if (this.Connected == false) return new Response<RobotPosition>(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response<RobotPosition>(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.GETLPOS;

                    var cmd = this.Send(type, teach.ToData());
                    if (cmd == null) return new Response<RobotPosition>(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response<RobotPosition>(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));

                    var data = new RobotPosition();
                    data.SetData(cmd.DataMsg);

                    return new Response<RobotPosition>(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<RobotTeachParam> GetTeachParm(RobotTeachData teach)
        {
            try
            {
                if (this.Connected == false) return new Response<RobotTeachParam>(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response<RobotTeachParam>(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.GETLPAR;

                    var cmd = this.Send(type, teach.ToData());
                    if (cmd == null) return new Response<RobotTeachParam>(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response<RobotTeachParam>(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));

                    var data = new RobotTeachParam();
                    data.SetData(cmd.DataMsg);

                    return new Response<RobotTeachParam>(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<int> GetSpeed(RobotSpeedMode mode)
        {
            try
            {
                if (this.Connected == false) return new Response<int>(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response<int>(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.GETLPAR;

                    var cmd = this.Send(type, (int)mode);
                    if (cmd == null) return new Response<int>(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response<int>(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));

                    if (int.TryParse(cmd.DataMsg, out int value) == false) return new Response<int>(false, string.Format("분석 할 수 없는 유형의 데이터 입니다. {0}", value));

                    return new Response<int>(value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetTeachPos(RobotTeachData teach, RobotPosition pos)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.SETCORD;

                    var cmd = this.Send(type, teach.ToData().Concat(pos.ToData()).ToArray());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetTeachParm(RobotTeachData teach, RobotTeachParam pos)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.SETCORD;

                    var cmd = this.Send(type, teach.ToData().Concat(pos.ToData()).ToArray());
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetSpeed(RobotSpeedMode mode, int speed)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.SETTSPD;

                    var cmd = this.Send(type, (int)mode, speed);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response MappingStart(int stageNo, WaferSize point, ref MappingResult result)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    this._mappingResult = result;
                    this._mappingResult.Complete = false;

                    var type = RobotCmdType.MAPSCAN;

                    var cmd = this.Send(type, stageNo, (int)point);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                this._mappingResult = null;

                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response GetWaferThickness(int stageNo, RobotPoint point, ref WaferThicknessResult result)
        {
            try
            {
                if (this.Connected == false) return new Response(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.UserRobotMoveCmd)) return new Response(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    this._waferThicknessResult = result;
                    this._waferThicknessResult.Complete = false;

                    var type = RobotCmdType.GETMDAT;

                    var cmd = this.Send(type, stageNo, (int)point);
                    if (cmd == null) return new Response(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<RobotPosition> GetMappingTeachPos(int stageNo, RobotPoint point)
        {
            try
            {
                if (this.Connected == false) return new Response<RobotPosition>(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response<RobotPosition>(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.GETLPAR;

                    var cmd = this.Send(type, stageNo, (int)point);
                    if (cmd == null) return new Response<RobotPosition>(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response<RobotPosition>(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));

                    var data = new RobotPosition();
                    data.SetData(cmd.DataMsg);

                    return new Response<RobotPosition>(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<MappingTeachParam> GetMappingTeachParm(int stageNo, RobotPoint point)
        {
            try
            {
                if (this.Connected == false) return new Response<MappingTeachParam>(false, "통신 연결 상태가 아닙니다.");
                if (this.State.StateCheck(RobotStateCheck.CmdReady)) return new Response<MappingTeachParam>(false, "명령 수신 Ready 상태가 아닙니다.");

                lock (this._locker)
                {
                    var type = RobotCmdType.GETLPAR;

                    var cmd = this.Send(type, stageNo, (int)point);
                    if (cmd == null) return new Response<MappingTeachParam>(false, string.Format("등록 또는 종료 되지 않은 명령입니다. {0}", type));

                    if (cmd.Error != RobotError.NONE) return new Response<MappingTeachParam>(false, string.Format("통신 에러코드가 발생하였습니다. {0} : {1}", cmd.Cmd, cmd.Error));

                    var data = new MappingTeachParam();
                    data.SetData(cmd.DataMsg);

                    return new Response<MappingTeachParam>(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }
    }
}
