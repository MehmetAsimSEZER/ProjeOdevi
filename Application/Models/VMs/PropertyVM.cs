﻿using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.VMs
{
    public class PropertyVM
    {
        public int Id { get; set; }

        public string PropertyName { get; set; }

        public List<ProductProperty>? ProductProperties { get; set; }
    }
}
