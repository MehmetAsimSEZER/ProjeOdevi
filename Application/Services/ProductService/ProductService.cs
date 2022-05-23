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

namespace Application.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);

            await _unitOfWork.ProductRepository.Create(product);

            await _unitOfWork.Commit();
        }

        public async Task Update(UpdateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);

            _unitOfWork.ProductRepository.Update(product);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetDefault(x => x.Id == id);

            product.Status = Status.Passive;
            product.DeleteDate = DateTime.Now;

            await _unitOfWork.Commit();

        }

        public async Task<ProductVM> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetFilteredFirstOrDefault(
                selector: x => new ProductVM
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    CategoryName = x.Category.CategoryName,
                    
                },
                expression: x => x.Id == id && x.Status != Status.Passive);
     
            return product;

        }

        public async Task<List<ProductVM>> GetProducts()
        {
            var product = await _unitOfWork.ProductRepository.GetFilteredList(
                selector: x => new ProductVM
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    CategoryName = x.Category.CategoryName,                   

                },
                expression: x => x.Status != Status.Passive, orderBy: x => x.OrderBy(x => x.ProductName));

            return product;

        }

        public async Task<bool> IsProductExsist(string name)
        {
            var result = await _unitOfWork.ProductRepository.Any(x => x.ProductName == name);

            return result;
        }


    }
}
