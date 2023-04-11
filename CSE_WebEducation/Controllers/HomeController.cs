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
            return View();
        }

        [Route("access-denied")]
        public IActionResult accessDenied()
        {
            return View("~/Views/Shared/None_Function.cshtml");
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
    }
}