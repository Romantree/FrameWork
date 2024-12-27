using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin.Dto;
using TS.FW.Device.Ajin.Dto.Axis;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin.Core
{
    internal static class AxisHelper
    {
        public static Response AxmMotSaveParaAll(this AjinAxis device, string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath)) return new Response(false, "FilePath 'Null' Or Empty.");

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotSaveParaAll(Path.GetFullPath(filePath));
                if (res.ErrorCheck() == false) return new Response(false, "AxmMotSaveParaAll Error [{0}]", filePath);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static bool GetServoOn(this AjinAxis axis)
        {
            try
            {
                uint isOnOff = 0;
                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalIsServoOn(axis.No, ref isOnOff);
                if (res.ErrorCheck() == false) return false;

                return isOnOff == 1;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return false;
            }
        }

        public static void SetServoOn(this AjinAxis axis, bool isOnOff)
        {
            try
            {
                if (axis.IsServoOn == isOnOff) return;

                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalServoOn(axis.No, (uint)(isOnOff ? 1 : 0));
                res.ErrorCheck();
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
            }
        }

        public static bool GetAlarm(this AjinAxis axis)
        {
            try
            {
                if(axis.Netowrk)
                {
                    uint isAlarm = 0;
                    var res = (AXT_FUNC_RESULT)CAXM.AxmStatusReadServoAlarm(axis.No, 0, ref isAlarm);
                    if (res.ErrorCheck() == false) return true;

                    return isAlarm != 0;
                }
                else
                {
                    if (axis.Setting.ServoAlarmResetLevel == AXT_MOTION_LEVEL_MODE.UNUSED) return false;

                    uint isAlarm = 0;
                    var res = (AXT_FUNC_RESULT)CAXM.AxmSignalReadServoAlarm(axis.No, ref isAlarm);
                    if (res.ErrorCheck() == false) return true;

                    return isAlarm != 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return true;
            }
        }

        public static bool GetBusy(this AjinAxis axis)
        {
            try
            {
                uint isBusy = 0;
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusReadInMotion(axis.No, ref isBusy);
                if (res.ErrorCheck() == false) return true;

                return isBusy != 0;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return false;
            }
        }

        public static bool GetHomeSensor(this AjinAxis axis)
        {
            try
            {
                uint isHome = 0;
                var res = (AXT_FUNC_RESULT)CAXM.AxmHomeReadSignal(axis.No, ref isHome);
                if (res.ErrorCheck() == false) return true;

                return isHome != 0;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return false;
            }
        }

        public static bool GetLimitPlus(this AjinAxis axis)
        {
            try
            {
                uint isPlus = 0;
                uint isMinus = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalReadLimit(axis.No, ref isPlus, ref isMinus);
                if (res.ErrorCheck() == false) return true;

                return isPlus != 0;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return false;
            }
        }

        public static bool GetLimitMinus(this AjinAxis axis)
        {
            try
            {
                uint isPlus = 0;
                uint isMinus = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalReadLimit(axis.No, ref isPlus, ref isMinus);
                if (res.ErrorCheck() == false) return true;

                return isMinus != 0;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return false;
            }
        }

        public static double GetActPosition(this AjinAxis axis)
        {
            try
            {
                var position = 0.0D;
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusGetActPos(axis.No, ref position);
                if (res.ErrorCheck() == false) return 0;

                return position;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return 0;
            }
        }

        public static double GetComPosition(this AjinAxis axis)
        {
            try
            {
                var position = 0.0D;
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusGetCmdPos(axis.No, ref position);
                if (res.ErrorCheck() == false) return 0;

                return position;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return 0;
            }
        }

        public static double GetLoadRatio(this AjinAxis axis)
        {
            try
            {
                var value = 0.0D;
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusReadServoLoadRatio(axis.No, ref value);
                if (res.ErrorCheck() == false) return 0;

                return value;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return 0;
            }
        }

        public static double GetSpeed(this AjinAxis axis)
        {
            try
            {
                var value = 0.0D;
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusReadActVel(axis.No, ref value);
                if (res.ErrorCheck() == false) return 0;

                return value;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return 0;
            }
        }

        public static Response AxmMoveEStop(this AjinAxis axis)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmMoveEStop(axis.No);
                if (res.ErrorCheck() == false) return new Response(false, "EStop Error [{0}]", axis.No);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMoveSStop(this AjinAxis axis)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmMoveSStop(axis.No);
                if (res.ErrorCheck() == false) return new Response(false, "SStop Error [{0}]", axis.No);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }

        }

        public static Response AxmSignalServoAlarmReset(this AjinAxis axis, bool isOnOff)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalServoAlarmReset(axis.No, (uint)(isOnOff ? 1 : 0));
                if (res.ErrorCheck() == false) return new Response(false, "AxmSignalServoAlarmReset Error [{0} = {1}]", axis.No, isOnOff);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmSignalWriteOutputBit(this AjinAxis axis, AXT_MOTION_UNIV_OUTPUT bit, bool isOnOff)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalWriteOutputBit(axis.No, (int)bit, (uint)(isOnOff ? 1 : 0));
                if (res.ErrorCheck() == false) return new Response(false, "AxmSignalWriteOutputBit Error [{0} - {1} = {2}]", axis.No, bit, isOnOff);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmStatusSetPosMatch(this AjinAxis axis, double position)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusSetPosMatch(axis.No, position);
                if (res.ErrorCheck() == false) return new Response(false, "AxmStatusSetPosMatch Error [{0} = {1}]", axis.No, position);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmStatusSetActPos(this AjinAxis axis, double position)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusSetActPos(axis.No, position);
                if (res.ErrorCheck() == false) return new Response(false, "AxmStatusSetActPos Error [{0} = {1}]", axis.No, position);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmStatusSetCmdPos(this AjinAxis axis, double position)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmStatusSetCmdPos(axis.No, position);
                if (res.ErrorCheck() == false) return new Response(false, "AxmStatusSetCmdPos Error [{0} = {1}]", axis.No, position);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<AXT_MOTION_ABSREL> AxmMotGetAbsRelMode(this AjinAxis axis)
        {
            try
            {
                uint mode = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotGetAbsRelMode(axis.No, ref mode);
                if (res.ErrorCheck() == false) return new Response<AXT_MOTION_ABSREL>(false, "AxmMotGetAbsRelMode Error [{0}]", axis.No);

                return new Response<AXT_MOTION_ABSREL>((AXT_MOTION_ABSREL)mode);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMotSetAbsRelMode(this AjinAxis axis, AXT_MOTION_ABSREL mode)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmMotSetAbsRelMode(axis.No, (uint)mode);
                if (res.ErrorCheck() == false) return new Response(false, "AxmMotSetAbsRelMode Error [{0} = {1}]", axis.No, mode);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMoveStartPos(this AjinAxis axis, double position, double speed, double accel, double decel)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmMoveStartPos(axis.No, position, speed, accel, decel);
                if (res.ErrorCheck() == false) return new Response(false, "AxmMoveStartPos Error [{0} = {1}]", axis.No, position);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMoveVel(this AjinAxis axis, eDirection direction, double speed, double accel, double decel)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmMoveVel(axis.No, direction == eDirection.Minus ? speed * -1.0D : speed, accel, decel);
                if (res.ErrorCheck() == false) return new Response(false, "AxmMoveStartPos Error [{0} = {1}]", axis.No, direction);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmOverrideAccelVelDecel(this AjinAxis axis, eDirection direction, double speed, double accel, double decel)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmOverrideAccelVelDecel(axis.No, direction == eDirection.Minus ? speed * -1.0D : speed, accel, decel);
                if (res.ErrorCheck() == false) return new Response(false, "AxmOverrideAccelVelDecel Error [{0} = {1}]", axis.No, direction);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmOverrideSetMaxVel(this AjinAxis axis, double speed)
        {
            try
            {
                //AxmOverrideSetMaxVel
                var res1 = (AXT_FUNC_RESULT)CAXM.AxmOverrideSetMaxVel(axis.No, speed);
                if (res1.ErrorCheck() == false) return new Response(false, "AxmOverrideSetMaxVel Error [{0} = {1}]", axis.No, speed);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmHomeSetStart(this AjinAxis axis)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmHomeSetStart(axis.No);
                if (res.ErrorCheck() == false) return new Response(false, "AxmHomeSetStart Error [{0}]", axis.No);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static AXT_MOTION_HOME_RESULT AxmHomeGetResult(this AjinAxis axis)
        {
            try
            {
                uint result = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmHomeGetResult(axis.No, ref result);
                if (res.ErrorCheck() == false) return AXT_MOTION_HOME_RESULT.HOME_RESERVED;

                return (AXT_MOTION_HOME_RESULT)result;
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex.Message);
                return AXT_MOTION_HOME_RESULT.HOME_RESERVED;
            }
        }

        public static string AxmHomeGetResultString(this AjinAxis axis)
        {
            var result = axis.AxmHomeGetResult();

            switch (result)
            {
                case AXT_MOTION_HOME_RESULT.HOME_SUCCESS: return "원점 검색 성공";
                case AXT_MOTION_HOME_RESULT.HOME_SEARCHING: return "현재 원정 진행 중";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_GNT_RANGE: return "겐트리 구동축의 Master축과 Slave축의 원점검색 결과가 설정한 Offset 범위를 벗어남";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_USER_BREAK: return "원점 검색 중 사용자가 정지";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_VELOCITY: return "원점 검색 속도 설정 값 중 0보다 작거나 같은 값이 존재";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_AMP_FAULT: return "원점 검색 중 Servo 알람 발생";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_NEG_LIMIT: return "+방향 원점 검색 중 -방향 Limit센서가 감지됨.";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_POS_LIMIT: return "-방향 원점 검색 중 +방향 Limit센서가 감지됨.";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_NOT_DETECT: return "원점 센서가 감지되지 않음";
                case AXT_MOTION_HOME_RESULT.HOME_ERR_UNKNOWN: return "알 수 없는 축 원점검색 실행.";

                default: return string.Format("Home Result Fail [Code = {0}]", result);
            }
        }

        public static GantrySettingInfo AxmGantryGetEnable(this AjinAxis axis)
        {
            try
            {
                uint homeUse = 0;
                double offSet = 0;
                double range = 0;
                uint isOnOff = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmGantryGetEnable(axis.No, ref homeUse, ref offSet, ref range, ref isOnOff);
                res.ErrorCheck();

                return new GantrySettingInfo()
                {
                    Enable = isOnOff != 0,
                    HomeUse = homeUse != 0,
                    OffSet = offSet,
                    Range = range,
                };
            }
            catch (Exception ex)
            {
                Logger.Write(axis, ex);
                return null;
            }
        }

        public static Response AxmGantrySetEnable(this AjinAxis axis, AjinAxis slaveAxis)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmGantrySetEnable(axis.No, slaveAxis.No, 0, 0, 100);
                if (res.ErrorCheck() == false) return new Response(false, "AxmGantrySetEnable Error [{0} = {1}]", axis.No, slaveAxis.No);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmGantrySetDisable(this AjinAxis axis, AjinAxis slaveAxis)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmGantrySetDisable(axis.No, slaveAxis.No);
                if (res.ErrorCheck() == false) return new Response(false, "AxmGantrySetDisable Error [{0} = {1}]", axis.No, slaveAxis.No);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmSignalSetSoftLimit(this AjinAxis axis, bool enable, AXT_MOTION_STOPMODE stopMode, AXT_MOTION_SELECTION selection, double positivePos, double negativePos)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalSetSoftLimit(axis.No, Convert.ToUInt32(enable), (uint)stopMode, (uint)selection, positivePos, negativePos);
                if (res.ErrorCheck() == false) return new Response(false, "AxmSignalSetSoftLimit Error [{0}]", axis.No);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static ResponseList<AXT_MOTION_ABSREL> AxmMotGetAbsRelMode(this AjinDevice device, int[] axis)
        {
            try
            {
                var list = new List<AXT_MOTION_ABSREL>();

                foreach (var no in axis)
                {
                    uint mode = 0;

                    var res = (AXT_FUNC_RESULT)CAXM.AxmMotGetAbsRelMode(no, ref mode);
                    if (res.ErrorCheck() == false) return new ResponseList<AXT_MOTION_ABSREL>(false, "AxmMotGetAbsRelMode Error [{0}]", no);

                    list.Add((AXT_MOTION_ABSREL)mode);
                }

                return new ResponseList<AXT_MOTION_ABSREL>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMotSetAbsRelMode(this AjinDevice device, int[] axis, AXT_MOTION_ABSREL mode)
        {
            try
            {
                foreach (var no in axis)
                {
                    var res = (AXT_FUNC_RESULT)CAXM.AxmMotSetAbsRelMode(no, (uint)mode);
                    if (res.ErrorCheck() == false) return new Response(false, "AxmMotSetAbsRelMode Error [{0} = {1}]", no, mode);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMoveStartMultiPos(this AjinDevice device, int[] axis)
        {
            try
            {
                device.GetMultiData(axis, out double[] pos, out double[] speed, out double[] accel, out double[] decel);

                var res = (AXT_FUNC_RESULT)CAXM.AxmMoveStartMultiPos(axis.Length, axis, pos, speed, accel, decel);
                if (res.ErrorCheck() == false) return new Response(false, "AxmMoveStartMultiPos");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMoveStartMultiVel(this AjinDevice device, int[] axis)
        {
            try
            {
                device.GetMultiDataVEL(axis, out double[] speed, out double[] accel, out double[] decel);

                var res = (AXT_FUNC_RESULT)CAXM.AxmMoveStartMultiVel(axis.Length, axis, speed, accel, decel);
                if (res.ErrorCheck() == false) return new Response(false, "AxmMoveStartMultiVel");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        #region Setting

        public static void LoadSetting(this AxisSettingInfo info)
        {
            try
            {
                info.AxmSignalGetServoAlarmResetLevel();

                info.AxmSignalGetLimit();

                info.AxmMotGetAbsRelMode();
                info.AxmMotGetProfileMode();
                info.AxmSignalGetEncoderType();

                info.AxmMotGetParaLoad();

                info.AxmHomeGetSignalLevel();
                info.AxmHomeGetMethod();
                info.AxmHomeGetVel();

                info.AxmSignalGetSoftLimit();

                info.AxmMotGetPulseOutMethod();
                info.AxmMotGetEncInputMethod();
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmSignalGetServoAlarmResetLevel(this AxisSettingInfo info)
        {
            try
            {
                uint level = 0;
                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalGetServoAlarmResetLevel(info.No, ref level);
                if (res.ErrorCheck() == false)
                {
                    info.ServoAlarmResetLevel = AXT_MOTION_LEVEL_MODE.UNUSED;
                }
                else
                {
                    info.ServoAlarmResetLevel = (AXT_MOTION_LEVEL_MODE)level;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmSignalGetLimit(this AxisSettingInfo info)
        {
            try
            {
                uint eStopMode = 0;
                uint positiveLevel = 0;
                uint negtiveLevel = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalGetLimit(info.No, ref eStopMode, ref positiveLevel, ref negtiveLevel);
                if (res.ErrorCheck() == false)
                {
                    info.Limit.EStopMode = AXT_MOTION_STOPMODE.EMERGENCY_STOP;
                    info.Limit.PositiveLevel = AXT_MOTION_LEVEL_MODE.UNUSED;
                    info.Limit.NegtiveLevel = AXT_MOTION_LEVEL_MODE.UNUSED;
                }
                else
                {
                    info.Limit.EStopMode = (AXT_MOTION_STOPMODE)eStopMode;
                    info.Limit.PositiveLevel = (AXT_MOTION_LEVEL_MODE)positiveLevel;
                    info.Limit.NegtiveLevel = (AXT_MOTION_LEVEL_MODE)negtiveLevel;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmSignalGetSoftLimit(this AxisSettingInfo info)
        {
            try
            {
                uint enable = 0;
                uint stopMode = 0;
                uint selection = 0;
                double positivePos = 0;
                double negativePos = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmSignalGetSoftLimit(info.No, ref enable, ref stopMode, ref selection, ref positivePos, ref negativePos);
                if (res.ErrorCheck() == false)
                {
                    info.SoftwareLimit.Enable = false;
                    info.SoftwareLimit.StopMode = AXT_MOTION_STOPMODE.EMERGENCY_STOP;
                    info.SoftwareLimit.Selection = AXT_MOTION_SELECTION.COMMAND;
                    info.SoftwareLimit.PositivePos = 0;
                    info.SoftwareLimit.NegativePos = 0;
                }
                else
                {
                    info.SoftwareLimit.Enable = enable == 1;
                    info.SoftwareLimit.StopMode = (AXT_MOTION_STOPMODE)stopMode;
                    info.SoftwareLimit.Selection = (AXT_MOTION_SELECTION)selection;
                    info.SoftwareLimit.PositivePos = positivePos;
                    info.SoftwareLimit.NegativePos = negativePos;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotGetAbsRelMode(this AxisSettingInfo info)
        {
            try
            {
                uint mode = 0;
                var res = (AXT_FUNC_RESULT)CAXM.AxmMotGetAbsRelMode(info.No, ref mode);
                if (res.ErrorCheck() == false)
                {
                    info.AbsRelMode = AXT_MOTION_ABSREL.POS_ABS_MODE;
                }
                else
                {
                    info.AbsRelMode = (AXT_MOTION_ABSREL)mode;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotGetProfileMode(this AxisSettingInfo info)
        {
            try
            {
                uint mode = 0;
                var res = (AXT_FUNC_RESULT)CAXM.AxmMotGetProfileMode(info.No, ref mode);
                if (res.ErrorCheck() == false)
                {
                    info.ProfileMode = AXT_MOTION_PROFILE_MODE.SYM_TRAPEZOIDE_MODE;
                }
                else
                {
                    info.ProfileMode = (AXT_MOTION_PROFILE_MODE)mode;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmSignalGetEncoderType(this AxisSettingInfo info)
        {
            try
            {
                uint mode = 0;
                var res = (AXT_FUNC_RESULT)CAXDev.AxmSignalGetEncoderType(info.No, ref mode);
                if (res.ErrorCheck() == false)
                {
                    info.EncodeType = AXT_ENCODE_TYPE.TYPE_INCREMENTAL;
                }
                else
                {
                    info.EncodeType = (AXT_ENCODE_TYPE)mode;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotGetParaLoad(this AxisSettingInfo info)
        {
            try
            {
                var position = 0.0D;
                var speed = 0.0D;
                var accel = 0.0D;
                var decel = 0.0D;

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotGetParaLoad(info.No, ref position, ref speed, ref accel, ref decel);
                res.ErrorCheck();

                info.InitMove.Position = position;
                info.InitMove.Speed = speed;
                info.InitMove.Accel = accel;
                info.InitMove.Decel = decel;
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotSetParaLoad(this AxisSettingInfo info)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmMotSetParaLoad(info.No, info.InitMove.Position, info.InitMove.Speed, info.InitMove.Accel, info.InitMove.Decel);
                res.ErrorCheck();
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmHomeGetSignalLevel(this AxisSettingInfo info)
        {
            try
            {
                uint level = 0;
                var res = (AXT_FUNC_RESULT)CAXM.AxmHomeGetSignalLevel(info.No, ref level);
                if (res.ErrorCheck() == false)
                {
                    info.Home.HomeMode = AXT_MOTION_LEVEL_MODE.UNUSED;
                }
                else
                {
                    info.Home.HomeMode = (AXT_MOTION_LEVEL_MODE)level;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmHomeGetMethod(this AxisSettingInfo info)
        {
            try
            {
                int direction = 0;
                uint level = 0;
                uint zPhase = 0;
                double clearDeley = 0;
                double offSet = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmHomeGetMethod(info.No, ref direction, ref level, ref zPhase, ref clearDeley, ref offSet);
                if (res.ErrorCheck() == false)
                {
                    info.Home.Direction = AXT_MOTION_MOVE_DIR.DIR_CCW;
                    info.Home.HomeLevel = AXT_MOTION_HOME_DETECT.PosEndLimit;
                    info.Home.ZPhase = false;
                    info.Home.ClearDeleay = 0;
                    info.Home.OffSet = 0;
                }
                else
                {
                    info.Home.Direction = (AXT_MOTION_MOVE_DIR)direction;
                    info.Home.HomeLevel = (AXT_MOTION_HOME_DETECT)level;
                    info.Home.ZPhase = zPhase == 1;
                    info.Home.ClearDeleay = clearDeley;
                    info.Home.OffSet = offSet;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmHomeGetVel(this AxisSettingInfo info)
        {
            try
            {
                double firstSpeed = 0;
                double secondSpeed = 0;
                double thirdSpeed = 0;
                double lastSpeed = 0;

                double firstAccel = 0;
                double secoundAccel = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmHomeGetVel(info.No, ref firstSpeed, ref secondSpeed, ref thirdSpeed, ref lastSpeed, ref firstAccel, ref secoundAccel);
                if (res.ErrorCheck() == false)
                {
                    info.Home.FirstSpeed = 0;
                    info.Home.SecondSpeed = 0;
                    info.Home.ThirdSpeed = 0;
                    info.Home.LastSpeed = 0;

                    info.Home.FirstAccel = 0;
                    info.Home.SecoundAccel = 0;
                }
                else
                {
                    info.Home.FirstSpeed = firstSpeed;
                    info.Home.SecondSpeed = secondSpeed;
                    info.Home.ThirdSpeed = thirdSpeed;
                    info.Home.LastSpeed = lastSpeed;

                    info.Home.FirstAccel = firstAccel;
                    info.Home.SecoundAccel = secoundAccel;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotSetPulseOutMethod(this AxisSettingInfo info)
        {
            try
            {
                if (info.OutputPulse.HasValue == false) return;

                var upMethod = (uint)info.OutputPulse.Value;

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotSetPulseOutMethod(info.No, upMethod);
                res.ErrorCheck();
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotGetPulseOutMethod(this AxisSettingInfo info)
        {
            try
            {
                uint upMethod = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotGetPulseOutMethod(info.No, ref upMethod);
                if (res.ErrorCheck() == false)
                {
                    info.OutputPulse = null;
                }
                else
                {
                    info.OutputPulse = (AXT_MOTION_PULSE_OUTPUT)upMethod;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotSetEncInputMethod(this AxisSettingInfo info)
        {
            try
            {
                if (info.EncInput.HasValue == false) return;

                var upMethod = (uint)info.EncInput.Value;

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotSetEncInputMethod(info.No, upMethod);
                res.ErrorCheck();
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        public static void AxmMotGetEncInputMethod(this AxisSettingInfo info)
        {
            try
            {
                uint upMethod = 0;

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotGetEncInputMethod(info.No, ref upMethod);
                if (res.ErrorCheck() == false)
                {
                    info.EncInput = null;
                }
                else
                {
                    info.EncInput = (AXT_MOTION_EXTERNAL_COUNTER_INPUT)upMethod;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        #endregion
    }
}
