using Ilkyar.Business.System.Extentions;
using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.EF;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.ServiceContracts.Parameter;
using Ilkyar.Contracts.Services;
using Ilkyar.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Business.BusinessRules.Parameter
{
    public class ParameterBusiness : IParameter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Town> _townRepository;
        private readonly IRepository<University> _universityRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<ProjectManager> _projectManagerRepository;
        private readonly IRepository<ScholarshipHolder> _scholarshipHolderRepository;
        private readonly IRepository<UserType> _userTypeRepository;
        private readonly IRepository<Occupation> _occupationRepository;
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<ProjectActivity> _projectActivityRepository;
        private readonly IRepository<Ilkyar.Contracts.Entities.EF.Project> _projectRepository;
        private readonly IRepository<ProjectDetail> _projectDetailRepository;
        private readonly IRepository<Volunteer> _volunteerRepository;
        private readonly IRepository<InterestVolunteer> _interestVolunteerRepository;
        private readonly IRepository<UploadRequirementList> _uploadRequirementListRepository;
        private readonly IRepository<Ilkyar.Contracts.Entities.EF.User> _userRepository;


        public ParameterBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cityRepository = _unitOfWork.GetRepository<City>();
            _townRepository = _unitOfWork.GetRepository<Town>();
            _universityRepository = _unitOfWork.GetRepository<University>();
            _departmentRepository = _unitOfWork.GetRepository<Department>();
            _projectManagerRepository = _unitOfWork.GetRepository<ProjectManager>();
            _scholarshipHolderRepository = _unitOfWork.GetRepository<ScholarshipHolder>();
            _userTypeRepository = _unitOfWork.GetRepository<UserType>();
            _occupationRepository = _unitOfWork.GetRepository<Occupation>();
            _activityRepository = _unitOfWork.GetRepository<Activity>();
            _projectActivityRepository = _unitOfWork.GetRepository<ProjectActivity>();
            _projectRepository = _unitOfWork.GetRepository<Ilkyar.Contracts.Entities.EF.Project>();
            _projectDetailRepository = _unitOfWork.GetRepository<ProjectDetail>();
            _volunteerRepository = _unitOfWork.GetRepository<Volunteer>();
            _interestVolunteerRepository = _unitOfWork.GetRepository<InterestVolunteer>();
            _uploadRequirementListRepository = _unitOfWork.GetRepository<UploadRequirementList>();
            _userRepository = _unitOfWork.GetRepository<Ilkyar.Contracts.Entities.EF.User>();
        }

        public ServiceResult<List<CityDTO>> GetCityList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<CityDTO> result = null;
            try
            {
                result = new List<CityDTO>();
                var cityList = _cityRepository.Entities.ToList();
                foreach (var item in cityList)
                {
                    result.Add(new CityDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<CityDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<TownDTO>> GetTownList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<TownDTO> result = null;
            try
            {
                result = new List<TownDTO>();
                var townList = _townRepository.Entities.ToList();
                foreach (var item in townList)
                {
                    result.Add(new TownDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CityId = item.CityId
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<TownDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<UniversityDTO>> GetUniversityList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<UniversityDTO> result = null;
            try
            {
                result = new List<UniversityDTO>();
                var universityList = _universityRepository.Entities.ToList();
                foreach (var item in universityList)
                {
                    result.Add(new UniversityDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<UniversityDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<DepartmentDTO>> GetDepartmentList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<DepartmentDTO> result = null;
            try
            {
                result = new List<DepartmentDTO>();
                var departmentList = _departmentRepository.Entities.ToList();
                foreach (var item in departmentList)
                {
                    result.Add(new DepartmentDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        UniversityId = item.UniversityId
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<DepartmentDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<ProjectManagerDTO>> GetProjectManagerList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectManagerDTO> result = null;
            try
            {
                result = new List<ProjectManagerDTO>();
                var projectManagerList = _projectManagerRepository.Entities.ToList();
                foreach (var item in projectManagerList)
                {
                    result.Add(new ProjectManagerDTO
                    {
                        Id = item.Id,
                        Name = $"{item.FirstName} {item.LastName}"
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<ProjectManagerDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<ScholarshipHolderDTO>> GetScholarshipHolderList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ScholarshipHolderDTO> result = null;
            try
            {
                result = new List<ScholarshipHolderDTO>();
                var scholarshipHolderList = _scholarshipHolderRepository.Entities.ToList();
                foreach (var item in scholarshipHolderList)
                {
                    result.Add(new ScholarshipHolderDTO
                    {
                        Id = item.Id,
                        Name = $"{item.FirstName} {item.LastName}"
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<ScholarshipHolderDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<UserTypeDTO>> GetUserTypeList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<UserTypeDTO> result = null;
            try
            {
                result = new List<UserTypeDTO>();
                var userTypeList = _userTypeRepository.Entities.ToList();
                foreach (var item in userTypeList)
                {
                    result.Add(new UserTypeDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<UserTypeDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<OccupationDTO>> GetOccupationList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<OccupationDTO> result = null;
            try
            {
                result = new List<OccupationDTO>();
                var occupationList = _occupationRepository.Entities.ToList();
                foreach (var item in occupationList)
                {
                    result.Add(new OccupationDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<OccupationDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<ActivityDTO>> GetActivityList(long projectDetailId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ActivityDTO> result = null;
            try
            {
                result = new List<ActivityDTO>();
                var activityList = _projectActivityRepository.Entities.Where(s => s.ProjectDetailId == projectDetailId).ToList();
                foreach (var item in activityList)
                {
                    result.Add(new ActivityDTO
                    {
                        Id = item.ActivityId,
                        Name = item.Activity.Name
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<ActivityDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<DashboardDTO> GetDashboardInfo()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            DashboardDTO result = null;
            try
            {
                var totalProjectCount = _projectRepository.Entities.Count();
                var totalProjectCompletedCount = _projectRepository.Entities.Where(o => o.StatusId == (int)EnumProjectStatusType.Tamamlandi).Count();
                var totalProjectActiveCount = _projectRepository.Entities.Where(o => o.StatusId == (int)EnumProjectStatusType.Aktif).Count();

                result = new DashboardDTO
                {
                    TotalProjectCount = totalProjectCount,
                    TotalProjectCompletedCount = totalProjectCompletedCount,
                    TotalProjectActiveCount = totalProjectActiveCount,
                };

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {

            }
            return new ServiceResult<DashboardDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<CityDTO> GetCity(int cityId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            CityDTO result = null;
            try
            {
                var city = _cityRepository.Entities.Where(p => p.Id == cityId).SingleOrDefault();

                if (city == null)
                    throw new Exception("İl bulunamadı.");

                result = new CityDTO
                {
                    Id = city.Id,
                    Name = city.Name
                };

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<CityDTO>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<TownDTO> GetTown(int cityId, int townId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            TownDTO result = null;
            try
            {
                var town = _townRepository.Entities.Where(p => p.CityId == cityId && p.Id == townId).SingleOrDefault();

                if (town == null)
                    throw new Exception("İlçe bulunamadı.");

                result = new TownDTO
                {
                    Id = town.Id,
                    CityId = town.CityId,
                    Name = town.Name
                };

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<TownDTO>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<InterestDTO>> GetInterestList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<InterestDTO> result = null;
            try
            {
                result = new List<InterestDTO>();
                var activityList = _activityRepository.Entities.ToList();
                foreach (var item in activityList)
                {
                    result.Add(new InterestDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<InterestDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<InterestVolunteerDTO>> GetVolunteerInterestList(long volunteerId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<InterestVolunteerDTO> result = null;

            try
            {
                var VolunteerInterestList = _interestVolunteerRepository.Entities.Where(p => p.VolunteerId == volunteerId).ToList();

                result = VolunteerInterestList.Select(p => new InterestVolunteerDTO
                {
                    InterestName = p.Activity.Name
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<InterestVolunteerDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> UploadRequirementListFile(UploadRequirementListFileDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                //Kontrole gerek var mı? Aynı dosyanın yüklenmesi neyi etkiler?
                var anyExistingFile = _uploadRequirementListRepository.Entities.Any(p => p.AttachedFile == model.AttachedFile && p.ContentType == model.ContentType);

                if (anyExistingFile)
                    throw new Exception("Dosya mevcut.");

                var newRequirementList = new Contracts.Entities.EF.UploadRequirementList
                {
                    Name = model.Name,
                    FileSize = model.FileSize,
                    AttachedFile = model.AttachedFile,
                    ContentType = model.ContentType,
                    UploadDate = DateTime.Today                  
                };
                
                var RequirementListResult = _uploadRequirementListRepository.Add(newRequirementList);
                _unitOfWork.SaveChanges();

                result = RequirementListResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

    }
}

