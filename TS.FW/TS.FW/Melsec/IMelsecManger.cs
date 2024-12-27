using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using TS.FW.Diagnostics;
using TS.FW.Utils;

namespace TS.FW.Melsec
{
    public abstract class IMelsecManger
    {
        public static BitHandler.ByteOrder BYTE_ORDER = BitHandler.ByteOrder.LittleEndian;

        private readonly BackgroundTimer _trUpdate = new BackgroundTimer(ApartmentState.MTA);

        protected readonly MelsecControl _control = new MelsecControl();
        protected readonly MelsecDataList _dataList = new MelsecDataList();
        protected readonly MelsecDataList _changeList = new MelsecDataList();

        private bool _firstRead = false;

        private MelsecSetting[] _settings;

        public event EventHandler<MelsecDataList> OnMelsecDataChangedEvent;

        public bool this[int address] => this._dataList[MDevType.LB, address].Data == 1;

        public MelsecDataList this[MDevType devTyp, int address, int lenght] => this.GetDataEx(devTyp, address, lenght);//new MelsecDataList(this._dataList.ToData(address, lenght, devTyp));

        private MelsecDataList GetDataEx(MDevType devTyp, int address, int lenght)
        {
            var ret = new MelsecDataList(this._dataList.ToData(address, lenght, devTyp));

            //if (this.Simulation == true)
            //{
            //    var first = ret.First();
            //    if (first != null) first.Data = (short)(DateTime.Now.Millisecond % 5);// == 0 ? 1 : 0);
            //    var last = ret.Last();
            //    if (last != null) last.Data = (short)(DateTime.Now.Millisecond % 5);
            //}

            return ret;
        }

        public bool Simulation { get => this._control.Simulation; set => this._control.Simulation = value; }

        public bool IsOpen => this._control.IsOpen;

        public bool IsRun => this.RecvTackTime != 0;

        public double RecvTackTime { get; private set; }

        public IMelsecManger(int timeMsc = 10)
        {
            this._trUpdate.SleepTimeMsc = timeMsc;
            this._trUpdate.DoWork += _trUpdate_DoWork;
        }

        public virtual void FirstRead() { }

        public virtual void Start() => this._trUpdate.Start();

        public virtual void Stop() => this._trUpdate.Stop();

