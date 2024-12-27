using Opc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Plc.Config;
using TS.FW.Plc.Dto;

namespace TS.FW.Plc.Core
{
    public class PlcOpcClient
    {
        private System.Timers.Timer _trReconnect = null;
        private readonly object _lock = new object();
        
        public Opc.URL Url { get; private set; }

        public Opc.Da.Server Server { get; private set; }

        public string OpcName { get; private set; }

        /// <summary>
        /// TODO : Items, DataListm, Group 하나로 합쳐서 사용할 수 있도록 수정.
        /// </summary>
        public Opc.Da.Item[] BitItems { get; private set; }

        public Opc.Da.Item[] WordItems { get; private set; }

        public Opc.Da.Item[] GlassItems { get; private set; }
        
        public List<PlcOpcData> PlcOpcGlassDataList { get; private set; } = new List<PlcOpcData>();

        public List<PlcOpcData> PlcOpcBitDataList { get; private set; } = new List<PlcOpcData>();

        public List<PlcOpcData> PlcOpcWordDataList { get; private set; } = new List<PlcOpcData>();

        public Opc.Da.Subscription BitGroup { get; private set; }

        public Opc.Da.Subscription WordGroup { get; private set; }

        public Opc.Da.Subscription GlassGroup { get; private set; }
        
        public event EventHandler OnConnectedEvent;
        
        public event EventHandler OnDisconnectedEvent;
        
        public bool Connected
        {
            get
            {
                return Server.IsConnected;
            }
        }

        private PlcOpcClient()
        {
        }

        public PlcOpcClient(List<PlcOpcData> bitdata, List<PlcOpcData> worddata, string opcame)
        {
            this.PlcOpcBitDataList = bitdata;
            this.PlcOpcWordDataList = worddata;
            this.OpcName = opcame;
        }

        #region 함수

        public void ConfigChange(string opcname)
        {
            this.InitializeComponent();

            this.OpcName = opcname;
        }
        
        public void Start()
        {
            if (this.Server != null) return;
            this.Connect();

            this.InitializeComponent();
        }

        /// <summary>
        /// TODO : Stop 했을 때 잘 멈춰지는지 확인.
        /// </summary>
        public void Stop()
        {
            this._trReconnect.Stop();
            this.Close();
        }

