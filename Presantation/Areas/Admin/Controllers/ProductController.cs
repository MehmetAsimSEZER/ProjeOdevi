using Application.Models.DTOs;
using Application.Services.CategoryService;
using Application.Services.ProductPropertyService;
using Application.Services.ProductService;
using Application.Services.PropertyService;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IPropertyService _propertyService;
        private readonly IProductPropertyService _productPropertyService;
        public ProductController(ICategoryService categoryService,
                                IProductService productService, 
                                IPropertyService propertyService, 
                                IProductPropertyService productPropertyService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _propertyService = propertyService;
            _productPropertyService = productPropertyService;
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
                if (model.CategoryId == -1)
                {
                    TempData["Error"] = $"The product hasn't been added..!";
                    return RedirectToAction("Create");
                }
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
            UpdateProductDTO model = new UpdateProductDTO();
            model.Categories = await _categoryService.GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Error"] = $"The product hasn't been updated..!";
                return RedirectToAction("Update");

            }
            else
            {
                await _productService.Update(model);
                TempData["Success"] = $"The {model.ProductName} has been updated..!";
                return RedirectToAction("List");

            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("List");
        }


        public async Task<IActionResult> CreateProperty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreatePropertyDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _propertyService.IsPropertyExsist(model.PropertyName);

                if (name != false)
                {
                    TempData["Warning"] = $"The {model.PropertyName} Property already exist..!";
                    return View(model);
                }
                else
                {
                    await _propertyService.Create(model);
                    TempData["Success"] = $"The {model.PropertyName} has been added..!";
                    return RedirectToAction("ListProperty");
                }
            }
            else
            {
                TempData["Error"] = $"The Property hasn't been added..!";
                return View(model);
            }
        }

        public async Task<IActionResult> ListProperty()
        {
            return View(await _propertyService.GetProperties());
        }

        public async Task<IActionResult> UpdateProperty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProperty(UpdatePropertyDTO model)
        {

            bool propertyExsist = await _propertyService.IsPropertyExsist(model.PropertyName);

            if (propertyExsist)
            {
                TempData["Warning"] = $"The {model.PropertyName} Property already exist..!";
                return View(model);
            }
            else
            {
                await _propertyService.Update(model);
                TempData["Success"] = $"The {model.PropertyName} has been updated..!";
                return RedirectToAction("ListProperty");
            }

        }

        public async Task<IActionResult> DeleteProperty(int id)
        {
            await _propertyService.Delete(id);
            return RedirectToAction("ListProperty");
        }


        public async Task<IActionResult> CreateProductProperty()
        {
            CreateProductPropertyDTO model = new CreateProductPropertyDTO();
            model.Properties = await _propertyService.GetProperties();
            model.Products = await _productService.GetProducts();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProductProperty(CreateProductPropertyDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.PropertyId == -1 || model.ProductId == -1)
                {
                    TempData["Error"] = $"The product hasn't been added..!";
                    return RedirectToAction("CreateProductProperty");
                }
                await _productPropertyService.Create(model);
                TempData["Success"] = $"The {model.Value} has been added..!";
                return RedirectToAction("ListProductProperty");

            }
            else
            {
                TempData["Error"] = $"The product hasn't been added..!";
                return RedirectToAction("CreateProductProperty");
            }
        }

        public async Task<IActionResult> ListProductProperty()
        {
            return View(await _productPropertyService.Get());
        }


        public async Task<IActionResult> UpdateProductProperty(int id)
        {
            UpdateProductPropertyDTO model = new UpdateProductPropertyDTO();
            model.Properties = await _propertyService.GetProperties();
            model.Products = await _productService.GetProducts();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductProperty(UpdateProductPropertyDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Error"] = $"The product hasn't been updated..!";
                return RedirectToAction("UpdateProductProperty");

            }
            else
            {
                await _productPropertyService.Update(model);
                TempData["Success"] = $"The {model.Value} has been updated..!";
                return RedirectToAction("ListProductProperty");

            }
        }

        public async Task<IActionResult> DeleteProductProperty(int id)
        {
            await _productPropertyService.Delete(id);
            return RedirectToAction("ListProductProperty");
        }
    }
}
