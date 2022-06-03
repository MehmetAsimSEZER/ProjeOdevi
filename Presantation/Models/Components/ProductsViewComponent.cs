
using Application.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Models.Components
{
    public class ProductsViewComponent:ViewComponent
    {
        private readonly IProductService _productService;

        public ProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _productService.GetProducts());
        }
    }
}
