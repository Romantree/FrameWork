namespace TS.FW.GIGA.Managers
{
    public partial class InOutManager
    {
        [IOSetting(OUT, OA + 0x001, "TOWER LAMP RED")]
        public bool TOWER_LAMP_RED { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x002, "TOWER LAMP YELLOW")]
        public bool TOWER_LAMP_YELLOW { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x003, "TOWER LAMP GREEN")]
        public bool TOWER_LAMP_GREEN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x004, "BUZZER #1")]
        public bool BUZZER_01 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x005, "BUZZER #2")]
        public bool BUZZER_02 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x006, "EQ LED")]
        public bool EQ_LED { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x00E, "SMMC_#1")]
        public bool SMMC_01 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x00F, "SMMC #2")]
        public bool SMMC_02 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x010, "DOOR LOCK #1")]
        public bool DOOR_LOCK_01 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x011, "DOOR LOCK #2")]
        public bool DOOR_LOCK_02 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x012, "DOOR LOCK #3")]
        public bool DOOR_LOCK_03 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x013, "DOOR LOCK #4")]
        public bool DOOR_LOCK_04 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x014, "DOOR LOCK #5")]
        public bool DOOR_LOCK_05 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x015, "DOOR LOCK #6")]
        public bool DOOR_LOCK_06 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x016, "DOOR LOCK #7")]
        public bool DOOR_LOCK_07 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x017, "DOOR LOCK #8")]
        public bool DOOR_LOCK_08 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x018, "DOOR LOCK #9")]
        public bool DOOR_LOCK_09 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x019, "DOOR LOCK #10")]
        public bool DOOR_LOCK_10 { get => this.ReadY(); set => this.WriteY(value); }
    }

    public partial class InOutManager
    {
        [IOSetting(OUT, OA + 0x020, "ION BLOWER RUN #1")]
        public bool ION_BLOWER_RUN_01 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x021, "ION BLOWER TIP CLEANING #1")]
        public bool ION_BLOWER_TIP_CLEANING_01 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x022, "ION BLOWER RUN #2")]
        public bool ION_BLOWER_RUN_02 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x023, "ION BLOWER TIP CLEANING #2")]
        public bool ION_BLOWER_TIP_CLEANING_02 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x024, "ION BLOWER RUN #3")]
        public bool ION_BLOWER_RUN_03 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x025, "ION BLOWER TIP CLEANING #3")]
        public bool ION_BLOWER_TIP_CLEANING_03 { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x026, "STAGE VACUUM")]
        public bool STAGE_VACUUM { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x027, "STAGE VENT")]
        public bool STAGE_VENT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x028, "UV LAMP ON")]
        public bool UV_LAMP_ON { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x029, "LOADCELL ZERO LEFT")]
        public bool LOADCELL_ZERO_LEFT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x02A, "LOADCELL ZERO RIGHT")]
        public bool LOADCELL_ZERO_RIGHT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x02B, "START S/W LAMP")]
        public bool START_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x02C, "STOP S/W LAMP")]
        public bool STOP_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x02D, "RESET S/W LAMP")]
        public bool RESET_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x030, "LIFT PIN S/W LAMP")]
        public bool LIFT_PIN_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x031, "STAGE VACUUM S/W LAMP")]
        public bool STAGE_VACUUM_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x032, "HANDLE UP S/W LAMP")]
        public bool HANDLE_UP_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x033, "HANDLE DOWN S/W LAMP")]
        public bool HANDLE_DOWN_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x034, "HANDLE VACUUM S/W LAMP")]
        public bool HANDLE_VACUUM_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x035, "UNWINDER AIR SHAFT S/W LAMP")]
        public bool UNWINDER_AIR_SHAFT_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x036, "COVER FILM AIR SHAFT S/W LAMP")]
        public bool COVER_FILM_AIR_SHAFT_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x037, "REWINDER AIR SHAFT S/W LAMP")]
        public bool REWINDER_AIR_SHAFT_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x038, "CUTTING PLATE S/W LAMP")]
        public bool CUTTING_PLATE_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x039, "CUTTING CLAMP #1 S/W LAMP")]
        public bool CUTTING_CLAMP_01_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x03A, "CUTTING CLAMP #2 S/W LAMP")]
        public bool CUTTING_CLAMP_02_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x03B, "NIP ROLL S/W LAMP")]
        public bool NIP_ROLL_SW_LAMP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x03C, "MC #1")]
        public bool MC_01 { get => this.ReadY(); set => this.WriteY(value); }
    }

    public partial class InOutManager
    {
        [IOSetting(OUT, OA + 0x040, "EPC UNWINDER OUT")]
        public bool EPC_UNWINDER_OUT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x041, "EPC UNWINDER IN")]
        public bool EPC_UNWINDER_IN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x042, "EPC UNWINDER CENTER")]
        public bool EPC_UNWINDER_CENTER { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x043, "EPC UNWINDER AUTO MODE")]
        public bool EPC_UNWINDER_AUTO_MODE { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x044, "EPC UNWINDER MANUAL MODE")]
        public bool EPC_UNWINDER_MANUAL_MODE { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x047, "EPC REWINDER OUT")]
        public bool EPC_REWINDER_OUT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x048, "EPC REWINDER IN")]
        public bool EPC_REWINDER_IN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x049, "EPC REWINDER CENTER")]
        public bool EPC_REWINDER_CENTER { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x04A, "EPC REWINDER AUTO MODE")]
        public bool EPC_REWINDER_AUTO_MODE { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x04B, "EPC REWINDER MANUAL MODE")]
        public bool EPC_REWINDER_MANUAL_MODE { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x04C, "TENSION UNWINDER AUTO")]
        public bool TENSION_UNWINDER_AUTO { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x04D, "TENSION UNWINDER OUT")]
        public bool TENSION_UNWINDER_OUT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x04E, "TENSION UNWINDER EMS")]
        public bool TENSION_UNWINDER_EMS { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x050, "TENSION REWINDER AUTO")]
        public bool TENSION_REWINDER_AUTO { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x051, "TENSION REWINDER OUT")]
        public bool TENSION_REWINDER_OUT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x052, "TENSION REWINDER EMS")]
        public bool TENSION_REWINDER_EMS { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x054, "TENSION COVER FILM EMS")]
        public bool TENSION_COVER_FILM_EMS { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x055, "TENSION COVER FILM OUT")]
        public bool TENSION_COVER_FILM_OUT { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x056, "TENSION COVER FILM RST")]
        public bool TENSION_COVER_FILM_RST { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x057, "TENSION COVER FILM BWN")]
        public bool TENSION_COVER_FILM_BWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x058, "TENSION COVER FILM RUN")]
        public bool TENSION_COVER_FILM_RUN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x05A, "AIR SHAFT UNWINDER")]
        public bool AIR_SHAFT_UNWINDER { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x05B, "AIR SHAFT REWINDER")]
        public bool AIR_SHAFT_REWINDER { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x05C, "AIR SHAFT COVER FILM")]
        public bool AIR_SHAFT_COVER_FILM { get => this.ReadY(); set => this.WriteY(value); }
    }

    public partial class InOutManager
    {
        [IOSetting(OUT, OA + 0x060, "FILM CUTTING PLATE [UP]")]
        public bool FILM_CUTTING_PLATE_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x061, "FILM CUTTING PLATE [DOWN]")]
        public bool FILM_CUTTING_PLATE_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x062, "FILM CUTTING CLAMP #1 [UP]")]
        public bool FILM_CUTTING_CLAMP_01_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x063, "FILM CUTTING CLAMP #1 [DOWN]")]
        public bool FILM_CUTTING_CLAMP_01_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x064, "FILM CUTTING CLAMP #2 [UP]")]
        public bool FILM_CUTTING_CLAMP_02_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x065, "FILM CUTTING CLAMP #2 [DOWN]")]
        public bool FILM_CUTTING_CLAMP_02_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x066, "GAP PRESS LEFT [UP]")]
        public bool GAP_PRESS_LEFT_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x067, "GAP PRESS LEFT [DOWN]")]
        public bool GAP_PRESS_LEFT_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x068, "GAP PRESS RIGHT [UP]")]
        public bool GAP_PRESS_RIGHT_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x069, "GAP PRESS RIGHT [DOWN]")]
        public bool GAP_PRESS_RIGHT_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x06A, "LIFT PIN [UP]")]
        public bool LIFT_PIN_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x06B, "LIFT PIN [DOWN]")]
        public bool LIFT_PIN_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x06C, "FILM PLATE [UP]")]
        public bool FILM_PLATE_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x06D, "FILM PLATE [DOWN]")]
        public bool FILM_PLATE_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x06E, "DEMOLD NIP ROLL [UP]")]
        public bool DEMOLD_NIP_ROLL_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x06F, "DEMOLD NIP ROLL [DOWN]")]
        public bool DEMOLD_NIP_ROLL_DOWN { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x070, "NIP ROLL [UP]")]
        public bool NIP_ROLL_UP { get => this.ReadY(); set => this.WriteY(value); }

        [IOSetting(OUT, OA + 0x071, "NIP ROLL [DOWN]")]
        public bool NIP_ROLL_DOWN { get => this.ReadY(); set => this.WriteY(value); }
    }
}
