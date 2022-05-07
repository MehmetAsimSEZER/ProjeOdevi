using Application.DTOs;
using Application.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();

            return Ok(categories);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _categoryService.GetById(id);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategories([FromBody] CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _categoryService.IsCategoryExsist(model.CategoryName);

                if (name != false)
                {
                    ModelState.AddModelError(String.Empty, "The category already exist..!");
                    return BadRequest(ModelState);
                }

                else
                {
                    await _categoryService.Create(model);
                    ModelState.AddModelError(String.Empty, "The category has been created..!");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The category hasn't been created..!");
                return BadRequest(ModelState);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutCategories([FromBody] UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _categoryService.IsCategoryExsist(model.CategoryName);

                if (name != false)
                {
                    ModelState.AddModelError(String.Empty, "The category already exist..!");
                    return BadRequest(ModelState);
                }

                else
                {
                    await _categoryService.Update(model);
                    ModelState.AddModelError(String.Empty, "The category has been modified..!");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The category hasn't been modified..!");
                return BadRequest(ModelState);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategories(Guid id)
        {
            await _categoryService.Delete(id);
            return Ok();
        }
    }
}
