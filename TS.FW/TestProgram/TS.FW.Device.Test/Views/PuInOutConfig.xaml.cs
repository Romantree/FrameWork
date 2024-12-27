using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TS.FW.Device.Test.Managers;
using TS.FW.Utils;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;

namespace TS.FW.Device.Test.Views
{
    /// <summary>
    /// PuInOutConfig.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PuInOutConfig : Window
    {
        public PuInOutConfig()
        {
            InitializeComponent();
        }
    }

    public class PuInOutConfigModel : ViewModelBase
    {
        private readonly PuInOutConfig _view = new PuInOutConfig();
        private readonly InOutModel _curModel = null;
        private int cmd = 0;


        private InOutManager IO => InOutManager.Ins;
        private List<List<InOutModel>> _Page;
        private int _pageNo = 0;


        public int BitNo { get => this.GetValue<int>(); set => this.SetValue(value); }

        public string DisplayName { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string SearchText { get => this.GetValue<string>(); set => this.SetValue(value); }

        public List<InOutModel> List { get => this.GetValue<List<InOutModel>>(); set => this.SetValue(value); }

        public NormalCommand OnSettingCmd => new NormalCommand(SettingCmd);

        public PuInOutConfigModel(InOutModel model)
        {
            this._view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this._view.DataContext = this;

            this._curModel = model;

            this.BitNo = model.BitNo + (model.ModuleNo * InOutManager.MODULE_BIT_COUNT);
            this.DisplayName = model.DisplayName;

            this.InitList();
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "PREV":
                        {
                            if (this._pageNo <= 0) return;
                            this._pageNo--;

                            this.UpdateList();
                        }
                        break;
                    case "NEXT":
                        {
                            if (this._pageNo >= this._Page.Count - 1) return;
                            this._pageNo++;

                            this.UpdateList();
                        }
                        break;
                    case "CANCEL":
                        {
                            this.cmd = 0;
                            this._view.Close();
                        }
                        break;
                    case "SearchText":
                        {
                            SearchText = Keyboard.Show();
                        }
                        break;
                    case "SEARCH":
                        {
                            if (string.IsNullOrWhiteSpace(this.SearchText))
                            {
                                this.UpdateList();
                            }
                            else
                            {
                                this.List = this._Page.SelectMany(t => t).Where(t => string.IsNullOrEmpty(t.DisplayName) == false && t.DisplayName.ToUpper().Contains(this.SearchText.ToUpper())).ToList();
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void SettingCmd(object param)
        {
            try
            {
                var model = param as InOutModel;
                if (model == null) return;

                if (MsgBox.ShowMsgInvoke(string.Format("[{0}][{1}]으로 입/출력 주소를 변경하시겠습니까?", model.DisplayNo, model.DisplayName), true) == false) return;

                if (model.DisplayNo == -1 && this.BitNo != -1)
                {
                    this._curModel.Key = model.Key;
                    this._curModel.DisplayName = model.DisplayName;
                    this._curModel.OutputType = model.OutputType;

                    this.IO.SetDatabase(this._curModel);
                }
                else
                {
                    var no = model.DisplayNo;
                    var mNo = no / InOutManager.MODULE_BIT_COUNT;
                    var bNo = no % InOutManager.MODULE_BIT_COUNT;

                    this._curModel.ModuleNo = mNo;
                    this._curModel.BitNo = bNo;

                    var tmNo = this.BitNo / InOutManager.MODULE_BIT_COUNT;
                    var tbNo = this.BitNo % InOutManager.MODULE_BIT_COUNT;

                    model.ModuleNo = tmNo;
                    model.BitNo = tbNo;

                    if (this._curModel.Key.Contains("Spare") == false)
                    {
                        this.IO.SetDatabase(this._curModel);
                    }

                    if (model.Key.Contains("Spare") == false)
                    {
                        this.IO.SetDatabase(model);
                    }
                }

                this.cmd = 1;

                this._view.Close();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void InitList()
        {
            try
            {
                var list = new List<InOutModel>();

                var temp = this.IO.ReadDataBase(this._curModel.Type);
                foreach (var item in temp.Where(t => t.BitNo == -1))
                {
                    list.Add(item);
                }

                if (this._curModel.Type == enInOutType.IN)
                {
                    foreach (var item in this.IO.InList)
                    {
                        if (this.BitNo == item.BitNo) continue;

                        list.Add(item);
                    }
                }
                else
                {
                    foreach (var item in this.IO.OutList)
                    {
                        if (this.BitNo == item.BitNo) continue;

                        list.Add(item);
                    }
                }

                this._Page = list.OrderBy(t => t.DisplayNo).ToPageList(8).Where(t => t != null && t.Count() > 0).Select(t => t.ToList()).ToList();

                this.UpdateList();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void UpdateList()
        {
            try
            {
                this.List = this._Page[this._pageNo];
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public static int Show(InOutModel data)
        {
            var model = new PuInOutConfigModel(data);
            model._view.ShowDialog();

            return model.cmd;
        }
    }
}
