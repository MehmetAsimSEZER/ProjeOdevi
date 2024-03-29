﻿using Domain.Entities;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.VMs
{
    public class ProductVM 
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public List<ProductProperty>? ProductProperties { get; set; }

    }
}
