using AutoMapper;
using Domain.Entities;
using Domain.Models.Entities;
using Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(AppUser user, Product product, int quantity)
        {
            _unitOfWork.ShoppingCartRepository.AddCart(user, product, quantity);

            await _unitOfWork.Commit();
        }

        public async Task Delete(AppUser user)
        {
            _unitOfWork.ShoppingCartRepository.DeleteCart(user);

            await _unitOfWork.Commit();
        }

        public async Task Total(ShoppingCart ShoppingCart)
        {
            _unitOfWork.ShoppingCartRepository.Total(ShoppingCart);

            await _unitOfWork.Commit();
        }
    }
}
