using CommonLib;
using Newtonsoft.Json;
using ObjectInfo;
using RestSharp;

namespace CSE_WebEducation
{
    public class ApiClient_Group_Function
    {
        private List<CSE_FunctionsInfo> _lstFullFunctionInGroup;
        private List<CSE_FunctionsInfo> _lstRootFunctionInGroup;

        public List<CSE_FunctionsInfo> GetFunctionsByGroupId(decimal groupId, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group-function/get-by-group-id", Method.GET);
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddParameter("group_id", groupId);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CSE_FunctionsInfo>>(response?.Content ?? "");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
                return new List<CSE_FunctionsInfo>();
            }
        }

        public decimal SetFunctionsInGroup(List<CSE_Group_Function_Info> lstFunctionsSetUp, decimal groupId, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group-function/Group_Rights_Set_Function", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", $"Bearer {p_token}");
                request.AddJsonBody(JsonConvert.SerializeObject(lstFunctionsSetUp, ConstData.defaultSettings));
                request.AddParameter("p_group_Id", groupId, ParameterType.QueryString);
              
                IRestResponse response = client.Execute(request);
                return Convert.ToDecimal(JsonConvert.DeserializeObject<ResponseInfo>(response?.Content ?? "").success);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1;
            }
        }

        public string GetHtmlTreeViewFunctionsInGroup(decimal groupId, string p_token)
        {
            this._lstFullFunctionInGroup = GetFunctionsByGroupId(groupId, p_token);
            this._lstRootFunctionInGroup = ApiClient_Function.GetRootFunctions(_lstFullFunctionInGroup);
            return this.RenderFunctionsInGroupToHtmlTreeView(_lstRootFunctionInGroup, 0);
        }


        private string RenderFunctionsInGroupToHtmlTreeView(List<CSE_FunctionsInfo> lstFunctionInfos, decimal parentFunctionId)
        {
            var strHtml = string.Empty;

            var lstChildrenFunctionsInFirstSeq = ApiClient_Function.GetChildrenFunctions(lstFunctionInfos, parentFunctionId);
            foreach (var function in lstChildrenFunctionsInFirstSeq)
            {
                var currentFunctionParentId = function.Prid;
                var lstChildrenFunctionsInSecondSeq = this._lstFullFunctionInGroup.Where(t => t.Prid.Equals(function.Function_Id)).ToList();
                var checkFunctionAddedToGroup = string.Empty;
                var cssClassParentId = "data-parentId-" + currentFunctionParentId;
                if (function.Added_To_Group == 1)
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
                    strHtml += this.RenderFunctionsInGroupToHtmlTreeView(lstChildrenFunctionsInSecondSeq, function.Function_Id);
                    strHtml += "</ul>";
                    strHtml += "</li>";
                }
            }

            return strHtml;
        }


        public static decimal SetFunctionsForUser(decimal groupId, string p_token)
        {
            try
            {
                var client = new RestClient(ConstData.httpApiClientHost);
                var request = new RestRequest("api/quan-tri/group-function/SetFunctionForUser", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", $"Bearer {p_token}");
            
                request.AddParameter("p_group_Id", groupId, ParameterType.QueryString);

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
