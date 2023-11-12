using Microsoft.AspNetCore.Mvc;
using PizzaMaster.Domain.ViewModels;

namespace PizzaMaster.App.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // Check username and password (you should implement your authentication logic here)

            if (ModelState.IsValid)
            {
                // Authentication logic (e.g., check credentials, generate and store tokens)

                // Redirect to home page after successful login
                return RedirectToAction("Index", "Home");
            }

            // If login fails, return to the login page with validation errors
            return View(model);
        }
    }
}
