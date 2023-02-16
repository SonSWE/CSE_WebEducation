using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation
{
    public class ApiClient_Group_User
    {
        public static List<CSE_UsersInfo> GetByGroupId(decimal group_id, string p_token = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group-user/get-by-group-id", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddParameter("group_id", group_id, ParameterType.QueryString);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_UsersInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return new List<CSE_UsersInfo>();
            }
        }

        public static decimal AddUserToGroup(CSE_Group_User_Info groupUser, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group-user/AddUserToGroup", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddJsonBody(JsonConvert.SerializeObject(groupUser, ConstData.defaultSettings));
                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public static decimal RemoveUserFromGroup(CSE_Group_User_Info groupUser, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group-user/RemoveUserFromGroup", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddJsonBody(JsonConvert.SerializeObject(groupUser, ConstData.defaultSettings));
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
