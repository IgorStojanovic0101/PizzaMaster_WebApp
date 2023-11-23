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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            var requestDTO = new UserLoginRequestDTO { Username = model.Username, Password = model.Password };

           var response = this._accountService.Login(requestDTO);

            if (!response.Validation)
            {


               var tokenStored = this._accountService.StoreToken(response.Payload.Token);
                if(!tokenStored)
                {
                    _notyfService.Warning("Token was expired");
                    return View(model);
                }

               
                var role = this._accountService.GetRole(response.Payload.Token);

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

        public IActionResult LogOut(string returnUrl = null)
        {
            return RedirectToAction(nameof(Login), "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var requestDTO = new UserRegisterRequestDTO { Username = model.Username, Password = model.Password, Email = model.Email, Name = model.FullName };

            var response = this._accountService.Register(requestDTO);

            if (response.Validation is true ) {

                _notyfService.Warning($"Warning: {string.Join("\n- ", response.Errors)}");
                return View(model);
            }

            var tokenStored = this._accountService.StoreToken(response.Payload.Token);
            if (!tokenStored)
            {
                _notyfService.Warning("Token was expired");
                return View(model);
            }


            var role = this._accountService.GetRole(response.Payload.Token);

            if (role == "Admin")
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            else if (role == "User")
            {
                return RedirectToAction("Index", "Home", new { area = "User" });
            }


            return RedirectToAction("Index", "Home");
        }
    }
}
