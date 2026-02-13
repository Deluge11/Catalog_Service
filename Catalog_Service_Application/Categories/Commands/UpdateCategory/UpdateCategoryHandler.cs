using Catalog_Service_Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        public UpdateCategoryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            bool isExists = await UnitOfWork.Categories.Exists(c => c.Name == request.name);

            if (isExists)
                return false;

            var category = await UnitOfWork.Categories.Single(c => c.Id == request.id);
            if (category == null || category.Name == request.name)
                return false;

            category.Name = request.name;
            return await UnitOfWork.Complete() != 0;
        }
    }
}
