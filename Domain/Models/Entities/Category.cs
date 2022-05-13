using Domain.Enums;
using Domain.Interfaces;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category:IBase<int>,IBaseEntity
    {
        public int Id { get ; set; }

        public string CategoryName { get; set; }

        public Category? ParentCategory { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Status Status { get; set; }

        public List<Product> Products { get; set; }


    }
}
