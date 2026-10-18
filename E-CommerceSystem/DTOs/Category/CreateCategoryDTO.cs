using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTOs.Category
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
