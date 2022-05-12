using Application.DTOs;
using Application.Models.DTOs;
using Application.Models.VMs;
using Application.VMs;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<UpdateCategoryDTO, CategoryVM>().ReverseMap();


            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<UpdateUserDTO, UserVM>().ReverseMap();


            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<UpdateProductDTO, ProductVM>().ReverseMap();

            CreateMap<Property, CreatePropertyDTO>().ReverseMap();
            CreateMap<Property, UpdatePropertyDTO>().ReverseMap();
            CreateMap<Property, PropertyVM>().ReverseMap();
            CreateMap<UpdatePropertyDTO, PropertyVM>().ReverseMap();

            CreateMap<ProductProperty, CreateProductPropertyDTO>().ReverseMap();
            CreateMap<ProductProperty, UpdateProductPropertyDTO>().ReverseMap();
            CreateMap<ProductProperty, ProductPropertyVM>().ReverseMap();
            CreateMap<UpdateProductPropertyDTO, ProductPropertyVM>().ReverseMap();

        }
    }
}
