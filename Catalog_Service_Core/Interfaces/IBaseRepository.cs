



using System.Linq.Expressions;

namespace Catalog_Service_Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> Single(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindIncludes(Expression<Func<T, bool>> match, string[] includes);
        IEnumerable<T> GetEntityWithoutTraching();
        IEnumerable<T> FindAllWithoutTracking(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAllWithTracking(Expression<Func<T, bool>> predicate);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
    }
}
