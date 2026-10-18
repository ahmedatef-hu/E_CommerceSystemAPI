using AutoMapper;
using E_CommerceSystem.DTOs.Product;
using E_CommerceSystem.Models;

namespace E_CommerceSystem.Mapping
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Product, GetProductDTO>()
                .ForMember(c => c.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateProductDTO, Product>();
        }
    }
}
