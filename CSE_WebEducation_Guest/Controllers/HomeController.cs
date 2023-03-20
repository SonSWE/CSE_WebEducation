using CommonLib;
using CSE_WebEducation_Guest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectInfo;
using System.Diagnostics;

namespace CSE_WebEducation_Guest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        //trang chủ
        [Route("")]
        public IActionResult Index()
        {
            
            try
            {
                ViewBag.curTab = "HOME";

                //danh sách 6 bài viết mới nhất
                SearchResponseInfo _search = ApiClient_Posts.Search("||BAOCSE|A", "1", "6", "", "Created_By DESC");
                ViewBag.LstNews = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);

                //danh sách 6 bài viết mới nhất
                SearchResponseInfo _search2 = ApiClient_Posts.Search("||SUKIEN|A", "1", "10", "", "Start_Date DESC");
                ViewBag.LstEvent = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);
            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/Index.cshtml");
        }

        //đọc 1 bài viết
        [Route("bai-viet/{id}")]
        public IActionResult ViewPosts(decimal id)
        {
            try
            {
                CSE_PostsInfo info = ApiClient_Posts.GetById(id);
                ViewBag.PostInfo = info;

                CSE_Posts_CategoriesInfo Cateinfo = ApiClient_Posts_Category.GetById(info.Category_Id);
                ViewBag.PostCategoyInfo = Cateinfo;
                ViewBag.curTab = info.Category_Id;

                //danh sách 4 bài viết mới nhất
                SearchResponseInfo _search = ApiClient_Posts.Search("|||A", "1", "4", "", "Created_By DESC");
                ViewBag.LstFourPostsNew = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);

                //lấy danh sách 10 để sự kiện gợi ý 
                if (info.Category_Id == "SUKIEN")
                {
                    _search = ApiClient_Posts.Search("||SUKIEN|A", "1", "10", "", "Start_Date DESC");
                    ViewBag.LstTenEvent = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);
                }
            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/_Partial_Posts_View.cshtml");
        }

        #region tìm kiếm bài viết

        //Hiển thị khi vào 1 chức năng
        [Route("danh-sach-bai-viet/{id}")]
        public IActionResult LoadListPosts(string id)
        {
            try
            {
                CSE_Posts_CategoriesInfo info = ApiClient_Posts_Category.GetById(id);
                ViewBag.PostCategoyInfo = info;
                ViewBag.curTab = id;

                //danh sách 4 bài viết mới nhất
                SearchResponseInfo _search1 = ApiClient_Posts.Search("|||A", "1", "4", "", " ");
                ViewBag.LstFourPostsNew = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search1.jsondata);
            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/Display_Post_List.cshtml");
        }
       

        
        [Route("tim-kiem-bai-viet/{keysearch?}"), HttpGet]
        public IActionResult LoadSearchPost(string keysearch = "")
        {
            try
            {
                ViewBag.keysearch = keysearch;

                //danh sách 4 bài viết mới nhất
                SearchResponseInfo _search1 = ApiClient_Posts.Search("|||A", "1", "4", "", " ");
                ViewBag.LstFourPostsNew = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search1.jsondata);
            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/Search_Post.cshtml");
        }

        [Route("tim-kiem-bai-viet"), HttpPost]
        public IActionResult ListPostsSearch(string keysearch, int curentPage)
        {
            try
            {
                ViewBag.keyseach = keysearch;
                keysearch = keysearch + "||A";
                int p_to = 0;
                int p_from = CommonFunc.GetFromToPaging(curentPage, CommonData.RecordsPerPage, out p_to);
                SearchResponseInfo _search = ApiClient_Posts.Search(keysearch, p_from.ToString(), p_to.ToString(), " ");

                ViewBag.LstData = JsonConvert.DeserializeObject<List<CSE_PostsInfo>>(_search.jsondata);
                ViewBag.Paging = CommonFunc.PagingData(curentPage, CommonData.RecordsPerPage, (int)_search.totalrows);
            }
            catch (Exception ex)
            {

            }
            return View("~/Views/Home/_Partial_Posts_List.cshtml");
        }
        #endregion
    }
}