using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Interfaces;
using Shopier.Application.UseCasess.CategoryServices;
using Shopier.Application.UseCasess.ProductServices;
using Shopier.Domain.Entities;
using System.Threading.Tasks;

namespace Shopier.WebApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryServices _categoryServices;
        public ProductController(IProductService productService, ICategoryServices categoryServices)
        {
            _productService = productService;
            _categoryServices = categoryServices;
        }

        // shop kısmı tasarımımız.
        public async Task<IActionResult> Index(int categoryId, decimal min, decimal max)
        {
            //  var product = await _productService.GetTakeAsync(6);  // 6 adet ürün getirmesini istiyrsak bu 
            //  var product = await _productService.GetByCategory(1);   // categoryıd sadece 1 olanları getirir.

            if (categoryId != 0)
            {
                var category = await _productService.GetByCategory(categoryId);
                return View(category);
            }
            if (max != 0)
            {
                var values2 = await _productService.GetProductByPrice(min, max);
                return View(values2);
            }
            var values = await _productService.GetAllProductAsync();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            if (search == null)
            {
                return View();
            }
            var values = await _productService.GetProductBySearch(search);
            return View(values);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var value = await _productService.GetByIdProductAsync(id);
            return View(value);
        }
    }
}

