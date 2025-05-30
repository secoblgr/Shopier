﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.CustomerDtos;
using Shopier.Application.UseCasess.CustomerServices;

namespace Shopier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _services;

        public CustomersController(ICustomerServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var values = await _services.GetAllCustomerAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCustomer (int id) 
        {
            var values = await _services.GetByIdCustomerAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            await _services.CreateCustomerAsync(dto);
            return Ok("Customer Succesfully Created !");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto dto)
        {
            await _services.UpdateCustomerAsync(dto);
            return Ok("Customer Succesfully updated !");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _services.DeleteCustomerAsync(id);
            return Ok("Customer Succesfuly Deleted !");
        }

    }
}
