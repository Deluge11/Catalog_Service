

using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using MediatR;


namespace Catalog_Service_Application.Products.Commands.InsertProduct
{
    public class InsertProductHandler : IRequestHandler<InsertProductCommand, bool>
    {
        public InsertProductHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.ipDTO.name.Trim(),
                CategoryId = request.ipDTO.categoryId,
                Description = request.ipDTO.description.Trim(),
                UserId = request.userId
            };

            await UnitOfWork.Products.Add(product);

            if (await UnitOfWork.Complete() == 0)
            {
                return false;
            }

            //New Product Event

            return true;
        }
    }
}
