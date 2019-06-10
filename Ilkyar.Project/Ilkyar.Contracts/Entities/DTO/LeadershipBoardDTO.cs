using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class LeadershipBoardDTO
    {
        public int OrderNumber { get; set; }
        public int UserTypeId { get; set; }
        public long VolunteerId { get; set; }
        public string VolunteerName { get; set; }
        public double TotalVote { get; set; }
        public string TotalActivity { get; set; }
        public double AvgVote { get; set; }
    }
}
