using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using Catalog_Service_Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(AppDbContext context)
        {
            Context = context;
            Products = new ProductsRepository(context);
            Categories = new BaseRepository<Category>(context);
            ProductImages = new ProductImagesRepository(context);
        }

        public AppDbContext Context { get; }

        public IProductRepository Products { get; }

        public IBaseRepository<Category> Categories { get; }

        public IProductImagesRepository ProductImages { get; }

        public async Task<int> Complete()
        {
            return await Context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
        }
    }
}
