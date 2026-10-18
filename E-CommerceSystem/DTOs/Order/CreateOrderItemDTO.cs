using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTOs.Order
{
    public class CreateOrderItemDTO
    {
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
