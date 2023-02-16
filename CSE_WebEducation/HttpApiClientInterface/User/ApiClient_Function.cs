using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation
{
    public class ApiClient_Function
    {

        public List<CSE_FunctionsInfo> GetAll(string p_token = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/function/get-all", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");

                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_FunctionsInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return null;
            }
        }
        //public List<CSE_FunctionsInfo> Search(string p_token, string function_name, string function_type, string orderby, int startrecord, int endrecord, ref decimal totalrows)
        //{
        //    try
        //    {
        //        List<CSE_FunctionsInfo> sysLogs = new List<CSE_FunctionsInfo>();

        //        var client = new RestClient(ConstData.httpApiClientHost);
        //        var request = new RestRequest("funtion/search", Method.GET);
        //        request.AddHeader("Authorization", $"Bearer {p_token}");

        //        request.AddParameter("function_name", function_name);
        //        request.AddParameter("function_type", function_type);
        //        request.AddParameter("orderby", orderby);
        //        request.AddParameter("startrecord", startrecord);
        //        request.AddParameter("endrecord", endrecord);

        //        IRestResponse response = client.Execute(request);
        //        SearchResponseInfo responseInfo = JsonConvert.DeserializeObject<SearchResponseInfo>(response?.Content ?? "");

        //        totalrows = responseInfo.totalrows;
        //        return JsonConvert.DeserializeObject<List<CSE_FunctionsInfo>>(responseInfo.jsondata);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.nlog.Error(ex.ToString());
        //        return null;
        //    }
        //}

        //public decimal Insert(CSE_FunctionsInfo func, string p_token)
        //{
        //    try
        //    {
        //        var client = new RestClient(ConstData.httpApiClientHost);
        //        var request = new RestRequest("funtion/insert", Method.POST);
        //        request.RequestFormat = DataFormat.Json;
        //        request.AddHeader("Authorization", $"Bearer {p_token}");
        //        request.AddJsonBody(func);
        //        IRestResponse response = client.Execute(request);
        //        return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.log.Error(ex.ToString());
        //        return -1;
        //    }
        //}

        //public List<CSE_FunctionsInfo> GetBy_User_type(decimal p_user_type, string p_token = "")
        //{
        //    try
        //    {
        //        var client = new RestClient(ConstData.httpApiClientHost);
        //        var request = new RestRequest("funtion/get-by-user-type", Method.GET);
        //        if (p_token != "")
        //        {
        //            request.AddHeader("Authorization", $"Bearer {p_token}");
        //        }
        //        request.AddParameter("p_user_type", p_user_type);
        //        IRestResponse response = client.Execute(request);
        //        return JsonConvert.DeserializeObject<List<CSE_FunctionsInfo>>(response?.Content ?? "");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.nlog.Error(ex.ToString());
        //        return null;
        //    }
        //}

        //public static List<CSE_FunctionsInfo> GetByUserId(decimal userId, string p_token)
        //{
        //    try
        //    {
        //        var client = new RestClient(ConstData.httpApiClientHost);
        //        var request = new RestRequest("funtion/function-get-by-id", Method.GET);
        //        request.AddHeader("Authorization", $"Bearer {p_token}");
        //        request.AddParameter("userId", userId);

        //        IRestResponse response = client.Execute(request);
        //        List<CSE_FunctionsInfo> _lst = JsonConvert.DeserializeObject<List<CSE_FunctionsInfo>>(response?.Content ?? "");
        //        return _lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.nlog.Error(ex.ToString());
        //        return new List<CSE_FunctionsInfo>();
        //    }
        //}

        public static List<CSE_FunctionsInfo> GetRootFunctions(List<CSE_FunctionsInfo> lstFunctionInfos)
        {
            return lstFunctionInfos.Where(t => t.Prid == 0).ToList();
        }

        public static List<CSE_FunctionsInfo> GetChildrenFunctions(List<CSE_FunctionsInfo> lstFunctionInfos, decimal parentId)
        {
            return lstFunctionInfos.Where(t => t.Prid.Equals(parentId)).ToList();
        }
    }
}
