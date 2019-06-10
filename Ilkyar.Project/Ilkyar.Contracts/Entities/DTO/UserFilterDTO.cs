using System;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class UserFilterDTO
    {
        public int CurrentUserTypeId { get; set; }
        public int? UserTypeId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Status { get; set; }
    }
}
