using Catalog_Service_Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.Products.Queries.GetProductsByCurserForAllCategories
{
    public record GetProductsByCurserForAllCategoriesQuery(int lastSeenId) :IRequest<IEnumerable<GetProductsCatalogDTO>>;
}
