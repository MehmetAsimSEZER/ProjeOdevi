using Application.DTOs;
using Application.VMs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task Create(CreateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);

            await _unitOfWork.CategoryRepository.Create(category);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetDefault(x => x.Id == id);

            category.Status = Status.Passive;

            category.DeleteDate = DateTime.Now;

            await _unitOfWork.Commit();

        }

        public async Task<UpdateCategoryDTO> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefault(
                selector: x => new CategoryVM
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    ParentCategoryName = x.ParentCategory.Name

                },
                expression: x => x.Id == id &&
                                 x.Status != Status.Passive);

            var model = _mapper.Map<UpdateCategoryDTO>(category);

            return model;
        }

        public async Task<Category> GetByName(string name)
        {
            var category = await _unitOfWork.CategoryRepository.GetDefault(x => x.CategoryName == name);

            return category;
        }

        public async Task<List<CategoryVM>> GetCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetFilteredList(
                selector: x => new CategoryVM
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    ParentCategoryName = x.ParentCategory.Name
                },
                expression: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.CategoryName));

            return categories;
        }

        public async Task<bool> IsCategoryExsist(string name)
        {
            var result = await _unitOfWork.CategoryRepository.Any(x => x.CategoryName == name);

            return result;
        }

        public async Task Update(UpdateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);

            _unitOfWork.CategoryRepository.Update(category);

            await _unitOfWork.Commit();
        }
    }
}
