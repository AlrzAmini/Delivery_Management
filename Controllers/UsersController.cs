using DriversManagement.Models.Data.Entities;
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
        public async Task<IActionResult> CreateUser(User user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = await validator.ValidateAsync(user);

            if (!results.IsValid)
            {
                var errors = new List<string>();
                foreach (var failure in results.Errors)
                {
                    errors.Add("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }

                return BadRequest(errors);
            }

            var insertedUser = await _userGenericRepository.Insert(user);
            return Ok(insertedUser);
        }
    }
}
