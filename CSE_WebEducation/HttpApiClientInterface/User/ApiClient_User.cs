using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation
{
    public class ApiClient_User
    {
        #region login
        public static CSE_UsersInfo Login(string username, string password)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/user/login", Method.GET);

                request.AddParameter("p_user_name", username);
                request.AddParameter("p_password", password);

                var response = client.Execute(request);
                return JsonConvert.DeserializeObject<CSE_UsersInfo>(response?.Content ?? "");
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region CRUD

        public static List<CSE_UsersInfo> GetAll(string p_token = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user/get-all", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_UsersInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return new List<CSE_UsersInfo>();
            }
        }

        public static SearchResponseInfo Search(string p_token, string keySearch, string startRow, string endRow, string orderBy = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user/search", Method.GET);
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

        public static CSE_UsersInfo GetById(decimal id, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user/get-by-id", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddParameter("id", id, ParameterType.QueryString);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<CSE_UsersInfo>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return null;
            }
        }


        public static decimal Insert(CSE_UsersInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user/insert", Method.POST);
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

        public static decimal Update(CSE_UsersInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user/update", Method.POST);
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

        public static decimal Delete(CSE_UsersInfo info, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user/delete", Method.POST);
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

        #endregion
    }
}
