using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class VolunteerSuggestionDTO
    {
        public string VolunteerFullName { get; set; }
        public string CurrentActivityName { get; set; }

        public double ApprovedCityMatchPercentage { get; set; }
        public double ApprovedRegionMatchPercentage { get; set; }
        public double ApprovedSchoolTypeMatchPercentage { get; set; }
        public double ApprovedProjectTypeMatchPercentage { get; set; }
        public double ApprovedNumberOfPeopleMatchTolerancePercentage { get; set; }

        public double OverallCityMatchPercentage { get; set; }
        public double OverallRegionMatchPercentage { get; set; }
        public double OverallSchoolTypeMatchPercentage { get; set; }
        public double OverallProjectTypeMatchPercentage { get; set; }
        public double OverallNumberOfPeopleMatchTolerancePercentage { get; set; }

        public int ApprovedActivityCount { get; set; }
        public int RejectedActivityCount { get; set; }
        public int OverallActivityCount { get; set; }
    }
}
