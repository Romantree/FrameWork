using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Plc.Communication;
using TS.FW.Plc.Config;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;
using TS.FW.Utils;

namespace TS.FW.Plc
{
    public class PlcControlManager
    {
        /// <summary>
        /// 싱글톤
        /// </summary>
        public static PlcControlManager Ins;

        // 전역 생성자
        static PlcControlManager()
        {
            Ins = new PlcControlManager();
        }

        // 통신 관리 리스트
        private readonly Dictionary<IPlcConfigBase, IPlcControl> _configList = new Dictionary<IPlcConfigBase, IPlcControl>();

        /// <summary>
        /// PLC 통신 연결 이벤트
        /// </summary>
        public event EventHandler OnConnectedEvent;

        /// <summary>
        /// PLC 통신 연결 해제 이벤트
        /// </summary>
        public event EventHandler OnDisconnectedEvent;

        /// <summary>
        /// Tick 발생 이벤트
        /// </summary>
        public event EventHandler<IPlcConfigBase> OnTickEvent;

        private PlcControlManager() { }

        #region 함수

        /// <summary>
        /// PLC 통신 시작
        /// </summary>
        /// <param name="config"></param>
        public void Start(IPlcConfigBase config)
        {
            try
            {
                if (config == null) throw new NullReferenceException("PLC 환경 정보가 NULL 입니다.");

                if (this._configList.ContainsKey(config))
                {
                    var updateControl = this._configList[config];
                    if (updateControl == null) throw new NullReferenceException(string.Format("PLC 통신 Control이 NULL 입니다. {0}", config));

                    updateControl.ChangeConfig(config);
                    return;
                }

                var control = this.CreatePlcControl(config);
                if (control == null) throw new TypeAccessException(string.Format("정의 되지 않은 PLC 정보 입니다. {0}", config));

                control.OnTickEvent += Control_OnTickEvent;
                control.OnConnectedChangedEvent += Control_OnConnectedChangedEvent;
                control.Connect(config);

                this._configList.Add(config, control);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        /// <summary>
        /// PLC 통신 중지
        /// </summary>
        /// <param name="config"></param>
        public void Stop(IPlcConfigBase config)
        {
            try
            {
                if (config == null) throw new NullReferenceException("PLC 환경 정보가 NULL 입니다.");

                if (this._configList.ContainsKey(config) == false) return;

                var control = this._configList[config];
                if (control != null)
                {
                    control.OnTickEvent -= Control_OnTickEvent;
                    control.OnConnectedChangedEvent -= Control_OnConnectedChangedEvent;
                    control.DisConnect();

                    this.OnDisconnectedEvent?.Invoke(config, new EventArgs());
                }

                this._configList.Remove(config);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        /// <summary>
        /// PLC 통신 중지
        /// </summary>
        public void Stop()
        {
            try
            {
                Logger.Write(this);

                foreach (var config in this._configList.ToList())
                {
                    this.Stop(config.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 등록 된 PLC 통신 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public List<IPlcConfigBase> GetPlcConfig()
        {
            try
            {
                return this._configList.Keys.ToList();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 등록 된 PLC 통신 정보 가져오기
        /// </summary>
        /// <param name="moudleID"></param>
        /// <returns></returns>
        public IPlcConfigBase GetPlcConfig(int plcNo)
        {
            try
            {
                return this._configList.FirstOrDefault(t => t.Key == plcNo).Key;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 등록 된 PLC 통신 연결 여부 확인
        /// </summary>
        /// <param name="plcNo"></param>
        /// <returns></returns>
        public bool IsConnected(int plcNo)
        {
            try
            {
                var config = this.GetPlcControl(plcNo);
                if (config.HasValue == false || config.Value.Value == null) return false;

                return config.Value.Value.Connected;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        // 주소 그룹핑 알고리즘
        private IEnumerable<List<int>> ToPlcAddressGroupList(List<int> tList, int maxCount)
        {
            var sAddress = tList.Min(); // 시작 주소
            var eAddress = sAddress + maxCount; // 종료 주소

            for (int i = 0; i < tList.Count; i++)
            {
                var uList = new List<int>(); // 주소 단위 목록

                for (int k = i; k < (i + maxCount); k++)
                {
                    if (k >= tList.Count || k == ((i + maxCount) - 1))
                    {
                        if (k < tList.Count) uList.Add(tList[k]);

                        yield return uList;

                        if (k == ((i + maxCount) - 1))
                        {
                            sAddress = tList[k + 1];
                            eAddress = sAddress + maxCount;
                        }

                        i = k;
                        break;
                    }
                    else if (tList[k] < eAddress)
                    {
                        uList.Add(tList[k]);
                    }
                    else
                    {
                        yield return uList;

                        sAddress = tList[k];
                        eAddress = sAddress + maxCount;

                        i = k - 1;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 미쯔비시 이터넷 통신 전용
        /// </summary>
        /// <param name="plcNo"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public ResponseList<PlcBit, PlcBit.Signal> ReadBitAll(int plcNo, List<PlcBit> list, int maxCount = 7000)
        {
            try
            {
                if (list == null || list.Count <= 0) return new ResponseList<PlcBit, PlcBit.Signal>();

                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                if (config.PlcType != PlcTypes.Ethernet) return new ResponseList<PlcBit, PlcBit.Signal>();

                var control = item.Value.Value as PlcEthernetControl;
                if (control.Connected == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "");

                var result = new Dictionary<PlcBit, PlcBit.Signal>();
                //var gList = list.GroupBy(t => t.Address / maxCount).ToList();
                var gList = this.ToPlcAddressGroupList(list.Select(t => t.Address).OrderBy(t => t).ToList(), maxCount).ToList();

                foreach (var gItem in gList)
                {
                    var address = gItem.Min();
                    var count = (gItem.Max() - address) + 1;

                    var res = control.ReadBitList(address, count);
                    if (res == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, res.Comment);

                    foreach (var data in list.Join(res.Result, x => x.Address, y => y.Key, (x, y) => new { PlcBit = x, OnOff = y.Value }))
                    {
                        result.Add(data.PlcBit, data.OnOff);
                    }

                    if (gList.Count > 1) System.Threading.Thread.Sleep(30);
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 미쯔비시 이터넷 통신 전용
        /// </summary>
        /// <param name="plcNo"></param>
        /// <param name="code"></param>
        /// <param name="list"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public ResponseList<PlcWord, object> ReadWordAll(int plcNo, PlcDeviceCode code, List<PlcWord> list, int maxCount = 960)
        {
            try
            {
                if (list == null || list.Count <= 0) return new ResponseList<PlcWord, object>();

                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new ResponseList<PlcWord, object>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                if (config.PlcType != PlcTypes.Ethernet) return new ResponseList<PlcWord, object>();

                var control = item.Value.Value as PlcEthernetControl;
                if (control.Connected == false) return new ResponseList<PlcWord, object>(false, "");

                //var gList = this.ToAddressList(list).GroupBy(t => t / maxCount).ToList();
                var gList = this.ToPlcAddressGroupList(this.ToAddressList(list), maxCount).ToList();

                var result = new Dictionary<PlcWord, object>();

                foreach (var gItem in gList)
                {
                    var address = gItem.Min();
                    var count = (gItem.Max() - address) + 1;

                    var res = control.ReadWordList(address, count, code, list);
                    if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);

                    foreach (var data in res.Result)
                    {
                        result.Add(data.Key, data.Value);
                    }

                    if (gList.Count > 1) System.Threading.Thread.Sleep(30);
                }

                return new ResponseList<PlcWord, object>(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// 미쯔비시 이터넷 통신 전용
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="wordName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Response WriteWord(int plcNo, int address, byte[] data)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                if (config.PlcType != PlcTypes.Ethernet) return new Response();

                var control = item.Value.Value as PlcEthernetControl;
                if (control.Connected == false) return new Response(false, "");

                return control.WriteWord(address, data);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private List<int> ToAddressList(List<PlcWord> list)
        {
            return this._ToAddressList(list).Distinct().OrderBy(t => t).ToList();
        }

        private IEnumerable<int> _ToAddressList(List<PlcWord> list)
        {
            foreach (var item in list)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    yield return item.Address + i;
                }
            }
        }

        #region Read

        /// <summary>
        /// Bit 정보 읽어오기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="bitName"></param>
        /// <returns></returns>
        public Response<PlcBit.Signal> ReadBit(int plcNo, string bitName, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithBit(plcNo, bitName, false);
                if (item == false) return new Response<PlcBit.Signal>(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new Response<PlcBit.Signal>(false, "");

                var bit = item.Value;

                return control.ReadBit(bit.ToPlcBitAddressOffset(offset));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        ///  Bit 정보 읽어오기
        ///  - 시작 점 주소 ~ 끝점 주소
        ///  - 연속 되는 주소로 그룹화 해서 여려번 읽음.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public ResponseList<PlcBit, PlcBit.Signal> ReadBitList(int plcNo, int offset = 0, params PlcBit[] parms)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var control = item.Value.Value;
                if (control.Connected == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "");

                var list = new Dictionary<PlcBit, PlcBit.Signal>();

                // 연속 되는 주소 그룹화
                foreach (var bitList in parms.ToContinuousList(t => t.Address, t => t.Address))
                {
                    var res = control.ReadBitList(bitList.Select(t => t.ToPlcBitAddressOffset(offset)));
                    if (res == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, res.Comment);

                    foreach (var bit in res.Result)
                    {
                        list.Add(bit.Key, bit.Value);
                    }
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        ///  Bit 정보 읽어오기
        ///  - 시작 점 주소 ~ 끝점 주소
        ///  - 연속 되는 주소로 그룹화 해서 여려번 읽음.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="fromBitName"></param>
        /// <param name="toBitName"></param>
        public ResponseList<PlcBit, PlcBit.Signal> ReadBitListToBetween(int plcNo, string fromBitName, string toBitName, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithBitListToBetween(plcNo, fromBitName, toBitName, false);
                if (item == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "");

                var list = new Dictionary<PlcBit, PlcBit.Signal>();

                // 연속 되는 주소 그룹화
                foreach (var bitList in item.Value.ToContinuousList(t => t.Address, t => t.Address))
                {
                    var res = control.ReadBitList(bitList.Select(t => t.ToPlcBitAddressOffset(offset)));
                    if (res == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, res.Comment);

                    foreach (var bit in res.Result)
                    {
                        list.Add(bit.Key, bit.Value);
                    }
                }

                return new ResponseList<PlcBit, PlcBit.Signal>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        ///  Bit 정보 읽어오기
        ///  - 시작 점 주소 ~ 끝점 주소
        ///  - 입력된 Count 만큼 Bit 읽어오기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="fromBitName"></param>
        /// <param name="toBitName"></param>
        public ResponseList<PlcBit, PlcBit.Signal> ReadBitListToBetweenFromWordCommand(int plcNo, string fromBitName, string toBitName, int offset = 0, int count = 0xFF)
        {
            try
            {
                var item = this.GetPlcControlWithBitListToBetween(plcNo, fromBitName, toBitName, false);
                if (item == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "");

                var list = new Dictionary<PlcBit, PlcBit.Signal>();

                foreach (var bitList in item.Value.ToPageList(count))
                {
                    foreach (var wordList in bitList.ToContinuousList(t => t.Address, t => t.Address))
                    {
                        var res = control.ReadBitFromWordCommand(wordList.Select(t => t.ToPlcBitAddressOffset(offset)));
                        if (res == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, res.Comment);

                        foreach (var bit in res.Result)
                        {
                            list.Add(bit.Key, bit.Value);
                        }
                    }

                }

                return new ResponseList<PlcBit, PlcBit.Signal>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<PlcBit, PlcBit.Signal> ReadInBitAllFromWordCommand(int plcNo)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                var control = item.Value.Value;
                if (control.Connected == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, "");

                var res = control.ReadBitFromWordCommand(config.InBitList.Select(t => t.Value));
                if (res == false) return new ResponseList<PlcBit, PlcBit.Signal>(false, res.Comment);

                return new ResponseList<PlcBit, PlcBit.Signal>(res.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Word 정보 읽어오기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="wordName"></param>
        /// <returns></returns>
        public Response<object> ReadWord(int plcNo, string wordName, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithWord(plcNo, wordName, false);
                if (item == false) return new Response<object>(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new Response<object>(false, "");

                var word = item.Value;

                return control.ReadWord(word.ToPlcWordAddressOffset(offset));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        ///  Word 정보 읽어오기
        ///  - 시작 점 주소 ~ 끝점 주소
        ///  - 연속 되는 주소로 그룹화 해서 여려번 읽음.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public ResponseList<PlcWord, object> ReadWordList(int plcNo, int offset = 0, params PlcWord[] parms)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new ResponseList<PlcWord, object>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var control = item.Value.Value;
                if (control.Connected == false) return new ResponseList<PlcWord, object>(false, "");

                var list = new Dictionary<PlcWord, object>();

                // 연속 되는 주소 그룹화
                foreach (var group in parms.GroupBy(t => t.Code))
                {
                    foreach (var wordList in group.ToContinuousList(t => t.Address, t => t.Address + (t.Length - 1)))
                    {
                        var res = control.ReadWordList(wordList.Select(t => t.ToPlcWordAddressOffset(offset)));
                        if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);

                        foreach (var bit in res.Result)
                        {
                            list.Add(bit.Key, bit.Value);
                        }
                    }
                }

                return new ResponseList<PlcWord, object>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        ///  Word 정보 읽어오기
        ///  - 시작 점 주소 ~ 끝점 주소
        ///  - 연속 되는 주소로 그룹화 해서 여려번 읽음.
        /// </summary>
        /// <param name="mouldeID"></param>
        /// <param name="fromWordName"></param>
        /// <param name="toWordName"></param>
        public ResponseList<PlcWord, object> ReadWordListToBetween(int plcNo, string fromWordName, string toWordName, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithWordListToBetween(plcNo, fromWordName, toWordName, false);
                if (item == false) return new ResponseList<PlcWord, object>(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new ResponseList<PlcWord, object>(false, "");

                var list = new Dictionary<PlcWord, object>();

                // 연속 되는 주소 그룹화
                foreach (var group in item.Value.GroupBy(t => t.Code))
                {
                    foreach (var wordList in group.ToContinuousList(t => t.Address, t => t.Address + (t.Length - 1)))
                    {
                        var res = control.ReadWordList(wordList.Select(t => t.ToPlcWordAddressOffset(offset)));
                        if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);

                        foreach (var word in res.Result)
                        {
                            list.Add(word.Key, word.Value);
                        }
                    }
                }

                return new ResponseList<PlcWord, object>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// 랜덤 Word 점수 읽어오기
        ///  TODO : Word(1) DWord(2) 사이즈의 아이템만 읽어 올 수 있음.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="offset"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public ResponseList<PlcWord, object> ReadRandomWord(int plcNo, int offset = 0, params PlcWord[] parms)
        {
            try
            {
                //폐기덤퍼 데이터로 인한 랜덤 워드 수정

                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new ResponseList<PlcWord, object>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var control = item.Value.Value;
                if (control.Connected == false) return new ResponseList<PlcWord, object>(false, "");

                var list = new Dictionary<PlcWord, object>();

                foreach (var group in parms.ToLengthList(t => t.Length, 192))
                {
                    var res = control.ReadRandomWord(group.Select(t => t.ToPlcWordAddressOffset(offset)));
                    if (res == false) return new ResponseList<PlcWord, object>(false, res.Comment);

                    foreach (var bit in res.Result)
                    {
                        list.Add(bit.Key, bit.Value);
                    }
                }

                return new ResponseList<PlcWord, object>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// 랜덤 Word 점수 읽어오기
        ///  TODO : Word(1) DWord(2) 사이즈의 아이템만 읽어 올 수 있음.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="fromWordName"></param>
        /// <param name="toWordName"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ResponseList<PlcWord, object> ReadRandomWordToBetween(int plcNo, string fromWordName, string toWordName, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithWordListToBetween(plcNo, fromWordName, toWordName, false);
                if (item == false) return new ResponseList<PlcWord, object>(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new ResponseList<PlcWord, object>(false, "");

                return control.ReadRandomWord(item.Value.Select(t => t.ToPlcWordAddressOffset(offset)));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        #endregion

        #region Wirte

        /// <summary>
        /// Bit 정보 입력하기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="bitName"></param>
        /// <param name="signal"></param>
        /// <returns></returns>
        public Response WriteBit(int plcNo, string bitName, PlcBit.Signal signal, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithBit(plcNo, bitName, true);
                if (item == false) return new Response<PlcBit.Signal>(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new Response(false, "");

                var bit = item.Value;

                return control.WriteBit(bit.ToPlcBitAddressOffset(offset), signal);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// Bit 정보 입력하기
        ///  - 연속 되는 주소로 그룹화 해서 여려번 입력.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Response WriteBitList(int plcNo, int offset = 0, params PlcWirteBitModel[] parms)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var control = item.Value.Value;
                if (control.Connected == false) return new Response(false, "");

                var list = parms.ToContinuousList(t => t.Address, t => t.Address).ToList();

                foreach (var bitList in list)
                {
                    var res = control.WriteBitList(bitList.ToDictionary(t => t.ToPlcBitAddressOffset(offset), t => t.WirteSignal));
                    if (res == false) return new Response(false, res.Comment);

                    if (list.Count <= 0) System.Threading.Thread.Sleep(30);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        ///  Bit 정보 입력하기
        ///  - 시작 점 주소 ~ 끝점 주소
        ///  - 연속 되는 주소로 그룹화 해서 여려번 입력.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="fromBitName"></param>
        /// <param name="toBitName"></param>
        public Response WriteBitListToBetween(int plcNo, string fromBitName, string toBitName, PlcBit.Signal signal, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithBitListToBetween(plcNo, fromBitName, toBitName, true);
                if (item == false) return new Response(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new Response(false, "");

                foreach (var bitList in item.Value.ToContinuousList(t => t.Address, t => t.Address))
                {
                    var res = control.WriteBitList(bitList.ToDictionary(t => t.ToPlcBitAddressOffset(offset), t => signal));
                    if (res == false) return new Response(false, res.Comment);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        ///  Bit 정보 입력하기
        ///  - 시작 점 주소 ~ 끝점 주소
        ///  - 입력된 Count 만큼 Bit 입력
        ///  TODO : 관리되지 않는 영역에 데이터 입력 될 수 있음.. 사용시 주위 요망.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="fromBitName"></param>
        /// <param name="toBitName"></param>
        public Response WriteBitListToBetweenFromWordCommand(int plcNo, string fromBitName, string toBitName, PlcBit.Signal signal, int offset = 0, int count = 0xFF)
        {
            try
            {
                var item = this.GetPlcControlWithBitListToBetween(plcNo, fromBitName, toBitName, true);
                if (item == false) return new Response(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new Response(false, "");

                foreach (var bitList in item.Value.ToPageList(count))
                {
                    var res = control.WriteBitFromWordCommand(bitList.ToDictionary(t => t.ToPlcBitAddressOffset(offset), t => signal));
                    if (res == false) return new Response(false, res.Comment);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// Word 정보 입력하기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="wordName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Response WriteWord(int plcNo, string wordName, object value, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithWord(plcNo, wordName, true);
                if (item == false) return new Response(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new Response(false, "");

                var word = item.Value;

                return control.WriteWord(word.ToPlcWordAddressOffset(offset), value);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// Word 정보 입력하기
        ///  - 연속 되는 주소로 그룹화 해서 여려번 입력.
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public Response WriteWordList(int plcNo, int offset = 0, params PlcWirteWordModel[] parms)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var control = item.Value.Value;
                if (control.Connected == false) return new Response(false, "");

                foreach (var group in parms.GroupBy(t => t.Code))
                {
                    foreach (var wordList in group.ToContinuousList(t => t.Address, t => t.Address + (t.Length - 1)))
                    {
                        var res = control.WriteWordList(wordList.ToDictionary(t => t.ToPlcWordAddressOffset(offset), t => t.WirteValue));
                        if (res == false) return new Response(false, res.Comment);
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// 랜덤 Bit 정보 쓰기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="offset"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public Response WriteRandomBitList(int plcNo, int offset = 0, params PlcWirteBitModel[] parms)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var control = item.Value.Value;
                if (control.Connected == false) return new Response(false, "");

                return control.WriteRandomBit(parms.ToDictionary(t => t.ToPlcBitAddressOffset(offset), t => t.WirteSignal));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// 랜던 Bit 정보 쓰기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="fromBitName"></param>
        /// <param name="toBitName"></param>
        /// <param name="signal"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Response WriteRandomBitListToBetween(int plcNo, string fromBitName, string toBitName, PlcBit.Signal signal, int offset = 0)
        {
            try
            {
                var item = this.GetPlcControlWithBitListToBetween(plcNo, fromBitName, toBitName, true);
                if (item == false) return new Response(false, item.Comment);

                var control = item.Key;
                if (control.Connected == false) return new Response(false, "");

                return control.WriteRandomBit(item.Value.ToDictionary(t => t.ToPlcBitAddressOffset(offset), t => signal));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// 랜덤 Word 정보 쓰기
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="offset"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public Response WriteRandomWordList(int plcNo, int offset = 0, params PlcWirteWordModel[] parms)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var control = item.Value.Value;
                if (control.Connected == false) return new Response(false, "");

                return control.WriteRandomWord(parms.ToDictionary(t => t.ToPlcWordAddressOffset(offset), t => t.WirteValue));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        #endregion

        #region private

        private IPlcControl CreatePlcControl(IPlcConfigBase config)
        {
            switch (config.PlcType)
            {
                case PlcTypes.Ethernet: return new PlcEthernetControl();
                case PlcTypes.Siemens: return new PlcSiemensControl();
                case PlcTypes.Mx: return new PlcMxControl();
                case PlcTypes.LS: return new PlcLSControl();
                case PlcTypes.AB: return new PlcABControl();
                case PlcTypes.Simulation:
                default:
                    return new PlcSimulationControl();
            }
        }

        private KeyValuePair<IPlcConfigBase, IPlcControl>? GetPlcControl(int plcNo)
        {
            return this._configList.FirstOrDefault(t => t.Key.PlcNo == plcNo);
        }

        private Response<IPlcControl, PlcBit> GetPlcControlWithBit(int plcNo, string bitName, bool isWirte)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response<IPlcControl, PlcBit>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                var control = item.Value.Value;

                var bit = config.GetBitItem(bitName, isWirte);
                if (bit == false) return new Response<IPlcControl, PlcBit>(false, bit.Comment);

                return new Response<IPlcControl, PlcBit>(control, bit.Result);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private Response<IPlcControl, PlcWord> GetPlcControlWithWord(int plcNo, string wordName, bool isWirte)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.Value.Key == null && item.Value.Value == null) return new Response<IPlcControl, PlcWord>(false, "데이터가 NULL 입니다.. '{0}'", plcNo);
                if (item.HasValue == false) return new Response<IPlcControl, PlcWord>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                var control = item.Value.Value;

                var word = config.GetWordItem(wordName, isWirte);
                if (word == false) return new Response<IPlcControl, PlcWord>(false, word.Comment);

                return new Response<IPlcControl, PlcWord>(control, word.Result);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private Response<IPlcControl, List<PlcBit>> GetPlcControlWithBitListToBetween(int plcNo, string fromBitName, string toBitName, bool isWirte)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response<IPlcControl, List<PlcBit>>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                var control = item.Value.Value;

                var bit = config.GetBitListToBetween(fromBitName, toBitName, isWirte);
                if (bit == false) return new Response<IPlcControl, List<PlcBit>>(false, bit.Comment);

                return new Response<IPlcControl, List<PlcBit>>(control, bit.Result);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private Response<IPlcControl, List<PlcWord>> GetPlcControlWithWordListToBetween(int plcNo, string fromWordName, string toWrodName, bool isWirte)
        {
            try
            {
                var item = this.GetPlcControl(plcNo);
                if (item.HasValue == false) return new Response<IPlcControl, List<PlcWord>>(false, "등록 되지 않은 PLC 번호 입니다. '{0}'", plcNo);

                var config = item.Value.Key;
                var control = item.Value.Value;

                var word = config.GetWordListToBetween(fromWordName, toWrodName, isWirte);
                if (word == false) return new Response<IPlcControl, List<PlcWord>>(false, word.Comment);

                return new Response<IPlcControl, List<PlcWord>>(control, word.Result);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private void Control_OnConnectedChangedEvent(object sender, bool isConnected)
        {
            try
            {
                if (isConnected)
                {
                    this.OnConnectedEvent?.BeginInvoke(sender, new EventArgs(), this.Control_OnConnectedChangedEvent_EndCallback, this.OnConnectedEvent);
                }
                else
                {
                    this.OnDisconnectedEvent?.BeginInvoke(sender, new EventArgs(), this.Control_OnConnectedChangedEvent_EndCallback, this.OnDisconnectedEvent);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Control_OnConnectedChangedEvent_EndCallback(IAsyncResult result)
        {
            try
            {
                var eventHandler = result.AsyncState as EventHandler;

                eventHandler?.EndInvoke(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Control_OnTickEvent(object sender, IPlcConfigBase e)
        {
            try
            {
                this.OnTickEvent?.Invoke(sender, e);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Control_OnTickEvent_EndCallback(IAsyncResult result)
        {
            try
            {
                var eventHandler = result.AsyncState as EventHandler<IPlcConfigBase>;

                eventHandler?.EndInvoke(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);

            }
        }

        #endregion

        #endregion
    }
}
