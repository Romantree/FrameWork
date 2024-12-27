using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TS.FW.Utils;

namespace TS.FW.Melsec
{
    public class MelsecControl
    {
        private int _path = -1;
        private readonly MelsecDataList _simDataList = new MelsecDataList();

        public bool Simulation { get; set; } = false;

        public bool IsOpen => Simulation == true ? true : this._path != -1;

        /// <summary>
        /// Melsec 통신 연결
        /// </summary>
        /// <param name="chan"></param>
        /// <returns></returns>
        public Response Open(short chan)
        {
            try
            {
                if (this.Simulation == true) return new Response();

                if (this.IsOpen == true) return new Response();

                lock (this)
                {
                    var code = MelsecApi.mdOpen(chan, -1, ref this._path);
                    this.ErrorCheack(code);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// Melsec 통신 종료
        /// </summary>
        /// <returns></returns>
        public Response Close()
        {
            try
            {
                if (this.Simulation == true) return new Response();

                if (this.IsOpen == false) return new Response();

                lock (this)
                {
                    var code = MelsecApi.mdClose(this._path);
                    this.ErrorCheack(code);

                    this._path = -1;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// Melsec에 Data Write
        /// Write Command에 대한 return 결과값으로 Error Check 후 정상일 경우에한 응답 return
        /// </summary>
        /// <param name="netNo"></param>
        /// <param name="stnNo"></param>
        /// <param name="devTyp"></param>
        /// <param name="start"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Response Send(int netNo, int stnNo, MDevType devTyp, int start, short[] buffer)
        {
            try
            {
                if (this.Simulation == true)
                {
                    this.SetSimulationData(devTyp, start, buffer);
                    return new Response();
                }

                if (this.IsOpen == true) //throw new Exception("Melsec Not Open");
                {

                    var size = buffer.Length * 2;

                    lock (this)
                    {
                        var code = MelsecApi.mdSendEx(this._path, netNo, stnNo, (int)devTyp, start, ref size, buffer);
                        this.ErrorCheack(code);
                    }

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }

            return new Response() { Success = false, Comment = "Melsec Not Open" };
        }

        /// <summary>
        /// Melsec에서 Read한 Recv Data를 list로 반환
        /// 정상적인 경우 반환된 결과값의 Success가 true
        /// 실패 또는 Exception의 경우 Success는 false, Comment에 Exception 정보 반환
        /// </summary>
        /// <param name="netNo"></param>
        /// <param name="stnNo"></param>
        /// <param name="devTyp"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public ResponseList<short> Recv(int netNo, int stnNo, MDevType devTyp, int start, int length, int maxCount = (ushort.MaxValue - 1) / 2)
        {
            try
            {
                if (this.Simulation == true)
                {
                    var buffer = this.GetSimulationData(devTyp, start, length);
                    return new ResponseList<short>(buffer);
                }

                if (this.IsOpen == false) throw new Exception("Melsec Not Open");

                var list = new List<short>();

                var count = length / maxCount;
                var split = length % maxCount;

                for (int i = 0; i < count; i++)
                {
                    var devNo = start + (maxCount * i);

                    var buffer = this.Recv(netNo, stnNo, devTyp, devNo, maxCount);
                    list.AddRange(buffer);
                }

                if (split > 0)
                {
                    var devNo = start + (maxCount * count);

                    var buffer = this.Recv(netNo, stnNo, devTyp, devNo, split);
                    list.AddRange(buffer);
                }

                return new ResponseList<short>(list);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// Melsec Bit On/Off
        /// </summary>
        /// <param name="netNo"></param>
        /// <param name="stnNo"></param>
        /// <param name="devTyp"></param>
        /// <param name="number"> On/Off할 Bit의 주소 </param>
        /// <param name="onOff"> On/Off 설정값 </param>
        /// <returns></returns>
        public Response OnOffBit(int netNo, int stnNo, MDevType devTyp, int number, bool onOff)
        {
            try
            {
                if (this.Simulation == true)
                {
                    this.SetSimulationData(devTyp, number, new short[] { Convert.ToInt16(onOff) });
                    return new Response();
                }

                if (this.IsOpen == false) throw new Exception("Melsec Not Open");

                lock (this)
                {
                    if (onOff)
                    {
                        var code = MelsecApi.mdDevSetEx(this._path, netNo, stnNo, (int)devTyp, number);
                        this.ErrorCheack(code);
                    }
                    else
                    {
                        var code = MelsecApi.mdDevRstEx(this._path, netNo, stnNo, (int)devTyp, number);
                        this.ErrorCheack(code);
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// Melsec Read한 Data를 배열 형태로 return
        /// DevType이 LB인 경우 LittleEndian 형식으로 Length만큼 return
        /// DevType이 LW인 경우 문자열을 Array형태로 return
        /// 실제 Melsec Data를 읽어오는 부분
        /// </summary>
        /// <param name="netNo"> Melsec NetworkNo </param>
        /// <param name="stnNo"> Melsec StationNo </param>
        /// <param name="devTyp"> Recv Data Type : LB / LW </param>
        /// <param name="start"> Recv Data의 시작 Address </param>
        /// <param name="length"> Recv Data의 길이 </param>
        /// <returns></returns>
        private short[] Recv(int netNo, int stnNo, MDevType devTyp, int start, int length)
        {
            var size = devTyp == MDevType.LB ? (int)Math.Ceiling(length / 16.0D) : length;
            var buffer = new short[size];

            var sizeEx = size * 2;

            lock (this)
            {
                var code = MelsecApi.mdReceiveEx(this._path, netNo, stnNo, (int)devTyp, start, ref sizeEx, buffer);
                this.ErrorCheack(code);
            }

            if (devTyp == MDevType.LB)
            {
                return buffer.SelectMany(t => t.ToBinary().Select(x => (short)(x == '1' ? 1 : 0)).Reverse()).Take(length).ToArray();
            }
            else
            {
                return buffer;
            }
        }

        /// <summary>
        /// Melsec 통신 결과에 대한 Error Check
        /// Error 발생 시 Error Code Return
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        private void ErrorCheack(int code, [CallerMemberName] string name = null)
        {
            var type = (MErrorCode)code;
            if (type == MErrorCode.SUCCESS) return;

            throw new Exception(string.Format("{2} : {0}({1})", type, (int)type, name));
        }

        private void SetSimulationData(MDevType devTyp, int start, short[] buffer)
        {
            try
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    var address = start + i;

                    var item = this._simDataList.FirstOrDefault(t => t.DevType == devTyp && t.Address == address);
                    if (item == null)
                    {
                        this._simDataList.Add(new MelsecData() { DevType = devTyp, Address = address, Data = buffer[i] });
                    }
                    else
                    {
                        this._simDataList[devTyp, address].Data = buffer[i];
                    }                    
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private List<short> GetSimulationData(MDevType devTyp, int start, int length)
        {
            try
            {
                var list = new List<short>();

                if(this._simDataList.CheckItem(start, length, devTyp) == false)
                {
                    for (int i = 0; i < length; i++)
                    {
                        var address = start + i;
                        var item = this._simDataList.FirstOrDefault(t => t.DevType == devTyp && t.Address == address);
                        if (item == null)
                        {
                            item = new MelsecData() { Address = address, Data = 0, DevType = devTyp };
                            this._simDataList.Add(item);
                        }

                        list.Add(item.Data);
                    }
                }
                else
                {
                    var dList = this._simDataList.ToData(start, length, devTyp);

                    list.AddRange(dList.Select(t => t.Data));
                }

                return list;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return new List<short>(new short[length]);

            }
        }
    }
}
