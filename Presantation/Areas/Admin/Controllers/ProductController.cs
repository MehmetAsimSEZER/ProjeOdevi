using Application.Models.DTOs;
using Application.Services.CategoryService;
using Application.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(ICategoryService categoryService,
                                IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IActionResult> Create()
        {
            CreateProductDTO model = new CreateProductDTO();
            model.Categories = await _categoryService.GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(model);
                TempData["Success"] = $"The {model.ProductName} has been added..!";
                return RedirectToAction("List");

            }
            else
            {
                TempData["Error"] = $"The product hasn't been added..!";
                return RedirectToAction("Create");
            }
        }


        public async Task<IActionResult> List()
        {
            return View(await _productService.GetProducts());
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _productService.GetById(id);
            return View(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(model);
                TempData["Success"] = $"The {model.ProductName} has been updated..!";
                return RedirectToAction("List");

            }
            else
            {
                TempData["Error"] = $"The product hasn't been updated..!";
                return RedirectToAction("Update");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
