using Domain.Models.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductCartRelRepository : BaseRepository<ProductCartRel>, IProductCartRelRepository
    {
        public ProductCartRelRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        private List<ProductCartRel> _productCartRels = new List<ProductCartRel>();

        public List<ProductCartRel> ProductCartRels
        {
            get { return _productCartRels; }
        }

        public void AddProduct(Product product, int quantity)
        {
            var cart = _productCartRels.FirstOrDefault(x => x.ProductId == product.Id);
            if (cart == null)
            {
                _productCartRels.Add(new ProductCartRel()
                {
                    ProductId = product.Id,
                    Quantity = quantity
                });
            }
            else
            {
                cart.Quantity += quantity;
            }
        }


        public void DeleteProduct(Product product)
        {
            _productCartRels.RemoveAll(x => x.ProductId == product.Id);
        }

        public decimal Total()
        {
            return _productCartRels.Sum(x => x.Product.Price * x.Quantity);
        }


        public void Clear()
        {
            _productCartRels.Clear();
        }
    }
}
