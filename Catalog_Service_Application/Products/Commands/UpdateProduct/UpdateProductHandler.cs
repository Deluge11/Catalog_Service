using Catalog_Service_Core.Interfaces;
using ConstantsLib.Events;
using ConstantsLib.Interfaces;
using MediatR;

namespace Catalog_Service_Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        public UpdateProductHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            UnitOfWork = unitOfWork;
            EventBus = eventBus;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IEventBus EventBus { get; }

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

            if (await UnitOfWork.Complete() == 0)
            {
                return false;
            }

            var productUpdatedEvent = new ProductUpdateEvent
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            await EventBus.Publish(productUpdatedEvent);

            return true;
        }
    }
}
