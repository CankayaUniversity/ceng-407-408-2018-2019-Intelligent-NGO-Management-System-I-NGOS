using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class UpdateProjectDetailActivityDTO
    {
        public long ProjectDetailActivityId { get; set; }
        public long ProjectId { get; set; }
        public long ProjectDetailId { get; set; }
        public int StatusId { get; set; }
    }
}
