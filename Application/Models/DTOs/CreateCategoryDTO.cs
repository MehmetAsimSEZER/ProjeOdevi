using Domain.Entities;
using Domain.Enums;
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

        public Category? ParentCategory { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;
    }
}
