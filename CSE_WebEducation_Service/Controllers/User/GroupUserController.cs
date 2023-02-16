using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System.Data;

namespace CSE_WebEducation_Service.Controllers.User
{
    public class GroupUserController : Controller
    {
        [HttpGet]
        //[Authorize]
        [Route("api/quan-tri/group-user/get-by-group-id")]
        public List<CSE_UsersInfo> GetByGroupId([FromQuery] decimal group_id)
        {
            try
            {
                CSE_Group_UserDA DA = new CSE_Group_UserDA();
                DataSet _ds = DA.GetByGroupId(group_id);

                return CBO<CSE_UsersInfo>.FillCollection_FromDataSet(_ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_UsersInfo>();
            }
        }

        [HttpPost]
        //[Authorize]
        [Route("api/quan-tri/group-user/AddUserToGroup")]
        public IActionResult AddUserToGroup([FromBody] CSE_Group_User_Info info)
        {
            try
            {
                CSE_Group_UserDA DA = new CSE_Group_UserDA();
                decimal result = DA.AddUserToGroup(info);

                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpPost]
        //[Authorize]
        [Route("api/quan-tri/group-user/RemoveUserFromGroup")]
        public IActionResult RemoveUserFromGroup([FromBody] CSE_Group_User_Info info)
        {
            try
            {
                CSE_Group_UserDA DA = new CSE_Group_UserDA();
                decimal result = DA.RemoveUserFromGroup(info);

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
