using DriversManagement.Models.Data.Entities;

namespace DriversManagement.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAll(int pageIndex,int pageSize);

    public Task<T?> GetById(int id);

    public Task<T> Insert(T entity);

    public Task Update(T entity);

    public Task<bool> Delete(T entity);
    public Task<bool> DeleteById(int id);

    public Task Save();
}