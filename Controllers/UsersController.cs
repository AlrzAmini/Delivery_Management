using DriversManagement.Models.Data.Entities;
using DriversManagement.Models.DTOs.User;
using DriversManagement.Models.Global;
using DriversManagement.Repositories.Implementations;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Utilities.ExtensionMethods;
using DriversManagement.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DriversManagement.Controllers
{
    public class UsersController : BaseController
    {
        #region ctor

        private readonly IUserRepository _userRepository;
        private readonly IPermissionService _permissionService;
        private readonly IGenericRepository<User> _userGenericRepository;

        public UsersController(IUserRepository userRepository, IPermissionService permissionService, IGenericRepository<User> userGenericRepository)
        {
            _userRepository = userRepository;
            _permissionService = permissionService;
            _userGenericRepository = userGenericRepository;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!await _permissionService.IsUserAdmin(User.GetUserId()))
            {
                var lstShipments = new List<UserForAdminDto>();
                return View(lstShipments);
            }

            var users = await _userRepository.GetUsersForAdmin(1,1000);
            return View(users);
        }

        #region create

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            if (!await _permissionService.IsUserAdmin(User.GetUserId()))
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto userDto)
        {
            if (!await _permissionService.IsUserAdmin(User.GetUserId()))
            {
                return BadRequest();
            }

            var validator = new CreateUserValidator();
            var results = await validator.ValidateAsync(userDto);
            
            if (!results.IsValid)
            {
                return View(userDto);
            }

            var isUserExists = await _userRepository.IsUserExists_ByMobile(userDto.Mobile);
            if (isUserExists)
            {
                return BadRequest("User already exists");
            }

            isUserExists = await _userRepository.IsUserExists_ByUserName(userDto.Name);
            if (isUserExists)
            {
                return BadRequest("User already exists");
            }

            var user = new User()
            {
                RoleId = StaticValues.AdminRoleId,
                Name = userDto.Name,
                Mobile = userDto.Mobile,
                Password = userDto.Password
            };

            var insertedUser = await _userGenericRepository.Insert(user);
            return Ok(insertedUser);
        }

        #endregion

        #region edit



        #endregion

        #region delete

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!await _permissionService.IsUserAdmin(User.GetUserId()))
            {
                return BadRequest();
            }

            var deletedSuccessfully = await _userGenericRepository.DeleteById(userId);

            if (deletedSuccessfully)
            {
                return RedirectToAction("Index");
            }

            return Content("something went wrong");
        }

        #endregion
    }
}


