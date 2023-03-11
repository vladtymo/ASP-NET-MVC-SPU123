using Microsoft.AspNetCore.Mvc;
using SPU123_Shop_MVC.Models;
using System.Diagnostics;

namespace SPU123_Shop_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ...code...
            return View(); // ~/Home/Index.cshtml
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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