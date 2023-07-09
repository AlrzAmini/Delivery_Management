using System.Security.Claims;
using DriversManagement.Models.Data.Entities;
using DriversManagement.Models.DTOs.Account;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DriversManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var isAuth = User.Identity?.IsAuthenticated;
            if (isAuth is null)
            {
                return View();
            }

            if (isAuth.Value)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validator = new LoginValidator();
            var validationResult = await validator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return View(loginDto);
            }

            if (!await _userRepository.CheckUserForLogin(loginDto))
            {
                return Content("User does not exists or password does not match");
            }

            var userId = await _userRepository.GetUserIdByMobile(loginDto.Mobile);
            if (userId is null)
            {
                return Content("user not found");
            }

            var claims = new List<Claim>()
            {
                new(ClaimTypes.MobilePhone,loginDto.Mobile),
                new(ClaimTypes.NameIdentifier,userId.Value.ToString()),
            };

            // Config Identity
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };

            // Logging user
            await HttpContext.SignInAsync(principal, properties);

            return RedirectToAction("Index","Home");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
