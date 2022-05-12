using Application.Models.DTOs;
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
        Task Delete(Guid id);
        Task<bool> IsProductExsist(string value);
    }
}
