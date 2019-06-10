using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class UpdateProjectDetailActivityScheduleDTO
    {
        public long ProjectDetailActivityScheduleId { get; set; }
        public long ProjectId { get; set; }
        public long ProjectDetailId { get; set; }
        public long ProjectDetailActivityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int OperationTypeId { get; set; }
    }
}
