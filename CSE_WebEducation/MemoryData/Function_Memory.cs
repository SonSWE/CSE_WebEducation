using CommonLib;
using ObjectInfo;

namespace CSE_WebEducation
{
    public class Function_Memory
    {
        private static object _locklstFunction = new object();
        private static List<CSE_FunctionsInfo> lstFunction = new List<CSE_FunctionsInfo>();

        public static void Function_ReloadMemory()
        {
            try
            {
                ApiClient_Function apiClient = new ApiClient_Function();
                List<CSE_FunctionsInfo> _lst = apiClient.GetAll();
                lock (_locklstFunction)
                {
                    lstFunction = _lst;
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
        }

        public static CSE_FunctionsInfo Function_GetByFunctionId(decimal functionId)
        {
            try
            {
                return lstFunction.FirstOrDefault(x => x.Function_Id == functionId);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new CSE_FunctionsInfo();
            }
        }

        public static CSE_FunctionsInfo Function_GetByFunctionCode_User_Type(string functionCode, decimal p_user_type)
        {
            try
            {
                List<CSE_FunctionsInfo> lst_tem = lstFunction.FindAll(x => x.Function_Type.ToNumberStringN0() == p_user_type.ToNumberStringN0()).ToList();
                return lstFunction.FirstOrDefault(x => x.Function_Code.ToUpper().Equals(functionCode.ToUpper()));
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new CSE_FunctionsInfo();
            }
        }

        public static List<CSE_FunctionsInfo> Function_GetLst_ByFunctionCode(string functionCode)
        {
            try
            {
                List<CSE_FunctionsInfo> lst_tem = lstFunction.FindAll(x => x.Function_Code == functionCode.ToUpper()).ToList();
                return lst_tem;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_FunctionsInfo>();
            }
        }

        public static CSE_FunctionsInfo Function_GetByUrl_User_Type(string functionUrl, decimal p_user_type)
        {
            try
            {
                List<CSE_FunctionsInfo> lst_tem = lstFunction.FindAll(x => x.Function_Type.ToNumberStringN0() == p_user_type.ToNumberStringN0()).ToList();
                return lst_tem.FirstOrDefault(x => (!string.IsNullOrEmpty(x.Function_Url) && x.Function_Url.ToUpper().Equals(functionUrl.ToUpper())) || (!string.IsNullOrEmpty(x.Function_Url_Post) && x.Function_Url_Post.ToUpper().Equals(functionUrl.ToUpper())));
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new CSE_FunctionsInfo();
            }
        }
    }
}
