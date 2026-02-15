using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Repositories
{
    /// <summary>
    /// Generic Repository Implementation - Design Pattern: Repository Pattern
    /// </summary>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        // Trong thực tế, đây sẽ là DbContext
        protected readonly List<T> _dataStore;

        public Repository(List<T> dataStore)
        {
            _dataStore = dataStore;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_dataStore.FirstOrDefault(x => x.Id == id));
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dataStore.Where(x => x.IsActive).ToList());
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dataStore.AsQueryable().Where(predicate).ToList());
        }

        public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dataStore.AsQueryable().FirstOrDefault(predicate));
        }

        public virtual async Task AddAsync(T entity)
        {
            entity.Id = _dataStore.Count > 0 ? _dataStore.Max(x => x.Id) + 1 : 1;
            entity.CreatedAt = DateTime.UtcNow;
            _dataStore.Add(entity);
            await Task.CompletedTask;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public virtual void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var existing = _dataStore.FirstOrDefault(x => x.Id == entity.Id);
            if (existing != null)
            {
                var index = _dataStore.IndexOf(existing);
                _dataStore[index] = entity;
            }
        }

        public virtual void Remove(T entity)
        {
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            Update(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null)
                return await Task.FromResult(_dataStore.Count);
            return await Task.FromResult(_dataStore.AsQueryable().Count(predicate));
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dataStore.AsQueryable().Any(predicate));
        }
    }
}
