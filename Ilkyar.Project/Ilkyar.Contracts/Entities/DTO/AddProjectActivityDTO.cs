﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class AddProjectActivityDTO
    {
        public int ActivityId { get; set; }
        public long ProjectId { get; set; }
        public long ProjectDetailId { get; set; }
    }
}
