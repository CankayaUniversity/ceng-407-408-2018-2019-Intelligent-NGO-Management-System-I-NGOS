using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ProjectDetailSummaryDTO
    {
        public long ProjectId { get; set; }
        public long ProjectDetailId { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ProjectDetailStartDate { get; set; }
        public DateTime ProjectDetailEndDate { get; set; }
    }
}
