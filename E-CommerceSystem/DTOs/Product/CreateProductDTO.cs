using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTOs.Product
{
    public class CreateProductDTO
    {
        [Required, MinLength(2), MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required, Range(0.01, 1000000)]
        public decimal Price { get; set; }
        [Required, Range(1, 1000000)]
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}
