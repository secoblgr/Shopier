using Shopier.Application.Dtos.CityDtos;
using Shopier.Application.Dtos.OrderDtos;
using Shopier.Application.Dtos.OrderItemDtos;
using Shopier.Application.Dtos.TownDtos;
using Shopier.Application.Interfaces;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.OrderServices
{
    public class OrderServices : IOrderServices 
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<OrderItem> _repositoryOrderItem;
        private readonly IRepository<Customer> _repositoryCustomer;
        private readonly IRepository<Product> _repositoryProduct;

        public OrderServices(IRepository<Order> repository, IRepository<OrderItem> repositoryOrderItem, IRepository<Customer> repositoryCustomer, IRepository<Product> repositoryProduct)
        {
            _repository = repository;
            _repositoryOrderItem = repositoryOrderItem;
            _repositoryCustomer = repositoryCustomer;
            _repositoryProduct = repositoryProduct;
        }

        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            decimal sum = 0;
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderStatus = model.OrderStatus,
                ShippingAdress = model.ShippingAdress,
                CityId = model.CityId,
                TownId = model.TownId,
                TotalAmount = sum,
                CustomerId = model.CustomerId,
                CustomerEmail = model.CustomerEmail,
                CustomerName = model.CustomerName,
                CustomerSurname = model.CustomerSurname,
                CustomerPhone = model.CustomerPhone,

            };
            await _repository.CreateAsync(order);

            foreach (var item in model.OrderItems)
            {
                await _repositoryOrderItem.CreateAsync(new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                });
                sum = sum + item.TotalPrice;
            }
            order.TotalAmount = sum;
            await _repository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            foreach (var item in values.OrderItems)
            {
                var orderItem = await _repositoryOrderItem.GetByIdAsync(item.OrderItemId);
                await _repositoryOrderItem.DeleteAsync(orderItem);
            }
            await _repository.DeleteAsync(values);
        }



        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var values = await _repository.GetAllAsync();
            var orderItems = await _repositoryOrderItem.GetAllAsync();
            var result = new List<ResultOrderDto>();
            foreach (var item in values)
            {
                var customer = await _repositoryCustomer.GetByIdAsync(item.CustomerId);
                var orderdto = new ResultOrderDto
                {
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    OrderStatus = item.OrderStatus,
                    ShippingAdress = item.ShippingAdress,
                    CityId = item.CityId,
                    TownId = item.TownId,
                    CustomerId = item.CustomerId,
                    CustomerEmail = item.CustomerEmail,
                    CustomerName = item.CustomerName,
                    CustomerSurname = item.CustomerSurname,
                    CustomerPhone = item.CustomerPhone,
                    OrderItems = new List<ResultOrderItemDto>(),
                };
                foreach (var item1 in orderItems)
                {
                    var product = await _repositoryProduct.GetByIdAsync(item1.ProductId);
                    var productdto = new ResultOrderItemDto
                    {
                        OrderId = item1.OrderId,
                        ProductId = item1.ProductId,
                        Quantity = item1.Quantity,
                        TotalPrice = item1.TotalPrice,
                        OrderItemId = item1.OrderItemId,
                        Product = product,
                    };
                    orderdto.OrderItems.Add(productdto);
                }
                result.Add(orderdto);
            }
            return result;
        }


        public async Task<GetByIdOrderDto> GetByIdOrderAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            var customer = await _repositoryCustomer.GetByIdAsync(values.CustomerId);

            var result = new GetByIdOrderDto
            {
                OrderId = values.OrderId,
                OrderDate = values.OrderDate,
                OrderStatus = values.OrderStatus,
                ShippingAdress = values.ShippingAdress,
                CityId = values.CityId,
                TownId = values.TownId,
                CustomerId = values.CustomerId,
                CustomerEmail = values.CustomerEmail,
                CustomerName = values.CustomerName,
                CustomerSurname = values.CustomerSurname,
                CustomerPhone = values.CustomerPhone,
                OrderItems = new List<ResultOrderItemDto>(),
            };

            foreach (var item in result.OrderItems)
            {
                var orderitemproduct = await _repositoryProduct.GetByIdAsync(item.ProductId);
                var orderitemdto = new ResultOrderItemDto
                {

                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    OrderItemId = item.OrderItemId,
                    Product = orderitemproduct,
                };
                result.OrderItems.Add(orderitemdto);
            }
            return result;
        }

        public async Task UpdateOrderAsync(UpdateOrderDto model)
        {
            var values = await _repository.GetByIdAsync(model.OrderId);
            var orderitems = await _repositoryOrderItem.GetAllAsync();
            values.OrderStatus = model.OrderStatus;
            decimal sum = 0;

            foreach (var item in model.OrderItems)
            {
                foreach (var item1 in values.OrderItems)
                {
                    var orderItemdto = await _repositoryOrderItem.GetByIdAsync(item1.OrderItemId);
                    if (item.OrderItemId == item1.OrderId)
                    {
                        orderItemdto.Quantity = item.Quantity;
                        orderItemdto.TotalPrice = item.TotalPrice;
                    }
                    sum = sum + item1.TotalPrice;
                }
            }
            values.TotalAmount = sum;
            await _repository.UpdateAsync(values);
        }

        public async Task<List<ResultCityDto>> GetAllCity()
        {
            var value = await _repository.GetCity();
            return value.Select(x => new ResultCityDto
            {
                Id = x.Id,
                CityId = x.CityId,
                CityName = x.CityName,

            }).ToList();
        }

        public async Task<List<ResultTownDto>> GetAllTownByCityId(int cityId)
        {
            var value = await _repository.GetTownByCityId(cityId);

            return value.Select(x => new ResultTownDto
            {
                Id = x.Id,
                CityId = x.CityId,
                TownId = x.TownId,
                TownName = x.TownName,

            }).ToList();
        }
    }
}
