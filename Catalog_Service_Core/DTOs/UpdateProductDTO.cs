using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Core.DTOs
{
    public record UpdateProductDTO(int id, string name, string description, decimal price, int categoryId, float weight);
}
