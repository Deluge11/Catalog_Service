using Catalog_Service_Core.DTOs;
using MediatR;


namespace Catalog_Service_Application.Products.Commands.InsertProduct
{
    public record InsertProductCommand(InsertProductDTO ipDTO,int userId) : IRequest<bool>;
}
