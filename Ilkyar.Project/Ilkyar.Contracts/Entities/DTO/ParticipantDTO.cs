using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ParticipantDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public string FullName { get; set; }
    }
}
