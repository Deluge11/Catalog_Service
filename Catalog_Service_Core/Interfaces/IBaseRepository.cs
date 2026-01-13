



using System.Linq.Expressions;

namespace Catalog_Service_Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Single(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindIncludes(Expression<Func<T, bool>> match, string[] includes);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
    }
}
