using Shopier.Application.Dtos.CityDtos;
using Shopier.Application.Dtos.OrderDtos;
using Shopier.Application.Dtos.TownDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.OrderServices
{
    public interface IOrderServices
    {
        Task<List<ResultOrderDto>> GetAllOrderAsync();
        Task<GetByIdOrderDto> GetByIdOrderAsync(int id);
        Task CreateOrderAsync(CreateOrderDto model);
        Task UpdateOrderAsync(UpdateOrderDto model);
        Task DeleteOrderAsync(int id);
        Task <List<ResultCityDto>> GetAllCity();
        Task<List<ResultTownDto>> GetAllTownByCityId(int cityId);
    }
}
