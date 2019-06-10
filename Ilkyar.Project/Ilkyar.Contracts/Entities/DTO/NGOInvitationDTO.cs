using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class NGOInvitationDTO
    {
        public long Id { get; set; }
        public long SchoolmasterId { get; set; }
        public string SchoolmasterName { get; set; }
        public string SchoolName { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public int NumberOfStudent { get; set; }
        public string RequirementList { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
