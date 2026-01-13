using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using MediatR;

namespace Catalog_Service_Application.ProductImages.Commands.UploadImage
{
    public class UploadImageHandler : IRequestHandler<UploadImageCommand, bool>
    {
        public UploadImageHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var isProductExists = await UnitOfWork.Products.Exists(p => p.Id == request.productId && p.UserId == request.userId);
            if (!isProductExists)
            {
                return false;
            }

            var imagesCount = await UnitOfWork.ProductImages.Count(pi => pi.ProductId == request.productId);
            if (imagesCount >= 8)
            {
                return false;
            }

            var imagePath = "mock-fileservice.uploadimage(request.image,productId)";
            if (imagePath == null)
            {
                return false;
            }

            var productImage = new ProductImage
            {
                Path = imagePath,
                ProductId = request.productId
            };

            await UnitOfWork.ProductImages.Add(productImage);
            await UnitOfWork.Complete();
            return true;
        }
    }
}
