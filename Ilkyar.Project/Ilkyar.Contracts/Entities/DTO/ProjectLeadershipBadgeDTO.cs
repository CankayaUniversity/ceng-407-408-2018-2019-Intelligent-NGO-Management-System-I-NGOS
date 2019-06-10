using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ProjectExperienceBadgeDTO
    {
        public long ProjectDetailId { get; set; }
        public string ProjectDetailName { get; set; }
        public string BadgeColor { get; set; }
    }
}
