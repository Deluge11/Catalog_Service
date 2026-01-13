using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Interfaces;
using MediatR;

namespace Catalog_Service_Application.ProductImages.Queries.GetImagesByProductId
{
    public class GetImagesByProductIdHandler : IRequestHandler<GetImagesByProductIdQuery, List<GetImagesByProductIdDTO>>
    {
        public GetImagesByProductIdHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<List<GetImagesByProductIdDTO>> Handle(GetImagesByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await UnitOfWork.ProductImages.GetImagesByProductId(request.productId);
        }
    }
}
