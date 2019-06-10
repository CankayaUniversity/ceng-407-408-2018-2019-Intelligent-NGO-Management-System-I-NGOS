using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class CreateNGOInvitationDTO
    {
        public long SchoolmasterId { get; set; }
        public int NumberOfStudent { get; set; }
        public long RequirementListId { get; set; }
        public int StatusId { get; set; }
    }
}
