using System.Linq.Expressions;

namespace LibraryHub.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            Expression<Func<T, bool>>? predicate,
            int skip,
            int take);

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task SaveChangesAsync();

        void Update(T entity);

        void Delete(T entity);
    }
}