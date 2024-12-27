using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;
using TS.FW.XCom.Core;

namespace TS.FW.XCom.Wpf.ClassFile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private XComReader _reader;

        public string ProjectName { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string CfgFilePath { get => this.GetValue<string>(); set => this.SetValue(value); }

        public ConfigModel Config { get => this.GetValue<ConfigModel>(); set => this.SetValue(value); }

        public string CreatePath { get => this.GetValue<string>(); set => this.SetValue(value); }

        public ObservableCollection<MessageModel> ClassList { get; set; } = new ObservableCollection<MessageModel>();

        public MessageModel SeletedClass { get => this.GetValue<MessageModel>(); set => this.SetValue(value); }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "EXIT":
                        {
                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                        }
                        break;
                    case "CLASS_PATH":
                        {
                            this.CreatePath = Keyboard.Show(this.CreatePath);
                        }
                        break;
                    case "CLASS_FILE":
                        {
                            if (Directory.Exists(this.CreatePath) == false) throw new DirectoryNotFoundException(this.CreatePath);

                            foreach (var item in this._reader.MakeClassFile(this.CreatePath, this.ProjectName))
                            {
                                var path = item.Key;
                                var contents = item.Value;

                                var dir = Path.GetDirectoryName(path);

                                if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                                if (File.Exists(path)) File.Delete(path);

                                File.WriteAllText(path, contents);
                            }

                            MsgBox.ShowMsg("클래스 파일 생성 완료.");
                        }
                        break;
                    case "CLASS_VIEW":
                        {
                            this.ClassList.Clear();

                            if (this._reader == null) return;

                            this._reader.LoadData();

                            var no = 1;

                            foreach (var item in this._reader.List)
                            {
                                this.ClassList.Add(new MessageModel()
                                {
                                    No = no++,
                                    Name = item.GetClassName(),
                                    SmlFormat = item.Format,
                                    ValidationText = item.ValidationClassFile(),
                                    ClassText = item.MakeClassFile(this.ProjectName),
                                });
                            }
                        }
                        break;
                    case "CONFIG_SETTING":
                        {
                            this._reader = new XComReader(this.CfgFilePath);
                            this.Config = this._reader.Config;
                        }
                        break;
                    case "PROJECT_NAME":
                        {
                            this.ProjectName = Keyboard.Show(this.ProjectName);
                        }
                        break;
                    case "CFG_FILE":
                        {
                            var dig = new OpenFileDialog()
                            {
                                Multiselect = false,
                                Filter = "*.cfg|*.cfg",
                            };

                            if (dig.ShowDialog() == false) return;

                            this.CfgFilePath = dig.FileName;
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
    }

    public class ConfigModel : DataModelBase
    {
        public string IP { get => this.GetValue<string>(); set => this.SetValue(value); }

        public int Port { get => this.GetValue<int>(); set => this.SetValue(value); }

        public int DeviceID { get => this.GetValue<int>(); set => this.SetValue(value); }

        public bool Active { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public string Sml { get => this.GetValue<string>(); set => this.SetValue(value); }

        public static implicit operator ConfigModel(XComConfig item)
        {
            if (item == null) return null;

            return new ConfigModel()
            {
                IP = item.IP,
                Port = item.Port,
                DeviceID = item.DeviceID,
                Active = item.Active,
                Sml = item.SML,
            };
        }
    }

    public class MessageModel : ModelBase
    {
        private static readonly SolidColorBrush _on = new SolidColorBrush(Colors.White);
        private static readonly SolidColorBrush _off = new SolidColorBrush(Colors.Red);

        public int No { get => this.GetValue<int>(); set => this.SetValue(value); }

        public string Name { get => this.GetValue<string>(); set => this.SetValue(value); }

        public bool IsValidation { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public string SmlFormat { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string ValidationText { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string ClassText { get => this.GetValue<string>(); set => this.SetValue(value); }

        public SolidColorBrush Color { get => this.GetValue<SolidColorBrush>(); set => this.SetValue(value); }

        protected override void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                base.SetValue(value, propertyName);
            }
            finally
            {
                if (string.Equals(propertyName, nameof(this.IsValidation)))
                {
                    this.Color = (bool)value ? _on : _off;
                }
                if (string.Equals(propertyName, nameof(this.ValidationText)))
                {
                    this.IsValidation = string.IsNullOrEmpty((string)value);
                }
            }
        }
    }
}
