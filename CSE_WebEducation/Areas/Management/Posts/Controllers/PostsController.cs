using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectInfo;

namespace CSE_WebEducation.Areas.Management.Posts.Controllers
{
    [Route("trang-khoa/bai-viet")]
    public class PostsController : Controller
    {
        [Route("danh-sach"), HttpGet]
        [CustomActionFilter]
        public IActionResult Index()
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();

                ViewBag.Record_On_Page = CommonData.RecordsPerPage;

                ViewBag.LstCategory = ApiClient_Posts_Category.GetAll(user.Token);  
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }

            return View("~/Areas/Management/Posts/Views/Index.cshtml");
        }

        [Route("tim-kiem"), HttpPost]
        [CustomActionFilter]
        public IActionResult Search(string keysearch, int curentPage, int p_record_on_page)
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                p_record_on_page = p_record_on_page != 0 ? p_record_on_page : CommonData.RecordsPerPage;
                int p_to = 0;
                int p_from = CommonFunc.GetFromToPaging(curentPage, p_record_on_page, out p_to);
                SearchResponseInfo _search = ApiClient_Posts.Search( keysearch, p_from.ToString(), p_to.ToString(), user.Token, " ");

                ViewBag.LstData = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);
                ViewBag.Paging = CommonFunc.PagingData(curentPage, p_record_on_page, (int)_search.totalrows);
                ViewBag.Record_On_Page = p_record_on_page;
                //ViewBag.UserType = user.User_Type;

                //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Tìm kiếm", $"Người dùng \"{user.User_Name}\" tìm kiếm thông tin bài viết", "Quản lý nhóm người sử dụng");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return PartialView("~/Areas/Management/Posts/Views/_Partial_List.cshtml");
        }

        [Route("chi-tiet/{id}"), HttpGet]
        //[CustomActionFilter]
        public IActionResult ViewDetail(decimal id)
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                CSE_PostsInfo info = ApiClient_Posts.GetById(id, user.Token);
                ViewBag.Info = info;

                //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Xem thông tin", $"Người dùng \"{user.User_Name}\" xem thông tin bài viết. Tên nhóm NSD " + _Au_Group_Info.Group_Name, "Quản lý nhóm người sử dụng");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return View("~/Areas/Management/Posts/Views/_Partial_View.cshtml");
        }

        [Route("them-moi"), HttpGet]
        //[CustomActionFilter]
        public IActionResult Insert()
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                //ViewBag.User_Type = user.User_Type;

                ViewBag.LstCategory = ApiClient_Posts_Category.GetAll(user.Token);
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return View("~/Areas/Management/Posts/Views/_Partial_Insert.cshtml");
        }

        [Route("them-moi"), HttpPost]
        //[CustomActionFilter]
        public IActionResult Insert(CSE_PostsInfo info)
        {
            decimal _success = -1;
            string _str_error = "";
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                info.Created_By = user.User_Name;
                info.Created_Date = DateTime.Now;

                var formFiles = this.HttpContext.Request.Form.Files;
                if (formFiles != null && formFiles.Count > 0)
                {
                    IFormFile file = formFiles.FirstOrDefault(x => x.Name.Equals("file_Content"));
                    if (file != null)
                    {
                        string base_url = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
                        string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssFFF")}{CommonFunc.ReplaceUnicodeString(file.FileName)}";
                        string url_full_url_file = Path.Combine(CommonData.FileAttach, fileName);

                        using (var save_file = new FileStream(url_full_url_file, FileMode.Create))
                        {
                            file.CopyTo(save_file);
                        }

                        info.Thumbnail = base_url + "/file_attach/" + fileName;
                    }
                }
                

                _success = ApiClient_Posts.Insert(info, user.Token);

                if (_success > 0)
                {
                    _str_error = "Thêm mới bài viết thành công!";
                }
                else
                {
                    _str_error = "Thêm mới bài viết thất bại!";
                }
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return Json(new
            {
                success = _success,
                responseMessage = _str_error
            });
        }

        [Route("cap-nhat/{id}"), HttpGet]
        //[CustomActionFilter]
        public IActionResult Update(decimal id)
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                CSE_PostsInfo info = ApiClient_Posts.GetById(id, user.Token);
                ViewBag.Info = info;

                ViewBag.LstCategory = ApiClient_Posts_Category.GetAll(user.Token);
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return View("~/Areas/Management/Posts/Views/_Partial_Update.cshtml");
        }

        [Route("cap-nhat"), HttpPost]
        //[CustomActionFilter]
        public IActionResult Update(CSE_PostsInfo info)
        {
            decimal _success = -1;
            string _str_error = "";
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                info.Modified_By = user.User_Name;
                info.Modified_Date = DateTime.Now;

                var formFiles = this.HttpContext.Request.Form.Files;
                if (formFiles != null && formFiles.Count > 0)
                {
                    IFormFile file = formFiles.FirstOrDefault(x => x.Name.Equals("file_Content"));
                    if (file != null)
                    {
                        string base_url = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
                        string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssFFF")}{CommonFunc.ReplaceUnicodeString(file.FileName)}";
                        string url_full_url_file = Path.Combine(CommonData.FileAttach, fileName);

                        using (var save_file = new FileStream(url_full_url_file, FileMode.Create))
                        {
                            file.CopyTo(save_file);
                        }

                        info.Thumbnail = base_url + "/file_attach/" + fileName;
                    }
                }

                _success = ApiClient_Posts.Update(info, user.Token);

                if (_success > 0)
                {
                    _str_error = "Chỉnh sửa bài viết thành công!";
                    //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Sửa", $"Người dùng \"{user.User_Name}\" sửa bài viết. Tên nhóm NSD " + info.Group_Name, "Người dùng");
                }
                else
                {
                    _str_error = "Chỉnh sửa bài viết thất bại!";
                }
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return Json(new
            {
                success = _success,
                responseMessage = _str_error
            });
        }

        [Route("xoa"), HttpPost]
        //[CustomActionFilter]
        public IActionResult Delete(decimal id)
        {
            decimal _id = -1;
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                CSE_PostsInfo info = ApiClient_Posts.GetById(id, user.Token);

                CSE_PostsInfo Info = new CSE_PostsInfo();
                Info.Id = id;
                Info.Modified_By = user.User_Name;
                Info.Modified_Date = DateTime.Now;

                _id = ApiClient_Posts.Delete(Info, user.Token);

                //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Xóa", $"Người dùng \"{user.User_Name}\" xóa bài viết. Tên nhóm NSD " + info.Group_Name, "Người dùng");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return Json(new { success = _id });
        }

        [Route("cap-nhat-trang-thai"), HttpPost]
        //[CustomActionFilter]
        public IActionResult ShowOrHide(decimal id, string status)
        {
            decimal _id = -1;
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                CSE_PostsInfo info = ApiClient_Posts.GetById(id, user.Token);

                CSE_PostsInfo Info = new CSE_PostsInfo();
                Info.Id = id;
                Info.Status = status;
                Info.Modified_By = user.User_Name;
                Info.Modified_Date = DateTime.Now;

                _id = ApiClient_Posts.UpdateStatus(Info, user.Token);

                //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Xóa", $"Người dùng \"{user.User_Name}\" xóa bài viết. Tên nhóm NSD " + info.Group_Name, "Người dùng");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return Json(new { success = _id });
        }
    }
}
