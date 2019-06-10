using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ProjectManagerBadgeDTO
    {
        public List<ProjectExperienceBadgeDTO> ProjectExperienceBadgeList { get; set; }
        public bool IsHighestVoteBadge { get; set; }
    }
}
