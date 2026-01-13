using Azure.Core;
using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog_Service_Infrastructure.Repositories
{
    public class ProductsRepository : BaseRepository<Product>, IProductRepository
    {
        private DbSet<Product> Entity { get; }
        public ProductsRepository(AppDbContext context) : base(context)
        {
            Entity = context.Set<Product>();
        }
        public async Task<IEnumerable<GetProductsCatalogDTO>> GetProductsCatalog(Expression<Func<Product, bool>> predicate)
        {
            var result = await Entity
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(p => p.Id)
                .Take(12)
                .Select(p => new GetProductsCatalogDTO(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.MainImage != null ? p.MainImage.Path : "default_image.jpg"))
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<GetProductDetailsDTO>> GetProductsDetails(Expression<Func<Product, bool>> predicate)
        {
            return await Entity
                .AsNoTracking()
                .Where(predicate)
                .Select(p => new GetProductDetailsDTO(
                 p.Id,
                 p.Name,
                 p.Description ?? "There is no description",
                 p.Price,
                 p.ProductImages.Select(img => new ProductImageDTO(img.Id, img.Path)),
                 p.CategoryId
                 ))
                 .ToListAsync();
        }
    }
}
