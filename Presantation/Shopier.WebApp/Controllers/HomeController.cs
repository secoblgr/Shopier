using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopier.Application.UseCasess.ProductServices;
using Shopier.WebApp.Models;

namespace Shopier.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var values = await _productService.GetTakeAsync(12);
        return View(values);
    }
}
