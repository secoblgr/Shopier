using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.CartItemDtos;
using Shopier.Application.Dtos.OrderDtos;
using Shopier.Application.Dtos.OrderItemDtos;
using Shopier.Application.UseCasess.CartServices;
using Shopier.Application.UseCasess.OrderServices;
using Shopier.Domain.Entities;
using System.Threading.Tasks;

namespace Shopier.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices orderServices;
        private readonly ICartService cartService;

        public OrderController(IOrderServices orderServices, ICartService cartService)
        {
            this.orderServices = orderServices;
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckOut(int cartId)
        {
            var value = await cartService.GetByIdCartAsync(cartId);
            if (value == null)
            {
                return View("values null");
            }
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder( CreateOrderDto dto, int cartId)
        {
            var cart = await cartService.GetByIdCartAsync(cartId);
            List<CreateOrderItemDto> result = new List<CreateOrderItemDto>();
            foreach (var item in cart.CartItems)
            {
                var oidto = new CreateOrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                };
                result.Add(oidto);
            }
            
            dto.CustomerId = 1;
            dto.OrderItems = result;
            dto.OrderStatus = "Order accepted !";
            await orderServices.CreateOrderAsync(dto);
            return Json(new {success=true});
        }

        public async Task<IActionResult> GetCity()
        {
            var values = await orderServices.GetAllCity();
            return Json(new { success = true, data = values });
        }
        public async Task<IActionResult> GetTownByCityId(int cityId)
        {
            var values = await orderServices.GetAllTownByCityId(cityId);
            return Json(new { success = true, data = values });
        }
    }
}
