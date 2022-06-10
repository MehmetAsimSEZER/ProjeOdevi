using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presantation.Areas.Admin.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
                              UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult List()
        {
            var roles = this._roleManager.Roles;
            return View(roles);
        }

        public IActionResult Create() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([MinLength(3, ErrorMessage = "Minimum lenght is 3"),
                                                 Required(ErrorMessage = "Must to type role name")] string roleName)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (result.Succeeded)
                {
                    TempData["Success"] = "The role has been created..!";
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        TempData["Error"] = $"{error.Description}";
                    }
                }
            }

            return RedirectToAction("Create");
        }


        public async Task<IActionResult> Delete(IdentityRole role)
        {
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("List");
        }


        public async Task<IActionResult> AssignedToUser(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<User> hasRole = new List<User>();
            List<User> hasNoRole = new List<User>();

            foreach (User user in _userManager.Users)
            {
                bool exists = await _userManager.IsInRoleAsync(user, role.Name);

                if (exists)
                {
                    hasRole.Add(user);
                }
                else
                {
                    hasNoRole.Add(user);
                }
            }

            AssignedRoleToUserDTO model = new AssignedRoleToUserDTO()
            {
                RoleName = role.Name,
                Role = role,
                HasRole = hasRole,
                HasNoRole = hasNoRole
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignedToUser(AssignedRoleToUserDTO model)
        {
            IdentityResult result;

            foreach (string userId in model.AddIds ?? new string[] { })
            {
                User user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(user, model.RoleName);
            }

            foreach (string userId in model.RemoveIds ?? new string[] { })
            {
                User user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }

            return RedirectToAction("List");
        }
    }
}
