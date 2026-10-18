using E_CommerceSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTOs.Order
{
    public class CreateOrderDTO
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string Status = "Pending";
        [Required]
        public decimal TotalAmount = 0;

        public List<CreateOrderItemDTO> orderItems { get; set; }
    }
}
