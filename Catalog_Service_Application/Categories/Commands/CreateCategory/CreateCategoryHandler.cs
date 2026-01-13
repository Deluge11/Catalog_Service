using Catalog_Service_Core.Entities;
using Catalog_Service_Core.Interfaces;
using MediatR;

namespace Catalog_Service_Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        public CreateCategoryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await UnitOfWork.Categories.Add(new Category { Name = request.name });
            return await UnitOfWork.Complete() != 0;
        }
    }
}
