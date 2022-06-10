using Application.Models.VMs;
using Domain.Entities;
using Domain.Enums;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateCategoryDTO
    {
        public string CategoryName { get; set; }

        public int? ParentCategoryId { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;

        public List<ParentCategoryVM>? parentCategories { get; set; }
    }
}
