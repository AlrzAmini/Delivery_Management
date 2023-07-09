using DriversManagement.Models.Data.Context;
using DriversManagement.Models.Data.Entities;
using DriversManagement.Models.DTOs.Account;
using DriversManagement.Models.DTOs.User;
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

        public async Task<List<UserForAdminDto>> GetUsersForAdmin(int pageIndex = 1, int pageSize = 10)
        {
            var skip = (pageIndex - 1) * pageSize;
            var users = await _context.Users
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new UserForAdminDto(u.Id, u.Name, u.Mobile, u.Password))
                .ToListAsync();

            return users;
        }

        public async Task<bool> IsUserExists_ByMobile(string mobile)
        {
            return await _context.Users.AnyAsync(u => u.Mobile == mobile);
        }

        public async Task<bool> CheckUserForLogin(LoginDto loginDto)
        {
            return await _context.Users.AnyAsync(u=>u.Mobile == loginDto.Mobile && u.Password == loginDto.Password);
        }

        public async Task<bool> IsUserExists_ByUserName(string userName)
        {
            return await _context.Users.AnyAsync(u => u.Name == userName);
        }

        public async Task<int?> GetUserIdByMobile(string mobile)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile);
            return user?.Id;
        }
    }
}
