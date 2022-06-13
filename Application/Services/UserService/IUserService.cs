using Application.Models.DTOs;
using Application.Models.VMs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task Create(CreateAppUserDTO model);
        Task Update(UpdateAppUserDTO model);
        Task Delete(string id);
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task LogOut();
        Task UpdateUser(UpdateAppUserDTO model);
        Task<UpdateAppUserDTO> GetById(string id);
        Task<List<AppUserVM>> GetUsers();
        Task<bool> IsUserExsist(string Email);
    }
}
