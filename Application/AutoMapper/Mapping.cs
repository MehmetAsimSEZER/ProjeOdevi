﻿using Application.DTOs;
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


            CreateMap<AppUser, CreateAppUserDTO>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserDTO>().ReverseMap();
            CreateMap<AppUser, AppUserVM>().ReverseMap();
            CreateMap<UpdateAppUserDTO, AppUserVM>().ReverseMap();
            CreateMap<UpdateProfileDTO, AppUserVM>().ReverseMap();

            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();


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

            CreateMap<ParentCategory, CreateParentCategoryDTO>().ReverseMap();
            CreateMap<ParentCategory, UpdateParentCategoryDTO>().ReverseMap();
            CreateMap<ParentCategory, ParentCategoryVM>().ReverseMap();
            CreateMap<UpdateParentCategoryDTO, ParentCategoryVM>().ReverseMap();

            CreateMap<Product, ProductCartRel>().ReverseMap();


        }
    }
}
