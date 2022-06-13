using Domain.Entities;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
    {
        public void AddCart(AppUser user,Product product,int quantity);

        public void DeleteCart(AppUser user);

        public decimal Total(ShoppingCart ShoppingCart);

    }
}
