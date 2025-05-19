using Shopier.Application.Dtos.OrderItemDtos;
using Shopier.Application.Interfaces;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.OrderItemServices
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _repository;

        public OrderItemService(IRepository<OrderItem> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrderItemAsync(CreateOrderItemDto model)
        {
            await _repository.CreateAsync(new OrderItem
            {
              //  OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalPrice = model.TotalPrice,

            });
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultOrderItemDto>> GetAllOrderItemAsync()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new ResultOrderItemDto
            {
                OrderId = x.OrderId,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice,
                ProductId = x.ProductId,
                OrderItemId = x.OrderItemId,
            }).ToList();
        }

        public async Task<GetByIdOrderItemDto> GetByIdOrderItemAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            var result = new GetByIdOrderItemDto
            {
                ProductId = values.ProductId,
                OrderId = values.OrderId,
                Quantity = values.Quantity,
                TotalPrice = values.TotalPrice,
                OrderItemId = values.OrderId,
            };
            return result;
        }

        public async Task UpdateOrderItemAsync(UpdateOrderItemDto model)
        {
            var values = await _repository.GetByIdAsync(model.OrderItemId);
          //  values.OrderId = model.OrderId;
            values.Quantity = model.Quantity;
            values.TotalPrice = model.TotalPrice;
            values.ProductId = model.ProductId;
            await _repository.UpdateAsync(values);
        }
    }
}
