using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Interfaces;
using MediatR;

namespace Catalog_Service_Application.Products.Queries.GetMyProducts
{
    public class GetMyProductsHandler : IRequestHandler<GetMyProductsQuery, IEnumerable<GetProductsCatalogDTO>>
    {
        public GetMyProductsHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<IEnumerable<GetProductsCatalogDTO>> Handle(GetMyProductsQuery request, CancellationToken cancellationToken)
        {
            bool getFirst = request.lastSeenId == 0 ? true : false;
            return await UnitOfWork.Products.GetProductsCatalog(p =>
            p.UserId == request.userId &&
            (p.Id < request.lastSeenId || getFirst));
        }
    }
}
