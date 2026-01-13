using Catalog_Service_Application.Categories.Commands.CreateCategory;
using Catalog_Service_Application.Categories.Commands.UpdateCategory;
using Catalog_Service_Application.Categories.Queries.GetAllCategories;
using Catalog_Service_Application.Categories.Queries.GetCategoryById;
using Catalog_Service_Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public IMediator Mediator { get; }

        public CategoryController(IMediator mediator)
        {
            Mediator = mediator;
        }



        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery(id));
            return result != null ? Ok(result) : NotFound();
        }



        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await Mediator.Send(new GetAllCategoriesQuery());
            return result.Any() ? Ok(result) : NotFound();
        }



        //[CheckPermission(Permission.Categories_ManageCategories)]
        [HttpPost("{name}")]
        public async Task<IActionResult> CreateCategory(string name)
        {
            var result = await Mediator.Send(new CreateCategoryCommand(name));
            return result ? Ok(result) : BadRequest();
        }



        //[CheckPermission(Permission.Categories_ManageCategories)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromQuery] string name)
        {
            var result = await Mediator.Send(new UpdateCategoryCommand(id, name));
            return result ? Ok(result) : BadRequest();
        }

    }
}
