using Shopier.Application.Dtos.CustomerDtos;
using Shopier.Application.Interfaces;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.CustomerServices
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IRepository<Customer> _repository;

        public CustomerServices(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto model)
        {
            await _repository.CreateAsync(new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            });
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(values);

        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var values = await _repository.GetAllAsync();

            return values.Select(x => new ResultCustomerDto
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
               // Orders = x.Orders,
            }).ToList();
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);

            return new GetByIdCustomerDto
            {
                CustomerId = values.CustomerId,
                FirstName = values.FirstName,
                LastName = values.LastName,
                Email = values.Email,
              //  Orders = values.Orders,
            };
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto model)
        {
            var values = await _repository.GetByIdAsync(model.CustomerId);
            values.FirstName = model.FirstName;
            values.LastName = model.LastName;
            values.Email = model.Email;
            //values.Orders = model.Orders;
            await _repository.UpdateAsync(values);
        }
    }
}
