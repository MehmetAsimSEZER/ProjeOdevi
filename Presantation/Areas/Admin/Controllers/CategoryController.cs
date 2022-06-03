using Application.DTOs;
using Application.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _categoryService.IsCategoryExsist(model.CategoryName);

                if (name != false)
                {
                    TempData["Warning"] = $"The {model.CategoryName} category already exist..!";
                    return View(model);
                }
                else
                {
                    await _categoryService.Create(model);
                    TempData["Success"] = $"The {model.CategoryName} has been added..!";
                    return RedirectToAction("List");
                }
            }
            else
            {
                TempData["Error"] = $"The category hasn't been added..!";
                return View(model);
            }
        }


        public async Task<IActionResult> List()
        {
            return View(await _categoryService.GetCategories());
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetById(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(model);
                TempData["Success"] = $"The {model.CategoryName} has been updated..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = $"The category hasn't been updated..!";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
