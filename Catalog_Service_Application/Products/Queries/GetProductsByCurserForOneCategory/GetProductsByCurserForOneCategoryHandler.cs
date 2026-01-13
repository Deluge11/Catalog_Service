using Catalog_Service_Application.Products.Queries.GetProductsByCurserForAllCategories;
using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.Products.Queries.GetProductsByCurserForOneCategory
{
    class GetProductsByCurserForOneCategoryHandler : IRequestHandler<GetProductsByCurserForOneCategoryQuery, IEnumerable<GetProductsCatalogDTO>>
    {
        public GetProductsByCurserForOneCategoryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<IEnumerable<GetProductsCatalogDTO>> Handle(GetProductsByCurserForOneCategoryQuery request, CancellationToken cancellationToken)
        {
            return await UnitOfWork.Products.GetProductsCatalog(p =>
            p.CategoryId == request.categoryId &&
            (p.Id < request.lastSeenId || request.lastSeenId == 0));
        }
    }
}
