using Microsoft.VisualStudio.TestTools.UnitTesting;
using S7.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Utils;

namespace TS.FW.Plc.Config
{
    /// <summary>
    /// PLC 환경설정 베이스 클래스
    /// </summary>
    [DataContract]
    [KnownType(typeof(PlcSimulationConfig))]
    [KnownType(typeof(PlcEthernetConfig))]
    [KnownType(typeof(PlcMxConfig))]
    [KnownType(typeof(PlcLSConfig))]
    public abstract class IPlcConfigBase
    {
        /// <summary>
        /// Tick 인터벌
        /// </summary>
        internal const int TICK_INTERVAL = 200; //210521 50ms >> 100ms 변경 cpu 점유율 변경 

        /// <summary>
        /// PLC 모듈 ID
        /// </summary>
        [DataMember]
        public int PlcNo { get; private set; }

        /// <summary>
        /// PLC 유형
        /// </summary>
        [DataMember]
        public PlcTypes PlcType { get; private set; }

        /// <summary>
        /// PLC Ini 파일 경로
        /// </summary>
        [DataMember]
        public string IniFilePath { get; private set; }

        /// <summary>
        /// PLC 영역 Bit 목록
        /// </summary>
        public Dictionary<string, PlcBit> InBitList { get; private set; } = new Dictionary<string, PlcBit>();

        /// <summary>
        /// PC 영역 Bit 목록
        /// </summary>
        public Dictionary<string, PlcBit> OutBitList { get; private set; } = new Dictionary<string, PlcBit>();

        /// <summary>
        /// PLC 영역 Word 목록
        /// </summary>
        public Dictionary<string, PlcWord> InWordList { get; private set; } = new Dictionary<string, PlcWord>();

        /// <summary>
        /// PC 영역 Word 목록
        /// </summary>
        public Dictionary<string, PlcWord> OutWordList { get; private set; } = new Dictionary<string, PlcWord>();

        /// <summary>
        /// Bit 전체 목록
        /// </summary>
        public Dictionary<string, PlcBit> TotalBitList { get; private set; } = new Dictionary<string, PlcBit>();

        /// <summary>
        /// Word 전체 목록
        /// </summary>
        public Dictionary<string, PlcWord> TotalWordList { get; private set; } = new Dictionary<string, PlcWord>();

        /// <summary>
        /// 부가 정보
        /// </summary>
        public object Tag { get; set; }

        public TimeSpan ReadBitTime { get; set; }

        public TimeSpan ReadWordTime { get; set; }

        private IPlcConfigBase()
        {
            this.PlcNo = -1;

            this.InBitList = new Dictionary<string, PlcBit>();
            this.OutBitList = new Dictionary<string, PlcBit>();

            this.InWordList = new Dictionary<string, PlcWord>();
            this.OutWordList = new Dictionary<string, PlcWord>();
        }

        public IPlcConfigBase(int plcNo, PlcTypes type, string iniFilePath) : this()
        {
            this.PlcNo = plcNo;
            this.PlcType = type;
            this.IniFilePath = iniFilePath;
        }
        
