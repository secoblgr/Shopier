using Shopier.Application.Dtos.CategoryDtos;
using Shopier.Application.Interfaces;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository<Category> _repository;

        public CategoryServices(IRepository<Category> repository)
        {
            _repository = repository;
        }

        async Task ICategoryServices.CreateCategoryAsync(CreateCategoryDto model)
        {
            await _repository.CreateAsync(new Category
            {
                CategoryName = model.CategoryName
            });
        }

        async Task ICategoryServices.UpdateCategoryAsync(UpdateCategoryDto model)
        {
            var category = await _repository.GetByIdAsync(model.CategoryId);
            category.CategoryName = model.CategoryName;
            await _repository.UpdateAsync(category);
        }
        async Task ICategoryServices.DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(category);
        }

        async Task<List<ResultCategoryDto>> ICategoryServices.GetAllCategoryAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(x => new ResultCategoryDto
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            }).ToList();
        }

        async Task<GetByIdCategoryDto> ICategoryServices.GetByIdCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            var newCategory = new GetByIdCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
            return newCategory;
        }

    }
}
