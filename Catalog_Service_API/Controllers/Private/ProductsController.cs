using Catalog_Service_API.Extensions;
using Catalog_Service_Core.DTOs;
using Catalog_Service_Application.Products.Commands.InsertProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog_Service_Application.Products.Queries.GetProductsByUserId;
using Catalog_Service_Application.ProductImages.Commands.UploadImage;
using Catalog_Service_Application.Products.Commands.UpdateProduct;
using Catalog_Service_Application.ProductImages.Commands.SetMainImage;
using ConstantsLib.Enums;
using Catalog_Service_API.Attributes;

namespace Catalog_Service_API.Controllers.Private
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        public IMediator Mediator { get; }

        public ProductsController(IMediator mediator)
        {
            Mediator = mediator;
        }


      
        [CheckPermission(EnPermission.Products_ManageOwnProduct)]
        [HttpGet("my-products")]
        public async Task<IActionResult> GetMyProducts([FromQuery] int lastSeenId)
        {
            var result = await Mediator.Send(new GetProductsByUserIdQuery(User.GetUserId(), lastSeenId));
            return result.Any() ? Ok(result) : NoContent();
        }


        [CheckPermission(EnPermission.Products_ManageOwnProduct)]
        [HttpPost]
        public async Task<IActionResult> InsertProduct(InsertProductDTO ipDTO)
        {
            var result = await Mediator.Send(new InsertProductCommand(ipDTO, User.GetUserId()));
            return result ? Created() : BadRequest();

        }


        [CheckPermission(EnPermission.Products_ManageOwnProduct)]
        [HttpPost("{productId}")]
        public async Task<IActionResult> UploadImage(IFormFile image, int productId)
        {
            var result = await Mediator.Send(new UploadImageCommand(image,productId,User.GetUserId()));
            return result ? Created() : BadRequest();
        }



        [CheckPermission(EnPermission.Products_ManageOwnProduct)]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO upDTO)
        {
            var result = await Mediator.Send(new UpdateProductCommand(upDTO,User.GetUserId()));
            return result ? NoContent() : BadRequest();
        }



        [CheckPermission(EnPermission.Products_ManageOwnProduct)]
        [HttpPatch("image/{productId}")]
        public async Task<IActionResult> SetMainImage(int productId, [FromQuery] int imageId)
        {
            var result = await Mediator.Send(new SetMainImageCommand(productId,imageId,User.GetUserId()));
            return result ? NoContent() : BadRequest();
        }

    }
}
