using Application.DTOs;
using Application.VMs;
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
        Task Delete(Guid id);
        Task<UpdateCategoryDTO> GetById(Guid id);
        Task<List<CategoryVM>> GetCategories();

        Task<bool> IsCategoryExsist(string name);
    }
}
