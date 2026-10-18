using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTOs.Category
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
