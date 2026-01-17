

using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using ConstantsLib.Events;
using ConstantsLib.Interfaces;
using MediatR;


namespace Catalog_Service_Application.Products.Commands.InsertProduct
{
    public class InsertProductHandler : IRequestHandler<InsertProductCommand, bool>
    {
        public InsertProductHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            UnitOfWork = unitOfWork;
            EventBus = eventBus;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IEventBus EventBus { get; }

        public async Task<bool> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.ipDTO.name.Trim(),
                CategoryId = request.ipDTO.categoryId,
                Price = request.ipDTO.price,
                Description = request.ipDTO.description.Trim(),
                UserId = request.userId
            };

            await UnitOfWork.Products.Add(product);

            if (await UnitOfWork.Complete() == 0)
            {
                return false;
            }

            var productCreatedEvent = new ProductCreatedEvent
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                UserId = product.UserId,
                Description = product.Description
            };

            await EventBus.Publish(productCreatedEvent);

            return true;
        }
    }
}
