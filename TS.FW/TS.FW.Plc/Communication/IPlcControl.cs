using System;
using System.Collections.Generic;
using TS.FW.Plc.Config;
using TS.FW.Plc.Dto;

namespace TS.FW.Plc.Communication
{
    /// <summary>
    /// PLC 통신 인터페이스
    /// </summary>
    internal interface IPlcControl
    {
        /// <summary>
        /// 통신 연결 상태 변경 이벤트
        /// </summary>
        event EventHandler<bool> OnConnectedChangedEvent;

        /// <summary>
        /// Tick 이벤트 발생
        /// </summary>
        event EventHandler<IPlcConfigBase> OnTickEvent;

        /// <summary>
        /// 연결 여부
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// 통신 연결
        /// </summary>
        /// <param name="config"></param>
        void Connect(IPlcConfigBase config);

        /// <summary>
        /// 통신 연결 해제
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 통신 환경설정 변경
        /// </summary>
        /// <param name="config"></param>
        void ChangeConfig(IPlcConfigBase config);

        /// <summary>
        /// Bit 1점 읽기
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        Response<PlcBit.Signal> ReadBit(PlcBit bit);

        /// <summary>
        /// 다수의 Bit 점수 읽기
        ///  - 연속되는 Bit 주소만 읽을 수 있음
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        ResponseList<PlcBit, PlcBit.Signal> ReadBitList(IEnumerable<PlcBit> list);

        /// <summary>
        /// 다수의 Bit 점수 읽기.
        ///  - 연속되는 Bit 주소만 읽을 수 있음
        ///  - 기존 ReadBit 함수 보다 더 많은 Bit을 읽을 수 있음
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        ResponseList<PlcBit, PlcBit.Signal> ReadBitFromWordCommand(IEnumerable<PlcBit> list);

        /// <summary>
        /// Word 1점 읽기
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        Response<object> ReadWord(PlcWord word);

        /// <summary>
        /// 다수의 Word 점수 읽기
        ///  - 연속되는 Word 주소만 읽을 수 있음
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        ResponseList<PlcWord, object> ReadWordList(IEnumerable<PlcWord> list);

        /// <summary>
        /// 다수의 Word 점수 읽기
        ///  - 통신 부하가 예상되는 함수
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        ResponseList<PlcWord, object> ReadRandomWord(IEnumerable<PlcWord> list);

        /// <summary>
        /// Bit 1점 쓰기
        /// </summary>
        /// <param name="bit"></param>
        /// <param name="signal"></param>
        /// <returns></returns>
        Response WriteBit(PlcBit bit, PlcBit.Signal signal);

        /// <summary>
        /// 다수의 Bit 점수 쓰기
        ///  - 연속되는 Bit 주소만 쓸수 있음.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Response WriteBitList(Dictionary<PlcBit, PlcBit.Signal> list);

        /// <summary>
        /// 다수의 Bit 점수 쓰기
        ///  - 연속되는 Bit 주소만 쓸수 있음.
        ///  - 기존 WriteBit 함수 보다 더 많은 Bit을 쓸수 있음.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Response WriteBitFromWordCommand(Dictionary<PlcBit, PlcBit.Signal> list);

        /// <summary>
        /// Word 1점 쓰기
        /// </summary>
        /// <param name="word"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Response WriteWord(PlcWord word, object value);

        /// <summary>
        /// 다수의 Word 점수 쓰기
        ///  - 연속되는 Word 주소만 쓸수 있음.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Response WriteWordList(Dictionary<PlcWord, object> list);

        /// <summary>
        /// 다수의 Bit 점수 쓰기
        ///  - 통신 부하가 예상 되는 함수
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Response WriteRandomBit(Dictionary<PlcBit, PlcBit.Signal> list);

        /// <summary>
        /// 다수의 Word 점수 쓰기
        ///  - 통신 부하가 예상 되는 함수
        ///  - Word(1), DWord(2) 크기의 점수만 쓸수 있음.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Response WriteRandomWord(Dictionary<PlcWord, object> list);
    }
}
