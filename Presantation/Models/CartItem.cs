﻿using Domain.Models.Entities;

namespace Presantation.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string Image { get; set; }

        public CartItem() { }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.ProductName;
            Quantity = 1;
            Price = product.Price - (product.Price/100*product.Discount);
            Image = product.ImagePath;
        }
    }
}
