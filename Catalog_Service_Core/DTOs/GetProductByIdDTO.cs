
namespace Catalog_Service_Core.DTOs
{
    public record GetProductDetailsDTO(
        int id,
        string name,
        string description,
        decimal price,
        IEnumerable<ProductImageDTO> Images,
        int categoryId
        );
}
