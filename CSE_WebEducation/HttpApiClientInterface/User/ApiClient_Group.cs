using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation
{
    public class ApiClient_Group
    {
        public static SearchResponseInfo Search(string p_token, string p_user_name, string keySearch, string startRow, string endRow, string orderBy = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("/api/quan-tri/group/search", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");

                request.AddParameter("user_name", p_user_name);
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

        public static CSE_GroupsInfo GetById(decimal id, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group/get-by-id", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddParameter("id", id, ParameterType.QueryString);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<CSE_GroupsInfo>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return null;
            }
        }

        public static List<CSE_GroupsInfo> GetAll(string p_token = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group/get-all", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_GroupsInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return new List<CSE_GroupsInfo>();
            }
        }

        public static decimal Insert(CSE_GroupsInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group/insert", Method.POST);
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

        public static decimal Update(CSE_GroupsInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group/update", Method.POST);
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

        public static decimal ActiveOrUnactive(CSE_GroupsInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group/activeOrUnactive", Method.POST);
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
        public static decimal Delete(CSE_GroupsInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group/delete", Method.POST);
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
