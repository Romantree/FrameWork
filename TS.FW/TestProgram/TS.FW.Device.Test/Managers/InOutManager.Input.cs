using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device.Test.Managers
{
    public partial class InOutManager
    {
        #region Station #1
        //======================== Station#1 ===============================================
        [InOut("MC02 ON", enInOutType.IN, 0)]
        public bool IsMcOn_02 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("RESET S/W", enInOutType.IN, 1)]
        public bool ResetSw { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("MAINTENANCE MODE", enInOutType.IN, 2)]
        public bool MaintenanceMode { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("MAIN POWER OP EMO", enInOutType.IN, 3)]
        public bool MainPowerOP_Emo { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("COATER ZONE EMO 01", enInOutType.IN, 4)]
        public bool CoaterZone_Emo01 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("AUTO START", enInOutType.IN, 5)]
        public bool AutoStart { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("AUTO STOP", enInOutType.IN, 6)]
        public bool AutoStop { get => this.ReadX(); set => this.WriteX(value); }



        [InOut("ALL DOOR CLOSE", enInOutType.IN, 8)]
        public bool AllDoorClose { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("DOOR LOCK 01", enInOutType.IN, 9)]
        public bool DoorLock01 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("DOOR LOCK 02", enInOutType.IN, 10)]
        public bool DoorLock02 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("DOOR LOCK 03", enInOutType.IN, 11)]
        public bool DoorLock03 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("MC03 TRIP", enInOutType.IN, 12)]
        public bool MC03_Trip { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("MC04 TRIP", enInOutType.IN, 13)]
        public bool MC04_Trip { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("MC05 TRIP", enInOutType.IN, 14)]
        public bool MC05_Trip { get => this.ReadX(); set => this.WriteX(value); }


        [InOut("MAIN CDA PRESSURE", enInOutType.IN, 16)]
        public bool MainCDA_Pressure { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("MAIN EXHAUST PRESSURE", enInOutType.IN, 17)]
        public bool MainExhaustPressure { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER CST DOOR OPEN", enInOutType.IN, 18)]
        public bool WaferCstDoorOpen { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER CST DOOR CLOSE", enInOutType.IN, 19)]
        public bool WaferCstDoorClose { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("IONIZER 이온레벨경보출력신호", enInOutType.IN, 20)]
        public bool IonizerLevelWaring { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("IONIZER 컨디션경보출력", enInOutType.IN, 21)]
        public bool IonizerConditionWaring { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("IONIZER 알람출력신호", enInOutType.IN, 22)]
        public bool IonizerAlarm { get => this.ReadX(); set => this.WriteX(value); }


        [InOut("N2 PRESSURE", enInOutType.IN, 23)]
        public bool N2_Pressure { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("311RM 레벨센서 TS1", enInOutType.IN, 24)]
        public bool LevelSen311RM_TS1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("311RM 레벨센서 TS2", enInOutType.IN, 25)]
        public bool LevelSen311RM_TS2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("RESIN 레벨센서 TS3", enInOutType.IN, 26)]
        public bool LevelSenResin_TS3 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("RESIN 레벨센서 TS4", enInOutType.IN, 27)]
        public bool LevelSenResin_TS4 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("SOLVENT 레벨센서 TS5", enInOutType.IN, 28)]
        public bool LevelSenSolvent_TS5 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("SOLVENT 레벨센서 TS6", enInOutType.IN, 29)]
        public bool LevelSenSolvent_TS6 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("SOLVENT 레벨센서 TS7", enInOutType.IN, 30)]
        public bool LevelSenSolvent_TS7 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("SOLVENT 레벨센서 TS8", enInOutType.IN, 31)]
        public bool LevelSenSolvent_TS8 { get => this.ReadX(); set => this.WriteX(value); }
        //======================== Station#1 End============================================
        #endregion

        #region Station #2
        //======================== Station#2 ===============================================
        [InOut("CASSETTE 안착", enInOutType.IN, 32)]
        public bool CstDetect { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("8' CASSETTE MODEL", enInOutType.IN, 33)]
        public bool Cst8Model { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("12' CASSETTE MODEL", enInOutType.IN, 34)]
        public bool Cst12Model { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("8' WAFER 이탈감지", enInOutType.IN, 35)]
        public bool Wafer8LeaveDetect { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("12' WAFER 이탈감지", enInOutType.IN, 36)]
        public bool Wafer12LeaveDetect { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER CST LOADER 전진 ", enInOutType.IN, 37)]
        public bool WaferCstLoaderFwd { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER CST LOADER 후진 ", enInOutType.IN, 38)]
        public bool WaferCstLoaderBwd { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER CST LOADER DOOR 감지1", enInOutType.IN, 39)]
        public bool WaferCstLoaderDoorDetect1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER CST LOADER DOOR 감지2", enInOutType.IN, 40)]
        public bool WaferCstLoaderDoorDetect2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("OPEN SHUTTER CYL감지 1-1", enInOutType.IN, 41)]
        public bool OpenShutterCylDetect1_1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("OPEN SHUTTER CYL감지 1-2", enInOutType.IN, 42)]
        public bool OpenShutterCylDetect1_2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("OPEN SHUTTER CYL감지 2-1", enInOutType.IN, 43)]
        public bool OpenShutterCylDetect2_1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("OPEN SHUTTER CYL감지 2-2", enInOutType.IN, 44)]
        public bool OpenShutterCylDetect2_2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("CENTERING UNIT CYL감지1", enInOutType.IN, 45)]
        public bool CenteringCylDetect1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("CENTERING UNIT CYL감지2", enInOutType.IN, 46)]
        public bool CenteringCylDetect2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("CENTERING UNIT 제품감지", enInOutType.IN, 47)]
        public bool CenteringWaferDetect { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER LIFT UNIT 8' CYL감지 1-1", enInOutType.IN, 48)]
        public bool Wafer8LiftCylDetect1_1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER LIFT UNIT 8' CYL감지 1-2", enInOutType.IN, 49)]
        public bool Wafer8LiftCylDetect1_2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER LIFT UNIT 8' CYL감지 1-3", enInOutType.IN, 50)]
        public bool Wafer8LiftCylDetect1_3 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER LIFT UNIT 12' CYL감지 1-1", enInOutType.IN, 51)]
        public bool Wafer12LiftCylDetect1_1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER LIFT UNIT 12' CYL감지 1-2", enInOutType.IN, 52)]
        public bool Wafer12LiftCylDetect1_2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("WAFER LIFT UNIT 12' CYL감지 1-3", enInOutType.IN, 53)]
        public bool Wafer12LiftCylDetect1_3 { get => this.ReadX(); set => this.WriteX(value); }
        //======================== Station#2 End============================================
        #endregion

        #region Station #3
        //======================== Station#3 ===============================================
        [InOut("SOLVENT FM1", enInOutType.IN, 64)]
        public bool SolventFM1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("SOLVENT FM2", enInOutType.IN, 65)]
        public bool SolventFM2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("SOLVENT FM3", enInOutType.IN, 66)]
        public bool SolventFM3 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("리크센서 PLS1", enInOutType.IN, 67)]
        public bool LeakSensorPls1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("리크센서 PLS2", enInOutType.IN, 68)]
        public bool LeakSensorPls2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("폐액센서 DS1", enInOutType.IN, 69)]
        public bool Wasteliquid_DS1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("폐액센서 DS2", enInOutType.IN, 70)]
        public bool Wasteliquid_DS2 { get => this.ReadX(); set => this.WriteX(value); }


        [InOut("DOOR SENSOR 좌", enInOutType.IN, 80)]
        public bool DoorSensorLeft { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("DOOR SENSOR 중앙1", enInOutType.IN, 81)]
        public bool DoorSensorCenter1 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("DOOR SENSOR 중앙2", enInOutType.IN, 82)]
        public bool DoorSensorCenter2 { get => this.ReadX(); set => this.WriteX(value); }

        [InOut("DOOR SENSOR 우", enInOutType.IN, 83)]
        public bool DoorSensorRight { get => this.ReadX(); set => this.WriteX(value); }
        //======================== Station#3 End============================================
        #endregion
    }
}
