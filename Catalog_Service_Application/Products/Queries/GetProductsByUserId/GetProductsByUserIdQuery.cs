using Catalog_Service_Core.DTOs;
using MediatR;

namespace Catalog_Service_Application.Products.Queries.GetProductsByUserId
{
    public record GetProductsByUserIdQuery(int userId, int lastSeenId) : IRequest<IEnumerable<GetProductsCatalogDTO>>;
}
