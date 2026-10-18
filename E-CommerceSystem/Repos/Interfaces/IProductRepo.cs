using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repos.Interfaces
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<List<Product>> GetPagination(int? CategoryId, int PageNumber, int PageSize, string? SortBY);
        Task<Product?> SecondMostExpensive(int CategoryId);
        Task<Product> UpdateStock(int id, int Stock);
        Task<List<Product>> Search(string Name);
    }
}
