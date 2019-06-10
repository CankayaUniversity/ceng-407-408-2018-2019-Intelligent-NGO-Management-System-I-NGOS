using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Services;

namespace Ilkyar.Contracts.ServiceContracts.Account
{
    public interface IAccount
    {
        ServiceResult<UserDTO> Login(string username, string password);
        ServiceResult<long> CreateNewNGOHead(CreateNewNGOHeadDTO model);
        ServiceResult<long> CreateNewProjectManager(CreateNewProjectManagerDTO model);
        ServiceResult<long> CreateNewScholarshipCommittee(CreateNewScholarshipCommitteeDTO model);
        ServiceResult<long> CreateNewDonator(CreateNewDonatorDTO model);
        ServiceResult<long> CreateNewSchoolmaster(CreateNewSchoolmasterDTO model);
        ServiceResult<long> CreateNewHostSchoolTeacher(CreateNewHostSchoolTeacherDTO model);
        ServiceResult<long> CreateNewStudent(CreateNewStudentDTO model);
        ServiceResult<long> CreateNewVolunteer(CreateNewVolunteerDTO model);
        ServiceResult<long> CreateNewYonder(CreateNewYonderDTO model);
        ServiceResult<VolunteerBadgeDTO> GetVolunteerBadge(long volunteerId);
        ServiceResult<ProjectManagerBadgeDTO> GetProjectManagerBadge(long projectManagerId);
    }
}
