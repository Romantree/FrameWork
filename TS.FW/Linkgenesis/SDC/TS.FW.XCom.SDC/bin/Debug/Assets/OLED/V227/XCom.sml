<XCom 2.4>
<S1F1 P AreyouThereReq
>

<S1F2 S OnLineData - EQ
  <LIST 4 
    <ASCII 20 VERSION>
    <ASCII 20 SPEC_CODE>
    <ASCII 28 MODULE_ID>
    <UINT1 1 MCMD>
  >
>

<S1F2 S OnLineData - Host
  <LIST 0 >
>

<S1F3 P SelectedEQStateReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <UINT2 1 SVID>
    >
  >
>

<S1F4 S SelectedEQStateData
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 5 
        <UINT2 1 SVID>
        <ASCII 40 SV>
        <ASCII 40 SVNAME>
        <ASCII 10 DATA_TYPE>
        <ASCII 28 MODULE_ID>
      >
    >
  >
>

<S1F5 P FromatStateReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
  >
>

<S1F6 S FromatStateData - EQOnlineParam
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 2 
        <UINT1 1 EOID>
        <LIST n 
          <LIST 2 
            <UINT1 1 EOMD>
            <UINT1 1 EOV>
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - PortState
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 2 
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
      >
    >
  >
>

<S1F6 S FromatStateData - GlassTracking
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST 1 
      <LIST 21 
        <ASCII 16 H_GLASSID>
        <ASCII 16 E_GLASSID>
        <ASCII 16 LOTID>
        <ASCII 16 BATCHID>
        <ASCII 16 JOBID>
        <ASCII 4 PORTID>
        <ASCII 2 SLOTNO>
        <ASCII 4 PRODUCT_TYPE>
        <ASCII 4 PORDUCT_KIND>
        <ASCII 16 PORDUCTID>
        <ASCII 16 RUNSPECID>
        <ASCII 8 LAYERID>
        <ASCII 8 STEPID>
        <ASCII 20 PPID>
        <ASCII 20 FLOWID>
        <UINT2 2 GLASS_SIZE>
        <UINT2 1 GLASS_THICKNESS>
        <UINT2 1 GLASS_STATE>
        <ASCII 4 GLASS_ORDER>
        <ASCII 16 COMMENT>
        <LIST 3 
          <LIST 10 
            <ASCII 4 USE_COUNT>
            <ASCII 4 JUDGEMENT>
            <ASCII 4 REASON_CODE>
            <ASCII 2 INS_FLAG>
            <ASCII 2 ENC_FLAG>
            <ASCII 2 PRERUN_FLAG>
            <ASCII 2 TURN_DIR>
            <ASCII 2 FLIP_STATE>
            <ASCII 4 WORK_STATE>
            <ASCII 16 MULTI_USE>
          >
          <LIST 2 
            <ASCII 16 PAIR_GLASSID>
            <ASCII 20 PAIR_PPID>
          >
          <LIST 5 
            <LIST 2 
              <ASCII 40 OPTION_NAME_1>
              <ASCII 40 OPTION_VALUE_1>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_2>
              <ASCII 40 OPTION_VALUE_2>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_3>
              <ASCII 40 OPTION_VALUE_3>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_4>
              <ASCII 40 OPTION_VALUE_4>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_5>
              <ASCII 40 OPTION_VALUE_5>
            >
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - ModuleState
  <LIST 2 
    <UINT1 1 SFCD>
    <LIST 5 
      <ASCII 28 MODULE_ID>
      <UINT1 1 MODULE_STATE>
      <UINT1 1 PROC_STATE>
      <UINT1 1 MCMD>
      <LIST n 
        <LIST 4 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <LIST n 
            <LIST 4 
              <ASCII 28 MODULE_ID>
              <UINT1 1 MODULE_STATE>
              <UINT1 1 PROC_STATE>
              <LIST n 
                <LIST 4 
                  <ASCII 28 MODULE_ID>
                  <UINT1 1 MODULE_STATE>
                  <UINT1 1 PROC_STATE>
                  <LIST n 
                    <LIST 3 
                      <ASCII 28 MODULE_ID>
                      <UINT1 1 MODULE_STATE>
                      <UINT1 1 PROC_STATE>
                    >
                  >
                >
              >
            >
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - MaterialTracking
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 21 
        <ASCII 16 MATERIAL_ID>
        <ASCII 16 MATERIAL_SETID>
        <ASCII 16 LOTID>
        <ASCII 16 BATCHID>
        <ASCII 16 JOBID>
        <ASCII 4 PORTID>
        <ASCII 2 SLOTNO>
        <ASCII 4 PRODUCT_TYPE>
        <ASCII 4 PRODUCT_KIND>
        <ASCII 16 PRODUCTID>
        <ASCII 16 RUNSPECID>
        <ASCII 8 LAYERID>
        <ASCII 8 STEPID>
        <ASCII 20 PPID>
        <ASCII 20 FLOWID>
        <UINT2 2 MATERIAL_SIZE>
        <UINT2 1 MATERIAL_THICKNESS>
        <UINT2 1 MATERIAL_STATE>
        <ASCII 4 MATERIAL_ORDER>
        <ASCII 16 COMMENT>
        <LIST 3 
          <LIST 10 
            <ASCII 4 USE_COUNT>
            <ASCII 4 JUDGEMENT>
            <ASCII 4 REASON_CODE>
            <ASCII 2 INS_FLAG>
            <ASCII 2 LIBRARYID>
            <ASCII 2 PRERUN_FLAG>
            <ASCII 2 TURN_DIR>
            <ASCII 2 FLIP_STATE>
            <ASCII 4 WORK_STATE>
            <ASCII 16 MULTI_USE>
          >
          <LIST 2 
            <ASCII 16 STAGE_STATE>
            <ASCII 20 LOCATION>
          >
          <LIST 5 
            <LIST 2 
              <ASCII 40 OPTION_NAME_1>
              <ASCII 40 OPTION_VALUE_1>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_2>
              <ASCII 40 OPTION_VALUE_2>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_3>
              <ASCII 40 OPTION_VALUE_3>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_4>
              <ASCII 40 OPTION_VALUE_4>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_5>
              <ASCII 40 OPTION_VALUE_5>
            >
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - MaskStock
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 3 
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
        <LIST 7 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
          <UINT1 1 ENVIRONMENT>
          <UINT1 1 REUSE_MODE>
        >
        <LIST n 
          <LIST 7 
            <ASCII 12 CSTID>
            <UINT1 1 CST_TYPE>
            <ASCII 56 MAP_STIF>
            <ASCII 56 CUR_STIF>
            <UINT1 1 BATCH_ORDER>
            <UINT1 1 POSITION>
            <LIST n 
              <LIST 21 
                <ASCII 16 MATERIAL_ID>
                <ASCII 16 MATERIAL_SETID>
                <ASCII 16 LOTID>
                <ASCII 16 BATCHID>
                <ASCII 16 JOBID>
                <ASCII 4 PORTID>
                <ASCII 2 SLOTNO>
                <ASCII 4 PRODUCT_TYPE>
                <ASCII 4 PRODUCT_KIND>
                <ASCII 16 PRODUCTID>
                <ASCII 16 RUNSPECID>
                <ASCII 8 LAYERID>
                <ASCII 8 STEPID>
                <ASCII 20 PPID>
                <ASCII 20 FLOWID>
                <UINT2 2 MATERIAL_SIZE>
                <UINT2 1 MATERIAL_THICKNESS>
                <UINT2 1 MATERIAL_STATE>
                <ASCII 4 MATERIAL_ORDER>
                <ASCII 16 COMMENT>
                <LIST 3 
                  <LIST 10 
                    <ASCII 4 USE_COUNT>
                    <ASCII 4 JUDGEMENT>
                    <ASCII 4 REASON_CODE>
                    <ASCII 2 INS_FLAG>
                    <ASCII 2 LIBRARYID>
                    <ASCII 2 PRERUN_FLAG>
                    <ASCII 2 TURN_DIR>
                    <ASCII 2 FLIP_STATE>
                    <ASCII 4 WORK_STATE>
                    <ASCII 16 MULTI_USE>
                  >
                  <LIST 2 
                    <ASCII 16 STAGE_STATE>
                    <ASCII 20 LOCATION>
                  >
                  <LIST 5 
                    <LIST 2 
                      <ASCII 40 OPTION_NAME_1>
                      <ASCII 40 OPTION_VALUE_1>
                    >
                    <LIST 2 
                      <ASCII 40 OPTION_NAME_2>
                      <ASCII 40 OPTION_VALUE_2>
                    >
                    <LIST 2 
                      <ASCII 40 OPTION_NAME_3>
                      <ASCII 40 OPTION_VALUE_3>
                    >
                    <LIST 2 
                      <ASCII 40 OPTION_NAME_4>
                      <ASCII 40 OPTION_VALUE_4>
                    >
                    <LIST 2 
                      <ASCII 40 OPTION_NAME_5>
                      <ASCII 40 OPTION_VALUE_5>
                    >
                  >
                >
              >
            >
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - GlassStockInfo
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 3 
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 ENC_FLAG>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 PAIR_GLASSID>
                <ASCII 20 PAIR_PPID>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - ProcessStepInquiry
  <LIST 2 
    <UINT1 1 SFCD>
    <LIST 7 
      <ASCII 28 MODULE_ID>
      <UINT1 1 MODULE_STATE>
      <UINT1 1 PROC_STATE>
      <UINT1 1 CUR_STEPNO>
      <LIST n 
        <LIST 2 
          <UINT1 1 STEPNO>
          <ASCII 40 STEP_DESC>
        >
      >
      <LIST 2 
        <LIST n 
          <ASCII 16 H_GLASSID>
        >
        <LIST n 
          <ASCII 16 MATERIAL_ID>
        >
      >
      <LIST n 
        <LIST 7 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 CUR_STEPNO>
          <LIST n 
            <LIST 2 
              <UINT1 1 STEPNO>
              <ASCII 40 STEP_DESC>
            >
          >
          <LIST 2 
            <LIST n 
              <ASCII 16 H_GLASSID>
            >
            <LIST n 
              <ASCII 16 MATERIAL_ID>
            >
          >
          <LIST n 
            <LIST 7 
              <ASCII 28 MODULE_ID>
              <UINT1 1 MODULE_STATE>
              <UINT1 1 PROC_STATE>
              <UINT1 1 CUR_STEPNO>
              <LIST n 
                <LIST 2 
                  <UINT1 1 STEPNO>
                  <ASCII 40 STEP_DESC>
                >
              >
              <LIST 2 
                <LIST n 
                  <ASCII 16 H_GLASSID>
                >
                <LIST n 
                  <ASCII 16 MATERIAL_ID>
                >
              >
              <LIST n 
                <LIST 7 
                  <ASCII 28 MODULE_ID>
                  <UINT1 1 MODULE_STATE>
                  <UINT1 1 PROC_STATE>
                  <UINT1 1 CUR_STEPNO>
                  <LIST n 
                    <LIST 2 
                      <UINT1 1 STEPNO>
                      <ASCII 40 STEP_DESC>
                    >
                  >
                  <LIST 2 
                    <LIST n 
                      <ASCII 16 H_GLASSID>
                    >
                    <LIST n 
                      <ASCII 16 MATERIAL_ID>
                    >
                  >
                  <LIST n 
                    <LIST 6 
                      <ASCII 28 MODULE_ID>
                      <UINT1 1 MODULE_STATE>
                      <UINT1 1 PROC_STATE>
                      <UINT1 1 CUR_STEPNO>
                      <LIST n 
                        <LIST 2 
                          <UINT1 1 STEPNO>
                          <ASCII 40 STEP_DESC>
                        >
                      >
                      <LIST 2 
                        <LIST n 
                          <ASCII 16 H_GLASSID>
                        >
                        <LIST n 
                          <ASCII 16 MATERIAL_ID>
                        >
                      >
                    >
                  >
                >
              >
            >
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - InEQPathDefinitionInquiry
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 2 
        <ASCII 10 PATHNAME>
        <LIST n 
          <LIST 2 
            <ASCII 28 PATH_MODULE_ID>
            <ASCII 2 PATH_ORDER>
          >
        >
      >
    >
  >
