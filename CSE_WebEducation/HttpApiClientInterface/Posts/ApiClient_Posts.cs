using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation
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

        public static decimal Insert(CSE_PostsInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/trang-khoa/bai-viet/insert", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonSerializer();

                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddJsonBody(info);

                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public static decimal Update(CSE_PostsInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/trang-khoa/bai-viet/update", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddJsonBody(info);
                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public static decimal Delete(CSE_PostsInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/trang-khoa/bai-viet/delete", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddJsonBody(info);
                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