        public Response InitSetting(params MelsecSetting[] settings)
        {
            try
            {
                if (this._trUpdate.IsBusy == true) throw new Exception("동작 중에는 변경 할 수 없습니다.");

                this._settings = settings;

                this._dataList.Clear();

                var list = this.ToMelsecData(settings);
                if (list.Count <= 0) return new Response();

                this._dataList.AddRange(list);

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        private List<MelsecData> ToMelsecData(MelsecSetting[] settings)
        {
            var list = new List<MelsecData>();

            foreach (var item in settings)
            {
                if (item.DevType == MDevType.LB && item.Start % 16 != 0) throw new Exception(string.Format("LB 시작 주소 설정이 잘못 되었습니다. 시작 주소는 16배수 이어야 합니다. {0}", item.Start));

                for (int i = 0; i < item.Lenght; i++)
                {
                    var address = item.Start + i;

                    if (list.Any(t => t.DevType == item.DevType && t.Address == address)) continue;

                    list.Add(new MelsecData()
                    {
                        DevType = item.DevType,
                        Address = address,
                    });
                }
            }

            return list.OrderBy(t => t.Address).OrderBy(t => t.DevType).ToList();
        }

        public Response Open(short chan) => this._control.Open(chan);

        public Response Close() => this._control.Close();

        public Response OnOffBit(int netNo, int stnNo, int number, bool onOff) => this._control.OnOffBit(netNo, stnNo, MDevType.LB, number, onOff);

        public Response SetData(int netNo, int stnNo, int number, short data)
        {
            try
            {
                return this._control.Send(netNo, stnNo, MDevType.LW, number, new short[] { data });
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetData(int netNo, int stnNo, int number, int data)
        {
            try
            {
                var buffer = this.ToConvert(data);

                return this._control.Send(netNo, stnNo, MDevType.LW, number, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetData(int netNo, int stnNo, int number, float data)
        {
            try
            {
                var buffer = this.ToConvert(data);

                return this._control.Send(netNo, stnNo, MDevType.LW, number, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetData(int netNo, int stnNo, int number, double data)
        {
            try
            {
                var buffer = this.ToConvert(data);

                return this._control.Send(netNo, stnNo, MDevType.LW, number, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetData(int netNo, int stnNo, int number, string data, int lenght)
        {
            try
            {
                var buffer = this.ToConvert(data.PadRight(lenght * 2, ' ').Substring(0, lenght * 2));

                return this._control.Send(netNo, stnNo, MDevType.LW, number, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SetData(int netNo, int stnNo, int number, short[] buffer)
        {
            try
            {
                return this._control.Send(netNo, stnNo, MDevType.LW, number, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        /// <summary>
        /// Read한 Melsec Data를 MelsecDataList 객체로 반환
        /// Melsec Mapping 정보대로 설정한 Melsec 영역 전체에 대한 DataList
        /// 이미 읽어온 DataList(_dataList와 새로 읽은 DataList(_changeList) 두 가지 List가 존재
        /// </summary>
        /// <param name="netNo"></param>
        /// <param name="stnNo"></param>
        /// <param name="start"></param>
        /// <param name="lenght"></param>
        /// <param name="devTyp"></param>
        /// <returns></returns>
        public MelsecDataList RecvData(int netNo, int stnNo, int start, int lenght, MDevType devTyp = MDevType.LW)
        {
            try
            {
                var list = this.Recv(netNo, stnNo, start, lenght, devTyp);
                var dataList = new MelsecDataList();

                var i = 0;

                foreach (var item in list)
                {
                    dataList.Add(new MelsecData()
                    {
                        DevType = devTyp,
                        Address = start + i++,
                        Data = item,
                    });
                }

                return dataList;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        public MelsecDataList RecvData(MelsecSetting item)
        {
            return this.RecvData(item.NetworkNo, item.StationNo, item.Start, item.Lenght, item.DevType);
        }

        protected T ToMelsecDataFormat<T>(int start) where T : IMelsecDataFormat
        {
            var item = Activator.CreateInstance<T>();
            var lenght = item.ToLenght();
            var dList = this[MDevType.LW, start, lenght];

            if (dList == null || dList.Count <= 0) return null;

            item.SetData(dList, start);

            return item;
        }

        /// <summary>
        /// setting에 저장된 영역의 Melsec Data 중 변경된 값의 정보만 Read
        /// </summary>
        /// <param name="setting"> Melsec Mapping 정보에 대한 설정값 </param>
        /// <returns></returns>
        protected List<MelsecData> Recv(MelsecSetting setting)
        {
            var cList = new List<MelsecData>();

            try
            {
                var netNo = setting.NetworkNo;
                var stnNo = setting.StationNo;
                var devTyp = setting.DevType;
                var start = setting.Start;
                var lenght = setting.Lenght;

                var list = this.Recv(netNo, stnNo, start, lenght, devTyp);
                var i = 0;

                var dList = this._dataList.ToData(start, lenght, devTyp).ToList();

                foreach (var item in list)
                {
                    var data = dList[i++];
                    if (data.Data == item) continue;

                    data.Data = item;
                    cList.Add(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }

            return cList;
        }

        /// <summary>
        /// Melsec Read Data의 길이에 대한 유효성 검사 후 길이가 맞으면 read Data Return
        /// </summary>
        /// <param name="netNo"></param>
        /// <param name="stnNo"></param>
        /// <param name="start"></param>
        /// <param name="lenght"></param>
        /// <param name="devTyp"></param>
        /// <returns></returns>
        protected List<short> Recv(int netNo, int stnNo, int start, int lenght, MDevType devTyp)
        {
            try
            {
                var res = this._control.Recv(netNo, stnNo, devTyp, start, lenght);
                if (res == false) throw new Exception(res.Comment);

                var list = res.Result;
                if (list.Count != lenght) throw new Exception(string.Format("데이터 요청 크기와 수신 크기가 맞지 않습니다. {0} : {1}", lenght, list.Count));

                return list;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 정수형 Data를 short 형으로 반환
        /// 반환 규칙은 BYTE_ORDER에 따름
        /// </summary>
        /// <param name="data"></param>
        /// <param name="BYTE_ORDER"> 예시 : LittleEndian or BigEndian </param>
        /// <returns></returns>
        protected short[] ToConvert(int data)
        {
            var i = 0;
            var buffer = new byte[sizeof(int)];

            foreach (var item in TS.FW.Utils.BitHandler.ToByte(data, BYTE_ORDER))
            {
                buffer[i++] = item;
            }

            return TS.FW.Utils.BitHandler.ToInt16List(buffer, 0, buffer.Length / 2, BYTE_ORDER).ToArray();
        }

        /// <summary>
        /// float 형 Data의 소수 셋째자리까지 정수형으로 변환 후 short 형으로 반환
        /// 반환 규칙은 BYTE_ORDER에 따름
        /// </summary>
        /// <param name="data"></param>
        /// <param name="BYTE_ORDER"> 예시 : LittleEndian or BigEndian </param>
        /// <returns></returns>
        protected short[] ToConvert(float data)
        {
            var i = 0;
            var buffer = new byte[sizeof(float)];

            foreach (var item in TS.FW.Utils.BitHandler.ToByte((int)(data * 1000), BYTE_ORDER))
            //foreach (var item in TS.FW.Utils.BitHandler.ToByte(data, BYTE_ORDER))
            {
                buffer[i++] = item;
            }

            return TS.FW.Utils.BitHandler.ToInt16List(buffer, 0, buffer.Length / 2, BYTE_ORDER).ToArray();
        }

        /// <summary>
        /// double 형 Data의 소수 셋째자리까지 정수형으로 변환 후 short 형으로 반환
        /// 반환 규칙은 BYTE_ORDER에 따름
        /// </summary>
        /// <param name="data"></param>
        /// <param name="BYTE_ORDER"> 예시 : LittleEndian or BigEndian </param>
        /// <returns></returns>
        protected short[] ToConvert(double data)
        {
            var i = 0;
            var buffer = new byte[sizeof(double)];

            //foreach (var item in TS.FW.Utils.BitHandler.ToByte((int)(data * 1000), BYTE_ORDER))
            foreach (var item in BitHandler.ToByte(data, BYTE_ORDER))
            {
                buffer[i++] = item;
            }

            return BitHandler.ToInt16List(buffer, 0, buffer.Length / 2, BYTE_ORDER).ToArray();
        }

        /// <summary>
        /// string 형 Data를 short 형 으로 변환
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected short[] ToConvert(string data)
        {
            var buffer = Encoding.ASCII.GetBytes(data);

            return TS.FW.Utils.BitHandler.ToInt16List(buffer, 0, buffer.Length / 2, BYTE_ORDER).ToArray();
        }

        /// <summary>
        /// 변경된 Melsec Data Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _trUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.IsOpen == false) return;
                if (this._settings == null || this._settings.Length <= 0) return;

                var start = DateTime.Now;

                this._changeList.Clear();

                foreach (var set in this._settings)
                {
                    var list = this.Recv(set);
                    if (list == null || list.Count <= 0) continue;

                    this._changeList.AddRange(list);
                }

                this.RecvTackTime = (DateTime.Now - start).TotalSeconds;

                if (this._firstRead == false)
                {
                    this._firstRead = true;
                    this.FirstRead();
                }

                if (this._changeList.Count > 0)
                {
                    this.OnMelsecDataChangedEvent?.Invoke(this, this._changeList);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
