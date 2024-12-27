namespace TS.FW.XCom
{
    public struct XComMsgInfo
    {
        public int msgID;
        public short deviceID;
        public short stream;
        public short func;
        public int sysByte;
        public short wBit;

        public string StreamFunction => string.Format("S{0}F{1}", this.stream, this.func);
    }
}
