using DriversManagement.Models.Data.Entities;

namespace DriversManagement.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<User>> GetUsersByRoleId(int roleId, int pageIndex, int pageSize);
}