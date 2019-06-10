using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class AddParticipantDTO
    {
        public long ProjectId { get; set; }
        public long ProjectDetailId { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
    }
}
