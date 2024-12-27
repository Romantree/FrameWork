using System;
using System.Collections.Generic;

namespace TS.FW.Device
{
    public interface IAxis
    {
        int No { get; }

        bool IsServoOn { get; set; }

        bool IsAlarm { get; }

        bool IsBusy { get; }

        bool HomeSensor { get; }

        bool LimitPlus { get; }

        bool LimitMinus { get; }

        double ActPosition { get; }

        double ComPosition { get; }

        double Speed { get; set; }

        double Accel { get; set; }

        double Decel { get; set; }

        Response ResetAlarm();

        Response ResetPosition();

        Response HomeAsync(out HomeAsyncResult result);

        Response MoveABS(double position);

        Response MoveABS(double position, double speed, double accel, double decel);

        Response MoveREL(double position);

        Response MoveREL(double position, double speed, double accel, double decel);

        Response MoveVEL(eDirection dir);

        Response MoveVEL(eDirection dir, double speed, double accel, double decel);

        Response SpeedChange(eDirection dir, double speed, double accel, double decel);

        Response Stop();

        Response EStop();
    }

    public interface IAxisModule : IEnumerable<IAxis>
    {
        IAxis this[int no] { get; }

        IAxis this[Enum type] { get; }

        Response InitAxis(IEnumerable<int> axisList);

        Response InitAxis(Type enumType);

        Response ServoOnOffAxis(bool isOnOff);

        Response ResetAlarmAxis();

        Response StopAxis();

        Response EStopAxis();

        void SetMultiData(Enum axis, double pos, double speed);

        void MoveMultiABS(params Enum[] list);

        void MoveMultiREL(params Enum[] list);

        void MoveMultiVEL(params Enum[] list);
    }

    internal class AxisMultiData
    {
        public double Pos;

        public double Speed;
    }
}
