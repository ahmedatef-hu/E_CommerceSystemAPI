using AutoMapper;
using E_CommerceSystem.DTOs.Product;
using E_CommerceSystem.Models;
using E_CommerceSystem.UnitOFWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_CommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public productsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? CategoryId, int PageNumber, int PageSize, string? SortBY)
        {
            var products = await unitOfWork.ProductRepo.GetPagination(CategoryId, PageNumber, PageSize, SortBY);
            var res = mapper.Map<List<GetProductDTO>>(products);
            return Ok(res);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var product = await unitOfWork.ProductRepo.GetById(Id);
            var dto = mapper.Map<GetProductDTO>(product);
            return Ok(dto);
        }

        [HttpGet("second-most-expensive-excluding-category")]
        public async Task<IActionResult> SecondMost(int CategoryId)
        {
            var products = await unitOfWork.ProductRepo.SecondMostExpensive(CategoryId);
            var res = mapper.Map<GetProductDTO>(products);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO dto)
        {
            var product = mapper.Map<Product>(dto);
            await unitOfWork.ProductRepo.Create(product);
            unitOfWork.Save();
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProductDTO dto)
        {
            var product = await unitOfWork.ProductRepo.GetById(id);
            if(product == null)
            {
                return BadRequest("Product Not Found");
            }
            mapper.Map<Product>(product);
            unitOfWork.ProductRepo.Update(product);
            unitOfWork.Save();
            return Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, int Stok)
        {
            var product = await unitOfWork.ProductRepo.UpdateStock(id, Stok);
            unitOfWork.ProductRepo.Update(product);
            unitOfWork.Save();
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string Name)
        {
            var products = await unitOfWork.ProductRepo.Search(Name);
            var res = mapper.Map<List<GetProductDTO>>(products);
            return Ok(res);
        }
    }
}
