using System;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ReportFilterDTO
    {
        public long? YonDerId { get; set; }
        public string YonDerName { get; set; }
        public long? ScholarshipHolderId { get; set; }
        public string ScholarshipHolderName { get; set; }
        public string Subject { get; set; }
        public DateTime? ReportDate { get; set; }
        public string ReportText { get; set; }
    }
}
