/********************************************************************************
;
;   X MOTION HEADER
;
;
;
;
;
;*******************************************************************************
; Rev 1.00 2012.01.01	Initiative	
; Rev 1.01 2012.09.24	errmap table
; Rev 1.02 2012.11.02	abs, compare
; Rev 1.03 2013.02.15	pclr out
; Rev 1.04 2013.03.04	input counter, home 13
; Rev 1.05 2013.05.09	v_move(acc,dec)
; Rev 1.07 2013.0x.xx	Compare
; Rev 1.08 2015.07.14	v_move1 (1 pps control)
;						csv_move (constant speed move)
; Rev 1.09 2018.09.18	input counter 2,3,4,5
;-------------------------------------------------------------------------------
; Rev 2.00 20200320		Add API according to H/W revision
;*******************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace X_MOTION
{
    public class XMotionLib
    {
        public const string XMOTION_DLL_NAME = @"x_motion.dll";
        
        // Define Axis Source
        public enum AxSrc : int
        {
            #region ENUM_AXIS_SOURCE
                ID_NONE				= 0x00000000,
                ID_HOME_SWITCH		= 0x00000001,
                ID_POS_LIMIT		= 0x00000002,
                ID_NEG_LIMIT   		= 0x00000004,
                ID_AMP_FAULT		= 0x00000008,

                ID_X_NEG_LIMIT 		= 0x00000040,
                ID_X_POS_LIMIT 		= 0x00000080,
                ID_ENC_Z			= 0x00000100,
                ID_LATCH			= 0x00000200,
                ID_AMP_POWER_OFF	= 0x00000800,
                ID_INPOSITION		= 0x00002000
            #endregion
        }

       // API Define
        public enum APIDef : int
        {
            #region ENUM_API_DEF

                // Error Return Define ===============================================
                MMC_OK				    =		(0), // No Err
                MMC_NOT_FOUND_ERR	    =		(1), // MMC Not Founded Err
                MMC_DRIVER_OPEN_ERR	    =		(2), // Device Driver Open Err
                MMC_FILE_PARA_ERR	    =		(3), // File Open Read Write Err
                MMC_ABS_TIME_OUT_ERR	=   	(4), // ABS Serial Comm Time Out
                MMC_MUTEX_OPEN_ERR	    =		(5), // Mutex Open Err
                MMC_MUTEX_TIME_ERR	    =		(6), // Mutex Time Out

                MMC_INVALID_AXIS	    =		(7), // Invalid Axis Number
                MMC_INVALID_PARAMETER   =		(8), // Invalid Parameter
                MMC_NOT_INITIALIZED	    =		(9), // Not Initialized
                MMC_INVALID_SPEED	    =		(10),// Invalid Speed
                MMC_INVALID_AXES	    =		(11),// Invalid Axes for Multi Axis
                MMC_AMP_OFF			    =		(12),// Amp Off Status
                MMC_INVALID_ACCDEC	    =		(13),// Invalid Acc Dec
                MMC_NOT_READY		    =		(14),// Continuous Move Not Ready
                MMC_INVALID_PORT	    =		(15), // Invalid Port Number
                MMC_ABS_DATA_ERR		=   	(16),// ABS Serial Data Err

                NO_EVENT                =       0,
                STOP_EVENT			    =       1,
                
                MAX_ErrMap_TABLE_N		=       (100),
                MMC_ABS_RX_SIZE			=   	(40)

            #endregion
        }


        //Motion API Functions
        #region Public Function
            // =============================== Motion Init=================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 mmc_initx();                                 // motion board & library init
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_mmc_parameter_init(Int16 ax);			// axis parameter reset
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_version();								// dll sw version
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_bd_num();								// tatal motion board num
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_axis_num();								// tatal axis num
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 mmc_axes(Int16 bd, ref Int16 ax);			// each axis num on board
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 version_chk(Int16 bd, ref Int16 ver);		// motion hw version
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int32 get_board_id(Int16 bd, ref Int16 idnum);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_parameter(Int16 ax);						// parameter file save
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_parameter(Int16 ax);						// parameter file read

            // =============================== Ex IO Init=================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 mmc_exio_initx();
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_exio_bd_num();								// tatal ex io board num
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_exio_num(ref Int16 exinport, ref Int16 exoutport);		// tatal ex ioport num
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 mmc_exio(Int16 bd, ref Int16 exinport, ref Int16 exoutport);// each ex ioport num on board
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 exio_version_chk(Int16 bd, ref Int16 ver);				// ex io hw version
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int32 get_exboard_id(Int16 bd, ref Int16 idnum);

            // =============================== Motion User IO=================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_io(Int16 port, Int32 value);						// 0: On, 1: Off
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_io(Int16 port, ref Int32 value);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_out_io(Int16 port, ref Int32 value);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_bit(Int16 bitno);								// 0: On
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 reset_bit(Int16 bitno);

            // =============================== Ex User IO=================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_option_io(Int16 port, Int32 value);				// 0: On, 1: Off
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_option_io(Int16 port, ref Int32 value);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_option_out_io(Int16 port, ref Int32 value);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_option_bit(Int16 bitno);							// 0: On
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 reset_option_bit(Int16 bitno);

            // =============================== Motion IO Setup=================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_pulse_mode(Int16 ax, Int16 mode);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_pulse_mode(Int16 ax, ref Int16 mode);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_encoder_mode(Int16 ax, Int16 mode);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_encoder_mode(Int16 ax, ref Int16 mode);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_encoder_direction(Int16 ax, Int16 dir);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_encoder_direction(Int16 ax, ref Int16 dir);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_amp_enable(Int16 ax, Int16 onoff);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_amp_enable(Int16 ax, ref Int16 onoff);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_amp_reset(Int16 ax, Int16 onoff);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_amp_reset(Int16 ax, ref Int16 onoff);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_amp_fault_level(Int16 ax, Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_amp_fault_level(Int16 ax, ref Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_inposition_level(Int16 ax, Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_inposition_level(Int16 ax, ref Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_home_level(Int16 ax, Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_home_level(Int16 ax, ref Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_limit_level(Int16 ax, Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_limit_level(Int16 ax, ref Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_latch_level(Int16 ax, Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_latch_level(Int16 ax, ref Int16 level);

            // =============================== Motion Status =============================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 in_sequence(Int16 ax);							// loading or execution of command
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 in_motion(Int16 ax);								// velocity is non zero
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 in_position(Int16 ax);							// inposition signal
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 motion_done(Int16 ax);							// !in_sequence !in_motion
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 axis_done(Int16 ax);								// motion_done in_position

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int32 axis_source(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_sts(Int16 ax, ref Int16 msts, ref Int16 ssts);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_errint(Int16 ax, ref Int32 errint);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 reset_errint(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_eventint(Int16 ax, ref Int32 eventint);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 reset_eventint(Int16 ax);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 wait_for_done(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 wait_for_all(Int16 n, Int16[] axes);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 axis_state(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 clear_status(Int16 ax);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 home_switch(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 pos_switch(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 neg_switch(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 amp_fault_switch(Int16 ax);

            // =============================== Position Counter =============================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_command(Int16 ax, ref double command);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_command(Int16 ax, double command);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_position(Int16 ax, ref double position);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_position(Int16 ax, double position);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_error(Int16 ax, ref double counter);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_error(Int16 ax, double counter);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_mpg_counter(Int16 ax, ref double counter);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_mpg_counter(Int16 ax, double counter);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int32 get_com_velocity(Int16 ax); // pps 32bit

            // =============================== Alarm Stop Action  ==================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_positive_sw_limit(Int16 ax, double pos, Int16 action);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_positive_sw_limit(Int16 ax, ref double pos, ref Int16 action);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_negative_sw_limit(Int16 ax, double pos, Int16 action);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_negative_sw_limit(Int16 ax, ref double pos, ref Int16 action);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_limit_action(Int16 ax, Int16 action);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_limit_action(Int16 ax, ref Int16 action);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_amp_fault(Int16 ax, Int16 action);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_amp_fault(Int16 ax, ref Int16 action);

            // =============================== Single Axis Motion =================================================

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 tv_move(Int16 ax, double strvel, double maxvel, Int16 acc);					// contiuous velocity move with trapezoidal acc
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 sv_move(Int16 ax, double strvel, double maxvel, Int16 acc);					// contiuous velocity move with s-curve acc
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 emg_stop(Int16 ax);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 dec_stop(Int16 ax);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_tr_imove(Int16 ax, double position, double strvel, double maxvel, Int16 acc, Int16 dec);		// trapezoidal relative move
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_ta_imove(Int16 ax, double position, double strvel, double maxvel, Int16 acc, Int16 dec);		// trapezoidal absolute move
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sr_imove(Int16 ax, double position, double strvel, double maxvel, Int16 acc, Int16 dec);		// s curve relative move
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sa_imove(Int16 ax, double position, double strvel, double maxvel, Int16 acc, Int16 dec);		// s curve absolute move

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_move(Int16 ax, double pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 move(Int16 ax, double pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_r_move(Int16 ax, double pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 r_move(Int16 ax, double pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_s_move(Int16 ax, double pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 s_move(Int16 ax, double pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_rs_move(Int16 ax, double pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 rs_move(Int16 ax, double pos, double vel, Int16 acc);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_t_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 t_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_tr_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 tr_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_ts_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 ts_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_trs_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 trs_move(Int16 ax, double pos, double vel, Int16 acc, Int16 dec);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_s_move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 s_move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_t_move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc, Int16[] dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 t_move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc, Int16[] dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_ts_move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc, Int16[] dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 ts_move_all(Int16 n, Int16[] axes, double[] pos, double[] vel, Int16[] acc, Int16[] dec);

            // =============================== Multi Axis Motion ==================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_tr_imove_n(Int16 n, Int16[] axes, double[] pos, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_ta_imove_n(Int16 n, Int16[] axes, double[] pos, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sr_imove_n(Int16 n, Int16[] axes, double[] pos, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sa_imove_n(Int16 n, Int16[] axes, double[] pos, double strvel, double maxvel, Int16 acc, Int16 dec);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 move_2ax(Int16 ax1, Int16 ax2, double x, double y, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 move_3ax(Int16 ax1, Int16 ax2, Int16 ax3, double x, double y, double z, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 move_4ax(Int16 ax1, Int16 ax2, Int16 ax3, Int16 ax4, double x, double y, double z, double w, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 move_nax(Int16 len, Int16[] ax, double[] pos, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 smove_2ax(Int16 ax1, Int16 ax2, double x, double y, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 smove_3ax(Int16 ax1, Int16 ax2, Int16 ax3, double x, double y, double z, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 smove_4ax(Int16 ax1, Int16 ax2, Int16 ax3, Int16 ax4, double x, double y, double z, double w, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 smove_nax(Int16 len, Int16[] ax, double[] pos, double vel, Int16 acc);

            // =============================== Arc Motion =========================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_tr_arc_n(Int16 n, Int16[] axes, double centerx, double centery, double[] endpos, Int16 dir, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_ta_arc_n(Int16 n, Int16[] axes, double centerx, double centery, double[] endpos, Int16 dir, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sr_arc_n(Int16 n, Int16[] axes, double centerx, double centery, double[] endpos, Int16 dir, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sa_arc_n(Int16 n, Int16[] axes, double centerx, double centery, double[] endpos, Int16 dir, double strvel, double maxvel, Int16 acc, Int16 dec);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_tr_arc_deg_n(Int16 n, Int16[] axes, double centerx, double centery, double degree, double endposz, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_ta_arc_deg_n(Int16 n, Int16[] axes, double centerx, double centery, double degree, double endposz, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sr_arc_deg_n(Int16 n, Int16[] axes, double centerx, double centery, double degree, double endposz, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sa_arc_deg_n(Int16 n, Int16[] axes, double centerx, double centery, double degree, double endposz, double strvel, double maxvel, Int16 acc, Int16 dec);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_tr_arc_pass_n(Int16 n, Int16[] axes, double[] passpos, double[] endpos, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_ta_arc_pass_n(Int16 n, Int16[] axes, double[] passpos, double[] endpos, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sr_arc_pass_n(Int16 n, Int16[] axes, double[] passpos, double[] endpos, double strvel, double maxvel, Int16 acc, Int16 dec);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 start_sa_arc_pass_n(Int16 n, Int16[] axes, double[] passpos, double[] endpos, double strvel, double maxvel, Int16 acc, Int16 dec);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 arc_2ax(Int16 ax1, Int16 ax2, double x_center, double y_center, double angle, double vel, Int16 acc);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 arc_3ax(Int16 ax1, Int16 ax2, Int16 ax3, double x_center, double y_center, double angle, double pos, double vel, Int16 acc);

            // =============================== Continuous Path Motion =============================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_continuous_enable(Int16 ax, Int16 en);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_continuous_enable(Int16 ax, ref Int16 en);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_continuous_buffer(Int16 ax);

            // =============================== Override Motion =================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 position_override(Int16 ax, double position);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 velocity_override(Int16 ax, double velocity);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_velocity_range(Int16 ax, double maxvel, Int16 en);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_velocity_range(Int16 ax, ref double maxvel, ref Int16 en);

            // =============================== MPG Motion =================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 mpg_v_move(Int16 ax, double vellimit);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 mpg_p_move(Int16 ax, double position, double vellimit);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_mpg_mode(Int16 ax, Int16 mode);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_mpg_mode(Int16 ax, ref Int16 mode);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_mpg_dir(Int16 ax, Int16 dir);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_mpg_dir(Int16 ax, ref Int16 dir);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_mpg_ratio(Int16 ax, Int32 ratio);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_mpg_ratio(Int16 ax, ref Int32 ratio);

            // =============================== Special Function ================================================
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 homing(Int16 ax, Int16 dir, double strvel, double maxvel, double homvel, Int16 acc, Int16 mode);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_electric_gear(Int16 ax, double ratio);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_electric_gear(Int16 ax, ref double ratio);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_latched_position(Int16 ax, ref double position);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_correction(Int16 ax, Int16 type, double errpos, double vel);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_correction(Int16 ax, ref Int16 type, ref double errpos);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_amp_resolution(Int16 ax, Int32 resolution);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_amp_resolution(Int16 ax, ref Int32 resolution);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_command_rpm(Int16 ax, ref Int16 rpm_val);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 mmc_register_reset(Int16 ax);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_sensor_filter(Int16 ax, Int16 enable);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_sensor_filter(Int16 ax, ref Int16 enable);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_jog_filter(Int16 ax, Int16 enable);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_jog_filter(Int16 ax, ref Int16 enable);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_encoder_filter(Int16 ax, Int16 enable);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_encoder_filter(Int16 ax, ref Int16 enable);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_mpg_filter(Int16 ax, Int16 enable);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_mpg_filter(Int16 ax, ref Int16 enable);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_errmap_enable(Int16 ax, Int16 en);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_errmap_enable(Int16 ax, ref Int16 en);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_errmap_table(Int16 ax, Int16 table_n, double[] position, double[] error);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_errmap_table(Int16 ax, ref Int16 table_n, [Out] double[] position, [Out] double[] error);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_latch_flag_enable(Int16 ax, Int16 en);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_latch_flag_enable(Int16 ax, ref Int16 en);   
     
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_abs_serial(Int16 ax, Int16 type, [Out] byte[] reg);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_abs_position(Int16 ax, Int16 type, Int32 multiturn, Int32 singleturn);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_position_compare1(Int16 ax, Int16 en, Int16 counter, Int16 mode, double position);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_position_compare1(Int16 ax, ref Int16 en, ref Int16 counter, ref Int16 mode, ref double position);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_position_compare2(Int16 ax, Int16 en, Int16 counter, Int16 mode, double position);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_position_compare2(Int16 ax, ref Int16 en, ref Int16 counter, ref Int16 mode, ref double position);
        

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_pclr_out(Int16 ax, Int16 level);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_pclr_out(Int16 ax, ref Int16 level);

            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 set_input_counter(Int16 bd, Int32 counter0, Int32 counter1);
            [DllImport(XMOTION_DLL_NAME)]
            public static extern Int16 get_input_counter(Int16 bd, ref Int32 counter0, ref Int32 counter1);

            // ===============================================================================
            // REV2 : 2020.03.20 
            //		H/W Revision 에 따른 API 추가
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 set_amp_enable_level(Int16 ax, Int16 level);
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 get_amp_enable_level(Int16 ax, ref Int16 level);
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 set_pulse_swap_enable(Int16 ax, Int16 enable);
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 get_pulse_swap_enable(Int16 ax, ref Int16 enable);
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 set_limit_swap_enable(Int16 ax, Int16 enable);
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 get_limit_swap_enable(Int16 ax, ref Int16 enable);
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 set_limit_enable(Int16 ax, Int16 enable);
            //[DllImport(XMOTION_DLL_NAME)]
            //public static extern Int16 get_limit_enable(Int16 ax, ref Int16 enable);

        #endregion
    }
}

