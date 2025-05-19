using Shopier.Application.Dtos.OrderItemDtos;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {

      
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }

        //  public string BillingAdress { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public string ShippingAdress { get; set; }
        //public string PaymentMethod { get; set; }
        public int CustomerId { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
