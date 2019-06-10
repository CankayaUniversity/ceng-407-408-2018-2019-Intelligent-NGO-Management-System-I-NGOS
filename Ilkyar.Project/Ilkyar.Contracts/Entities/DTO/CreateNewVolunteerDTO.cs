using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class CreateNewVolunteerDTO
    {
        public int UserTypeId { get; set; }
        public int Interest1Id { get; set; }        
        public int Interest2Id { get; set; }        
        public int Interest3Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNum { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsStudent { get; set; }
        public int? UniversityId { get; set; }
        public long? DepartmentId { get; set; }
        public string Class { get; set; }
        public int? OccupationId { get; set; }
    }
}
