


using System.ComponentModel.DataAnnotations;

namespace Catalog_Service_Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [MinLength(2)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public int? ImageId { get; set; }
        public ProductImage? MainImage { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
