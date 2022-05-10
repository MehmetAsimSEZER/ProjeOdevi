using Module = Autofac.Module;
using Autofac;
using AutoMapper;
using Application.AutoMapper;
using Application.Services.CategoryService;
using Domain.UoW;
using Infrastructure.UoW;
using Application.Services.UserService;
using Application.Validation;
using FluentValidation;
using Application.Models.DTOs;
using Application.Services.ProductService;

namespace Application.IoC
{
    public class DependencyResolver:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();




            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();


            builder.RegisterType<LoginValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();


            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}
