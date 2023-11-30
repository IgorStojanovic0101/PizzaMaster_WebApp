using Microsoft.AspNetCore.Mvc;
using PizzaMaster.Application.Services;
using PizzaMaster.Domain.ViewModels;
using System.Diagnostics;

namespace PizzaMaster.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminService _adminService;

        public HomeController(ILogger<HomeController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            var viewModel = new AdminViewModel(); // Initialize your model here

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new PizzaMaster.Domain.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult UploadFile1(IFormFile file1, AdminViewModel model)
        {
            if (file1 != null && file1.Length > 0)
            {

                model.Image2 = $"data:image/png;base64, {ConvertImageToBase64(file1)}";
            }

            _adminService.SetAdminData(new Shared.DTOs.Admin.Admin_RequestDTO { File = file1, Text = "123" });


            return View("Index", model);
        }
        private string ConvertImageToBase64(IFormFile imageFile)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Copy the content of the image file to the memory stream
                    imageFile.CopyTo(ms);

                    // Convert the byte array to a Base64 string
                    string base64String = Convert.ToBase64String(ms.ToArray());

                    return base64String;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log the error, and return an empty string or throw an exception based on your requirements
                return string.Empty;
            }
        }

        [HttpPost]
        public IActionResult UploadFile2(IFormFile file2, AdminViewModel model)
        {
            if (file2 != null && file2.Length > 0)
            {
                // Process the file and save it
                // Update the Image2 property in the model with the path to the uploaded image
                model.Image2 = "/path/to/uploaded/image2.jpg"; // Change this to the actual path
            }

            return View("_PartialImageView", model);
        }
    }
}