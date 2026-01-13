using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Application.ProductImages.Commands.SetMainImage
{
    public record SetMainImageCommand(int productId, int imageId,int userId) : IRequest<bool>;
}
