using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class EvaluateVolunteerDTO
    {
        public long ActivityId { get; set; }
        public string ActivityName { get; set; }
        public long VolunteerId { get; set; }
        public string VolunteerName { get; set; }
        public int Vote { get; set; }
    }
}