>

<S1F6 S FromatStateData - CurrentRecipeInquiry
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 3 
        <ASCII 28 SUB_MODULE_ID>
        <ASCII 20 CUR_RECIPE_TYPE2>
        <ASCII 20 CUR_RECIPE_TYPE1>
      >
    >
  >
>

<S1F6 S FromatStateData - InquireRecipeRelationinEQs
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST n 
      <LIST 2 
        <ASCII 40 RECIPEID>
        <ASCII 20 PPID>
      >
    >
  >
>

<S1F6 S FromatStateData - InquireLibraryState
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST 2 
      <ASCII 4 TRANS_POSSIBLE_STATE>
      <LIST n 
        <LIST 6 
          <ASCII 2 LIBRARYID>
          <ASCII 16 STAGE_STATE>
          <ASCII 20 LOCATION>
          <ASCII 16 MATERIAL_ID>
          <UINT2 1 MATERIAL_STATE>
          <ASCII 2 INS_FLAG>
        >
      >
    >
  >
>

<S1F6 S FromatStateData - InquireProductionPlanInfo
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST 4 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 EQP_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 5 
            <ASCII 4 PORTID>
            <UINT1 1 PORT_STATE>
            <UINT1 1 PORT_TYPE>
            <ASCII 2 PORT_MODE>
            <UINT1 1 SORT_TYPE>
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 22 
          <ASCII 32 PLAN_ID>
          <UINT2 1 PLAN_ORDER>
          <ASCII 8 PLAN_STATE>
          <UINT1 1 PRIORITY>
          <ASCII 4 PRODUCT_TYPE>
          <ASCII 4 PRODUCT_KIND>
          <ASCII 16 PRODUCTID>
          <ASCII 16 RUNSPECID>
          <ASCII 8 LAYERID>
          <ASCII 8 STEPID>
          <ASCII 20 PPID>
          <UINT4 1 PLAN_SIZE>
          <ASCII 18 MAKER>
          <ASCII 6 THICKNESS>
          <ASCII 2 INPUT_LINE>
          <ASCII 16 COMMENT>
          <UINT4 1 TOTAL_QTY>
          <UINT4 1 USED_QTY>
          <UINT4 1 INEQP_QTY>
          <UINT4 1 REMAIN_QTY>
          <UINT4 1 NG_QTY>
          <UINT4 1 ASSEMBLE_QTY>
        >
      >
    >
  >
>

<S1F6 S FromatStateData - InquireMaterialInfo
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 SFCD>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 1 
            <LIST 21 
              <ASCII 32 M_TRAYID>
              <ASCII 32 M_BATCHID>
              <UINT1 1 M_KIND>
              <ASCII 16 M_TYPE>
              <ASCII 16 M_MAKER>
              <ASCII 16 M_CODE>
              <ASCII 8 M_REVNO>
              <ASCII 8 TRAY_STATE>
              <UINT4 1 TOTAL_QTY>
              <UINT4 1 USED_QTY>
              <UINT4 1 INEQP_QTY>
              <UINT4 1 REMAIN_QTY>
              <UINT4 1 NG_QTY>
              <UINT4 1 ASSEMBLE_QTY>
              <ASCII 4 PORTID>
              <ASCII 4 PRODUCT_TYPE>
              <ASCII 4 PRODUCT_KIND>
              <ASCII 16 PRODUCTID>
              <ASCII 16 M_STEP>
              <ASCII 16 COMMENT>
              <LIST n 
                <LIST 6 
                  <ASCII 32 M_ID>
                  <ASCII 16 M_LOC>
                  <ASCII 16 M_SUBLOC>
                  <ASCII 8 M_SLOT>
                  <ASCII 4 D_CODE>
                  <ASCII 16 TAG>
                >
              >
            >
          >
        >
      >
    >
  >
>

<S1F7 P ReqOnlineModeChange
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <UINT1 1 MCMD>
  >
>

<S1F8 S OnlineAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 MCMD>
    <UINT1 1 ONLACK>
  >
>

<S2F15 P NewEQConstantSend
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 7 
        <UINT2 1 ECID>
        <ASCII 40 ECNAME>
        <ASCII 40 ECDEF>
        <ASCII 40 ECSLL>
        <ASCII 40 ECSUL>
        <ASCII 40 ECWLL>
        <ASCII 40 ECWUL>
      >
    >
  >
>

<S2F16 S NewEQConstantAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 MIACK>
    <LIST n 
      <LIST 8 
        <UINT1 1 TEAC>
        <LIST 2 
          <UINT2 1 ECID>
          <UINT1 1 EAC>
        >
        <LIST 2 
          <ASCII 40 ECNAME>
          <UINT1 1 EAC>
        >
        <LIST 2 
          <ASCII 40 ECDEF>
          <UINT1 1 EAC>
        >
        <LIST 2 
          <ASCII 40 ECSLL>
          <UINT1 1 EAC>
        >
        <LIST 2 
          <ASCII 40 ECSUL>
          <UINT1 1 EAC>
        >
        <LIST 2 
          <ASCII 40 ECWLL>
          <UINT1 1 EAC>
        >
        <LIST 2 
          <ASCII 40 ECWUL>
          <UINT1 1 EAC>
        >
      >
    >
  >
>

<S2F23 P TraceInitializeSend
  <LIST 6 
    <ASCII 28 MODULE_ID>
    <UINT2 1 TRID>
    <ASCII 6 SMPTIME>
    <UINT2 1 TOTSMP>
    <UINT2 1 GRSIZE>
    <LIST 1 
      <UINT2 1 SVID>
    >
  >
>

<S2F24 S TraceInitializeAck
  <UINT1 1 TIACK>
>

<S2F29 P EQConstantNamelistReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <UINT2 1 ECID>
    >
  >
>

<S2F30 S EQConstantNamelist
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 7 
        <UINT2 1 ECID>
        <ASCII 40 ECNAME>
        <ASCII 40 ECDEF>
        <ASCII 40 ECSLL>
        <ASCII 40 ECSUL>
        <ASCII 40 ECWLL>
        <ASCII 40 ECWUL>
      >
    >
  >
>

<S2F31 P DateTimeSetReq
  <ASCII 14 TIME>
>

<S2F32 S DateTimeSetAck
  <UINT1 1 ACKC2>
>

<S2F41 P HostCommandSend - JobProcessCmd
  <LIST 2 
    <UINT2 1 >
    <LIST 7 
      <LIST 2 
        <ASCII 10 JOBID_NAME>
        <ASCII 16 JOBID>
      >
      <LIST 2 
        <ASCII 10 IPID_NAME>
        <ASCII 4 IPID>
      >
      <LIST 2 
        <ASCII 10 ICID_NAME>
        <ASCII 12 ICID>
      >
      <LIST 2 
        <ASCII 10 OPID_NAME>
        <ASCII 4 OPID>
      >
      <LIST 2 
        <ASCII 10 OCID_NAME>
        <ASCII 12 OCID>
      >
      <LIST 2 
        <ASCII 10 SLOTINFO_NAME>
        <ASCII 56 SLOTINFO>
      >
      <LIST 2 
        <ASCII 10 ORDER_NAME>
        <LIST n 
          <UINT1 1 SLOTNO>
        >
      >
    >
  >
>

<S2F41 P HostCommandSend - PortCmd
  <LIST 2 
    <UINT2 1 RCMD>
    <LIST n 
      <LIST 2 
        <ASCII 10 PORTID_NAME>
        <ASCII 4 PORTID>
      >
    >
  >
>

<S2F41 P HostCommandSend - EQCmd
  <LIST 2 
    <UINT2 1 RCMD>
    <LIST n 
      <LIST 2 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
      >
    >
  >
>

<S2F41 P HostCommandSend - SortCmd
  <LIST 2 
    <UINT2 1 RCMD>
    <LIST 2 
      <ASCII 20 JOBID>
      <LIST n 
        <LIST 6 
          <LIST 2 
            <ASCII 10 SPID_NAME>
            <ASCII 4 SPID>
          >
          <LIST 2 
            <ASCII 10 SCID_NAME>
            <ASCII 12 SCID>
          >
          <LIST 2 
            <ASCII 10 TPID_NAME>
            <ASCII 4 TPID>
          >
          <LIST 2 
            <ASCII 10 TCID_NAME>
            <ASCII 12 TCID>
          >
          <LIST 2 
            <ASCII 10 VCRFLAG_NAME>
            <UINT1 1 VCRFLAG>
          >
          <LIST n 
            <LIST 3 
              <ASCII 16 H_GLASSID>
              <ASCII 4 SSLOTID>
              <ASCII 4 TSLOTID>
            >
          >
        >
      >
    >
  >
