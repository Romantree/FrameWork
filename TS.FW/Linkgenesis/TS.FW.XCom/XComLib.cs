using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using TS.FW.Utils;
using XCOMLib;

namespace TS.FW.XCom
{
    public static class XComLib
    {
        /// <summary>
        /// XCom 통신 에러 리스트
        /// </summary>
        private readonly static Dictionary<int, string> _errorList;

        static XComLib()
        {
            _errorList = GetXComErrorList().ToDictionary(t => t.Key, t => t.Value);
        }

        public static void LoadControl(this XCOM xcom)
        {
            if (xcom.Created == true) return;

            ((ISupportInitialize)xcom).BeginInit();
            xcom.CreateControl();
            xcom.Name = "XCOMLib.dll";
            ((ISupportInitialize)xcom).EndInit();
        }

        /// <summary>
        /// XCOM 초기화
        /// </summary>
        /// <param name="xcom">XCOM 클래스</param>
        /// <param name="cfgFilePath">cfg 파일 경로</param>
        public static void InitializeEx(this XCOM xcom, string cfgFilePath)
        {
            if (File.Exists(cfgFilePath) == false) throw new FileNotFoundException("해당 경로에 파일이 존재하지 않습니다.", cfgFilePath);

            xcom.Initialize(cfgFilePath).CheckError();
        }

        /// <summary>
        /// XCOM 통신 시작
        /// </summary>
        /// <param name="xcom">XCOM 클래스</param>
        public static void StartEx(this XCOM xcom)
        {
            xcom.Start().CheckError();
        }

        /// <summary>
        /// XCOM 통신 중지
        /// </summary>
        /// <param name="xcom">XCOM 클래스</param>
        public static void StopEx(this XCOM xcom)
        {
            xcom.Stop().CheckError();
            xcom.Close().CheckError();
        }

        /// <summary>
        /// XCOM 통신 메세지 수신
        /// </summary>
        /// <param name="xcom">XCOM 클래스</param>
        /// <returns>수신 메세지 목록</returns>
        public static IEnumerable<XComMsgInfo> LoadMsg(this XCOM xcom)
        {
            short res = 0;

            do
            {
                var msg = new XComMsgInfo();
                res = xcom.LoadSecsMsg(out msg.msgID, out msg.deviceID, out msg.stream, out msg.func, out msg.sysByte, out msg.wBit);

                if (res >= 0) yield return msg;

            } while (res >= 0);
        }

        /// <summary>
        /// XCOM 메세지 ID 생성
        /// </summary>
        /// <param name="xcom"></param>
        /// <param name="devcieID"></param>
        /// <param name="stream"></param>
        /// <param name="func"></param>
        /// <param name="sysByte"></param>
        /// <returns></returns>
        public static int MakeMsg(this XCOM xcom, int devcieID, int stream, int func, int sysByte)
        {
            xcom.MakeSecsMsg(out int msg, devcieID, stream, func, sysByte).CheckError();

            return msg;
        }

        /// <summary>
        /// XCOM 메세지 송신
        /// </summary>
        /// <param name="xcom"></param>
        /// <param name="msgID"></param>
        public static void SendMsg(this XCOM xcom, int msgID)
        {
            xcom.Send(msgID).CheckError();
        }

        /// <summary>
        /// XCOM 메세지 알람 가져오기
        /// </summary>
        /// <param name="xcom">XCOM 클래스</param>
        /// <param name="messageId">메세지 ID</param>
        /// <returns></returns>
        public static XComMsgInfo GetAlarmMsgInfo(this XCOM xcom, int messageId)
        {
            var msg = new XComMsgInfo();

            xcom.GetAlarmMsgInfo(messageId, out msg.deviceID, out msg.stream, out msg.func, out msg.sysByte, out msg.wBit).CheckError();

            return msg;
        }

        /// <summary>
        /// 해당 관련 함수에 대한 정보가 정확하지 않음.
        /// </summary>
        /// <param name="xcom">XCOM 클래스</param>
        /// <param name="messageId">메세지 ID</param>
        /// <returns></returns>
        public static XComMsgInfo GetInvalidMsgInfo(this XCOM xcom, int messageId)
        {
            var msg = new XComMsgInfo();

            xcom.GetInvalidMsgInfo(messageId, out msg.deviceID, out msg.stream, out msg.func, out msg.sysByte, out msg.wBit).CheckError();

            return msg;
        }

