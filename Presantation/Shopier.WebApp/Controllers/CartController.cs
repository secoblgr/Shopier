using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.CartDtos;
using Shopier.Application.Dtos.CartItemDtos;
using Shopier.Application.UseCasess.CartItemServices;
using Shopier.Application.UseCasess.CartServices;
using System.Threading.Tasks;

namespace Shopier.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;

        public CartController(ICartService cartService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
        }

        public async Task<IActionResult> Index(int id = 1)
        {
            var value = await _cartService.GetByIdCartAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<JsonResult> AddToCartItem([FromBody] CreateCartItemDto model)
        {
            try
            {
                model.CartId = 1;
                await _cartItemService.CreateCartItemAsync(model);
                var cart = await _cartService.GetByIdCartAsync(model.CartId);
                var total = cart.CartItems.Sum(item => item.TotalPrice);
                var newCart = new UpdateCartDto
                {
                    CartId = cart.CartId,
                    TotalAmount = total,
                    CartItems = cart.CartItems.Select(item => new UpdateCartItemDto
                    {
                        CartItemId = item.CartItemId,
                        ProductId = item.ProductId,
                        TotalPrice = item.TotalPrice,
                        Quantity = item.Quantity,

                    }).ToList()
                };
                await _cartService.UpdateCartAsync(newCart);
                return Json(new { success = true });
            }
            catch (Exception exception)
            {
                return Json(new { error = exception });
            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteCartItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Json(new { error = "product not found" });
                }
                var cartıItem = await _cartItemService.GetByIdCartItemAsync(id);
                if (cartıItem == null)
                {
                    return Json(new { error = "product not found" });
                }
                await _cartItemService.DeleteCartItemAsync(id);
                var cart = await _cartService.GetByIdCartAsync(cartıItem.CartId);
                var tempsum = cart.TotalAmount - cartıItem.TotalPrice;
                await _cartService.UpdateCartAsync(new UpdateCartDto
                {
                    CartId = cart.CartId,
                    TotalAmount = tempsum,
                    CartItems = cart.CartItems.Select(item => new UpdateCartItemDto
                    {
                        CartItemId = item.CartItemId,
                        ProductId = item.ProductId,
                        TotalPrice = item.TotalPrice,
                        Quantity = item.Quantity,
                    }).ToList()
                });
                return Json(new { success = true });

            }
            catch (Exception exception)
            {
                return Json(new { error = exception });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateQuantity([FromBody] UpdateCartItemDto dto)
        {
            try
            {
              
                await _cartItemService.UpdateCartItemAsync(new UpdateCartItemDto
                {
                    CartId = dto.CartId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    CartItemId =dto.CartItemId,
                });
                return Json(new { success = true });
            }
            catch (Exception exception)
            {
                return Json(new { error = exception });
            }
        }
    }
}
