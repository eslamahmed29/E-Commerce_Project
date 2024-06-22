using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(T entity);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC");
        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        Task<IQueryable<T>> SelectAsync(Expression<Func<T, bool>> expression);
        Task<bool?> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T?> FirstOrDefaultASync(Expression<Func<T, bool>> expression);

     // ================================= without Async
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T GetById(string id);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        IEnumerable<IGrouping<IKey,T>> GroupBy(Expression<Func<T, IKey>> keySelector);
        public IEnumerable<T>? Where(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        public IEnumerable<T>? FindAll(Expression<Func<T, bool>> expression, string[] includes = null);

        public IEnumerable<T>? FindAll(Expression<Func<T, bool>> expression,
           Expression<Func<T, object>> Orderby = null, string OrderByDirection = "ASC");
        public int? Count();

        public int? Count(Expression<Func<T, bool>> expression);

        IQueryable<T>? Select(Expression<Func<T, bool>> expression);

        public T? GetWithInclude(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        IQueryable<T>? Include(params Expression<Func<T, object>>[] includeProperties);

        bool Any(Expression<Func<T, bool>> expression);

        public void Attach(T Entity);

        public void Entry(T Entity);

        public IEnumerable<T>? SelectMany(Expression<Func<T, IEnumerable<T>>> selector);
        public IQueryable<T> AsNoTracking();
    }
}
