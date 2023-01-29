using ErsaCase.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Core.Repository.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties); // var kullanici = repository.GetAsync(k=>k.Id==15);
        Task<T?> GetAsyncNoTrack(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task DetachAsync(T entity);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();

        //Select sorgusu için kullanılacak method.
        IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAllWithThenInclude();
    }
}
