using Application.DTOs;
using Application.Models.DTOs;
using Application.Services.CategoryService;
using Application.Services.ParentCategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IParentCategoryService _parentCategoryService;

        public CategoryController(ICategoryService categoryService, IParentCategoryService parentCategoryService)
        {
            _categoryService = categoryService;
            _parentCategoryService = parentCategoryService;
        }

        public async Task<IActionResult> Create()
        {
            CreateCategoryDTO model = new CreateCategoryDTO();
            model.parentCategories = await _parentCategoryService.GetParentCategories();

            return View(model);
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
            UpdateCategoryDTO model = new UpdateCategoryDTO();
            model.parentCategories = await _parentCategoryService.GetParentCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO model)
        {

             bool categoryExists = await _categoryService.IsCategoryExsist(model.CategoryName);
             
             if (categoryExists)
             {
                 TempData["Warning"] = $"The {model.CategoryName} category already exist..!";
                 return View(model);
             }
             else
             {
                 await _categoryService.Update(model);
                 TempData["Success"] = $"The {model.CategoryName} has been updated..!";
                 return RedirectToAction("List");
             }

        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("List");
        }

        public IActionResult CreateParent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateParent(CreateParentCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _parentCategoryService.IsParentCategoryExsist(model.Name);

                if (name != false)
                {
                    TempData["Warning"] = $"The {model.Name} parentCategory already exist..!";
                    return View(model);
                }
                else
                {
                    await _parentCategoryService.Create(model);
                    TempData["Success"] = $"The {model.Name} has been added..!";
                    return RedirectToAction("ListParent");
                }
            }
            else
            {
                TempData["Error"] = $"The Parent Category hasn't been added..!";
                return View(model);
            }
        }


        public async Task<IActionResult> ListParent()
        {
            return View(await _parentCategoryService.GetParentCategories());
        }

        public async Task<IActionResult> UpdateParent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateParent(UpdateParentCategoryDTO model)
        {

             bool parentExsist = await _parentCategoryService.IsParentCategoryExsist(model.Name);
             
             if (parentExsist)
             {
                 TempData["Warning"] = $"The {model.Name} parentCategory already exist..!";
                 return View(model);
             }
             else
             {
                 await _parentCategoryService.Update(model);
                 TempData["Success"] = $"The {model.Name} has been updated..!";
                 return RedirectToAction("ListParent");
             }

        }

        public async Task<IActionResult> DeleteParent(int id)
        {
            await _parentCategoryService.Delete(id);
            return RedirectToAction("ListParent");
        }
    }
}
