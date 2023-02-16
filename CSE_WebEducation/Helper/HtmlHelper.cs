using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectInfo;
using System.Text;

namespace CSE_WebEducation.Helper
{
    public static class HtmlHelper
    {
        public static string AntiForgeryTokenValue(this IHtmlHelper htmlHelper)
        {
            var antiforgery = (IAntiforgery)htmlHelper.ViewContext.HttpContext.RequestServices.GetService(typeof(IAntiforgery));
            var tokenSet = antiforgery.GetAndStoreTokens(htmlHelper.ViewContext.HttpContext);
            string fieldName = tokenSet.FormFieldName;
            string requestToken = tokenSet.RequestToken;
            return requestToken;
        }
        public static IHtmlContent CreateMenuLeft(this IHtmlHelper htmlHelper, List<CSE_FunctionsInfo> lstAllFunctionsByUser, decimal rootId, decimal functionLev1, decimal funcLev2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"navigation\">");
            string htmlString = GetTreeMenuLeft(lstAllFunctionsByUser, rootId, functionLev1, funcLev2);
            sb.Append(htmlString);
            sb.Append("</nav>");
            return new HtmlString(sb.ToString());
        }

        private static string GetTreeMenuLeft(List<CSE_FunctionsInfo> lstAllFunctionsByUser, decimal rootId, decimal functionLev1, decimal functionLev2)
        {
            StringBuilder sb = new StringBuilder();
            try
            {

                List<CSE_FunctionsInfo> lstChilds = lstAllFunctionsByUser.Where(f => f.Prid == rootId && f.Display_On_Menu == 1).OrderBy(f => f.Position).ToList();
                //string todoTrans = "";
                //string todoApprv = "";

                if (lstChilds?.Count > 0)
                {

                    var lev = lstChilds.First()?.Lev ?? 0;
                    string displayLev2 = "";
                    string classname = lev == 0 ? "navigation" : "block-dropdown";

                    sb.Append("<div class=\"" + classname + "\">");

                    string menu = lev == 0 ? "menu-parent" : "menuchild";
                    if (rootId == functionLev1 && lev == 1)
                    {
                        displayLev2 = " style=\"display:block\"";
                    }
                    
                    sb.Append("<ul class=\"" + menu + "\"" + displayLev2 + ">");


                    foreach (CSE_FunctionsInfo item in lstChilds)
                    {
                        bool hasChild = (lstAllFunctionsByUser.Any(o => o.Prid == item.Function_Id && o.Display_On_Menu == 1));
                        string classActive = "", classRotateDown = "";
                        if (functionLev1 == item.Function_Id)
                        {
                            classActive = "active";
                            classRotateDown = "rotate-down";
                        }

                        if (functionLev2 == item.Function_Id) classActive = "active";
                        if (hasChild)
                        {
                            sb.Append("<li class=\"" + classActive + " has-dropdown " + classRotateDown + "\">");
                            sb.Append("<a href=\"javascript:;\">" + "<img class=\"icon-nav\" src=\"" + item.Function_Icon + ".svg\" alt=\"img\">" + "<span>" + item.Function_Name + "</span>" + "<img class=\"icon-agldown\" src=\"/bidding_lib/img/icon-agldownnav.svg\" alt=\"img\">");
                        }
                        else
                        {
                            sb.Append("<li class=\"" + classActive + "\">");
                            sb.Append("<a href='" + (item.Function_Url != null && item.Function_Url.Length > 0 ? item.Function_Url : "javascript:;") + "'>" + item.Function_Name);
                        }
                        
                        sb.Append("</a>");

                        // Get childs
                        sb.Append(GetTreeMenuLeft(lstAllFunctionsByUser, item.Function_Id, functionLev1, functionLev2));
                        sb.Append("</li>");
                    }
                    sb.Append("</ul>");
                    sb.Append("</div>");

                }


            }
            catch (Exception)
            {
                sb = new StringBuilder();
            }

            return sb.ToString();
        }

        public static IHtmlContent CreateBreadCrumb(this IHtmlHelper htmlHelper, List<CSE_FunctionsInfo> lstTreeFunctions)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0; string requestUrl = string.Empty;
            sb.Append("<a href=\"\"><img class=\"icon-home\" src=\"/bidding_lib/img/icon-home.svg\"></a>");

            sb.Append("<span><img src=\"/bidding_lib/img/icon-angleleft.svg\"></span>");
            foreach (var item in lstTreeFunctions)
            {
                requestUrl = item.Full_Request_Url ?? item.Function_Url;
                
                if (i != 0)
                {

                    sb.Append("<span><img src=\"/bidding_lib/img/icon-angleleft.svg\"></span>");
                }

                if (requestUrl == null || requestUrl == "")
                {
                    sb.Append("<a href=\"javascript:;\">" + item.Function_Name + "</a>");
                }
                else
                {
                    sb.Append("<a href=\"" + requestUrl + "\">" + item.Function_Name + "</a>");
                }


                i++;
            }
            return new HtmlString(sb.ToString());
        }
    }
}
