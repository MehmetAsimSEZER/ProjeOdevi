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
            var parent = _mapper.Map<ParentCategory>(model);

            await _unitOfWork.ParentCategoryRepository.Create(parent);

            await _unitOfWork.Commit();
        }

        public async Task Update(UpdateParentCategoryDTO model)
        {
            var parent = _mapper.Map<ParentCategory>(model);

            _unitOfWork.ParentCategoryRepository.Update(parent);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var parent = await _unitOfWork.ParentCategoryRepository.GetDefault(x => x.Id == id);

            parent.DeleteDate = DateTime.Now;
            parent.Status = Status.Passive;

            await _unitOfWork.Commit();
        }

        public async Task<List<ParentCategoryVM>> GetParentCategories()
        {
            var parents = await _unitOfWork.ParentCategoryRepository.GetFilteredList(
                selector: x => new ParentCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                expression: x => x.Status != Status.Passive, orderBy: x => x.OrderBy(x => x.Name));
            return parents;
        }

        public async Task<bool> IsParentCategoryExsist(string name)
        {
            var parent = await _unitOfWork.ParentCategoryRepository.Any(x => x.Name == name);

            return parent;
        }


    }
}