>

<S2F41 P HostCommandSend - GlassProcessCmd
  <LIST 2 
    <UINT2 1 RCMD>
    <LIST 3 
      <LIST 2 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
      >
      <LIST 2 
        <ASCII 10 H_GLASSLID_NAME>
        <ASCII 16 H_GLASSLID>
      >
      <LIST 2 
        <ASCII 10 SLOTNO_NAME>
        <LIST n 
          <UINT1 1 SLOTNO>
        >
      >
    >
  >
>

<S2F41 P HostCommandSend - MaterialProcessCmd
  <LIST 2 
    <UINT2 1 RCMD>
    <LIST 3 
      <LIST 2 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
      >
      <LIST 2 
        <ASCII 10 MATERIALID_NAME>
        <ASCII 16 MATERIALID>
      >
      <LIST 2 
        <ASCII 10 SLOTNO_NAME>
        <LIST n 
          <UINT1 1 SLOTNO>
        >
      >
    >
  >
>

<S2F41 P HostCommandSend - MaskStockChamberControlCmd
  <LIST 2 
    <UINT2 1 RCMD>
    <LIST 4 
      <LIST 2 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
      >
      <LIST 2 
        <ASCII 10 CSTID_NAME>
        <ASCII 12 CSTID>
      >
      <LIST 2 
        <ASCII 10 CUR_POS_NAME>
        <ASCII 10 CUR_POS>
      >
      <LIST 2 
        <ASCII 10 ACTION_NAME>
        <ASCII 10 ACTION>
      >
    >
  >
>

<S2F41 P HostCommandSend - InEQPathChangeCmd
  <LIST 2 
    <UINT2 1 RCMD>
    <LIST n 
      <LIST 2 
        <ASCII 10 PATHNAME>
        <LIST n 
          <LIST 2 
            <ASCII 28 PATH_MODULE_ID>
            <ASCII 2 PATH_ORDER>
          >
        >
      >
    >
  >
>

<S2F42 S HostCommandAck - ProcessCmd
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST 7 
      <LIST 3 
        <ASCII 10 JOBID_NAME>
        <ASCII 16 JOBID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 IPID_NAME>
        <ASCII 4 IPID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 OCID_NAME>
        <ASCII 12 OCID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 OPID_NAME>
        <ASCII 4 OPID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 OCID_NAME>
        <ASCII 12 OCID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 SLOTINFO_NAME>
        <ASCII 56 SLOTINFO>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 ORDER_NAME>
        <LIST n 
          <UINT1 1 SLOTNO>
        >
        <UINT1 1 CPACK>
      >
    >
  >
>

<S2F42 S HostCommandAck - PortCmd
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST n 
      <LIST 3 
        <ASCII 10 PORTID_NAME>
        <ASCII 4 PORTID>
        <UINT1 1 CPACK>
      >
    >
  >
>

<S2F42 S HostCommandAck - EQCmd
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST n 
      <LIST 3 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
        <UINT1 1 CPACK>
      >
    >
  >
>

<S2F42 S HostCommandAck - SortCmd
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST 2 
      <ASCII 16 JOBID>
      <LIST n 
        <LIST 6 
          <LIST 3 
            <ASCII 10 SPID_NAME>
            <ASCII 4 SPID>
            <UINT1 1 CPACK>
          >
          <LIST 3 
            <ASCII 10 SCID_NAME>
            <ASCII 12 SCID>
            <UINT1 1 CPACK>
          >
          <LIST 3 
            <ASCII 10 TPID_NAME>
            <ASCII 4 TPID>
            <UINT1 1 CPACK>
          >
          <LIST 3 
            <ASCII 10 TCID_NAME>
            <ASCII 12 TCID>
            <UINT1 1 CPACK>
          >
          <LIST 3 
            <ASCII 10 VCRFLAG_NAME>
            <UINT1 1 VCRFLAG>
            <UINT1 1 CPACK>
          >
          <LIST 4 
            <ASCII 16 H_GLASSID>
            <ASCII 4 SSLOTID>
            <ASCII 4 TSLOTID>
            <UINT1 1 CPACK>
          >
        >
      >
    >
  >
>

<S2F42 S HostCommandAck - GlassCmd
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST 3 
      <LIST 3 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 H_GLASSLID_NAME>
        <ASCII 16 H_GLASSLID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 SLOT_NO_NAME>
        <LIST n 
          <UINT1 1 SLOT_NO>
        >
        <UINT1 1 CPACK>
      >
    >
  >
>

<S2F42 S HostCommandAck - MaterialProcessCmd
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST 3 
      <LIST 3 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 MATERIALID_NAME>
        <ASCII 16 MATERIALID>
        <UINT1 1 CPACK>
      >
      <LIST 3 
        <ASCII 10 SLOT_NO_NAME>
        <LIST n 
          <UINT1 1 SLOT_NO>
        >
        <UINT1 1 CPACK>
      >
    >
  >
>

<S2F42 S HostCommandAck - MaskStockChamberControlCmd
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST 4 
      <LIST 2 
        <ASCII 10 MODULE_ID_NAME>
        <ASCII 28 MODULE_ID>
      >
      <LIST 2 
        <ASCII 10 CSTID_NAME>
        <ASCII 12 CSTID>
      >
      <LIST 2 
        <ASCII 10 CUR_POS_NAME>
        <ASCII 10 CUR_POS>
      >
      <LIST 2 
        <ASCII 10 ACTION_NAME>
        <ASCII 10 ACTION>
      >
    >
  >
>

<S2F42 S HostCommandAck - InEQPathChangeCommand
  <LIST 3 
    <UINT2 1 RCMD>
    <UINT1 1 HCACK>
    <LIST n 
      <LIST 2 
        <ASCII 10 PATHNAME>
        <LIST n 
          <LIST 2 
            <ASCII 28 PATH_MODULE_ID>
            <ASCII 2 PATH_ORDER>
          >
        >
      >
    >
  >
>

<S2F101 P OperatorCall
  <LIST 3 
    <UINT1 1 >
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 100 MSG>
    >
  >
>

<S2F102 S OperatorCallAck
  <UINT1 1 ACKC2>
>

<S2F103 P EQOnlineParamChange
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 2 
        <UINT1 1 EOID>
        <LIST n 
          <LIST 2 
            <UINT1 1 EOMD>
            <UINT1 1 EOV>
          >
        >
      >
    >
  >
>

<S2F104 S EQOnlineParamAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 MIACK>
    <LIST n 
      <LIST 2 
        <UINT1 1 EOID>
        <LIST n 
          <LIST 2 
            <UINT1 1 EOMD>
            <UINT1 1 EAC>
          >
        >
      >
    >
  >
>

<S3F1 P MaterialStateReq
  <ASCII 28 MODULE_ID>
>

<S3F2 S MaterialStateData
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 8 
        <ASCII 16 MATERIAL_ID>
        <ASCII 4 PRODUCT_TYPE>
        <ASCII 2 LIBRARYID>
        <ASCII 16 STAGE_STATE>
        <UINT2 1 MATERIAL_STATE>
        <ASCII 20 LOCATION>
        <UINT2 2 MATERIAL_SIZE>
        <LIST n 
          <LIST 3 
            <ASCII 16 PRODUCTID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
          >
        >
      >
    >
  >
>

<S3F101 P CassetteInfo
  <LIST 3 
    <UINT1 1 HOT_DEVICE>
    <UINT1 1 HOT_LEVEL>
    <LIST 3 
      <LIST 5 
        <ASCII 4 PORTID>
        <UINT1 1 PORT_STATE>
        <UINT1 1 PORT_TYPE>
        <ASCII 2 PORT_MODE>
        <UINT1 1 SORT_TYPE>
      >
      <LIST 5 
        <ASCII 12 CSTID>
        <UINT1 1 CST_TYPE>
        <ASCII 56 MAP_STIF>
        <ASCII 56 CUR_STIF>
        <UINT1 1 BATCH_ORDER>
      >
      <LIST n 
        <LIST 21 
          <ASCII 16 H_GLASSID>
          <ASCII 16 E_GLASSID>
          <ASCII 16 LOTID>
          <ASCII 16 BATCHID>
          <ASCII 16 JOBID>
          <ASCII 4 PORTID>
          <ASCII 2 SLOTNO>
          <ASCII 4 PRODUCT_TYPE>
          <ASCII 4 PORDUCT_KIND>
          <ASCII 16 PORDUCTID>
          <ASCII 16 RUNSPECID>
          <ASCII 8 LAYERID>
          <ASCII 8 STEPID>
          <ASCII 20 PPID>
          <ASCII 20 FLOWID>
          <UINT2 2 GLASS_SIZE>
          <UINT2 1 GLASS_THICKNESS>
          <UINT2 1 GLASS_STATE>
          <ASCII 4 GLASS_ORDER>
          <ASCII 16 COMMENT>
          <LIST 3 
            <LIST 10 
              <ASCII 4 USE_COUNT>
              <ASCII 4 JUDGEMENT>
              <ASCII 4 REASON_CODE>
              <ASCII 2 INS_FLAG>
              <ASCII 2 ENC_FLAG>
              <ASCII 2 PRERUN_FLAG>
              <ASCII 2 TURN_DIR>
              <ASCII 2 FLIP_STATE>
              <ASCII 4 WORK_STATE>
              <ASCII 16 MULTI_USE>
            >
            <LIST 2 
              <ASCII 16 PAIR_GLASSID>
              <ASCII 20 PAIR_PPID>
            >
            <LIST 5 
              <LIST 2 
                <ASCII 40 OPTION_NAME_1>
                <ASCII 40 OPTION_VALUE_1>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_2>
                <ASCII 40 OPTION_VALUE_2>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_3>
                <ASCII 40 OPTION_VALUE_3>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_4>
                <ASCII 40 OPTION_VALUE_4>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_5>
                <ASCII 40 OPTION_VALUE_5>
              >
            >
          >
        >
      >
    >
  >
>

<S3F102 S CassetteInfoAck
  <UINT1 1 ACKC3>
>

