﻿namespace TS.FW.XCom.SDC.OLED.V227
{
    public enum eCEID
    {
        JOB_PROCESS_START = 1,
        JOB_PROCESS_CANCEL,
        JOB_PROCESS_ABORT,
        JOB_PROCESS_END,

        GLASS_CASSETTE_OUT = 6,
        GLASS_CASSETTE_IN,
        MIDDLE_SUMMARIZED_MERGE,
        SUMMARIZED_MERGE,

        GLASS_ID_READ = 10,
        GLASS_PROCESS_START,
        GLASS_PROCESS_CANCEL,
        GLASS_PROCESS_ABORT,
        GLASS_PROCESS_END,
        GLASS_JUDGEMENT_RESULT,
        GLASS_MODULE_IN,
        GLASS_MODULE_OUT,
        GLASS_SCRAP,
        GLASS_UNSCRAP,
        GLASS_ASSEMBLY,

        PROCESS_STEP_CHANGE_EVENT = 23,

        PROCESS_MODULE_CHOICE = 24,

        ATTACHMENT_OR_DETACHMENT_EVENT = 25,

        UNIT_PROCESS_START = 26,
        UNIT_PROCESS_END,

        JOB_START_PERMISSION_EVENT = 30,

        CASSETTE_LOAD_REQUEST = 31,
        CASSETTE_PRE_LOAD,
        CASSETTE_LOAD_COMPLETE,
        CASSETTE_UNLOAD_REQUEST,
        CASSETTE_UNLOAD_COMPLETE,
        CASSETTE_LOAD_REJECTED,
        PORT_PARAMETER_CHANGE,

        MATERIAL_STOCK_INFO_CHANGE_EVENT = 38,

        NG_CASSETTE_FULL = 41,
        CONVEYOR_BUFFER_FULL,
        BUFFER_CASSETTE_FULL,
        CASSETTE_QTY_COUNT_FULL,

        MODULE_PROCESS_STATE_CHANGE = 51,

        MODULE_STATE_CHANGE = 53,

        ENVIRONMENT_STATE_CHANGE_EVP = 54,
        REUSE_MODE_CHANGE_EVNET_EVP,

        CHANGE_TO_OFF_LINE_MODE = 71,
        CHANGE_TO_ON_LINE_LOCAL_MODE,
        CHANGE_TO_ON_LINE_REMOTE_MODE,

        CANNOT_FIND_GLASS = 61,
        CANNOT_READ_GLASS,
        CANNOT_SAVE_GLASS,
        CANNOT_SAVE_LOT_GLASS,
        CANNOT_SAVE_IMAGE_GLASS,
        FILE_FORMAT_IS_DIFFERENT_GLASS,

        PARAMETER_EOID_CHANGE = 101,
        PARAMETER_ECID_CHANGE,

        CARRIER_MAKE_COMPLETE = 104,

        GLASSID_READ_FAIL = 111,
        H_GLASSID_SEARCH_FAIL,
        GLASSID_MISMATCH,

        CASSETTE_IN = 116,
        CASSETTE_OUT,

        CHAMBER_CLEANING_START = 121,
        CHAMBER_CLEANING_END,
        END_POINT_DETECTION,

        CURRENT_PPID_CHANGE = 131,

        IN_EQ_PATH_DEFINITION_CHANGE_EVENT = 132,

        NETWORK_SERVER_NOT_CONNECTED = 171,

        GLASS_LOAD_COMPLETE = 133,
        GLASS_UNLOAD_COMPLETE = 135,

        SPLIT_RECIPE_PROCESSING_EXPIRATION_EVENT = 141,

        MATERIAL_STOCK_IN = 201,
        MATERIAL_STOCK_OUT,
        MATERIAL_DOCK_IN,
        MATERIAL_DOCK_OUT,
        MATERIAL_SUPPLEMENT_REQUEST,
        MATERIAL_EXCHANGE_REQUEST,
        MATERIAL_CONSUME_START,
        MATERIAL_CONSUME_END,
        GAS_CHEMICAL_CHANGE_START,
        GAS_CHEMICAL_CHANGE_END,

        JOB_PROCESS_END_FOR_LABEL_PRINTING = 217,

        MATERIAL_TRANSFER_AVAILABILITY_STATE_CHANGE = 221,
        MATERIAL_LIBRARY_STAGE_STATE_CHANGE,

        SORTING_JOB_START = 301,
        SORTING_JOB_CANCEL,
        SORTING_JOB_ABORT,
        SORTING_JOB_END,
        SCAN_JOB_START,
        SCAN_JOB_END,
        CASSETTE_UPDATE_REQUEST,

        PRODUCTION_PLAN_START = 501,
        PRODUCTION_PLAN_CANCEL,
        PRODUCTION_PLAN_ABORT,
        PRODUCTION_PLAN_END,
        PRODUCTION_PLAN_PAUSE,
        PRODUCTION_PLAN_RESUME,

        PRODUCTION_PLAN_CREATE = 511,
        PRODUCTION_PLAN_DELETE,
        PRODUCTION_PLAN_MODIFY,

        PRODUCTION_PLAN_BATCH_CHECK_REQUEST = 527,
        PREBATCH_AVAILABLE_CHECK_REQUEST,
        BATCH_AVAILABLE_CHECK_REQUEST,

        PRODUCTION_PLAN_CREATE_MANUAL = 580,
        PRODUCTION_PLAN_DELETE_MANUAL,
        PRODUCTION_PLAN_CHANGE_MANUAL,
        PRODUCTION_FULL_INFO_REQUEST,
        PRODUCTION_BATCH_PLAN_REGISTER_REQUEST_EVENT,
        CURRENT_PRODUCT_PLAN_REGISTER_EVENT,
        PRODUCTION_BATCH_LOW_PRODUCE_RESULT_EVENT,

        PRODUCT_PLAN_INFO_REQUEST = 591,

        MODULE_MATERIAL_SUPPLEMENT_REQUEST = 601,
        MODULE_MATERIAL_STOCK_IN,
        MODULE_MATERIAL_DOCK_IN,
        MODULE_MATERIAL_CONSUME_START,

        MODULE_MATERIAL_ASSEMBLE = 605,

        MODULE_MATERIAL_CONSUME_END = 606,
        MODULE_MATERIAL_DOCK_OUT,
        MODULE_MATERIAL_STOCK_OUT,
        MODULE_MATERIAL_SHORTAGE,
        MODULE_MATERIAL_NG,
        MODULE_MATERIAL_INFORMATION_UPDATE,

        MODULE_MATERIAL_CODE_INFORMATION_REQUEST = 612,

        LABEL_PRINTING_COMPLETED_EVENT = 630,
        LABEL_SERIAL_NUMBER_REQUEST_EVENT,

        LABEL_INFORMATION_REQUEST_EVENT = 632,

        VCD_MCR_READING_RATE_EVENT = 661,

        MATERIAL_JOB_PROCESS_START = 1001,
        MATERIAL_JOB_PROCESS_CANCEL,
        MATERIAL_JOB_PROCESS_ABORT,
        MATERIAL_JOB_PROCESS_END,
        MATERIAL_CASSETTE_OUT,
        MATERIAL_CASSETTE_IN,
        MATERIAL_MIDDLE_SUMMARIZED_MERGE,
        MATERIAL_SUMMARIZED_MERGE,

        MATERIAL_ID_READ = 1010,
        MATERIAL_PROCESS_START,
        MATERIAL_PROCESS_CANCEL,
        MATERIAL_PROCESS_ABORT,
        MATERIAL_PROCESS_END,
        MATERIAL_JUDGEMENT_RESULT,
        MATERIAL_MODULE_IN,
        MATERIAL_MODULE_OUT,
        MATERIAL_SCRAP,
        MATERIAL_UNSCRAP,

        MATERIAL_UNIT_PROCESS_START = 1026,
        MATERIAL_UNIT_PROCESS_END,

        CANNOT_FIND_MATERIAL = 1061,
        CANNOT_READ_MATERIAL,
        CANNOT_SAVE_MATERIAL,
        CANNOT_SAVE_LOT_MATERIAL,
        CANNOT_SAVE_IMAGE_MATERIAL,
        FILE_FORMAT_IS_DIFFERENT_MATERIAL,

        MATERIAL_ID_READ_FAIL = 1111,
        H_MATERIAL_ID_SEARCH_FAIL,
        MATERIAL_ID_MISMATCH,

        MATERIAL_LOAD_COMPLETE = 1133,
        MATERIAL_UNLOAD_COMPLETE = 1135,
    }
}
