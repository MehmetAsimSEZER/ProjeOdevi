using Domain.Entities;
using Domain.Models.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        private List<ShoppingCart> _shoppingCarts = new List<ShoppingCart>();

        public List<ShoppingCart> shoppingCarts
        { 
            get { return _shoppingCarts; } 
        }

        public void AddCart(User user, Product product, int quantity)
        {
            var currentCart = _shoppingCarts.FirstOrDefault(x => x.UserId == user.Id);
            if (currentCart != null)
            {
                var itemRel = currentCart.ProductCartRels.FirstOrDefault(x => x.ProductId == product.Id);
                if (itemRel != null)
                {
                    itemRel.Quantity += quantity;
                }
                else
                {
                    var productCartRel = new ProductCartRel()
                    {
                        ProductId = product.Id,
                        Quantity = quantity
                    };
                    currentCart.ProductCartRels.Add(productCartRel);
                }
            }
            else
            {
                var newShoppingCart = new ShoppingCart()
                {
                    UserId = user.Id,
                };
                var productCartRel = new ProductCartRel()
                {
                    ProductId = product.Id,
                    Quantity = quantity
                };

                newShoppingCart.ProductCartRels.Add(productCartRel);
                _shoppingCarts.Add(newShoppingCart);
            }

        }


        public void DeleteCart(User user)
        {
            _shoppingCarts.RemoveAll(x => x.UserId == user.Id);
        }

        public decimal Total(ShoppingCart ShoppingCart)
        {
            var cart = _shoppingCarts.FirstOrDefault(shoppingCart => shoppingCart.Id == ShoppingCart.Id);

            if (cart != null)
            {
                var total = cart.ProductCartRels.Sum(x => x.Product.Price * x.Quantity);
                return total;

            }
            else
            {
                return 0;
            }

        }

    }
}