        /// <summary>
        ///  TODO : for문으로 사용할 필요 있을까 foreach문으로 수정 필요.
        /// </summary>
        /// <param name="subscriptionHandle"></param>
        /// <param name="requestHandle"></param>
        /// <param name="items"></param>
        private void BitGroup_DataChanged(object subscriptionHandle, object requestHandle, Opc.Da.ItemValueResult[] items)
        {
            try
            {
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    foreach (var item in this.PlcOpcBitDataList)
                    {
                        if (item.OpcItemName == items[i].ItemName)
                        {
                            item.OnOff = int.Parse(items[i].Value.ToString()) == 0 ? PlcBit.Signal.OFF : PlcBit.Signal.ON;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void WordGroup_DataChanged(object subscriptionHandle, object requestHandle, Opc.Da.ItemValueResult[] items)
        {
            try
            {
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    foreach (var item in this.PlcOpcWordDataList)
                    {
                        if (item.OpcItemName == items[i].ItemName)
                        {
                            item.Value = items[i].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void GlassGroup_ReadCompleteCallback(object clientHandle, Opc.Da.ItemValueResult[] results)
        {
            try
            {

                foreach (Opc.Da.ItemValueResult readResult in results)
                {
                    PlcOpcGlassDataList.Add(new PlcOpcData()
                    {
                        OpcItemName = readResult.ItemName,
                        Value = readResult.Value
                    });
                }

                GlassGroup.DataChanged += GlassGroup_DataChanged;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void GlassGroup_DataChanged(object subscriptionHandle, object requestHandle, Opc.Da.ItemValueResult[] items)
        {
            try
            {
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    foreach (var item in PlcOpcGlassDataList)
                    {
                        if (item.OpcItemName == items[i].ItemName)
                        {
                            item.Value = items[i].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
        
        #region 명령

        public Response<PlcBit.Signal> ReadBit(PlcBit bit)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");
                
                foreach (var item in this.PlcOpcBitDataList)
                {
                    if (item.DataName == bit.Name)
                    {
                        return new Response<PlcBit.Signal>(item.OnOff);
                    }
                }

                throw new Exception(string.Format("PLC BIt 데이터 읽는데 실패하였습니다. BIT : {0}", bit));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<object> ReadWord(PlcWord word)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                if (word.Name.StartsWith("GLASS_"))
                {
                    if (word.Name == "GLASS_H_PANEL_ID")
                    {
                        foreach (var item in PlcOpcGlassDataList)
                        {
                            if (item.OpcItemName == string.Format("[{1}]R{0}", word.Address, this.OpcName))
                            {
                                return new Response<object>(item.Value);
                            }
                        }
                    }
                    else
                    {
                        return new Response<object>(word.Value);
                    }
                }

                foreach (var item in this.PlcOpcWordDataList)
                {
                    if (item.DataName == word.Name)
                    {
                        return new Response<object>(item.Value);
                    }
                }

                throw new Exception(string.Format("PLC BIt 데이터 읽는데 실패하였습니다. WORD : {0}", word));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteBit(PlcBit bit, PlcBit.Signal signal)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                Opc.Da.ItemValue[] writeValues = new Opc.Da.ItemValue[1];

                foreach (var item in this.PlcOpcBitDataList)
                {
                    if (item.DataName == bit.Name)
                    {
                        foreach (var item2 in this.BitGroup.Items)
                        {
                            if (item2.ItemName == item.OpcItemName)
                            {
                                writeValues[0] = new Opc.Da.ItemValue();
                                writeValues[0].ServerHandle = item2.ServerHandle;
                                writeValues[0].Value = signal;
                            }
                        }
                    }
                }

                Opc.IRequest req;
                BitGroup.Write(writeValues, 321, new Opc.Da.WriteCompleteEventHandler(WriteCompleteCallback), out req);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteWord(PlcWord word, short[] buffer)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                Opc.Da.ItemValue[] writeValues = new Opc.Da.ItemValue[1];

                foreach (var item in this.PlcOpcWordDataList)
                {
                    if (item.DataName == word.Name)
                    {
                        foreach (var item2 in this.WordGroup.Items)
                        {
                            if (item2.ItemName == item.OpcItemName)
                            {
                                writeValues[0] = new Opc.Da.ItemValue();
                                writeValues[0].ServerHandle = item2.ServerHandle;
                                writeValues[0].Value = buffer;
                            }
                        }
                    }
                }

                // TODO :  out req에 어떤 정보가 나오는지, 리턴 정보가 있는데 어떤 정보가 리턴되고 있는지 조사 필요.
                Opc.IRequest req;
                WordGroup.Write(writeValues, 123, new Opc.Da.WriteCompleteEventHandler(WriteCompleteCallback), out req);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        static void WriteCompleteCallback(object clientHandle, Opc.IdentifiedResult[] results) { }

        #endregion

        #region private
        
        /// <summary>
        /// TODO : 재실행하는 부분 필요. (리스트 없애고 다시 생성)
        /// </summary>
        private void InitializeComponent()
        {
            BitGroup = (Opc.Da.Subscription)Server.CreateSubscription(new Opc.Da.SubscriptionState()
            {
                Name = "Bit",
                Active = true,
            });

            WordGroup = (Opc.Da.Subscription)Server.CreateSubscription(new Opc.Da.SubscriptionState()
            {
                Name = "Word",
                Active = true,
            });

            GlassGroup = (Opc.Da.Subscription)Server.CreateSubscription(new Opc.Da.SubscriptionState()
            {
                Name = "Glass",
                Active = true,
            });
            
            BitItems = new Opc.Da.Item[this.PlcOpcBitDataList.Count];
            WordItems = new Opc.Da.Item[this.PlcOpcWordDataList.Count];

            for (int i = 0; i < this.PlcOpcBitDataList.Count; i++)
            {
                BitItems[i] = new Opc.Da.Item() { ItemName = PlcOpcBitDataList[i].OpcItemName };
            }

            for (int i = 0; i < this.PlcOpcWordDataList.Count; i++)
            {
                WordItems[i] = new Opc.Da.Item() { ItemName = PlcOpcWordDataList[i].OpcItemName };
            }
            
            BitGroup.DataChanged += BitGroup_DataChanged;

            WordGroup.DataChanged += WordGroup_DataChanged;

            GlassItems = new Opc.Da.Item[9];
            GlassItems[0] = new Opc.Da.Item() { ItemName = "[aa]R0" };
            GlassItems[1] = new Opc.Da.Item() { ItemName = "[aa]R200" };
            GlassItems[2] = new Opc.Da.Item() { ItemName = "[aa]R400" };
            GlassItems[3] = new Opc.Da.Item() { ItemName = "[aa]R600" };
            GlassItems[4] = new Opc.Da.Item() { ItemName = "[aa]R800" };
            GlassItems[5] = new Opc.Da.Item() { ItemName = "[aa]R1000" };
            GlassItems[6] = new Opc.Da.Item() { ItemName = "[aa]R1200" };
            GlassItems[7] = new Opc.Da.Item() { ItemName = "[aa]R1400" };
            GlassItems[8] = new Opc.Da.Item() { ItemName = "[aa]R1600" };
            GlassItems = GlassGroup.AddItems(GlassItems);

            Opc.IRequest req;

            // TODO : (GlassGroup_ReadCompleteCallback) Read 방법 비동기처리 말고 다른 방법이 있는지 확인, 숫자 123 무슨 의미인지 확인.
            GlassGroup.Read(GlassGroup.Items, 123, new Opc.Da.ReadCompleteEventHandler(GlassGroup_ReadCompleteCallback), out req);
        }

        /// <summary>
        ///  TODO : 서버 널인지 확인을 해야 하나, 일단 삭제
        /// </summary>
        private void Connect()
        {
            try
            {
                this.Url = new URL("opcda://localhost/RSLinx OPC Server");
                this.Server = new Opc.Da.Server(new OpcCom.Factory(), Url);

                this.Server.Connect();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            finally
            {
                this.OnConnectedEvent?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// 강제로 연결 끊겼을 때 OnDisconnectedEvent 바꿔 주는 부분 되어 있지 않음. 현재는 클로즈만 되어 있음.
        /// </summary>
        private void Close()
        {
            try
            {
                if (this.Server == null) return;

                this.Server.Disconnect();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }

            finally
            {
                this.OnDisconnectedEvent?.Invoke(this, new EventArgs());
            }
        }
        
        private void Reconnect_DoWork(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (this.Connected) return;

                this.Connect();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this._trReconnect.Start();
            }
        }

        #endregion

        #endregion
    }
}
