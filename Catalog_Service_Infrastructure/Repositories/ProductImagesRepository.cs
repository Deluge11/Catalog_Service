using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Service_Infrastructure.Repositories
{
    public class ProductImagesRepository : BaseRepository<ProductImage>, IProductImagesRepository
    {
        private DbSet<ProductImage> Entity { get; }
        public ProductImagesRepository(AppDbContext context) : base(context)
        {
            Entity = context.Set<ProductImage>();
        }

        public async Task<List<GetImagesByProductIdDTO>> GetImagesByProductId(int productId)
        {
            IQueryable query = Entity;
            return await Entity
                .AsNoTracking()
                .Where(pi=>pi.ProductId == productId)
                .Select(pi => new GetImagesByProductIdDTO(pi.Id, pi.Path))
                .ToListAsync();
        }

    }
}
