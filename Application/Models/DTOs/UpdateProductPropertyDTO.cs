using Application.Models.VMs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class UpdateProductPropertyDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }
        public int PropertyId { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public List<ProductVM> Products { get; set; }
        public List<PropertyVM> Properties { get; set; }
    }
}
