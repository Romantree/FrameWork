using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Test.Managers;
using TS.FW.Plc.Test.Models;
using TS.FW.Plc.Test.Views.Page.MitsubishiPlc;
using TS.FW.Utils;
using TS.FW.Wpf.Controls.Pu;

namespace TS.FW.Plc.Test.ViewModels.Page.MitsubishiPlc
{
    public class MEtherneWordMonitoringViewModel : IMEtherneMenuViewModel
    {
        private readonly MEtherneWordMonitoringView _view = new MEtherneWordMonitoringView();

        public override int No => 1;

        public override string Name => "WORD 모니터링";

        public override ContentControl View => _view;

        private int _index = 0;
        private List<MonitorMapModel> _totalList = null;

        public List<MonitorMapModelList> _DataList { get; set; } = new List<MonitorMapModelList>();

        public MonitorMapModelList DataList { get; set; } = new MonitorMapModelList();

        public string Address { get => this.GetValue<string>(); set => this.SetValue(value); }

        public override void Init()
        {
            try
            {
                this.Address = "0x0000";

                PlcManager.Ins.OnWordChanged += Ins_OnWordChanged;
                PlcManager.Ins.OnOutWordChanged += Ins_OnOutWordChanged;

                base.Init();

                this.InitMap();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Ins_OnOutWordChanged(object sender, WordChangedEventArg e)
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

        private void Ins_OnWordChanged(object sender, WordChangedEventArg e)
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

        private void UpdateData(List<PlcWord> list)
        {
            try
            {
                lock (this._DataList)
                {
                    this._totalList = this._totalList ?? this._DataList.SelectMany(t => t.List).ToList();

                    foreach (var word in list)
                    {
                        var wList = this._totalList.SkipWhile(t => t._address != word.Address).Take(word.Length).ToList();
                        if (wList == null || wList.Count <= 0) continue;

                        var buffer = this.ToByte(word);
                        if (buffer.Length != (word.Length * 2)) continue;

                        for (int i = 0; i < word.Length; i++)
                        {
                            var temp = string.Join("", buffer.Skip(i).Take(2).Reverse().Select(t => t.ToBinary()));

                            wList[i].ValueList = temp.Reverse().Select(t => (byte)(t == '0' ? 0 : 1)).ToArray();
                            wList[i].UpdateValue();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private byte[] ToByte(PlcWord word)
        {
            try
            {
                if (word.Type == PlcWordDataType.Number)
                {
                    switch (word.Length)
                    {
                        case 1: return Convert.ToInt16(word.Value).ToByte(BitHandler.ByteOrder.LittleEndian);
                        case 2: return Convert.ToInt32(word.Value).ToByte(BitHandler.ByteOrder.LittleEndian);
                    }
                }
                else if (word.Type == PlcWordDataType.LIST)
                {
                    if (word.Value is IEnumerable<byte>)
                    {
                        return (word.Value as IEnumerable<byte>).ToArray();
                    }
                }
                else if (word.Type == PlcWordDataType.Double)
                {
                    switch (word.Length)
                    {
                        case 2: return Convert.ToSingle(word.Value).ToByte(BitHandler.ByteOrder.LittleEndian);
                        case 4: return Convert.ToDouble(word.Value).ToByte(BitHandler.ByteOrder.LittleEndian);
                    }
                }
                else if (word.Type == PlcWordDataType.ASCII)
                {
                    // 문자형 데이터 - ASCII 코드                
                    if (word.Length > 1)
                    {
                        // 다수의 문자열 전송
                        return this.ToAsciiString(Convert.ToString(word.Value), word.Length).ToArray();
                    }
                    else
                    {
                        // 하나의 문자열만 전송
                        return this.ToAsciiString(Convert.ToString(word.Value), word.Length).Take(2).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }

            return new byte[0];
        }

        private IEnumerable<byte> ToAsciiString(string text, int length)
        {
            if (text.Length > length * 2) text = text.Substring(0, length * 2);

            return Encoding.ASCII.GetBytes(text.PadRight(length * 2, ' '));
        }

        private void InitMap()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        var start = (0x0040 * k) + (0x0100 * j) + (0x1000 * i);

                        var list = new MonitorMapModelList();

                        foreach (var item in MonitorMapModel.ToMonitorWordMapModel(start).ToList())
                        {
                            item.WordChangedHandler = this.WordChanged;
                            list.List.Add(item);
                        }

                        this._DataList.Add(list);
                    }
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

        private void WordChanged(MonitorMapModel model, byte[] data)
        {
            try
            {
                if (this._config == null) return;

                if (PlcManager.Ins.Control.WriteWord(this._config.PlcNo, model._address, data) == false) return;

                var item = this._DataList[this._index].List.First(t => t.Address == model.Address);
                var temp = string.Join("", data.Reverse().Select(t => t.ToBinary()));

                item.ValueList = temp.Reverse().Select(t => (byte)(t == '0' ? 0 : 1)).ToArray();
                item.UpdateValue();
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
                            var temp = Convert.ToInt32(this.Address, 16) & 0xFFF0;
                            var index = temp / 64;

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
