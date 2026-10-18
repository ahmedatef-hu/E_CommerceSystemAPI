using E_CommerceSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTOs.Order
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }

        public List<GetOrderItemDTO> orderItems { get; set; }
    }
}
