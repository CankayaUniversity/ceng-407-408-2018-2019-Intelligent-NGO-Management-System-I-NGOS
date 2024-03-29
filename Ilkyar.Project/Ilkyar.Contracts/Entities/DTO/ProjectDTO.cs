﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ProjectDTO
    {
        public long Id { get; set; }
        public int ProjectTypeId { get; set; }
        public string ProjectTypeName { get; set; }
        public long ProjectManagerId { get; set; }
        public string ProjectManagerName { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int isActive { get; set; }
        public string StatusName { get; set; }
    }
}
