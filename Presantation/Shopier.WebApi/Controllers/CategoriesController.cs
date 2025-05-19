using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.CategoryDtos;
using Shopier.Application.UseCasess.CategoryServices;

namespace Shopier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var category = await _categoryServices.GetByIdCategoryAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDto dto)
        {
            await _categoryServices.CreateCategoryAsync(dto);
            return Ok("Category Succesfully Created !");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            await _categoryServices.UpdateCategoryAsync(dto);
            return Ok("Category Succesfuly Updated !");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory (int id) 
        {
            await _categoryServices.DeleteCategoryAsync(id);
            return Ok("Category Succesfuly Deleted !");
        }
    }
}
