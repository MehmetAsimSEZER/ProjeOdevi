using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductCartRelRepository : IBaseRepository<ProductCartRel>
    {
        public void AddProduct(Product product, int quantity);

        public void DeleteProduct(Product product);

        public decimal Total();

        public void Clear();
    }
}
