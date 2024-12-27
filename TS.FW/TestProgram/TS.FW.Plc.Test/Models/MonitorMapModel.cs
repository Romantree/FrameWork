using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Utils;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;

namespace TS.FW.Plc.Test.Models
{
    public class MonitorMapModelList : ModelBase
    {
        public List<MonitorMapModel> List { get => this.GetValue<List<MonitorMapModel>>(); set => this.SetValue(value); }

        public MonitorMapModelList()
        {
            this.List = new List<MonitorMapModel>();
        }

        public void Update(MonitorMapModelList model)
        {
            if (this.List == null || this.List.Count <= 0)
            {
                foreach (var item in model.List)
                {
                    this.List.Add(new MonitorMapModel(item));
                }
            }
            else
            {
                for (int i = 0; i < model.List.Count; i++)
                {
                    this.List[i].Update(model.List[i]);
                }
            }
        }
    }

    public class MonitorMapModel : ModelBase
    {
        private readonly static BackgroundTimer _trBitChangedAuto = new BackgroundTimer();
        private readonly static Dictionary<MonitorMapModel, List<int>> _autoList = new Dictionary<MonitorMapModel, List<int>>();

        static MonitorMapModel()
        {
            _trBitChangedAuto.SleepTimeMsc = 2000;
            _trBitChangedAuto.DoWork += _trBitChangedAuto_DoWork;
            _trBitChangedAuto.Start();
        }

        private static void _trBitChangedAuto_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                lock (_autoList)
                {
                    foreach (var model in _autoList)
                    {
                        foreach (var index in model.Value)
                        {
                            model.Key.ValueList[index] = (byte)(model.Key.ValueList[index] == 0 ? 1 : 0);
                            model.Key.BitChanged(index);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(MonitorMapModel), ex);
            }
        }

        public int _address = 0;
        public int DataType = 0;
        public Action<MonitorMapModel, int> BitChangedHandler;
        public Action<MonitorMapModel, byte[]> WordChangedHandler;

        public string Address { get => this.GetValue<string>(); set => this.SetValue(value); }

        public byte[] ValueList { get => this.GetValue<byte[]>(); set => this.SetValue(value); }

        public string Value { get => this.GetValue<string>(); set => this.SetValue(value); }

        public NormalCommand OnBitChanged => new NormalCommand(BitChanged);

        public NormalCommand OnBitChangeAuto => new NormalCommand(BitChangeAuto);

        public NormalCommand OnWordChanged => new NormalCommand(WordChanged);

        public MonitorMapModel()
        {
            this.ValueList = new byte[16];
            this.Value = "0";
        }

        public MonitorMapModel(MonitorMapModel model)
        {
            this.Update(model);
        }

        public void Update(MonitorMapModel model)
        {
            if (this._address != model._address) this._address = model._address;
            if (this.DataType != model.DataType) this.DataType = model.DataType;

            if (this.Address != model.Address) this.Address = model.Address;
            if (this.Equals(this.ValueList, model.ValueList) == false) this.ValueList = model.ValueList.ToArray();
            if (this.Value != model.Value)
            {
                this.OnNotifyPropertyChanged(nameof(this.ValueList));
                this.Value = model.Value;
            }

            if (this.BitChangedHandler != model.BitChangedHandler) this.BitChangedHandler = model.BitChangedHandler;
            if (this.WordChangedHandler != model.WordChangedHandler) this.WordChangedHandler = model.WordChangedHandler;
        }

        private bool Equals(byte[] a, byte[] b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;
            if (b == null) return false;
            if (a.Length != b.Length) return false;

            for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;

            return true;
        }

        public void UpdateValue()
        {
            this.OnNotifyPropertyChanged(nameof(this.ValueList));

            var data = Convert.ToUInt16(string.Join("", this.ValueList.Reverse()), 2);

            switch (this.DataType)
            {
                case 0:
                    {
                        this.Value = Convert.ToString(data, 10);
                    }
                    break;
                case 1:
                    {
                        this.Value = "0x" + Convert.ToString(data, 16).PadLeft(4, '0').ToUpper();
                    }
                    break;
                case 2:
                    {
                        this.Value = Encoding.ASCII.GetString(data.ToByte(BitHandler.ByteOrder.LittleEndian));
                    }
                    break;
            }
        }

        public void BitChanged(object param)
        {
            try
            {
                var index = Convert.ToInt32(param);

                this.BitChangedHandler?.Invoke(this, index);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void BitChangeAuto(object param)
        {
            try
            {
                var index = Convert.ToInt32(param);

                lock (_autoList)
                {
                    if (_autoList.ContainsKey(this) == false) _autoList.Add(new MonitorMapModel(this), new List<int>());

                    if (_autoList[this].Contains(index))
                    {
                        _autoList[this].Remove(index);
                    }
                    else
                    {
                        _autoList[this].Add(index);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void WordChanged(object param)
        {
            try
            {
                switch (this.DataType)
                {
                    case 0:
                    case 1:
                        {
                            ushort value = 0;
                            var data = NumberPad.Show(value);

                            this.WordChangedHandler?.Invoke(this, data.ToByte());
                        }
                        break;
                    case 2:
                        {
                            var data = Keyboard.Show();
                            var buffer = Encoding.ASCII.GetBytes(data.PadLeft(2, ' ').ToArray()).Take(2).ToArray();

                            this.WordChangedHandler?.Invoke(this, buffer);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public override int GetHashCode()
        {
            return this._address;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public static IEnumerable<MonitorMapModel> ToMonitorBitMapModel(int address)
        {
            for (int i = 0; i < 16; i++)
            {
                var sAddress = address + (16 * i);

                yield return new MonitorMapModel()
                {
                    _address = sAddress,
                    Address = "0x" + Convert.ToString(sAddress, 16).PadLeft(4, '0').ToUpper(),
                };
            }
        }

        public static IEnumerable<MonitorMapModel> ToMonitorWordMapModel(int address)
        {
            for (int i = 0; i < 64; i++)
            {
                var sAddress = address + i;

                yield return new MonitorMapModel()
                {
                    _address = sAddress,
                    Address = "0x" + Convert.ToString(sAddress, 16).PadLeft(4, '0').ToUpper(),
                };
            }
        }
    }
}
