using E_CommerceSystem.DTOs.Order;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repos.Implementations;

namespace E_CommerceSystem.Repos.Interfaces
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<Order> GetByIdd(int id);
        Task<Order> UpdateStatus(int id, string Status);
        Task<TotalRevenueDTO> Revenue();
    }
}
