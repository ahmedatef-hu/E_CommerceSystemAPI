using E_CommerceSystem.Data;
using E_CommerceSystem.JWT;
using E_CommerceSystem.Models;
using E_CommerceSystem.Repos.Interfaces;

namespace E_CommerceSystem.UnitOFWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public IAuthRepo AuthRepo { get; }

        public IGenericRepo<Category> CategoryRepo {  get; }
        public IProductRepo ProductRepo { get; }

        public IGenericRepo<OrderItem> OrderItemRepo { get; }

        public IOrderRepo OrderRepo { get; }

        public UnitOfWork(AppDbContext context, IAuthRepo authRepo, IGenericRepo<Category> CategoryRepo, IProductRepo productRepo, IGenericRepo<OrderItem> orderItemRepo, IOrderRepo orderRepo)
        {
            this.context = context;
            AuthRepo = authRepo;
            this.CategoryRepo = CategoryRepo;
            ProductRepo = productRepo;
            OrderItemRepo = orderItemRepo;
            OrderRepo = orderRepo;
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
