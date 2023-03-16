using CommonLib;
using CSE_WebEducation.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectInfo;
using System.Diagnostics;

namespace CSE_WebEducation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[CustomActionFilter(CheckRight = false)]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.curTab = "HOME";
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("dang-nhap")]
        public IActionResult Load_Login()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/Login.cshtml");
        }

        [Route("bai-viet/{id}")]
        public IActionResult ViewPosts(decimal id)
        {
            try
            {
                CSE_PostsInfo  info = ApiClient_Posts.GetById(id);
                ViewBag.PostInfo = info;

                //danh sách 4 bài viết mới nhất
                SearchResponseInfo _search = ApiClient_Posts.Search("|||", "1", "4", "", "Created_By DESC");
                ViewBag.LstFourPostsNew = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);

                //lấy danh sách 10 để sự kiện gợi ý 
                if (info.Post_Type == CSE_Post_Type.events)
                {
                    _search = ApiClient_Posts.Search("|||2", "1", "10", "", "Start_Date DESC");
                    ViewBag.LstTenEvent = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);
                }    
            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/_Partial_Posts_View.cshtml");
        }

        [Route("danh-sach-bai-viet/{id}")]
        public IActionResult ListPosts(string id)
        {
            try
            {
                int p_to = 0;
                int p_from = CommonFunc.GetFromToPaging(1, CommonData.RecordsPerPage, out p_to);
                SearchResponseInfo _search = ApiClient_Posts.Search("||" + id, p_from.ToString(), p_to.ToString(), " ");

                ViewBag.LstData = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);
                ViewBag.Paging = CommonFunc.PagingData(1, CommonData.RecordsPerPage, (int)_search.totalrows);
                ViewBag.curTab = id;

                //danh sách 4 bài viết mới nhất
                SearchResponseInfo _search1 = ApiClient_Posts.Search("|||", "1", "4", "", " ");
                ViewBag.LstFourPostsNew = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search1.jsondata);
            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/_Partial_Posts_List.cshtml");
        }
    }
}