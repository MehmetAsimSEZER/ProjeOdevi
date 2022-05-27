using Application.Models.DTOs;
using Application.Services.UserService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userService.GetUsers();

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetById(id);

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUsers(CreateUserDTO user)
        {
            if (ModelState.IsValid)
            {
                var Email = await _userService.IsUserExsist(user.Email);

                if (Email != false)
                {
                    ModelState.AddModelError(string.Empty, "The Users already exist");
                    return BadRequest(ModelState);
                }
                else
                {
                    await _userService.Create(user);
                    ModelState.AddModelError(String.Empty, "The Users has been Created");
                    return Ok(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The Users hasn't been Created");
                return BadRequest(ModelState);
            }
                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutUsers([FromBody] UpdateUserDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Update(user);
                ModelState.AddModelError(String.Empty, "The Users has been modified..!");
                return Ok(ModelState);

            }
            else
            {
                ModelState.AddModelError(String.Empty, "The Users hasn't been modified..!");
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