        #region public

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public virtual Response Init()
        {
            try
            {
                var list = this.IniFilePath.ToIniDictionary();

                this.InitBitList(list);
                this.InitWordList(list);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        
        /// <summary>
        /// 데이터 셋팅
        /// </summary>
        /// <param name="inBitList"></param>
        /// <param name="outBitList"></param>
        /// <param name="inWordList"></param>
        /// <param name="outWordList"></param>
        /// <returns></returns>
        public virtual Response Init(IEnumerable<PlcBit> inBitList, IEnumerable<PlcBit> outBitList
            , IEnumerable<PlcWord> inWordList, IEnumerable<PlcWord> outWordList)
        {
            try
            {
                this.InBitList = inBitList.ToDictionary(t => t.Name, t => t);
                this.OutBitList = outBitList.ToDictionary(t => t.Name, t => t);
                this.InWordList = inWordList.ToDictionary(t => t.Name, t => t);
                this.OutWordList = outWordList.ToDictionary(t => t.Name, t => t);

                this.TotalBitList = this.InBitList.Concat(this.OutBitList).OrderBy(t => t.Value.Address).ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, PlcBit>();
                this.TotalWordList = this.InWordList.Concat(this.OutWordList).OrderBy(t => t.Value.Address).ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, PlcWord>();

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// 데이터 셋팅
        /// </summary>
        /// <param name="inBitList"></param>
        /// <param name="outBitList"></param>
        /// <param name="inWordList"></param>
        /// <param name="outWordList"></param>
        /// <returns></returns>
        public virtual Response Init(IEnumerable<PlcBit> inBitList, IEnumerable<PlcBit> outBitList
            , IEnumerable<PlcWord> inWordList, IEnumerable<PlcWord> outWordList
            , IEnumerable<PlcWord> glassDataList)
        {
            try
            {
                this.InBitList = inBitList.ToDictionary(t => t.Name, t => t);
                this.OutBitList = outBitList.ToDictionary(t => t.Name, t => t);
                this.InWordList = inWordList.ToDictionary(t => t.Name, t => t);
                this.OutWordList = outWordList.ToDictionary(t => t.Name, t => t);

                foreach (var item in glassDataList.ToDictionary(t => t.Name, t => t))
                {
                    this.InWordList.Add(item.Key, item.Value);
                }

                this.TotalBitList = this.InBitList.Concat(this.OutBitList).OrderBy(t => t.Value.Address).ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, PlcBit>();
                this.TotalWordList = this.InWordList.Concat(this.OutWordList).OrderBy(t => t.Value.Address).ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, PlcWord>();

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<PlcBit> GetBitItem(string bitName, bool isWirte)
        {
            try
            {
                var list = this.PlcType == PlcTypes.Simulation ? this.TotalBitList : isWirte ? this.OutBitList : this.TotalBitList;

                if (list.ContainsKey(bitName) == false) return new Response<PlcBit>(false, "존재하지 Bit 입니다. PlcNo : '{0}', BitName : '{1}'", this.PlcNo, bitName);

                return new Response<PlcBit>(list[bitName]);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<PlcWord> GetWordItem(string wordName, bool isWirte)
        {
            try
            {
                var list = this.PlcType == PlcTypes.Simulation ? this.TotalWordList : isWirte ? this.OutWordList : this.TotalWordList;

                if (list.ContainsKey(wordName) == false) return new Response<PlcWord>(false, "존재하지 Word 입니다. PlcNo : '{0}', WordName : '{1}'", this.PlcNo, wordName);

                return new Response<PlcWord>(list[wordName]);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<PlcBit> GetBitListToBetween(string fromBitName, string toBitName, bool isWirte)
        {
            try
            {
                var list = this.PlcType == PlcTypes.Simulation ? this.TotalBitList : isWirte ? this.OutBitList : this.TotalBitList;

                if (list.ContainsKey(fromBitName) == false)
                    return new ResponseList<PlcBit>(false, "존재하지 Bit 입니다. PlcNo : '{0}', fromBitName : '{1}'", this.PlcNo, fromBitName);
                if (list.ContainsKey(toBitName) == false)
                    return new ResponseList<PlcBit>(false, "존재하지 Bit 입니다. PlcNo : '{0}', toBitName : '{1}'", this.PlcNo, toBitName);

                var formBit = list[fromBitName];
                var toBit = list[toBitName];

                if (formBit.Address > toBit.Address)
                    return new ResponseList<PlcBit>(false, "시작점 주소가 끝점 주소보다 높습니다. From:'{0}' To:'{1}'", formBit.Address.ToHex(), toBit.Address.ToHex());

                var result = list.Values.ToBetweenList(t => t.Address, formBit, toBit).ToList();

                return new ResponseList<PlcBit>(result);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<PlcWord> GetWordListToBetween(string fromWordName, string toWordName, bool isWirte)
        {
            try
            {
                var list = this.PlcType == PlcTypes.Simulation ? this.TotalWordList : isWirte ? this.OutWordList : this.TotalWordList;

                if (list.ContainsKey(fromWordName) == false)
                    return new ResponseList<PlcWord>(false, "존재하지 Bit 입니다. PlcNo : '{0}', fromWordName : '{1}'", this.PlcNo, fromWordName);
                if (list.ContainsKey(toWordName) == false)
                    return new ResponseList<PlcWord>(false, "존재하지 Bit 입니다. PlcNo : '{0}', toWordName : '{1}'", this.PlcNo, toWordName);

                var formWord = list[fromWordName];
                var toWord = list[toWordName];

                if (formWord.Address > toWord.Address)
                    return new ResponseList<PlcWord>(false, "시작점 주소가 끝점 주소보다 높습니다. From:'{0}' To:'{1}'", formWord.Address.ToHex(), toWord.Address.ToHex());

                var result = list.Values.ToBetweenList(t => t.Address, formWord, toWord).ToList();

                return new ResponseList<PlcWord>(result);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response ToCreateIniFile_EX(string filePath)
        {
            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);

                foreach (var item in this.InBitList)
                {
                    IniFileHandler.SetIniFile_Value(filePath, "B_IN", item.Key, this.ToHexString(item.Value.Address));
                }

                foreach (var item in this.OutBitList)
                {
                    IniFileHandler.SetIniFile_Value(filePath, "B_OUT", item.Key, this.ToHexString(item.Value.Address));
                }

                foreach (var item in this.InWordList)
                {
                    var value = string.Format("{0}/{1}/{2}", this.ToHexString(item.Value.Address), item.Value.Length, this.ToToPlcWordDataType(item.Value.Type));

                    if(item.Value.Code == PlcDeviceCode.W_LINK_REGISTER)
                    {
                        IniFileHandler.SetIniFile_Value(filePath, "W_IN", item.Key, value);
                    }
                    else if (item.Value.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER)
                    {
                        IniFileHandler.SetIniFile_Value(filePath, "R_IN", item.Key, value);
                    }
                }

                foreach (var item in this.OutWordList)
                {
                    var value = string.Format("{0}/{1}/{2}", this.ToHexString(item.Value.Address), item.Value.Length, this.ToToPlcWordDataType(item.Value.Type));

                    if (item.Value.Code == PlcDeviceCode.W_LINK_REGISTER)
                    {
                        IniFileHandler.SetIniFile_Value(filePath, "W_OUT", item.Key, value);
                    }
                    else if (item.Value.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER)
                    {
                        IniFileHandler.SetIniFile_Value(filePath, "R_OUT", item.Key, value);
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response ToCreateIniFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);

                var res = this.ToIniFileText();
                if (res == false) return res;

                File.AppendAllText(filePath, res.Result, Encoding.ASCII);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<string> ToIniFileText()
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("[B_IN]");
                foreach (var item in this.InBitList)
                {
                    sb.AppendLine(string.Format("{0}={1}", item.Key, this.ToHexString(item.Value.Address)));
                }

                sb.AppendLine("[B_OUT]");
                foreach (var item in this.OutBitList)
                {
                    sb.AppendLine(string.Format("{0}={1}", item.Key, this.ToHexString(item.Value.Address)));
                }

                sb.AppendLine("[W_IN]");
                foreach (var item in this.InWordList.Where(t => t.Value.Code == PlcDeviceCode.W_LINK_REGISTER))
                {
                    var value = string.Format("{0}/{1}/{2}", this.ToHexString(item.Value.Address), item.Value.Length, this.ToToPlcWordDataType(item.Value.Type));

                    sb.AppendLine(string.Format("{0}={1}", item.Key, value));
                }

                sb.AppendLine("[W_OUT]");
                foreach (var item in this.OutWordList.Where(t => t.Value.Code == PlcDeviceCode.W_LINK_REGISTER))
                {
                    var value = string.Format("{0}/{1}/{2}", this.ToHexString(item.Value.Address), item.Value.Length, this.ToToPlcWordDataType(item.Value.Type));

                    sb.AppendLine(string.Format("{0}={1}", item.Key, value));
                }

                sb.AppendLine("[R_IN]");
                foreach (var item in this.InWordList.Where(t => t.Value.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER))
                {
                    var value = string.Format("{0}/{1}/{2}", this.ToHexString(item.Value.Address), item.Value.Length, this.ToToPlcWordDataType(item.Value.Type));

                    sb.AppendLine(string.Format("{0}={1}", item.Key, value));
                }

                sb.AppendLine("[R_OUT]");
                foreach (var item in this.OutWordList.Where(t => t.Value.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER))
                {
                    var value = string.Format("{0}/{1}/{2}", this.ToHexString(item.Value.Address), item.Value.Length, this.ToToPlcWordDataType(item.Value.Type));

                    sb.AppendLine(string.Format("{0}={1}", item.Key, value));
                }

                return new Response<string>(sb.ToString());
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public override int GetHashCode()
        {
            return this.PlcNo;
        }

        public override bool Equals(object obj)
        {
            if (object.Equals(this, null) && object.Equals(obj, null)) return true;
            if (object.Equals(this, null) || object.Equals(obj, null)) return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("PLC No : {0}({1})", this.PlcNo, this.PlcType);
        }

        public string ToHexString(int value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).PadLeft(4, '0').ToUpper());
        }

        public string ToToPlcWordDataType(PlcWordDataType type)
        {
            switch (type)
            {
                case PlcWordDataType.Number: return "N";
                case PlcWordDataType.Double: return "D";
                case PlcWordDataType.ASCII: return "A";
                case PlcWordDataType.LIST: return "L";
            }

            throw new Exception(type.ToString());
        }

        #endregion

        #region private

        /// <summary>
        /// Bit 목록 초기화
        /// </summary>
        private void InitBitList(Dictionary<string, Dictionary<string, string>> list)
        {
            this.InitInBitList(list);
            this.InitOutBitList(list);

            this.TotalBitList = this.InBitList.Concat(this.OutBitList).OrderBy(t => t.Value.Address).ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, PlcBit>();
        }

        /// <summary>
        /// Word 목록 초기화
        /// </summary>
        private void InitWordList(Dictionary<string, Dictionary<string, string>> list)
        {
            this.InitInWordList(list);
            this.InitOutWordList(list);

            this.TotalWordList = this.InWordList.Concat(this.OutWordList).OrderBy(t => t.Value.Address).ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, PlcWord>();
        }

        private void InitInBitList(Dictionary<string, Dictionary<string, string>> list)
        {
            if (this.InBitList == null) this.InBitList = new Dictionary<string, PlcBit>();
            this.InBitList.Clear();

            //var bitList = this.IniFilePath.GetIniFile_ToDictionary("B_IN", "");
            //if (bitList == false) throw new Exception(bitList.Comment);

            if (list.ContainsKey("B_IN") == false) return;
            foreach (var bit in list["B_IN"])
            {
                var value = AddValue(bit.Value);

                var model = new PlcBit(value, bit.Key);

                this.InBitList.Add(bit.Key, model);
            }
        }

        private void InitOutBitList(Dictionary<string, Dictionary<string, string>> list)
        {
            if (this.OutBitList == null) this.OutBitList = new Dictionary<string, PlcBit>();
            this.OutBitList.Clear();

            //var bitList = this.IniFilePath.GetIniFile_ToDictionary("B_OUT", "");
            //if (bitList == false) throw new Exception(bitList.Comment);

            if (list.ContainsKey("B_OUT") == false) return;
            foreach (var bit in list["B_OUT"])
            {
                var value = AddValue(bit.Value);

                var model = new PlcBit(value, bit.Key);

                this.OutBitList.Add(bit.Key, model);
            }
        }

        public int AddValue(string value)
        {
            var Add = 0;

            if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                Add = Convert.ToInt32(value, 16);
            }
            else
            {
                Add = Convert.ToInt32(value);
            }

            return Add;
        }

        private void InitInWordList(Dictionary<string, Dictionary<string, string>> list)
        {
            if (this.InWordList == null) this.InWordList = new Dictionary<string, PlcWord>();
            this.InWordList.Clear();

            //var wordList = this.IniFilePath.GetIniFile_ToDictionary("W_IN", "/0/");
            //if (wordList == false) throw new Exception(wordList.Comment);

            if (list.ContainsKey("W_IN") == true)
            {
                foreach (var word in list["W_IN"])
                {
                    var tokens = word.Value.Split('/');
                    var address = AddValue(tokens[0]);
                    var length = Convert.ToInt32(tokens[1]);
                    var type = this.ConvertToPlcWordDataType(tokens[2]);

                    var model = new PlcWord(address, word.Key, length, type, PlcDeviceCode.W_LINK_REGISTER);

                    this.InWordList.Add(word.Key, model);
                }
            }


            //var rList = this.IniFilePath.GetIniFile_ToDictionary("R_IN", "/0/");
            //if (rList == false) throw new Exception(rList.Comment);

            if (list.ContainsKey("R_IN") == true)
            {
                foreach (var word in list["R_IN"])
                {
                    var tokens = word.Value.Split('/');
                    var address = AddValue(tokens[0]);
                    var length = Convert.ToInt32(tokens[1]);
                    var type = this.ConvertToPlcWordDataType(tokens[2]);

                    var model = new PlcWord(address, word.Key, length, type, PlcDeviceCode.R_FILE_ACCESS_REGISTER);

                    this.InWordList.Add(word.Key, model);
                }
            }                
        }

        private void InitOutWordList(Dictionary<string, Dictionary<string, string>> list)
        {
            if (this.OutWordList == null) this.OutWordList = new Dictionary<string, PlcWord>();
            this.OutWordList.Clear();

            //var wordList = this.IniFilePath.GetIniFile_ToDictionary("W_OUT", "/0/");
            //if (wordList == false) throw new Exception(wordList.Comment);

            if (list.ContainsKey("W_OUT") == true)
            {
                foreach (var word in list["W_OUT"])
                {
                    var tokens = word.Value.Split('/');
                    var address = AddValue(tokens[0]);
                    var length = Convert.ToInt32(tokens[1]);
                    var type = this.ConvertToPlcWordDataType(tokens[2]);

                    var model = new PlcWord(address, word.Key, length, type, PlcDeviceCode.W_LINK_REGISTER);

                    this.OutWordList.Add(word.Key, model);
                }
            }

            //var rList = this.IniFilePath.GetIniFile_ToDictionary("R_OUT", "/0/");
            //if (rList == false) throw new Exception(rList.Comment);

            if (list.ContainsKey("R_OUT") == true)
            {
                foreach (var word in list["R_OUT"])
                {
                    var tokens = word.Value.Split('/');
                    var address = AddValue(tokens[0]);
                    var length = Convert.ToInt32(tokens[1]);
                    var type = this.ConvertToPlcWordDataType(tokens[2]);

                    var model = new PlcWord(address, word.Key, length, type, PlcDeviceCode.R_FILE_ACCESS_REGISTER);

                    this.OutWordList.Add(word.Key, model);
                }
            }
        }

        private PlcWordDataType ConvertToPlcWordDataType(string type)
        {
            switch (type)
            {
                case "N": // shout int long
                case "n": return PlcWordDataType.Number;
                case "L":
                case "l": return PlcWordDataType.LIST;
                case "D": // float double
                case "d": return PlcWordDataType.Double;
                case "A": // 문자열 ASCII
                case "a":
                default: return PlcWordDataType.ASCII;
            }
        }
        

        #endregion

        public static implicit operator int(IPlcConfigBase config)
        {
            if (config == null) return -1;
            return config.PlcNo;
        }
    }
}
