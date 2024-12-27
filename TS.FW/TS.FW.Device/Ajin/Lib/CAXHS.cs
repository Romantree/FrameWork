using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device.Ajin.Lib
{
    public class CAXHS
    {
        public delegate void AXT_INTERRUPT_PROC(int nActiveNo, uint uFlag);

        public readonly static uint WM_USER = 0x0400;
        public readonly static uint WM_AXL_INTERRUPT = (WM_USER + 1001);
        public readonly static uint MAX_SERVO_ALARM_HISTORY = 15;

        public static int AXIS_EVN(int nAxisNo)
        {
            nAxisNo = (nAxisNo - (nAxisNo % 2));                // 쌍을 이루는 축의 짝수축을 찾음
            return nAxisNo;
        }

        public static int AXIS_ODD(int nAxisNo)
        {
            nAxisNo = (nAxisNo + ((nAxisNo + 1) % 2));          // 쌍을 이루는 축의 홀수축을 찾음
            return nAxisNo;
        }

        public static int AXIS_QUR(int nAxisNo)
        {
            nAxisNo = (nAxisNo % 4);                            // 쌍을 이루는 축의 홀수축을 찾음
            return nAxisNo;
        }

        public static int AXIS_N04(int nAxisNo, int nPos)
        {
            nAxisNo = (((nAxisNo / 4) * 4) + nPos);             // 한 칩의 축 위치로 변경(0~3)
            return nAxisNo;
        }

        public static int AXIS_N01(int nAxisNo)
        {
            nAxisNo = ((nAxisNo % 4) >> 2);                     // 0, 1축을 0으로 2, 3축을 1로 변경
            return nAxisNo;
        }

        public static int AXIS_N02(int nAxisNo)
        {
            nAxisNo = ((nAxisNo % 4) % 2);                       // 0, 2축을 0으로 1, 3축을 1로 변경
            return nAxisNo;
        }

        public static int m_SendAxis = 0;           // 현재 축번호

        public const int F_50M_CLK = 50000000;    /* 50.000 MHz */
    }

    public struct MOTION_INFO
    {
        public double dCmdPos;      // Command 위치[0x01]
        public double dActPos;      // Encoder 위치[0x02]
        public uint uMechSig;       // Mechanical Signal[0x04]
        public uint uDrvStat;       // Driver Status[0x08]
        public uint uInput;         // Universal Signal Input[0x10]
        public uint uOutput;        // Universal Signal Output[0x10]
        public uint uMask;          // 읽기 설정 Mask Ex) 0x1F, 모든정보 읽기
    }
}
