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
        Task Create(CreateUserDTO model);
        Task Update(UpdateUserDTO model);
        Task Delete(string id);
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task LogOut();
        Task UpdateUser(UpdateUserDTO model);
        Task<UpdateUserDTO> GetById(string id);
        Task<List<UserVM>> GetUsers();
        Task<bool> IsUserExsist(string Email);
    }
}
