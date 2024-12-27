using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.JF.DTO;
using X_MOTION;

namespace TS.FW.JF.Core
{
    internal static class JFAxisCore
    {

        #region JFC - Motion Init 
        /// <summary>
        /// Motion Board 및 Library 초기화 함수
        /// Motion API를 사용하기 위해 제일 먼저 실행되어야 한다.
        /// </summary>
        /// <param name="Device"></param>
        /// <returns> 0 : OK, 1 : Error</returns>
        public static MMC_STAT MotionBoardInit(this JFDevice Device)
        {
            var iResult = XMotionLib.mmc_initx();
            return (MMC_STAT)iResult;
        }
        /// <summary>
        /// 파라미터 공정 초기 값으로 셋팅
        /// </summary>
        /// <param name="AxisNo"></param>
        public static void SetInitParameter(int AxisNo)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.set_mmc_parameter_init(iAxisNo);
        }
        /// <summary>
        /// 현재 셋팅되어진 파라미터 값들을 파일로 저장
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static MMC_STAT SetParameter(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.set_parameter(iAxisNo);
            return (MMC_STAT)iResult;
        }
        /// <summary>
        /// 파라미터 파일을 불러들여 파라미터 셋팅
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static MMC_STAT GetParameter(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.get_parameter(iAxisNo);
            return (MMC_STAT)iResult;
        }
        #endregion

