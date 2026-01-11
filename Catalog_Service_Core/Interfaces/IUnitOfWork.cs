using Catalog_Service_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Core.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IBaseRepository<Product> Products { get; }
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<ProductImage> ProductImages { get; }
        Task<int> Complete();
    }
}
