using System;
using System.Collections.Generic;
using System.Linq;

namespace TS.FW.Robot.RC100.State
{
    public class RobotState
    {
        /// <summary>
        /// User Command 를  처리할  수  있는  프로그램의  실행여부 
        /// [1] Program Run, [0] Program Stop 
        /// </summary>
        public bool ProgramRun { get; internal set; }

        /// <summary>
        /// 제어기의  알람  발생  여부 
        /// [1]  정상상태, [0]  알람상태 
        /// </summary>
        public bool Alarm { get; internal set; }

        /// <summary>
        /// Robot 의  모터  전원  투입  여부 
        /// [1] Servo ON, [0] Servo OFF 
        /// </summary>
        public bool ServoON { get; internal set; }

        /// <summary>
        /// Remote  모드  여부 
        /// [1] Remote Mode, [0] Auto or Teach Mode 
        /// </summary>
        public bool RemoteMode { get; internal set; }

        /// <summary>
        /// 제어기의  구동전원  투입  및  모든  축의  Origin  완료  여부 
        /// [1] Ready, [0] Not Ready 
        /// </summary>
        public bool RobotReady { get; internal set; }

        /// <summary>
        /// User Command  처리  가능여부 
        /// [1] Command Ready, [0] Command Busy 
        /// </summary>
        public bool CmdReady { get; internal set; }

        /// <summary>
        /// Robot 의  이동  상태  여부 
        /// [1] Robot 이동중, [0] Robot  정지중 
        /// </summary>
        public bool IsBusy { get; internal set; }

        /// <summary>
        /// R1 축의  Home  위치  여부  Lower Arm
        /// [1] R1 Home  위치인  경우, [0] R1 Home  위치가  아닌  경우
        /// </summary>
        public bool ArmFoldR1 { get; internal set; }

        /// <summary>
        /// R1 축의  Vacuum  또는  Gripper  센서  입력여부 Lower Arm
        /// [1] R1 Vacuum ON, [0] R1 Vacuum OFF 
        /// </summary>
        public bool ArmDetectR1 { get; internal set; }

        /// <summary>
        /// R2 축의  Home  위치  여부 Upper Arm
        /// [1] R2 Home  위치인  경우, [0] R2 Home  위치가  아닌  경우
        /// </summary>
        public bool ArmFoldR2 { get; internal set; }

        /// <summary>
        /// R2 축의  Vacuum  또는  Gripper  센서  입력여부 Upper Arm
        /// [1] R2 Vacuum ON, [0] R2 Vacuum OFF 
        /// </summary>
        public bool ArmDetectR2 { get; internal set; }

        /// <summary>
        /// 제어기  FAN  고장  여부 
        /// [1]  정상  동작  [0]  고장  경고 
        /// </summary>
        public bool FanWarning { get; internal set; }

        /// <summary>
        /// 모터  Encoder Battery  저전압  여부 
        /// [1]  정상  동작  [0]  저전압  경고 
        /// </summary>
        public bool BatWarning { get; internal set; }

        public bool StateCheck(RobotStateCheck state)
        {
            if (this.RemoteMode == false) return true;

            if (state.HasFlag(RobotStateCheck.Alarm) && (this.Alarm)) return true;
            //if (state.HasFlag(RobotStateCheck.Alarm) && (this.Alarm || this.BatWarning || this.FanWarning)) return true;
            //if (state.HasFlag(RobotStateCheck.ProgramRun) && (this.ProgramRun || this.IsBusy)) return true;
            if (state.HasFlag(RobotStateCheck.RobotReady) && this.RobotReady == false) return true;
            if (state.HasFlag(RobotStateCheck.CmdReady) && this.CmdReady == false) return true;

            if (state.HasFlag(RobotStateCheck.ArmDetectOnR1) && this.ArmDetectR1 == false) return true;
            if (state.HasFlag(RobotStateCheck.ArmDetectOffR1) && this.ArmDetectR1 == true) return true;

            if (state.HasFlag(RobotStateCheck.ArmDetectOnR2) && this.ArmDetectR2 == false) return true;
            if (state.HasFlag(RobotStateCheck.ArmDetectOffR2) && this.ArmDetectR2 == true) return true;

            return false;
        }

        public string ToUserRobotMoveCmd()
        {
            var list = new List<string>();

            if (this.Alarm) list.Add("Alarm");
            if (this.RobotReady == false) list.Add("RobotReady");
            if (this.CmdReady == false) list.Add("CmdReady");

            return string.Join(", ", list);
        }

        internal void SetData(string msg)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(msg)) return;


                //var recv = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var token = msg.Replace(",","").Select(t => t.ToString()).ToArray();//.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                //var token = msg.Select(t => t.ToString()).ToArray();//.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (token.Length < 13) return;

                this.ProgramRun = token[0] == "1";
                this.Alarm = token[1] == "0";
                this.ServoON = token[2] == "1";
                this.RemoteMode = token[3] == "1";
                this.RobotReady = token[4] == "1";
                this.CmdReady = token[5] == "1";
                this.IsBusy = token[6] == "1";
                this.ArmFoldR1 = token[7] == "1";
                this.ArmDetectR1 = token[8] == "1";
                this.ArmFoldR2 = token[9] == "1";
                this.ArmDetectR2 = token[10] == "1";
                this.FanWarning = token[11] == "1";
                this.BatWarning = token[12] == "1";
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        internal string GetData()
        {
            var list = new List<string>();

            foreach (var info in this.GetType().GetProperties())
            {
                var value = (bool)info.GetValue(this);

                list.Add(value ? "1" : "0");
            }

            return string.Join("", list);
        }
    }
}
