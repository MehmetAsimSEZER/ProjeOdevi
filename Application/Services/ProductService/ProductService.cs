using Application.Models.DTOs;
using Application.Models.VMs;
using Application.Services.PropertyService;
using AutoMapper;
using Domain.Enums;
using Domain.Models.Entities;
using Domain.UoW;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
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
        private readonly IPropertyService _propertyService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IPropertyService propertyService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _propertyService = propertyService;
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
                    ProductProperties = x.ProductProperties,
                    Discount = x.Price - (x.Price / 100 * x.Discount),
                    DiscountPrice = x.Price > 0 ? x.Price / 100 * x.Discount : 0

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
                    ProductProperties = x.ProductProperties,
                    Discount = x.Price - (x.Price / 100 * x.Discount),
                    DiscountPrice = x.Price > 0 ? x.Price / 100 * x.Discount : 0

                },
                expression: x => x.Status != Status.Passive, orderBy: x => x.OrderBy(x => x.ProductName));

            return product;

        }

        public async Task<bool> IsProductExsist(string name)
        {
            var result = await _unitOfWork.ProductRepository.Any(x => x.ProductName == name);

            return result;
        }


        public async Task<List<ProductVM>> GetProductByCategory(int categoryId)
        {
            var products = await _unitOfWork.ProductRepository.GetFilteredList(
                selector: x => new ProductVM
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    CategoryName = x.Category.CategoryName,
                    ProductProperties = x.ProductProperties,
                    Discount = x.Price - (x.Price / 100 * x.Discount),
                    DiscountPrice = x.Price > 0 ? x.Price / 100 * x.Discount : 0
                },
                expression: x => x.CategoryId == categoryId &&
                                x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.ProductName),
                include: x => x.Include(x => x.Category));

            return products;
        }

        public async Task<ProductVM> GetByIdWithProductProperty(int id)
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
                    ProductProperties = x.ProductProperties,
                    Discount = x.Price - (x.Price / 100 * x.Discount),
                    DiscountPrice = x.Price > 0 ? x.Price / 100 * x.Discount : 0
                },
                expression: x => x.Id == id && x.Status != Status.Passive );

            if (product.ProductProperties != null)
            {
                product.ProductProperties.ForEach(relation =>
                {
                    Task<PropertyVM> serviceResponse = _propertyService.GetById(relation.PropertyId);
                    relation.Property = new Property();
                    relation.Property.Id = serviceResponse.GetAwaiter().GetResult().Id;
                    relation.Property.PropertyName = serviceResponse.GetAwaiter().GetResult().PropertyName;
                });

            }

            return product;

        }



        public async Task<List<ProductVM>> GetDiscountProducts()
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
                    ProductProperties = x.ProductProperties,
                    Discount = x.Price / 100 * x.Discount,
                    DiscountPrice = x.Price - (x.Price / 100 * x.Discount)

                },
                expression: x => x.Status != Status.Passive && x.Discount >0 , orderBy: x => x.OrderBy(x => x.ProductName));

            return product;

        }
    }
}
