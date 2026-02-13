using Catalog_Service_API.Attributes;
using Catalog_Service_Application.Categories.Commands.CreateCategory;
using Catalog_Service_Application.Categories.Commands.UpdateCategory;
using ConstantsLib.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service_API.Controllers.Private
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


        [CheckPermission(EnPermission.Categories_ManageCategories)]
        [HttpPost("add/{name}")]
        public async Task<IActionResult> CreateCategory(string name)
        {
            var result = await Mediator.Send(new CreateCategoryCommand(name));
            return result ? Created() : BadRequest();
        }


        [CheckPermission(EnPermission.Categories_ManageCategories)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromQuery] string name)
        {
            var result = await Mediator.Send(new UpdateCategoryCommand(id, name));
            return result ? NoContent() : BadRequest();
        }

    }
}
