using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Dto;

namespace TS.FW.Plc.Communication
{
    public class PlcSimulationControl : IPlcControl
    {
        private PlcSimulationConfig _Config = null;
        private BackgroundTimer _trTick = null;

        private Dictionary<string, PlcBit.Signal> _bitList = new Dictionary<string, PlcBit.Signal>();
        private Dictionary<string, object> _wordList = new Dictionary<string, object>();

        public event EventHandler<bool> OnConnectedChangedEvent;

        public event EventHandler<IPlcConfigBase> OnTickEvent;

        public bool Connected => true;

        public void ChangeConfig(IPlcConfigBase config)
        {
            Logger.Write(this, Logger.LogEventLevel.Warning);

            if (this._Config == null) return;

            this.SetConfig(config);
        }

        public void Connect(IPlcConfigBase config)
        {
            this.SetConfig(config);

            this.OnConnectedChangedEvent?.Invoke(this._Config, true);

            if (this._trTick == null)
            {
                this._trTick = new BackgroundTimer();
                this._trTick.DoWork += _trTick_DoWork;
                this._trTick.SleepTimeMsc = IPlcConfigBase.TICK_INTERVAL;
            }

            this._trTick.Start();
        }

        public void DisConnect()
        {
            this._Config = null;

            this.OnConnectedChangedEvent?.Invoke(this._Config, false);

            if (this._trTick != null)
            {
                this._trTick.Stop();
            }

            this._trTick = null;
        }

        public Response<PlcBit.Signal> ReadBit(PlcBit bit)
        {
            try
            {
                if (bit == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                if (this._bitList.ContainsKey(bit.Name) == false) return new Response<PlcBit.Signal>(false, string.Format("존재하지 않은 Bit 정보 입니다. {0}", bit.Name));

                return new Response<PlcBit.Signal>(this._bitList[bit.Name]);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcBit, PlcBit.Signal> ReadBitList(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var result = this._bitList.ToList().Join(list, x => x.Key, y => y.Name, (x, y) => new { y, x.Value }).ToDictionary(t => t.y, t => t.Value);
                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcBit, PlcBit.Signal> ReadBitFromWordCommand(IEnumerable<PlcBit> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var result = this._bitList.ToList().Join(list, x => x.Key, y => y.Name, (x, y) => new { y, x.Value }).ToDictionary(t => t.y, t => t.Value);
                if (result == null || result.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>(false, "Plc Bit 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcWord, object> ReadRandomWord(IEnumerable<PlcWord> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var result = this._wordList.ToList().Join(list, x => x.Key, y => y.Name, (x, y) => new { y, x.Value }).ToDictionary(t => t.y, t => t.Value);
                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<object> ReadWord(PlcWord word)
        {
            try
            {
                if (word == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                if (this._wordList.ContainsKey(word.Name) == false) return new Response<object>(false, string.Format("존재하지 않은 Word 정보 입니다. {0}", word.Name));

                return new Response<object>(this._wordList[word.Name]);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public ResponseList<PlcWord, object> ReadWordList(IEnumerable<PlcWord> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                var result = this._wordList.ToList().Join(list, x => x.Key, y => y.Name, (x, y) => new { y, x.Value }).ToDictionary(t => t.y, t => t.Value);
                if (result == null || result.Count <= 0) return new ResponseList<PlcWord, object>(false, "Plc Word 데이터 읽는데 실패하였습니다.");

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteBit(PlcBit bit, PlcBit.Signal signal)
        {
            try
            {
                if (bit == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                if (this._bitList.ContainsKey(bit.Name) == false) return new Response(false, string.Format("존재하지 않은 Bit 정보 입니다. {0}", bit.Name));
                this._bitList[bit.Name] = signal;

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteBitList(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var item in list)
                {
                    if (this._bitList.ContainsKey(item.Key.Name))
                    {
                        this._bitList[item.Key.Name] = item.Value;
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteBitFromWordCommand(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var item in list)
                {
                    if (this._bitList.ContainsKey(item.Key.Name))
                    {
                        this._bitList[item.Key.Name] = item.Value;
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteRandomBit(Dictionary<PlcBit, PlcBit.Signal> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Bit 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var item in list)
                {
                    if (this._bitList.ContainsKey(item.Key.Name))
                    {
                        this._bitList[item.Key.Name] = item.Value;
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteRandomWord(Dictionary<PlcWord, object> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var item in list)
                {
                    if (this._wordList.ContainsKey(item.Key.Name))
                    {
                        this._wordList[item.Key.Name] = item.Value;
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteWord(PlcWord word, object value)
        {
            try
            {
                if (word == null) throw new NullReferenceException("PLC Bit 정보가 NULL 입니다.");

                this.IsInitializeCheck();

                if (this._wordList.ContainsKey(word.Name) == false) return new Response(false, string.Format("존재하지 않은 Word 정보 입니다. {0}", word.Name));
                this._wordList[word.Name] = value;

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response WriteWordList(Dictionary<PlcWord, object> list)
        {
            try
            {
                if (list == null) throw new NullReferenceException("PLC Word 정보가 NULL 입니다.");
                if (list.Count() == 0) throw new Exception("PLC Word 정보가 존재하지 않습니다.");

                this.IsInitializeCheck();

                foreach (var item in list)
                {
                    if (this._wordList.ContainsKey(item.Key.Name))
                    {
                        this._wordList[item.Key.Name] = item.Value;
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        private void SetConfig(IPlcConfigBase config)
        {
            if (config == null) throw new NullReferenceException("PLC 이터넷 통신 설정 정보가 NULL 입니다.");
            if (config is PlcSimulationConfig == false) throw new TypeAccessException(string.Format("PLC 시뮬레이션 통신 설정 정보가 잘못되었습니다. {0}", config));

            this._Config = config as PlcSimulationConfig;

            foreach (var item in this._Config.TotalBitList)
            {
                this._bitList.Add(item.Key, item.Value.OnOff);
            }

            foreach (var item in this._Config.TotalWordList)
            {
                this._wordList.Add(item.Key, item.Value.Value);
            }
        }

        private void IsInitializeCheck()
        {
            if (this._Config == null) throw new Exception("PLC 통신 환경설정 정보가 존재하지 않습니다.");
        }

        private void _trTick_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                this.OnTickEvent?.Invoke(this, this._Config);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
