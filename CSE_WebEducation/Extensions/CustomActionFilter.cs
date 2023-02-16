using CommonLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ObjectInfo;

namespace CSE_WebEducation
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public string FunctionCode { get; set; }
        public bool CheckAuthentication { get; set; } = true;
        public bool CheckRight { get; set; } = true;
        public bool ShowNavigator { get; set; } = false;

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            bool passAction = true, isLastestLogin = true;
            string redirectTo = "/";
            string functionCode = FunctionCode;
            CSE_FunctionsInfo currFunc = new CSE_FunctionsInfo();
            int actionResultCode = 1, loggedStatus = 0;
            var controller = context.Controller as Controller;
            CSE_UsersInfo authUser = new CSE_UsersInfo();

            try
            {
                var requestUrl = context.HttpContext.Request.Path.ToString().ToLower();
                var requestQuery = context.HttpContext.Request.QueryString.ToString();
                var requestMethod = context.HttpContext.Request.Method.ToString();
                var isAjaxRequest = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

                var userLoggedState = await GetUserLoggedIn(context);
                loggedStatus = userLoggedState.Item1;
                authUser = userLoggedState.Item2;

                //chưa đăng nhập
                if (authUser == null)
                {
                    passAction = false;
                    isLastestLogin = false;
                    actionResultCode = ActionResultConfig.TIME_OUT;
                    redirectTo = RouteConfig.TIME_OUT;
                    goto getActionResult;
                }

                //chưa đăng nhập
                var user = context.HttpContext.GetCurrentUser();
                if (user == null)
                {
                    passAction = false;
                    isLastestLogin = false;
                    actionResultCode = ActionResultConfig.TIME_OUT;
                    redirectTo = RouteConfig.TIME_OUT;
                    goto getActionResult;
                }

                //nếu không check quyền bằng code thì lấy url để check
                if (string.IsNullOrEmpty(FunctionCode))
                {
                    currFunc = Function_Memory.Function_GetByUrl_User_Type(requestUrl, user.User_Type);
                    functionCode = currFunc?.Function_Code ?? string.Empty;
                }
                else
                {
                    currFunc = Function_Memory.Function_GetByFunctionCode_User_Type(FunctionCode, user.User_Type);
                }

                //nếu không check quyền hoặc user không được phân quyền
                if (CheckRight && !CheckUserRight(authUser, functionCode) && CheckAuthentication == true)
                {
                    passAction = false;
                    actionResultCode = ActionResultConfig.UNAUTHORIZED_AJAX;
                    redirectTo = RouteConfig.ACCESS_DENIED;
                    goto getActionResult;
                }

            getActionResult:
                if (!passAction)
                {
                    if (isAjaxRequest)
                    {
                        context.Result = new JsonResult(new { success = actionResultCode });
                        return;
                    }
                    else
                    {
                        context.Result = new RedirectResult(redirectTo);
                        return;
                    }
                }
                else if (requestMethod == "GET" && !isAjaxRequest && CheckAuthentication == true)
                {
                    if (CheckRight || ShowNavigator == true)
                    {
                        currFunc.Full_Request_Url = requestUrl + requestQuery;

                        // lấy từ mem ra đỡ phải làm cho vào thông tin user
                        List<CSE_FunctionsInfo> lstTreeFunctions = TreeFunctionsByChild(authUser.List_User_Functions, currFunc);
                        decimal rootFunctionId = lstTreeFunctions.FirstOrDefault(o => o.Lev == 0)?.Function_Id ?? 0;
                        controller.ViewBag.CurrFunction = currFunc;
                        controller.ViewBag.RootFunctionId = rootFunctionId;
                        controller.ViewBag.TreeFunctions = lstTreeFunctions;
                    }
                }

                base.OnActionExecuting(context);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                context.Result = new RedirectResult("/error");
            }
        }

        private async Task<Tuple<int, CSE_UsersInfo>> GetUserLoggedIn(ActionExecutingContext context)
        {
            CSE_UsersInfo authUser = new CSE_UsersInfo();
            int loggedStatus = 1;
            try
            {
                AuthenticateResult authResult = await context.HttpContext.AuthenticateAsync(ConstData.authType);
                if (authResult == null || authResult.None)
                {
                    loggedStatus = 0;  // Chưa login
                    authUser = null;
                }
                else if (!authResult.Succeeded)
                {
                    loggedStatus = -1; //timeout...
                    authUser = null;
                }
                else
                {
                    var claimsIdentity = context.HttpContext.User.Identities.FirstOrDefault(x => x.AuthenticationType == ConstData.authType);
                    string authUserValue = claimsIdentity?.Claims?.FirstOrDefault(p => p.Type == ConstData.authClaimType)?.Value ?? "";
                    authUser = JsonConvert.DeserializeObject<CSE_UsersInfo>(authUserValue);

                    if (MemoryData.c_dic_Function_ByUser.ContainsKey(authUser.User_Id))
                    {
                        authUser.List_User_Functions = MemoryData.c_dic_Function_ByUser[authUser.User_Id];
                    }
                    else
                    {
                        authUser.List_User_Functions = new List<CSE_FunctionsInfo>();
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
            return new Tuple<int, CSE_UsersInfo>(loggedStatus, authUser);
        }

        private bool CheckUserRight(CSE_UsersInfo authUser, string functionCode)
        {
            try
            {
                return authUser.List_User_Functions.Any(o => o.Function_Code.ToUpper() == functionCode?.ToUpper());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return false;
            }
        }

        private List<CSE_FunctionsInfo> TreeFunctionsByChild(List<CSE_FunctionsInfo> lstUserFunction, CSE_FunctionsInfo function)
        {
            List<CSE_FunctionsInfo> lstTreeFunctions = new List<CSE_FunctionsInfo>();
            try
            {

                CSE_FunctionsInfo rootFunction = function;
                lstTreeFunctions.Insert(0, rootFunction);
                Function_Memory _FunctionMemory = new Function_Memory();
                CSE_FunctionsInfo parentFunction = ParentFunctionByChild(lstUserFunction, rootFunction);
                while (parentFunction != null)
                {
                    rootFunction = parentFunction;
                    lstTreeFunctions.Insert(0, rootFunction);
                    parentFunction = ParentFunctionByChild(lstUserFunction, parentFunction);
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
            return lstTreeFunctions;
        }

        private static CSE_FunctionsInfo ParentFunctionByChild(List<CSE_FunctionsInfo> lstUserFunction, CSE_FunctionsInfo function)
        {
            try
            {
                return lstUserFunction.FirstOrDefault(o => o.Function_Id == function.Prid);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return null;
            }
        }
    }
}
