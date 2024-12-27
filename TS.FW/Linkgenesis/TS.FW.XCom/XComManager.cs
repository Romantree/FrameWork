using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TS.FW.XCom.Core;
using XCOMLib;

namespace TS.FW.XCom
{
    public class XComManager
    {
        internal readonly XCOM xcom = null;
        internal readonly XComReader _reader = null;
        internal readonly Assembly _assembly = null;
        internal readonly string _projectName = null;

        private int _devId = 1;
        private SECS_STATE _state = SECS_STATE.UNKNOWN;
        public event EventHandler<SECS_STATE> OnStateChangingEvent;
        public event EventHandler<SECS_STATE> OnStateChangedEvent;
        public event EventHandler<IXComModel> OnSendMessageEvent;
        public event EventHandler<IXComModel> OnRecvMessageEvent;

        public XComManager(string cfgFilePath, Assembly assembly, string projectName)
        {
            this._reader = new XComReader(cfgFilePath);
            this._assembly = assembly;
            this._projectName = projectName;

            this.xcom = new XCOM();
            this.xcom.SecsEvent += Xcom_SecsEvent;
            this.xcom.SecsMsg += Xcom_SecsMsg;
        }

        public XComConfig Config => this._reader.Config;

        public SECS_STATE State
        {
            get
            {
                return this._state;
            }
            set
            {
                var old = this._state;

                if (old != value)
                {
                    this.OnStateChangingEvent?.Invoke(this, value);
                }

                this._state = value;

                this.OnStateChangedEvent?.Invoke(this, value);
            }
        }

        public Response Start()
        {
            try
            {
                this._devId = this.Config.DeviceID;
                this._reader.LoadData();

                this.xcom.LoadControl();
                this.xcom.InitializeEx(this.Config.ConfigFilePath);
                this.xcom.StartEx();

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response Stop()
        {
            try
            {
                this.xcom.StopEx();

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response SendMsg(IXComModel model, int sysByte = 0)
        {
            try
            {
                this.SendMessage(model, sysByte);

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        private void SendMessage(IXComModel model, int sysByte)
        {
            if (model == null) return;
            if (this.State != SECS_STATE.SELECTED) return;

            if (this.xcom.InvokeRequired)
            {
                this.xcom.Invoke(new Action<IXComModel, int>(this.SendMessage), model, sysByte);
            }
            else
            {
                Logger.Write(this, string.Format("[SEND] {0}", model.StreamFunction));

                var msg = this._reader.FindMessage(model);
                if (msg == null) throw new NotImplementedException(string.Format("등록된 메세지가 없습니다. {0}", model.StreamFunction));

                var msgId = this.xcom.MakeMsg(this._devId, msg.Stream, msg.Function, sysByte);
                if (msgId <= 0) throw new Exception(string.Format("메세지 ID 생성에 실패하였습니다. {0}, sysByte:{1}", model.StreamFunction, sysByte));

                msg.SetSendData(msgId, this.xcom, model);

                this.xcom.SendMsg(msgId);

                this.OnSendMessageEvent?.Invoke(this, model);
            }
        }

        private void RecvMessage(XComMsgInfo info)
        {
            try
            {
                Logger.Write(this, string.Format("[RECV] {0}", info.StreamFunction));

                var data = IXComData.ToXComData(this.xcom.GetMsg(info.msgID));

                if (data == null) return; //230322 by Jp

                var msg = this._reader.FindMessage(info.stream, info.func, data);
                if (msg == null) throw new NotImplementedException(string.Format("[{0}] unknown Recv Message.", info.StreamFunction));

                var model = msg.CreateMsgData(info, data, this._projectName, this._assembly);

                this.OnRecvMessageEvent?.Invoke(this, model);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Xcom_SecsMsg(object sender, EventArgs e)
        {
            try
            {
                foreach (var info in this.xcom.LoadMsg())
                {
                    this.RecvMessage(info);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Xcom_SecsEvent(object sender, _DeXComEvents_SecsEventEvent e)
        {
            try
            {
                switch (e.nEventId)
                {
                    case (int)SECS_STATE.UNKNOWN:
                    case (int)SECS_STATE.NOT_CONNECTED:
                    case (int)SECS_STATE.NOT_SELECTED:
                        {
                            this.State = (SECS_STATE)e.nEventId;
                        }
                        break;
                    case (int)SECS_STATE.SELECTED:
                        {
                            this.State = SECS_STATE.SELECTED;
                        }
                        break;
                    case (int)SECS_STATE.ALMXC_T3:
                        {
                            var info = this.xcom.GetAlarmMsgInfo(e.lParam);
                            Logger.Write(this, string.Format("{0} Secondary Message Receive Timeout.", info.StreamFunction), Logger.LogEventLevel.Error);
                        }
                        break;
                    case (int)SECS_STATE.ALMXC_PROTECTION:
                        {
                            Logger.Write(this, "Protection error XCom 의 하드웨어키의 체크에 실패 하였을 때 발생.", Logger.LogEventLevel.Error);
                        }
                        break;
                    case (int)SECS_STATE.ALMXC_INVALID_MSG:
                        {
                            var info = this.xcom.GetInvalidMsgInfo(e.lParam);
                            Logger.Write(this, string.Format("{0} Invalid Message Receive.", info.StreamFunction), Logger.LogEventLevel.Error);
                        }
                        break;
                    case (int)SECS_STATE.ALMXC_UNKNOWN_DEV_ID:
                        {
                            Logger.Write(this, string.Format("Unknown DeviceId[{0}] Receive.", e.lParam), Logger.LogEventLevel.Error);
                        }
                        break;
                    case (int)SECS_STATE.ALMXC_UNKNOWN_STREAM:
                        {
                            Logger.Write(this, string.Format("Unknown Stream[{0}] Receive.", e.lParam), Logger.LogEventLevel.Error);
                        }
                        break;
                    case (int)SECS_STATE.ALMXC_UNKNOWN_FUNC:
                        {
                            Logger.Write(this, string.Format("Unknown Function[{0}] Receive.", e.lParam), Logger.LogEventLevel.Error);
                        }
                        break;
                    default:
                        {
                            Logger.Write(this, string.Format("UnDefined EventId[{0}] lParam[{1}].", e.nEventId, e.lParam), Logger.LogEventLevel.Error);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
