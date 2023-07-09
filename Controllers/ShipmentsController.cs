using DriversManagement.Models.Data.Entities;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace DriversManagement.Controllers
{
    public class ShipmentsController : BaseController
    {
        private readonly IGenericRepository<Shipment> _shipmentsRepository;
        private readonly IPermissionService _permissionService;

        public ShipmentsController(IGenericRepository<Shipment> shipmentsRepository, IPermissionService permissionService)
        {
            _shipmentsRepository = shipmentsRepository;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Index()
        {
            if (!await _permissionService.IsUserAdmin(User.GetUserId()))
            {
                var lstShipments = new List<Shipment>();
                return View(lstShipments);
            }

            var shipments = await _shipmentsRepository.GetAll(1, 200);
            return View(shipments);
        }
    }
}
