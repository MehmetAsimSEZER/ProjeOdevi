using Application.Models.DTOs;
using Application.Services.ParentCategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParentCategoryController : ControllerBase
    {
        private readonly IParentCategoryService _parentCategoryService;

        public ParentCategoryController(IParentCategoryService parentCategoryService)
        {
            _parentCategoryService = parentCategoryService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var name = await _parentCategoryService.GetParentCategories();

            return Ok(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateParentCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _parentCategoryService.IsParentCategoryExsist(model.Name);

                if (name != false)
                {
                    ModelState.AddModelError(String.Empty, "The Parentcategory already exist..!");
                    return BadRequest(ModelState);
                }

                else
                {
                    await _parentCategoryService.Create(model);
                    ModelState.AddModelError(String.Empty, "The Parentcategory has been created..!");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The Parentcategory hasn't been created..!");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateParentCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                await _parentCategoryService.Update(model);
                ModelState.AddModelError(String.Empty, "The Parentcategory has been modified..!");
                return Ok(ModelState);
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The Parentcategory hasn't been modified..!");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _parentCategoryService.Delete(id);
            return Ok();
        }
    }
}
