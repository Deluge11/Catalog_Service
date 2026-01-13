using MediatR;
using Microsoft.AspNetCore.Http;


namespace Catalog_Service_Application.ProductImages.Commands.UploadImage
{
    public record UploadImageCommand(IFormFile image, int productId, int userId) : IRequest<bool>;
}
