using Microsoft.AspNetCore.Mvc;

namespace DarkFurnus.Controllers
{
    public class UserRegistrationController : Controller
    {
        public IActionResult RegistrationView()
        {
            return View();
        }
    }
}
