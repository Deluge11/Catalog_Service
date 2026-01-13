using Catalog_Service_API.Extensions;
using Catalog_Service_Core.DTOs;
using Catalog_Service_Application.Products.Commands.InsertProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service_API.Controllers
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


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProductsByCurserForOneCategory([FromQuery] int categoryId, [FromQuery] int take, [FromQuery] int lastSeenId)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("get-all-sections")]
        public async Task<IActionResult> GetProductsByCurserForAllCategories([FromQuery] int take, [FromQuery] int lastSeenId)
        {
            return Ok();

        }

        [AllowAnonymous]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return Ok();

        }



        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProductsByUserId(int userId)
        {
            return Ok();

        }



        [AllowAnonymous]
        [HttpGet("images/{productId}")]
        public async Task<IActionResult> GetImagesByProductId(int productId)
        {
            return Ok();

        }



        //[CheckPermission(Permission.Products_ManageOwnProduct)]
        [HttpGet("my-products")]
        public async Task<IActionResult> GetMyProducts()
        {
            return Ok();

        }



        //[CheckPermission(Permission.Products_ManageOwnProduct)]
        [HttpPost]
        public async Task<IActionResult> InsertProduct(InsertProductDTO ipDTO)
        {
            var result = await Mediator.Send(new InsertProductCommand(ipDTO, User.GetUserId()));
            return result ? Ok() : BadRequest();

        }



        //[CheckPermission(Permission.Products_ManageOwnProduct)]
        [HttpPost("{productId}")]
        public async Task<ActionResult> UploadImage(List<IFormFile> images, int productId)
        {
            return Ok();
        }



        //[CheckPermission(Permission.Products_ManageOwnProduct)]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct()
        {
            //UpdateProductRequest product
            return Ok();

        }



        //[CheckPermission(Permission.Products_ManageOwnProduct)]
        [HttpPatch("image/{productId}")]
        public async Task<ActionResult> SetMainImage(int productId, [FromQuery] int imageId)
        {
            return Ok();
        }

    }
}
