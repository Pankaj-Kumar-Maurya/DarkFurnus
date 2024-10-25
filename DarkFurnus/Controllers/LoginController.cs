using DarkFurnus.Models;
using Microsoft.AspNetCore.Mvc;

namespace DarkFurnus.Controllers
{
    public class LoginController : Controller
    {
        private readonly DarkFurnusDbContext _context;
        public LoginController(DarkFurnusDbContext context)
        {
            this._context = context;
        }
        public IActionResult LoginView()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginVerify(string email, string password)
        {
            // Query the TblUserRegistration table to find a user by email
            var user = _context.TblUserRegistrations.SingleOrDefault(u => u.Email == email);

            if (user == null)
            {
                // User not found
                TempData["ToastrMessage"] = "Invalid email";
                return View("LoginView");
            }

            // Compare the provided password with the user's stored password
            if (user.Password != password)
            {
                // Invalid password
                TempData["ToastrMessage"] = "Invalid password.";
                return View("LoginView");
            }

            // If the email and password are valid, proceed (e.g., log in the user)
            TempData["Success"] = "Login successful!";
            return RedirectToAction("Index", "Home"); // Redirect to a different action after login
        }

    }
}
