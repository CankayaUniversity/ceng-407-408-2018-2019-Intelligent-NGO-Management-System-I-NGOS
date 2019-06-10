using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class VolunteerBadgeDTO
    {
        public List<ActivityLeadershipBadgeDTO> ActivityLeadershipBadgeList { get; set; }
        public List<GeniusBadgeDTO> GeniusBadgeList { get; set; }
        public bool IsBronzeActivityLeadershipBadge { get; set; }
        public bool IsSilverActivityLeadershipBadge { get; set; }
        public bool IsGoldActivityLeadershipBadge { get; set; }
        public bool IsBee { get; set; }
        public int NeededForBeeBadge { get; set; }
    }
}
