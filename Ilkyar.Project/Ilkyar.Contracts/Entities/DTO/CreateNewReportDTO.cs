using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class CreateNewReportDTO
    {
        public long YonDerId { get; set; }
        public long ScholarshipHolderId { get; set; }
        public string Subject { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportText { get; set; }
    }
}
