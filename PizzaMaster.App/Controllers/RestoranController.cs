using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using PizzaMaster.Domain.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PizzaMaster.App.Controllers
{
    public class RestoranController : Controller
    {
        private readonly INotyfService _notyfService;
        public RestoranController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            List<RestoranViewModel> list = new List<RestoranViewModel>();
            list.Add(new RestoranViewModel { Id = 1, RestoranIme = "Restoran 1" });
            _notyfService.Success("You have successfully saved the data.");
            _notyfService.Error("Exception...");
            _notyfService.Warning("Warning...");
            _notyfService.Information("Welcome to CoreSpider.", 5);
            return View(list);
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Imlnb3IiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjk5ODI3OTQ3fQ.mMVvc4FhHMzMaNm6fc3nGmkVYg3StGevDZCNbR-7GpI";

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    // Retrieve the role claim from the decoded token
                    var roleClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                    var expirationClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

                    if (roleClaim != null)
                    {
                        string role = roleClaim.Value;
                        string exp = expirationClaim?.Value;
                        // Now you have the role, and you can use it as needed

                        var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationClaim?.Value)).UtcDateTime;

                        // Check if the token is still valid
                        if (expirationDateTime > DateTime.UtcNow)
                        {
                            // Token is still valid
                            Console.WriteLine("Token is valid!");
                        }
                        else
                        {
                            // Token has expired
                            Console.WriteLine("Token has expired.");
                        }

                        if (role == "Admin")
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });

                        }
                        else if (role == "User")
                        {
                            return RedirectToAction("Index", "Home", new { area = "User" });
                        }
                    }
                }
            }

            return View();

          
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int obj)
        {

            if (ModelState.IsValid)
            {


              


                return RedirectToAction(nameof(Index));
            }
            _notyfService.Success("You have successfully saved the data.");
            _notyfService.Error("Exception...");
            _notyfService.Warning("Warning...");
            _notyfService.Information("Welcome to CoreSpider.", 5);
            return View(obj);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            return View();

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int obj)
        {
            

            if (ModelState.IsValid)
            {
               

                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }





        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Json(new { data = new List<int>() });
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();

          
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

          
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
