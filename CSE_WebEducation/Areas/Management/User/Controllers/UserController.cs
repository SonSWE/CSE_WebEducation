using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectInfo;

namespace CSE_WebEducation.Areas.User.Controllers
{
    [Route("quan-tri/nguoi-dung")]
    public class UserController : Controller
    {
        [Route("danh-sach"), HttpGet]
        [CustomActionFilter]
        public IActionResult Index()
        {
            try
            {
                ViewBag.Record_On_Page = CommonData.RecordsPerPage;
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }

            return View("~/Areas/Management/User/Views/User/Index.cshtml");
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
                SearchResponseInfo _search = ApiClient_User.Search(user.Token, user.User_Name, keysearch, p_from.ToString(), p_to.ToString(), " ");

                ViewBag.LstData = JsonConvert.DeserializeObject<List<CSE_UsersInfo>>(_search.jsondata);
                ViewBag.Paging = CommonFunc.PagingData(curentPage, p_record_on_page, (int)_search.totalrows);
                ViewBag.Record_On_Page = p_record_on_page;
                ViewBag.UserType = user.User_Type;

                //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Tìm kiếm", $"Người dùng \"{user.User_Name}\" tìm kiếm thông tin nhóm người dùng", "Quản lý nhóm người sử dụng");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return PartialView("~/Areas/Management/User/Views/User/_Partial_List.cshtml");
        }

        [Route("chi-tiet"), HttpGet]
        [CustomActionFilter]
        public IActionResult ViewDetail(decimal user_id)
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                CSE_UsersInfo user_Info = ApiClient_User.GetById(user_id, user.Token);
                ViewBag.User_Info = user_Info;

                //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Xem thông tin", $"Người dùng \"{user.User_Name}\" xem thông tin nhóm người dùng. Tên nhóm NSD " + _Au_Group_Info.Group_Name, "Quản lý nhóm người sử dụng");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return View("~/Areas/Management/User/Views/User/_Partial_View.cshtml");
        }

        [Route("them-moi"), HttpGet]
        //[CustomActionFilter]
        public IActionResult Insert()
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                ViewBag.User_Type = user.User_Type;
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return View("~/Areas/Management/User/Views/User/_Partial_Insert.cshtml");
        }

        [Route("them-moi"), HttpPost]
        //[CustomActionFilter]
        public IActionResult Insert(CSE_UsersInfo info)
        {
            decimal _success = -1;
            string _str_error = "";
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                info.Created_By = user.User_Name;
                info.Created_Date = DateTime.Now;
                _success = ApiClient_User.Insert(info, user.Token);

                if (_success > 0)
                {
                    _str_error = "Thêm mới người dùng thành công!";
                }
                else if(_success == -2)
                {
                    _str_error = "Tên đăng nhập đã tồn tại trong hệ thống!";
                }
                else
                {
                    _str_error = "Thêm mới người dùng thất bại!";
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

        [Route("cap-nhat"), HttpGet]
        //[CustomActionFilter]
        public IActionResult Update(decimal user_id)
        {
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                CSE_UsersInfo info = ApiClient_User.GetById(user_id, user.Token);
                ViewBag.User_Info = info;
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return View("~/Areas/Management/User/Views/User/_Partial_Update.cshtml");
        }

        [Route("cap-nhat"), HttpPost]
        //[CustomActionFilter]
        public IActionResult Update(CSE_UsersInfo info)
        {
            decimal _success = -1;
            string _str_error = "";
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                info.Modified_By = user.User_Name;
                info.Modified_Date = DateTime.Now;

                _success = ApiClient_User.Update(info, user.Token);

                if (_success > 0)
                {
                    _str_error = "Chỉnh sửa người dùng thành công!";
                    //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Sửa", $"Người dùng \"{user.User_Name}\" sửa nhóm người dùng. Tên nhóm NSD " + info.Group_Name, "Người dùng");
                }
                else
                {
                    _str_error = "Chỉnh sửa người dùng thất bại!";
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

        [Route("cap-nhat-trang-thai"), HttpPost]
        //[CustomActionFilter]
        public IActionResult Update(decimal user_id, string status)
        {
            decimal _success = -1;
            string _str_error = "";
            try
            {
                var user = this.HttpContext.GetCurrentUser();

                CSE_UsersInfo info = new CSE_UsersInfo();

                info.User_Id = user_id;
                info.Status = status;
                info.Modified_By = user.User_Name;
                info.Modified_Date = DateTime.Now;

                _success = ApiClient_User.ActiveOrUnactive(info, user.Token);

                if (_success > 0)
                {
                    _str_error = "Cập nhật trạng thái người dùng thành công!";
                    //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Sửa", $"Người dùng \"{user.User_Name}\" sửa nhóm người dùng. Tên nhóm NSD " + info.Group_Name, "Người dùng");
                }
                else
                {
                    _str_error = "Cập nhật trạng thái người dùng thất bại!";
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
        public IActionResult Delete(decimal user_id)
        {
            decimal _id = -1;
            try
            {
                var user = this.HttpContext.GetCurrentUser();
                CSE_UsersInfo info = ApiClient_User.GetById(user_id, user.Token);

                CSE_UsersInfo _user_info = new CSE_UsersInfo();
                _user_info.User_Id = user_id;
                _user_info.Modified_By = user.User_Name;
                _user_info.Modified_Date = DateTime.Now;

                _id = ApiClient_User.Delete(_user_info, user.Token);

                //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Xóa", $"Người dùng \"{user.User_Name}\" xóa nhóm người dùng. Tên nhóm NSD " + info.Group_Name, "Người dùng");
            }
            catch (Exception ex)
            {
                Logger.nlog.Error(ex.ToString());
            }
            return Json(new { success = _id });
        }


        [HttpGet, Route("phan-quyen")]
        [CustomActionFilter]
        public IActionResult SetUpFunctionsInGroup(decimal userId)
        {
            var user = this.HttpContext.GetCurrentUser();
            var htmlTreeViewFunctionsInGroup = string.Empty;
            try
            {
                var info = ApiClient_User.GetById(userId, user.Token);
                ViewBag.User_Info = info;

                htmlTreeViewFunctionsInGroup = new ApiClient_User_Function().GetHtmlTreeViewFunctionsOfUser(userId, user.Token);

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }

            return PartialView("~/Areas/Management/User/Views/User/_Partial_Set_Functions.cshtml", htmlTreeViewFunctionsInGroup);
        }

        [HttpPost, Route("phan-quyen")]
        [CustomActionFilter]
        public IActionResult SetUpFunctionsInGroup(List<CSE_User_Function_Info> lstFunctionsOfUser, decimal userId)
        {
            var user = this.HttpContext.GetCurrentUser();
            decimal _success = -1;
            string _str_error = "Phân quyền cho người dùng thất bại";
            try
            {
                _success = ApiClient_User_Function.ResetFunction(userId, user.Token);

                if (_success > 0)
                {
                    _str_error = "Phân quyền cho người dùng thành công";

                    ApiClient_User_Function.UpdateFunction(lstFunctionsOfUser, user.Token);

                    //Api_TraceLog.Client_Log_Insert(this.HttpContext, "Phân quyền", $"Người dùng \"{user.User_Name}\" phân quyền nhóm \"{groupInfo.Group_Name}\"", "Nhóm người dùng");
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                _success = -1;
            }

            return Json(new
            {
                success = _success,
                responseMessage = _str_error
            });
        }
    }
}
