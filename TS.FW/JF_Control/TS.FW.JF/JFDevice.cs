using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.FW.JF.Core;

namespace TS.FW.JF
{
    public class JFDevice : IEnumerable//, IEnumerable<JFAxis>
    {
        private readonly Dictionary<int, JFAxis> _axisList = new Dictionary<int, JFAxis>();

        public bool IsOpen { get; private set; } 

        public JFAxis this[int no] => this.GetMotionAxis(no);

        private JFAxis GetMotionAxis(int no)
        {
            if (IsOpen == false) return null;

            if (_axisList.ContainsKey(no) == false)
            {
                _axisList.Add(no, new JFAxis(no));
            }

            return _axisList[no];
        }

        public JFDevice()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }
        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in _axisList)
                {
                    item.Value.SaveMotionFile();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public MMC_STAT Open()
        {
            try
            {
                var ret = this.MotionBoardInit();
                //var ret2 = this.InOutBoardInit();              
                if (ret == 0) IsOpen = true;

                _axisList.Clear();
                return ret;
            }
            catch (Exception ex)
            {
                IsOpen = false;
                throw ex;
            }
        }
        public MMC_STAT Close()
        {
            // Lib None Close Function in JF Lib.

            return MMC_STAT.MMC_OK;
        }

        public bool StopAxis()
        {
            try
            {
                foreach (var item in this._axisList)
                {
                    var result = item.Value.Stop();
                    if (result == false) return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Aixs Emergency Stop.
        public bool EStopAxis()
        {
            try
            {
                var list = this._axisList.ToList();

                foreach (var item in list)
                {
                    var result = item.Value.EStop();
                    if (result == false) return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AlarmReset()
        {
            try
            {
                var list = this._axisList.ToList();

                foreach (var item in list)
                {
                    if (item.Value.IsEnable == false) continue;
                    var result = item.Value.ResetAlarm();
                    if (result == false) return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Servo On / Off Axis.
        public bool ServoOnOff(bool isOnOff)
        {
            try
            {
                foreach (var item in this._axisList)
                {
                    if (item.Value.IsEnable == false) continue;
                    item.Value.IsServoOn = isOnOff;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerator GetEnumerator()
        {
            return _axisList.Values.GetEnumerator();
        }

        public bool LoadParameter()
        {
            try
            {
                if (IsOpen == false) return false;

                foreach (var item in _axisList)
                {
                    item.Value.LoadMotionFile();
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
            return true;
        }

        public bool SetParameter()
        {
            try
            {
                if (IsOpen == false) return false;

                foreach (var item in _axisList.Values)
                {
                    if (item.IsEnable == false) continue;

                    //softlimit Set 
                    var posLimit = item.Setting.SoftwareLimit.PosLimitPos * item.Rstn;
                    item.SetPosLimit(posLimit, item.Setting.SoftwareLimit.PosLimitAct);
                    var negLimit = item.Setting.SoftwareLimit.NegLimitPos * item.Rstn;
                    item.SetNegLimit(negLimit, item.Setting.SoftwareLimit.NegLimitAct);

                    item.SaveMotionFile();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
            return true;
        }
    }
}
