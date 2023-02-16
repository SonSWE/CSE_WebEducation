using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System.Transactions;

namespace CSE_WebEducation_Service.Controllers.User
{
    public class GroupFucntionController : Controller
    {
        [Route("api/quan-tri/group-function/get-by-group-id"), HttpGet]
        public List<CSE_FunctionsInfo> GetByGroupId([FromQuery] decimal group_id)
        {
            try
            {
                List<CSE_FunctionsInfo> _lst = new List<CSE_FunctionsInfo>();
                CSE_Group_FunctionDA DA = new CSE_Group_FunctionDA();
                _lst = DA.GetByGroupId(group_id);

                return _lst;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_FunctionsInfo>();
            }
        }


        [HttpPost]
        //[Authorize]
        [Route("api/quan-tri/group-function/Group_Rights_Set_Function")]
        public IActionResult Group_Rights_Set_Function([FromBody] List<CSE_Group_Function_Info> p_lst_data, [FromQuery] decimal p_group_Id)
        {
            decimal success = -1;
            try
            {
                CSE_Group_FunctionDA _DA = new CSE_Group_FunctionDA();
                using (var scope = new TransactionScope())
                {
                    success = _DA.Function_Group_Delete(p_group_Id);

                    if (success > 0 && p_lst_data?.Count > 0)
                    {
                        success = _DA.Function_Group_Insert_Batch(p_lst_data);
                    }

                    if (success <= 0)
                    {
                        Transaction.Current.Rollback();
                    }
                    else
                    {
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                success = -1;
            }
            return Json(new { success = success.ToString() });
        }

        [HttpPost]
        //[Authorize]
        [Route("api/quan-tri/group-function/SetFunctionForUser")]
        public IActionResult Set_Righ_4User_InGroup([FromQuery] decimal p_group_Id)
        {
            decimal success = -1;
            try
            {
                CSE_Group_FunctionDA _DA = new CSE_Group_FunctionDA();
                success = _DA.SetFunctionForUser(p_group_Id);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                success = -1;
            }
            return Json(new { success = success.ToString() });
        }

    }
}
