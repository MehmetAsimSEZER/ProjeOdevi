using Application.Models.DTOs;
using Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PropertyService
{
    public interface IPropertyService
    {
        Task Create(CreatePropertyDTO model);
        Task Update(UpdatePropertyDTO model);
        Task Delete(int id);
        Task<List<PropertyVM>> GetProperties();
        Task<bool> IsProductExsist(string name);
    }
}
