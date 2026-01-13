
using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Entities;
using MediatR;

namespace Catalog_Service_Application.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery() : IRequest<IEnumerable<GetCategoryDTO>>;
}
