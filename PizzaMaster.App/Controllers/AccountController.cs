using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaMaster.Application.Services;
using PizzaMaster.Domain.ViewModels;
using PizzaMaster.Infrastructure.System;
using PizzaMaster.Shared.DTOs.User;
using PizzaMaster.Shared.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PizzaMaster.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IAccountService _accountService;

        public AccountController(INotyfService notyfService, IAccountService accountService)
        {
            this._notyfService = notyfService;
            this._accountService = accountService;
        }
 

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var requestDTO = new UserLoginRequestDTO { Username = model.Username, Password = model.Password };

           var response = this._accountService.Login(requestDTO);

            if (!response.Validation)
            {

                //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Imlnb3IiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjk5ODI3OTQ3fQ.mMVvc4FhHMzMaNm6fc3nGmkVYg3StGevDZCNbR-7GpI";


               var tokenStored = this._accountService.StoreToken(response.Payload);
                if(!tokenStored)
                {
                    _notyfService.Warning("Token was expired");
                    return View(model);
                }

               
                var role = this._accountService.GetRole(response.Payload);

                if (role == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });

                }
                else if (role == "User")
                {
                    return RedirectToAction("Index", "Home", new { area = "User" });
                }
                
            }
            else
            {
                _notyfService.Warning($"Warning: {string.Join("\n- ", response.Errors)}");
                return View(model);

            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
