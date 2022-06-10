using Application.Models.DTOs;
using Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ParentCategoryService
{
    public interface IParentCategoryService
    {
        Task Create(CreateParentCategoryDTO model);
        Task Update(UpdateParentCategoryDTO model);
        Task Delete(int id);
        Task<List<ParentCategoryVM>> GetParentCategories();
        Task<bool> IsParentCategoryExsist(string name);
        Task<ParentCategoryVM> GetById(int id);
    }
}
