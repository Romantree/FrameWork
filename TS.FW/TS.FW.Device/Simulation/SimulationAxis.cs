using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using TS.FW.Helper;

namespace TS.FW.Device.Simulation
{
    public class SimulationAxis : IAxis
    {
        private const int SLEEP_TIME = 100;

        public static double LIMIT_POSITION_PLUS = 100000000000;
        public static double LIMIT_POSITION_MINUS = -100000000000;

        private bool _isHomeResult = false;
        private BackgroundWorker _work = new BackgroundWorker();

        public int No { get; private set; }

        public bool IsServoOn { get; set; } = true;

        public bool IsAlarm { get; private set; }

        public bool IsBusy => this._work != null ? this._work.IsBusy : false;

        public bool HomeSensor => this.ActPosition == 0;

        public bool LimitPlus => this.ActPosition == LIMIT_POSITION_PLUS;

        public bool LimitMinus => this.ActPosition == LIMIT_POSITION_MINUS;

        public double ActPosition { get; private set; }

        public double ComPosition { get; private set; }

        public double LoadRatio { get; private set; }

        public double Speed { get; set; }

        public double Accel { get; set; }

        public double Decel { get; set; }

        public SimulationAxis(int no)
        {
            this.No = no;
            this._work = new BackgroundWorker();
            this._work.WorkerSupportsCancellation = true;
            this._work.DoWork += _work_DoWork;
        }

        public Response EStop()
        {
            try
            {
                if (this._work != null && this._work.IsBusy)
                {
                    this._work.CancelAsync();
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private Task _tkHome = null;
        private bool IsStop = false;

        public Response HomeAsync(out HomeAsyncResult result)
        {
            result = new HomeAsyncResult() { Success = false };

            try
            {
                if (this._tkHome != null && this._tkHome.IsCompleted == false)
                {
                    result.Success = false;
                    result.Comment = "Home 진행 중 정지 후 다시 진행 해주세요.";
                    result.Complete = true;

                    this.IsStop = true;

                    return new Response(false, "Home 진행 중 정지 후 다시 진행 해주세요.");
                }

                this.IsStop = false;

                this._tkHome = Task.Factory.StartNew(Home_DoWork, result, TaskCreationOptions.None);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response MoveABS(double position)
        {
            try
            {
                this.ComPosition = Math.Round(position, 3);
                this.RunWorkerAsync("Move");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response MoveABS(double position, double speed, double accel, double decel)
        {
            try
            {
                this.ComPosition = Math.Round(position, 3);
                this.Speed = speed;
                this.Accel = accel;
                this.Decel = decel;

                this.RunWorkerAsync("Move");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response MoveREL(double position)
        {
            try
            {
                this.ComPosition += Math.Round(position, 3);
                this.RunWorkerAsync("Move");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response MoveREL(double position, double speed, double accel, double decel)
        {
            try
            {
                this.ComPosition += Math.Round(position, 3);
                this.Speed = speed;
                this.Accel = accel;
                this.Decel = decel;

                this.RunWorkerAsync("Move");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response MoveVEL(eDirection dir)
        {
            try
            {
                this.RunWorkerAsync(dir.ToString());

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response MoveVEL(eDirection dir, double speed, double accel, double decel)
        {
            try
            {
                this.Speed = speed;
                this.Accel = accel;
                this.Decel = decel;

                this.RunWorkerAsync(dir.ToString());

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response SpeedChange(eDirection dir, double speed, double accel, double decel)
        {
            try
            {
                this.Speed = speed;
                this.Accel = accel;
                this.Decel = decel;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response ResetAlarm()
        {
            try
            {
                this.IsAlarm = false;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response ResetPosition()
        {
            try
            {
                this.ActPosition = 0;
                this.ComPosition = 0;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response Stop()
        {
            try
            {
                if (this._work != null && this._work.IsBusy)
                {
                    this._work.CancelAsync();
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private void RunWorkerAsync(object state)
        {
            if (this.IsBusy)
            {
                this._work.CancelAsync();
                Thread.Sleep(100);
            }

            this._work.RunWorkerAsync(state);
        }

        private void _work_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.IsServoOn == false || this.IsAlarm) return;

                var command = e.Argument as string;

                while (this._work.CancellationPending == false)
                {
                    try
                    {
                        switch (command)
                        {
                            case "Home":
                                {
                                    if (this.HomeSensor == true)
                                    {
                                        this.ComPosition = 0;
                                        this._isHomeResult = true;
                                        return;
                                    }
                                    else
                                    {
                                        this.ActPosition = 0;
                                    }
                                }
                                break;
                            case "Move":
                                {
                                    if (this.ActPosition.CheckPosition(this.ComPosition) || this.LimitPlus || this.LimitMinus)
                                    {
                                        this.ActPosition = this.ComPosition;
                                        return;
                                    }
                                    else if (this.ActPosition > this.ComPosition)
                                    {
                                        this.ActPosition = this.SetPos(this.ActPosition, this.Speed, eDirection.Minus);
                                    }
                                    else if (this.ActPosition < this.ComPosition)
                                    {
                                        this.ActPosition = this.SetPos(this.ActPosition, this.Speed, eDirection.Plus);
                                    }
                                }
                                break;
                            case "Plus":
                                {
                                    if (this.LimitPlus)
                                    {
                                        return;
                                    }

                                    this.ComPosition++;
                                    this.ActPosition++;
                                }
                                break;
                            case "Minus":
                                {
                                    if (this.LimitMinus)
                                    {
                                        return;
                                    }

                                    this.ComPosition--;
                                    this.ActPosition--;
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(this, ex);
                    }
                    finally
                    {
                        Thread.Sleep(SLEEP_TIME);
                    }
                }
            }
            finally
            {
                e.Cancel = true;
            }
        }

        private double SetPos(double pos, double speed, eDirection dir)
        {
            var movePos = (speed * 0.1);

            if (dir == eDirection.Plus)
            {
                var temp = Math.Round(pos + movePos, 2);

                if (temp > this.ComPosition) return this.ComPosition;

                return temp;
            }
            else
            {
                var temp = Math.Round(pos - movePos, 2);

                if (temp < this.ComPosition) return this.ComPosition;

                return temp;
            }
        }

        private void Home_DoWork(object state)
        {
            var item = state as HomeAsyncResult;

            try
            {
                this._isHomeResult = false;
                this._work.RunWorkerAsync("Home");

                Thread.Sleep(1 * 1000);

                do
                {
                    if (item.IsStop)
                    {
                        this._work.CancelAsync();
                        throw new Exception("Home Stop 신호 감지.");
                    }

                    if (this.IsStop == true) return;

                    Thread.Sleep(10);

                } while (this._isHomeResult == false);

                item.Success = true;
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
    }
}
