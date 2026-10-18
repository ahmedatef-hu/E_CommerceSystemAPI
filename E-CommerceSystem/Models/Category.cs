using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        [Required, MinLength(2), MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
