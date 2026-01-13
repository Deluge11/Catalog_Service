using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Entities;
using System.Linq.Expressions;

namespace Catalog_Service_Core.Interfaces
{
    public interface IProductImagesRepository : IBaseRepository<ProductImage>
    {
        public Task<List<GetImagesByProductIdDTO>> GetImagesByProductId(int productId);
    }
}
