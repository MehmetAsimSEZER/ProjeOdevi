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

namespace Application.Services.ParentCategoryService
{
    public class ParentCategoryService : IParentCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParentCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task Create(CreateParentCategoryDTO model)
        {
            var name = _mapper.Map<ParentCategory>(model);

            await _unitOfWork.ParentCategoryRepository.Create(name);

            await _unitOfWork.Commit();
        }

        public async Task Update(UpdateParentCategoryDTO model)
        {
            var name = _mapper.Map<ParentCategory>(model);

            _unitOfWork.ParentCategoryRepository.Update(name);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var result = await _unitOfWork.ParentCategoryRepository.GetDefault(x => x.Id == id);

            result.DeleteDate = DateTime.Now;
            result.Status = Status.Passive;

            await _unitOfWork.Commit();
        }

        public async Task<List<ParentCategoryVM>> GetParentCategories()
        {
            var list = await _unitOfWork.ParentCategoryRepository.GetFilteredList(
                selector: x => new ParentCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                expression: x => x.Status != Status.Passive, orderBy: x => x.OrderBy(x => x.Name));
            return list;
        }

        public async Task<bool> IsParentCategoryExsist(string name)
        {
            var result = await _unitOfWork.ParentCategoryRepository.Any(x => x.Name == name);

            return result;
        }


    }
}
