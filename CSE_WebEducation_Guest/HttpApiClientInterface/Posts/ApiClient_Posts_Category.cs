using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation_Guest
{
    public class ApiClient_Posts_Category
    {
        public static List<CSE_Posts_CategoriesInfo> GetAll(string p_token = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/posts-category/get-all", Method.GET);
                //request.AddHeader("Authorization", $"Bearer {p_token}");
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_Posts_CategoriesInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return new List<CSE_Posts_CategoriesInfo>();
            }
        }

        public static CSE_Posts_CategoriesInfo GetById(string id, string p_token = "")
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/posts-category/get-by-id", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddParameter("id", id, ParameterType.QueryString);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<CSE_Posts_CategoriesInfo>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return null;
            }
        }
    }
}