<S3F103 P ProductionPlanInfoSend
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT2 1 PLCD>
    <LIST n 
      <LIST 22 
        <ASCII 32 PLAN_ID>
        <UINT2 1 PLAN_ORDER>
        <ASCII 8 PLAN_STATE>
        <UINT1 1 PRIORITY>
        <ASCII 4 PRODUCT_TYPE>
        <ASCII 4 PRODUCT_KIND>
        <ASCII 16 PRODUCTID>
        <ASCII 16 RUNSPECID>
        <ASCII 8 LAYERID>
        <ASCII 8 STEPID>
        <ASCII 20 PPID>
        <UINT4 1 PLAN_SIZE>
        <ASCII 18 MAKER>
        <ASCII 6 THICKNESS>
        <ASCII 2 INPUT_LINE>
        <ASCII 16 COMMENT>
        <UINT4 1 TOTAL_QTY>
        <UINT4 1 USED_QTY>
        <UINT4 1 INEQP_QTY>
        <UINT4 1 REMAIN_QTY>
        <UINT4 1 NG_QTY>
        <UINT4 1 ASSEMBLE_QTY>
      >
    >
  >
>

<S3F104 S ProductionPlanInfoAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT2 1 PLCD>
    <UINT1 1 ACKC3>
  >
>

<S3F105 P ModulesMaterialInfoSend
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 1 
        <LIST 21 
          <ASCII 32 M_TRAYID>
          <ASCII 32 M_BATCHID>
          <UINT1 1 M_KIND>
          <ASCII 16 M_TYPE>
          <ASCII 16 M_MAKER>
          <ASCII 16 M_CODE>
          <ASCII 8 M_REVNO>
          <ASCII 8 TRAY_STATE>
          <UINT4 1 TOTAL_QTY>
          <UINT4 1 USED_QTY>
          <UINT4 1 INEQP_QTY>
          <UINT4 1 REMAIN_QTY>
          <UINT4 1 NG_QTY>
          <UINT4 1 ASSEMBLE_QTY>
          <ASCII 4 PORTID>
          <ASCII 4 PRODUCT_TYPE>
          <ASCII 4 PRODUCT_KIND>
          <ASCII 16 PRODUCTID>
          <ASCII 16 M_STEP>
          <ASCII 16 COMMENT>
          <LIST n 
            <LIST 6 
              <ASCII 32 M_ID>
              <ASCII 16 M_LOC>
              <ASCII 16 M_SUBLOC>
              <ASCII 8 M_SLOT>
              <ASCII 4 D_CODE>
              <ASCII 16 TAG>
            >
          >
        >
      >
    >
  >
>

<S3F106 S ModulesMaterialInfoAck
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <UINT1 1 ACKC3>
  >
>

<S3F111 P ModulesMaterialCodeInfoSend
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST 3 
      <LIST 4 
        <ASCII 16 BATCHID>
        <ASCII 16 PRODUCTID>
        <ASCII 8 STEPID>
        <ASCII 20 PPID>
      >
      <LIST n 
        <ASCII 16 M_CODE>
      >
      <LIST 2 
        <ASCII 16 M_TYPE>
        <ASCII 16 M_STEP>
      >
    >
  >
>

<S3F112 S ModulesMaterialCodeInfoAck
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <UINT1 1 ACKC3>
  >
>

<S3F115 P MaterialInfo
  <LIST 3 
    <UINT1 1 HOT_DEVICE>
    <UINT1 1 HOT_LEVEL>
    <LIST 3 
      <LIST 5 
        <ASCII 4 PORTID>
        <UINT1 1 PORT_STATE>
        <UINT1 1 PORT_TYPE>
        <ASCII 2 PORT_MODE>
        <UINT1 1 SORT_TYPE>
      >
      <LIST 5 
        <ASCII 12 CSTID>
        <UINT1 1 CST_TYPE>
        <ASCII 56 MAP_STIF>
        <ASCII 56 CUR_STIF>
        <UINT1 1 BATCH_ORDER>
      >
      <LIST n 
        <LIST 22 
          <ASCII 16 MATERIAL_ID>
          <ASCII 16 MATERIAL_SETID>
          <ASCII 16 LOTID>
          <ASCII 16 BATCHID>
          <ASCII 16 JOBID>
          <ASCII 4 PORTID>
          <ASCII 2 SLOTNO>
          <ASCII 4 PRODUCT_TYPE>
          <ASCII 4 PRODUCT_KIND>
          <ASCII 16 PRODUCTID>
          <ASCII 16 RUNSPECID>
          <ASCII 8 LAYERID>
          <ASCII 8 STEPID>
          <ASCII 20 PPID>
          <ASCII 20 FLOWID>
          <UINT2 2 MATERIAL_SIZE>
          <UINT2 1 MATERIAL_THICKNESS>
          <UINT2 1 MATERIAL_STATE>
          <ASCII 4 MATERIAL_ORDER>
          <ASCII 16 COMMENT>
          <LIST 3 
            <LIST 10 
              <ASCII 4 USE_COUNT>
              <ASCII 4 JUDGEMENT>
              <ASCII 4 REASON_CODE>
              <ASCII 2 INS_FLAG>
              <ASCII 2 LIBRARYID>
              <ASCII 2 PRERUN_FLAG>
              <ASCII 2 TURN_DIR>
              <ASCII 2 FLIP_STATE>
              <ASCII 4 WORK_STATE>
              <ASCII 16 MULTI_USE>
            >
            <LIST 2 
              <ASCII 16 STAGE_STATE>
              <ASCII 20 LOCATION>
            >
            <LIST 5 
              <LIST 2 
                <ASCII 40 OPTION_NAME_1>
                <ASCII 40 OPTION_VALUE_1>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_2>
                <ASCII 40 OPTION_VALUE_2>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_3>
                <ASCII 40 OPTION_VALUE_3>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_4>
                <ASCII 40 OPTION_VALUE_4>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_5>
                <ASCII 40 OPTION_VALUE_5>
              >
            >
          >
          <LIST n 
            <LIST 15 
              <ASCII 30 SUB_MATERIALID>
              <ASCII 8 SUB_MATERIAL_KIND>
              <ASCII 8 SUB_MATERIAL_TYPE>
              <ASCII 4 SUB_MATERIAL_MODEL>
              <ASCII 10 SUB_MATERIAL_MAKER>
              <ASCII 10 SUB_MATERIAL_MATTER>
              <UINT2 1 SUB_MATERIAL_THICKNESS>
              <UINT2 1 SUB_MATERIAL_STATE>
              <ASCII 4 SUB_POSITION>
              <ASCII 4 SUB_LAYER>
              <ASCII 14 SUB_DATE_FABIN>
              <ASCII 14 SUB_DATE_DISCARD>
              <ASCII 14 SUB_DATE_ETCHING>
              <ASCII 14 SUB_DATE_SHIPPING>
              <ASCII 4 SUB_JUDGEMENT>
            >
          >
        >
      >
    >
  >
>

<S3F116 S MaterialAck
  <UINT1 1 ACKC3>
>

<S3F117 P ModuleLabelInfoSend
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 2 
        <ASCII 16 PRODUCTID>
        <LIST n 
          <LIST 2 
            <ASCII 40 ATTRID>
            <ASCII 40 ATTRDATA>
          >
        >
      >
    >
  >
>

<S3F118 S ModuleLabelInfoAck
  <UINT1 1 ACKC3>
>

<S3F119 P ModuleLabelSerialQuantityInfoSend
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST 1 
      <LIST 3 
        <ASCII 16 PRODUCTID>
        <LIST n 
          <ASCII 40 SERIAL>
        >
        <LIST n 
          <LIST 2 
            <ASCII 40 OPTION_NAME>
            <ASCII 40 OPTION_VALUE>
          >
        >
      >
    >
  >
>

<S3F120 S ModuleLabelSerialQuantityInfoAck
  <UINT1 1 ACKC3>
>

<S3F121 P GlassUnitInfo
  <LIST 3 
    <UINT1 1 HOT_DEVICE>
    <UINT1 1 HOT_LEVEL>
    <LIST 4 
      <ASCII 28 MODULE_ID>
      <LIST 5 
        <ASCII 4 PORTID>
        <UINT1 1 PORT_STATE>
        <UINT1 1 PORT_TYPE>
        <ASCII 2 PORT_MODE>
        <UINT1 1 SORT_TYPE>
      >
      <LIST 5 
        <ASCII 12 CSTID>
        <UINT1 1 CST_TYPE>
        <ASCII 56 MAP_STIF>
        <ASCII 56 CUR_STIF>
        <UINT1 1 BATCH_ORDER>
      >
      <LIST n 
        <LIST 21 
          <ASCII 16 H_GLASSID>
          <ASCII 16 E_GLASSID>
          <ASCII 16 LOTID>
          <ASCII 16 BATCHID>
          <ASCII 16 JOBID>
          <ASCII 4 PORTID>
          <ASCII 2 SLOTNO>
          <ASCII 4 PRODUCT_TYPE>
          <ASCII 4 PORDUCT_KIND>
          <ASCII 16 PORDUCTID>
          <ASCII 16 RUNSPECID>
          <ASCII 8 LAYERID>
          <ASCII 8 STEPID>
          <ASCII 20 PPID>
          <ASCII 20 FLOWID>
          <UINT2 2 GLASS_SIZE>
          <UINT2 1 GLASS_THICKNESS>
          <UINT2 1 GLASS_STATE>
          <ASCII 4 GLASS_ORDER>
          <ASCII 16 COMMENT>
          <LIST 3 
            <LIST 10 
              <ASCII 4 USE_COUNT>
              <ASCII 4 JUDGEMENT>
              <ASCII 4 REASON_CODE>
              <ASCII 2 INS_FLAG>
              <ASCII 2 ENC_FLAG>
              <ASCII 2 PRERUN_FLAG>
              <ASCII 2 TURN_DIR>
              <ASCII 2 FLIP_STATE>
              <ASCII 4 WORK_STATE>
              <ASCII 16 MULTI_USE>
            >
            <LIST 2 
              <ASCII 16 PAIR_GLASSID>
              <ASCII 20 PAIR_PPID>
            >
            <LIST 5 
              <LIST 2 
                <ASCII 40 OPTION_NAME_1>
                <ASCII 40 OPTION_VALUE_1>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_2>
                <ASCII 40 OPTION_VALUE_2>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_3>
                <ASCII 40 OPTION_VALUE_3>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_4>
                <ASCII 40 OPTION_VALUE_4>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_5>
                <ASCII 40 OPTION_VALUE_5>
              >
            >
          >
        >
      >
    >
  >
