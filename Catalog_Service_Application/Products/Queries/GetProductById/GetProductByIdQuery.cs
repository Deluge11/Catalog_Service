using Catalog_Service_Core.DTOs;
using MediatR;

namespace Catalog_Service_Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int productId) : IRequest<GetProductDetailsDTO>;
}
