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
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid ProductId { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime UpdateDate => DateTime.Now;

        public Status Status => Status.Modified;
    }
}
