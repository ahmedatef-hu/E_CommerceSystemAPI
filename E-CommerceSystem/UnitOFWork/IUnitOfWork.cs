using E_CommerceSystem.JWT;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repos.Interfaces;

namespace E_CommerceSystem.UnitOFWork
{
    public interface IUnitOfWork
    {
        IAuthRepo AuthRepo { get; }
        IGenericRepo<Category> CategoryRepo { get; }
        IGenericRepo<OrderItem> OrderItemRepo { get; }
        IProductRepo ProductRepo { get; }
        IOrderRepo OrderRepo { get; }

        int Save();
    }
}
