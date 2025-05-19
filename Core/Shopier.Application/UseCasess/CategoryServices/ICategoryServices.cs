using Shopier.Application.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.CategoryServices
{
    public interface ICategoryServices
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(int id);
        Task CreateCategoryAsync(CreateCategoryDto model);
        Task UpdateCategoryAsync(UpdateCategoryDto model);
        Task DeleteCategoryAsync(int id);
    }
}
