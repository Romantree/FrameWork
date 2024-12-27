namespace TS.FW.GIGA.Managers
{
    public partial class InOutManager
    {
        [IOSetting(IN, 0x001, "EMO #1")]
        public bool X_EMO_01 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x002, "EMO #2")]
        public bool X_EMO_02 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x003, "EMO #3")]
        public bool X_EMO_03 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x004, "EMO #4")]
        public bool X_EMO_04 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x005, "EMO #5")]
        public bool X_EMO_05 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x006, "EMO #6")]
        public bool X_EMO_06 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x007, "EMO #7")]
        public bool X_EMO_07 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x008, "EMO #8")]
        public bool X_EMO_08 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x00A, "MAIN GPS FAN #1")]
        public bool X_MAIN_GPS_FAN_01 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x00B, "MAIN GPS FAN #2")]
        public bool X_MAIN_GPS_FAN_02 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x00C, "MAIN GPS FAN #3")]
        public bool X_MAIN_GPS_FAN_03 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x00D, "MAIN GPS FAN #4")]
        public bool X_MAIN_GPS_FAN_04 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x00E, "SMMC #1")]
        public bool X_SMMC_01 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x00F, "SMMC #2")]
        public bool X_SMMC_02 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x010, "DOOR LOCK #1")]
        public bool X_DOOR_LOCK_01 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x011, "DOOR LOCK #2")]
        public bool X_DOOR_LOCK_02 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x012, "DOOR LOCK #3")]
        public bool X_DOOR_LOCK_03 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x013, "DOOR LOCK #4")]
        public bool X_DOOR_LOCK_04 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x014, "DOOR LOCK #5")]
        public bool X_DOOR_LOCK_05 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x015, "DOOR LOCK #6")]
        public bool X_DOOR_LOCK_06 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x016, "DOOR LOCK #7")]
        public bool X_DOOR_LOCK_07 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x017, "DOOR LOCK #8")]
        public bool X_DOOR_LOCK_08 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x018, "DOOR LOCK #9")]
        public bool X_DOOR_LOCK_09 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x019, "DOOR LOCK #10")]
        public bool X_DOOR_LOCK_10 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x01A, "DOOR LOCK #11")]
        public bool X_DOOR_LOCK_11 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x01B, "DOOR LOCK #12")]
        public bool X_DOOR_LOCK_12 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x01C, "DOOR LOCK #13")]
        public bool X_DOOR_LOCK_13 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x01D, "DOOR LOCK #14")]
        public bool X_DOOR_LOCK_14 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x01E, "DOOR LOCK ALL")]
        public bool X_DOOR_LOCK_ALL { get => this.ReadX(); set => this.WriteX(value); }
    }

    public partial class InOutManager
    {
        [IOSetting(IN, 0x020, "ION BLOWER RUN #1")]
        public bool X_ION_BLOWER_RUN_01 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x021, "ION BLOWER ALARM #1")]
        public bool X_ION_BLOWER_ALARM_01 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x022, "ION BLOWER RUN #2")]
        public bool X_ION_BLOWER_RUN_02 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x023, "ION BLOWER ALARM #2")]
        public bool X_ION_BLOWER_ALARM_02 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x024, "ION BLOWER RUN #3")]
        public bool X_ION_BLOWER_RUN_03 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x025, "ION BLOWER ALARM #3")]
        public bool X_ION_BLOWER_ALARM_03 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x028, "MAIN CDA PRESSURE #1")]
        public bool X_MAIN_CDA_PRESSURE_01 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x029, "MAIN CDA PRESSURE #2")]
        public bool X_MAIN_CDA_PRESSURE_02 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x02A, "MAIN CDA PRESSURE #3")]
        public bool X_MAIN_CDA_PRESSURE_03 { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x02B, "START S/W")]
        public bool X_START_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x02C, "STOP S/W")]
        public bool X_STOP_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x02D, "RESET S/W")]
        public bool X_RESET_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x030, "LIFT PIN S/W")]
        public bool X_LIFT_PIN_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x031, "STAGE VACUUM S/W")]
        public bool X_STAGE_VACUUM_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x032, "HANDLE UP S/W")]
        public bool X_HANDLE_UP_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x033, "HANDLE DOWN S/W")]
        public bool X_HANDLE_DOWN_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x034, "HANDLE VACUUM S/W")]
        public bool X_HANDLE_VACUUM_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x035, "UNWINDER AIR SHAFT S/W")]
        public bool X_UNWINDER_AIR_SHAFT_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x036, "COVER FILM AIR SHAFT S/W")]
        public bool X_COVER_FILM_AIR_SHAFT_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x037, "REWINDER AIR SHAFT S/W")]
        public bool X_REWINDER_AIR_SHAFT_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x038, "CUTTING PLATE S/W")]
        public bool X_CUTTING_PLATE_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x039, "CUTTING CLAMP #1 S/W")]
        public bool X_CUTTING_CLAMP_01_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x03A, "CUTTING CLAMP #2 S/W")]
        public bool X_CUTTING_CLAMP_02_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x03B, "NIP ROLL S/W")]
        public bool X_NIP_ROLL_SW { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x03B, "MC #1")]
        public bool X_MC_01 { get => this.ReadX(); set => this.WriteX(value); }
    }

    public partial class InOutManager
    {
        [IOSetting(IN, 0x040, "EPC UNWINDER CENTER")]
        public bool X_EPC_UNWINDER_CENTER { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x041, "EPC UNWINDER DEVIATION")]
        public bool X_EPC_UNWINDER_DEVIATION { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x042, "EPC UNWINDER OVERLOAD")]
        public bool X_EPC_UNWINDER_OVERLOAD { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x043, "EPC UNWINDER ACTUATTOR LIMIT")]
        public bool X_EPC_UNWINDER_ACTUATTOR_LIMIT { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x044, "EPC UNWINDER AUTO MODE")]
        public bool X_EPC_UNWINDER_AUTO_MODE { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x045, "EPC UNWINDER MANUAL MODE")]
        public bool X_EPC_UNWINDER_MANUAL_MODE { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x047, "EPC REWINDER CENTER")]
        public bool X_EPC_REWINDER_CENTER { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x048, "EPC REWINDER DEVIATION")]
        public bool X_EPC_REWINDER_DEVIATION { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x049, "EPC REWINDER OVERLOAD")]
        public bool X_EPC_REWINDER_OVERLOAD { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x04A, "EPC REWINDER ACTUATTOR LIMIT")]
        public bool X_EPC_REWINDER_ACTUATTOR_LIMIT { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x04B, "EPC REWINDER AUTO MODE")]
        public bool X_EPC_REWINDER_AUTO_MODE { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x04C, "EPC REWINDER MANUAL MODE")]
        public bool X_EPC_REWINDER_MANUAL_MODE { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x04E, "TENSION UNWINDER ALARM")]
        public bool X_TENSION_UNWINDER_ALARM { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x04F, "TENSION REWINDER ALARM")]
        public bool X_TENSION_REWINDER_ALARM { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x050, "UNWINDER FILM CHECK SENSOR")]
        public bool X_UNWINDER_FILM_CHECK_SENSER { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x051, "COVER FILM CHECK SENSOR")]
        public bool X_COVER_FILM_FILM_CHECK_SENSER { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x052, "REWINDER FILM CHECK SENSOR")]
        public bool X_REWINDER_FILM_CHECK_SENSER { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x054, "STAGE VACUUM PRESSURE")]
        public bool X_STAGE_VACUUM { get => this.ReadX(); set => this.WriteX(value); }
    }

    public partial class InOutManager
    {
        [IOSetting(IN, 0x060, "FILM CUTTING PLATE [UP]")]
        public bool X_FILM_CUTTING_PLATE_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x061, "FILM CUTTING PLATE [DOWN]")]
        public bool X_FILM_CUTTING_PLATE_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x062, "FILM CUTTING CLAMP #1 [UP]")]
        public bool X_FILM_CUTTING_CLAMP_01_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x063, "FILM CUTTING CLAMP #1 [DOWN]")]
        public bool X_FILM_CUTTING_CLAMP_01_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x064, "FILM CUTTING CLAMP #2 [UP]")]
        public bool X_FILM_CUTTING_CLAMP_02_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x065, "FILM CUTTING CLAMP #2 [DOWN]")]
        public bool X_FILM_CUTTING_CLAMP_02_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x066, "GAP PRESS LEFT [UP]")]
        public bool X_GAP_PRESS_LEFT_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x067, "GAP PRESS LEFT [DOWN]")]
        public bool X_GAP_PRESS_LEFT_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x068, "GAP PRESS RIGHT [UP]")]
        public bool X_GAP_PRESS_RIGHT_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x069, "GAP PRESS RIGHT [DOWN]")]
        public bool X_GAP_PRESS_RIGHT_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x06A, "LIFT PIN [UP]")]
        public bool X_LIFT_PIN_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x06B, "LIFT PIN [DOWN]")]
        public bool X_LIFT_PIN_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x06C, "FILM PLATE [UP]")]
        public bool X_FILM_PLATE_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x06D, "FILM PLATE [DOWN]")]
        public bool X_FILM_PLATE_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x06E, "DEMOLD NIP ROLL [UP]")]
        public bool X_DEMOLD_NIP_ROLL_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x06F, "DEMOLD NIP ROLL [DOWN]")]
        public bool X_DEMOLD_NIP_ROLL_DOWN { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x070, "NIP ROLL [UP]")]
        public bool X_NIP_ROLL_UP { get => this.ReadX(); set => this.WriteX(value); }

        [IOSetting(IN, 0x071, "NIP ROLL [DOWN]")]
        public bool X_NIP_ROLL_DOWN { get => this.ReadX(); set => this.WriteX(value); }
    }
}
