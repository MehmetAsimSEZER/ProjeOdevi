using Application.DTOs;
using Application.Models.DTOs;
using Application.Models.VMs;
using Application.VMs;
using AutoMapper;
using Domain.Entities;
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
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<UpdateUserDTO, UserVM>().ReverseMap();
        }
    }
}
