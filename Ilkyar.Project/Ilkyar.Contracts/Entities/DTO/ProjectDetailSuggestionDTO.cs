using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ProjectDetailSuggestionDTO
    {
        public int ProjectId { get; set; }
        public long ProjectDetailId { get; set; }
        public string ProjectDetailName { get; set; }
        public int StatusId { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public long VolunteerId { get; set; }
        public DateTime ProjectDetailStartDate { get; set; }

    }
}