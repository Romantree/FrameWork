using System;
using System.Collections.Generic;
using System.Threading;

namespace TS.FW.Robot.RC100
{
    internal class RobotCmdData
    {
        private readonly EventWaitHandle _wait = new EventWaitHandle(false, EventResetMode.ManualReset);

        public RobotCmdType Cmd { get; private set; }

        public RobotError Error { get; private set; } = RobotError.NONE;

        public bool IsEnd { get; set; } = true;

        public string DataMsg { get; private set; } = null;

        public RobotCmdData(RobotCmdType type)
        {
            this.Cmd = type;
        }

        public void SerError(string msg)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(msg)) return;
                if (int.TryParse(msg.Replace("*ERR:", ""), out int code) == false) return;

                this.Error = (RobotError)code;
            }
            finally
            {
                this.IsEnd = true;
                this.Set();
            }
        }

        public void SetData(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return;

            this.DataMsg = msg.Replace(string.Format("*{0},", this.Cmd), "");
        }

        public void Reset()
        {
            this.IsEnd = false;
            this.DataMsg = null;

            this.Error = RobotError.NONE;

            this._wait.Reset();
        }

        public bool Set() => this._wait.Set();

        public bool WaitOne() => this._wait.WaitOne();

        public bool WaitOne(int millisecondsTimeout) => this._wait.WaitOne(millisecondsTimeout);

        public static IEnumerable<RobotCmdData> ToRobotCmdData()
        {
            foreach (RobotCmdType item in Enum.GetValues(typeof(RobotCmdType)))
            {
                yield return new RobotCmdData(item);
            }
        }
    }
}
