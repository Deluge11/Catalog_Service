using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(int id, string name) : IRequest<bool>;
}
