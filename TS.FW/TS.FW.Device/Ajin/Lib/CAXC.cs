﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device.Ajin.Lib
{
    public class CAXC
    {
        public const string DLL_PATH = "AXL.dll";

        //========== 보드 및 모듈 정보 
        // CNT 모듈이 있는지 확인
        [DllImport(DLL_PATH)] public static extern uint AxcInfoIsCNTModule(ref uint upStatus);

        // CNT 모듈 No 확인
        [DllImport(DLL_PATH)] public static extern uint AxcInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);

        // CNT 입출력 모듈의 개수 확인
        [DllImport(DLL_PATH)] public static extern uint AxcInfoGetModuleCount(ref int lpModuleCount);

        // 지정한 모듈의 카운터 입력 채널 개수 확인
        [DllImport(DLL_PATH)] public static extern uint AxcInfoGetChannelCount(int lModuleNo, ref int lpCount);

        // 시스템에 장착된 카운터의 전 채널 수를 확인
        [DllImport(DLL_PATH)] public static extern uint AxcInfoGetTotalChannelCount(ref int lpChannelCount);

        // 지정한 모듈 번호로 베이스 보드 번호, 모듈 위치, 모듈 ID 확인
        [DllImport(DLL_PATH)] public static extern uint AxcInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);

        // 해당 모듈이 제어가 가능한 상태인지 반환한다.
        [DllImport(DLL_PATH)] public static extern uint AxcInfoGetModuleStatus(int lModuleNo);

        [DllImport(DLL_PATH)] public static extern uint AxcInfoGetFirstChannelNoOfModuleNo(int lModuleNo, ref int lpChannelNo);

        // 카운터 모듈의 Encoder 입력 방식을 설정 합니다.
        // dwMethod --> 0x00 : Sign and pulse, x1 multiplication
        // dwMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
        // dwMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
        // dwMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
        // dwMethod --> 0x08 : Sign and pulse, x2 multiplication
        // dwMethod --> 0x09 : Increment and decrement pulses, x1 multiplication
        // dwMethod --> 0x0A : Increment and decrement pulses, x2 multiplication
        // SIO-CN2CH의 경우
        // dwMethod --> 0x00 : Up/Down 방식, A phase : 펄스, B phase : 방향
        // dwMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
        // dwMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
        // dwMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
        [DllImport(DLL_PATH)] public static extern uint AxcSignalSetEncInputMethod(int lChannelNo, uint dwMethod);

        // 카운터 모듈의 Encoder 입력 방식을 설정 합니다.
        // *dwpUpMethod --> 0x00 : Sign and pulse, x1 multiplication
        // *dwpUpMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
        // *dwpUpMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
        // *dwpUpMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
        // *dwpUpMethod --> 0x08 : Sign and pulse, x2 multiplication
        // *dwpUpMethod --> 0x09 : Increment and decrement pulses, x1 multiplication
        // *dwpUpMethod --> 0x0A : Increment and decrement pulses, x2 multiplication
        // SIO-CN2CH의 경우
        // dwMethod --> 0x00 : Up/Down 방식, A phase : 펄스, B phase : 방향
        // dwMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
        // dwMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
        // dwMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
        [DllImport(DLL_PATH)] public static extern uint AxcSignalGetEncInputMethod(int lChannelNo, ref uint dwpUpMethod);

        // 카운터 모듈의 트리거를 설정 합니다.
        // dwMode -->  0x00 : Latch
        // dwMode -->  0x01 : State
        // dwMode -->  0x02 : Special State    --> SIO-CN2CH 전용
        // SIO-CN2CH의 경우
        // dwMode -->  0x00 : 절대 위치 트리거 또는 주기 위치 트리거.
        // 주의 : 제품마다 기능이 서로 다르기 때문에 구별하여 사용 필요.
        // dwMode -->  0x01 : 시간 주기 트리거(AxcTriggerSetFreq로 설정)
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetFunction(int lChannelNo, uint dwMode);

        // 카운터 모듈의 트리거 설정을 확인 합니다.
        // *dwMode -->  0x00 : Latch
        // *dwMode -->  0x01 : State
        // *dwMode -->  0x02 : Special State
        // dwMode -->  0x00 : 절대 위치 트리거 또는 주기 위치 트리거.
        // 주의 : 제품마다 기능이 서로 다르기 때문에 구별하여 사용 필요.
        // dwMode -->  0x01 : 시간 주기 트리거(AxcTriggerSetFreq로 설정)
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetFunction(int lChannelNo, ref uint dwpMode);

        // dwUsage --> 0x00 : Trigger Not use
        // dwUsage --> 0x01 : Trigger use
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetNotchEnable(int lChannelNo, uint dwUsage);

        // *dwUsage --> 0x00 : Trigger Not use
        // *dwUsage --> 0x01 : Trigger use
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetNotchEnable(int lChannelNo, ref uint dwpUsage);


        // 카운터 모듈의 Captuer 극성을 설정 합니다.(External latch input polarity)
        // dwCapturePol --> 0x00 : Signal OFF -> ON
        // dwCapturePol --> 0x01 : Signal ON -> OFF
        [DllImport(DLL_PATH)] public static extern uint AxcSignalSetCaptureFunction(int lChannelNo, uint dwCapturePol);

        // 카운터 모듈의 Captuer 극성 설정을 확인 합니다.(External latch input polarity)
        // *dwCapturePol --> 0x00 : Signal OFF -> ON
        // *dwCapturePol --> 0x01 : Signal ON -> OFF
        [DllImport(DLL_PATH)] public static extern uint AxcSignalGetCaptureFunction(int lChannelNo, ref uint dwpCapturePol);

        // 카운터 모듈의 Captuer 위치를 확인 합니다.(External latch)
        // *dbpCapturePos --> Capture position 위치
        [DllImport(DLL_PATH)] public static extern uint AxcSignalGetCapturePos(int lChannelNo, ref double dbpCapturePos);

        // 카운터 모듈의 카운터 값을 확인 합니다.
        // *dbpActPos --> 카운터 값
        [DllImport(DLL_PATH)] public static extern uint AxcStatusGetActPos(int lChannelNo, ref double dbpActPos);

        // 카운터 모듈의 카운터 값을 dbActPos 값으로 설정 합니다.
        [DllImport(DLL_PATH)] public static extern uint AxcStatusSetActPos(int lChannelNo, double dbActPos);

        // 카운터 모듈의 트리거 위치를 설정합니다.
        // 카운터 모듈의 트리거 위치는 2개까지만 설정 할 수 있습니다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetNotchPos(int lChannelNo, double dbLowerPos, double dbUpperPos);

        // 카운터 모듈의 설정한 트리거 위치를 확인 합니다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetNotchPos(int lChannelNo, ref double dbpLowerPos, ref double dbpUpperPos);

        // 카운터 모듈의 트리거 출력을 강제로 합니다.
        // dwOutVal --> 0x00 : 트리거 출력 '0'
        // dwOutVal --> 0x01 : 트리거 출력 '1'
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetOutput(int lChannelNo, uint dwOutVal);

        // 카운터 모듈의 상태를 확인합니다.
        // Bit '0' : Carry (카운터 현재치가 덧셈 펄스에 의해 카운터 상한치를 넘어 0로 바뀌었을 때 1스캔만 ON으로 합니다.)
        // Bit '1' : Borrow (카운터 현재치가 뺄셈 펄스에 의해 0을 넘어 카운터 상한치로 바뀌었을 때 1스캔만 ON으로 합니다.)
        // Bit '2' : Trigger output status
        // Bit '3' : Latch input status
        [DllImport(DLL_PATH)] public static extern uint AxcStatusGetChannel(int lChannelNo, ref uint dwpChannelStatus);


        // SIO-CN2CH 전용 함수군
        //
        // 카운터 모듈의 위치 단위를 설정한다.
        // 실제 위치 이동량에 대한 펄스 갯수를 설정하는데,
        // ex) 1mm 이동에 1000가 필요하다면dMoveUnitPerPulse = 0.001로 입력하고,
        //     이후 모든 함수의 위치와 관련된 값을 mm 단위로 설정하면 된다.
        [DllImport(DLL_PATH)] public static extern uint AxcMotSetMoveUnitPerPulse(int lChannelNo, double dMoveUnitPerPulse);

        // 카운터 모듈의 위치 단위를 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcMotGetMoveUnitPerPulse(int lChannelNo, ref double dpMoveUnitPerPuls);

        // 카운터 모듈의 엔코더 입력 카운터를 반전 기능을 설정한다.
        // dwReverse --> 0x00 : 반전하지 않음.
        // dwReverse --> 0x01 : 반전.
        [DllImport(DLL_PATH)] public static extern uint AxcSignalSetEncReverse(int lChannelNo, uint dwReverse);

        // 카운터 모듈의 엔코더 입력 카운터를 반전 기능을 설정을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcSignalGetEncReverse(int lChannelNo, ref uint dwpReverse);

        // 카운터 모듈의 Encoder 입력 신호를 선택한다.
        // dwSource -->  0x00 : 2(A/B)-Phase 신호.
        // dwSource -->  0x01 : Z-Phase 신호.(방향성 없음.)
        [DllImport(DLL_PATH)] public static extern uint AxcSignalSetEncSource(int lChannelNo, uint dwSource);

        // 카운터 모듈의 Encoder 입력 신호 선택 설정을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcSignalGetEncSource(int lChannelNo, ref uint dwpSource);

        // 카운터 모듈의 트리거 출력 범위 중 하한 값을 설정한다.
        // 위치 주기 트리거 제품의 경우 위치 주기로 트리거 출력을 발생시킬 범위 중 하한 값을 설정한다.
        // 절대 위치 트리거 제품의 경우 Ram 시작 번지의 트리거 정보의 적용 기준 위치를 설정한다.
        // 단위 : AxcMotSetMoveUnitPerPulse로 설정한 단위.
        // Note : 하한값은 상한값보다 작은값을 설정하여야 합니다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetBlockLowerPos(int lChannelNo, double dLowerPosition);

        // 카운터 모듈의 트리거 출력 범위 중 하한 값을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetBlockLowerPos(int lChannelNo, ref double dpLowerPosition);

        // 카운터 모듈의 트리거 출력 범위 중 상한 값을 설정한다.
        // 위치 주기 트리거 제품의 경우 위치 주기로 트리거 출력을 발생시킬 범위 중 상한 값을 설정한다.
        // 절대 위치 트리거 제품의 경우 트리거 정보가 설정된 Ram 의 마지막 번지의 트리거 정보가 적용되는 위치로 사용된다.
        // 단위 : AxcMotSetMoveUnitPerPulse로 설정한 단위.
        // Note : 상한값은 하한값보다 큰값을 설정하여야 합니다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetBlockUpperPos(int lChannelNo, double dUpperPosition);
        // 카운터 모듈의 트리거 출력 범위 중 하한 값을 설정한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetBlockUpperPos(int lChannelNo, ref double dpUpperrPosition);

        // 카운터 모듈의 위치 주기 모드 트리거에 사용되는 위치 주기를 설정한다.
        // 단위 : AxcMotSetMoveUnitPerPulse로 설정한 단위.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetPosPeriod(int lChannelNo, double dPeriod);

        // 카운터 모듈의 위치 주기 모드 트리거에 사용되는 위치 주기를 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetPosPeriod(int lChannelNo, ref double dpPeriod);

        // 카운터 모듈의 위치 주기 모드 트리거 사용시 위치 증감에 대한 유효기능을 설정한다.
        // dwDirection -->  0x00 : 카운터의 증/감에 대하여 트리거 주기 마다 출력.
        // dwDirection -->  0x01 : 카운터가 증가 할때만 트리거 주기 마다 출력.
        // dwDirection -->  0x01 : 카운터가 감소 할때만 트리거 주기 마다 출력.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetDirectionCheck(int lChannelNo, uint dwDirection);
        // 카운터 모듈의 위치 주기 모드 트리거 사용시 위치 증감에 대한 유효기능 설정을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetDirectionCheck(int lChannelNo, ref uint dwpDirection);

        // 카운터 모듈의 위치 주기 모드 트리거 기능의 범위, 위치 주기를 한번에 설정한다.
        // 위치 설정 단위 :  AxcMotSetMoveUnitPerPulse로 설정한 단위.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetBlock(int lChannelNo, double dLower, double dUpper, double dABSod);

        // 카운터 모듈의 위치 주기 모드 트리거 기능의 범위, 위치 주기를 설정을 한번에 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetBlock(int lChannelNo, ref double dpLower, ref double dpUpper, ref double dpABSod);

        // 카운터 모듈의 트리거 출력 펄스 폭을 설정한다.
        // 단위 : uSec
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetTime(int lChannelNo, double dTrigTime);

        // 카운터 모듈의 트리거 출력 펄스 폭 설정을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetTime(int lChannelNo, ref double dpTrigTime);

        // 카운터 모듈의 트리거 출력 펄스의 출력 레벨을 설정한다.
        // dwLevel -->  0x00 : 트리거 출력시 'Low' 레벨 출력.
        // dwLevel -->  0x00 : 트리거 출력시 'High' 레벨 출력.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetLevel(int lChannelNo, uint dwLevel);
        // 카운터 모듈의 트리거 출력 펄스의 출력 레벨 설정을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetLevel(int lChannelNo, ref uint dwpLevel);

        // 카운터 모듈의 주파수 트리거 출력 기능에 필요한 주파수를 설정한다.
        // 단위 : Hz, 범위 : 1Hz ~ 500 kHz
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetFreq(int lChannelNo, uint dwFreqency);
        // 카운터 모듈의 주파수 트리거 출력 기능에 필요한 주파수를 설정을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetFreq(int lChannelNo, ref uint dwpFreqency);

        // 카운터 모듈의 지정 채널에 대한 범용 출력 값을 설정한다.
        // dwOutput 범위 : 0x00 ~ 0x0F, 각 채널당 4개의 범용 출력
        [DllImport(DLL_PATH)] public static extern uint AxcSignalWriteOutput(int lChannelNo, uint dwOutput);

        // 카운터 모듈의 지정 채널에 대한 범용 출력 값을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcSignalReadOutput(int lChannelNo, ref uint dwpOutput);

        // 카운터 모듈의 지정 채널에 대한 범용 출력 값을 비트 별로 설정한다.
        // lBitNo 범위 : 0 ~ 3, 각 채널당 4개의 범용 출력
        [DllImport(DLL_PATH)] public static extern uint AxcSignalWriteOutputBit(int lChannelNo, int lBitNo, uint uOnOff);
        // 카운터 모듈의 지정 채널에 대한 범용 출력 값을 비트 별로 확인 한다.
        // lBitNo 범위 : 0 ~ 3
        [DllImport(DLL_PATH)] public static extern uint AxcSignalReadOutputBit(int lChannelNo, int lBitNo, ref uint upOnOff);

        // 카운터 모듈의 지정 채널에 대한 범용 입력 값을 확인한다.
        [DllImport(DLL_PATH)] public static extern uint AxcSignalReadInput(int lChannelNo, ref uint dwpInput);

        // 카운터 모듈의 지정 채널에 대한 범용 입력 값을 비트 별로 확인 한다.
        // lBitNo 범위 : 0 ~ 3
        [DllImport(DLL_PATH)] public static extern uint AxcSignalReadInputBit(int lChannelNo, int lBitNo, ref uint upOnOff);

        // 카운터 모듈의 트리거 출력을 활성화 한다.
        // 현재 설정된 기능에 따라 트리거 출력이 최종적으로 출력할 것인지 설정한다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetEnable(int lChannelNo, uint dwUsage);

        // 카운터 모듈의 트리거 출력 활설화 설정 내용을 확인하다.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerGetEnable(int lChannelNo, ref uint dwpUsage);

        // 카운터 모듈의 절대위치 트리거 기능을 위해 설정된 RAM 내용을 확인한다.
        // dwAddr 범위 : 0x0000 ~ 0x1FFFF;
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerReadAbsRamData(int lChannelNo, uint dwAddr, ref uint dwpData);

        // 카운터 모듈의 절대위치 트리거 기능을 위해 필요한 RAM 내용을 설정한다.
        // dwAddr 범위 : 0x0000 ~ 0x1FFFF;
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerWriteAbsRamData(int lChannelNo, uint dwAddr, uint dwData);

        // 카운터 모듈의 절대위치 트리거 기능을 위해 필요한 RAM 내용을 한번에 설정한다.
        // dwTrigNum 범위 : ~ 0x20000, 
        // dwDirection --> 0x0 : 하한 트리거 위치에 대한 트리거 정보 부터 입력. 위치가 증가하는 방향으로 트리거 출력시 사용.
        // dwDirection --> 0x1 : 상한 카운터에 대한 트리거 정보 부터 입력. 위치가 감소하는 방향으로 트리거 출력시 사용.
        [DllImport(DLL_PATH)] public static extern uint AxcTriggerSetAbs(int lChannelNo, uint dwTrigNum, ref uint dwTrigPos, uint dwDirection);
    }
}
