using Microsoft.AspNetCore.Mvc;

namespace CSE_WebEducation.Controllers
{
    [Route("quan-tri")]
    public class ManagementController : Controller
    {
        [Route("trang-chu"), HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Management/Index.cshtml");
        }
    }
}
