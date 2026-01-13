using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Interfaces;
using MediatR;

namespace Catalog_Service_Application.Products.Queries.GetProductsByUserId
{
    public class GetProductsByUserIdHandler : IRequestHandler<GetProductsByUserIdQuery, IEnumerable<GetProductsCatalogDTO>>
    {
        public GetProductsByUserIdHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<IEnumerable<GetProductsCatalogDTO>> Handle(GetProductsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await UnitOfWork.Products.GetProductsCatalog(p =>
            p.UserId == request.userId &&
            (p.Id < request.lastSeenId || request.lastSeenId == 0));
        }
    }
}
