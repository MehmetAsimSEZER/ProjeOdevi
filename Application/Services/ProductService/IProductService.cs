using Application.Models.DTOs;
using Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService
{
    public interface IProductService
    {
        Task Create(CreateProductDTO model);
        Task Update(UpdateProductDTO model);
        Task Delete(int id);
        Task<UpdateProductDTO> GetById(int id);
        Task<List<ProductVM>> GetProducts();
        Task<bool> IsProductExsist(string name);

        

    }
}
