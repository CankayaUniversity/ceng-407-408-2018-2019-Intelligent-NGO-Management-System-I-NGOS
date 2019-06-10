using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class InterestVolunteerDTO
    {
        public int Id { get; set; }
        public int InterestId { get; set; }
        public int VolunteerId { get; set; }
        public int UserId { get; set; }
        public string InterestName { get; set; }
    }
}
