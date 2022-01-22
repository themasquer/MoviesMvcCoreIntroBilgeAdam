using _038_MoviesMvcCoreIntroBilgeAdam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() // ~/Home/Index
        {
            //return View();
            return View("MyIndex");
        }

        public IActionResult Privacy() // ~/Home/Privacy
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() // ~/Home/Error
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}