using Catalog_Service_Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace Catalog_Service_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public CategoryController()
        {

        }



        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok();
        }



        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok();
        }



        [Authorize]
        //[CheckPermission(Permission.Categories_ManageCategories)]
        [HttpPost("{name}")]
        public async Task<IActionResult> CreateCategory(string name)
        {
            return Ok();
        }


        [Authorize]
        //[CheckPermission(Permission.Categories_ManageCategories)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromQuery] string name)
        {
            return Ok();
        }

    }
}
