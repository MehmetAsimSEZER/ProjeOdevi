using Application.Models.DTOs;
using Application.Models.VMs;
using AutoMapper;
using Domain.Enums;
using Domain.Models.Entities;
using Domain.Repositories;
using Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductPropertyService
{
    public class ProductPropertyService : IProductPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductPropertyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreateProductPropertyDTO model)
        {
            var name = _mapper.Map<ProductProperty>(model);

            await _unitOfWork.ProductPropertyRepository.Create(name);

            await _unitOfWork.Commit();

        }

        public async Task Update(UpdateProductPropertyDTO model)
        {
            var name = _mapper.Map<ProductProperty>(model);

            _unitOfWork.ProductPropertyRepository.Update(name);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var result = await _unitOfWork.ProductPropertyRepository.GetDefault(x => x.Id == id);

            result.DeleteDate = DateTime.Now;
            result.Status = Status.Passive;

            await _unitOfWork.Commit(); 
        }

        public async Task<bool> IsProductPropertyExsist(string value)
        {
            var result = await _unitOfWork.ProductPropertyRepository.Any(x => x.Value == value);

            return result;
        }

        public async Task<List<ProductPropertyVM>> Get()
        {
            var list = await _unitOfWork.ProductPropertyRepository.GetFilteredList(
                selector: x => new ProductPropertyVM
                {
                    Id = x.Id,
                    Value = x.Value,
                },
                expression: x => x.Status != Status.Passive, orderBy: x => x.OrderBy(x => x.Value));
            return list;
        }
    }
}
