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
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<User> signInManager, UserManager<User> userManager = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateUser(CreateUserDTO model)
        {
            var user = _mapper.Map<User>(model);

            await _unitOfWork.UserRepository.Create(user);

            await _unitOfWork.Commit();


        }

        public async Task<UserVM> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetFilteredFirstOrDefault(
                selector: x => new UserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Adress = x.Adress,
                    Phone = x.Phone,
                    Email = x.Email,
                },
                expression: x => x.Id == id &&
                            x.Status != Status.Passive);

            var model = _mapper.Map<UserVM>(user);

            return model;
        }

        public async Task<bool> IsUserExsist(string Email)
        {
            var result = await _unitOfWork.UserRepository.Any(x => x.Email == Email);

            return result;
        }



        public async Task UpdateUser(UpdateUserDTO model)
        {
            var user = _mapper.Map<User>(model);

            _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.Commit();
        }

        public async Task<List<UserVM>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetFilteredList(
                selector: x => new UserVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Adress = x.Adress,
                    Phone = x.Phone,
                    Email = x.Email,
                },
                expression: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName));

            return users;
        }

        public async Task Delete(int id)
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
            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }

    }
}
