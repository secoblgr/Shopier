using Shopier.Application.Dtos.CartItemDtos;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.Dtos.CartDtos
{
    public class CreateCartDto
    {
       // public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }
        // public Customer? Customer { get; set; }
        public ICollection<CreateCartItemDto>? CartItems { get; set; }
    }
}
