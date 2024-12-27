using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.GIGA.Managers
{
    public partial class InOutManager
    {
        public VacuumStatus GetVacuum()
        {
            //if (this.X_STAGE_VACUUM == true) return VacuumStatus.Vacuum;
            //if (this.STAGE_VENT == true && this.X_STAGE_VACUUM == false) return VacuumStatus.Vent;

            //if (this.STAGE_VACUUM == true) return VacuumStatus.VacuumProcess;
            //if (this.STAGE_VENT == true) return VacuumStatus.VentProcess;

            //return this.X_STAGE_VACUUM == false ? VacuumStatus.ATM : VacuumStatus.Error;

            return VacuumStatus.ATM;
        }

        public void SetVacuum(VacuumSetting set)
        {
            //switch (set)
            //{
            //    case VacuumSetting.Vacuum:
            //        {
            //            this.STAGE_VENT = false;
            //            this.STAGE_VACUUM = true;

            //            if (AP.IsSim) this.X_STAGE_VACUUM = true;
            //        }
            //        break;
            //    case VacuumSetting.Vent:
            //        {
            //            this.STAGE_VACUUM = false;
            //            this.STAGE_VENT = true;

            //            if (AP.IsSim) this.X_STAGE_VACUUM = false;
            //        }
            //        break;
            //    case VacuumSetting.Off:
            //        {
            //            this.STAGE_VACUUM = false;
            //            this.STAGE_VENT = false;
            //        }
            //        break;
            //}
        }
    }
}
