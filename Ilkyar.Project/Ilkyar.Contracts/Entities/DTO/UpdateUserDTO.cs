using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int UserTypeId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public string UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UserStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNum { get; set; }
        public DateTime DutyStartDate { get; set; }
        public DateTime? DutyEndDate { get; set; }
        public string Title { get; set; }
        public string DonatorName { get; set; }
        public string YonDerName { get; set; }
        public DateTime ScholarshipStartDate { get; set; }
        public DateTime? ScholarshipEndDate { get; set; }
        public double ScholarshipAmount { get; set; }
        public string IbanNo { get; set; }
        public string EducationLevel { get; set; }
        public string School { get; set; }
        public string Class { get; set; }
        public double CumGPA { get; set; }
        public string StudentDocument { get; set; }
        public string Transcript { get; set; }
        public string HealthConditionInfo { get; set; }
        public double MonthlyIncome { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public int NumberOfSiblings { get; set; }
        public string WorkPlace { get; set; }
        public string Branch { get; set; }

        public bool Volunteer_IsStudent { get; set; }
        public int? Volunteer_UniversityId { get; set; }
        public int? Volunteer_DepartmentId { get; set; }
        public string Volunteer_Class { get; set; }
        public int? Volunteer_OccupationId { get; set; }
    }
}
