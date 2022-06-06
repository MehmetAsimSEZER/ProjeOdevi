using Application.Services.CategoryService;
using Application.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService,
                                ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> ProductByCategory(string categoryName)
        {
            var category = await _categoryService.GetByName(categoryName);

            if (categoryName == null)
                return RedirectToAction("Index");

            var products = await _productService.GetProductByCategory(category.Id);

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetById(id);

            return View(product);
        }

    }
}
