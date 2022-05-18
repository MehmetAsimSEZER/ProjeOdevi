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
        public void AddCart(User user,Product product,int quantity);

        public void DeleteCart(User user);

        public decimal Total(ShoppingCart ShoppingCart);

    }
}
