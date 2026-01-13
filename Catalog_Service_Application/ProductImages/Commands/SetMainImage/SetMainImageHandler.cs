using Catalog_Service_Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.ProductImages.Commands.SetMainImage
{
    public class SetMainImageHandler : IRequestHandler<SetMainImageCommand, bool>
    {
        public SetMainImageHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> Handle(SetMainImageCommand request, CancellationToken cancellationToken)
        {
            var product = await UnitOfWork.Products.Single(p => p.Id == request.productId && p.UserId == request.userId);

            if (product == null)
            {
                return false;
            }

            var isImageExists = await UnitOfWork.ProductImages.Exists(pi => pi.Id == request.imageId && pi.ProductId == product.Id);

            if (!isImageExists)
            {
                return false;
            }

            product.ImageId = request.imageId;

            return await UnitOfWork.Complete() > 0;
        }
    }
}
