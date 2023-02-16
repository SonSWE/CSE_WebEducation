using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation
{
    public class ApiClient_User_Function
    {
        private List<CSE_FunctionsInfo> _lstFullFunctionOfUser;
        private List<CSE_FunctionsInfo> _lstRootFunctionOfGroup;

        public static List<CSE_FunctionsInfo> GetByUserId(decimal userId, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user-function/get-by-user-id", Method.GET);
                //request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddParameter("userId", userId,ParameterType.QueryString);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_FunctionsInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return new List<CSE_FunctionsInfo>();
            }
        }

        public string GetHtmlTreeViewFunctionsOfUser(decimal userId, string p_token)
        {
            this._lstFullFunctionOfUser = GetByUserId(userId, p_token);
            this._lstRootFunctionOfGroup = ApiClient_Function.GetRootFunctions(_lstFullFunctionOfUser);
            return this.RenderFunctionsOfGroupToHtmlTreeView(_lstRootFunctionOfGroup, 0);
        }


        private string RenderFunctionsOfGroupToHtmlTreeView(List<CSE_FunctionsInfo> lstFunctionInfos, decimal parentFunctionId)
        {
            var strHtml = string.Empty;

            var lstChildrenFunctionsInFirstSeq = ApiClient_Function.GetChildrenFunctions(lstFunctionInfos, parentFunctionId);
            foreach (var function in lstChildrenFunctionsInFirstSeq)
            {
                var currentFunctionParentId = function.Prid;
                var lstChildrenFunctionsInSecondSeq = this._lstFullFunctionOfUser.Where(t => t.Prid.Equals(function.Function_Id)).ToList();
                var checkFunctionAddedToGroup = string.Empty;
                var cssClassParentId = "data-parentId-" + currentFunctionParentId;
                if (function.Authcode == "1")
                {
                    checkFunctionAddedToGroup = "checked";
                }
                if (lstChildrenFunctionsInSecondSeq.Count == 0)
                {
                    strHtml += "<li class=\"nestedCheckboxes\">";
                    strHtml += "<input type='checkbox' value='" + function.Function_Id + "' data-parent-id='" + function.Prid + "' data-id='" + function.Function_Id + "' id='currentFunction-" + function.Function_Id + "'"
                        + " class='x1000 " + cssClassParentId + "'" + checkFunctionAddedToGroup + "/> ";
                    strHtml += "<label for='" + function.Function_Id + "'>" + function.Function_Name + "</label>";
                    strHtml += "</li>";
                }
                else
                {
                    strHtml += "<li class=\"nestedCheckboxes\">";
                    strHtml += "<input type='checkbox' value='" + function.Function_Id + "' data-parent-id='" + function.Prid + "' data-id='" + function.Function_Id + "' id='currentFunction-" + function.Function_Id + "' ";
                    strHtml += " class='x1000 " + cssClassParentId + "' " + checkFunctionAddedToGroup + "/>";
                    strHtml += "<label for='" + function.Function_Id + "'>" + function.Function_Name + "</label>";
                    strHtml += "<ul class=\"children\">";
                    strHtml += this.RenderFunctionsOfGroupToHtmlTreeView(lstChildrenFunctionsInSecondSeq, function.Function_Id);
                    strHtml += "</ul>";
                    strHtml += "</li>";
                }
            }

            return strHtml;
        }

        public static decimal ResetFunction(decimal userId, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user-function/reset", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", $"Bearer {p_token}");

                request.AddParameter("userId", userId, ParameterType.QueryString);

                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public static decimal UpdateFunction(List<CSE_User_Function_Info> _lst, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/user-function/update", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonSerializer();

                request.AddHeader("Authorization", $"Bearer {p_token}");

                request.AddJsonBody(_lst);

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
