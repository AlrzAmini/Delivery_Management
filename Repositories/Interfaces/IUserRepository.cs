using DriversManagement.Models.Data.Entities;
using DriversManagement.Models.DTOs.Account;
using DriversManagement.Models.DTOs.User;

namespace DriversManagement.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<User>> GetUsersByRoleId(int roleId, int pageIndex = 1, int pageSize = 10);

    public Task<List<UserForAdminDto>> GetUsersForAdmin(int pageIndex = 1, int pageSize = 10);

    public Task<bool> IsUserExists_ByUserName(string userName);
    public Task<bool> IsUserExists_ByMobile(string mobile);

    public Task<bool> CheckUserForLogin(LoginDto loginDto);

    public Task<int?> GetUserIdByMobile(string mobile);
}