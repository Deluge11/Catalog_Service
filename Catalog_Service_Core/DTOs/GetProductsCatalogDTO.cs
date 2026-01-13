using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Core.DTOs
{
    public record GetProductsCatalogDTO(int id, string name, decimal price, string imagePath);

}
