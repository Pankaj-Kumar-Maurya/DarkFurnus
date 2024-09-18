using Microsoft.AspNetCore.Mvc;

namespace DarkFurnus.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginView()
        {
            return View();
        }
    }
}
