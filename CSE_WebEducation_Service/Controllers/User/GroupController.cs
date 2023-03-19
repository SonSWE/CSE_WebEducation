using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System.Data;
using System.Text.Json;

namespace CSE_WebEducation_Service.Controllers.User
{
    [Route("api")]
    public class GroupController : Controller
    {
        [HttpGet]
        [Authorize]
        [Route("quan-tri/group/search")]
        public IActionResult Search(string user_name, string keySearch, int startRow, int endRow, string? orderBy)
        {
            try
            {
                CSE_GroupDA DA = new CSE_GroupDA();

                decimal totalRecord = 0;
                List<CSE_GroupsInfo> _lst = new List<CSE_GroupsInfo>();
                DataSet ds = DA.Search(user_name, keySearch, startRow, endRow, orderBy, ref totalRecord);
                _lst = CBO<CSE_GroupsInfo>.FillCollection_FromDataSet(ds);

                return Json(new { totalRows = totalRecord, jsonData = JsonSerializer.Serialize(_lst) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { totalRows = "-1", jsonData = "" }); ;
            }
        }

        [HttpPost]
        [Authorize]
        [Route("quan-tri/group/insert")]
        public IActionResult Insert([FromBody] CSE_GroupsInfo info)
        {
            try
            {
                CSE_GroupDA DA = new CSE_GroupDA(); 
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
        [Route("quan-tri/group/update")]
        public IActionResult Update([FromBody] CSE_GroupsInfo info)
        {
            try
            {
                CSE_GroupDA DA = new CSE_GroupDA();
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
        [Route("quan-tri/group/activeOrUnactive")]
        public IActionResult ActiveOrUnactive([FromBody] CSE_GroupsInfo info)
        {
            try
            {
                CSE_GroupDA DA = new CSE_GroupDA();
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
        [Route("quan-tri/group/delete")]
        public IActionResult Delete([FromBody] CSE_GroupsInfo info)
        {
            try
            {
                CSE_GroupDA DA = new CSE_GroupDA();
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
        [Route("quan-tri/group/get-by-id")]
        public CSE_GroupsInfo GetById([FromQuery] decimal id)
        {
            try
            {
                CSE_GroupDA DA = new CSE_GroupDA();
                DataSet ds = DA.GetById(id);

                return CBO<CSE_GroupsInfo>.FillObjectFromDataSet(ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new CSE_GroupsInfo();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/quan-tri/group/get-all")]
        public List<CSE_GroupsInfo> GetAll()
        {
            try
            {
                CSE_GroupDA DA = new CSE_GroupDA();
                DataSet _ds = DA.GetAll();

                return CBO<CSE_GroupsInfo>.FillCollection_FromDataSet(_ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new List<CSE_GroupsInfo>();
            }
        }

    }
}
