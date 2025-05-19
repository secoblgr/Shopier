using Shopier.Application.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.CustomerServices
{
    public interface ICustomerServices
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task<GetByIdCustomerDto> GetByIdCustomerAsync(int id);
        Task CreateCustomerAsync(CreateCustomerDto model);
        Task UpdateCustomerAsync(UpdateCustomerDto model);
        Task DeleteCustomerAsync(int id);
    }
}
