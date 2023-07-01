﻿using DriversManagement.Models.Data.Entities;
using DriversManagement.Models.DTOs.User;
using DriversManagement.Models.Global;
using DriversManagement.Repositories.Interfaces;
using DriversManagement.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DriversManagement.Controllers
{
    public class UsersController : Controller
    {
        #region ctor

        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _userGenericRepository;

        public UsersController(IUserRepository userRepository, IGenericRepository<User> userGenericRepository)
        {
            _userRepository = userRepository;
            _userGenericRepository = userGenericRepository;
        }

        #endregion

        public IActionResult Index()
        {
            return Ok("Users");
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto userDto)
        {
            CreateUserValidator validator = new CreateUserValidator();
            ValidationResult results = await validator.ValidateAsync(userDto);

            if (!results.IsValid)
            {
                var errors = new List<string>();
                foreach (var failure in results.Errors)
                {
                    errors.Add("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }

                return BadRequest(errors);
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
    }
}
