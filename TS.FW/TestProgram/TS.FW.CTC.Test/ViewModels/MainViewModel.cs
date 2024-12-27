using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TS.FW.CTC.Test.Dto;
using TS.FW.CTC.Test.Managers;
using TS.FW.CTC.Test.Models;
using TS.FW.CTC.Test.Process;
using TS.FW.CTC.Test.Process.Robot;
using TS.FW.CTC.Test.Process.Robot.Item;
using TS.FW.CTC.Test.Process.Work;
using TS.FW.Dac.Core;
using TS.FW.Dac.Transaction;
using TS.FW.Diagnostics;
using TS.FW.Robot.RC100;
using TS.FW.Robot.RC100.Data;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;

namespace TS.FW.CTC.Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const double ROBOT_ARM_DEF = 10;

        private readonly BackgroundTimer _trUpdate = new BackgroundTimer();
        private readonly ScheduleManager _proc = new ScheduleManager();

        public RobotMoveModel RobotMoveData { get; set; }

        public bool IsPortWafer { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsAlignWafer { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsCenteringWafer { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsCotterWafer { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public int WaferNo { get => this.GetValue<int>(); set => this.SetValue(value); }

        public int Delay { get => this.GetValue<int>(); set => this.SetValue(value); }

        public bool IsProtMap1 { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsProtMap2 { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsProtMap3 { get => this.GetValue<bool>(); set => this.SetValue(value); }

        /// <summary>
        /// 로봇 팔 우선순위 옵션
        /// </summary>
        public bool RobotArmPriority { get => this.GetValue<bool>(); set => this.SetValue(value); }

        /// <summary>
        /// 로봇 팔 1개만 사용
        /// </summary>
        public RobotHeadMode RobotHeadMode { get => this.GetValue<RobotHeadMode>(); set => this.SetValue(value); }

        public List<RobotHeadMode> RobotHeadModeList { get; set; }

        /// <summary>
        /// Slot 작업 순서 반전
        /// </summary>
        public bool IsSlotNoDesc { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public ObservableCollection<RobotHistory> History { get; set; } = new ObservableCollection<RobotHistory>();

        public RobotSimulation server = new RobotSimulation();
        public RobotClient client = new RobotClient();

        private void RobotTest()
        {
            server.Open("127.0.0.1", 3000);
            client.Open("127.0.0.1", 3000);

            System.Threading.Thread.Sleep(1000);

            var res = client.Initial();
            if (res == false) return;

            while (client.IsComplete(RobotCmdType.INITIAL) == false)
            {
                System.Threading.Thread.Sleep(100);
            }

            System.Threading.Thread.Sleep(1000);

            var result = new MappingResult();
            var res1 = client.MappingStart(1, WaferSize.Inch_8, ref result);
            if (res1 == false) return;

            while (client.IsComplete(RobotCmdType.MAPSCAN) == false || result.Complete == false)
            {
                System.Threading.Thread.Sleep(100);
            }

            var waferList = result.Wafers;
        }

        public MainViewModel()
        {
            MainViewModel.SourceLevels = System.Diagnostics.SourceLevels.Critical;
            Logger.LogLevel = Logger.LogEventLevel.Information;

            //this.RobotTest();

            this.RobotHeadModeList = ((IEnumerable<RobotHeadMode>)(Enum.GetValues(typeof(RobotHeadMode)))).ToList();

            if (this.IsDesignMode) return;

            var data = LotRecipe.ToTest();
            var model = (LotRecipeModel)data;
            var data2 = (LotRecipe)model;

            var josn = TS.FW.Serialization.Serialization.JsonSerializer(data2);
            var data3 = TS.FW.Serialization.Serialization.JsonDeserializer<LotRecipe>(josn.Result);

            this.RobotMoveData = this.MainWindow.Resources["xRobotMove"] as RobotMoveModel;

            this.RobotMoveData.RobotArmPos = ROBOT_ARM_DEF;
            this.RobotMoveData.RobotTopArmPos = 10;
            this.RobotMoveData.RobotBottomArmPos = 10;

            this.MainWindow.Closed += MainWindow_Closed;

            this.IsProtMap1 = false;
            this.IsProtMap2 = false;
            this.IsProtMap3 = false;

            this.WaferNo = 1;
            this.Delay = 100;
            this.IsPortWafer = true;

            this._trUpdate.SleepTimeMsc = 10;
            this._trUpdate.DoWork += _trUpdate_DoWork;
            this._trUpdate.Start();

            this._proc.OnRobotHistoryEvent += _sc_OnRobotHistoryEvent;
            _proc.OnPortMappingStartEvent += _proc_OnPortMappingStartEvent;
            _proc.OnPortMappingCompleteEvent += _proc_OnPortMappingCompleteEvent;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            TS.FW.Diagnostics.BackgroundTimer.AllStop();
        }

        private void _sc_OnRobotHistoryEvent(object sender, RobotHistory e)
        {
            try
            {
                var robot = sender as RobotProcess;

                this.Move(e);

                this.Dispatcher.Invoke(() =>
                {
                    this.History.Add(e);
                });
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _proc.Robot1.RobotArmPriority = this.RobotArmPriority ? RobotArmType.TOP : RobotArmType.BOTTOM;
            _proc.Robot1.RobotHeadMode = this.RobotHeadMode;
            _proc.Robot1.IsSlotNoDesc = this.IsSlotNoDesc;
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "T0-0":
                        {
                            ListMsgBox.Show("Alarm", $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 일자 테스트 메세지");

                            //this.History.Clear();

                            //IWorkProcess.UnitTestDelay = this.Delay;
                            //RobotProcess.TestWaferCount = this.WaferNo;

                            //var test = LotRecipe.ToTest();
                            //_proc.Start(1, 0, test);
                        }
                        break;
                    case "T0-1":
                        {
                            ListMsgBox.Show("Msg", $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 일자 테스트 메세지");

                            //this.History.Clear();

                            //IWorkProcess.UnitTestDelay = this.Delay;
                            //RobotProcess.TestWaferCount = this.WaferNo;

                            //var test = LotRecipe.ToTest();
                            //_proc.Start(1, 0, test);

                            //_proc.Start(1, 1, test);

                            //_proc.Start(1, 2, test);
                        }
                        break;
                    case "T0-2":
                        {
                            ListMsgBox.Show("Alarm");

                            //this.History.Clear();

                            //IWorkProcess.UnitTestDelay = this.Delay;
                            //RobotProcess.TestWaferCount = this.WaferNo;

                            //var test = LotRecipe.ToTest();
                            //_proc.Start(1, 0, test);

                            //_proc.Start(1, 2, test);
                        }
                        break;
                    case "T0-3":
                        {
                            ListMsgBox.Show("Msg");

                            //this.History.Clear();

                            //IWorkProcess.UnitTestDelay = this.Delay;
                            //RobotProcess.TestWaferCount = this.WaferNo;

                            //var test = LotRecipe.ToTest();
                            //_proc.Start(1, 0, test);

                            //_proc.Start(1, 1, test);

                            //_proc.Start(1, 2, test);

                            //_proc.Start(1, 0, test);
                        }
                        break;
                    case "T0-4":
                        {
                            _proc.AllStop();
                        }
                        break;
                    case "T0-5":
                        {
                            _proc.Robot1.CleanStart();
                        }
                        break;
                    case "T1":
                        {
                            this.History.Clear();

                            IWorkProcess.UnitTestDelay = this.Delay;
                            RobotProcess.TestWaferCount = this.WaferNo;

                            //_proc.MappingStart(0);
                        }
                        break;
                    case "T2-1":
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var item = new RobotHistory()
                                {
                                    RobotArm = RobotArmType.TOP,
                                    Action = RobotAction.GET,
                                    Move = RobotMove.Port,
                                    PortNo = 1,
                                };

                                this.Move(item, false);
                            });

                        }
                        break;
                    case "T2-2":
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var item = new RobotHistory()
                                {
                                    RobotArm = RobotArmType.TOP,
                                    Action = RobotAction.GET,
                                    Move = RobotMove.Port,
                                    PortNo = 2,
                                };

                                this.Move(item, false);
                            });

                        }
                        break;
                    case "T2-3":
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var item = new RobotHistory()
                                {
                                    RobotArm = RobotArmType.TOP,
                                    Action = RobotAction.GET,
                                    Move = RobotMove.Port,
                                    PortNo = 3,
                                };

                                this.Move(item, false);
                            });

                        }
                        break;
                    case "T3":
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var item = new RobotHistory()
                                {
                                    RobotArm = RobotArmType.TOP,
                                    Action = RobotAction.GET,
                                    Move = RobotMove.Align,
                                };

                                this.Move(item, false);
                            });

                        }
                        break;
                    case "T4":
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var item = new RobotHistory()
                                {
                                    RobotArm = RobotArmType.TOP,
                                    Action = RobotAction.GET,
                                    Move = RobotMove.Centering,
                                };

                                this.Move(item, false);
                            });

                        }
                        break;
                    case "T5":
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var item = new RobotHistory()
                                {
                                    RobotArm = RobotArmType.TOP,
                                    Action = RobotAction.GET,
                                    Move = RobotMove.Cotter,
                                };

                                this.Move(item, false);
                            });

                        }
                        break;
                    case "T6":
                        {
                            var log = string.Join(Environment.NewLine, this.History.Select(t => string.Format("[{3}:{0}] : {2} - {1}", t.SlotNo, t.RobotArm, t.Move, t.Action)));

                            Clipboard.SetText(log);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _proc_OnPortMappingStartEvent(object sender, PortData e)
        {
            var robot = sender as RobotProcess;

            switch (e.PortNo)
            {
                case 0:
                    {
                        this.IsProtMap1 = true;
                    }
                    break;
                case 1:
                    {
                        this.IsProtMap2 = true;
                    }
                    break;
                case 2:
                    {
                        this.IsProtMap3 = true;
                    }
                    break;
            }
        }

        private void _proc_OnPortMappingCompleteEvent(object sender, PortData e)
        {
            switch (e.PortNo)
            {
                case 0:
                    {
                        this.IsProtMap1 = false;
                    }
                    break;
                case 1:
                    {
                        this.IsProtMap2 = false;
                    }
                    break;
                case 2:
                    {
                        this.IsProtMap3 = false;
                    }
                    break;
            }
        }

        private void Move(RobotHistory item, bool IsM = true)
        {
            try
            {
                var delay = 5;

                if (IsM == false)
                {
                    RobotControl.Move(this.RobotMoveData, RobotMoveType.BottomArm, 10, delay);
                    RobotControl.Move(this.RobotMoveData, RobotMoveType.TopArm, 10, delay);
                    RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 20, delay);
                }

                switch (item.Move)
                {
                    case RobotMove.Port:
                        {
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Y, 40, delay);

                            if (item.PortNo == 1)
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.Angle, 31, delay);
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 42, delay);
                            }
                            else if (item.PortNo == 2)
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.Angle, 3, delay);
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 42, delay);
                            }
                            else
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.Angle, -26, delay);
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 57, delay);
                            }
                        }
                        break;
                    case RobotMove.Align:
                        {
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Y, -75, delay);
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Angle, -90, delay);
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 45, delay);
                        }
                        break;
                    case RobotMove.Centering:
                        {
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Y, 75, delay);
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Angle, -90, delay);
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 45, delay);
                        }
                        break;
                    case RobotMove.Cotter:
                        {
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Y, 60, delay);
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Angle, -51, delay);
                            RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 80, delay);
                        }
                        break;
                }

                if (item.RobotArm == RobotArmType.TOP)
                {
                    switch (item.Move)
                    {
                        case RobotMove.Port:
                            {
                                if (item.PortNo == 1)
                                {
                                    RobotControl.Move(this.RobotMoveData, RobotMoveType.TopArm, 65, delay);
                                }
                                else
                                {
                                    RobotControl.Move(this.RobotMoveData, RobotMoveType.TopArm, 40, delay);
                                }
                            }
                            break;
                        case RobotMove.Align:
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.TopArm, 66, delay);
                            }
                            break;
                        case RobotMove.Centering:
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.TopArm, 66, delay);
                            }
                            break;
                        case RobotMove.Cotter:
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.TopArm, 80, delay);
                            }
                            break;
                    }

                    if (item.Action == RobotAction.GET)
                    {
                        this.RobotMoveData.TopWaferVisibility = Visibility.Visible;

                        switch (item.Move)
                        {
                            case RobotMove.Port:
                                {

                                }
                                break;
                            case RobotMove.Align:
                                {
                                    this.IsAlignWafer = false;
                                }
                                break;
                            case RobotMove.Centering:
                                {
                                    this.IsCenteringWafer = false;
                                }
                                break;
                            case RobotMove.Cotter:
                                {
                                    this.IsCotterWafer = false;
                                }
                                break;
                        }
                    }
                    else
                    {
                        this.RobotMoveData.TopWaferVisibility = Visibility.Hidden;

                        switch (item.Move)
                        {
                            case RobotMove.Port:
                                {

                                }
                                break;
                            case RobotMove.Align:
                                {
                                    this.IsAlignWafer = true;
                                }
                                break;
                            case RobotMove.Centering:
                                {
                                    this.IsCenteringWafer = true;
                                }
                                break;
                            case RobotMove.Cotter:
                                {
                                    this.IsCotterWafer = true;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    switch (item.Move)
                    {
                        case RobotMove.Port:
                            {
                                if (item.PortNo == 1)
                                {
                                    RobotControl.Move(this.RobotMoveData, RobotMoveType.BottomArm, 65, delay);
                                }
                                else
                                {
                                    RobotControl.Move(this.RobotMoveData, RobotMoveType.BottomArm, 40, delay);
                                }
                            }
                            break;
                        case RobotMove.Align:
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.BottomArm, 66, delay);
                            }
                            break;
                        case RobotMove.Centering:
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.BottomArm, 66, delay);
                            }
                            break;
                        case RobotMove.Cotter:
                            {
                                RobotControl.Move(this.RobotMoveData, RobotMoveType.BottomArm, 80, delay);
                            }
                            break;
                    }

                    if (item.Action == RobotAction.GET)
                    {
                        this.RobotMoveData.BottomWaferVisibility = Visibility.Visible;

                        switch (item.Move)
                        {
                            case RobotMove.Port:
                                {

                                }
                                break;
                            case RobotMove.Align:
                                {
                                    this.IsAlignWafer = false;
                                }
                                break;
                            case RobotMove.Centering:
                                {
                                    this.IsCenteringWafer = false;
                                }
                                break;
                            case RobotMove.Cotter:
                                {
                                    this.IsCotterWafer = false;
                                }
                                break;
                        }
                    }
                    else
                    {
                        this.RobotMoveData.BottomWaferVisibility = Visibility.Hidden;

                        switch (item.Move)
                        {
                            case RobotMove.Port:
                                {

                                }
                                break;
                            case RobotMove.Align:
                                {
                                    this.IsAlignWafer = true;
                                }
                                break;
                            case RobotMove.Centering:
                                {
                                    this.IsCenteringWafer = true;
                                }
                                break;
                            case RobotMove.Cotter:
                                {
                                    this.IsCotterWafer = true;
                                }
                                break;
                        }
                    }
                }

                if (IsM)
                {
                    RobotControl.Move(this.RobotMoveData, RobotMoveType.BottomArm, 10, delay);
                    RobotControl.Move(this.RobotMoveData, RobotMoveType.TopArm, 10, delay);
                    RobotControl.Move(this.RobotMoveData, RobotMoveType.Arm, 20, delay);
                }

                //this.Move(1, 0);
                //this.Move(0, 0);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class TestRTx : IBizRTxBase
    {
        public void T2(string name, string value, DateTime logTime, double value2)
        {
            try
            {
                //using (var dac = new Test())
                //{
                //    for (int i = 0; i < 1000; i++)
                //    {
                //        dac.T2(name, value + (i + 1).ToString(), logTime.AddMinutes(2), value2);
                //    }
                //}

                //this.SetAbort();
            }
            catch (Exception ex)
            {
                this.SetAbort();
                Logger.Write(this, ex);
            }
        }
    }

    //public class Test : IDacBase<TestDao>
    //{
    //    public Test() : base("LOCAL_TEST")
    //    {

    //    }

    //    public void T(string name, string value)
    //    {
    //        var param = this.CreateParameter();

    //        param["@Name"].Value = name;
    //        param["@Value"].Value = value;

    //        var abcd = this.ExecuteQuery<TestDao>("SP_SELECT_TEST", param);
    //    }

    //    public void T2(string name, string value, DateTime logTime, double value2)
    //    {
    //        var param = this.CreateParameter();

    //        param["@Name"].Value = name;
    //        param["@Value"].Value = value;
    //        param["@LogTime"].Value = logTime;
    //        param["@Value2"].Value = value2;

    //        var a = this.ExecuteCommand("SP_INSERT_TEST", param);
    //    }
    //}

    //[Table(Name = "TbData")]
    //public class TestDao
    //{
    //    [Column]
    //    public int No { get; set; }

    //    [Column]
    //    public string Name { get; set; }

    //    [Column]
    //    public string Value { get; set; }

    //    [Column]
    //    public DateTime LogTime { get; set; }

    //    [Column]
    //    public double Value2 { get; set; }
    //}
}
