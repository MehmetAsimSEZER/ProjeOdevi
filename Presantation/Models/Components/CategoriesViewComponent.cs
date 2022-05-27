using Application.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Models.Components
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _categoryService.GetCategories());
        }
    }
}
