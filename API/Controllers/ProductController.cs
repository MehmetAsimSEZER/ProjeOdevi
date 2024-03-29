﻿using Application.Models.DTOs;
using Application.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();

            return Ok(products);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetById(id);

            return Ok(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] CreateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var name = await _productService.IsProductExsist(model.ProductName);

                if (name != false)
                {
                    ModelState.AddModelError(String.Empty, "The Product already exist..!");
                    return BadRequest(ModelState);
                }
                else
                {
                    await _productService.Create(model);
                    ModelState.AddModelError(String.Empty, "The Product has been created..!");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The Product hasn't been created..!");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutProducts([FromBody] UpdateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                 await _productService.Update(model);
                 ModelState.AddModelError(String.Empty, "The Product has been modified..!");
                 return Ok(ModelState);

            }
            else
            {
                ModelState.AddModelError(String.Empty, "The Product hasn't been modified..!");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }
    }
}
