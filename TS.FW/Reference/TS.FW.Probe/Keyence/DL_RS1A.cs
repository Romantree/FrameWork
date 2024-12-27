using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS.FW.Probe.Keyence
{
    public class DL_RS1A
    {
        private Encoding Encoding => Encoding.ASCII;

        /// <summary>
        /// 전체 Probe 데이터 요청 명령
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public byte[] M_Zero(int no)
        {
            return this.Encoding.GetBytes("M0\r\n");
        }

        public IEnumerable<ProbeStatus> ToStatus(byte[] buffer)
        {
            var data = this.Encoding.GetString(buffer);

            foreach (var msg in data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var cmd = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (cmd.Length < 3) continue;

                switch (cmd[0])
                {
                    case "ER":
                        {
                            Logger.Write(this, msg, Logger.LogEventLevel.Error);
                        }
                        break;
                    case "M0":
                        {
                            foreach (var item in cmd.Skip(1).Select((t, Ch) => new { Ch, Data = Convert.ToDouble(t) }))
                            {
                                yield return new ProbeStatus() { No = item.Ch, Data = item.Data };
                            }
                        }
                        break;
                }
            }
        }
    }

    public struct ProbeStatus
    {
        public int No { get; internal set; }

        public double Data { get; internal set; }

        //public ProbeStatus(int no)
        //{
        //    this.No = no;
        //}
    }
}
