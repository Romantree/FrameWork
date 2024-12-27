using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Test.Managers;
using TS.FW.Plc.Test.Models;
using TS.FW.Plc.Test.Views.Page.MitsubishiPlc;
using TS.FW.Utils;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;

namespace TS.FW.Plc.Test.ViewModels.Page.MitsubishiPlc
{
    public class MEtherneBitMonitoringViewModel : IMEtherneMenuViewModel
    {
        private readonly MEtherneBitMonitoringView _view = new MEtherneBitMonitoringView();

        public override int No => 1;

        public override string Name => "BIT 모니터링";

        public override ContentControl View => _view;

        private int _index = 0;

        public List<MonitorMapModelList> _DataList { get; set; } = new List<MonitorMapModelList>();

        public MonitorMapModelList DataList { get; set; } = new MonitorMapModelList();

        public string Address { get => this.GetValue<string>(); set => this.SetValue(value); }

        public override void Init()
        {
            try
            {
                this.Address = "0x0000";

                PlcManager.Ins.OnOutBitChanged += Ins_OnOutBitChanged;
                PlcManager.Ins.OnBitChanged += Ins_OnBitChanged;

                base.Init();

                this.InitMap();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Ins_OnOutBitChanged(object sender, BitChangedEventArg e)
        {
            try
            {
                if (this._config != e.Config) return;

                this.UpdateData(e.List);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Ins_OnBitChanged(object sender, BitChangedEventArg e)
        {
            try
            {
                if (this._config != e.Config) return;

                this.UpdateData(e.List);
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
                this.UpdateMap();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void UpdateData(List<PlcBit> list)
        {
            try
            {
                lock (this._DataList)
                {
                    var dlist = this._DataList.SelectMany(t => t.List).ToList();

                    foreach (var bit in list)
                    {
                        var address = bit.Address & 0xFFF0;
                        var index = bit.Address & 0x000F;

                        var item = dlist.FirstOrDefault(t => t._address == address);
                        if (item == null) continue;

                        item.ValueList[index] = (byte)(bit.OnOff == PlcBit.Signal.ON ? 1 : 0);
                        item.UpdateValue();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void InitMap()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    var start = (0x0100 * j) + (0x1000 * i);

                    var list = new MonitorMapModelList();

                    foreach (var item in MonitorMapModel.ToMonitorBitMapModel(start).ToList())
                    {
                        item.BitChangedHandler = this.BitChanged;
                        list.List.Add(item);
                    }

                    this._DataList.Add(list);
                }
            }
        }

        public override void Show()
        {
            base.Show();

            this.UpdateMap();
        }

        private void UpdateMap()
        {
            lock (this._DataList)
            {
                this.DataList.Update(this._DataList[this._index]);
            }
        }

        private void BitChanged(MonitorMapModel model, int index)
        {
            try
            {
                if (this._config == null) return;

                var address = model._address + index;
                var bit = this._config.OutBitList.Values.FirstOrDefault(t => t.Address == address);
                if (bit == null) return;

                bit.WriteOnOff = model.ValueList[index] == 1 ? PlcBit.Signal.OFF : PlcBit.Signal.ON;

                PlcManager.Ins.WriteBitProcess(this._config);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "PREV":
                        {
                            if (this._index <= 0) return;

                            this._index--;
                        }
                        break;
                    case "NEXT":
                        {
                            if (this._index >= this._DataList.Count) return;

                            this._index++;
                        }
                        break;
                    case "TYPE_10":
                        {
                            foreach (var item in this._DataList.SelectMany(t => t.List))
                            {
                                item.DataType = 0;
                                item.UpdateValue();
                            }
                        }
                        break;
                    case "TYPE_16":
                        {
                            foreach (var item in this._DataList.SelectMany(t => t.List))
                            {
                                item.DataType = 1;
                                item.UpdateValue();
                            }
                        }
                        break;
                    case "TYPE_ASCII":
                        {
                            foreach (var item in this._DataList.SelectMany(t => t.List))
                            {
                                item.DataType = 2;
                                item.UpdateValue();
                            }
                        }
                        break;
                    case "ADDRESS":
                        {
                            this.Address = Keyboard.Show(this.Address);
                        }
                        break;
                    case "SET_ADDRESS":
                        {
                            var temp = Convert.ToInt32(this.Address, 16) & 0xFF00;
                            var index = temp / 256;

                            if (index < 0 || index >= this._DataList.Count) return;

                            this._index = index;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }    
}
