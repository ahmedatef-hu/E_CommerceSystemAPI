using E_CommerceSystem.Data;
using E_CommerceSystem.DTOs.Order;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Repos.Implementations
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<Order> GetByIdd(int id)
        {
            return await context.Orders
                .Include(o => o.orderItems)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<TotalRevenueDTO> Revenue()
        {
            var revenue = await context.Orders
                .Include (o => o.orderItems)
                .Where(c => c.Status == "Completed")
                .SumAsync(c => c.TotalAmount);
            return new TotalRevenueDTO
            {
                TotalRevenue = revenue
            };
        }

        public async Task<Order> UpdateStatus(int id, string Status)
        {
            var order = await GetById(id);
            order.Status = Status;

            context.SaveChanges();
            return order;
        }
    }
}
