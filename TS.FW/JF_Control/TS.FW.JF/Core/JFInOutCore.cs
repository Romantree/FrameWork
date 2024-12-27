using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_MOTION;

namespace TS.FW.JF.Core
{
    public class JFInOutCore
    {
        public bool IsConnect { get; private set; }
        public short InPortCount { get; private set; }
        public short OutPortCount { get; private set; }
        public IList<bool> InList { get; private set; } = new List<bool>();
        public IList<bool> OutList { get; private set; } = new List<bool>();


        public JFInOutCore()
        {
        }

        public Int64 GetXBits(short moduleno)
        {
            try
            {
                var refdata = 0;
                if (XMotionLib.get_option_io(moduleno, ref refdata) == 0)
                    return refdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        public Int64 GetYBits(short moduleno)
        {
            try
            {
                var refdata = 0;
                if (XMotionLib.get_option_out_io(moduleno, ref refdata) == 0)
                    return refdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        /// <summary>
        /// 확장 입출력 보드의 초기화 함수
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public MMC_STAT InitBoard()// JFDevice Device)
        {
            var iResult = XMotionLib.mmc_exio_initx();

            //IO Port Value 처리 List 생성
            if ((MMC_STAT)iResult == MMC_STAT.MMC_OK)
            {
                short incnt = 0, outcnt = 0;
                XMotionLib.get_exio_num(ref incnt, ref outcnt);
                InPortCount = incnt;
                OutPortCount = outcnt;

                for (int i = 0; i < InPortCount * 32; i++)
                    InList.Add(false);

                for (int i = 0; i < OutPortCount * 32; i++)
                    OutList.Add(false);

                IsConnect = true;

            }
            return (MMC_STAT)iResult;
        }

        //public void SetInOutList(Dictionary<int, string> inpoint, Dictionary<int, string> outpoint)
        //{
        //    ioSim.SetConfigInOut(inpoint, outpoint);
        //}
        #region JFC - Motion User IO 
        /// <summary>
        /// 범용 출력 PortNo 별로 동시 처리
        /// </summary>
        /// <param name="portno"></param>
        /// <param name="bits"></param>
        public MMC_STAT SetBitIO(Int16 portno, int bits)
        {
            var ret = XMotionLib.set_io(portno, bits);
            return (MMC_STAT)ret;
        }

        /// <summary>
        /// 범용 출력 1개 접점을 1 (High, 싱크출력 Off) 지정
        /// </summary>
        /// <param name="BitNo"></param>
        public MMC_STAT SetBitIO_OFF(int BitNo)
        {
            Int16 iNo = Convert.ToInt16(BitNo);
            var ret = XMotionLib.set_option_bit(iNo);
            return (MMC_STAT)ret;
        }
        /// <summary>
        /// 범용 출력 1개 접점을 0 (Low, 싱크출력 On) 지정
        /// </summary>
        /// <param name="BitNo"></param>
        public MMC_STAT ResetIO_ON(int BitNo)
        {
            Int16 iNo = Convert.ToInt16(BitNo);
            var ret = XMotionLib.reset_option_bit(iNo);
            return (MMC_STAT)ret;
        }
        /// <summary>
        /// 출력상태 확인
        /// </summary>
        /// <param name="BitNo"></param>
        /// <returns></returns>
        public bool GetYBitIO(int BitNo)
        {
            var ret = false;

            lock (OutList)
                ret = OutList[BitNo];

            return ret;
        }
        /// <summary>
        /// 입력상태 확인
        /// </summary>
        /// <param name="BitNo"></param>
        /// <returns></returns>
        public bool GetXBitIO(int BitNo)
        {
            var ret = false;

            lock (InList)
                ret = InList[BitNo];

            return ret;
        }

        public void SetXBitIO(int bitno)
        {
            //if (_Simulation == false) return;

            //var tmp = ioSim.GetInData(bitno);
            //ioSim.SetInData(bitno, !tmp);
        }

        public bool SetConfigInOut(Dictionary<int, string> inpoint, Dictionary<int, string> outpoint)
        {
            if (IsConnect == false) return false;

            return true;
        }
        #endregion 
    }
}
