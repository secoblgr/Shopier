using Shopier.Application.Dtos.CartDtos;
using Shopier.Application.Dtos.CartItemDtos;
using Shopier.Application.Dtos.ProductDtos;
using Shopier.Application.Interfaces;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.CartServices
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _repository;
        private readonly IRepository<CartItem> _repositoryItem;
        private readonly IRepository<Customer> _repositorCustomer;
        private readonly IRepository<Product> _repositoryProduct;
        public CartService(IRepository<Cart> repository, IRepository<CartItem> repositoryItem, IRepository<Customer> repositorCustomer, IRepository<Product> repositoryProduct)
        {
            _repository = repository;
            _repositoryItem = repositoryItem;
            _repositorCustomer = repositorCustomer;
            _repositoryProduct = repositoryProduct;
        }

        public async Task CreateCartAsync(CreateCartDto model)
        {
            var cart = new Cart()
            {

                CreatedDate = DateTime.Now,
                CustomerId = model.CustomerId,
            };
            await _repository.CreateAsync(cart);
            var sum = 0;

            foreach (var item in model.CartItems)
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                };
                sum += (item.TotalPrice);
                await _repositoryItem.CreateAsync(cartItem);
            }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            var carItems = await _repositoryItem.GetAllAsync();
            foreach (var item in carItems)
            {
                if (item.CartId == id)
                {
                    var cartItem = await _repositoryItem.GetByIdAsync(item.CartItemId);
                    await _repositoryItem.DeleteAsync(cartItem);
                }
            }
            await _repository.DeleteAsync(cart);
        }

        public async Task<List<ResultCartDto>> GetAllCartAsync()
        {
            var carts = await _repository.GetAllAsync();
            var cartItems = await _repositoryItem.GetAllAsync();
            var product = await _repositoryProduct.GetAllAsync();
            var result = new List<ResultCartDto>();

            foreach (var item in carts)
            {
                var customerdto = await _repositorCustomer.GetByFilterAsync(cus => cus.CustomerId == item.CustomerId);
                var cartdto = new ResultCartDto
                {
                    CartId = item.CartId,
                    CreatedDate = item.CreatedDate,
                    CustomerId = item.CustomerId,
                    Customer = customerdto,
                    TotalAmount = item.TotalAmount,
                    CartItems = new List<ResultCartItemDto>()

                };
                foreach (var item1 in item.CartItems)
                {
                    var productdto = await _repositoryProduct.GetByFilterAsync(prd => prd.ProductId == item1.ProductId);
                    var cartıtemdto = new ResultCartItemDto
                    {
                        CartId = item1.CartId,
                        CartItemId = item1.CartItemId,
                        ProductId = item1.ProductId,
                        Product = productdto,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                    };
                    cartdto.CartItems.Add(cartıtemdto);
                }
                result.Add(cartdto);
            }
            return result;
        }

        public async Task<GetByIdCartDto> GetByIdCartAsync(int id)
        {
            var cart = await _repository.GetByIdAsync(id);
            var cartItem = await _repositoryItem.GetAllAsync();
            var customer = await _repositorCustomer.GetByIdAsync(id);

            var result = new GetByIdCartDto
            {
                CartId = cart.CartId,
                CartItems = new List<ResultCartItemDto>(),
                CreatedDate = cart.CreatedDate,
                CustomerId = cart.CustomerId,
                Customer = customer,
                TotalAmount = cart.TotalAmount,
            };
            foreach (var item1 in cart.CartItems)
            {
                var productdto = await _repositoryProduct.GetByFilterAsync(prd => prd.ProductId == item1.ProductId);
                var cartıtemdto = new ResultCartItemDto
                {
                    CartId = item1.CartId,
                    CartItemId = item1.CartItemId,
                    ProductId = item1.ProductId,
                    Product = productdto,
                    Quantity = item1.Quantity,
                    TotalPrice = item1.TotalPrice,
                };
                result.CartItems.Add(cartıtemdto);
            }
            return result;
        }

        public async Task UpdateCartAsync(UpdateCartDto model)
        {
            var cart = await _repository.GetByIdAsync(model.CartId);
            var cartItems = await _repositoryItem.GetAllAsync();
            var sum = 0;
            foreach (var item1 in model.CartItems)

                foreach (var item in cart.CartItems)
                {
                    var cartItem = await _repositoryItem.GetByIdAsync(item.CartItemId);
                    {
                        if (item.CartItemId == item1.CartItemId)
                        {
                            cartItem.Quantity = item1.Quantity;
                            cartItem.TotalPrice = item1.TotalPrice;
                        }
                        sum = sum + item.TotalPrice;
                    }
                }
            cart.TotalAmount = sum;
            await _repository.UpdateAsync(cart);
        }

    }
}
