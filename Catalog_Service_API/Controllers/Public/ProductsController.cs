using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog_Service_Application.Products.Queries.GetProductsByCurserForOneCategory;
using Catalog_Service_Application.Products.Queries.GetProductsByCurserForAllCategories;
using Catalog_Service_Application.Products.Queries.GetProductById;
using Catalog_Service_Application.Products.Queries.GetProductsByUserId;
using Catalog_Service_Application.ProductImages.Queries.GetImagesByProductId;

namespace Catalog_Service_API.Controllers.Public
{
    [ApiController]
    [Route("public/[controller]")]
    public class ProductsController : ControllerBase
    {
        public IMediator Mediator { get; }

        public ProductsController(IMediator mediator)
        {
            Mediator = mediator;
        }


        [HttpGet("get-category-catalog")]
        public async Task<IActionResult> GetProductsByCurserForOneCategory([FromQuery] int lastSeenId, [FromQuery] int categoryId)
        {
            var result = await Mediator.Send(new GetProductsByCurserForOneCategoryQuery(lastSeenId, categoryId));
            return result.Any() ? Ok(result) : NotFound();
        }

        [HttpGet("get-all-sections")]
        public async Task<IActionResult> GetProductsByCurserForAllCategories([FromQuery] int lastSeenId)
        {
            var result = await Mediator.Send(new GetProductsByCurserForAllCategoriesQuery(lastSeenId));
            return result.Any() ? Ok(result) : NotFound();

        }

        [HttpGet("get-by-id/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await Mediator.Send(new GetProductByIdQuery(productId));
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProductsByUserId(int userId, [FromQuery] int lastSeenId)
        {
            var result = await Mediator.Send(new GetProductsByUserIdQuery(userId, lastSeenId));
            return result.Any() ? Ok(result) : NotFound();
        }

        [HttpGet("images/{productId}")]
        public async Task<IActionResult> GetImagesByProductId(int productId)
        {
            var result = await Mediator.Send(new GetImagesByProductIdQuery(productId));
            return result.Any() ? Ok(result) : NotFound();
        }

    }
}
