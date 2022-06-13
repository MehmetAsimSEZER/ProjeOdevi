using Application.Models.VMs;
using Domain.Entities;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ShoppingCartService
{
    public interface IShoppingCartService
    {
        Task Add(AppUser user,Product product,int quantity);
        Task Delete(AppUser user);
        Task Total (ShoppingCart ShoppingCart);
    }

}
