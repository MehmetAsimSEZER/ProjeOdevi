using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presantation.Areas.Admin.Models.VMs;
using System.ComponentModel.DataAnnotations;

namespace Presantation.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
                              UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult List()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole ıdentityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(ıdentityRole);               

                if (result.Succeeded)
                {
                    TempData["Success"] = "The role has been created..!";
                    return RedirectToAction("List");
                }
             
                foreach (IdentityError error in result.Errors)
                {
                    TempData["Error"] = $"{error.Description}";
                }               
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                TempData["Error"] = $"Role with Id = {id} cannot be found";
                return View();

            }
            var model = new UpdateRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            foreach (var user in _userManager.Users)
            {
                if (ModelState.IsValid)
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                TempData["Error"] = $"Role with Id = {model.Id} cannot be found";
                return View();

            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }

                foreach (var error in result.Errors)
                {
                    TempData["Error"] = $"{error.Description}";
                }
                return View(model);
            }           
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                TempData["Error"] = $"Role with Id = {roleId} cannot be found";
                return View();
            }

            var model = new List<UserRoleViewModel>();

            IQueryable<AppUser> users = _userManager.Users;
            List<AppUser> appUsers = users.ToList();
            foreach (var user in appUsers)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                bool isInRole = await _userManager.IsInRoleAsync(user, role.Name);

                if (isInRole)
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                TempData["Error"] = $"Role with Id = {roleId} cannot be found";
                return View();
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected)
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Update", new { Id = roleId });
                }
            }
            return RedirectToAction("Update", new { Id = roleId });
        }
    }
}
