using Catalog_Service_Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        public UpdateProductHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await UnitOfWork.Products.Single(p => p.Id == request.upDTP.id && p.UserId == request.userId);
            if (product == null)
            {
                return false;
            }

            product.Name = request.upDTP.name.Trim();
            product.Description = request.upDTP.description?.Trim();
            product.Price = request.upDTP.price;
            product.CategoryId = request.upDTP.categoryId;

            return await UnitOfWork.Complete() > 0;
        }
    }
}
