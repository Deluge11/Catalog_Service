using Catalog_Service_Core.DTOs;
using MediatR;

namespace Catalog_Service_Application.ProductImages.Queries.GetImagesByProductId
{
    public record GetImagesByProductIdQuery(int productId) : IRequest<List<GetImagesByProductIdDTO>>;
}
