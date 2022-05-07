using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VMs
{
    public class CategoryVM
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public Category? ParentCategory { get; set; }

    }
}
