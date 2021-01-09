using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ToDoListDataService.Data.Repository.BaseRepository.Impl
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            this._context = context;
        }

        public EntityEntry<T> AddWithoutSave(T entity)
        {
            return _context.Add(entity);
        }

        public EntityEntry<T> UpdateWithoutSave(T entity)
        {
            return _context.Update(entity);
        }

        public EntityEntry<T> DeleteWithoutSave(T entity)
        {
            return _context.Remove(entity);
        }


        public T Add(T entity)
        {
            var result = _context.Entry(entity);

            result.State = EntityState.Added;

            _context.SaveChanges();

            return result.Entity;
        }

        public T Update(T entity)
        {
            var result = _context.Entry(entity);

            result.State = EntityState.Modified;

            _context.SaveChanges();

            return result.Entity;
        }
        
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        
        public bool Delete(T entity)
        {
            var result = _context.Set<T>().Remove(entity);

            result.State = EntityState.Deleted;

            _context.SaveChanges();

            return true;
        }

        public IQueryable<T> All()
        {
            return this._context.Set<T>();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Where(where);
        }

        public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
        {
            return isDesc ? _context.Set<T>().OrderByDescending(orderBy) : _context.Set<T>().OrderBy(orderBy);
        }

        public bool Save()
        {
            return this._context.SaveChanges() > -1;
        }

        public Task<int> SaveAsync()
        {
            return this._context.SaveChangesAsync();
        }

        public async Task<bool> AddAsync(T entity)
        {
            _context.Add(entity);
            return await SaveAsync() > -1;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            return await SaveAsync() > -1;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);
            return await SaveAsync() > -1;
        }
    }
}