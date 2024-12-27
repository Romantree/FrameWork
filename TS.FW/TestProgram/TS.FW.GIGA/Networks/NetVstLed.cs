using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace TS.FW.GIGA.Networks
{
    public class NetVstLed : INetSerialPort
    {
        private const string STX ="@";
        private const string ETX = "\r\n";
        private const string ID = "00";        

        private readonly VstLedData _ch1 = new VstLedData();
        private readonly VstLedData _ch2 = new VstLedData();
        private readonly VstLedData _ch3 = new VstLedData();
        private readonly VstLedData _ch4 = new VstLedData();

        private string _msgTemp = string.Empty;

        public VstLedData this[eVstLedCh ch]
        {
            get
            {
                switch (ch)
                {
                    case eVstLedCh.CH1: return _ch1;
                    case eVstLedCh.CH2: return _ch2;
                    case eVstLedCh.CH3: return _ch3;
                    case eVstLedCh.CH4: return _ch4;
                }

                return null;
            }
        }

        public NetVstLed()
        {
            this.baudRate = 19200;
            this.dataBit = 8;
            this.stopBits = StopBits.One;
            this.parity = Parity.None;
            this.readBuffer = 50;
            this.IsRecvEvent = true;
        }

        public override void Init()
        {
            this.Off();
        }

        public void Off()
        {
            if (this.IsOpen == false) return;

            this.Send($"{STX}FFA0000{ID}{ETX}");
        }

        public void SetPower(eVstLedCh channel, int power = 0)
        {
            try
            {
                this[channel].SetPower = power;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void UpdatePower(params  eVstLedCh[] ch)
        {
            try
            {
                if (this.IsOpen == false || ch == null || ch.Length <= 0) return;

                var cmd = string.Join("", ch.Select(t => SetCmd(t)));

                this.Send(cmd);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private string SetCmd(eVstLedCh channel) => $"{STX}{(int)channel:D2}F{this[channel].SetPower:D3}{ID}{ETX}";

        protected override void DataReceived(SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Eof) return;
            if (this._client.BytesToRead <= 0) return;

            try
            {
                var buffer = new byte[this._client.BytesToRead];
                var len = this._client.Read(buffer, 0, buffer.Length);

                if (buffer.Length != len) return;

                foreach (var item in this.ToDataSplit(buffer))
                {
                    this.DataParser(item);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this._client.DiscardInBuffer();
            }
        }

        private void DataParser(string msg)
        {
            try
            {
                if (int.TryParse(msg.Substring(1, 2), out int ch) == false) return;
                if (msg[3] != 'O') return;

                this[(eVstLedCh)ch].Setting();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private IEnumerable<string> ToDataSplit(byte[] buffer)
        {
            var str = this.encoding.GetString(buffer);
            Logger.DebugWrite(this, str);

            var msg = (_msgTemp + str).Replace(ETX, "");
            var count = msg.Length / 6;

            for (int i = 0; i < count; i++)
            {
                yield return msg.Substring(i * 6, 6);
            }

            _msgTemp = msg.Substring(count * 6);
        }
    }

    public enum eVstLedCh { CH1, CH2, CH3, CH4 }

    public class VstLedData
    {
        public bool OnOff { get; set; } = false;

        public int Power { get; set; } = 0;

        public int SetPower { get; set; } = 0;

        public void Setting()
        {
            this.Power = this.SetPower;
            this.OnOff = this.Power > 0;
        }

        public override string ToString() => $"{(OnOff ? "On" : "Off")} : {Power}";

        public static implicit operator bool(VstLedData data) => data.OnOff;
        public static implicit operator int(VstLedData data) => data.Power;
    }
}