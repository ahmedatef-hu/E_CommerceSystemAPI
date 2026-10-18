using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTOs.Order
{
    public class GetOrderItemDTO
    {
        public int Id { get; set; }
        [Required, Range(1, 1000)]
        public int Quantity { get; set; }
        [Required, Range(0.01, 1000000)]
        public decimal UnitPrice { get; set; }
    }
}
