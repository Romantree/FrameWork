using System;
using System.Linq;
using TS.FW.GIGA.Networks;

namespace TS.FW.GIGA.Managers
{
    public class NetManager
    {

        public INetSerialPort this[NetworkUnit unit]
        {
            get
            {
                switch (unit)
                {
                    //case NetworkUnit.StageLeftLD: return this.StageLeftLD;
                    //case NetworkUnit.StageRightLD: return this.StageRightLD;
                    //case NetworkUnit.StageUv: return this.StageUv;
                    //case NetworkUnit.StageLeftRG: return this.StageLeftRG;
                    //case NetworkUnit.StageRightRG: return this.StageRightRG;
                    //case NetworkUnit.Temperature: return this.Temp;
                }

                return null;
            }
        }

        public void Start()
        {
            if (AP.IsSim) return;

            var flag = false;

            //if (this.Start(this.StageLeftLD, NetworkUnit.StageLeftLD) == false) flag = true;
            //if (this.Start(this.StageRightLD, NetworkUnit.StageRightLD) == false) flag = true;
            //if (this.Start(this.StageUv, NetworkUnit.StageUv) == false) flag = true;
            //if (this.Start(this.StageLeftRG, NetworkUnit.StageLeftRG) == false) flag = true;
            //if (this.Start(this.StageRightRG, NetworkUnit.StageRightRG) == false) flag = true;
            //if (this.Start(this.Temp, NetworkUnit.Temperature) == false) flag = true;

            if (flag == true) AP.System.InterlockMsgEvent("통신 연결에 실패하였습니다.");
        }

        public void Abort()
        {
            try
            {
                //this.StageUv.LedOnOff(false);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public Response Start(INetSerialPort net, NetworkUnit unit)
        {
            try
            {
                var port = "";// DB.Config[unit];

                if (string.IsNullOrWhiteSpace(port)) throw new Exception($"Port가 null 입니다. [{net.GetType()}]");
                if (EnumHelper.SerialPort.Any(t => t == port) == false) throw new Exception($"Port가 없습니다. [{net.GetType()}]");

                net.Start(port);
                net.Init();

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public bool LoadcellCheck(double left, double right, double gap = 0.1) => true;// StageLeftLD.CheckData(left, gap) && StageRightLD.CheckData(right, gap);

        public bool RequlatorCheck(double left, double right, double gap = 1) => true;// this.StageLeftRG.CheckData(left, gap) && this.StageRightRG.CheckData(right, gap);
    }

    public enum NetworkUnit
    {
        //StageLeftLD,
        //StageRightLD,
        //StageUv,
        //StageLeftRG,
        //StageRightRG,
    }
}
