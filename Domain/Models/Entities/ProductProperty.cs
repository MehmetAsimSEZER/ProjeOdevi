using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class ProductProperty : IBase<Guid>, IBaseEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Status Status { get; set; }
    }
}
