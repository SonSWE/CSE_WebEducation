using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;

namespace CSE_WebEducation_Service.Controllers.User
{
    public class UserFunctionController : Controller
    {
        [Route("api/quan-tri/user-function/get-by-user-id"), HttpGet]
        public List<CSE_FunctionsInfo> GetByUserId([FromQuery] decimal userId)
        {
            try
            {
                List<CSE_FunctionsInfo> _lst = new List<CSE_FunctionsInfo>();
                CSE_User_FunctionDA DA = new CSE_User_FunctionDA();
                _lst = DA.GetByUserId(userId);

                return _lst;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_FunctionsInfo>();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/quan-tri/user-function/reset")]
        public IActionResult ResetFunction([FromQuery] decimal userId)
        {
            try
            {
                CSE_User_FunctionDA DA = new CSE_User_FunctionDA();
                decimal result = DA.ResetFunction(userId);

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
        [Route("api/quan-tri/user-function/update")]
        public IActionResult UpdateFunction([FromBody] List<CSE_User_Function_Info> _lsta)
        {
            try
            {
                CSE_User_FunctionDA DA = new CSE_User_FunctionDA();
                decimal result = DA.UpdateFunction(_lsta);

                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }
    }
}
