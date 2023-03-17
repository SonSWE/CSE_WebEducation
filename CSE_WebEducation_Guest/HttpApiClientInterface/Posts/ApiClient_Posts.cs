using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation_Guest
{
    public class ApiClient_Posts
    {
        public static SearchResponseInfo Search( string keySearch, string startRow, string endRow, string p_token = "", string orderBy = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("/api/trang-khoa/bai-viet/search", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");

                request.AddParameter("keySearch", keySearch);
                request.AddParameter("startRow", startRow);
                request.AddParameter("endRow", endRow);
                request.AddParameter("orderBy", orderBy);

                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<SearchResponseInfo>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return null;
            }
        }

        public static CSE_PostsInfo GetById(decimal id, string p_token="")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/trang-khoa/bai-viet/get-by-id", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddParameter("id", id, ParameterType.QueryString);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<CSE_PostsInfo>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return null;
            }
        }
    }
}
