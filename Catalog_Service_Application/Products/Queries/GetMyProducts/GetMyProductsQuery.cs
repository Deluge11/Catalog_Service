using Catalog_Service_Core.DTOs;
using MediatR;

namespace Catalog_Service_Application.Products.Queries.GetMyProducts
{
    public record GetMyProductsQuery(int userId,int lastSeenId) : IRequest<List<GetProductsCatalogDTO>>;
}
