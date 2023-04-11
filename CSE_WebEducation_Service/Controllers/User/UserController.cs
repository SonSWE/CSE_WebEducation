using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ObjectInfo;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CSE_WebEducation_Service.Controllers.User
{
    public class UserController : Controller
    {
        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        #region login

        [Route("api/user/login"),HttpGet]
        public ActionResult<CSE_UsersInfo> Login(string p_user_name, string p_password)
        {
            try
            {
                CSE_UsersInfo user = AuthenticateUser(p_user_name, p_password);
                if (user != null)
                {
                    user.Token = GenerateJSONWebToken(user);
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return null;
            }
        }

        private string GenerateJSONWebToken(CSE_UsersInfo CSE_UsersInfo)
        {
            string key = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, CSE_UsersInfo.User_Name),
                new Claim(JwtRegisteredClaimNames.Email, ""),
                //new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                signingCredentials: credentials);

            if (CommonData.TimeOutLogin > 0)
            {
                token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(CommonData.TimeOutLogin),
                signingCredentials: credentials);
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private CSE_UsersInfo AuthenticateUser(string username, string password)
        {
            CSE_UserDA DA = new CSE_UserDA();
            CSE_UsersInfo user = DA.Login(username, password);
            if (user != null && user.User_Name != "")
            {
                return user;
            }
            return null;
        }

        #endregion

        #region CRUD
        [HttpGet]
        [Authorize]
        [Route("api/quan-tri/user/search")]
        public IActionResult Search(string user_name,string keySearch, int startRow, int endRow, string orderBy)
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();

                decimal totalRecord = 0;
                List<CSE_UsersInfo> _lst = new List<CSE_UsersInfo>();
                DataSet ds = DA.Search(user_name, keySearch, startRow, endRow, orderBy, ref totalRecord);
                _lst = CBO<CSE_UsersInfo>.FillCollection_FromDataSet(ds);

                return Json(new { totalRows = totalRecord, jsonData = JsonSerializer.Serialize(_lst) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { totalRows = "-1", jsonData = "" }); ;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/quan-tri/user/get-by-id")]
        public CSE_UsersInfo GetById([FromQuery] decimal id)
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();
                DataSet ds = DA.GetById(id);

                return CBO<CSE_UsersInfo>.FillObjectFromDataSet(ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new CSE_UsersInfo();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/quan-tri/user/insert")]
        public IActionResult Insert([FromBody] CSE_UsersInfo info)
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();
                decimal result = DA.Insert(info);

                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/quan-tri/user/update")]
        public IActionResult Update([FromBody] CSE_UsersInfo info)
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();
                decimal result = DA.Update(info);

                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/quan-tri/user/activeOrUnactive")]
        public IActionResult ActiveOrUnactive([FromBody] CSE_UsersInfo info)
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();
                decimal result = DA.ActiveOrUnactive(info);

                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/quan-tri/user/changePass")]
        public IActionResult ChangePass([FromBody] CSE_UsersInfo info)
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();
                decimal result = DA.ChangePass(info);

                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/quan-tri/user/delete")]
        public IActionResult Delete([FromBody] CSE_UsersInfo info)
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();
                decimal result = DA.Delete(info);

                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/quan-tri/user/get-all")]
        public List<CSE_UsersInfo> GetAll()
        {
            try
            {
                CSE_UserDA DA = new CSE_UserDA();
                DataSet _ds = DA.GetAll();

                return CBO<CSE_UsersInfo>.FillCollection_FromDataSet(_ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_UsersInfo>();
            }
        }
        #endregion
    }
}
