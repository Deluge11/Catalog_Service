



using System.Linq.Expressions;

namespace Catalog_Service_Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
    }
}
