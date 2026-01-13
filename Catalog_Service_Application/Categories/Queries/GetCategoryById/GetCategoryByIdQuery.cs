using Catalog_Service_Core.DTOs;
using MediatR;

namespace Catalog_Service_Application.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int id) : IRequest<GetCategoryDTO>;
}
