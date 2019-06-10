using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class DashboardDTO
    {
        public int TotalProjectCount { get; set; }
        public int TotalProjectCompletedCount { get; set; }
        public int TotalProjectActiveCount { get; set; }
        public int TotalUserCount { get; set; }
        public int TotalVolunteerCount { get; set; }
    }
}
