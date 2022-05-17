using Application.Models.DTOs;
using Application.Models.VMs;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Entities;
using Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductCartRelService
{
    public  class ProductCartRelService : IProductCartRelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCartRelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task Create(ProductCartRelDTO model)
        {
            _unitOfWork.ProductCartRelRepository.AddProduct(model.ProductId,model.Quantity);

            await _unitOfWork.Commit();
        }

        public async Task Delete(ProductCartRelDTO model)
        {
            _unitOfWork.ProductCartRelRepository.DeleteProduct(model.ProductId);

            await _unitOfWork.Commit();
        }

        public async Task Total()
        {
            _unitOfWork.ProductCartRelRepository.Total();

            await _unitOfWork.Commit();
        }


        public async Task Clear()
        {
            _unitOfWork.ProductCartRelRepository.Clear();

            await _unitOfWork.Commit();
        }

    }
}
