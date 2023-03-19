using CommonLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectInfo;
using System.Security.Claims;

namespace CSE_WebEducation.Controllers
{
    public class LoginController : Controller
    {
        [Route("login/do-login"), HttpPost]
        public async Task<IActionResult> DoLogin(string userName, string password)
        {
            decimal _responseCode = -1;
            string _responseMessage = "Sai tài khoản hoặc mật khẩu!";

            CSE_UsersInfo _user = new CSE_UsersInfo();
            try
            {
                //giải mã pass code 64
                string textPass = CommonFunc.Encrypt_MD5(password);

                _user = ApiClient_User.Login(userName, textPass);
                if (_user != null)
                {
                    _responseCode = 1;
                    _responseMessage = "Đăng nhập thành công!";

                    //lấy quyền
                    List<CSE_FunctionsInfo> _lstFunctions = new List<CSE_FunctionsInfo>();
                    _lstFunctions = ApiClient_User_Function.GetByUserId(_user.User_Id, _user.Token);

                    MemoryData.c_dic_Function_ByUser[_user.User_Id] = _lstFunctions;

                    await Update_ManageSuccessLoginAsync(_user);
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                _responseCode = -1;
                _responseMessage = "Đăng nhập thất bại!";
            }

            return Json(new { success = _responseCode, responseMessage = _responseMessage });
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                CSE_UsersInfo loggedUser = this.HttpContext.GetCurrentUser();
                //if (loggedUser != null)
                //{
                //    Api_TraceLog.Client_Log_Insert(this.HttpContext, "Đăng xuất", $"Người dùng \"{loggedUser.User_Name}\" đăng xuất hệ thống", "Người dùng");
                //}
                await HttpContext.SignOutAsync(ConstData.authType);

                // remove cookies
                Response.Cookies.Delete("UserLogin");
                HttpContext.Session.Clear();

                if (loggedUser != null)
                {
                    MemoryData.Process_Logout(loggedUser.User_Id, this.HttpContext);
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
            return Redirect("/");
        }

        async Task Update_ManageSuccessLoginAsync(CSE_UsersInfo _user)
        {
            try
            {
                var _userShare = new
                {
                    User_Id = _user.User_Id,
                    //User_Type = _user.User_Type,
                    User_Name = _user.User_Name,
                    Full_Name = _user.Full_Name,
                    Status = _user.Status,
                };

                var claims = new List<Claim>
                    {
                        new Claim(ConstData.authClaimType, JsonConvert.SerializeObject(_userShare))
                    };
                var claimsIdentity = new ClaimsIdentity(claims, ConstData.authType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
       
                //lưu tài khoản sau khi đăng nhập vào session
                this.HttpContext.Session.SetObjectAsJson("user", _user);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
        }
    }
}
