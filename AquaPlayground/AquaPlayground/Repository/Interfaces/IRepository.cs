namespace AquaPlayground.Repository.Interfaces
{
    using System.Linq.Expressions;

    public interface IRepository<T>
        where T : class
    {
        public IQueryable<T> GetAsQueryable(bool noTracking = true);

        public Task<List<T>> GetElementListAsync();

        /// <summary>
        /// Get elemnts with pagination first page is 1.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="count">Count elements per page.</param>
        /// <returns>Chosen elements list.</returns>
        public Task<List<T>> GetElementListAsync(int page, int count);

        public Task<List<T>> GetElementListAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get elemnts with pagination first page is 1.
        /// </summary>
        /// <param name="predicate">Rule to sort collection.</param>
        /// <param name="page">Page number.</param>
        /// <param name="count">Count elements per page.</param>
        /// <returns>Chosen elements list.</returns>
        public Task<List<T>> GetElementListAsync(Expression<Func<T, bool>> predicate, int page, int count);

        public Task<int> CountAsync();

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        public void Remove(T value);

        public void RemoveRange(List<T> values);

        public Task AddAsync(T value);

        public Task AddRangeAsync(List<T> values);

        public void Attach(T value);

        public void Update(T value);
    }
}
