using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.CartDtos;
using Shopier.Application.UseCasess.CartServices;

namespace Shopier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            var values = await _service.GetAllCartAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCart(int id)
        {
            var values = await _service.GetByIdCartAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCart(CreateCartDto dto)
        {
            await _service.CreateCartAsync(dto);
            return Ok("Successfully Card Added !");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart (UpdateCartDto dto)
        {
            await _service.UpdateCartAsync(dto);
            return Ok("Successfully Updated Card !");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _service.DeleteCartAsync(id);
            return Ok("Successfully Deleted Card !");
        }
    }
}
