
using Catalog_Service_Core.DTOs;
using Catalog_Service_Core.Interfaces;
using MediatR;

namespace Catalog_Service_Application.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductDetailsDTO>
    {
        public GetProductByIdHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<GetProductDetailsDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var products = await UnitOfWork.Products.GetProductsDetails(p => p.Id == request.productId);
            return products.FirstOrDefault();
        }
    }
}
