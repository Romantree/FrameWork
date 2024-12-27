using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Utils;

namespace TS.FW.Elec
{
    public class ElectMeterData
    {
        #region Data

        public ElectMeterState State { get; set; }

        public ElectMeterAlarm Alarm { get; set; }

        public int EventCount => (this.EventCount2 * 1000) + this.EventCount1;

        /// <summary>
        /// 0~100
        /// </summary>
        public int EventCount1 { get; set; }

        /// <summary>
        /// 1000 단위
        /// </summary>
        public int EventCount2 { get; set; }

        /// <summary>
        /// 온도 ℃
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// A상 상전압(VA) V
        /// </summary>
        public double A_PhaseVoltage { get; set; }

        /// <summary>
        /// B상 상전압(VB) V
        /// </summary>
        public double B_PhaseVoltage { get; set; }

        /// <summary>
        /// C상 상전압(VC) V
        /// </summary>
        public double C_PhaseVoltage { get; set; }

        /// <summary>
        /// 평균 상전압 V
        /// </summary>
        public double PhaseVoltage { get; set; }

        /// <summary>
        /// AB상 선간전압(Vab) V
        /// </summary>
        public double AB_LineVoltage { get; set; }

        /// <summary>
        /// BC상 선간전압(Vbc) V
        /// </summary>
        public double BC_LineVoltage { get; set; }

        /// <summary>
        /// CA상 선간전압(Vca) V
        /// </summary>
        public double CA_LineVoltage { get; set; }

        /// <summary>
        /// 평균 선간전압 V
        /// </summary>
        public double LineVoltage { get; set; }

        /// <summary>
        /// 주파수 Hz
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// Van/Vab THD 전압 %
        /// </summary>
        public double VanVabThdVoltage { get; set; }

        /// <summary>
        /// Vbn/Vbc THD 전압 %
        /// </summary>
        public double VbnVbcThdVoltage { get; set; }

        /// <summary>
        /// Vcn/Vca THD 전압 %
        /// </summary>
        public double VcnVcaThdVoltage { get; set; }

        /// <summary>
        /// 영상 전압 %
        /// </summary>
        public double Voltage1 { get; set; }

        /// <summary>
        /// 정상 전압 %
        /// </summary>
        public double Voltage2 { get; set; }

        /// <summary>
        /// 역상 전압 %
        /// </summary>
        public double Voltage3 { get; set; }

        /// <summary>
        /// 전압 불평형 %
        /// </summary>
        public double NEMA { get; set; }

        /// <summary>
        /// 영상 전압 불평형 %
        /// </summary>
        public double NEMA1 { get; set; }

        /// <summary>
        /// 역상 전압 불평형 %
        /// </summary>
        public double NEMA2 { get; set; }

        /// <summary>
        /// A상 Crest Factor
        /// </summary>
        public double A_CrestFactor { get; set; }

        /// <summary>
        /// B상 Crest Factor
        /// </summary>
        public double B_CrestFactor { get; set; }

        /// <summary>
        /// C상 Crest Factor
        /// </summary>
        public double C_CrestFactor { get; set; }

        /// <summary>
        /// A상 전압 위상
        /// </summary>
        public double A_VoltagePhase { get; set; }

        /// <summary>
        /// B상 전압 위상
        /// </summary>
        public double B_VoltagePhase { get; set; }

        /// <summary>
        /// C상 전압 위상
        /// </summary>
        public double C_VoltagePhase { get; set; }

        /// <summary>
        /// 누설 전류
        /// </summary>
        public double LeakageCharge { get; set; }

        #endregion

        /// <summary>
        /// SEM 4번째 R상 전류 20007-20008
        /// </summary>
        public double CRRNT_R { get; set; }
        /// <summary>
        /// SEM 5번째 T상 전류 20011-20012
        /// </summary>
        public double CRRNT_T { get; set; }

        /// <summary>
        /// SEM 1번째 순시 전력 20017-20018
        /// </summary>
        public double I_PWR { get; set; }


        /// <summary>
        /// SEM 0번째 적산전력량 20021-20022  
        /// </summary>
        public double INTEGR_PWR { get; set; }

        /// <summary>
        /// SEM 2번째 R상 전압 20025 - 20026
        /// </summary>
        public double VOLT_R { get; set; }
        /// <summary>
        /// SEM 3번째 T상 전압 20029 - 20030
        /// </summary>
        public double VOLT_T { get; set; }
        public double LEAK_CRRNT { get; set; }

        public double VAN_AB_VTHD { get; set; }
        public double VAN_BC_VTHD { get; set; }
        public double VAN_CA_VTHD { get; set; }
        public double VAN_AB_ITDD { get; set; }
        public double VAN_BC_ITDD { get; set; }
        public double VAN_CA_ITDD { get; set; }

