using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TS.FW.Device.Ajin;

namespace TS.FW.Device.Simulation
{
    public class SimulationDevice : IDevice, IAxisModule, IDInOutModule, IAnalogModule
    {
        private readonly Dictionary<int, SimulationAxis> _axisList = new Dictionary<int, SimulationAxis>();
        private readonly SimulationDInOut _inOut = new SimulationDInOut();
        private readonly SimulationAnalog _analog = new SimulationAnalog();

        private readonly Dictionary<int, AxisMultiData> _multiData = new Dictionary<int, AxisMultiData>();

        public IAxis this[int no] => this.GetMotionAxis(no);

        public IAxis this[Enum type] => this.GetMotionAxis(Convert.ToInt32(type));

        public bool IsOpen { get; private set; }

        public bool IsDInOutModule => true;

        public bool IsIAnalogModule => true;

        public IDInOut IO => this.IsOpen ? this._inOut : null;

        public IAnalog Al => this.IsOpen ? this._analog : null;

        public Response Close()
        {
            try
            {
                this.IsOpen = false;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
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
                    this._axisList.Add(no, new SimulationAxis(no));
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
                    this._axisList.Add(no, new SimulationAxis(no));
                    this._multiData.Add(no, new AxisMultiData());
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response Open()
        {
            try
            {
                this.IsOpen = true;

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._axisList.Values.GetEnumerator();
        }

        private SimulationAxis GetMotionAxis(int no)
        {
            if (this.IsOpen == false) return null;

            if (this._axisList.ContainsKey(no) == false)
            {
                this._axisList.Add(no, new SimulationAxis(no));
            }

            return this._axisList[no];
        }

        public void MoveMultiABS(params Enum[] list)
        {
            foreach (var axis in list)
            {
                var item = this[axis];
                var data = _multiData[Convert.ToInt32(axis)];

                item.MoveABS(data.Pos, data.Speed, data.Speed * 4, data.Speed * 4);
            }
        }

        public void MoveMultiREL(params Enum[] list)
        {
            foreach (var axis in list)
            {
                var item = this[axis];
                var data = _multiData[Convert.ToInt32(axis)];

                item.MoveREL(data.Pos, data.Speed, data.Speed * 4, data.Speed * 4);
            }
        }

        public void MoveMultiVEL(params Enum[] list)
        {
            foreach (var axis in list)
            {
                var item = this[axis];
                var data = _multiData[Convert.ToInt32(axis)];

                item.MoveVEL(data.Pos > 0 ? eDirection.Plus : eDirection.Minus, data.Speed, data.Speed * 4, data.Speed * 4);
            }
        }

        public void SetMultiData(Enum axis, double pos, double speed)
        {
            var no = Convert.ToInt32(axis);

            var item = this[no] as SimulationAxis;
            var data = _multiData[no];

            data.Pos = pos;
            data.Speed = speed;
        }
    }
}
