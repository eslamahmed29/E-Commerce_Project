using DataAccess.Data;
using E_Commerce_Test.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDBContext _db;

        public BaseRepository(AppDBContext db)
        {
            _db = db;

        }
        public void Add(T entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            await _db.AddRangeAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
           await _db.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _db.Set<T>().Any(expression);
        }

        public async Task<bool?> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().AnyAsync(expression);
        }

        public IQueryable<T> AsNoTracking()
        {
            return _db.Set<T>().AsNoTracking();
        }

        public void Attach(T Entity)
        {
_db.Attach(Entity);
            _db.Entry(Entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public int? Count()
        {
            return _db.Set<T>().Count();
        }

        public int? Count(Expression<Func<T, bool>> expression)
        {
            return _db.Set<T>().Count(expression);
        }

        public async Task<int> CountAsync()
        {
            return await _db.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().CountAsync(expression);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
            _db.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
             _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
            _db.SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
            await _db.SaveChangesAsync();
        }

        public void Entry(T Entity)
        {
            _db.Entry(Entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<T>? FindAll(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> query = _db.Set<T>();
            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }
            return query.Where(expression).ToList();
        }

        public IEnumerable<T>? FindAll(Expression<Func<T, bool>> expression, Expression<Func<T, object>> Orderby = null, string OrderByDirection = "ASC")
        {
IQueryable<T> query = _db.Set<T>();
            if (Orderby != null)
            {
                if (OrderByDirection == "ASC")
                {
                    query = query.OrderBy(Orderby);
                }
                else
                {
                    query = query.OrderByDescending(Orderby);
                }
            }
            return query.Where(expression).ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>().Where(expression);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _db.Set<T>().Where(expression);
            if (orderBy != null)
            {
                query = orderByDirection == "ASC" ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> FirstOrDefaultASync(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().FirstOrDefaultAsync(expression);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _db.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public  T GetById(string id)
        {
            return _db.Set<T>().Find(id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _db.Set<T>();
            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>().Where(filter);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync();
        }

        public T? GetWithInclude(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();
            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }
            return query.Where(expression).FirstOrDefault();
        }

        public IEnumerable<IGrouping<IKey, T>> GroupBy(Expression<Func<T, IKey>> keySelector)
        {
            return _db.Set<T>().GroupBy(keySelector);
        }

        public IQueryable<T>? Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _db.Set<T>();
            return includeProperties.Aggregate(query, (current, include) => current.Include(include));

        }

        public IQueryable<T>? Select(Expression<Func<T, bool>> expression)
        {
            return _db.Set<T>().Where(expression);
        }

        public async Task<IQueryable<T>> SelectAsync(Expression<Func<T, bool>> expression)
        {
            return (IQueryable<T>)await _db.Set<T>().Where(expression).ToListAsync();
        }

        public IEnumerable<T>? SelectMany(Expression<Func<T, IEnumerable<T>>> selector)
        {
            return _db.Set<T>().SelectMany(selector);
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(T entity)
        {
            _db.UpdateRange(entity);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<T>? Where(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _db.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query;
        }

        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }
    }
}
