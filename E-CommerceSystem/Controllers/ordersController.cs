using E_CommerceSystem.DTOs.Order;
using E_CommerceSystem.Models;
using E_CommerceSystem.UnitOFWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_CommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ordersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ordersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDTO dto)
        {
            var UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var order = new Order
            {
                OrderDate = dto.OrderDate,
                Status = "Pending",
                TotalAmount = 0,
                UserId = UserId,
                orderItems = new List<OrderItem>(),
            };

            foreach (var item in dto.orderItems)
            {
                var product = await unitOfWork.ProductRepo.GetById(item.ProductId);
                var orderitems = new OrderItem
                {
                    Quantity = item.Quantity,
                    ProductId = product.Id
                };

                order.orderItems.Add(orderitems);

                order.TotalAmount = product.Price * item.Quantity;
                product.StockQuantity -= item.Quantity;

            }
            await unitOfWork.OrderRepo.Create(order);
            unitOfWork.Save();

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await unitOfWork.OrderRepo.GetByIdd(id);
            if (order == null)
                return NotFound("Order not found");
            var dto = new GetOrderDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId,
                orderItems = order.orderItems.Select(c => new GetOrderItemDTO
                {
                    Id = c.Id,
                    Quantity = c.Quantity,
                    UnitPrice = c.UnitPrice,
                }).ToList()
            };
            return Ok(dto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, string status)
        {
            var order = await unitOfWork.OrderRepo.UpdateStatus(id, status);
            unitOfWork.OrderRepo.Update(order);
            unitOfWork.Save();
            return Ok(order);
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> Revenue()
        {
            var order = await unitOfWork.OrderRepo.Revenue();
            return Ok(order);
        }
    }
}
