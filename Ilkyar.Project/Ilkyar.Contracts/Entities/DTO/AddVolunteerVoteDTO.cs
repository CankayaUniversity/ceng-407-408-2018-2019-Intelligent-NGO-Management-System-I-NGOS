using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class AddVolunteerVoteDTO
    {
        public long ProjectDetailId { get; set; }
        public long UserId { get; set; }
        public long VolunteerId { get; set; }
        public int ActivityId { get; set; }
        public short Vote { get; set; }
    }
}