>

<S3F122 S GlassUnitInfoAck
  <UINT1 1 ACKC3>
>

<S3F125 P MaterialUnitInfo
  <LIST 3 
    <UINT1 1 HOT_DEVICE>
    <UINT1 1 HOT_LEVEL>
    <LIST 4 
      <ASCII 28 MODULE_ID>
      <LIST 5 
        <ASCII 4 PORTID>
        <UINT1 1 PORT_STATE>
        <UINT1 1 PORT_TYPE>
        <ASCII 2 PORT_MODE>
        <UINT1 1 SORT_TYPE>
      >
      <LIST 5 
        <ASCII 12 CSTID>
        <UINT1 1 CST_TYPE>
        <ASCII 56 MAP_STIF>
        <ASCII 56 CUR_STIF>
        <UINT1 1 BATCH_ORDER>
      >
      <LIST n 
        <LIST 22 
          <ASCII 16 MATERIAL_ID>
          <ASCII 16 MATERIAL_SETID>
          <ASCII 16 LOTID>
          <ASCII 16 BATCHID>
          <ASCII 16 JOBID>
          <ASCII 4 PORTID>
          <ASCII 2 SLOTNO>
          <ASCII 4 PRODUCT_TYPE>
          <ASCII 4 PRODUCT_KIND>
          <ASCII 16 PRODUCTID>
          <ASCII 16 RUNSPECID>
          <ASCII 8 LAYERID>
          <ASCII 8 STEPID>
          <ASCII 20 PPID>
          <ASCII 20 FLOWID>
          <UINT2 2 MATERIAL_SIZE>
          <UINT2 1 MATERIAL_THICKNESS>
          <UINT2 1 MATERIAL_STATE>
          <ASCII 4 MATERIAL_ORDER>
          <ASCII 16 COMMENT>
          <LIST 3 
            <LIST 10 
              <ASCII 4 USE_COUNT>
              <ASCII 4 JUDGEMENT>
              <ASCII 4 REASON_CODE>
              <ASCII 2 INS_FLAG>
              <ASCII 2 LIBRARYID>
              <ASCII 2 PRERUN_FLAG>
              <ASCII 2 TURN_DIR>
              <ASCII 2 FLIP_STATE>
              <ASCII 4 WORK_STATE>
              <ASCII 16 MULTI_USE>
            >
            <LIST 2 
              <ASCII 16 STAGE_STATE>
              <ASCII 20 LOCATION>
            >
            <LIST 5 
              <LIST 2 
                <ASCII 40 OPTION_NAME_1>
                <ASCII 40 OPTION_VALUE_1>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_2>
                <ASCII 40 OPTION_VALUE_2>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_3>
                <ASCII 40 OPTION_VALUE_3>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_4>
                <ASCII 40 OPTION_VALUE_4>
              >
              <LIST 2 
                <ASCII 40 OPTION_NAME_5>
                <ASCII 40 OPTION_VALUE_5>
              >
            >
          >
          <LIST n 
            <LIST 15 
              <ASCII 30 SUB_MATERIALID>
              <ASCII 8 SUB_MATERIAL_KIND>
              <ASCII 8 SUB_MATERIAL_TYPE>
              <ASCII 4 SUB_MATERIAL_MODEL>
              <ASCII 10 SUB_MATERIAL_MAKER>
              <ASCII 10 SUB_MATERIAL_MATTER>
              <UINT2 1 SUB_MATERIAL_THICKNESS>
              <UINT2 1 SUB_MATERIAL_STATE>
              <ASCII 4 SUB_POSITION>
              <ASCII 4 SUB_LAYER>
              <ASCII 14 SUB_DATE_FABIN>
              <ASCII 14 SUB_DATE_DISCARD>
              <ASCII 14 SUB_DATE_ETCHING>
              <ASCII 14 SUB_DATE_SHIPPING>
              <ASCII 4 SUB_JUDGEMENT>
            >
          >
        >
      >
    >
  >
>

<S3F126 S MaterialUnitInfoAck
  <UINT1 1 ACKC3>
>

<S3F217 P LotTagInfoSend
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 15 
        <ASCII 20 LOT_ID>
        <ASCII 16 PRODUCTID>
        <ASCII 25 PROD_DES>
        <ASCII 5 T_C_QTY>
        <ASCII 10 LOT_GRADE>
        <ASCII 4 PRODUCT_TYPE>
        <ASCII 16 PRODUCT_PARTNO>
        <ASCII 10 TOSITE>
        <ASCII 40 SERIAL_NO>
        <ASCII 20 RESULT1>
        <ASCII 20 RESULT2>
        <ASCII 20 RESULT3>
        <ASCII 20 RESULT4>
        <LIST 1 
          <LIST 2 
            <ASCII 10 PROCESS_ORDER>
            <ASCII 20 PROCESS_NAME>
          >
        >
        <LIST n 
          <LIST 2 
            <ASCII 40 ITEM_NAME>
            <ASCII 40 ITEM_VALUE>
          >
        >
      >
    >
  >
>

<S3F218 S LotTagInfoAck
  <UINT1 1 ACKC3>
>

<S5F1 P AlarmReportSend
  <LIST 2 
    <UINT1 1 SETCODE>
    <LIST 4 
      <ASCII 28 MODULE_ID>
      <UINT1 1 ALCD>
      <UINT4 1 ALID>
      <ASCII 100 ALTX>
    >
  >
>

<S5F2 S AlarmReportAck
  <UINT1 1 ACKC5>
>

<S5F5 P AlarmListReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <UINT4 1 ALID>
    >
  >
>

<S5F6 S AlarmListAck
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 4 
        <ASCII 28 MODULE_ID>
        <UINT1 1 ALCD>
        <UINT4 1 ALID>
        <ASCII 100 ALTX>
      >
    >
  >
>

<S5F101 P WaitingResetAlarmsList
  <ASCII 28 MODULE_ID>
>

<S5F102 S WaitingResetAlarmsListAck
  <LIST n 
    <LIST 4 
      <ASCII 28 MODULE_ID>
      <UINT1 1 ALCD>
      <UINT4 1 ALID>
      <ASCII 100 ALTX>
    >
  >
>

<S6F1 N TraceDataSend
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST 3 
      <UINT2 1 TRID>
      <UINT2 1 SMPLN>
      <LIST n 
        <LIST 2 
          <ASCII 16 SCTIME>
          <LIST n 
            <LIST 3 
              <UINT2 1 SVID>
              <ASCII 40 SV>
              <ASCII 40 SVNAME>
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - JobProcessEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 4 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 ENC_FLAG>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 PAIR_GLASSID>
                <ASCII 20 PAIR_PPID>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - GlassProcessEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 ENC_FLAG>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 PAIR_GLASSID>
                <ASCII 20 PAIR_PPID>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - ProcessStatusEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 4 
        <ASCII 28 MODULE_ID>
        <UINT1 1 MCMD>
        <UINT1 1 MODULE_STATE>
        <UINT1 1 PROC_STATE>
      >
      <LIST 2 
        <UINT1 1 STEPNO>
        <UINT1 1 PREV_STEPNO>
      >
      <LIST n 
        <LIST 4 
          <ASCII 40 IDENTIFICATION>
          <UINT1 1 POSITION>
          <ASCII 1 PROCESS_ACT>
          <ASCII 20 PROCESSED_PROD>
        >
      >
    >
  >
>

<S6F11 P EventReportSend - JobPermissionEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 4 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <UINT1 1 EOID>
            <LIST n 
              <LIST 2 
                <UINT1 1 EOMD>
                <UINT1 1 EOV>
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - PortEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
      >
    >
  >
>

<S6F11 P EventReportSend - MaskStockStateChangeEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 1 
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 1 
        <LIST 7 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
          <UINT1 1 ENVIRONMENT>
          <UINT1 1 REUSE_MODE>
        >
      >
      <LIST n 
        <LIST 7 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
          <UINT1 1 POSITION>
          <LIST n 
            <LIST 2 
              <ASCII 16 MATERIAL_ID>
              <ASCII 2 SLOTNO>
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - GlassLoadEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 3 
            <ASCII 12 CSTID>
            <ASCII 16 H_GLASSID>
            <ASCII 2 SLOTNO>
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - MaterialLoadEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 3 
            <ASCII 12 MATERIAL_KIND>
            <ASCII 16 MATERIAL_ID>
            <ASCII 2 SLOTNO>
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - EQEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 2 
          <UINT1 1 OLD_STATE>
          <UINT1 1 NEW_STATE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 4 
          <ASCII 28 MODULE_ID>
          <UINT1 1 ALCD>
          <UINT4 1 ALID>
          <ASCII 100 ALTX>
        >
      >
    >
  >
>

<S6F11 P EventReportSend - MaskStockEQEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 1 
        <LIST 8 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
          <UINT1 1 ENVIRONMENT>
          <UINT1 1 REUSE_MODE>
        >
      >
      <LIST 1 
        <LIST 2 
          <UINT1 1 OLD_STATE>
          <UINT1 1 NEW_STATE>
        >
      >
      <LIST 1 
        <LIST 4 
          <ASCII 28 MODULE_ID>
          <UINT1 1 ALCD>
          <UINT4 1 ALID>
          <ASCII 100 ALTX>
        >
      >
    >
  >
>

