using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Core.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<GetProductsCatalogDTO>> GetProductsCatalog(Expression<Func<Product, bool>> predicate);
        Task<IEnumerable<GetProductDetailsDTO>> GetProductsDetails(Expression<Func<Product, bool>> predicate);
    }
}
