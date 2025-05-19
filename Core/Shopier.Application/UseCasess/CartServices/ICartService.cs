using Shopier.Application.Dtos.CartDtos;
using Shopier.Application.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.CartServices
{
   public interface ICartService
    {
        Task<List<ResultCartDto>> GetAllCartAsync();
        Task<GetByIdCartDto> GetByIdCartAsync(int id);
        Task CreateCartAsync(CreateCartDto model);
        Task UpdateCartAsync(UpdateCartDto model);
        Task DeleteCartAsync(int id);

    }
}