        public void SetData(byte[] buffer, BitHandler.ByteOrder order)
        {
            try
            {
                var idx = 0;

                //var tmp1 = buffer.ToUInt16(ref idx, order);
                this.CRRNT_R = Math.Round(buffer.ToFloat(ref idx, order), 3); //0-1 20007 A상전류
                var tmpB = buffer.ToFloat(ref idx, order);
                this.CRRNT_T = Math.Round(buffer.ToFloat(ref idx, order), 3); //4-5 20011 C상전류
                var tmpReserved = buffer.ToFloat(ref idx, order);
                var 역률 = buffer.ToFloat(ref idx, order);
                this.I_PWR = Math.Round(buffer.ToFloat(ref idx, order), 3); //10-11 20017 유효전력
                var 무효전력 = buffer.ToFloat(ref idx, order); //
                this.INTEGR_PWR = buffer.ToFloat(ref idx, order); //12-13 20021 유효전력량
                var 무효전력량 = buffer.ToFloat(ref idx, order);
                this.VOLT_R = Math.Round(buffer.ToFloat(ref idx, order), 1); //18-19 20025 A상 상전얍(VA)
                var tmp3 = buffer.ToFloat(ref idx, order);
                this.VOLT_T = Math.Round(buffer.ToFloat(ref idx, order), 1); //22-23 20029 C상 상전압(VC)
                var tempA = buffer.ToFloat(ref idx, order);
                var tempB = buffer.ToFloat(ref idx, order);
                var tempC = buffer.ToFloat(ref idx, order);
                this.VAN_AB_VTHD = Math.Round(buffer.ToFloat(ref idx, order), 3); // 20037
                this.VAN_BC_VTHD = Math.Round(buffer.ToFloat(ref idx, order), 3); // 20039
                this.VAN_CA_VTHD = Math.Round(buffer.ToFloat(ref idx, order), 3); // 20041
                var crrntA = buffer.ToFloat(ref idx, order);
                var crrntB = buffer.ToFloat(ref idx, order);
                var crrntC = buffer.ToFloat(ref idx, order);
                this.VAN_AB_ITDD = Math.Round(buffer.ToFloat(ref idx, order), 3); // 20049
                this.VAN_BC_ITDD = Math.Round(buffer.ToFloat(ref idx, order), 3); // 20051
                this.VAN_CA_ITDD = Math.Round(buffer.ToFloat(ref idx, order), 3); // 20053
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }


        /// <summary>
        /// 누설전류량 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="order"></param>
        public void SetData2(byte[] buffer, BitHandler.ByteOrder order)
        {
            try
            {
                var idx = 0;

                this.LEAK_CRRNT = Math.Round(buffer.ToFloat(ref idx, order), 4);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        /// <summary>
        /// 소수점 부분을 연산을 통해서 정수로 변경해서 처리 하는 함수
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int DoubleToInt(double value)
        {
            if (value.ToString().IndexOf('.') == -1) return (int)value;
            var idx = value.ToString().Length - value.ToString().IndexOf('.') - 1;
            switch (idx)
            {
                case 1: return (int)(double.Parse(value.ToString()) * 10);
                case 2: return (int)(double.Parse(value.ToString()) * 100);
                case 3: return (int)(double.Parse(value.ToString()) * 1000);
                default:
                    return (int)(double.Parse(value.ToString()));
            }
        }

        public IEnumerable<short> GetAllData()
        {
            foreach (var item in int.Parse(this.INTEGR_PWR.ToString()).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.I_PWR).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.VOLT_R).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.VOLT_T).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.CRRNT_R).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.CRRNT_T).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.LEAK_CRRNT).ToShort()) yield return item;

            foreach (var item in this.DoubleToInt(this.VAN_AB_VTHD).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.VAN_BC_VTHD).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.VAN_CA_VTHD).ToShort()) yield return item;

            foreach (var item in this.DoubleToInt(this.VAN_AB_ITDD).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.VAN_BC_ITDD).ToShort()) yield return item;
            foreach (var item in this.DoubleToInt(this.VAN_CA_ITDD).ToShort()) yield return item;
        }

        //public void SetData(byte[] buffer, BitHandler.ByteOrder order)
        //{
        //    try
        //    {
        //        var index = 0;

        //        this.State = (ElectMeterState)buffer.ToUInt16(ref index, order);
        //        this.Alarm = (ElectMeterAlarm)buffer.ToUInt16(ref index, order);
        //        this.EventCount1 = buffer.ToUInt16(ref index, order);
        //        this.EventCount2 = buffer.ToUInt16(ref index, order);
        //        this.Temperature = buffer.ToFloat(ref index, order);
        //        this.A_PhaseVoltage = buffer.ToFloat(ref index, order);
        //        this.B_PhaseVoltage = buffer.ToFloat(ref index, order);
        //        this.C_PhaseVoltage = buffer.ToFloat(ref index, order);
        //        this.PhaseVoltage = buffer.ToFloat(ref index, order);
        //        this.AB_LineVoltage = buffer.ToFloat(ref index, order);
        //        this.BC_LineVoltage = buffer.ToFloat(ref index, order);
        //        this.CA_LineVoltage = buffer.ToFloat(ref index, order);
        //        this.LineVoltage = buffer.ToFloat(ref index, order);
        //        this.Frequency = buffer.ToFloat(ref index, order);
        //        this.VanVabThdVoltage = buffer.ToFloat(ref index, order);
        //        this.VbnVbcThdVoltage = buffer.ToFloat(ref index, order);
        //        this.VcnVcaThdVoltage = buffer.ToFloat(ref index, order);
        //        this.Voltage1 = buffer.ToFloat(ref index, order);
        //        this.Voltage2 = buffer.ToFloat(ref index, order);
        //        this.Voltage3 = buffer.ToFloat(ref index, order);
        //        this.NEMA = buffer.ToFloat(ref index, order);
        //        this.NEMA1 = buffer.ToFloat(ref index, order);
        //        this.NEMA2 = buffer.ToFloat(ref index, order);
        //        this.A_CrestFactor = buffer.ToFloat(ref index, order);
        //        this.B_CrestFactor = buffer.ToFloat(ref index, order);
        //        this.C_CrestFactor = buffer.ToFloat(ref index, order);
        //        this.A_VoltagePhase = buffer.ToFloat(ref index, order);
        //        this.B_VoltagePhase = buffer.ToFloat(ref index, order);
        //        this.C_VoltagePhase = buffer.ToFloat(ref index, order);
        //        this.LeakageCharge = buffer.ToFloat(ref index, order);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(this, ex);
        //    }
        //}
    }
}
