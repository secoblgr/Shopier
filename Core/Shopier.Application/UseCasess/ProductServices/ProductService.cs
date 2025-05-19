using Shopier.Application.Dtos.OrderDtos;
using Shopier.Application.Dtos.ProductDtos;
using Shopier.Application.Interfaces;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task CreateProductAsync(CreateProductDto model)
        {

            await _repository.CreateAsync(new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

    

        public async Task<GetByIdProductDto> GetByIdProductAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            var result = new GetByIdProductDto
            {
                ProductId = values.ProductId,
                ProductName = values.ProductName,
                Description = values.Description,
                Price = values.Price,
                Stock = values.Stock,
                ImageUrl = values.ImageUrl,
                CategoryId = values.CategoryId
            };
            return result;
        }

        public async Task<List<ResultProductDto>> GetTakeAsync(int count)
        {
            var values = await _repository.GetTakeAsync(count);
            return values.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task UpdateProductAsync(UpdateProductDto model)
        {
            var values = await _repository.GetByIdAsync(model.ProductId);
            values.ProductName = model.ProductName;
            values.Description = model.Description;
            values.Price = model.Price;
            values.Stock = model.Stock;
            values.ImageUrl = model.ImageUrl;
            values.CategoryId = model.CategoryId;

            await _repository.UpdateAsync(values);
        }
        public async Task<List<ResultProductDto>> GetByCategory(int categoryId)
        {
            var values = await _repository.GetByCategory(x => x.CategoryId == categoryId);
            return values.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task<List<ResultProductDto>> GetProductByPrice(decimal min, decimal max)
        {
            var values = await _repository.GetFilterByPrice(min,max);
            return values.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task<List<ResultProductDto>> GetProductBySearch(string search)
        {
            var values = await _repository.GetProductBysearch(search);
            return values.Select(x => new ResultProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }
    }
}
