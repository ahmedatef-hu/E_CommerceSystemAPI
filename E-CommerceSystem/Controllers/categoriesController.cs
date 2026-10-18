using AutoMapper;
using E_CommerceSystem.DTOs.Category;
using E_CommerceSystem.Models;
using E_CommerceSystem.UnitOFWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_CommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public categoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await unitOfWork.CategoryRepo.GetAll();
            var dto = categories.Select(x => new GetCategoryDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            });
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO category)
        {
            var categoryy = new Category
            {
                Description = category.Description,
                Name = category.Name,
            };
            await unitOfWork.CategoryRepo.Create(categoryy);
            unitOfWork.Save();
            return Created();
        }
    }
}
