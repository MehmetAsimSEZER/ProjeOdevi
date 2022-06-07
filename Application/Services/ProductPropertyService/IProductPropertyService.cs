using Application.Models.DTOs;
using Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductPropertyService
{
    public interface IProductPropertyService
    {
        Task Create(CreateProductPropertyDTO model);
        Task Update(UpdateProductPropertyDTO model);
        Task Delete(int id);
        Task<List<ProductPropertyVM>> Get();
        Task<bool> IsProductPropertyExsist(string value);
        Task<ProductPropertyVM> GetById(int id);
    }
}
