﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class UpdatePropertyDTO
    {
        public int Id { get; set; }

        public string PropertyName { get; set; }

        public DateTime UpdateDate => DateTime.Now;

        public Status Status => Status.Modified;
    }
}
