using CommonLib;
using ObjectInfo;
using System.Collections.Concurrent;

namespace CSE_WebEducation
{
    public class Config_Info
    {
        public static string NumberRedis = "";
        public static string c_key_NumberRedis = "NUMBER_REDIS";
        public static int Time_out = 120;
        public static int Numday_begin_recv = 4;

        //public static int Time_out_api = 3;
        //public static int Number_TimeOut = 2;


        public static int NUMBER_LOGIN_WRONG_LOCK = 6;                          // Số lần sai mật khẩu liên tiếp thì khóa tài khoản
        public static int NUMBER_LOGIN_WRONG_LOCK_TEMP = 3;                     // Số lần sai mật khẩu liên tiếp thì tạm khóa tài khoản
        public static int TIME_LOGIN_WRONG_LOCK_TEMP = 20;                      // Thời gian đăng nhập sai mật khẩu liên tiếp thì tạm khóa tài khoản tính bằng phút
        public static int TIME_LOCK_TEMP = 6;                                   // Thời gian khóa tạm thời
        public static int NUMBER_DATE_CHANGE_PASS = 30;                         // Số ngày đổi mật khẩu tính từ ngày đổi mật khẩu gần nhất

        /// <summary>
        /// Thời gian mặc định check dissconnect
        /// </summary>
        public static int c_time_check_disconnect = 5;
        public static string c_key_time_check_disconnect = "TIME_CHECK_DISCONNECT";

    }
    public class MemoryData
    {
        public static ConcurrentDictionary<decimal, List<CSE_FunctionsInfo>> c_dic_Function_ByUser = new ConcurrentDictionary<decimal, List<CSE_FunctionsInfo>>();

        public static void LoadMemory()
        {
            try
            {
                AllCodeMemory.Allcode_ReloadMemory();
                Function_Memory.Function_ReloadMemory();
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
        }
    }
}