        #region GET

        public static byte[] GetMsg(this XCOM xcom, int msgID)
        {
            var len = xcom.GetMsgBytes(msgID, out byte[] buffer);

            return buffer;
        }

        public static int GetList(this XCOM xcom, int msgID)
        {
            xcom.GetListItem(msgID, out short item);

            return item;
        }

        public static string GetString(this XCOM xcom, int msgID)
        {
            return xcom.GetStringItem(msgID, out int count).TrimEnd();
        }

        public static bool[] GetBoolList(this XCOM xcom, int msgID)
        {
            xcom.GetBoolItem(msgID, out bool[] item);
            return item;
        }

        public static byte[] GetBinaryList(this XCOM xcom, int msgID)
        {
            xcom.GetBinaryItem(msgID, out byte[] item);
            return item;
        }

        public static byte[] GetINT1List(this XCOM xcom, int msgID)
        {
            xcom.GetI1Item(msgID, out byte[] item);
            return item;
        }

        public static short[] GetINT2List(this XCOM xcom, int msgID)
        {
            xcom.GetI2Item(msgID, out short[] item);
            return item;
        }

        public static int[] GetINT4List(this XCOM xcom, int msgID)
        {
            xcom.GetI4Item(msgID, out int[] item);
            return item;
        }

        public static int[] GetINT8List(this XCOM xcom, int msgID)
        {
            xcom.GetI8Item(msgID, out int[] item);
            return item;
        }

        public static byte[] GetUINT1List(this XCOM xcom, int msgID)
        {
            xcom.GetU1Item(msgID, out byte[] item);
            return item;
        }

        public static int[] GetUINT2List(this XCOM xcom, int msgID)
        {
            xcom.GetU2Item(msgID, out int[] item);
            return item;
        }

        public static long[] GetUINT4List(this XCOM xcom, int msgID)
        {
            xcom.GetU4Item(msgID, out long[] item);
            return item;
        }

        public static long[] GetUINT8List(this XCOM xcom, int msgID)
        {
            xcom.GetU8Item(msgID, out long[] item);
            return item;
        }

        public static float[] GetFLOAT4List(this XCOM xcom, int msgID)
        {
            xcom.GetF4Item(msgID, out float[] item);
            return item;
        }

        public static double[] GetFLOAT8List(this XCOM xcom, int msgID)
        {
            xcom.GetF8Item(msgID, out double[] item);
            return item;
        }

        public static bool GetBool(this XCOM xcom, int msgID)
        {
            var item = xcom.GetBoolList(msgID);
            if (item == null || item.Length <= 0) return false;

            return item[0];
        }

