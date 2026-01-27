using Catalog_Service_Application.Categories.Commands.CreateCategory;
using Catalog_Service_Application.Categories.Commands.UpdateCategory;
using Catalog_Service_Application.Categories.Queries.GetAllCategories;
using Catalog_Service_Application.Categories.Queries.GetCategoryById;
using Catalog_Service_Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service_API.Controllers.Public
{
    [ApiController]
    [Route("public/[controller]")]
    public class CategoryController : ControllerBase
    {
        public IMediator Mediator { get; }

        public CategoryController(IMediator mediator)
        {
            Mediator = mediator;
        }



        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery(id));
            return result != null ? Ok(result) : NotFound();
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await Mediator.Send(new GetAllCategoriesQuery());
            return result.Any() ? Ok(result) : NotFound();
        }
      
    }
}
