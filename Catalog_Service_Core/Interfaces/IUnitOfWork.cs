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
        IProductRepository Products { get; }
        IBaseRepository<Category> Categories { get; }
        IProductImagesRepository ProductImages { get; }
        public IBaseRepository<User> Users { get; }
        Task<int> Complete();
    }
}
