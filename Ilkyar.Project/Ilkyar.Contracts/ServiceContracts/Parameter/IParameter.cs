using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Services;
using System.Collections.Generic;

namespace Ilkyar.Contracts.ServiceContracts.Parameter
{
    public interface IParameter
    {
        ServiceResult<List<CityDTO>> GetCityList();
        ServiceResult<List<TownDTO>> GetTownList();
        ServiceResult<List<UniversityDTO>> GetUniversityList();
        ServiceResult<List<DepartmentDTO>> GetDepartmentList();
        ServiceResult<List<ProjectManagerDTO>> GetProjectManagerList();
        ServiceResult<List<ScholarshipHolderDTO>> GetScholarshipHolderList();
        ServiceResult<List<UserTypeDTO>> GetUserTypeList();
        ServiceResult<List<OccupationDTO>> GetOccupationList();
        ServiceResult<List<ActivityDTO>> GetActivityList(long projectDetailId);
        ServiceResult<DashboardDTO> GetDashboardInfo();
        ServiceResult<CityDTO> GetCity(int cityId);
        ServiceResult<TownDTO> GetTown(int cityId, int townId);
        ServiceResult<List<InterestDTO>> GetInterestList();
        ServiceResult<List<InterestVolunteerDTO>> GetVolunteerInterestList(long volunteerId);
        ServiceResult<long> UploadRequirementListFile(UploadRequirementListFileDTO model);
    }
}
