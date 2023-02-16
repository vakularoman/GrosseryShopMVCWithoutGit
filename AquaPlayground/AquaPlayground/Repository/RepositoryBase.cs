namespace AquaPlayground.Repository
{
    using System;
    using System.Linq.Expressions;
    using AquaPlayground.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        public RepositoryBase(AquaPlaygroundContext context)
        {
            Context = context;
        }

        protected AquaPlaygroundContext Context { get; }

        public IQueryable<T> GetAsQueryable(bool noTracking = true)
        {
            if (noTracking)
            {
                return Context.Set<T>().AsNoTracking().AsQueryable();
            }

            return Context.Set<T>().AsQueryable();
        }

        public async Task<List<T>> GetElementListAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetElementListAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }

        public void Remove(T value)
        {
            Context.Remove(value);
        }

        public void RemoveRange(List<T> values)
        {
            Context.RemoveRange(values);
        }

        public async Task AddAsync(T value)
        {
            await Context.AddAsync(value);
        }

        public async Task AddRangeAsync(List<T> values)
        {
            await Context.AddRangeAsync(values);
        }

        public void Attach(T value)
        {
            Context.Attach(value);
        }

        public void Update(T value)
        {
            Context.Update(value);
        }

        public async Task<List<T>> GetElementListAsync(int page, int count)
        {
            return await Context.Set<T>().Skip((page - 1) * count).Take(count).ToListAsync();
        }

        public async Task<List<T>> GetElementListAsync(Expression<Func<T, bool>> predicate, int page, int count)
        {
            return await Context.Set<T>().Where(predicate).Skip((page - 1) * count).Take(count).ToListAsync();
        }
    }
}
