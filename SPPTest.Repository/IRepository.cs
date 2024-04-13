using System.Linq.Expressions;

namespace SPPTest.Repository
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
    }
}
