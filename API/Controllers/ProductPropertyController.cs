using Application.Models.DTOs;
using Application.Services.ProductPropertyService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPropertyController : ControllerBase
    {
        private readonly IProductPropertyService _productPropertyService;

        public ProductPropertyController(IProductPropertyService productPropertyService)
        {
            _productPropertyService = productPropertyService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductPropertyDTO model)
        {
            if (ModelState.IsValid)
            {
                var value = await _productPropertyService.IsProductExsist(model.Value);

                if (value != false)
                {
                    ModelState.AddModelError(string.Empty, "The ProductProperty already exist..!");
                    return BadRequest(ModelState);
                }
                else
                {
                    await _productPropertyService.Create(model);
                    ModelState.AddModelError(string.Empty, "The ProductProperty has been created..!");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The ProductProperty hasn't been created..!");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductPropertyDTO model)
        {
            if (ModelState.IsValid)
            {
                var value = await _productPropertyService.IsProductExsist(model.Value);

                if (value != false)
                {
                    ModelState.AddModelError(String.Empty, "The ProductProperty already exist..!");
                    return BadRequest(ModelState);
                }
                else
                {
                    await _productPropertyService.Update(model);
                    ModelState.AddModelError(String.Empty, "The ProductProperty has been modified..!");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The ProductProperty hasn't been modified..!");
                return BadRequest(ModelState);
            }
        }
    }
}
