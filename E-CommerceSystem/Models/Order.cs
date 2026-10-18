using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
    }
}