<S6F11 P EventReportSend - EQParamEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <UINT1 1 EOID>
            <LIST n 
              <LIST 2 
                <UINT1 1 EOMD>
                <UINT1 1 EOV>
              >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 7 
            <UINT2 1 ECID>
            <ASCII 40 ECNAME>
            <ASCII 40 ECDEF>
            <ASCII 40 ECSLL>
            <ASCII 40 ECSUL>
            <ASCII 40 ECWLL>
            <ASCII 40 ECWUL>
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - SpecifiedControlEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 40 ITEM_NAME>
            <ASCII 40 ITEM_VALUE>
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - MaterialEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <ASCII 28 MODULE_ID>
        <LIST n 
          <LIST 8 
            <ASCII 16 MATERIAL_ID>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 2 LIBRARYID>
            <ASCII 16 STAGE_STATE>
            <UINT2 1 MATERIAL_STATE>
            <ASCII 20 LOCATION>
            <UINT2 2 MATERIAL_SIZE>
            <LIST 1 
              <LIST 3 
                <ASCII 16 PRODUCTID>
                <ASCII 8 STEPID>
                <ASCII 20 PPID>
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - MaterialJobProcessEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 4 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 22 
            <ASCII 16 MATERIAL_ID>
            <ASCII 16 MATERIAL_SETID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PRODUCT_KIND>
            <ASCII 16 PRODUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 MATERIAL_SIZE>
            <UINT2 1 MATERIAL_THICKNESS>
            <UINT2 1 MATERIAL_STATE>
            <ASCII 4 MATERIAL_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 14 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 LIBRARYID>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
                <UINT2 1 C_QTY>
                <UINT2 1 O_QTY>
                <UINT2 1 R_QTY>
                <UINT2 1 N_QTY>
              >
              <LIST 2 
                <ASCII 16 STAGE_STATE>
                <ASCII 20 LOCATION>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
            <LIST n 
              <LIST 19 
                <ASCII 30 SUB_MATERIALID>
                <ASCII 8 SUB_MATERIAL_KIND>
                <ASCII 8 SUB_MATERIAL_TYPE>
                <ASCII 4 SUB_MATERIAL_MODEL>
                <ASCII 10 SUB_MATERIAL_MAKER>
                <ASCII 10 SUB_MATERIAL_MATTER>
                <UINT2 1 SUB_MATERIAL_THICKNESS>
                <UINT2 1 SUB_MATERIAL_STATE>
                <ASCII 4 SUB_POSITION>
                <ASCII 4 SUB_LAYER>
                <ASCII 14 SUB_DATE_FABIN>
                <ASCII 14 SUB_DATE_DISCARD>
                <ASCII 14 SUB_DATE_ETCHING>
                <ASCII 14 SUB_DATE_SHIPPING>
                <ASCII 4 SUB_JUDGEMENT>
                <UINT2 1 C_QTY>
                <UINT2 1 O_QTY>
                <UINT2 1 R_QTY>
                <UINT2 1 N_QTY>
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - MaterialProcessEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 22 
            <ASCII 16 MATERIAL_ID>
            <ASCII 16 MATERIAL_SETID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PRODUCT_KIND>
            <ASCII 16 PRODUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 MATERIAL_SIZE>
            <UINT2 1 MATERIAL_THICKNESS>
            <UINT2 1 MATERIAL_STATE>
            <ASCII 4 MATERIAL_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 LIBRARYID>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 STAGE_STATE>
                <ASCII 20 LOCATION>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
            <LIST n 
              <LIST 15 
                <ASCII 30 SUB_MATERIALID>
                <ASCII 8 SUB_MATERIAL_KIND>
                <ASCII 8 SUB_MATERIAL_TYPE>
                <ASCII 4 SUB_MATERIAL_MODEL>
                <ASCII 10 SUB_MATERIAL_MAKER>
                <ASCII 10 SUB_MATERIAL_MATTER>
                <UINT2 1 SUB_MATERIAL_THICKNESS>
                <UINT2 1 SUB_MATERIAL_STATE>
                <ASCII 4 SUB_POSITION>
                <ASCII 4 SUB_LAYER>
                <ASCII 14 SUB_DATE_FABIN>
                <ASCII 14 SUB_DATE_DISCARD>
                <ASCII 14 SUB_DATE_ETCHING>
                <ASCII 14 SUB_DATE_SHIPPING>
                <ASCII 4 SUB_JUDGEMENT>
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - MaterialStageEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 2 
          <UINT1 1 OLD_STATE>
          <UINT1 1 NEW_STATE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 2 
          <ASCII 4 TRANS_POSSIBLE_STATE>
          <LIST 1 
            <LIST 6 
              <ASCII 2 LIBRARYID>
              <ASCII 16 STAGE_STATE>
              <ASCII 20 LOCATION>
              <ASCII 16 MATERIAL_ID>
              <UINT2 1 MATERIAL_STATE>
              <ASCII 2 INS_FLAG>
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - ReadingModuleEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 4 
        <ASCII 6 READING_RATE>
        <ASCII 6 TOTAL_COUNT>
        <ASCII 6 SUCCESS_COUNT>
        <LIST n 
          <LIST 3 
            <ASCII 16 READING_TIME>
            <ASCII 40 IDENTIFICATION>
            <ASCII 3 TRY_COUNT>
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - ModuleCarrierProcessEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 4 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 4 PORTID>
          <UINT1 1 PORT_STATE>
          <UINT1 1 PORT_TYPE>
          <ASCII 2 PORT_MODE>
          <UINT1 1 SORT_TYPE>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 5 
          <ASCII 12 CSTID>
          <UINT1 1 CST_TYPE>
          <ASCII 56 MAP_STIF>
          <ASCII 56 CUR_STIF>
          <UINT1 1 BATCH_ORDER>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 2 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 ENC_FLAG>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 PAIR_GLASSID>
                <ASCII 20 PAIR_PPID>
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - ModuleMaterialEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 1 
          <LIST 1 
            <LIST 21 
              <ASCII 32 M_TRAYID>
              <ASCII 32 M_BATCHID>
              <UINT1 1 M_KIND>
              <ASCII 16 M_TYPE>
              <ASCII 16 M_MAKER>
              <ASCII 16 M_CODE>
              <ASCII 8 M_REVNO>
              <ASCII 8 TRAY_STATE>
              <UINT4 1 TOTAL_QTY>
              <UINT4 1 USED_QTY>
              <UINT4 1 INEQP_QTY>
              <UINT4 1 REMAIN_QTY>
              <UINT4 1 NG_QTY>
              <UINT4 1 ASSEMBLE_QTY>
              <ASCII 4 PORTID>
              <ASCII 4 PRODUCT_TYPE>
              <ASCII 4 PRODUCT_KIND>
              <ASCII 16 PRODUCTID>
              <ASCII 16 M_STEP>
              <ASCII 16 COMMENT>
              <LIST n 
                <LIST 6 
                  <ASCII 32 M_ID>
                  <ASCII 16 M_LOC>
                  <ASCII 16 M_SUBLOC>
                  <ASCII 8 M_SLOT>
                  <ASCII 4 D_CODE>
                  <ASCII 16 TAG>
                >
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - ModuleLabelPrintEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 5 
            <UINT1 1 L_KIND>
            <ASCII 16 PRODUCTID>
            <ASCII 12 BATCHID>
            <ASCII 40 SERIAL_NO>
            <LIST n 
              <ASCII 16 H_GLASSID>
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - ModuleMaterialAssembleEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 1 
            <LIST 21 
              <ASCII 32 M_TRAYID>
              <ASCII 32 M_BATCHID>
              <UINT1 1 M_KIND>
              <ASCII 16 M_TYPE>
              <ASCII 16 M_MAKER>
              <ASCII 16 M_CODE>
              <ASCII 8 M_REVNO>
              <ASCII 8 TRAY_STATE>
              <UINT4 1 TOTAL_QTY>
              <UINT4 1 USED_QTY>
              <UINT4 1 INEQP_QTY>
              <UINT4 1 REMAIN_QTY>
              <UINT4 1 NG_QTY>
              <UINT4 1 ASSEMBLE_QTY>
              <ASCII 4 PORTID>
              <ASCII 4 PRODUCT_TYPE>
              <ASCII 4 PRODUCT_KIND>
              <ASCII 16 PRODUCTID>
              <ASCII 16 M_STEP>
              <ASCII 16 COMMENT>
              <LIST n 
                <LIST 6 
                  <ASCII 32 M_ID>
                  <ASCII 16 M_LOC>
                  <ASCII 16 M_SUBLOC>
                  <ASCII 8 M_SLOT>
                  <ASCII 4 D_CODE>
                  <ASCII 16 TAG>
                >
              >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 ENC_FLAG>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 PAIR_GLASSID>
                <ASCII 20 PAIR_PPID>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - ModuleProductionPlanEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 22 
          <ASCII 32 PLAN_ID>
          <UINT2 1 PLAN_ORDER>
          <ASCII 8 PLAN_STATE>
          <UINT1 1 PRIORITY>
          <ASCII 4 PRODUCT_TYPE>
          <ASCII 4 PRODUCT_KIND>
          <ASCII 16 PRODUCTID>
          <ASCII 16 RUNSPECID>
          <ASCII 8 LAYERID>
          <ASCII 8 STEPID>
          <ASCII 20 PPID>
          <UINT4 1 PLAN_SIZE>
          <ASCII 18 MAKER>
          <ASCII 6 THICKNESS>
          <ASCII 2 INPUT_LINE>
          <ASCII 16 COMMENT>
          <UINT4 1 TOTAL_QTY>
          <UINT4 1 USED_QTY>
          <UINT4 1 INEQP_QTY>
          <UINT4 1 REMAIN_QTY>
          <UINT4 1 NG_QTY>
          <UINT4 1 ASSEMBLE_QTY>
        >
      >
    >
  >
>

<S6F11 P EventReportSend - InEQRouteProductionPlanEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST 6 
          <ASCII 28 MODULE_ID>
          <UINT1 1 MCMD>
          <UINT1 1 MODULE_STATE>
          <UINT1 1 PROC_STATE>
          <UINT1 1 BYWHO>
          <ASCII 16 OPERID>
        >
      >
      <LIST 2 
        <LIST 3 
          <ASCII 20 OLDPATH>
          <ASCII 10 PATHNAME_OLD>
          <LIST n 
            <LIST 2 
              <ASCII 28 OLD_PATH_MODULE_ID>
              <ASCII 2 OLD_PATH_ORDER>
            >
          >
        >
        <LIST 3 
          <ASCII 20 NEWPATH>
          <ASCII 10 PATHNAME_NEW>
          <LIST n 
            <LIST 2 
              <ASCII 28 NEW_PATH_MODULE_ID>
              <ASCII 2 NEW_PATH_ORDER>
            >
          >
        >
      >
    >
  >
>

<S6F11 P EventReportSend - SplitRecipeManagementEvent
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 5 
      <UINT1 1 MODE>
      <ASCII 28 MODULE_ID>
      <ASCII 20 PPID>
      <UINT1 1 PPID_TYPE>
      <LIST n 
        <LIST 5 
          <ASCII 16 GROUP_CODE>
          <UINT2 1 PARTIAL_NO>
          <UINT2 1 TOTAL_NO>
          <ASCII 28 SUB_MODULE_ID>
          <LIST n 
            <LIST 2 
              <ASCII 40 P_PARM_NAME>
              <ASCII 40 P_PARM>
            >
          >
        >
      >
    >
  >
