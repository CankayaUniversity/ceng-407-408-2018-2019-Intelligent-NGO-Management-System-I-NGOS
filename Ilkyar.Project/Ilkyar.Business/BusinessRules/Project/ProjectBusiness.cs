using Ilkyar.Business.System.Extentions;
using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.EF;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.ServiceContracts.Project;
using Ilkyar.Contracts.Services;
using Ilkyar.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ilkyar.Business.BusinessRules.Project
{
    public class ProjectBusiness : IProject
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Contracts.Entities.EF.Project> _projectRepository;
        private readonly IRepository<Contracts.Entities.EF.ProjectDetail> _projectDetailRepository;
        private readonly IRepository<Contracts.Entities.EF.Transportation> _projectTransportationRepository;
        private readonly IRepository<Contracts.Entities.EF.ProjectDetailParticipant> _projectDetailParticipantRepository;
        private readonly IRepository<Contracts.Entities.EF.ProjectDetailActivity> _projectDetailActivityRepository;
        private readonly IRepository<Contracts.Entities.EF.ProjectActivity> _projectActivityRepository;
        private readonly IRepository<Contracts.Entities.EF.NGOInvitation> _NGOInvitationRepository;
        private readonly IRepository<Contracts.Entities.EF.SurveyProjectDetailQuestion> _surveyProjectDetailQuestionRepository;
        private readonly IRepository<Contracts.Entities.EF.ProjectDetailVote> _projectDetailVoteRepository;
        private readonly IRepository<Contracts.Entities.EF.Activity> _activityRepository;
        private readonly IRepository<Contracts.Entities.EF.VolunteerVote> _volunteerVoteRepository;
        private readonly IRepository<Contracts.Entities.EF.Volunteer> _volunteerRepository;
        private readonly IRepository<Contracts.Entities.EF.InterestVolunteer> _volunteerInterestRepository;
        private readonly IRepository<Contracts.Entities.EF.ProjectDetailActivitySchedule> _projectDetailActivityScheduleRepository;
        private readonly IRepository<Contracts.Entities.EF.RequirementProjectDetail> _requirementProjectDetailRepository;

        public ProjectBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.Project>();
            _projectDetailRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.ProjectDetail>();
            _projectTransportationRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.Transportation>();
            _projectDetailParticipantRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.ProjectDetailParticipant>();
            _projectDetailActivityRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.ProjectDetailActivity>();
            _projectActivityRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.ProjectActivity>();
            _NGOInvitationRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.NGOInvitation>();
            _surveyProjectDetailQuestionRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.SurveyProjectDetailQuestion>();
            _projectDetailVoteRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.ProjectDetailVote>();
            _activityRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.Activity>();
            _volunteerVoteRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.VolunteerVote>();
            _volunteerRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.Volunteer>();
            _volunteerInterestRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.InterestVolunteer>();
            _projectDetailActivityScheduleRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.ProjectDetailActivitySchedule>();
            _requirementProjectDetailRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.RequirementProjectDetail>();
        }

        public ServiceResult<long> CreateNewProject(CreateNewProjectDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingProject = _projectRepository.Entities.Any(p => p.ProjectTypeId == model.ProjectTypeId && p.Name == model.Name && p.ProjectManagerId == model.ProjectManagerId && p.StartDate == model.StartDate && p.EndDate == model.EndDate && p.StatusId == model.StatusId);

                if (anyExistingProject)
                    throw new Exception("Proje bilgisi mevcut.");

                var newProject = new Contracts.Entities.EF.Project
                {
                    ProjectTypeId = model.ProjectTypeId,
                    ProjectManagerId = model.ProjectManagerId,
                    Name = model.Name,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    StatusId = model.StatusId
                };

                var projectResult = _projectRepository.Add(newProject);
                _unitOfWork.SaveChanges();

                result = projectResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<ProjectDTO>> GetProjectList(ProjectFilterDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectDTO> result = null;
            try
            {
                Expression<Func<Contracts.Entities.EF.Project, bool>> expProject = p => true;

                if (model.ProjectTypeId != null)
                {
                    expProject = expProject.And(p => p.ProjectTypeId == model.ProjectTypeId);
                }

                if (model.ProjectManagerId != null)
                {
                    expProject = expProject.And(p => p.ProjectManagerId == model.ProjectManagerId);
                }

                if (!string.IsNullOrEmpty(model.ProjectName))
                {
                    expProject = expProject.And(p => p.Name.Contains(model.ProjectName));
                }

                if (model.StartDate != null)
                {
                    expProject = expProject.And(p => p.StartDate == model.StartDate);
                }

                if (model.EndDate != null)
                {
                    expProject = expProject.And(p => p.EndDate == model.EndDate);
                }

                if (model.Status != null)
                {
                    if (model.Status == (int)EnumProjectStatusType.Aktif)
                    {
                        expProject = expProject.And(p => p.StatusId == model.Status);
                    }
                    else if (model.Status == (int)EnumProjectStatusType.IptalEdildi)
                    {
                        expProject = expProject.And(p => p.StatusId == model.Status);
                    }
                    else if (model.Status == (int)EnumProjectStatusType.Tamamlandi)
                    {
                        expProject = expProject.And(p => p.StatusId == model.Status);
                    }
                }

                var projectList = _projectRepository.Entities.Where(expProject).ToList();

                result = projectList.Select(p => new ProjectDTO
                {
                    Id = p.Id,
                    ProjectTypeId = p.ProjectTypeId,
                    ProjectTypeName = p.ProjectType.Name,
                    Name = p.Name,
                    ProjectManagerId = p.ProjectManagerId,
                    ProjectManagerName = $"{p.ProjectManager.FirstName} {p.ProjectManager.LastName}",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    isActive = p.StatusId,
                    StatusName = EnumHelper.GetEnumDescription(typeof(EnumProjectStatusType), p.StatusId.ToString())
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<ProjectDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateProjectDetail(CreateProjectDetailDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingProject = _projectDetailRepository.Entities.Any(p => p.ProjectId == model.ProjectId && p.SchoolTypeId == model.SchoolTypeId && p.School == model.School && p.StartDate == model.DetailStartDate && p.EndDate == model.DetailEndDate && p.StatusId == model.StatusId);

                if (anyExistingProject)
                    throw new Exception("Proje Detayı mevcut.");

                var newProjectDetail = new Contracts.Entities.EF.ProjectDetail
                {
                    ProjectId = model.ProjectId,
                    CityId = model.CityId,
                    TownId = model.TownId,
                    SchoolTypeId = model.SchoolTypeId,
                    School = model.School,
                    StartDate = model.DetailStartDate,
                    EndDate = model.DetailEndDate,
                    ProjectInfo = model.ProjectInfo,
                    NumberOfPeople = model.AccNumOfPeople,
                    Inn = model.Inn,
                    StatusId = model.StatusId
                };

                var newTransportationDetail = new Contracts.Entities.EF.Transportation
                {
                    TransportationTypeId = model.TransportationTypeId,
                    ComebackTransportationTypeId = model.ArrivalTransportationTypeId,
                    StartDate = model.TrnsStartDate,
                    EndDate = model.TrnsEndDate,
                    NumberOfPeople = model.TrnsNumOfPeople,
                    ComebackNumberOfPeople = model.TrnsArrNumOfPeople,
                    Departure = model.Departure,
                    Comeback = model.Comeback,
                    DepartureFirm = model.DepartureFirm,
                    ComebackFirm = model.ComebackFirm


                };

                var newRequirement = new RequirementProjectDetail
                {
                    Requirement = model.ReqText
                };

                newProjectDetail.Transportation.Add(newTransportationDetail);
                newProjectDetail.RequirementProjectDetail.Add(newRequirement);

                var projectDetailResult = _projectDetailRepository.Add(newProjectDetail);
                _unitOfWork.SaveChanges();

                result = projectDetailResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<ProjectDTO> GetProject(long projectId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            ProjectDTO result = null;
            try
            {
                var existingProject = _projectRepository.Entities.Where(p => p.Id == projectId).SingleOrDefault();

                if (existingProject == null)
                    throw new Exception("Proje bulunamadı.");

                result = new ProjectDTO
                {
                    Id = existingProject.Id,
                    ProjectTypeId = existingProject.ProjectTypeId,
                    ProjectTypeName = EnumHelper.GetEnumDescription(typeof(EnumProjectType), existingProject.ProjectTypeId.ToString()),
                    ProjectManagerId = existingProject.ProjectManagerId,
                    ProjectManagerName = $"{existingProject.ProjectManager.FirstName} {existingProject.ProjectManager.LastName}",
                    Name = existingProject.Name,
                    StartDate = existingProject.StartDate,
                    EndDate = existingProject.EndDate,
                    isActive = existingProject.StatusId,
                    StatusName = EnumHelper.GetEnumDescription(typeof(EnumProjectStatusType), existingProject.StatusId.ToString())
                };

                serviceResultType = EnumServiceResultType.Success;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<ProjectDTO>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };

        }

        public ServiceResult<bool> UpdateProject(UpdateProjectDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var existingProject = _projectRepository.Entities.Where(p => p.Id == model.Id).SingleOrDefault();

                if (existingProject == null)
                    throw new Exception("Proje bulunamadı.");

                existingProject.ProjectTypeId = model.ProjectTypeId;
                existingProject.ProjectManagerId = model.ProjectManagerId;
                existingProject.StatusId = model.StatusId;
                existingProject.Name = model.Name;
                existingProject.StartDate = model.StartDate;
                existingProject.EndDate = model.EndDate;

                if (model.StatusId == (int)EnumProjectStatusType.Tamamlandi)
                {
                    foreach (var item in existingProject.ProjectDetail)
                    {
                        item.StatusId = (int)EnumProjectSubDetailStatusType.Tamamlandi;
                    }
                }

                if (model.StatusId == (int)EnumProjectStatusType.IptalEdildi)
                {
                    foreach (var item in existingProject.ProjectDetail)
                    {
                        item.StatusId = (int)EnumProjectSubDetailStatusType.IptalEdildi;
                    }
                }

                _unitOfWork.SaveChanges();

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<ProjectSubDetailDTO>> GetProjectSubDetailList(long projectId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectSubDetailDTO> result = null;

            try
            {
                var projectSubDetailList = _projectDetailRepository.Entities.Where(p => p.ProjectId == projectId).ToList();

                result = projectSubDetailList.Select(p => new ProjectSubDetailDTO
                {
                    Id = p.Id,
                    ProjectId = p.ProjectId,
                    ProjectTypeId = p.Project.ProjectTypeId,
                    City = p.City.Name,
                    SchoolType = p.SchoolType.Name,
                    School = p.School,
                    DetailStartDate = p.StartDate,
                    DetailEndDate = p.EndDate,
                    StatusId = p.StatusId,
                    StatusName = EnumHelper.GetEnumDescription(typeof(EnumProjectSubDetailStatusType), p.StatusId.ToString())


                }).ToList();



                serviceResultType = EnumServiceResultType.Success;


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<ProjectSubDetailDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<bool> UpdateProjectSubDetail(UpdateProjectSubDetailDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var anyExistingProject = _projectDetailRepository.Entities.Any(p => p.ProjectId == model.ProjectId && p.Id == model.Id);

                if (!anyExistingProject)
                    throw new Exception("Proje Detayı bulunamadı.");

                var existingProjectDetail = _projectDetailRepository.Entities.Where(p => p.Id == model.Id).SingleOrDefault();

                existingProjectDetail.CityId = model.CityId;
                existingProjectDetail.TownId = model.TownId;
                existingProjectDetail.School = model.School;
                existingProjectDetail.SchoolTypeId = model.SchoolTypeId;
                existingProjectDetail.StartDate = model.DetailStartDate;
                existingProjectDetail.EndDate = model.DetailEndDate;
                existingProjectDetail.Inn = model.Inn;
                existingProjectDetail.NumberOfPeople = model.AccNumOfPeople;
                existingProjectDetail.ProjectInfo = model.ProjectInfo;

                var existingTransportation = _projectTransportationRepository.Entities.Where(p => p.ProjectDetailId == model.Id).SingleOrDefault();

                existingTransportation.StartDate = model.TrnsStartDate;
                existingTransportation.EndDate = model.TrnsEndDate;
                existingTransportation.TransportationTypeId = model.TransportationTypeId;
                existingTransportation.ComebackTransportationTypeId = model.ArrivalTransportationTypeId;
                existingTransportation.Departure = model.Departure;
                existingTransportation.DepartureFirm = model.DepartureFirm;
                existingTransportation.Comeback = model.Comeback;
                existingTransportation.ComebackFirm = model.ComebackFirm;
                existingTransportation.NumberOfPeople = model.TrnsNumOfPeople;
                existingTransportation.ComebackNumberOfPeople = model.TrnsArrNumOfPeople;

                var existingRequirement = _requirementProjectDetailRepository.Entities.Where(p => p.ProjectDetailId == model.Id).SingleOrDefault();

                existingRequirement.Requirement = model.ReqText;


                _unitOfWork.SaveChanges();

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<ProjectSubDetailDTO> GetProjectSubDetail(long projectId, long projectDetailId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            ProjectSubDetailDTO result = null;
            try
            {
                var existingProject = _projectRepository.Entities.Where(p => p.Id == projectId).SingleOrDefault();

                if (existingProject == null)
                    throw new Exception("Proje bulunamadı.");

                var existingSubDetail = _projectDetailRepository.Entities.Where(p => p.Id == projectDetailId).SingleOrDefault();
                if (existingSubDetail == null)
                    throw new Exception("Proje detayı bulunamadı.");

                var existingTransportation = _projectTransportationRepository.Entities.Where(p => p.ProjectDetailId == projectDetailId).SingleOrDefault();
                if (existingTransportation == null)
                    throw new Exception("Ulaşım detayı bulunamadı.");

                var existingRequirement = _requirementProjectDetailRepository.Entities.Where(p => p.ProjectDetailId == projectDetailId).SingleOrDefault();
                if (existingRequirement == null)
                    throw new Exception("İhtiyaç detayı bulunamadı.");

                var anyExistingActivityProject = _projectActivityRepository.Entities.Any(p => p.ProjectId == projectId && p.ProjectDetailId == projectDetailId);

                if (!anyExistingActivityProject)
                    throw new Exception("Aktivite bilgisi bulunamadı.");

                var ExistingActivityProjectList = _projectActivityRepository.Entities.Where(p => p.ProjectId == projectId && p.ProjectDetailId == projectDetailId).ToList();

                result = new ProjectSubDetailDTO
                {
                    ProjectId = existingProject.Id,
                    CityId = Convert.ToInt32(existingSubDetail.CityId),
                    TownId = Convert.ToInt32(existingSubDetail.TownId),
                    SchoolTypeId = existingSubDetail.SchoolTypeId,
                    School = existingSubDetail.School,
                    ReqText = existingRequirement.Requirement,
                    DetailStartDate = existingSubDetail.StartDate,
                    DetailEndDate = existingSubDetail.EndDate,
                    AccNumOfPeople = existingSubDetail.NumberOfPeople,
                    Inn = existingSubDetail.Inn,
                    ProjectInfo = existingSubDetail.ProjectInfo,
                    TrnsStartDate = existingTransportation.StartDate ?? DateTime.Now,
                    TrnsEndDate = existingTransportation.EndDate ?? DateTime.Now,
                    Departure = existingTransportation.Departure,
                    DepartureFirm = existingTransportation.DepartureFirm,
                    Comeback = existingTransportation.ComebackFirm,
                    ComebackFirm = existingTransportation.ComebackFirm,
                    TrnsNumOfPeople = existingTransportation.NumberOfPeople,
                    TrnsArrNumOfPeople = existingTransportation.ComebackNumberOfPeople,
                    TransportationTypeId = existingTransportation.TransportationTypeId,
                    ArrivalTransportationTypeId = existingTransportation.ComebackTransportationTypeId,
                    ActivityList = new List<ActivityDTO>()
                };


                foreach (var activity in ExistingActivityProjectList)
                {
                    int id = activity.ActivityId;
                    string name = activity.Activity.Name;
                    result.ActivityList.Add(new ActivityDTO
                    {
                        Id = id,
                        Name = name
                    });

                }

                serviceResultType = EnumServiceResultType.Success;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<ProjectSubDetailDTO>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<ProjectDetailSummaryDTO> GetProjectDetailSummary(long projectId, long projectDetailId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            ProjectDetailSummaryDTO result = null;
            try
            {
                var existingProjectDetail = _projectDetailRepository.Entities.Where(p => p.Id == projectDetailId && p.ProjectId == projectId).SingleOrDefault();

                if (existingProjectDetail == null)
                    throw new Exception("Proje detayı bulunamadı.");

                result = new ProjectDetailSummaryDTO
                {
                    ProjectId = existingProjectDetail.ProjectId,
                    ProjectDetailId = existingProjectDetail.Id,
                    ProjectName = existingProjectDetail.Project.Name,
                    ProjectStartDate = existingProjectDetail.Project.StartDate,
                    ProjectEndDate = existingProjectDetail.Project.EndDate,
                    ProjectDetailStartDate = existingProjectDetail.StartDate,
                    ProjectDetailEndDate = existingProjectDetail.EndDate
                };

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<ProjectDetailSummaryDTO>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<ParticipantDTO>> GetParticipantList(long projectId, long projectDetailId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ParticipantDTO> result = null;
            try
            {
                result = new List<ParticipantDTO>();

                var participantList = _projectDetailParticipantRepository.Entities.Where(p => p.ProjectId == projectId && p.ProjectDetailId == projectDetailId).ToList();

                foreach (var item in participantList)
                {
                    var newParticipant = new ParticipantDTO();
                    newParticipant.Id = item.Id;
                    newParticipant.UserId = item.UserId;
                    newParticipant.UserTypeId = item.UserTypeId;
                    newParticipant.UserTypeName = EnumHelper.GetEnumDescription(typeof(EnumUserType), item.UserTypeId.ToString());

                    if (item.UserTypeId == (int)EnumUserType.NGOHead)
                    {
                        if (item.User.NgoHead.Any())
                        {
                            newParticipant.FullName = $"{item.User.NgoHead.Single().FirstName} {item.User.NgoHead.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.ProjectManager)
                    {
                        if (item.User.ProjectManager.Any())
                        {
                            newParticipant.FullName = $"{item.User.ProjectManager.Single().FirstName} {item.User.ProjectManager.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                    {
                        if (item.User.ScholarshipCommittee.Any())
                        {
                            newParticipant.FullName = $"{item.User.ScholarshipCommittee.Single().FirstName} {item.User.ScholarshipCommittee.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                    {
                        if (item.User.ScholarshipHolder.Any())
                        {
                            newParticipant.FullName = $"{item.User.ScholarshipHolder.Single().FirstName} {item.User.ScholarshipHolder.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.Donator)
                    {
                        if (item.User.Donator.Any())
                        {
                            newParticipant.FullName = $"{item.User.Donator.Single().FirstName} {item.User.Donator.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.Schoolmaster)
                    {
                        if (item.User.Schoolmaster.Any())
                        {
                            newParticipant.FullName = $"{item.User.Schoolmaster.Single().FirstName} {item.User.Schoolmaster.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                    {
                        if (item.User.HostSchoolTeacher.Any())
                        {
                            newParticipant.FullName = $"{item.User.HostSchoolTeacher.Single().FirstName} {item.User.HostSchoolTeacher.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.Student)
                    {
                        if (item.User.Student.Any())
                        {
                            newParticipant.FullName = $"{item.User.Student.Single().FirstName} {item.User.Student.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.Volunteer)
                    {
                        if (item.User.Volunteer.Any())
                        {
                            newParticipant.FullName = $"{item.User.Volunteer.Single().FirstName} {item.User.Volunteer.Single().LastName}";
                        }
                    }

                    if (item.UserTypeId == (int)EnumUserType.YonDer)
                    {
                        if (item.User.YonDer.Any())
                        {
                            newParticipant.FullName = $"{item.User.YonDer.Single().FirstName} {item.User.YonDer.Single().LastName}";
                        }
                    }

                    result.Add(newParticipant);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<ParticipantDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<long> AddNewParticipant(AddParticipantDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingParticipant = _projectDetailParticipantRepository.Entities.Any(p => p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId && p.UserId == model.UserId && p.UserTypeId == model.UserTypeId);

                if (anyExistingParticipant)
                    throw new Exception("Katılımcı bilgisi mevcut.");

                var newParticipant = new Contracts.Entities.EF.ProjectDetailParticipant
                {
                    ProjectId = model.ProjectId,
                    ProjectDetailId = model.ProjectDetailId,
                    UserId = model.UserId,
                    UserTypeId = model.UserTypeId
                };

                var participantResult = _projectDetailParticipantRepository.Add(newParticipant);
                _unitOfWork.SaveChanges();

                result = participantResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<bool> DeleteParticipant(DeleteParticipantDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var existingParticipant = _projectDetailParticipantRepository.Entities.SingleOrDefault(p => p.Id == model.ProjectDetailParticipantId && p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId);

                if (existingParticipant == null)
                    throw new Exception("Katılımcı bilgisi bulunamadı.");

                _projectDetailParticipantRepository.Delete(existingParticipant);
                _unitOfWork.SaveChanges();

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<ProjectDetailActivityDTO>> GetProjectDetailActivityList(ProjectDetailActivityFilterDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectDetailActivityDTO> result = null;

            try
            {
                result = new List<ProjectDetailActivityDTO>();

                Expression<Func<Contracts.Entities.EF.ProjectDetailActivity, bool>> expProjectDetailActivity = p => true;

                expProjectDetailActivity = expProjectDetailActivity.And(p => p.ProjectDetailId == model.ProjectDetailId);

                if (model.VolunteerId.HasValue)
                {
                    expProjectDetailActivity = expProjectDetailActivity.And(p => p.VolunteerId == model.VolunteerId.Value);
                }

                var activityList = _projectDetailActivityRepository.Entities.Where(expProjectDetailActivity).ToList();

                foreach (var activityGroup in activityList.GroupBy(g => g.ActivityId))
                {
                    foreach (var activity in activityGroup)
                    {
                        var newProjectDetailActivity = new ProjectDetailActivityDTO
                        {
                            Id = activity.Id,
                            ActivityId = activity.ActivityId,
                            ActivityName = activity.Activity.Name,
                            ProjectId = activity.ProjectId,
                            ProjectDetailId = activity.ProjectDetailId,
                            VolunteerId = activity.VolunteerId,
                            UserId = activity.Volunteer.UserId,
                            VolunteerFullName = $"{activity.Volunteer.FirstName} {activity.Volunteer.LastName}",
                            StatusId = activity.StatusId,
                            StatusName = EnumHelper.GetEnumDescription(typeof(EnumActivityStatusType), activity.StatusId.ToString())
                        };

                        result.Add(newProjectDetailActivity);
                    }
                }

                result = result.OrderBy(o => o.ActivityId).ThenBy(o => o.VolunteerFullName).ToList();
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<ProjectDetailActivityDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> AddNewProjectDetailActivity(AddProjectDetailActivityDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingActivityProjectDetail = _projectDetailActivityRepository.Entities.Any(p => p.ActivityId == model.ActivityId && p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId && p.VolunteerId == model.VolunteerId);

                if (anyExistingActivityProjectDetail)
                    throw new Exception("Aktivite bilgisi mevcut.");

                var newActivityProjectDetail = new Contracts.Entities.EF.ProjectDetailActivity
                {
                    ActivityId = model.ActivityId,
                    ProjectId = model.ProjectId,
                    ProjectDetailId = model.ProjectDetailId,
                    VolunteerId = model.VolunteerId,
                    StatusId = model.StatusId
                };

                var activityProjectDetailResult = _projectDetailActivityRepository.Add(newActivityProjectDetail);
                _unitOfWork.SaveChanges();

                result = activityProjectDetailResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<bool> DeleteProjectDetailActivity(DeleteProjectDetailActivityDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var existingActivityProjectDetail = _projectDetailActivityRepository.Entities.SingleOrDefault(p => p.Id == model.ActivityProjectDetailId && p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId);

                if (existingActivityProjectDetail == null)
                    throw new Exception("Aktivite bilgisi bulunamadı.");

                _projectDetailActivityRepository.Delete(existingActivityProjectDetail);
                _unitOfWork.SaveChanges();

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<bool> UpdateProjectDetailActivity(UpdateProjectDetailActivityDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var existingActivityProjectDetail = _projectDetailActivityRepository.Entities.SingleOrDefault(p => p.Id == model.ProjectDetailActivityId && p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId);

                if (existingActivityProjectDetail == null)
                    throw new Exception("Aktivite bilgisi bulunamadı.");

                existingActivityProjectDetail.StatusId = model.StatusId;

                _projectDetailActivityRepository.Update(existingActivityProjectDetail);
                _unitOfWork.SaveChanges();

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNGOInvitation(CreateNGOInvitationDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingNGOInvitation = _NGOInvitationRepository.Entities.Any(p => p.RequirementListId == model.RequirementListId && p.StatusId == model.StatusId);

                if (anyExistingNGOInvitation)
                    throw new Exception("ILKYAR'ı daha önce davet ettiniz.");

                var newNGOInvitation = new Contracts.Entities.EF.NGOInvitation
                {
                    RequirementListId = model.RequirementListId,
                    NumberOfStudent = model.NumberOfStudent,
                    SchoolmasterId = model.SchoolmasterId,
                    StatusId = model.StatusId
                };

                var NGOInvitationResult = _NGOInvitationRepository.Add(newNGOInvitation);
                _unitOfWork.SaveChanges();

                result = NGOInvitationResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<VoteProjectProjectDetailDTO>> GetVoteProjectProjectDetailList(long userId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<VoteProjectProjectDetailDTO> result = null;
            try
            {
                var projectDetailParticipantList = _projectDetailParticipantRepository.Entities.Where(p => p.UserId == userId).ToList();

                result = projectDetailParticipantList.Where(p => p.ProjectDetail.StatusId == (int)EnumProjectSubDetailStatusType.Tamamlandi).Select(p => new VoteProjectProjectDetailDTO
                {
                    ProjectDetailId = p.ProjectDetailId,
                    ProjectDetailName = $"{p.Project.Name} ({p.ProjectDetail.StartDate.ToString("dd/MM/yyyy")} - {p.ProjectDetail.EndDate.ToString("dd/MM/yyyy")})"
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<VoteProjectProjectDetailDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<SurveyProjectDetailQuestionDTO>> GetSurveyProjectDetailQuestionList(long projectDetailId, long userId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<SurveyProjectDetailQuestionDTO> result = null;
            try
            {
                result = new List<SurveyProjectDetailQuestionDTO>();

                var surveyProjectDetailQuestionList = _surveyProjectDetailQuestionRepository.Entities.ToList();
                var projectDetailVoteList = _projectDetailVoteRepository.Entities.Where(p => p.ProjectDetailId == projectDetailId && p.UserId == userId).ToList();

                foreach (var item in surveyProjectDetailQuestionList)
                {
                    SurveyProjectDetailQuestionDTO newSurveyProjectDetailQuestion = new SurveyProjectDetailQuestionDTO();
                    newSurveyProjectDetailQuestion.Id = item.Id;
                    newSurveyProjectDetailQuestion.Name = item.Name;
                    newSurveyProjectDetailQuestion.ProjectDetailId = projectDetailId;
                    newSurveyProjectDetailQuestion.UserId = userId;

                    if (projectDetailVoteList.Any(p => p.SurveyProjectDetailQuestionId == item.Id))
                    {
                        newSurveyProjectDetailQuestion.IsAnswered = true;
                        newSurveyProjectDetailQuestion.Vote = projectDetailVoteList.Single(p => p.SurveyProjectDetailQuestionId == item.Id).Vote;
                    }

                    result.Add(newSurveyProjectDetailQuestion);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<SurveyProjectDetailQuestionDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<long> AddNewSurveyProjectDetailQuestion(AddSurveyProjectDetailQuestionDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var projectDetail = _projectDetailRepository.GetById(model.ProjectDetailId);

                if (projectDetail == null)
                    throw new Exception("Proje detay bilgisi bulunamadı.");

                ProjectDetailVote projectDetailVoteResult = null;

                var existingProjectDetailVote = _projectDetailVoteRepository.Entities.Where(p => p.ProjectId == projectDetail.ProjectId && p.ProjectDetailId == model.ProjectDetailId && p.UserId == model.UserId && p.SurveyProjectDetailQuestionId == model.SurveyProjectDetailQuestionId).SingleOrDefault();

                if (existingProjectDetailVote != null)
                {
                    existingProjectDetailVote.Vote = model.Vote;
                    projectDetailVoteResult = _projectDetailVoteRepository.Update(existingProjectDetailVote);
                }
                else
                {
                    var newProjectDetailVote = new Contracts.Entities.EF.ProjectDetailVote
                    {
                        ProjectId = projectDetail.ProjectId,
                        ProjectDetailId = model.ProjectDetailId,
                        UserId = model.UserId,
                        SurveyProjectDetailQuestionId = model.SurveyProjectDetailQuestionId,
                        Vote = model.Vote
                    };

                    projectDetailVoteResult = _projectDetailVoteRepository.Add(newProjectDetailVote);
                }

                _unitOfWork.SaveChanges();

                result = projectDetailVoteResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<VoteVolunteerProjectDetailDTO>> GetVoteVolunteerProjectDetailList(long userId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<VoteVolunteerProjectDetailDTO> result = null;
            try
            {
                var projectDetailParticipantList = _projectDetailParticipantRepository.Entities.Where(p => p.UserId == userId).ToList();

                result = projectDetailParticipantList.Where(p => p.ProjectDetail.StatusId == (int)EnumProjectSubDetailStatusType.Tamamlandi).Select(p => new VoteVolunteerProjectDetailDTO
                {
                    ProjectDetailId = p.ProjectDetailId,
                    ProjectDetailName = $"{p.Project.Name} ({p.ProjectDetail.StartDate.ToString("dd/MM/yyyy")} - {p.ProjectDetail.EndDate.ToString("dd/MM/yyyy")})"
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<VoteVolunteerProjectDetailDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<EvaluateVolunteerDTO>> GetEvaluateVolunteerList(long projectDetailId, long userId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<EvaluateVolunteerDTO> result = null;
            try
            {
                result = new List<EvaluateVolunteerDTO>();

                var projectDetailActivityList = _projectDetailActivityRepository.Entities.Where(p => p.ProjectDetailId == projectDetailId && p.StatusId == (int)EnumActivityStatusType.Onaylandi).ToList();
                var volunteerVoteList = _volunteerVoteRepository.Entities.Where(p => p.ProjectDetailId == projectDetailId && p.UserId == userId).ToList();

                foreach (var item in projectDetailActivityList)
                {
                    EvaluateVolunteerDTO newEvaluateVolunteer = new EvaluateVolunteerDTO();
                    newEvaluateVolunteer.ActivityId = item.ActivityId;
                    newEvaluateVolunteer.ActivityName = item.Activity.Name;
                    newEvaluateVolunteer.VolunteerId = item.VolunteerId;
                    newEvaluateVolunteer.VolunteerName = $"{item.Volunteer.FirstName} {item.Volunteer.LastName}";

                    if (volunteerVoteList.Any(p => p.ActivityId == item.ActivityId && p.VolunteerId == item.VolunteerId))
                    {
                        newEvaluateVolunteer.Vote = volunteerVoteList.Single(p => p.ActivityId == item.ActivityId && p.VolunteerId == item.VolunteerId).Vote;
                    }

                    result.Add(newEvaluateVolunteer);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<EvaluateVolunteerDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<long> AddNewVolunteerVote(AddVolunteerVoteDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var projectDetail = _projectDetailRepository.GetById(model.ProjectDetailId);

                if (projectDetail == null)
                    throw new Exception("Proje detay bilgisi bulunamadı.");

                VolunteerVote volunteerVoteResult = null;

                var existingVolunteerVote = _volunteerVoteRepository.Entities.Where(p => p.ProjectId == projectDetail.ProjectId && p.ProjectDetailId == model.ProjectDetailId && p.UserId == model.UserId && p.VolunteerId == model.VolunteerId && p.ActivityId == model.ActivityId).SingleOrDefault();

                if (existingVolunteerVote != null)
                {
                    existingVolunteerVote.Vote = model.Vote;
                    volunteerVoteResult = _volunteerVoteRepository.Update(existingVolunteerVote);
                }
                else
                {
                    var newProjectDetailVote = new Contracts.Entities.EF.VolunteerVote
                    {
                        ProjectId = projectDetail.ProjectId,
                        ProjectDetailId = model.ProjectDetailId,
                        UserId = model.UserId,
                        VolunteerId = model.VolunteerId,
                        ActivityId = model.ActivityId,
                        Vote = model.Vote
                    };

                    volunteerVoteResult = _volunteerVoteRepository.Add(newProjectDetailVote);
                }

                _unitOfWork.SaveChanges();

                result = volunteerVoteResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<LeadershipBoardDTO>> GetLeadershipBoardList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<LeadershipBoardDTO> result = null;
            try
            {
                var volunteerList = _volunteerRepository.Entities.ToList();
                var totalActivityList = _projectDetailActivityRepository.Entities.ToList();
                double totalActivityCount = totalActivityList.Count;
                double attendancePercentage = 0;

                result = volunteerList.Select(p => new LeadershipBoardDTO
                {
                    UserTypeId = p.User.UserTypeId,
                    VolunteerId = p.Id,
                    VolunteerName = $"{p.FirstName} {p.LastName}",
                    TotalVote = p.VolunteerVote.Sum(s => s.Vote)
                }).ToList();

                foreach (var volunteer in result)
                {                    
                    var activities = totalActivityList.Where(p => p.VolunteerId == volunteer.VolunteerId).ToList();
                    double activityCount = activities.Count;
                    var votes = _volunteerVoteRepository.Entities.Where(p => p.VolunteerId == volunteer.VolunteerId).ToList().Count;
                    if (totalActivityCount != 0)
                    {
                        attendancePercentage = (activityCount / totalActivityCount) * 100;
                        volunteer.TotalActivity = attendancePercentage.ToString("#.##");
                        if (volunteer.TotalActivity == "")
                        {
                            volunteer.TotalActivity = "0";
                        }
                    }
                    
                    if (votes != 0)
                    {
                        volunteer.AvgVote = volunteer.TotalVote / votes ; 
                    }
                   
                }
                result = result.OrderByDescending(o => o.TotalVote).ThenByDescending(o => o.TotalActivity).ToList();

                int orderNumber = 0;

                foreach (var item in result)
                {
                    orderNumber++;
                    item.OrderNumber = orderNumber;
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<LeadershipBoardDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<ProjectDetailActivityScheduleDTO>> GetProjectDetailScheduleList(long projectDetailId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectDetailActivityScheduleDTO> result = null;
            try
            {
                result = new List<ProjectDetailActivityScheduleDTO>();

                var existingProjectDetail = _projectDetailRepository.GetById(projectDetailId);

                if (existingProjectDetail == null)
                    throw new Exception("Proje detay bilgisi bulunamadı.");

                var projectDetailActivityScheduleList = _projectDetailActivityScheduleRepository.Entities.Where(p => p.ProjectId == existingProjectDetail.ProjectId && p.ProjectDetailId == existingProjectDetail.Id).ToList();

                foreach (var item in projectDetailActivityScheduleList)
                {
                    ProjectDetailActivityScheduleDTO newProjectDetailSchedule = new ProjectDetailActivityScheduleDTO();
                    newProjectDetailSchedule.ProjectDetailActivityScheduleId = item.Id;
                    newProjectDetailSchedule.ProjectId = item.ProjectId;
                    newProjectDetailSchedule.ProjectDetailId = item.ProjectDetailId;
                    newProjectDetailSchedule.ProjectDetailActivityId = item.ProjectDetailActivityId;
                    newProjectDetailSchedule.StartDate = item.StartDate;
                    newProjectDetailSchedule.EndDate = item.EndDate;
                    newProjectDetailSchedule.SummaryInfo = $"{item.ProjectDetailActivity.Volunteer.FirstName} {item.ProjectDetailActivity.Volunteer.LastName} ({item.ProjectDetailActivity.Activity.Name})";

                    result.Add(newProjectDetailSchedule);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<ProjectDetailActivityScheduleDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<bool> UpdateProjectDetailActivitySchedule(UpdateProjectDetailActivityScheduleDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                if (model.OperationTypeId == (int)EnumProjectDetailActivityScheduleOperationType.Insert)
                {
                    var existingProjectDetail = _projectDetailRepository.GetById(model.ProjectDetailId);

                    if (existingProjectDetail == null)
                        throw new Exception("Proje detay bilgisi bulunamadı.");

                    var anyExistingProjectDetailActivitySchedule = _projectDetailActivityScheduleRepository.Entities.Any(p => p.ProjectId == existingProjectDetail.ProjectId && p.ProjectDetailId == existingProjectDetail.Id && p.ProjectDetailActivityId == model.ProjectDetailActivityId);

                    if (anyExistingProjectDetailActivitySchedule)
                        throw new Exception("Proje çizelgesi bilgisi mevcut.");

                    var newProjectDetailActivitySchedule = new ProjectDetailActivitySchedule
                    {
                        ProjectId = existingProjectDetail.ProjectId,
                        ProjectDetailId = existingProjectDetail.Id,
                        ProjectDetailActivityId = model.ProjectDetailActivityId,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate
                    };

                    _projectDetailActivityScheduleRepository.Add(newProjectDetailActivitySchedule);
                    _unitOfWork.SaveChanges();
                }
                if (model.OperationTypeId == (int)EnumProjectDetailActivityScheduleOperationType.Update)
                {
                    var existingProjectDetail = _projectDetailRepository.GetById(model.ProjectDetailId);

                    if (existingProjectDetail == null)
                        throw new Exception("Proje detay bilgisi bulunamadı.");

                    var existingProjectDetailActivitySchedule = _projectDetailActivityScheduleRepository.GetById(model.ProjectDetailActivityScheduleId);

                    if (existingProjectDetailActivitySchedule == null)
                        throw new Exception("Proje çizelgesi bilgisi bulunamadı.");

                    var anyExistingProjectDetailActivitySchedule = _projectDetailActivityScheduleRepository.Entities.Any(p => p.Id != existingProjectDetailActivitySchedule.Id && p.ProjectId == existingProjectDetail.ProjectId && p.ProjectDetailId == existingProjectDetail.Id && p.ProjectDetailActivityId == model.ProjectDetailActivityId);

                    if (anyExistingProjectDetailActivitySchedule)
                        throw new Exception("Proje çizelgesi bilgisi mevcut.");

                    existingProjectDetailActivitySchedule.ProjectDetailActivityId = model.ProjectDetailActivityId;
                    existingProjectDetailActivitySchedule.StartDate = model.StartDate;
                    existingProjectDetailActivitySchedule.EndDate = model.EndDate;

                    _projectDetailActivityScheduleRepository.Update(existingProjectDetailActivitySchedule);
                    _unitOfWork.SaveChanges();
                }
                if (model.OperationTypeId == (int)EnumProjectDetailActivityScheduleOperationType.Delete)
                {
                    var existingProjectDetailActivitySchedule = _projectDetailActivityScheduleRepository.GetById(model.ProjectDetailActivityScheduleId);

                    if (existingProjectDetailActivitySchedule == null)
                        throw new Exception("Proje çizelgesi bilgisi bulunamadı.");

                    _projectDetailActivityScheduleRepository.Delete(existingProjectDetailActivitySchedule);
                    _unitOfWork.SaveChanges();
                }

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<ProjectScheduleProjectDetailDTO>> GetProjectScheduleProjectDetailList(long userId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectScheduleProjectDetailDTO> result = null;
            try
            {
                var projectDetailList = _projectDetailRepository.Entities.Where(p => p.Project.ProjectManager.UserId == userId).ToList();

                result = projectDetailList.Where(p => p.StatusId == (int)EnumProjectSubDetailStatusType.Aktif).Select(p => new ProjectScheduleProjectDetailDTO
                {
                    ProjectDetailId = p.Id,
                    ProjectDetailName = $"{p.Project.Name} ({p.StartDate.ToString("dd/MM/yyyy")} - {p.EndDate.ToString("dd/MM/yyyy")})",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<ProjectScheduleProjectDetailDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<NGOInvitationDTO>> GetNGOInvitationList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<NGOInvitationDTO> result = null;

            try
            {
                var NGOInvitationList = _NGOInvitationRepository.Entities.ToList();

                result = NGOInvitationList.Select(p => new NGOInvitationDTO
                {
                    Id = p.Id,
                    //SchoolmasterId = p.SchoolmasterId,
                    //SchoolmasterName = $"{p.Schoolmaster.FirstName} {p.Schoolmaster.LastName}",
                    SchoolName = p.Schoolmaster.School,
                    City = p.Schoolmaster.City.Name,
                    Town = p.Schoolmaster.Town.Name,
                    RequirementList = p.UploadRequirementList.Name,
                    NumberOfStudent = Convert.ToInt32(p.NumberOfStudent),
                    StatusId = p.StatusId,
                    StatusName = EnumHelper.GetEnumDescription(typeof(EnumInvitationStatusType), p.StatusId.ToString())
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<NGOInvitationDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<bool> UpdateNGOInvitation(UpdateNGOInvitationDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var existingInvitationDetail = _NGOInvitationRepository.Entities.SingleOrDefault(p => p.Id == model.Id);

                if (existingInvitationDetail == null)
                    throw new Exception("Davet bilgisi bulunamadı.");

                existingInvitationDetail.StatusId = model.StatusId;

                _NGOInvitationRepository.Update(existingInvitationDetail);
                _unitOfWork.SaveChanges();

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<SurveyProjectDetailResultDTO>> GetSurveyProjectDetailResultList(long projectId, long projectDetailId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<SurveyProjectDetailResultDTO> result = null;
            try
            {
                result = new List<SurveyProjectDetailResultDTO>();

                var surveyProjectDetailResultList = _projectDetailVoteRepository.Entities.Where(p => p.ProjectDetailId == projectDetailId).ToList();

                foreach (var item in surveyProjectDetailResultList.GroupBy(g => g.SurveyProjectDetailQuestionId))
                {
                    SurveyProjectDetailResultDTO newSurveyProjectDetailResult = new SurveyProjectDetailResultDTO();
                    newSurveyProjectDetailResult.ProjectDetailId = item.First().ProjectDetailId;
                    newSurveyProjectDetailResult.ProjectDetailName = $"{item.First().Project.Name} ({item.First().ProjectDetail.StartDate.ToString("dd/MM/yyyy")} - {item.First().ProjectDetail.EndDate.ToString("dd/MM/yyyy")})";
                    newSurveyProjectDetailResult.SurveyProjectDetailQuestionId = item.First().SurveyProjectDetailQuestionId;
                    newSurveyProjectDetailResult.SurveyProjectDetailQuestionName = item.First().SurveyProjectDetailQuestion.Name;
                    newSurveyProjectDetailResult.Vote = item.Sum(s => s.Vote);

                    result.Add(newSurveyProjectDetailResult);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<SurveyProjectDetailResultDTO>>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }

        public ServiceResult<List<ActivityDTO>> GetActivityList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ActivityDTO> result = null;
            try
            {
                result = new List<ActivityDTO>();
                var activityList = _activityRepository.Entities.ToList();
                foreach (var item in activityList)
                {
                    result.Add(new ActivityDTO
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
            return new ServiceResult<List<ActivityDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> AddNewProjectActivity(AddProjectActivityDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingActivityProject = _projectDetailActivityRepository.Entities.Any(p => p.ActivityId == model.ActivityId && p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId);

                if (anyExistingActivityProject)
                    throw new Exception("Aktivite bilgisi mevcut.");

                var newActivityProject = new Contracts.Entities.EF.ProjectActivity
                {
                    ActivityId = model.ActivityId,
                    ProjectId = model.ProjectId,

                    ProjectDetailId = model.ProjectDetailId
                };

                var activityProjectResult = _projectActivityRepository.Add(newActivityProject);
                _unitOfWork.SaveChanges();

                result = activityProjectResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };

        }

        public ServiceResult<List<ProjectActivityDTO>> GetProjectActivityList(long projectId, long projectDetailId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectActivityDTO> result = null;

            try
            {
                var ProjectActivityList = _projectActivityRepository.Entities.Where(p => p.ProjectId == projectId && p.ProjectDetailId == projectDetailId).ToList();

                result = ProjectActivityList.Select(p => new ProjectActivityDTO
                {
                    ActivityName = p.Activity.Name
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<ProjectActivityDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<ProjectDetailSuggestionDTO>> GetProjectDetailSuggestionList(long volunteerId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ProjectDetailSuggestionDTO> result = null;

            try
            {
                result = new List<ProjectDetailSuggestionDTO>();

                var projectDetailList = _projectDetailRepository.Entities.Where(p => p.StatusId == (int)EnumProjectSubDetailStatusType.Aktif).ToList();

                var volunteerInterestList = _volunteerInterestRepository.Entities.Where(v => v.VolunteerId == volunteerId).ToList();

                foreach (var item in projectDetailList)
                {
                    var projectActivityList = _projectActivityRepository.Entities.Where(p => p.ProjectDetailId == item.Id).ToList();

                    foreach (var activity in projectActivityList)
                    {
                        foreach (var interest in volunteerInterestList)
                        {
                            if (activity.ActivityId == interest.ActivityId)
                            {
                                var projectDetailActivityList = _projectDetailActivityRepository.Entities.Where(s => s.ProjectDetailId == item.Id && s.VolunteerId == volunteerId && s.StatusId != (int)EnumActivityStatusType.Onaylandi).ToList();

                                if (projectDetailActivityList.Count == 0)
                                {
                                    if (item.StatusId == (int)EnumProjectSubDetailStatusType.Aktif)
                                    {
                                        var projectSuggestion = new ProjectDetailSuggestionDTO
                                        {
                                            ProjectId = Convert.ToInt32(item.Project.Id),
                                            ProjectDetailId = item.Id,
                                            ActivityId = activity.ActivityId,
                                            ActivityName = activity.Activity.Name,
                                            ProjectDetailName = $"{item.Project.Name} ({item.StartDate.ToString("dd/MM/yyyy")} - {item.EndDate.ToString("dd/MM/yyyy")})"
                                        };

                                        result.Add(projectSuggestion);
                                    }
                                }

                                foreach (var detailActivity in projectDetailActivityList)
                                {
                                    if (detailActivity.StatusId == (int)EnumActivityStatusType.Reddedildi)
                                    {
                                        if (item.StatusId == (int)EnumProjectSubDetailStatusType.Aktif)
                                        {
                                            var projectSuggestion = new ProjectDetailSuggestionDTO
                                            {
                                                ProjectId = Convert.ToInt32(item.Project.Id),
                                                ProjectDetailId = item.Id,
                                                ActivityId = activity.ActivityId,
                                                ActivityName = activity.Activity.Name,
                                                ProjectDetailName = $"{item.Project.Name} ({item.StartDate.ToString("dd/MM/yyyy")} - {item.EndDate.ToString("dd/MM/yyyy")})"
                                            };

                                            result.Add(projectSuggestion);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                result = result.OrderBy(o => o.ProjectDetailStartDate).ToList();
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<List<ProjectDetailSuggestionDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<bool> UpdateProjectActivity(UpdateProjectActivityDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var anyExistingActivityProject = _projectDetailActivityRepository.Entities.Any(p => p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId);

                if (!anyExistingActivityProject)
                    throw new Exception("Aktivite bilgisi bulunamadı.");

                var ExistingActivityProjectList = _projectDetailActivityRepository.Entities.Where(p => p.ProjectId == model.ProjectId && p.ProjectDetailId == model.ProjectDetailId).ToList();

                foreach (var activity in ExistingActivityProjectList)
                {
                    if (!model.ActivityList.Select(s => s.Id).Contains(activity.ActivityId))
                    {
                        var deleteActivity = new Contracts.Entities.EF.ProjectActivity
                        {
                            Id = activity.Id,
                            ActivityId = activity.ActivityId,
                            ProjectId = model.ProjectId,
                            ProjectDetailId = model.ProjectDetailId
                        };
                        _projectActivityRepository.Delete(deleteActivity);
                        _unitOfWork.SaveChanges();
                    };

                }

                foreach (var item in model.ActivityList)
                {
                    if (!ExistingActivityProjectList.Select(s => s.ActivityId).Contains(item.Id))
                    {
                        var newActivityProject = new Contracts.Entities.EF.ProjectActivity
                        {
                            ActivityId = item.Id,
                            ProjectId = model.ProjectId,
                            ProjectDetailId = model.ProjectDetailId
                        };
                        _projectActivityRepository.Add(newActivityProject);
                        _unitOfWork.SaveChanges();
                    };

                }

                result = true;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<bool> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<VolunteerSuggestionDTO> GetVolunteerSuggestion(VolunteerSuggestionFilterDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            VolunteerSuggestionDTO result = null;

            try
            {
                result = new VolunteerSuggestionDTO();

                var projectDetailActivity = _projectDetailActivityRepository.GetById(model.ProjectDetailActivityId);

                if (projectDetailActivity == null)
                    throw new Exception("Proje detay aktivite bilgisi bulunamadı.");

                var volunteer = _volunteerRepository.GetById(projectDetailActivity.VolunteerId);

                if (volunteer == null)
                    throw new Exception("Gönüllü bilgisi bulunamadı.");

                var activity = _activityRepository.GetById(projectDetailActivity.ActivityId);

                if (volunteer == null)
                    throw new Exception("Aktivite bilgisi bulunamadı.");

                var projectDetail = _projectDetailRepository.GetById(projectDetailActivity.ProjectDetailId);

                if (projectDetail == null)
                    throw new Exception("Proje detay bilgisi bulunamadı.");

                result.VolunteerFullName = $"{volunteer.FirstName} {volunteer.LastName}";
                result.CurrentActivityName = activity.Name;

                var volunteerProjectDetailActivityList = _projectDetailActivityRepository.Entities.Where(p => p.VolunteerId == volunteer.Id).ToList();

                #region City

                double approvedCityMatchCount = volunteerProjectDetailActivityList.Count(c => c.StatusId == (int)EnumActivityStatusType.Onaylandi && c.ProjectDetail.CityId == projectDetail.CityId);
                double overallCityMatchCount = volunteerProjectDetailActivityList.Count(c => c.ProjectDetail.CityId == projectDetail.CityId);

                result.ApprovedCityMatchPercentage = approvedCityMatchCount / volunteerProjectDetailActivityList.Count * 100;
                result.OverallCityMatchPercentage = overallCityMatchCount / volunteerProjectDetailActivityList.Count * 100;

                #endregion

                #region Region 

                double approvedRegionMatchCount = volunteerProjectDetailActivityList.Count(c => c.StatusId == (int)EnumActivityStatusType.Onaylandi && c.ProjectDetail.City.RegionId == projectDetail.City.RegionId);
                double overallRegionMatchCount = volunteerProjectDetailActivityList.Count(c => c.ProjectDetail.City.RegionId == projectDetail.City.RegionId);

                result.ApprovedRegionMatchPercentage = approvedRegionMatchCount / volunteerProjectDetailActivityList.Count * 100;
                result.OverallRegionMatchPercentage = overallRegionMatchCount / volunteerProjectDetailActivityList.Count * 100;

                #endregion

                #region SchoolType

                double approvedSchoolTypeMatchCount = volunteerProjectDetailActivityList.Count(c => c.StatusId == (int)EnumActivityStatusType.Onaylandi && c.ProjectDetail.SchoolTypeId == projectDetail.SchoolTypeId);
                double overallSchoolTypeMatchCount = volunteerProjectDetailActivityList.Count(c => c.ProjectDetail.SchoolTypeId == projectDetail.SchoolTypeId);

                result.ApprovedSchoolTypeMatchPercentage = approvedSchoolTypeMatchCount / volunteerProjectDetailActivityList.Count * 100;
                result.OverallSchoolTypeMatchPercentage = overallSchoolTypeMatchCount / volunteerProjectDetailActivityList.Count * 100;

                #endregion

                #region ProjectType

                double approvedProjectTypeMatchCount = volunteerProjectDetailActivityList.Count(c => c.StatusId == (int)EnumActivityStatusType.Onaylandi && c.ProjectDetail.Project.ProjectTypeId == projectDetail.Project.ProjectTypeId);
                double overallProjectTypeMatchCount = volunteerProjectDetailActivityList.Count(c => c.ProjectDetail.Project.ProjectTypeId == projectDetail.Project.ProjectTypeId);

                result.ApprovedProjectTypeMatchPercentage = approvedProjectTypeMatchCount / volunteerProjectDetailActivityList.Count * 100;
                result.OverallProjectTypeMatchPercentage = overallProjectTypeMatchCount / volunteerProjectDetailActivityList.Count * 100;

                #endregion

                #region NumberOfPeople 

                double approvedNumberOfPeopleMatchSum = volunteerProjectDetailActivityList.Where(p => p.StatusId == (int)EnumActivityStatusType.Onaylandi).Select(s => s.ProjectDetail.NumberOfPeople).Average();
                double overallNumberOfPeopleMatchSum = volunteerProjectDetailActivityList.Select(s => s.ProjectDetail.NumberOfPeople).Average();

                result.ApprovedNumberOfPeopleMatchTolerancePercentage = projectDetail.NumberOfPeople / approvedNumberOfPeopleMatchSum;
                result.OverallNumberOfPeopleMatchTolerancePercentage = projectDetail.NumberOfPeople / overallNumberOfPeopleMatchSum;

                #endregion  

                result.ApprovedActivityCount = volunteerProjectDetailActivityList.Count(c => c.StatusId == (int)EnumActivityStatusType.Onaylandi);
                result.RejectedActivityCount = volunteerProjectDetailActivityList.Count(c => c.StatusId == (int)EnumActivityStatusType.Reddedildi);
                result.OverallActivityCount = volunteerProjectDetailActivityList.Count();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }

            return new ServiceResult<VolunteerSuggestionDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }
    }

}