        public static byte GetBinary(this XCOM xcom, int msgID)
        {
            var item = xcom.GetBinaryList(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static byte GetINT1(this XCOM xcom, int msgID)
        {
            var item = xcom.GetINT1List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static short GetINT2(this XCOM xcom, int msgID)
        {
            var item = xcom.GetINT2List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static int GetINT4(this XCOM xcom, int msgID)
        {
            var item = xcom.GetINT4List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static int GetINT8(this XCOM xcom, int msgID)
        {
            var item = xcom.GetINT8List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static byte GetUINT1(this XCOM xcom, int msgID)
        {
            var item = xcom.GetUINT1List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static int GetUINT2(this XCOM xcom, int msgID)
        {
            var item = xcom.GetUINT2List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static long GetUINT4(this XCOM xcom, int msgID)
        {
            var item = xcom.GetUINT4List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static long GetUINT8(this XCOM xcom, int msgID)
        {
            var item = xcom.GetUINT8List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static float GetFLOAT4(this XCOM xcom, int msgID)
        {
            var item = xcom.GetFLOAT4List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        public static double GetFLOAT8(this XCOM xcom, int msgID)
        {
            var item = xcom.GetFLOAT8List(msgID);
            if (item == null || item.Length <= 0) return 0;

            return item[0];
        }

        #endregion

        #region SET

        public static void SetList(this XCOM xcom, int msgiD, int value)
        {
            xcom.SetListItem(msgiD, value);
        }

        public static void SetString(this XCOM xcom, int msgID, string value, int len)
        {
            var temp = (string.IsNullOrWhiteSpace(value) ? string.Empty : value).PadRight(len, ' ');

            xcom.SetAsciiItem(msgID, temp, len);
        }

        public static void SetBool(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetBoolItem(msgId, (bool)value);
            }
            else
            {
                xcom.SetBoolItem(msgId, ToArray((bool[])value, len));
            }
        }

        public static void SetBinary(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetBinaryItem(msgId, (byte)value);
            }
            else
            {
                xcom.SetBinaryItem(msgId, ToArray((byte[])value, len));
            }
        }

        public static void SetINT1(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetI1Item(msgId, (byte)value);
            }
            else
            {
                xcom.SetI1Item(msgId, ToArray((byte[])value, len));
            }
        }

        public static void SetINT2(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetI2Item(msgId, (short)value);
            }
            else
            {
                xcom.SetI2Item(msgId, ToArray((short[])value, len));
            }
        }

        public static void SetINT4(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetI4Item(msgId, (int)value);
            }
            else
            {
                xcom.SetI4Item(msgId, ToArray((int[])value, len));
            }
        }

        public static void SetINT8(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetI8Item(msgId, (int)value);
            }
            else
            {
                xcom.SetI8Item(msgId, ToArray((int[])value, len));
            }
        }

        public static void SetUINT1(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetU1Item(msgId, (byte)value);
            }
            else
            {
                xcom.SetU1Item(msgId, ToArray((byte[])value, len));
            }
        }

        public static void SetUINT2(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetU2Item(msgId, (int)value);
            }
            else
            {
                xcom.SetU2Item(msgId, ToArray((int[])value, len));
            }
        }

        public static void SetUINT4(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetU4Item(msgId, (long)value);
            }
            else
            {
                xcom.SetU4Item(msgId, ToArray((long[])value, len));
            }
        }

        public static void SetUINT8(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetU8Item(msgId, (long)value);
            }
            else
            {
                xcom.SetU8Item(msgId, ToArray((long[])value, len));
            }
        }

        public static void SetFLOAT4(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetF4Item(msgId, (float)value);
            }
            else
            {
                xcom.SetF4Item(msgId, ToArray((float[])value, len));
            }
        }

        public static void SetFLOAT8(this XCOM xcom, int msgId, object value, int len = 1)
        {
            if (len <= 1)
            {
                xcom.SetF8Item(msgId, (double)value);
            }
            else
            {
                xcom.SetF8Item(msgId, ToArray((double[])value, len));
            }
        }

        #endregion

        /// <summary>
        /// XCOM 코드 에러 체크
        /// </summary>
        /// <param name="errorCode">에러 코드</param>
        private static void CheckError(this short errorCode)
        {
            try
            {
                if (errorCode < 0)
                {
                    if (_errorList.ContainsKey(errorCode)) throw new Exception(_errorList[errorCode]);

                    throw new Exception(string.Format("XCom 함수 실행 중 정의 되지 않는 에러가 발생하였습니다. Code={0}", errorCode));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(XComLib), ex);
            }
        }

