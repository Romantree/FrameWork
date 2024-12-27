using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Test.Managers;
using TS.FW.Plc.Test.Models;
using TS.FW.Plc.Test.Views.Page;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Helper;

namespace TS.FW.Plc.Test.ViewModels.Page
{
    public class MitsubishiPlcViewModel : IMainMenuViewModel
    {
        private readonly float M_BYTE = 1024 * 1024;

        private readonly MitsubishiPlcView _view = new MitsubishiPlcView();

        private readonly PerformanceCounter _cpu;
        private readonly PerformanceCounter _memory;

        private readonly List<int> _cpuList = new List<int>();
        private readonly List<float> _memoryList = new List<float>();

        private readonly BackgroundTimer _trLogUpdate = new BackgroundTimer();

        private int _countInterval = 0;

        private Process CurProcess => Process.GetCurrentProcess();

        public override int No => 0;

        public override string Name => "미쯔비시 - PLC (E)";

        public override string Title => "Mitsubishi";

        public override string SubTitle => "PLC Ethernet";

        public override ContentControl View => _view;

        public override Visual Visual => ResourceHelper.ToVisual("appbar_scrabble_e", "Resources/Icons.xaml");

        public bool Connected { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public DateTime StartTime { get => this.GetValue<DateTime>(); set => this.SetValue(value); }

        public TimeSpan RunTime { get => this.GetValue<TimeSpan>(); set => this.SetValue(value); }

        public double CPU { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double Memory { get => this.GetValue<double>(); set => this.SetValue(value); }

        public MEtherneConfig Config { get => this.GetValue<MEtherneConfig>(); set => this.SetValue(value); }

        public List<IMEtherneMenuViewModel> MenuList { get; set; } = new List<IMEtherneMenuViewModel>();

        public IMEtherneMenuViewModel SelectedMenu { get => this.GetValue<IMEtherneMenuViewModel>(); set => this.SetValue(value); }

        public string LogMessage { get => this.GetValue<string>(); set => this.SetValue(value); }
        private readonly List<string> _logMsg = new List<string>();

        private PlcEthernetConfig _config = null;
        private string iniFilePath = string.Empty;

        public bool IsIniFileCheck { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public double ReadBitTime { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double ReadWordTime { get => this.GetValue<double>(); set => this.SetValue(value); }

        public MitsubishiPlcViewModel()
        {
            PlcManager.Ins.OnConnectedChanged += Ins_OnConnectedChanged;

            this._cpu = new PerformanceCounter("Process", "% Processor Time", this.CurProcess.ProcessName);
            this._memory = new PerformanceCounter("Process", "Working Set", this.CurProcess.ProcessName);
        }

        private void Ins_OnConnectedChanged(object sender, IPlcConfigBase e)
        {
            try
            {
                if (this._config == null) return;
                if (e.PlcNo != this._config.PlcNo) return;

                if(PlcManager.Ins.IsConnected(e))
                {
                    foreach (var item in this.MenuList)
                    {
                        item.UpdateConfig(this._config);
                    }

                    this.LogWrite("통신 연결");
                }
                else
                {
                    foreach (var item in this.MenuList)
                    {
                        item.UpdateConfig(null);
                    }

                    this.LogWrite("통신 연결 해제");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void LogWrite(string message)
        {
            Logger.Write(this, message);

            lock (this._logMsg)
            {
                if (this._logMsg.Count >= 100) this._logMsg.RemoveAt(this._logMsg.Count - 1);

                this._logMsg.Insert(0, string.Format("[{0}] : {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), message));
                this.LogMessage = string.Join(Environment.NewLine, this._logMsg);
            }
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "START":
                        {
                            if (this._config != null) PlcManager.Ins.Stop(this._config);

                            this._config = new PlcEthernetConfig(this.Config.PlcNo, this.iniFilePath)
                            {
                                NetworkNo = this.Config.NetworkNo,
                                StationNo = this.Config.StationNo,
                            };

                            this._config.SetNetworkInfo(this.Config.IpAddress, this.Config.Port);

                            var res = this._config.Init();
                            if (res == false)
                            {
                                MsgBox.ShowMsg(res.Comment);
                                return;
                            }

                            PlcManager.Ins.Start(this._config);

                            this.StartTime = DateTime.Now;
                        }
                        break;
                    case "STOP":
                        {
                            if (this._config != null) PlcManager.Ins.Stop(this._config);

                            this._config = null;
                        }
                        break;
                    case "INI_OPEN":
                        {
                            var dig = new OpenFileDialog()
                            {
                                Multiselect = false,
                                Filter = "*.ini|*.ini",
                            };

                            if (dig.ShowDialog() == false) return;

                            this.iniFilePath = dig.FileName;
                        }
                        break;
                    case "SV_OPEN":
                        {

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                MsgBox.ShowMsg(ex.Message);
            }
        }

        public override void Init()
        {
            try
            {
                base.Init();

                this._trLogUpdate.SleepTimeMsc = 1 * (60 * 1000);
                this._trLogUpdate.DoWork += _trLogUpdate_DoWork;
                this._trLogUpdate.Stop();

                this.Config = new MEtherneConfig()
                {
                    IpAddress = "111.111.111.112",
                    Port = 2000,
                    PlcNo = 1,
                    NetworkNo = 3,
                    StationNo = 1,
                };

                foreach (var item in IMenuViewModel.ToMenuViewModelList<IMEtherneMenuViewModel>())
                {
                    item.Init();
                    this.MenuList.Add(item);
                }

                this.SelectedMenu = this.MenuList.First() as IMEtherneMenuViewModel;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _trLogUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (this.IsSelect == false) return;

                this.LogWrite(string.Format("CPU : {0:f1}%, 메모리 : {1:f1}Mb, Thread : {2} 가동시간 : {3:dd\\.hh\\:mm\\:ss}", this.CPU, this.Memory, this.CurProcess.Threads.Count, this.RunTime));
                this.LogWrite(string.Format("Read Bit Time : {0:f3} Sec, Read Word Time : {1:f3} Sec", this.ReadBitTime, this.ReadWordTime));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public override void Update()
        {
            try
            {
                base.Update();

                this.Connected = PlcManager.Ins.IsConnected(this._config);

                if(this.Connected)
                {
                    this.ReadBitTime = this._config.ReadBitTime.TotalSeconds;
                    this.ReadWordTime = this._config.ReadWordTime.TotalSeconds;
                }

                this.IsIniFileCheck = string.IsNullOrEmpty(this.iniFilePath);

                this.SetPerformanceCounter();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                if (this.SelectedMenu != null)
                {
                    this.SelectedMenu.Update();
                }
            }
        }

        private void SetPerformanceCounter()
        {
            try
            {
                this.RunTime = this.CurProcess.UserProcessorTime;                

                this._countInterval++;

                this._cpuList.Add((int)this._cpu.NextValue());
                this._memoryList.Add(((int)this._memory.NextValue() / M_BYTE));

                if (this._countInterval <= 10) return;

                this.CPU = this._cpuList.Average() / 4;
                this.Memory = this._memoryList.Average() / 2;

                this._countInterval = 0;
                this._cpuList.Clear();
                this._memoryList.Clear();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public abstract class IMEtherneMenuViewModel : IMenuViewModel
    {
        protected PlcEthernetConfig _config = null;

        public void UpdateConfig(PlcEthernetConfig config)
        {
            this._config = config;
        }
    }
}
