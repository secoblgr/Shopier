using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.CartItemDtos;
using Shopier.Application.UseCasess.CartItemServices;

namespace Shopier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _service;

        public CartItemController(ICartItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItem()
        {
            var values = await _service.GetAllCartItemAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCartItem(int id)
        {
            var values = await _service.GetByIdCartItemAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem(CreateCartItemDto dto)
        {
            await _service.CreateCartItemAsync(dto);
            return Ok("Cart Item Succesfully Created!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDto dto)
        {
            await _service.UpdateCartItemAsync(dto);
            return Ok("Cart Item Successfully Updated !");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            await _service.DeleteCartItemAsync(id);
            return Ok("Cart Item Successfully Deleted !");
        }
    }
}