        /// <summary>
        /// XCOM 에러 코드 리스트
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<KeyValuePair<int, string>> GetXComErrorList()
        {
            yield return new KeyValuePair<int, string>(-10001, "EXC_NOT_INITITIALIZED");
            yield return new KeyValuePair<int, string>(-10002, "EXC_ALREADY_INITIALIZED");
            yield return new KeyValuePair<int, string>(-10003, "EXC_NOT_STARTED");
            yield return new KeyValuePair<int, string>(-10004, "EXC_RUNNING");
            yield return new KeyValuePair<int, string>(-10005, "EXC_PROTECTION_KEY");
            yield return new KeyValuePair<int, string>(-10006, "EXC_INVALID_INSTANCE");
            yield return new KeyValuePair<int, string>(-10007, "EXC_WRONG_PATH");
            yield return new KeyValuePair<int, string>(-10008, "EXC_INVALID_SIZE");
            yield return new KeyValuePair<int, string>(-10010, "EXC_CFG_WRONG_FILE");
            yield return new KeyValuePair<int, string>(-10011, "EXC_CFG_GENERAL");
            yield return new KeyValuePair<int, string>(-10012, "EXC_CFG_HSMS");
            yield return new KeyValuePair<int, string>(-10013, "EXC_CFG_SECS1");
            yield return new KeyValuePair<int, string>(-10014, "EXC_CFG_TIMEOUT");
            yield return new KeyValuePair<int, string>(-10015, "EXC_CFG_LOG");
            yield return new KeyValuePair<int, string>(-10016, "EXC_CFG_STREAM9");
            yield return new KeyValuePair<int, string>(-10020, "EXC_CFG_INCOMPLETE");
            yield return new KeyValuePair<int, string>(-10021, "EXC_CREATE_WINDOW");
            yield return new KeyValuePair<int, string>(-10022, "EXC_INVALID_POINTER");
            yield return new KeyValuePair<int, string>(-10023, "EXC_SOCKET");
            yield return new KeyValuePair<int, string>(-10024, "EXC_SERIAL");
            yield return new KeyValuePair<int, string>(-10025, "EXC_NOT_ENOUGH_SIZE");
            yield return new KeyValuePair<int, string>(-10026, "EXC_NO_MSG");
            yield return new KeyValuePair<int, string>(-10027, "EXC_INVALID_DEV_ID");
            yield return new KeyValuePair<int, string>(-10028, "EXC_GET_MAX_INSTANCES");
            yield return new KeyValuePair<int, string>(-10032, "EXC_GARBAGE_INPUT");
            yield return new KeyValuePair<int, string>(-10033, "EXC_DATA_ALREADY_SET");
            yield return new KeyValuePair<int, string>(-11001, "EXC_S1_UNKNOWN_DEV_ID");
            yield return new KeyValuePair<int, string>(-11002, "EXC_S1_UNKNOWN_STREAM");
            yield return new KeyValuePair<int, string>(-11003, "EXC_S1_UNKNOWN_FUNC");
            yield return new KeyValuePair<int, string>(-11005, "EXC_S1_TOO_MANY_TRANSACTIONS");
            yield return new KeyValuePair<int, string>(-11006, "EXC_S1_BLOCK_SEQUENCE");
            yield return new KeyValuePair<int, string>(-12001, "EXC_HS_UNKNOWN_DEV_ID");
            yield return new KeyValuePair<int, string>(-12002, "EXC_HS_UNKNOWN_STREAM");
            yield return new KeyValuePair<int, string>(-12003, "EXC_HS_UNKNOWN_FUNC");
            yield return new KeyValuePair<int, string>(-12004, "EXC_HS_NOT_SELECTED");
            yield return new KeyValuePair<int, string>(-13001, "EXC_S2_TOO_MANY_ITEMS");
            yield return new KeyValuePair<int, string>(-13002, "EXC_S2_ITEM_FORMAT");
            yield return new KeyValuePair<int, string>(-13003, "EXC_S2_ITEM_COUNT");
            yield return new KeyValuePair<int, string>(-13004, "EXC_S2_ADD_MSG_TO_DB");
            yield return new KeyValuePair<int, string>(-13005, "EXC_S2_INVALID_MSG_ID");
            yield return new KeyValuePair<int, string>(-13006, "EXC_S2_DEL_MSG_FROM_DB");
            yield return new KeyValuePair<int, string>(-13007, "EXC_S2_INVALID_STREAM");
            yield return new KeyValuePair<int, string>(-13008, "EXC_S2_INVALID_FUNC");
            yield return new KeyValuePair<int, string>(-13009, "EXC_S2_INVALID_STRUCT");
            yield return new KeyValuePair<int, string>(-13010, "EXC_S2_INVALID_HEADER");
            yield return new KeyValuePair<int, string>(-13011, "EXC_S2_NO_ITEMS_LEFT");
            yield return new KeyValuePair<int, string>(-13012, "EXC_S2_FORMAT_MISMATCHED");
            yield return new KeyValuePair<int, string>(-13013, "EXC_S2_ITEM_VALUE");
            yield return new KeyValuePair<int, string>(-13014, "EXC_S2_DUPLICATED_MSG");
            yield return new KeyValuePair<int, string>(-13015, "EXC_S2_UNKNOWN_DEV_ID");
            yield return new KeyValuePair<int, string>(-13016, "EXC_S2_UNKNOWN_STREAM");
            yield return new KeyValuePair<int, string>(-13017, "EXC_S2_UNKNOWN_FUNC");
        }

        /// <summary>
        /// 데이터 갯수 마추기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static T[] ToArray<T>(T[] buffer, int count) where T : struct
        {
            var temp = new T[count];
            if (buffer == null) return temp;

            for (int i = 0; i < count; i++)
            {
                if (i >= buffer.Length) break;

                temp[i] = buffer[i];
            }

            return temp;
        }
    }
}
