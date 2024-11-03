using DarkFurnus.Models;
using Microsoft.AspNetCore.Mvc;

namespace DarkFurnus.Controllers
{
    public class LoginController : Controller
    {
        private readonly DarkFurnusDbContext _context;
        private bool isRememberMeChecked;

        public LoginController(DarkFurnusDbContext context)
        {
            this._context = context;
        }
        public IActionResult LoginView()
        {
            // Retrieve email and password from cookies if they exist
            ViewBag.Email = Request.Cookies["email"];
            ViewBag.Password = Request.Cookies["password"];
            return View();
        }
        [HttpPost]
        public IActionResult LoginVerify(string email, string password, bool isRememberMeChecked)
        {
            // Query the TblUserRegistration table to find a user by email
            var user = _context.TblUserRegistrations.SingleOrDefault(u => u.Email == email);

            if (user == null)
            {
                // User not found
                TempData["ToastrType"] = "error";
                TempData["ToastrMessage"] = "No user fonded with this email";
                return RedirectToAction("LoginView");
            }

            // Compare the provided password with the user's stored password
            if (user.Password != password)
            {
                // Invalid password
                TempData["ToastrType"] = "error";
                TempData["ToastrMessage"] = "Wrong password.";
                return RedirectToAction("LoginView");
            }
            //Set cookies if "Remember Me" is checked
            if (isRememberMeChecked)
            {
                Response.Cookies.Append("email", email, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
                Response.Cookies.Append("password", password, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            }
            else
            {
                // Clear cookies if "Remember Me" is unchecked
                Response.Cookies.Delete("email");
                Response.Cookies.Delete("password");
            }

            // If the email and password are valid, proceed (e.g., log in the user)
            TempData["Success"] = "Login successful!";
            return RedirectToAction("Index", "Home"); // Redirect to a different action after login
        }

    }
}