>

<S6F12 S Event ReportAck
  <UINT1 1 ACKC6>
>

<S6F13 P NameListVariableDataSend - GlassData
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 ENC_FLAG>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 PAIR_GLASSID>
                <ASCII 20 PAIR_PPID>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 28 MODULE_ID>
            <LIST n 
              <LIST 2 
                <ASCII 40 ITEM_NAME>
                <ASCII 40 ITEM_VALUE>
              >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 40 FLOW_PATH>
            <ASCII 40 FLOW_MODULE_ID>
          >
        >
      >
    >
  >
>

<S6F13 P NameListVariableDataSend - LotData
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 0 >
              <LIST 0 >
              <LIST 0 >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 28 MODULE_ID>
            <LIST n 
              <LIST 2 
                <ASCII 40 ITEM_NAME>
                <ASCII 40 ITEM_VALUE>
              >
            >
          >
        >
      >
    >
  >
>

<S6F13 P NameListVariableDataSend - GlassLotData
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 H_GLASSID>
            <ASCII 16 E_GLASSID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PORDUCT_KIND>
            <ASCII 16 PORDUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 GLASS_SIZE>
            <UINT2 1 GLASS_THICKNESS>
            <UINT2 1 GLASS_STATE>
            <ASCII 4 GLASS_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 0 >
              <LIST 0 >
              <LIST 0 >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 28 MODULE_ID>
            <LIST 4 
              <LIST 2 
                <ASCII 16 RAWPATH>
                <ASCII 100 GLASS_DATA_FILE_PATH>
              >
              <LIST 2 
                <ASCII 16 SUMPATH>
                <ASCII 100 LOT_DATA_FILE_PATH>
              >
              <LIST 2 
                <ASCII 16 IMGPATH>
                <ASCII 100 IMAGE_FILE_PATH>
              >
              <LIST 2 
                <ASCII 16 DISK>
                <ASCII 100 FILE_DISK>
              >
            >
          >
        >
      >
    >
  >
>

<S6F13 P NameListVariableDataSend - MaterialData
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 3 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 MATERIAL_ID>
            <ASCII 16 MATERIAL_SETID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PRODUCT_KIND>
            <ASCII 16 PRODUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 MATERIAL_SIZE>
            <UINT2 1 MATERIAL_THICKNESS>
            <UINT2 1 MATERIAL_STATE>
            <ASCII 4 MATERIAL_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 10 
                <ASCII 4 USE_COUNT>
                <ASCII 4 JUDGEMENT>
                <ASCII 4 REASON_CODE>
                <ASCII 2 INS_FLAG>
                <ASCII 2 LIBRARYID>
                <ASCII 2 PRERUN_FLAG>
                <ASCII 2 TURN_DIR>
                <ASCII 2 FLIP_STATE>
                <ASCII 4 WORK_STATE>
                <ASCII 16 MULTI_USE>
              >
              <LIST 2 
                <ASCII 16 STAGE_STATE>
                <ASCII 20 LOCATION>
              >
              <LIST 5 
                <LIST 2 
                  <ASCII 40 OPTION_NAME_1>
                  <ASCII 40 OPTION_VALUE_1>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_2>
                  <ASCII 40 OPTION_VALUE_2>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_3>
                  <ASCII 40 OPTION_VALUE_3>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_4>
                  <ASCII 40 OPTION_VALUE_4>
                >
                <LIST 2 
                  <ASCII 40 OPTION_NAME_5>
                  <ASCII 40 OPTION_VALUE_5>
                >
              >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 28 MODULE_ID>
            <LIST n 
              <LIST 2 
                <ASCII 40 ITEM_NAME>
                <ASCII 40 ITEM_VALUE>
              >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 40 FLOW_PATH>
            <ASCII 40 FLOW_MODULE_ID>
          >
        >
      >
    >
  >
>

<S6F13 P NameListVariableDataSend - MaterialLotData
  <LIST 3 
    <UINT1 1 DATA_ID>
    <UINT2 1 CEID>
    <LIST 2 
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 21 
            <ASCII 16 MATERIAL_ID>
            <ASCII 16 MATERIAL_SETID>
            <ASCII 16 LOTID>
            <ASCII 16 BATCHID>
            <ASCII 16 JOBID>
            <ASCII 4 PORTID>
            <ASCII 2 SLOTNO>
            <ASCII 4 PRODUCT_TYPE>
            <ASCII 4 PRODUCT_KIND>
            <ASCII 16 PRODUCTID>
            <ASCII 16 RUNSPECID>
            <ASCII 8 LAYERID>
            <ASCII 8 STEPID>
            <ASCII 20 PPID>
            <ASCII 20 FLOWID>
            <UINT2 2 MATERIAL_SIZE>
            <UINT2 1 MATERIAL_THICKNESS>
            <UINT2 1 MATERIAL_STATE>
            <ASCII 4 MATERIAL_ORDER>
            <ASCII 16 COMMENT>
            <LIST 3 
              <LIST 0 >
              <LIST 0 >
              <LIST 0 >
            >
          >
        >
      >
      <LIST 2 
        <UINT1 1 RPTID>
        <LIST n 
          <LIST 2 
            <ASCII 28 MODULE_ID>
            <LIST 4 
              <LIST 2 
                <ASCII 16 RAWPATH>
                <ASCII 100 GLASS_DATA_FILE_PATH>
              >
              <LIST 2 
                <ASCII 16 SUMPATH>
                <ASCII 100 LOT_DATA_FILE_PATH>
              >
              <LIST 2 
                <ASCII 16 IMGPATH>
                <ASCII 100 IMAGE_FILE_PATH>
              >
              <LIST 2 
                <ASCII 16 DISK>
                <ASCII 100 FILE_DISK>
              >
            >
          >
        >
      >
    >
  >
>

<S6F14 S AnnotatedEventReportAck
  <UINT1 1 ACKC6>
>

<S6F101 P VariableDataNameListReportReq
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT2 1 RPTUNIT>
    <LIST n 
      <ASCII 40 ITEM_NAME>
    >
  >
>

<S6F102 S VariableDataNameListReportData
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT2 1 RPTUNIT>
    <LIST n 
      <ASCII 40 DATA_ITEM>
    >
  >
>

<S7F1 P PPL_Inquire
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F2 S PPL_Grant
  <UINT1 1 PPGNT>
>

<S7F9 P MaterialIDReq
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F10 S MaterialIDData
  <LIST 3 
    <UINT1 1 >
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 21 
        <ASCII 16 MATERIAL_ID>
        <ASCII 16 MATERIAL_SETID>
        <ASCII 16 LOTID>
        <ASCII 16 BATCHID>
        <ASCII 16 JOBID>
        <ASCII 4 PORTID>
        <ASCII 2 SLOTNO>
        <ASCII 4 PRODUCT_TYPE>
        <ASCII 4 PRODUCT_KIND>
        <ASCII 16 PRODUCTID>
        <ASCII 16 RUNSPECID>
        <ASCII 8 LAYERID>
        <ASCII 8 STEPID>
        <ASCII 20 PPID>
        <ASCII 20 FLOWID>
        <UINT2 2 MATERIAL_SIZE>
        <UINT2 1 MATERIAL_THICKNESS>
        <UINT2 1 MATERIAL_STATE>
        <ASCII 4 MATERIAL_ORDER>
        <ASCII 16 COMMENT>
        <LIST 3 
          <LIST 10 
            <ASCII 4 USE_COUNT>
            <ASCII 4 JUDGEMENT>
            <ASCII 4 REASON_CODE>
            <ASCII 2 INS_FLAG>
            <ASCII 2 LIBRARYID>
            <ASCII 2 PRERUN_FLAG>
            <ASCII 2 TURN_DIR>
            <ASCII 2 FLIP_STATE>
            <ASCII 4 WORK_STATE>
            <ASCII 16 MULTI_USE>
          >
          <LIST 2 
            <ASCII 16 STAGE_STATE>
            <ASCII 20 LOCATION>
          >
          <LIST 5 
            <LIST 2 
              <ASCII 40 OPTION_NAME_1>
              <ASCII 40 OPTION_VALUE_1>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_2>
              <ASCII 40 OPTION_VALUE_2>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_3>
              <ASCII 40 OPTION_VALUE_3>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_4>
              <ASCII 40 OPTION_VALUE_4>
            >
            <LIST 2 
              <ASCII 40 OPTION_NAME_5>
              <ASCII 40 OPTION_VALUE_5>
            >
          >
        >
      >
    >
  >
>

<S7F21 P Remote_FPP
  <LIST 5 
    <UINT1 1 MODE>
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 2 
        <UINT2 1 CCODE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F22 S Remote_FPP_Ack
  <UINT1 1 ACKC7>
>

<S7F23 P FPP_Send
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 2 
        <UINT2 1 CCODE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F24 S FPP_Ack
  <UINT1 1 ACKC7>
>

<S7F25 P FPP_Req
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F26 S FPP_Data
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 2 
        <UINT2 1 CCODE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F31 P RemoteFPP_EVP
  <LIST 5 
    <UINT1 1 MODE>
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 5 
        <ASCII 16 GROUP_CODE>
        <UINT2 1 PARTIAL_NO>
        <UINT2 1 TOTAL_NO>
        <ASCII 28 SUB_MODULE_ID>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F32 S RemoteFPP_EVP_Ack
  <UINT1 1 ACKC7>
>

<S7F33 P FPP_EVP_Send
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 5 
        <ASCII 16 GROUP_CODE>
        <UINT2 1 PARTIAL_NO>
        <UINT2 1 TOTAL_NO>
        <ASCII 28 SUB_MODULE_ID>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F34 S FPP_EVP_Ack
  <UINT1 1 ACKC7>
>

<S7F35 P FPP_EVP_Req
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F36 S FPP_EVP_Data
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 2 
        <UINT2 1 CCODE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F101 P CurrentEQPPIDListReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F102 S CurrentEQPPIDData
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <ASCII 20 PPID>
    >
  >
>

<S7F103 P PPIDExistenceCheck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F104 S PPIDExistenceCheckAck
  <UINT1 1 ACK7>
>

<S7F107 P PPBodyModifyReport
  <LIST 5 
    <UINT1 1 MODE>
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 2 
        <UINT2 1 CCODE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F108 S PPBodyModifyReportAck
  <UINT1 1 ACK7>
