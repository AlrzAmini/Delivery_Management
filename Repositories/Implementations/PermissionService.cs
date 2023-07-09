using DriversManagement.Models.Data.Context;
using DriversManagement.Models.Data.Entities;
using DriversManagement.Models.Global;
using DriversManagement.Repositories.Interfaces;

namespace DriversManagement.Repositories.Implementations
{
    public class PermissionService : IPermissionService
    {
        private readonly DriversManagementDbContext _context;
        private readonly IGenericRepository<User> _userRepository;

        public PermissionService(DriversManagementDbContext context, IGenericRepository<User> userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<bool> IsUserAdmin(int userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user is null)
            {
                return false;
            }

            if (user.RoleId == StaticValues.AdminRoleId)
            {
                return true;
            }

            return false;
        }
    }
}
