using TS.FW.Serialization;
using XCOMLib;

namespace TS.FW.XCom
{
    public abstract class IXComModel
    {
        private XComMsgInfo _info;

        /// <summary>
        /// Stream 번호
        /// </summary>
        public int Stream { get; protected set; }

        /// <summary>
        /// Function 번호
        /// </summary>
        public int Function { get; protected set; }

        /// <summary>
        /// 메세지 전체 명
        /// </summary>
        public string FullName { get; protected set; }

        /// <summary>
        /// 메세지 명
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 메세지 하위 명
        /// </summary>
        public string SubName { get; protected set; }

        public bool IsUnDefined { get; protected set; }

        public string StreamFunction => string.Format("S{0}F{1}", this.Stream, this.Function);

        public int SysByte { get => this._info.sysByte; set => this._info.sysByte = value; }

        public string CreateJson()
        {
            return this.JsonSerializer().Result;
        }

        public void SetXComMsgInfo(XComMsgInfo info)
        {
            this._info = info;
        }

        public virtual void RecvData(IXComData data) { }

        public virtual void SendData(XCOM xcom, int msgId) { }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.StreamFunction, this.FullName);
        }
    }
}
