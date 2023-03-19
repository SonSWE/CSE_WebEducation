using CommonLib;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System.Data;
using System.Text.Json;

namespace CSE_WebEducation_Service.Controllers.Posts
{
    public class PostsController : Controller
    {
        [HttpGet]
        //[Authorize]
        [Route("api/trang-khoa/bai-viet/search")]
        public IActionResult Search(string user_name, string keySearch, int startRow, int endRow, string? orderBy)
        {
            try
            {
                CSE_PostsDA DA = new CSE_PostsDA();

                decimal totalRecord = 0;
                List<CSE_PostsInfo> _lst = new List<CSE_PostsInfo>();
                DataSet ds = DA.Search( keySearch, startRow, endRow, orderBy, ref totalRecord);
                _lst = CBO<CSE_PostsInfo>.FillCollection_FromDataSet(ds);

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
        [Route("api/trang-khoa/bai-viet/insert")]
        public IActionResult Insert([FromBody] CSE_PostsInfo info)
        {
            try
            {
                CSE_PostsDA DA = new CSE_PostsDA();
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
        [Route("api/trang-khoa/bai-viet/update")]
        public IActionResult Update([FromBody] CSE_PostsInfo info)
        {
            try
            {
                CSE_PostsDA DA = new CSE_PostsDA();
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
        [Route("api/trang-khoa/bai-viet/updateStatus")]
        public IActionResult UpdateStatus([FromBody] CSE_PostsInfo info)
        {
            try
            {
                CSE_PostsDA DA = new CSE_PostsDA();
                decimal result = DA.UpdateStatus(info);
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
        [Route("api/trang-khoa/bai-viet/delete")]
        public IActionResult Delete([FromBody] CSE_PostsInfo info)
        {
            try
            {
                CSE_PostsDA DA = new CSE_PostsDA();
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
        //[Authorize]
        [Route("api/trang-khoa/bai-viet/get-by-id")]
        public CSE_PostsInfo GetById([FromQuery] decimal id)
        {
            try
            {
                CSE_PostsDA DA = new CSE_PostsDA();
                DataSet ds = DA.GetById(id);

                return CBO<CSE_PostsInfo>.FillObjectFromDataSet(ds);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new CSE_PostsInfo();
            }
        }
    }
}
