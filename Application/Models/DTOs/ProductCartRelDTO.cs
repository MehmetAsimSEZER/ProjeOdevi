using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class ProductCartRelDTO
    {
        public int Id { get; set; }
        public Product ProductId { get; set; }
        public int Quantity { get; set; }
        public ShoppingCart ShoppingCartId { get; set; }
    }
}