        #region JFC - Motion Mode 
        /// <summary>
        /// 출력 펄스 모드를 설정
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="PulseMode"></param>
        public static void SetPulseMode(int AxisNo, int PulseMode)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iSetMode = Convert.ToInt16(PulseMode);
            XMotionLib.set_pulse_mode(iAxisNo, iSetMode);
        }
        public static int GetPulseMode(int AxisNo)
        {
            //출력 펄스 모드 반환
            Int16 iGetMode = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.get_pulse_mode(iAxisNo, ref iGetMode);
            return iGetMode;
        }
        public static void SetEncorderMode(int AxisNo, int PulseMode)
        {
            //입력 엔코더 모드 설정
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iSetMode = Convert.ToInt16(PulseMode);
            XMotionLib.set_encoder_mode(iAxisNo, iSetMode);
        }
        public static int GetEncorderMode(int AxisNo)
        {
            //입력 엔코더 모드 반환
            Int16 iGetMode = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.get_encoder_mode(iAxisNo, ref iGetMode);
            return iGetMode;
        }
        public static void SetEncoderDir(this JFAxis Axis, enDir Dir)
        {
            //입력 엔코더 카운트 방향을 설정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetDir = Convert.ToInt16(Dir);
            XMotionLib.set_encoder_direction(iAxisNo, iSetDir);
        }
        public static enDir GetEncoderDir(this JFAxis Axis)
        {
            //입력 엔코더 카운트 방향을 반환
            Int16 iGetDir = 0;
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.get_encoder_direction(iAxisNo, ref iGetDir);
            return (enDir)iGetDir;
        }
        public static void SetAmpEnable(this JFAxis Axis, bool Status)
        {
            //서보 드라이브 ON 출력을 설정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetStatus = Convert.ToInt16(Status);
            XMotionLib.set_amp_enable(iAxisNo, iSetStatus);
        }
        public static bool GetAmpEnable(this JFAxis Axis)
        {
            //서보 드라이브 ON 출력을 설정
            Int16 iGetStatus = 0;
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.get_amp_enable(iAxisNo, ref iGetStatus);
            return Convert.ToBoolean(iGetStatus);
        }
        public static MMC_STAT SetAmpReset(this JFAxis Axis, bool Status)
        {
            //서보 드라이브 Reset 출력을 설정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetStatus = Convert.ToInt16(Status);
            var iResult = XMotionLib.set_amp_reset(iAxisNo, iSetStatus);
            return (MMC_STAT)iResult;
        }
        public static int GetAmpReset(int AxisNo)
        {
            //서보 드라이브 Reset 출력을 설정
            Int16 iGetStatus = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.get_amp_reset(iAxisNo, ref iGetStatus);
            return iGetStatus;
        }
        public static MMC_STAT SetAmpFault_Level(this JFAxis Axis, enLevel Level)
        {
            //서보 드라이브 알람 입력 레벨을 설정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetLevel = Convert.ToInt16(Level);
            var iResult = XMotionLib.set_amp_fault_level(iAxisNo, iSetLevel);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetAmpFaultLevel(int AxisNo, ref enLevel GetLevel)
        {
            //서보 드라이브 알람 입력 레벨을 반환
            Int16 iGetLevel = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iResult = XMotionLib.get_amp_fault_level(iAxisNo, ref iGetLevel);
            GetLevel = (enLevel)iGetLevel;
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT SetInPos_Level(this JFAxis Axis, enLevel Level)
        {
            //서보 드라이브 인포지션 입력 레벨을 설정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetLevel = Convert.ToInt16(Level);
            var iResult = XMotionLib.set_inposition_level(iAxisNo, iSetLevel);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetInPosLevel(int AxisNo, ref enLevel GetLevel)
        {
            //서보 드라이브 인포지션 입력 레벨을 반환
            Int16 iGetLevel = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iResult = XMotionLib.get_inposition_level(iAxisNo, ref iGetLevel);
            GetLevel = (enLevel)iGetLevel;
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT SetHome_Level(this JFAxis Axis, enLevel Level)
        {
            //홈 센서 입력 레벨을 설정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetLevel = Convert.ToInt16(Level);
            var iResult = XMotionLib.set_home_level(iAxisNo, iSetLevel);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetHomeLevel(int AxisNo, ref enLevel GetLevel)
        {
            //홈 센서 입력 레벨을 반환
            Int16 iGetLevel = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iResult = XMotionLib.get_home_level(iAxisNo, ref iGetLevel);
            GetLevel = (enLevel)iGetLevel;
            return (MMC_STAT)iResult;
        }
        /// <summary>
        /// Positive, Negative Limit 센서 입력 레벨을 설정
        /// Positive, Negative Limit 센서 동시에 적용되르모, 같은 접점 타입의 센서 사용 권장 
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static MMC_STAT SetLimit_Level(this JFAxis Axis, enLevel Level)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetLevel = Convert.ToInt16(Level);
            var iResult = XMotionLib.set_limit_level(iAxisNo, iSetLevel);
            return (MMC_STAT)iResult;
        }
        /// <summary>
        /// Positive, Negative Limit 센서 입력 레벨을 반환
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="GetLevel"></param>
        /// <returns></returns>
        public static MMC_STAT GetLimitLevel(int AxisNo, ref enLevel GetLevel)
        {
            Int16 iGetLevel = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iResult = XMotionLib.get_limit_level(iAxisNo, ref iGetLevel);
            GetLevel = (enLevel)iGetLevel;
            return (MMC_STAT)iResult;
        }
        /// <summary>
        /// 래치 입력 레벨을 설정
        /// User Input 0번을 래치 트리거 입력으로 사용,
        /// get_latch_position()로 래치된 위치를 읽을 수 있으며,
        /// 이때, 래치 트리거 입력 신호의 감지 레벨을 설정한다.
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static MMC_STAT SetLatch_Level(this JFAxis Axis, enLevel Level)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iSetLevel = Convert.ToInt16(Level);
            var iResult = XMotionLib.set_latch_level(iAxisNo, iSetLevel);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetLatchLevel(int axno, ref enLevel GetLevel)
        {
            //래치 입력 레벨을 반환
            Int16 iGetLevel = 0;
            var iAxisNo = Convert.ToInt16(axno);
            var iResult = XMotionLib.get_latch_level(iAxisNo, ref iGetLevel);
            GetLevel = (enLevel)iGetLevel;
            return (MMC_STAT)iResult;
        }
        public static bool GetLatchState(this JFAxis Axis, enLevel Level = enLevel.HIGH)
        {
            var tmpint = 0;
            XMotionLib.get_io(0, ref tmpint);
            var st = (tmpint >> 0) & 0x00000001;
            var ret = Level == enLevel.LOW ? st == 0 : st == 1;
            return ret;
        }
        #endregion

        #region JFC - Motion Status 
        /// <summary>
        /// 모션 보드의 이동에 관한 내부 동작이 진행 중인 상태 반환
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <returns>0 : 모션 동작 정지 상태, 1 : 모션 동작 진행 중인 상태</returns>
        public static bool InSequenceDone(int AxisNo)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iResult = XMotionLib.in_sequence(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// 모션 보드의 이동 출력이 진행 중인 상태 반환
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <returns>0 : 모션 동작 정지 상태, 1 : 모션 동작 진행 중인 상태</returns>
        public static bool InMotionDone(int AxisNo)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iResult = XMotionLib.in_motion(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// 서보 드라이브로부터 오는 위치 완료의 상태 반환
        /// </summary>
        /// <param name="Axis">0 : 서보 드라이브 InPosition 입력 Off 상태, 1 : 서보 드라이브 InPosition 입력 On 상태 </param>
        /// <returns></returns>
        public static bool InPositionDone(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.in_position(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// 모션 보드의 제어와 출력이 완료된 상태 반환
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <returns>0 : 모션 동작 중인 상태 (in_sequence || in_motion ), 1 : 모션 동작 완료 상태 (!in_sequence && !in_motion )</returns>
        public static bool MotionDone(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.motion_done(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// 모션 보드의 동작과 서보드라이브의 동작이 완료된 상태 반환 
        /// 최종적으로 Motion Done 상태 확인하는 용도로 사용 가능
        /// </summary>
        /// <param name="Axis">0 : 모션 동작 중인 상태 ( !motion_done || ! in_position ), 1 : 모션 동작 완료 상태 ( motion_done && in_position )</param>
        /// <returns></returns>
        public static bool MoveDone(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.axis_done(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// 지정축의 모션 완료를 대기
        /// Same -  while(!axis_done(ax)) Sleep(1); 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <returns>0 : 모션 동작 중인 상태 ( !motion_done || ! in_position ), 1 : 모션 동작 완료 상태 ( motion_done && in_position )</returns>
        public static int WaitMoveDone(int AxisNo)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iResult = XMotionLib.wait_for_done(iAxisNo);
            return iResult;
        }
        /// <summary>
        /// 에러 인터럽트 발생으로 멈추게 된 상태를 나타낸다.
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static int GetError(this JFAxis Axis)
        {
            int iResult = 0;
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.get_errint(iAxisNo, ref iResult);
            return iResult;
        }
        /// <summary>
        /// 에러 인터럽트 발생 플래그 레지스터를 클리어
        /// </summary>
        /// <param name="Axis"></param>
        public static void ResetClear(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.reset_errint(iAxisNo);
        }
        /// <summary>
        /// 에러 인터럽트(get_errint() ) 발생으로 멈추었는지 반환한다. 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <returns></returns>
        public static int GetAxisStatus(int AxisNo)
        {
            int iResult = 0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            iResult = XMotionLib.axis_state(iAxisNo);
            return iResult;
        }
        /// <summary>
        /// 홈센서 입력 상태를 나타낸다. ( 0: OFF, 1: ON ) 
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static bool GetHome(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.home_switch(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// + Limit 센서 입력 상태를 나타낸다. ( 0: OFF, 1: ON ) 
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static bool GetPosLimit(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.pos_switch(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// - Limit 센서 입력 상태를 나타낸다. ( 0: OFF, 1: ON )  
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static bool GetNegLimit(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.neg_switch(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        /// <summary>
        /// 서보드라이브 ALARM 입력 상태를 나타낸다. ( 0: OFF, 1: ON ) 
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static bool GetFault(this JFAxis Axis)
        {
            int iResult = 0;
            var iAxisNo = Convert.ToInt16(Axis.No);
            iResult = XMotionLib.amp_fault_switch(iAxisNo);
            return Convert.ToBoolean(iResult);
        }
        #endregion

        #region JFC - Position Counter
        public static MMC_STAT SetCommand(this JFAxis Axis, double SetCommand)
        {
            //위치 지령 펄스 카운터 값을 지정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.set_command(iAxisNo, SetCommand);
            return (MMC_STAT)iResult;
        }
        public static double GetCommand(this JFAxis Axis)
        {
            //위치 지령 펄스 카운터 값을 반환 
            double dGetCommand = 0.0;
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.get_command(iAxisNo, ref dGetCommand);
            return dGetCommand;
        }
        public static MMC_STAT SetEncoder(this JFAxis Axis, double SetEncoder)
        {
            //위치 피드백 엔코더 카운터 값을 지정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.set_position(iAxisNo, SetEncoder);
            return (MMC_STAT)iResult;
        }
        public static double GetEncoder(this JFAxis Axis)
        {
            //위치 피드백 엔코더 카운터 값을 반환 
            double dGetEncoder = 0.0;
            if (Axis.IsEnable == false)  //todo : Encoder Axis used 
                return 0;
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.get_position(iAxisNo, ref dGetEncoder);
            return dGetEncoder;
        }
        public static double GetPosLatch(this JFAxis Axis)
        {
            double Ratio = 0.0;
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.get_latched_position(iAxisNo, ref Ratio);
            return Ratio;
        }

        public static void SetInPos(int AxisNo, double SetInpos)
        {
            //에러 (출력 펄스 - 입력 엔코더 차이값) 카운터 값을 지정
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.set_error(iAxisNo, SetInpos);
        }
        public static double GetInPos(int AxisNo)
        {
            //에러 (출력 펄스 - 입력 엔코더 차이값) 카운터 값을 반환
            double dGetInPos = 0.0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.get_error(iAxisNo, ref dGetInPos);
            return dGetInPos;
        }
        public static void SetMPG(int AxisNo, double SetMPG)
        {
            //위치 피드백 엔코더 카운터 값을 지정
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.set_mpg_counter(iAxisNo, SetMPG);
        }
        public static double GetMPG(int AxisNo)
        {
            //위치 피드백 엔코더 카운터 값을 반환 
            double dGetMPG = 0.0;
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.get_mpg_counter(iAxisNo, ref dGetMPG);
            return dGetMPG;
        }
        #endregion

        #region JFC - Alarm Stop Action
        public static MMC_STAT SetPosLimit(this JFAxis Axis, double Pos, enStopAct Act)
        {
            // + Software Limit 위치와 동작을 지정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.set_positive_sw_limit(iAxisNo, Pos, iAct);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetPosLimit(int AxisNo, ref double Pos, ref enStopAct Act)
        {
            // + Software Limit 위치와 동작을 반환
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.get_positive_sw_limit(iAxisNo, ref Pos, ref iAct);
            Act = (enStopAct)iAct;
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT SetNegLimit(this JFAxis Axis, double Pos, enStopAct Act)
        {
            // + Software Limit 위치와 동작을 지정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.set_negative_sw_limit(iAxisNo, Pos, iAct);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetNegLimit(int AxisNo, ref double Pos, ref enStopAct Act)
        {
            // + Software Limit 위치와 동작을 반환
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.get_negative_sw_limit(iAxisNo, ref Pos, ref iAct);
            Act = (enStopAct)iAct;
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT SetLimitAct(this JFAxis Axis, enStopAct Act)
        {
            // +/- Limit 센서 감지시 동작을 지정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.set_limit_action(iAxisNo, iAct);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetLimitAct(int AxisNo, ref enStopAct Act)
        {
            // +/- Limit 센서 감지시 동작을 반환
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.get_limit_action(iAxisNo, ref iAct);
            Act = (enStopAct)iAct;
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT SetAlarmLimit(this JFAxis Axis, enStopAct Act)
        {
            //서보 드라이브 알람 감지시 동작을 지정
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.set_amp_fault(iAxisNo, iAct);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT GetAmpLimit(int AxisNo, ref enStopAct Act)
        {
            //서보 드라이브 알람 감지시 동작을 반환
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAct = Convert.ToInt16(Act);
            var iResult = XMotionLib.get_amp_fault(iAxisNo, ref iAct);
            Act = (enStopAct)iAct;
            return (MMC_STAT)iResult;
        }
        #endregion

        #region JFC - Single Axis Motion
        //tv_move, sv_move, emg_stop, dec_stop 은 호출 즉시 적용되는 명령이며, 다른 이동 명령들은
        //연속 이동 사용시 일단 버퍼에 저장되어 현재 이동중인 명령을 수행 완료한 후 기동된다.
        public static void Move_tv(this JFAxis Axis, double StartVel, double MaxVel, int Acc)
        {
            //속도 이동 명령으로 지정된 속도로 사다리꼴 가속 프로파일로 이동
            // tv_move, sv_move 인 경우만 maxvel 의 부호가 회전 방향을 정의
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.tv_move(iAxisNo, StartVel, MaxVel, iAcc);
        }
        public static void Move_sv(int AxisNo, double StartVel, double MaxVel, int Acc)
        {
            //속도 이동 명령으로 지정된 속도로 S커브 가속 프로파일로 이동
            // tv_move, sv_move 인 경우만 maxvel 의 부호가 회전 방향을 정의
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.sv_move(iAxisNo, StartVel, MaxVel, iAcc);
        }
        public static MMC_STAT EmgStop(this JFAxis Axis)
        {
            //가감속 없이 즉시 정지하는 명령
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.emg_stop(iAxisNo);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT DecStop(this JFAxis Axis)
        {
            //이동 명령으로 운행 도중에 감속하여 정지하는 명쳥
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iResult = XMotionLib.dec_stop(iAxisNo);
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT Start_tr_iMove(this JFAxis Axis, double Pos, double StartVel, double MaxVel, int Acc, int Dec)
        {
            //사다리꼴 상대 이동 명령
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            var iResult = XMotionLib.start_tr_imove(iAxisNo, Pos, StartVel, MaxVel, iAcc, iDec); //Pos : 목표 상대위치
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT Start_ta_iMove(this JFAxis Axis, double Pos, double StartVel, double MaxVel, int Acc, int Dec)
        {
            //사다리꼴 절대 이동 명령
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            var iResult = XMotionLib.start_ta_imove(iAxisNo, Pos, StartVel, MaxVel, iAcc, iDec); //Pos : 목표 절대위치
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT Start_sr_iMove(this JFAxis Axis, double Pos, double StartVel, double MaxVel, int Acc, int Dec)
        {
            //S커브 상대 이동 명령
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            var iResult = XMotionLib.start_sr_imove(iAxisNo, Pos, StartVel, MaxVel, iAcc, iDec); //Pos : 목표 상대위치
            return (MMC_STAT)iResult;
        }
        public static MMC_STAT Start_sa_iMove(this JFAxis Axis, double Pos, double StartVel, double MaxVel, int Acc, int Dec)
        {
            //S커브 절대 이동 명령
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            var iResult = XMotionLib.start_sa_imove(iAxisNo, Pos, StartVel, MaxVel, iAcc, iDec); //Pos : 목표 절대위치
            return (MMC_STAT)iResult;
        }

        //대칭 이동 Motion Profile
        public static void Start_Move(int AxisNo, double Pos, double Vel, int Acc)
        {
            //절대 대칭 사다리꼴 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.start_move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 절대위치
        }
        public static void Move(int AxisNo, double Pos, double Vel, int Acc)
        {
            //절대 대칭 사다리꼴 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 절대위치
        }
        public static void Start_rMove(int AxisNo, double Pos, double Vel, int Acc)
        {
            //상대 대칭 사다리꼴 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.start_r_move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 상대위치
        }
        public static void rMove(int AxisNo, double Pos, double Vel, int Acc)
        {
            //상대 대칭 사다리꼴 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.r_move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 상대위치
        }
        public static void Start_sMove(int AxisNo, double Pos, double Vel, int Acc)
        {
            //절대 대칭 S커브 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.start_s_move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 절대위치
        }
        public static void sMove(int AxisNo, double Pos, double Vel, int Acc)
        {
            //절대 대칭 S커브 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.s_move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 절대위치
        }
        public static void Start_rsMove(int AxisNo, double Pos, double Vel, int Acc)
        {
            //상대 대칭 S커브 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.start_rs_move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 상대위치
        }
        public static void rsMove(int AxisNo, double Pos, double Vel, int Acc)
        {
            //상대 대칭 S커브 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            XMotionLib.rs_move(iAxisNo, Pos, Vel, iAcc); //Pos : 목표 상대위치
        }

        //비대칭 이동 Motion Profile
        public static void Start_tMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //절대 비대칭 사다리꼴 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.start_t_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 절대위치
        }
        public static void tMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //절대 비대칭 사다리꼴 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.t_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 절대위치
        }
        public static void Start_trMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //상대 비대칭 사다리꼴 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.start_tr_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 상대위치
        }
        public static void trMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //상대 비대칭 사다리꼴 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.tr_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 상대위치
        }
        public static void Start_tsMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //절대 비대칭 S커브 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.start_ts_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 절대위치
        }
        public static void tsMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //절대 비대칭 S커브 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.ts_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 절대위치
        }
        public static void Start_trsMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //상대 비대칭 S커브 이동
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.start_trs_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 상대위치
        }
        public static void trsMove(int AxisNo, double Pos, double Vel, int Acc, int Dec)
        {
            //상대 비대칭 S커브 이동 & 완료 대기
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iAcc = Convert.ToInt16(Acc);
            var iDec = Convert.ToInt16(Dec);
            XMotionLib.trs_move(iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 목표 상대위치
        }

        //단축 다중 Motion Profile
        public static void Start_Move_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc)
        {
            //절대 대칭 사다리꼴 이동
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            XMotionLib.start_move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc); //Pos : 절대위치
        }
        public static void Move_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc)
        {
            //절대 대칭 사다리꼴 이동 & 완료 대기
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            XMotionLib.move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc); //Pos : 절대위치
        }
        public static void Start_sMove_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc)
        {
            //절대 대칭 S커브 이동
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            XMotionLib.start_s_move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc); //Pos : 절대위치
        }
        public static void sMove_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc)
        {
            //절대 대칭 사다리꼴 이동
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            XMotionLib.s_move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc); //Pos : 절대 위치
        }
        public static void Start_tMove_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc, int[] Dec)
        {
            //절대 비대칭 사다리꼴 이동
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            var iDec = ToInt16(Dec);
            XMotionLib.start_t_move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 절대위치
        }
        public static void tMove_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc, int[] Dec)
        {
            //절대 비대칭 사다리꼴 이동 & 완료 대기
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            var iDec = ToInt16(Dec);
            XMotionLib.t_move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 절대위치
        }
        public static void Start_tsMove_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc, int[] Dec)
        {
            //절대 비대칭 S커브 이동
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            var iDec = ToInt16(Dec);
            XMotionLib.start_ts_move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 절대위치
        }
        public static void tsMove_All(int AxisCnt, int[] AxisNo, double[] Pos, double[] Vel, int[] Acc, int[] Dec)
        {
            //절대 비대칭 사다리꼴 이동
            var iAxisCnt = Convert.ToInt16(AxisCnt);
            var iAxisNo = ToInt16(AxisNo);
            var iAcc = ToInt16(Acc);
            var iDec = ToInt16(Dec);
            XMotionLib.ts_move_all(iAxisCnt, iAxisNo, Pos, Vel, iAcc, iDec); //Pos : 절대 위치
        }
        #endregion

        #region JFC - Continuous Path Motion
        public static void SetContEnable(int AxisNo, int Enable)
        {
            //연속이동 설정
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iEnable = Convert.ToInt16(Enable);
            XMotionLib.set_continuous_enable(iAxisNo, iEnable);
        }
        public static int GetContBuffer(int AxisNo)
        {
            //연속이동 버퍼 반환
            var iAxisNo = Convert.ToInt16(AxisNo);
            var GetEnable = XMotionLib.get_continuous_buffer(iAxisNo);
            return GetEnable;
        }
        #endregion

        #region JFC - Override Motion
        public static void OverridePos(int AxisNo, double Pos)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.position_override(iAxisNo, Pos);
        }
        public static void OverrideVel(int AxisNo, double Vel)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.velocity_override(iAxisNo, Vel);
        }
        public static void SetOverrideVelRange(int AxisNo, double MaxVel, int Enable)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iEnable = Convert.ToInt16(Enable);
            XMotionLib.set_velocity_range(iAxisNo, MaxVel, iEnable);
        }
        public static void GetOverrideVelRange(int AxisNo, ref double MaxVel, ref int Enable)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iEnable = Convert.ToInt16(Enable);
            XMotionLib.get_velocity_range(iAxisNo, ref MaxVel, ref iEnable);
            Enable = iEnable;
        }
        #endregion

        #region JFC - MPG Motion
        /// <summary>
        /// MPG 속도 이동 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="LimitVel">최대 제한 속도 </param>
        public static void vMoveMPG(this JFAxis Axis, double LimitVel)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var ret = XMotionLib.mpg_v_move(iAxisNo, LimitVel);
        }
        /// <summary>
        /// MPG 위치 이동 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="Pos">상대 이동 거리량 </param>
        /// <param name="LimitVel">최대 제한 속도 </param>
        public static void pMoveMPG(this JFAxis Axis, double Pos, double LimitVel)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var ret = XMotionLib.mpg_p_move(iAxisNo, Pos, LimitVel);
        }
        /// <summary>
        /// MPG 입력 모드 설정 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="Mode">
        /// 0 : AqB x1 (AB상 1체배)  1 : AqB x2(AB상 2체배) 3 : AqB x4(AB상 4체배) 3 : Two Pulse(CW + CCW ) 
        /// </param>
        public static void SetMPGMode(this JFAxis Axis, int Mode)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iMode = Convert.ToInt16(Mode);
            XMotionLib.set_mpg_mode(iAxisNo, iMode);
        }
        /// <summary>
        /// MPG 입력 모드 반환 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="Mode"></param>
        public static int GetMPGMode(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            short iMode = 0;
            XMotionLib.get_mpg_mode(iAxisNo, ref iMode);
            return iMode;
        }
        /// <summary>
        /// MPG 입력 방향 설정 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="Dir">0 : A lead B ( CW 회전시 +1 씩 증가) , 1 : B lead A ( CCW 회전시 +1 씩 증가 )</param>
        public static void SetMPGDir(this JFAxis Axis, int Dir)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iDir = Convert.ToInt16(Dir);
            XMotionLib.set_mpg_dir(iAxisNo, iDir);
        }
        /// <summary>
        /// MPG 입력 방향 반환 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="Dir"></param>
        public static int GetMPGDir(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            short iDir = 0;
            XMotionLib.get_mpg_dir(iAxisNo, ref iDir);
            return iDir;
        }
        /// <summary>
        /// MPG 입력 체배 설정 (ratio : 1~32 배의 비율로 MPG 이동)
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="Ratio"></param>
        public static void SetMPGRatio(this JFAxis Axis, int Ratio)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.set_mpg_ratio(iAxisNo, Ratio);
        }
        /// <summary>
        /// MPG 입력 체배 반환 
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="Ratio"></param>
        public static int GetMPGRatio(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            int Ratio = 0;
            XMotionLib.get_mpg_ratio(iAxisNo, ref Ratio);
            return Ratio;
        }
        public static bool GetMPGFilter(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            short enable = 0;

            XMotionLib.get_mpg_filter(iAxisNo, ref enable);

            return enable == 1;
        }

        public static void SetMPGFilter(this JFAxis Axis, bool enable)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);

            XMotionLib.set_mpg_filter(iAxisNo, (short)(enable ? 1 : 0));
        }

        public static double GetMPGCount(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            double counter = 0;

            XMotionLib.get_mpg_counter(iAxisNo, ref counter);

            return counter;

        }

        public static void SetMPGCount(this JFAxis Axis, double counter)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);

            XMotionLib.set_mpg_counter(iAxisNo, counter);
        }

        #endregion

        #region JFC - Special Function
        public static MMC_STAT MoveHome(this JFAxis Axis, int Dir, double StartVel, double MaxVel, double HomeVel, int Acc, int Mode)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            var iDir = Convert.ToInt16(Dir);
            var iAcc = Convert.ToInt16(Acc);
            var iMode = Convert.ToInt16(Mode);
            var iResult = XMotionLib.homing(iAxisNo, iDir, StartVel, MaxVel, HomeVel, iAcc, iMode);
            return (MMC_STAT)iResult;
        }
        public static void SetElectricGear(this JFAxis Axis, double Ratio)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.set_electric_gear(iAxisNo, Ratio);
        }
        public static double GetElectricGear(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            double dRatio = 0.0;
            XMotionLib.get_electric_gear(iAxisNo, ref dRatio);
            return dRatio;
        }
        //public static void GetPosLatch(int AxisNo, ref double Ratio)
        //{
        //    var iAxisNo = Convert.ToInt16(AxisNo);
        //    XMotionLib.get_latched_position(iAxisNo, ref Ratio);
        //}
        public static void SetCorrect(int AxisNo, int Type, double ErrPos, double Vel)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iType = Convert.ToInt16(Type);
            XMotionLib.set_correction(iAxisNo, iType, ErrPos, Vel);
        }
        public static void GetCorrect(int AxisNo, ref int Type, ref double ErrPos)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            var iType = Convert.ToInt16(Type);
            XMotionLib.get_correction(iAxisNo, ref iType, ref ErrPos);
            Type = iType;
        }
        public static void SetAmpResolution(this JFAxis Axis, int Resolution)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            XMotionLib.set_amp_resolution(iAxisNo, Resolution);
        }
        public static int GetAmpResolution(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            int iResolution = 0;
            XMotionLib.get_amp_resolution(iAxisNo, ref iResolution);
            return iResolution;
        }

        public static int GetCommandRPM(this JFAxis Axis)
        {
            var iAxisNo = Convert.ToInt16(Axis.No);
            Int16 iRpm = 0;
            XMotionLib.get_command_rpm(iAxisNo, ref iRpm);
            return iRpm;
        }
        public static void Reset(int AxisNo)
        {
            var iAxisNo = Convert.ToInt16(AxisNo);
            XMotionLib.mmc_register_reset(iAxisNo);
        }
        #endregion


        public static void LoadSetting(this SettingInfo info)
        {
            try
            {
                info.MMC_GetPosLimitData();
                info.MMC_GetNegLimitData();
                info.MMC_GetHWLimitData();
                info.MMC_GetAmpLimitData();
                info.MMC_GetLimitLevelData();
                info.MMC_GetHomeLevelData();
                info.MMC_GetLatchLevelData();
                info.MMC_GetInPosLevelData();
                info.MMC_GetAmpFaultLevelData();
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetPosLimitData(this SettingInfo info)
        {
            try
            {
                double pos = 0.0;
                enStopAct act = 0;
                var result = GetPosLimit(info.No, ref pos, ref act);
                if (result.IsOK())
                {
                    info.SoftwareLimit.PosLimitPos = pos;
                    info.SoftwareLimit.PosLimitAct = act;
                }
                else
                {
                    info.SoftwareLimit.PosLimitPos = 0.0;
                    info.SoftwareLimit.PosLimitAct = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetNegLimitData(this SettingInfo info)
        {
            try
            {
                double pos = 0.0;
                enStopAct act = 0;
                var result = GetNegLimit(info.No, ref pos, ref act);
                if (result.IsOK())
                {
                    info.SoftwareLimit.NegLimitPos = pos;
                    info.SoftwareLimit.NegLimitAct = act;
                }
                else
                {
                    info.SoftwareLimit.NegLimitPos = 0.0;
                    info.SoftwareLimit.NegLimitAct = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetHWLimitData(this SettingInfo info)
        {
            try
            {
                enStopAct act = 0;
                var result = GetLimitAct(info.No, ref act);
                if (result.IsOK())
                {
                    info.Limit.HwLimitAct = act;
                }
                else
                {
                    info.Limit.HwLimitAct = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetAmpLimitData(this SettingInfo info)
        {
            try
            {
                enStopAct act = 0;
                var result = GetAmpLimit(info.No, ref act);
                if (result.IsOK())
                {
                    info.Limit.AlarmAct = act;
                }
                else
                {
                    info.Limit.AlarmAct = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetLimitLevelData(this SettingInfo info)
        {
            try
            {
                enLevel level = 0;
                var result = GetLimitLevel(info.No, ref level);
                if (result.IsOK())
                {
                    info.Limit.LimitLevel = level;
                }
                else
                {
                    info.Limit.LimitLevel = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetHomeLevelData(this SettingInfo info)
        {
            try
            {
                enLevel level = 0;
                var result = GetHomeLevel(info.No, ref level);
                if (result.IsOK())
                {
                    info.Limit.HomeLevel = level;
                }
                else
                {
                    info.Limit.HomeLevel = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetLatchLevelData(this SettingInfo info)
        {
            try
            {
                enLevel level = 0;
                var result = GetLatchLevel(info.No, ref level);
                if (result.IsOK())
                {
                    info.Limit.LatchLevel = level;
                }
                else
                {
                    info.Limit.LatchLevel = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetInPosLevelData(this SettingInfo info)
        {
            try
            {
                enLevel level = 0;
                var result = GetInPosLevel(info.No, ref level);
                if (result.IsOK())
                {
                    info.Limit.InPosLevel = level;
                }
                else
                {
                    info.Limit.InPosLevel = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }
        static void MMC_GetAmpFaultLevelData(this SettingInfo info)
        {
            try
            {
                enLevel level = 0;
                var result = GetAmpFaultLevel(info.No, ref level);
                if (result.IsOK())
                {
                    info.Limit.AmpLevel = level;
                }
                else
                {
                    info.Limit.AmpLevel = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(info, ex);
            }
        }

        //todo : 아래 자료처리 분리 수정 필요 by Jp
        //Array Type-Change
        private static Int16[] ToInt16(int[] iArray)
        {
            Int16[] sArray = new Int16[iArray.Length];
            for (int i = 0; i < iArray.Length; i++)
            {
                sArray[i] = Convert.ToInt16(iArray[i]);
            }
            return sArray;
        }
        public static bool IsOK(this MMC_STAT result)
        {
            if (result == MMC_STAT.MMC_OK)
                return true;
            else
                return false;
        }
        public static bool IsError(this MMC_STAT result)
        {
            if (result != MMC_STAT.MMC_OK)
                return true;
            else
                return false;
        }
    }
}
