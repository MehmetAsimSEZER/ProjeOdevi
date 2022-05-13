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
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _productPropertyService.Get();
            return Ok(value);
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
                await _productPropertyService.Create(model);
                ModelState.AddModelError(string.Empty, "The ProductProperty has been created..!");
                return Ok(ModelState);
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
                await _productPropertyService.Update(model);
                ModelState.AddModelError(string.Empty, "The ProductProperty has been created..!");
                return Ok(ModelState);
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productPropertyService.Delete(id);
            return Ok();
        }
    }
}
