using Application.Models.DTOs;
using Application.Models.VMs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.UoW;
using Microsoft.AspNetCore.Identity;


namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task Create(CreateAppUserDTO model)
        {
            var user = _mapper.Map<AppUser>(model);

            await _unitOfWork.UserRepository.Create(user);

            await _unitOfWork.Commit();


        }

        public async Task<UpdateProfileDTO> GetById(string id)
        {
            AppUserVM user = await _unitOfWork.UserRepository.GetFilteredFirstOrDefault(
                selector: x => new AppUserVM
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Adress = x.Adress,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    UserName = x.UserName,
                },
                expression: x => x.Id == id &&
                            x.Status != Status.Passive);

            UpdateProfileDTO model = _mapper.Map<UpdateProfileDTO>(user);

            return model;
        }

        public async Task<bool> IsUserExsist(string Email)
        {
            var result = await _unitOfWork.UserRepository.Any(x => x.Email == Email);

            return result;
        }



        public async Task Update(UpdateAppUserDTO model)
        {
            var user = _mapper.Map<AppUser>(model);

            _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.Commit();
        }

        public async Task<List<AppUserVM>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetFilteredList(
                selector: x => new AppUserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Adress = x.Adress,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    UserName = x.UserName,
                },
                expression: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName));

            return users;
        }

        public async Task Delete(string id)
        {
            var user = await _unitOfWork.UserRepository.GetDefault(x => x.Id == id);

            user.Status = Status.Passive;

            user.DeleteDate = DateTime.Now;

            await _unitOfWork.Commit();
        }


        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            var user = await _unitOfWork.UserRepository.GetDefault(x => x.Id == model.Id);

            if (user != null)
            {
                if (model.UserName != null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                    await _signInManager.SignInAsync(user, false);
                }

                if (model.Email != null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }

                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    await _userManager.UpdateAsync(user);
                }

                if (model.PhoneNumber != null)
                {
                    await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                }
            }
        }
    }
}
