using Application.Models.DTOs;
using Application.Models.VMs;
using AutoMapper;
using Domain.Enums;
using Domain.Models.Entities;
using Domain.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PropertyService
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreatePropertyDTO model)
        {
            var property = _mapper.Map<Property>(model);

            await _unitOfWork.PropertyRepository.Create(property);

            await _unitOfWork.Commit();
        }

        public async Task Update(UpdatePropertyDTO model)
        {
            var property = _mapper.Map<Property>(model);

            _unitOfWork.PropertyRepository.Update(property);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var property = await _unitOfWork.PropertyRepository.GetDefault(x => x.Id == id);

            property.Status = Status.Passive;
            property.DeleteDate = DateTime.Now;

            await _unitOfWork.Commit();
        }

        public async Task<bool> IsProductExsist(string name)
        {
            var result = await _unitOfWork.PropertyRepository.Any(x => x.Name == name);

            return result;
        }

        public async Task<List<PropertyVM>> GetProperties()
        {
            var properties = await _unitOfWork.PropertyRepository.GetFilteredList(
                selector: x => new PropertyVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                expression: x => x.Status != Status.Passive, orderBy: x => x.OrderBy(x => x.Name));
            return properties;
        }
    }
}
