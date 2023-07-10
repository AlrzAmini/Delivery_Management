using DriversManagement.Models.Data.Context;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using DriversManagement.Models.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace DriversManagement.Controllers
{
    public class DeliveriesController : BaseController
    {
        private readonly DriversManagementDbContext _context;
        private readonly IPermissionService _permissionService;

        public DeliveriesController(DriversManagementDbContext context, IPermissionService permissionService)
        {
            _context = context;
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var take = 2000;
            var skip = (1 - 1) * take;

            if (await _permissionService.IsUserAdmin(User.GetUserId()))
            {

                var deliveries = await _context.Deliveries
                    .Include(d => d.Driver)
                    .Include(d => d.Shipment)
                    .Include(d => d.Car)
                    .Skip(skip)
                    .Take(take)
                    .OrderByDescending(d => d.Id)
                    .Select(d => new DeliveryDto(d.Driver.Name, d.Shipment.Title, d.DestinationAddress, d.Price))
                    .ToListAsync();

                return View(deliveries);
            }


            var userDeliveries = await _context.Deliveries
                .Where(d => d.DriverId == User.GetUserId())
                .Include(d => d.Driver)
                .Include(d => d.Shipment)
                .Include(d => d.Car)
                .Skip(skip)
                .Take(take)
                .OrderByDescending(d => d.Id)
                .Select(d => new DeliveryDto(d.Driver.Name, d.Shipment.Title, d.DestinationAddress, d.Price))
                .ToListAsync();

            return View(userDeliveries);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new LoadCreateDeliveryModel
            {
                Drivers = await _context.Users.Where(s => s.RoleId == 2).OrderByDescending(s => s.Id).ToListAsync(),
                Cars = await _context.Car.OrderByDescending(s => s.Id).ToListAsync(),
                Shipments = await _context.Shipments.OrderByDescending(s => s.Id).ToListAsync()
            };

            var drivers = new List<SelectListItem>();
            foreach (var driver in model.Drivers)
            {
                drivers.Add(new SelectListItem()
                {
                    Text = driver.Name,
                    Value = driver.Id.ToString()
                });
            }

            var cars = new List<SelectListItem>();
            foreach (var car in model.Cars)
            {
                cars.Add(new SelectListItem()
                {
                    Text = car.Model,
                    Value = car.Id.ToString()
                });
            }

            var shipments = new List<SelectListItem>();
            foreach (var shipment in model.Shipments)
            {
                shipments.Add(new SelectListItem()
                {
                    Text = shipment.Title,
                    Value = shipment.Id.ToString()
                });
            }

            ViewBag.Drivers = drivers;
            ViewBag.Cars = cars;
            ViewBag.Shipments = shipments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Delivery delivery)
        {
            try
            {
                var entryDelivery = await _context.Deliveries.AddAsync(delivery);
                await _context.SaveChangesAsync();
                return Ok(entryDelivery.Entity);
            }
            catch (Exception e)
            {
                return Content(e.Message + " + " + e.InnerException);
            }
        }
    }

    public record DeliveryDto(string? DriverName, string? ShipmentTitle, string? Address, int Price);

    public class LoadCreateDeliveryModel
    {
        public List<User> Drivers { get; set; } = new List<User>();
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
