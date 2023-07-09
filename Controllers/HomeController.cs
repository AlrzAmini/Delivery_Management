using DriversManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using DriversManagement.Models.Data.Entities;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Utilities;
using DriversManagement.Utilities.ExtensionMethods;

namespace DriversManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<User> _userGenericRepository;
        private readonly IPermissionService _permissionService;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<User> userGenericRepository, IPermissionService permissionService)
        {
            _logger = logger;
            _userGenericRepository = userGenericRepository;
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                var isAdmin = await _permissionService.IsUserAdmin(userId);

                return View(isAdmin);
            }

            return View(false);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}