>

<S7F109 P CurrentRunningEQPPIDReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F110 S CurrentRunningEQPPIDData
  <LIST 2 
    <UINT1 1 ACK>
    <LIST 3 
      <ASCII 28 MODULE_ID>
      <ASCII 20 PPID>
      <UINT1 1 PPID_TYPE>
    >
  >
>

<S7F117 P PPBodyChangedReportEVP
  <LIST 5 
    <UINT1 1 MODE>
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 5 
        <ASCII 16 GROUP_CODE>
        <UINT2 1 PARTIAL_NO>
        <UINT2 1 TOTAL_NO>
        <ASCII 28 SUB_MODULE_ID>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F118 S PPBodyChangedReportEVP_Ack
  <UINT1 1 ACK7>
>

<S7F123 P RunRecipeModificationEVP
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 2 
        <UINT2 1 CCODE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F124 S RecipeModificationEVP_Ack
  <UINT1 1 ACKC7>
>

<S7F125 P RunRecipeParamEVP_Req
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
  >
>

<S7F126 S RunRecipeParamDataEVP_Ack
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 2 
        <UINT2 1 CCODE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F207 P RunRecipeBodyModifyReportEVP
  <LIST 5 
    <UINT1 1 MODE>
    <ASCII 28 MODULE_ID>
    <ASCII 20 PPID>
    <UINT1 1 PPID_TYPE>
    <LIST n 
      <LIST 5 
        <ASCII 16 GROUP_CODE>
        <UINT2 1 PARTIAL_NO>
        <UINT2 1 TOTAL_NO>
        <ASCII 28 SUB_MODULE_ID>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM>
          >
        >
      >
    >
  >
>

<S7F208 S RunRecipeBodyModifyReportEVP_Ack
  <UINT1 1 ACK7>
>

<S8F1 P Multi UseDataSet
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 2 
        <ASCII 10 DATA_TYPE>
        <LIST 3 
          <ASCII 40 ITEM_NAME>
          <ASCII 40 ITEM_VALUE>
          <ASCII 10 REFRENCE>
        >
      >
    >
  >
>

<S8F2 S Multi UseDataSetAck
  <LIST 2 
    <UINT1 1 ACK>
    <LIST n 
      <LIST 3 
        <ASCII 10 DATA_TYPE>
        <LIST 3 
          <ASCII 40 ITEM_NAME>
          <ASCII 40 ITEM_VALUE>
          <ASCII 10 REFRENCE>
        >
        <UINT1 1 EAC>
      >
    >
  >
>

<S8F3 P Multi UseDataInquiry
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 2 
        <ASCII 10 DATA_TYPE>
        <LIST n 
          <ASCII 40 ITEM_NAME>
        >
      >
    >
  >
>

<S8F4 S Multi UseDataInquiryAck
  <LIST 2 
    <UINT1 1 ACK>
    <LIST n 
      <LIST 2 
        <ASCII 10 DATA_TYPE>
        <LIST 3 
          <ASCII 40 ITEM_NAME>
          <ASCII 40 ITEM_VALUE>
          <ASCII 10 REFRENCE>
        >
      >
    >
  >
>

<S8F11 P MassDataTransfer
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 4 
        <ASCII 10 CATEGORY>
        <ASCII 28 REF_MODULE_ID>
        <ASCII 20 REFERENCE>
        <LIST n 
          <ASCII 1000 DATA_SET>
        >
      >
    >
  >
>

<S8F12 S MassData TransferAck
  <UINT1 1 ACK>
>

<S8F13 P MassData Report
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 4 
        <ASCII 10 CATEGORY>
        <ASCII 28 REF_MODULE_ID>
        <ASCII 20 REFERENCE>
        <LIST n 
          <ASCII 1000 DATA_SET>
        >
      >
    >
  >
>

<S8F14 S MassDataReportAck
  <UINT1 1 ACK>
>

<S10F3 N TerminalDisplaySingle
  <LIST 3 
    <UINT1 1 TID>
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 100 MSG>
    >
  >
>

<S16F1 P PPC_DataListReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 16 H_GLASSID>
    >
  >
>

<S16F2 S PPC_DataListAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 ACKC16>
    <LIST n 
      <LIST 4 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 16 SET_TIME>
        <LIST n 
          <LIST 3 
            <ASCII 28 P_MODULE_ID>
            <ASCII 2 P_ORDER>
            <UINT1 1 P_STATUS>
          >
        >
      >
    >
  >
>

<S16F3 P PPC_DataSet
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 3 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <LIST n 
          <LIST 2 
            <ASCII 28 P_MODULE_ID>
            <ASCII 2 P_ORDER>
          >
        >
      >
    >
  >
>

<S16F4 S PPC_DataSetAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 ACKC16>
    <LIST n 
      <LIST 3 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <LIST n 
          <LIST 2 
            <ASCII 28 P_MODULE_ID>
            <ASCII 2 P_ORDER>
          >
        >
      >
    >
  >
>

<S16F5 P PPC_DataDeletionCmd
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 16 H_GLASSID>
    >
  >
>

<S16F6 S PPC_DataDeletionCmdAckd
  <UINT1 1 ACK16>
>

<S16F11 P PPC_DataRunningEvent
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 4 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 16 SET_TIME>
        <LIST n 
          <LIST 3 
            <ASCII 28 P_MODULE_ID>
            <ASCII 2 P_ORDER>
            <UINT1 1 P_STATUS>
          >
        >
      >
    >
  >
>

<S16F12 S PPC_DataRunningEventAck
  <UINT1 1 ACKC16>
>

<S16F15 P PPC_Event
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <UINT1 1 MODE>
    <UINT1 1 BYWHO>
    <LIST 4 
      <ASCII 16 H_GLASSID>
      <ASCII 16 JOBID>
      <ASCII 16 SET_TIME>
      <LIST n 
        <LIST 3 
          <ASCII 28 P_MODULE_ID>
          <ASCII 2 P_ORDER>
          <UINT1 1 P_STATUS>
        >
      >
    >
  >
>

<S16F16 S PPC_EventAck
  <UINT1 1 ACKC16>
>

<S16F101 P APC_DataReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 16 H_GLASSID>
    >
  >
>

<S16F102 S APC_DataAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 TCACK>
    <LIST n 
      <LIST 6 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RECIPE>
        <ASCII 16 SET_TIME>
        <UINT1 1 APC_STATE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM_VALUE>
          >
        >
      >
    >
  >
>

<S16F103 P APC_DataSet
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 4 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RECIPE>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM_VALUE>
          >
        >
      >
    >
  >
>

<S16F104 S APC_DataSetAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 TCACK>
    <LIST n 
      <LIST 5 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RECIPE>
        <ASCII 16 SET_TIME>
        <LIST n 
          <LIST 2 
            <LIST 2 
              <ASCII 40 P_PARM_NAME>
              <UINT1 1 PACK>
            >
            <LIST 2 
              <ASCII 40 P_PARM_VALUE>
              <UINT1 1 PACK>
            >
          >
        >
      >
    >
  >
>

<S16F105 P APC_DataDeleteCommand
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 16 H_GLASSID>
    >
  >
>

<S16F106 S APC_DataDeleteCmdAckd
  <UINT1 1 TCACK>
>

<S16F111 P APC_StartReport
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 5 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RECIPE>
        <ASCII 16 SET_TIME>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM_VALUE>
          >
        >
      >
    >
  >
>

<S16F112 S APC_StartReportAckd
  <UINT1 1 TCACK>
>

<S16F113 P APC_EndReport
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 5 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RECIPE>
        <ASCII 16 SET_TIME>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM_VALUE>
          >
        >
      >
    >
  >
>

<S16F114 S APC_EndReportAckd
  <UINT1 1 TCACK>
>

<S16F115 P APC_DataReport
  <LIST 4 
    <ASCII 28 MODULE_ID>
    <UINT1 1 MODE>
    <UINT1 1 BYWHO>
    <LIST n 
      <LIST 5 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RECIPE>
        <ASCII 16 SET_TIME>
        <LIST n 
          <LIST 2 
            <ASCII 40 P_PARM_NAME>
            <ASCII 40 P_PARM_VALUE>
          >
        >
      >
    >
  >
>

<S16F116 S APC_DataReportAckd
  <UINT1 1 TCACK>
>

<S16F121 P RPC_ProcessChangeDataListReq
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 16 H_GLASSID>
    >
  >
>

<S16F122 S RPC_ProcessChangeDataListAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 ACKC16>
    <LIST n 
      <LIST 5 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RPC_PPID>
        <ASCII 16 SET_TIME>
        <UINT1 1 RPC_STATE>
      >
    >
  >
>

<S16F123 P RPC_ProcessChangeSet
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 3 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RPC_PPID>
      >
    >
  >
>

<S16F124 S RPC_ProcessChangeSetAck
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 ACKC16>
    <LIST n 
      <LIST 4 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RPC_PPID>
        <ASCII 16 SET_TIME>
      >
    >
  >
>

<S16F125 P RPC_DataDeleteCmd
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <ASCII 16 H_GLASSID>
    >
  >
>

<S16F126 S RPC_DataDeleteCmdAck
  <UINT1 1 ACK16>
>

<S16F131 P RPC_ProcessStartedEvent
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 4 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RPC_PPID>
        <ASCII 16 SET_TIME>
      >
    >
  >
>

<S16F132 S RPC_ProcessStartedEventAck
  <UINT1 1 ACKC16>
>

<S16F133 P RPC_ProcessEndedEvent
  <LIST 2 
    <ASCII 28 MODULE_ID>
    <LIST n 
      <LIST 4 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RPC_PPID>
        <ASCII 16 SET_TIME>
      >
    >
  >
>

<S16F134 S RPC_ProcessEndedEventAck
  <UINT1 1 ACKC16>
>

<S16F135 P RPC_Event
  <LIST 3 
    <ASCII 28 MODULE_ID>
    <UINT1 1 MODE>
    <LIST n 
      <LIST 5 
        <ASCII 16 H_GLASSID>
        <ASCII 16 JOBID>
        <ASCII 20 RPC_PPID>
        <ASCII 16 SET_TIME>
        <UINT1 1 BYWHO>
      >
    >
  >
>

<S16F136 S RPC_EventAck
  <UINT1 1 ACKC16>
>

