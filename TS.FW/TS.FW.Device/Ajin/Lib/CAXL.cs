﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device.Ajin.Lib
{
    public class CAXL
    {
        public const string DLL_PATH = "AXL.dll";

        //========== 라이브러리 초기화 ========================================================================

        // 라이브러리 초기화
        [DllImport(DLL_PATH)] public static extern uint AxlOpen(int lIrqNo);
        // 라이브러리 초기화시 하드웨어 칩에 리셋을 하지 않음.
        [DllImport(DLL_PATH)] public static extern uint AxlOpenNoReset(uint lIrqNo);
        // 라이브러리 사용을 종료
        [DllImport(DLL_PATH)] public static extern int AxlClose();
        // 라이브러리가 초기화 되어 있는 지 확인
        [DllImport(DLL_PATH)] public static extern int AxlIsOpened();

        // 인터럽트를 사용한다.
        [DllImport(DLL_PATH)] public static extern uint AxlInterruptEnable();
        // 인터럽트를 사용안한다.
        [DllImport(DLL_PATH)] public static extern uint AxlInterruptDisable();

        //========== 라이브러리 및 베이스 보드 정보 ===========================================================

        // 등록된 베이스 보드의 개수 확인
        [DllImport(DLL_PATH)] public static extern uint AxlGetBoardCount(ref int lpBoardCount);
        // 라이브러리 버전 확인
        [DllImport(DLL_PATH)] public static extern uint AxlGetLibVersion(ref byte szVersion);
        // Network제품의 각 모듈별 연결상태를 확인하는 함수
        [DllImport(DLL_PATH)] public static extern uint AxlGetModuleNodeStatus(int nBoardNo, int nModulePos);
        // 해당 보드가 제어 가능한 상태인지 반환한다.
        [DllImport(DLL_PATH)] public static extern uint AxlGetBoardStatus(int nBoardNo);
        // Network 제품의 Configuration Lock 상태를 반환한다.
        // *wpLockMode  : DISABLE(0), ENABLE(1)
        [DllImport(DLL_PATH)] public static extern uint AxlGetLockMode(int nBoardNo, ref uint upLockMode);

        [DllImport(DLL_PATH)]
        public static extern uint AxlSetLockMode(int nBoardNo, uint upLockMode);

        // Network 제품의 ScanTime 상태를 설정한다.
        [DllImport(DLL_PATH)]
        public static extern uint AxlSetNetComTime(int nBoardNo, byte szNetComTime);

        // Network 제품의 ScanTime 상태를 반환한다.
        [DllImport(DLL_PATH)]
        public static extern uint AxlGetNetComTime(int nBoardNo, ref byte szNetComTime);
        //========= 로그 레벨 =================================================================================

        // EzSpy에 출력할 메시지 레벨 설정
        // uLevel : 0 - 3 설정
        // LEVEL_NONE(0)    : 모든 메시지를 출력하지 않는다.
        // LEVEL_ERROR(1)   : 에러가 발생한 메시지만 출력한다.
        // LEVEL_RUNSTOP(2) : 모션에서 Run / Stop 관련 메시지를 출력한다.
        // LEVEL_FUNCTION(3): 모든 메시지를 출력한다.
        [DllImport(DLL_PATH)] public static extern uint AxlSetLogLevel(uint uLevel);
        // EzSpy에 출력할 메시지 레벨 확인
        [DllImport(DLL_PATH)] public static extern uint AxlGetLogLevel(ref uint upLevel);

        //========== MLIII =================================================================================
        // Network제품의 각 모듈을 검색을 시작하는 함수
        [DllImport(DLL_PATH)] public static extern uint AxlScanStart(int lBoardNo, long lNet);
        // Network제품 각 보드의 모든 모듈을 connect하는 함수
        [DllImport(DLL_PATH)] public static extern uint AxlBoardConnect(int lBoardNo, long lNet);
        // Network제품 각 보드의 모든 모듈을 Disconnect하는 함수
        [DllImport(DLL_PATH)] public static extern uint AxlBoardDisconnect(int lBoardNo, long lNet);
    }
}
