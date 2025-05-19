using Shopier.Application.Dtos.OrderDtos;
using Shopier.Application.Dtos.ProductDtos;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<GetByIdProductDto> GetByIdProductAsync(int id);
        Task CreateProductAsync(CreateProductDto model);
        Task UpdateProductAsync(UpdateProductDto model);
        Task DeleteProductAsync(int id);
        Task<List<ResultProductDto>> GetTakeAsync(int count);
        Task<List<ResultProductDto>> GetByCategory(int categoryId);
        Task<List<ResultProductDto>> GetProductByPrice(decimal min, decimal max);

        Task<List<ResultProductDto>> GetProductBySearch(string search);
    }
}
