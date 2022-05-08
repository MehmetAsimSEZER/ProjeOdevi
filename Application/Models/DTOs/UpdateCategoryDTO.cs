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
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public Category? ParentCategory { get; set; }

        public DateTime UpdateDate => DateTime.Now;

        public Status Status => Status.Modified;
    }
}
