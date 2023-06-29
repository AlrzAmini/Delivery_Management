using DriversManagement.Models.Data.Context;
using DriversManagement.Models.Data.Entities;
using DriversManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DriversManagement.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DriversManagementDbContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(DriversManagementDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<List<T>> GetAll(int pageIndex, int pageSize)
        {
            var skip = (pageIndex - 1) * pageSize;
            return await _entities.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            var add = _entities.AddAsync(entity).AsTask();
            var save = Save();

            await Task.WhenAll(add, save);
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await Save();
        }

        public async Task<bool> Delete(T entity)
        {
            entity.IsDeleted = true;
            await Update(entity);
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }

            return await Delete(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

