using Application.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Presantation.Models;
using System.Diagnostics;

namespace Presantation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DiscountedProducts()
        {
            var discountProducts = await _productService.GetDiscountProducts();
            return View(discountProducts);
        }       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}