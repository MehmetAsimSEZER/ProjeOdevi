using Application.DTOs;
using Application.VMs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryService
{
    public interface ICategoryService
    {
        Task Create(CreateCategoryDTO model);
        Task Update(UpdateCategoryDTO model);
        Task Delete(int id);
        Task<UpdateCategoryDTO> GetById(int id);
        Task<List<CategoryVM>> GetCategories();

        Task<bool> IsCategoryExsist(string name);

        Task<Category> GetByName(string name);
    }
}
