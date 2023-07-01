using DriversManagement.Models.Data.Context;
using DriversManagement.Models.Data.Entities;
using DriversManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DriversManagement.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DriversManagementDbContext _context;

        public UserRepository(DriversManagementDbContext context)
        {
            _context = context;
        }


        public async Task<List<User>> GetUsersByRoleId(int roleId, int pageIndex = 1, int pageSize = 10)
        {
            var skip = (pageIndex - 1) * pageSize;
            var users = await _context.Users
                .Where(u => u.RoleId == roleId)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return users;
        }
    }
}
