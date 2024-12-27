using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TS.FW.Device.Ajin.Core;

namespace TS.FW.Device.Ajin
{
    public class AjinDevice : IDevice, IAxisModule, IDInOutModule, IAnalogModule
    {
        private readonly Dictionary<int, AjinAxis> _axisList = new Dictionary<int, AjinAxis>();
        private readonly AjinDInOut _io = new AjinDInOut();
        private readonly AjinAnalog _ai = new AjinAnalog();

        private readonly Dictionary<int, AxisMultiData> _multiData = new Dictionary<int, AxisMultiData>();

        public string MOT_FILE_PATH { get; private set; }

        public IAxis this[int no] => this.GetMotionAxis(no);

        public IAxis this[Enum type] => this.GetMotionAxis(Convert.ToInt32(type));

        public bool IsOpen => this.AxlIsOpened();

        public bool IsDInOutModule => this.AxdInfoIsDIOModule();

        public bool IsIAnalogModule => this.AxaInfoIsAIOModule();

        public IDInOut IO => this.IsOpen && this.IsDInOutModule ? this._io : null;

        public IAnalog Al => this.IsOpen && this.IsIAnalogModule ? this._ai : null;

        public AjinDevice()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        public Response Open()
        {
            return this.AxlOpenNoReset();
        }

        public Response Close()
        {
            return this.AxlClose();
        }

        public Response EStopAxis()
        {
            try
            {
                foreach (var item in this._axisList)
                {
                    var res = item.Value.EStop();
                    if (res == false) return res;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerator<IAxis> GetEnumerator()
        {
            return this._axisList.Values.GetEnumerator();
        }

        public Response InitAxis(IEnumerable<int> axisList)
        {
            try
            {
                this._axisList.Clear();

                foreach (var no in axisList)
                {
                    this._axisList.Add(no, new AjinAxis(no));
                    this._multiData.Add(no, new AxisMultiData());
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response InitAxis(Type enumType)
        {
            try
            {
                this._axisList.Clear();

                foreach (var item in Enum.GetValues(enumType))
                {
                    var no = Convert.ToInt32(item);
                    this._axisList.Add(no, new AjinAxis(no));
                    this._multiData.Add(no, new AxisMultiData());
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response ResetAlarmAxis()
        {
            try
            {
                foreach (var item in this._axisList)
                {
                    var res = item.Value.ResetAlarm();
                    if (res == false) return res;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response ServoOnOffAxis(bool isOnOff)
        {
            try
            {
                foreach (var item in this._axisList)
                {
                    item.Value.IsServoOn = isOnOff;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response StopAxis()
        {
            try
            {
                foreach (var item in this._axisList)
                {
                    var res = item.Value.Stop();
                    if (res == false) return res;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response LoadMotionFile(string filePath)
        {
            var res = this.AxmMotLoadParaAll(filePath);

            if (res == true)
            {
                MOT_FILE_PATH = filePath;
            }



            return res;
        }

        public Response SaveMotionFile(string filePath)
        {
            //filePath = this.MOT_FILE_PATH;
            var res = this.AxmMotSaveParaAll(filePath);

            return res;
        }

        public Response ChangeAxisNo(int no, int changeNo)
        {
            try
            {
                return this.AxmVirtualSetAxisNoMap(no, changeNo);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._axisList.Values.GetEnumerator();
        }

        private AjinAxis GetMotionAxis(int no)
        {
            if (this.IsOpen == false) return null;

            if (this._axisList.ContainsKey(no) == false)
            {
                this._axisList.Add(no, new AjinAxis(no));
            }

            return this._axisList[no];
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in this._axisList)
                {
                    item.Value.Stop();
                    item.Value.Setting.SaveIniMove();
                }

                this.SaveMotionFile(MOT_FILE_PATH);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void MoveMultiABS(params Enum[] list)
        {
            try
            {
                var noList = list.Select(t=> Convert.ToInt32(t)).OrderBy(t=>t).ToArray();

                var res = this.AxmMotSetAbsRelMode(noList, Lib.AXT_MOTION_ABSREL.POS_ABS_MODE);
                if (res == false)
                {
                    Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                    return;
                }

                this.AxmMoveStartMultiPos(noList);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void MoveMultiREL(params Enum[] list)
        {
            try
            {
                var noList = list.Select(t=> Convert.ToInt32(t)).OrderBy(t=>t).ToArray();

                var res = this.AxmMotSetAbsRelMode(noList, Lib.AXT_MOTION_ABSREL.POS_REL_MODE);
                if (res == false)
                {
                    Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                    return;
                }

                this.AxmMoveStartMultiPos(noList);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void MoveMultiVEL(params Enum[] list)
        {
            try
            {
                var noList = list.Select(t=> Convert.ToInt32(t)).OrderBy(t=>t).ToArray();

                this.AxmMoveStartMultiVel(noList);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetMultiData(Enum axis, double pos, double speed)
        {
            var no = Convert.ToInt32(axis);

            var item = this[no] as AjinAxis;
            var data = _multiData[no];

            data.Pos = pos / item.Setting.SCALE;
            data.Speed = speed / item.Setting.SCALE;
        }

        internal void GetMultiData(int[] axis, out double[] pos, out double[] speed, out double[] accel, out double[] decel)
        {
            pos = new double[axis.Length];
            speed = new double[axis.Length];
            accel = new double[axis.Length];
            decel = new double[axis.Length];

            var i = 0;

            foreach (var no in axis)
            {
                var data = _multiData[no];

                pos[i] = data.Pos;
                speed[i] = data.Speed;
                accel[i] = data.Speed * 4;
                decel[i] = data.Speed * 4;

                i++;
            }
        }

        internal void GetMultiDataVEL(int[] axis, out double[] speed, out double[] accel, out double[] decel)
        {
            speed = new double[axis.Length];
            accel = new double[axis.Length];
            decel = new double[axis.Length];

            var i = 0;

            foreach (var no in axis)
            {
                var data = _multiData[no];

                speed[i] = data.Speed * (data.Pos > 0 ? 1 : -1);
                accel[i] = data.Speed * 4;
                decel[i] = data.Speed * 4;

                i++;
            }
        }
    }
}
