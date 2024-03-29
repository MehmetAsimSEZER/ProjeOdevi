﻿using Application.Models.VMs;

namespace Presantation.Models
{
    public class CartVM:ProductVM
    {
        public List<CartItem>? CartItems { get; set; }

        public decimal GrandTotal { get; set; }
        public int NumberOfItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
