using E_CommerceSystem.Data;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Repos.Implementations
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetPagination(int? CategoryId, int PageNumber, int PageSize, string? SortBY)
        {
            var query = context.Products.AsQueryable();

            if (CategoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == CategoryId);
            }

            query = SortBY switch
            {
                "Name" => query.OrderBy(c => c.Name),
                "Stock" => query.OrderBy(c => c.StockQuantity),
                "Price" => query.OrderBy(c => c.Price),
                _ => query.OrderBy(c => c.Id),
            };

            return await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        public async Task<List<Product>> Search(string Name)
        {
            return await context.Products.Where(c => c.Name.Contains(Name)).ToListAsync();
        }

        public async Task<Product?> SecondMostExpensive(int CategoryId)
        {
            return context.Products
                .Where(c => c.CategoryId != CategoryId)
                .OrderByDescending(c => c.Price)
                .Skip(1)
                .FirstOrDefault();
        }

        public async Task<Product> UpdateStock(int id, int Stock)
        {
            var product = await GetById(id);
            if (product == null)
            {
                return null;
            }
            product.StockQuantity = Stock;
            context.SaveChanges();
            return product;
        }
    }
}
