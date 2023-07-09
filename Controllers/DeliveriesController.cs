using DriversManagement.Models.Data.Context;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

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
    }

    public record DeliveryDto(string? DriverName, string? ShipmentTitle, string? Address, int Price);
}
