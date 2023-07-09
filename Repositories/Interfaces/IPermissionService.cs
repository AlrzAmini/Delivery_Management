namespace DriversManagement.Repositories.Interfaces;

public interface IPermissionService
{
    public Task<bool> IsUserAdmin(int userId);
}