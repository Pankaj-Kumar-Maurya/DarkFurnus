using DarkFurnus.Models;
using Microsoft.AspNetCore.Mvc;

namespace DarkFurnus.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly DarkFurnusDbContext context;

        public UserRegistrationController(DarkFurnusDbContext context)
        {
            this.context = context;
        }

        // Displays the registration view
        public IActionResult RegistrationView()
        {
            // The message will display only if TempData["ToastrMessage"] is set by NewRegistration
            return View();
        }

        // Handles the form submission for a new registration
        [HttpPost]
        public IActionResult NewRegistration(TblUserRegistration tblUserRegistration)
        {
            try
            {
                if (tblUserRegistration != null)
                {
                    // Check if the email already exists in the database
                    var existingUser = context.TblUserRegistrations
                        .FirstOrDefault(u => u.Email == tblUserRegistration.Email);

                    if (existingUser != null)
                    {
                        // Email already exists, redirect to RegistrationView with an error message
                        TempData["ToastrMessage"] = "This Email already exists.";
                        TempData["ToastrType"] = "error";  // Types: success, error, warning, info
                        return RedirectToAction("RegistrationView");
                    }

                    // Add new user if the email is unique
                    context.TblUserRegistrations.Add(tblUserRegistration);
                    context.SaveChanges();

                    TempData["ToastrMessage"] = "Registration successful. Welcome!";
                    TempData["ToastrType"] = "success";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                TempData["ToastrMessage"] = "An error occurred during registration.";
                TempData["ToastrType"] = "error";
                return RedirectToAction("RegistrationView");
            }

            // If the model was null or empty, redirect back to the registration view
            return RedirectToAction("RegistrationView");
        }
    }
}
