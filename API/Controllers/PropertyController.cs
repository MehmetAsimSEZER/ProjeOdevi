using Application.Models.DTOs;
using Application.Services.PropertyService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await _propertyService.GetProperties();
            return Ok(properties);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostProperty([FromBody] CreatePropertyDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _propertyService.IsProductExsist(model.Name);

                if (name != false)
                {
                    ModelState.AddModelError(string.Empty, "The Property already exist..!");
                    return BadRequest(ModelState);
                }
                else
                {
                    await _propertyService.Create(model);
                    ModelState.AddModelError(String.Empty, "The Property has been created..!");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The Property hasn't been created..!");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            await _propertyService.Delete(id);
            return Ok();
        }
    }
}
