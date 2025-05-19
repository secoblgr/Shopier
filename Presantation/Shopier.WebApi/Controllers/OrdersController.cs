using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.OrderDtos;
using Shopier.Application.UseCasess.OrderServices;

namespace Shopier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _services;

        public OrdersController(IOrderServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var values = await _services.GetAllOrderAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var values = await _services.GetByIdOrderAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            await _services.CreateOrderAsync(dto);
            return Ok("Order Successfully Created !");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            await _services.UpdateOrderAsync(dto);
            return Ok("Order SuccessFully Updated !");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _services.DeleteOrderAsync(id);
            return Ok("Order Successfully Deleted !");
        }
    }
}
