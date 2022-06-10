using Application.Models.VMs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public int ParentCategoryId { get; set; }

        public DateTime UpdateDate => DateTime.Now;

        public Status Status => Status.Modified;

        public List<ParentCategoryVM> parentCategories { get; set; }
    }
}
