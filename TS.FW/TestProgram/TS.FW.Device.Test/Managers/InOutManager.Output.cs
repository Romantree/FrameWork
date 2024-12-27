using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device.Test.Managers
{
    public partial class InOutManager
    {
        [InOut("MC02 ON", enInOutType.OUT, 96)]
        public bool McOn_02 { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("RESET S/W ON LAMP", enInOutType.OUT, 97)]
        public bool ResetSwLamp { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("TOWER LAMP RED", enInOutType.OUT, 98)]
        public bool TowerLampRed { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("TOWER LAMP YELLOW", enInOutType.OUT, 99)]
        public bool TowerLampYellow { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("TOWER LAMP GREEN", enInOutType.OUT, 100)]
        public bool TowerLampGreen { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("BUZZER SOUND1", enInOutType.OUT, 101)]
        public bool BuzzerSound1 { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("BUZZER SOUND2", enInOutType.OUT, 102)]
        public bool BuzzerSound2 { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("ALL DOOR OPEN", enInOutType.OUT, 103)]
        public bool AllDoorOpen { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("LAMP ON", enInOutType.OUT, 104)]
        public bool LampOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("ELEC BOX LAMP ON", enInOutType.OUT, 105)]
        public bool ElecBoxLampOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("AUTO START LAMP", enInOutType.OUT, 106)]
        public bool AutoStartLamp { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("AUTO STOP LAMP", enInOutType.OUT, 107)]
        public bool AutoStopLamp { get => this.ReadY(); set => this.WriteY(value); }


        [InOut("IONIZER OFF", enInOutType.OUT, 112)]
        public bool IonizerOff { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("BOWL UNIT FAN MOTOR ON", enInOutType.OUT, 113)]
        public bool BowlUnitFanMotorOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("FRAME MOVING CW", enInOutType.OUT, 114)]
        public bool FrameMovingCw { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("FRAME MOVING RUN", enInOutType.OUT, 115)]
        public bool FrameMovingRun { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("311RM 공급펌프 ON", enInOutType.OUT, 116)]
        public bool Pump311RM_On { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("RESIN2 공급펌프 ON", enInOutType.OUT, 117)]
        public bool PumpResin2_On { get => this.ReadY(); set => this.WriteY(value); }



        [InOut("WAFER CST LOADER SOL ON", enInOutType.OUT, 128)]
        public bool WaferCstSolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("WAFER CST LOADER SOL OFF", enInOutType.OUT, 129)]
        public bool WaferCstSolOff { get => this.ReadX(); set => this.WriteY(value); }

        [InOut("OPEN SHUTTER SOL ON", enInOutType.OUT, 130)]
        public bool OpenShutterSolOn { get => this.ReadX(); set => this.WriteY(value); }

        [InOut("OPEN SHUTTER SOL OFF", enInOutType.OUT, 131)]
        public bool OpenShutterSolOff { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("CENTERING UNIT SOL#1 ON", enInOutType.OUT, 132)]
        public bool CenteringSol_1On { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("CENTERING UNIT SOL#1 OFF", enInOutType.OUT, 133)]
        public bool CenteringSol_1Off { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("CENTERING UNIT SOL#2 ON", enInOutType.OUT, 134)]
        public bool CenteringSol_2On { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("CENTERING UNIT SOL#2 OFF", enInOutType.OUT, 135)]
        public bool CenteringSol_2Off { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("WAFER LIFTING UNIT 8' SOL ON", enInOutType.OUT, 136)]
        public bool WaferLifting8_SolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("WAFER LIFTING UNIT 8' SOL OFF", enInOutType.OUT, 137)]
        public bool WaferLifting8_SolOff { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("WAFER LIFTING UNIT 12' SOL ON", enInOutType.OUT, 138)]
        public bool WaferLifting12_SolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("WAFER LIFTING UNIT 12' SOL OFF", enInOutType.OUT, 139)]
        public bool WaferLifting12_SolOff { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("CENTERING UNIT SOL ON", enInOutType.OUT, 140)]
        public bool CenteringSolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("CENTERING UNIT SOL OFF", enInOutType.OUT, 141)]
        public bool CenteringSolOff { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("SOLVENT 가압 SOL ON", enInOutType.OUT, 142)]
        public bool SolventPressureSolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("SOLVENT RETURN SOL ON", enInOutType.OUT, 143)]
        public bool SolventReturnSolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("SOLVENT BATH SOL ON", enInOutType.OUT, 144)]
        public bool SolventBathSolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("SOLVENT BOTTOM RINSE SOL ON", enInOutType.OUT, 145)]
        public bool SolventBottonRinseSolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("RESIN 1-1 SV SOL ON", enInOutType.OUT, 146)]
        public bool Resin1_1_SolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("RESIN 1-2 SV SOL ON", enInOutType.OUT, 147)]
        public bool Resin1_2_SolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("RESIN 2-1 SV SOL ON", enInOutType.OUT, 148)]
        public bool Resin2_1_SolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("RESIN 2-2 SV SOL ON", enInOutType.OUT, 146)]
        public bool Resin2_2_SolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("SOLVENT 1-1 SV SOL ON", enInOutType.OUT, 150)]
        public bool RSolvent1_1_SolOn { get => this.ReadY(); set => this.WriteY(value); }

        [InOut("SOLVENT 1-2 SV SOL ON", enInOutType.OUT, 151)]
        public bool RSolvent1_2_SolOn { get => this.ReadY(); set => this.WriteY(value); }
    }
}
