using DriversManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DriversManagement.Models.Data.Entities;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Utilities;

namespace DriversManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<User> _userGenericRepository;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<User> userGenericRepository)
        {
            _logger = logger;
            _userGenericRepository = userGenericRepository;
        }

        public IActionResult Index()
        {
            return View();
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