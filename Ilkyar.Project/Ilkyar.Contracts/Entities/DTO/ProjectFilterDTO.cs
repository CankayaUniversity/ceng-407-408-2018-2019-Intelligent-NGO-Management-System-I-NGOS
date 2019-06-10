using System;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ProjectFilterDTO
    {
        public int? ProjectTypeId { get; set; }
        public string ProjectTypeName { get; set; }
        public long? ProjectManagerId { get; set; }
        public string ProjectManagerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public string ProjectName { get; set; }
    }
}
