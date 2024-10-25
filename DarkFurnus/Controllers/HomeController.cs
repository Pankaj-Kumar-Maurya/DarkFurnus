using DarkFurnus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DarkFurnus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(TempData["Registration"] != null && TempData["Registration"].ToString() == "Done")
            {
                TempData["ToastrMessage"] = "Registration Successful";
                TempData["ToastrType"] = "success";
            }
            return View();
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
