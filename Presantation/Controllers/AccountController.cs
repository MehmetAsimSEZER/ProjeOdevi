using Application.Extensions;
using Application.Models.DTOs;
using Application.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presantation.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                    TempData["Warning"] = $"{item.Description}";
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LogIn(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                TempData["Warning"] = "Invalid log in credantial..!";
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userService.LogOut();

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(string userName)
        {
            if (userName == User.Identity.Name)
            {
                var user = await _userService.GetById(User.GetUserId());

                if (user == null)
                    return NotFound();

                return View(user);
            }
            else
                return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserDTO model)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(model);
                TempData["Success"] = "Your profile has been edited..!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been edited..!";
                return RedirectToAction("Edit");
            }
        }
    }
}
