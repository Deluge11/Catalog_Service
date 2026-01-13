using Catalog_Service_Core.DTOs;
using MediatR;

namespace Catalog_Service_Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(UpdateProductDTO upDTP, int userId) : IRequest<bool>;
}
