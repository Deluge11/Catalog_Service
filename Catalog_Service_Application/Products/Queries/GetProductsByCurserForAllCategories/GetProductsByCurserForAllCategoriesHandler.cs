
using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Interfaces;
using MediatR;

namespace Catalog_Service_Application.Products.Queries.GetProductsByCurserForAllCategories
{
    public class GetProductsByCurserForAllCategoriesHandler : IRequestHandler<GetProductsByCurserForAllCategoriesQuery, IEnumerable<GetProductsCatalogDTO>>
    {
        public GetProductsByCurserForAllCategoriesHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<IEnumerable<GetProductsCatalogDTO>> Handle(GetProductsByCurserForAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await UnitOfWork.Products.GetProductsCatalog(p =>
            p.Id < request.lastSeenId || request.lastSeenId == 0);
        }
    }
}
