using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickAnIcon.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PickAnIcon.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PickAnIcon.Controllers
{
    public class AccountController : Controller
    {
        private protected ILogger _logger;
        private protected IConfiguration _configuration;
        private protected IUsersService _usersService;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration, IUsersService usersService)
        {
            _logger = logger;
            _configuration = configuration;
            _usersService = usersService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "username and password is required";
                return View(model);
            }

            var result = _usersService.Login(model.Username, model.Password);

            if (result.HasErrors)
            {
                ViewBag.Error = result.ErrorMessage;
                return View(model);
            }

            var userIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim("authtest://claims/login-time", DateTime.Now.ToString())
            }, "login");

            await HttpContext.SignInAsync(new ClaimsPrincipal(userIdentity),
                new AuthenticationProperties() { IsPersistent = false });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignInViewModel model)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "username and password is required";
                return View(model);
            }

            var result = _usersService.Register(model.Username, model.Password);
            if (result.HasErrors)
            {
                ViewBag.Error = result.ErrorMessage;
                return View(model);
            }

            _logger.LogDebug("New user registered, username {0}", model.Username);

            var userIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim("authtest://claims/login-time", DateTime.Now.ToString())
            }, "login");

            await HttpContext.SignInAsync(new ClaimsPrincipal(userIdentity),
                new AuthenticationProperties() { IsPersistent = false });
           
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignOut()  
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Panel()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account");
            }

            var model = new PanelViewModel();
            model.Username = HttpContext.User.Identity.Name;
            model.IconsCount = _usersService.GetByName(HttpContext.User.Identity.Name).Value.Icons.Count;

            return View(model);
        }
    }
}
