using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ToDoListDataService.Data.Repository.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T entity);
        bool Delete(T entity);
        T Update(T entity);
        T Get(int id);
        EntityEntry<T> AddWithoutSave(T entity);
        EntityEntry<T> UpdateWithoutSave(T entity);
        EntityEntry<T> DeleteWithoutSave(T entity);
        IQueryable<T> All();
        IQueryable<T> Where(Expression<Func<T, bool>> where);
        IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc);
        bool Save();
        Task<int> SaveAsync();
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}