using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class UpdateProjectActivityDTO
    {
        public long ProjectId { get; set; }
        public long ProjectDetailId { get; set; }
        public List<ActivityDTO> ActivityList { get; set; }
    }
}
