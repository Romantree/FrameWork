using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Test.Managers;
using TS.FW.Device.Test.Views;
using TS.FW.Diagnostics;
using TS.FW.Utils;
using TS.FW.Wpf.Core;

namespace TS.FW.Device.Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int _inPageNo = 0;
        private int _OutPageNo = 0;
        private List<List<InOutModel>> _InPage = new List<List<InOutModel>>();
        private List<List<InOutModel>> _OutPage = new List<List<InOutModel>>();

        private BackgroundTimer _trUpdate = new BackgroundTimer();

        private InOutManager IO => InOutManager.Ins;

        public List<InOutModel> InList { get => this.GetValue<List<InOutModel>>(); set => this.SetValue(value); }

        public List<InOutModel> OutList { get => this.GetValue<List<InOutModel>>(); set => this.SetValue(value); }

        public bool IsInPrev { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsInNext { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsOutPrev { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsOutNext { get => this.GetValue<bool>(); set => this.SetValue(value); }

        #region Button Command 
        public NormalCommand OnBitNoCmd => new NormalCommand(BitNoCmd);

        public NormalCommand OnChangedCmd => new NormalCommand(ChangedCmd);

        public NormalCommand OnDigOnOffCmd => new NormalCommand(DigOnOffCmd);

        public NormalCommand OnDigOnCmd => new NormalCommand(DigOnCmd);

        public NormalCommand OnDigOffCmd => new NormalCommand(DigOffCmd);

        #endregion

        public MainViewModel()
        {
            if (this.IsDesignMode == true) return;

            this.MainWindow.Closed += MainWindow_Closed;

            DeviceManager.Ins.Start();
            InOutManager.Ins.Start();

            this.InitModel(this._InPage, enInOutType.IN, this.IO.InList, 0);
            this.InitModel(this._OutPage, enInOutType.OUT, this.IO.OutList, 3);

            this.InList = this.UpdatePage(this._InPage, this._inPageNo);
            this.OutList = this.UpdatePage(this._OutPage, this._OutPageNo);

            this._trUpdate.SleepTimeMsc = 100;
            this._trUpdate.DoWork += _trUpdate_DoWork;
            this._trUpdate.Start();
        }

        private void BitNoCmd(object param)
        {
            try
            {
                var item = param as InOutModel;
                if (item == null) return;

                item.OutputType = item.OutputType == enOutputType.A ? enOutputType.B : enOutputType.A;

                IO.SetDatabase(item);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void ChangedCmd(object param)
        {
            try
            {
                var item = param as InOutModel;
                if (item == null) return;

                if (PuInOutConfigModel.Show(item) == 0) return;

                if (item.Type == enInOutType.IN)
                {
                    this.InitModel(this._InPage, enInOutType.IN, this.IO.InList, 0);
                    this.InList = this.UpdatePage(this._InPage, this._inPageNo);
                }
                else
                {
                    this.InitModel(this._OutPage, enInOutType.OUT, this.IO.OutList, 3);
                    this.OutList = this.UpdatePage(this._OutPage, this._OutPageNo);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void DigOnOffCmd(object param)
        {
            try
            {
                var item = param as InOutModel;
                if (item == null) return;

                if (item.Type == enInOutType.IN)
                {
                    this.IO.WriteX(item.OnOff ? false : true, item.Key);
                }
                else
                {
                    this.IO.WriteY(item.OnOff ? false : true, item.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void DigOnCmd(object param)
        {
            try
            {
                var item = param as InOutModel;
                if (item == null || item.DisplayName == "Spare") return;

                if (item.Type == enInOutType.IN)
                {
                    this.IO.WriteX(true, item.Key);
                }
                else
                {
                    this.IO.WriteY(true, item.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void DigOffCmd(object param)
        {
            try
            {
                var item = param as InOutModel;
                if (item == null || item.DisplayName == "Spare") return;

                if (item.Type == enInOutType.IN)
                {
                    this.IO.WriteX(false, item.Key);
                }
                else
                {
                    this.IO.WriteY(false, item.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                this.IsInPrev = this._inPageNo != 0;
                this.IsInNext = this._inPageNo != this._InPage.Count - 1;

                this.IsOutPrev = this._OutPageNo != 0;
                this.IsOutNext = this._OutPageNo != this._OutPage.Count - 1;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            TS.FW.Diagnostics.BackgroundTimer.AllStop();
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "IN_PREV":
                        {
                            if (this._inPageNo <= 0) return;
                            this._inPageNo--;

                            this.InList = this.UpdatePage(this._InPage, this._inPageNo);
                        }
                        break;
                    case "IN_NEXT":
                        {
                            if (this._inPageNo >= this._InPage.Count - 1) return;
                            this._inPageNo++;

                            this.InList = this.UpdatePage(this._InPage, this._inPageNo);
                        }
                        break;
                    case "OUT_PREV":
                        {
                            if (this._OutPageNo <= 0) return;
                            this._OutPageNo--;

                            this.OutList = this.UpdatePage(this._OutPage, this._OutPageNo);
                        }
                        break;
                    case "OUT_NEXT":
                        {
                            if (this._OutPageNo >= this._OutPage.Count - 1) return;
                            this._OutPageNo++;

                            this.OutList = this.UpdatePage(this._OutPage, this._OutPageNo);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private List<InOutModel> UpdatePage(List<List<InOutModel>> page, int pageNo)
        {
            try
            {
                return page[pageNo];
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return null;
            }
        }

        private void InitModel(List<List<InOutModel>> page, enInOutType type, List<InOutModel> list, int sPortNo)
        {
            try
            {
                page.Clear();

                var oList = list.OrderBy(t => t.BitNo);
                var temp = new List<InOutModel>();

                foreach (var item in InOutManager.Ins.ToInOutModelList(type, sPortNo, InOutManager.MODULE_COUNT, InOutManager.MODULE_BIT_COUNT))
                {
                    var model = oList.FirstOrDefault(t => t.ModuleNo == item.ModuleNo && t.BitNo == item.BitNo);
                    if (model == null)
                    {
                        temp.Add(item);
                    }
                    else
                    {
                        temp.Add(model);
                    }
                }

                foreach (var item in temp.ToPageList(InOutManager.MODULE_BIT_COUNT).Where(t => t != null && t.Count() > 0).Select(t => t.ToList()))
                {
                    page.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
