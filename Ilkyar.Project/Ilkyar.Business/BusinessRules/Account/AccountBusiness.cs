using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.EF;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.ServiceContracts.Account;
using Ilkyar.Contracts.Services;
using Ilkyar.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ilkyar.Business.BusinessRules.Account
{
    public class AccountBusiness : IAccount
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Contracts.Entities.EF.User> _userRepository;
        private readonly IRepository<Occupation> _occupationRepository;
        private readonly IRepository<Donator> _donatorRepository;
        private readonly IRepository<YonDer> _yonderRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<ProjectDetailActivity> _projectDetailActivityRepository;
        private readonly IRepository<VolunteerVote> _volunteerVoteRepository;
        private readonly IRepository<ProjectDetail> _projectDetailRepository;
        private readonly IRepository<Volunteer> _volunteerRepository;
        private readonly IRepository<ProjectManager> _projectManagerRepository;
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<ProjectDetailVote> _projectDetailVoteRepository;

        public AccountBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.User>();
            _occupationRepository = _unitOfWork.GetRepository<Occupation>();
            _donatorRepository = _unitOfWork.GetRepository<Donator>();
            _yonderRepository = _unitOfWork.GetRepository<YonDer>();
            _branchRepository = _unitOfWork.GetRepository<Branch>();
            _departmentRepository = _unitOfWork.GetRepository<Department>();
            _projectDetailActivityRepository = _unitOfWork.GetRepository<ProjectDetailActivity>();
            _volunteerVoteRepository = _unitOfWork.GetRepository<VolunteerVote>();
            _projectDetailRepository = _unitOfWork.GetRepository<ProjectDetail>();
            _volunteerRepository = _unitOfWork.GetRepository<Volunteer>();
            _projectManagerRepository = _unitOfWork.GetRepository<ProjectManager>();
            _activityRepository = _unitOfWork.GetRepository<Activity>();
            _projectDetailVoteRepository = _unitOfWork.GetRepository<ProjectDetailVote>();
        }

        public ServiceResult<UserDTO> Login(string username, string password)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            UserDTO result = null;
            try
            {
                var user = _userRepository.Entities.Where(m => m.Username == username && m.Password == password).SingleOrDefault();

                if (user == null)
                {
                    errorMessage = "Girdiğiniz bilgiler hatalı! Lütfen kontrol edip tekrar deneyin.";
                    throw new Exception(errorMessage);
                }

                result = new UserDTO()
                {
                    UserId = user.Id,
                    UserTypeId = user.UserTypeId,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    IsActive = user.IsActive
                };

                //---NGO Head Info---//
                if (user.UserTypeId == (int)EnumUserType.NGOHead)
                {
                    if (user.NgoHead.Any())
                    {
                        var currentNgoHead = user.NgoHead.Single();

                        result.Id = currentNgoHead.Id;
                        result.FirstName = currentNgoHead.FirstName;
                        result.LastName = currentNgoHead.LastName;
                        result.BirthDate = currentNgoHead.BirthDate;
                        result.PhoneNum = currentNgoHead.PhoneNumber;
                        result.DutyStartDate = currentNgoHead.DutyStartDate;
                        result.DutyEndDate = currentNgoHead.DutyEndDate;
                    }
                }

                //---Project Manager Info---//
                if (user.UserTypeId == (int)EnumUserType.ProjectManager)
                {
                    if (user.ProjectManager.Any())
                    {
                        var currentProjectManager = user.ProjectManager.Single();

                        result.Id = currentProjectManager.Id;
                        result.FirstName = currentProjectManager.FirstName;
                        result.LastName = currentProjectManager.LastName;
                        result.BirthDate = currentProjectManager.BirthDate;
                        result.PhoneNum = currentProjectManager.PhoneNumber;
                        result.DutyStartDate = currentProjectManager.DutyStartDate;
                        result.DutyEndDate = currentProjectManager.DutyEndDate;
                    }
                }

                //---Scholarship Committee Info---//
                if (user.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                {
                    if (user.ScholarshipCommittee.Any())
                    {
                        var currentScholarshipCommittee = user.ScholarshipCommittee.Single();

                        result.Id = currentScholarshipCommittee.Id;
                        result.FirstName = currentScholarshipCommittee.FirstName;
                        result.LastName = currentScholarshipCommittee.LastName;
                        result.BirthDate = currentScholarshipCommittee.BirthDate;
                        result.Title = currentScholarshipCommittee.Title;
                        result.PhoneNum = currentScholarshipCommittee.PhoneNumber;
                        result.DutyStartDate = currentScholarshipCommittee.DutyStartDate;
                        result.DutyEndDate = currentScholarshipCommittee.DutyEndDate;
                    }
                }

                //---Scholarship Holder Info---//
                if (user.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                {
                    if (user.ScholarshipHolder.Any())
                    {
                        var currentScholarshipHolder = user.ScholarshipHolder.Single();

                        result.Id = currentScholarshipHolder.Id;
                        result.FirstName = currentScholarshipHolder.FirstName;
                        result.LastName = currentScholarshipHolder.LastName;
                        if (currentScholarshipHolder.Donator != null)
                        {
                            result.DonatorName = $"{currentScholarshipHolder.Donator.FirstName} {currentScholarshipHolder.Donator.LastName}";
                        }
                        if (currentScholarshipHolder.YonDer != null)
                        {
                            result.YonDerName = $"{currentScholarshipHolder.YonDer.FirstName} {currentScholarshipHolder.YonDer.LastName}";
                        }
                        result.BirthDate = currentScholarshipHolder.BirthDate;
                        result.PhoneNum = currentScholarshipHolder.PhoneNumber;
                        result.ScholarshipStartDate = currentScholarshipHolder.ScholarshipStartDate;
                        result.ScholarshipEndDate = currentScholarshipHolder.ScholarshipEndDate;
                        result.ScholarshipAmount = currentScholarshipHolder.ScholarshipAmount;
                        result.IbanNo = currentScholarshipHolder.IbanNo;

                        #region EducationInfo
                        result.EducationLevel = currentScholarshipHolder.EducationLevel;
                        if (currentScholarshipHolder.School != null)
                        {
                            result.School = currentScholarshipHolder.School;
                        }
                        result.Class = currentScholarshipHolder.Class;
                        result.CumGPA = currentScholarshipHolder.CumGPA;
                        result.StudentDocument = currentScholarshipHolder.StudentDocument;
                        result.Transcript = currentScholarshipHolder.Transcript;
                        #endregion

                        result.HealthConditionInfo = currentScholarshipHolder.HealthConditionInfo;
                        result.MonthlyIncome = currentScholarshipHolder.MonthlyIncome;

                        #region MotherInfo
                        result.MotherName = currentScholarshipHolder.MotherName;
                        if (currentScholarshipHolder.IsMotherWorking)
                        {
                            var occupation = _occupationRepository.GetById(currentScholarshipHolder.MotherOccupationId);
                            if (occupation != null)
                                result.MotherOccupation = occupation.Name;
                        }
                        #endregion

                        #region FatherInfo
                        result.FatherName = currentScholarshipHolder.FatherName;
                        if (currentScholarshipHolder.IsFatherWorking)
                        {
                            var occupation = _occupationRepository.GetById(currentScholarshipHolder.FatherOccupationId);
                            if (occupation != null)
                                result.FatherOccupation = occupation.Name;
                        }
                        #endregion

                        result.NumberOfSiblings = currentScholarshipHolder.NumberOfSiblings;
                    }
                }

                //---Donator Info---//
                if (user.UserTypeId == (int)EnumUserType.Donator)
                {
                    if (user.Donator.Any())
                    {
                        var currentDonator = user.Donator.Single();

                        result.Id = currentDonator.Id;
                        result.FirstName = currentDonator.FirstName;
                        result.LastName = currentDonator.LastName;

                        if (currentDonator.Occupation != null)
                        {
                            result.Occupation = currentDonator.Occupation.Name;
                        }
                        result.BirthDate = currentDonator.BirthDate;
                        result.PhoneNum = currentDonator.PhoneNumber;
                        result.WorkPlace = currentDonator.WorkPlace;

                    }
                }

                //---Schoolmaster Info---//
                if (user.UserTypeId == (int)EnumUserType.Schoolmaster)
                {
                    if (user.Schoolmaster.Any())
                    {
                        var currentSchoolmaster = user.Schoolmaster.Single();

                        result.Id = currentSchoolmaster.Id;
                        result.FirstName = currentSchoolmaster.FirstName;
                        result.LastName = currentSchoolmaster.LastName;
                        result.BirthDate = currentSchoolmaster.BirthDate;
                        result.PhoneNum = currentSchoolmaster.PhoneNumber;
                        if (currentSchoolmaster.School != null)
                        {
                            result.School = currentSchoolmaster.School;
                        }
                    }
                }

                //---Host School Teacher Info---//
                if (user.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                {
                    if (user.HostSchoolTeacher.Any())
                    {
                        var currentHostSchoolTeacher = user.HostSchoolTeacher.Single();

                        result.Id = currentHostSchoolTeacher.Id;
                        result.FirstName = currentHostSchoolTeacher.FirstName;
                        result.LastName = currentHostSchoolTeacher.LastName;
                        result.BirthDate = currentHostSchoolTeacher.BirthDate;
                        result.PhoneNum = currentHostSchoolTeacher.PhoneNumber;
                        if (currentHostSchoolTeacher.School != null)
                        {
                            result.School = currentHostSchoolTeacher.School;
                        }
                        if (currentHostSchoolTeacher.Branch != null)
                        {
                            result.Branch = currentHostSchoolTeacher.Branch.Name;
                        }
                    }
                }

                //---Student Info---//
                if (user.UserTypeId == (int)EnumUserType.Student)
                {
                    if (user.Student.Any())
                    {
                        var currentStudent = user.Student.Single();

                        result.Id = currentStudent.Id;
                        result.FirstName = currentStudent.FirstName;
                        result.LastName = currentStudent.LastName;
                        result.BirthDate = currentStudent.BirthDate;
                        result.PhoneNum = currentStudent.PhoneNumber;
                        if (currentStudent.School != null)
                        {
                            result.School = currentStudent.School;
                        }
                        result.CumGPA = currentStudent.CumGPA;
                        result.Class = currentStudent.Class;
                        result.EducationLevel = currentStudent.EducationLevel;
                    }

                }

                //---Volunteer Info---//
                if (user.UserTypeId == (int)EnumUserType.Volunteer)
                {
                    if (user.Volunteer.Any())
                    {
                        var currentVolunteer = user.Volunteer.Single();

                        result.Id = currentVolunteer.Id;
                        result.FirstName = currentVolunteer.FirstName;
                        result.LastName = currentVolunteer.LastName;
                        result.BirthDate = currentVolunteer.BirthDate;
                        result.PhoneNum = currentVolunteer.PhoneNumber;
                        result.IsStudent = currentVolunteer.IsStudent;

                        result.UniversityId = currentVolunteer.UniversityId;
                        result.DepartmentId = currentVolunteer.DepartmentId;
                        result.Class = currentVolunteer.Class;
                        result.OccupationId = currentVolunteer.OccupationId;

                        if (currentVolunteer.University != null)
                        {
                            result.University = currentVolunteer.University.Name;
                        }
                        if (currentVolunteer.Department != null)
                        {
                            result.Department = currentVolunteer.Department.Name;
                        }
                        if (currentVolunteer.Occupation != null)
                        {
                            result.Occupation = currentVolunteer.Occupation.Name;
                        }
                    }
                }

                //---Yön-Der Info---//
                if (user.UserTypeId == (int)EnumUserType.YonDer)
                {
                    if (user.YonDer.Any())
                    {
                        var currentYonDer = user.YonDer.Single();

                        result.Id = currentYonDer.Id;
                        result.FirstName = currentYonDer.FirstName;
                        result.LastName = currentYonDer.LastName;
                        result.BirthDate = currentYonDer.BirthDate;
                        result.PhoneNum = currentYonDer.PhoneNumber;
                        result.DutyStartDate = currentYonDer.DutyStartDate;
                        result.DutyEndDate = currentYonDer.DutyEndDate;
                    }
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<UserDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewNGOHead(CreateNewNGOHeadDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newNGOHead = new NgoHead
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNum
                };

                newUser.NgoHead.Add(newNGOHead);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewProjectManager(CreateNewProjectManagerDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newProjectManager = new ProjectManager
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNum
                };

                newUser.ProjectManager.Add(newProjectManager);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewScholarshipCommittee(CreateNewScholarshipCommitteeDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newScholarshipCommittee = new ScholarshipCommittee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNum,
                    Title = model.Title
                };

                newUser.ScholarshipCommittee.Add(newScholarshipCommittee);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewDonator(CreateNewDonatorDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newDonator = new Donator
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    OccupationId = model.OccupationId,
                    PhoneNumber = model.PhoneNum,
                    WorkPlace = model.WorkPlace,
                };

                newUser.Donator.Add(newDonator);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewSchoolmaster(CreateNewSchoolmasterDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newSchoolmaster = new Schoolmaster
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNum
                };

                newUser.Schoolmaster.Add(newSchoolmaster);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewHostSchoolTeacher(CreateNewHostSchoolTeacherDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newHostSchoolTeacher = new HostSchoolTeacher
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNum
                };

                newUser.HostSchoolTeacher.Add(newHostSchoolTeacher);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewStudent(CreateNewStudentDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newStudent = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNum
                };

                newUser.Student.Add(newStudent);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewVolunteer(CreateNewVolunteerDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newVolunteer = new Volunteer
                {
                    BirthDate = model.BirthDate,
                    Class = model.Class,
                    DepartmentId = model.DepartmentId,
                    FirstName = model.FirstName,
                    IsStudent = model.IsStudent,
                    LastName = model.LastName,
                    OccupationId = model.OccupationId,
                    PhoneNumber = model.PhoneNum,
                    UniversityId = model.UniversityId
                };

                var newInterest1 = new InterestVolunteer
                {
                    ActivityId = model.Interest1Id
                };

                var newInterest2 = new InterestVolunteer
                {
                    ActivityId = model.Interest2Id
                };

                var newInterest3 = new InterestVolunteer
                {
                    ActivityId = model.Interest3Id
                };

                newUser.Volunteer.Add(newVolunteer);
                newVolunteer.InterestVolunteer.Add(newInterest1);
                newVolunteer.InterestVolunteer.Add(newInterest2);
                newVolunteer.InterestVolunteer.Add(newInterest3);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewYonder(CreateNewYonderDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var anyExistingUser = _userRepository.Entities.Any(p => p.Username == model.Username && p.Email == model.Email && p.IsActive);

                if (anyExistingUser)
                    throw new Exception("Kullanıcı bilgisi mevcut.");

                var newUser = new Contracts.Entities.EF.User
                {
                    IsActive = true,
                    Password = model.Password,
                    Username = model.Username,
                    UserTypeId = model.UserTypeId,
                    Email = model.Email
                };

                var newYonder = new YonDer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNum
                };

                newUser.YonDer.Add(newYonder);

                var userResult = _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                result = userResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<VolunteerBadgeDTO> GetVolunteerBadge(long volunteerId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            VolunteerBadgeDTO result = null;
            try
            {
                result = new VolunteerBadgeDTO
                {
                    ActivityLeadershipBadgeList = new List<ActivityLeadershipBadgeDTO>(),
                    GeniusBadgeList = new List<GeniusBadgeDTO>()
                };

                var currentVolunteer = _volunteerRepository.GetById(volunteerId);

                if (currentVolunteer == null)
                    throw new Exception("Gönüllü bilgisi bulunamadı.");

                var currentVolunteerProjectDetailIdList = _projectDetailActivityRepository.Entities.Where(p => p.VolunteerId == currentVolunteer.Id && p.StatusId == (int)EnumActivityStatusType.Onaylandi).Select(s => s.ProjectDetailId).Distinct().ToList();
                var filteredProjectDetailActivityList = _projectDetailActivityRepository.Entities.Where(p => currentVolunteerProjectDetailIdList.Contains(p.ProjectDetailId) && p.StatusId == (int)EnumActivityStatusType.Onaylandi).ToList();

                var groupedFilteredProjectDetailActivityList = filteredProjectDetailActivityList.GroupBy(g => new
                {
                    g.ProjectDetailId,
                    g.ActivityId,
                }).ToList();

                foreach (var projectDetailActivity in groupedFilteredProjectDetailActivityList)
                {
                    var projectDetailId = projectDetailActivity.Key.ProjectDetailId;
                    var activityId = projectDetailActivity.Key.ActivityId;
                    var currentActivity = _activityRepository.GetById(activityId);

                    var volunteerVoteList = _volunteerVoteRepository.Entities.Where(p => p.ProjectDetailId == projectDetailId && p.ActivityId == activityId).ToList();

                    if (volunteerVoteList != null)
                    {
                        var maxVoteByVolunteer = volunteerVoteList.Max(m => m.Vote);

                        var volunteerVoteTotalList = volunteerVoteList.Where(p => p.VolunteerId == currentVolunteer.Id).ToList();
                        var volunteerVote = volunteerVoteTotalList.Average(p=> p.Vote);

                        if (volunteerVote != 0)
                        {
                            if (volunteerVote >= maxVoteByVolunteer)
                            {
                                string projectDetailName = $"Proje: {_projectDetailRepository.GetById(projectDetailId).Project.Name} ({_projectDetailRepository.GetById(projectDetailId).StartDate.ToString("dd/MM/yyyy")}-{_projectDetailRepository.GetById(projectDetailId).EndDate.ToString("dd/MM/yyyy")}) Aktivite: {currentActivity.Name}";
                                var randomHexColor = CreateRandomHexColor();

                                result.ActivityLeadershipBadgeList.Add(new ActivityLeadershipBadgeDTO
                                {
                                    ProjectDetailId = projectDetailId,
                                    ProjectDetailName = projectDetailName,
                                    BadgeColor = randomHexColor
                                });
                            }

                            if (volunteerVote >= 4)
                            {
                                string activityName = currentActivity.Name;
                                var randomHexColor = CreateRandomHexColor();

                                result.GeniusBadgeList.Add(new GeniusBadgeDTO
                                {
                                    ActivityId = activityId,
                                    ActivityName = activityName,
                                    BadgeColor = randomHexColor
                                });
                            }
                        }
                    }


                }

                if (result.ActivityLeadershipBadgeList.Count >= 3 && result.ActivityLeadershipBadgeList.Count < 7)
                {
                    result.IsBronzeActivityLeadershipBadge = true;
                }
                else if (result.ActivityLeadershipBadgeList.Count >= 7 && result.ActivityLeadershipBadgeList.Count < 12)
                {
                    result.IsSilverActivityLeadershipBadge = true;
                }
                else if (result.ActivityLeadershipBadgeList.Count >= 12)
                {
                    result.IsGoldActivityLeadershipBadge = true;
                }

                var VolunteerProjectDetailActivityList = _projectDetailActivityRepository.Entities.Where(p => p.StatusId == (int)EnumActivityStatusType.Onaylandi).GroupBy(v => v.VolunteerId).ToList();
                var maxActivityJoined = VolunteerProjectDetailActivityList.Max(m => m.Count());
                var currentvolunteerJoinedActivity = _projectDetailActivityRepository.Entities.Where(p => p.VolunteerId == currentVolunteer.Id && p.StatusId == (int)EnumActivityStatusType.Onaylandi).Count();

                if (maxActivityJoined == currentvolunteerJoinedActivity)
                {
                    result.IsBee = true;
                }
                else if (maxActivityJoined > currentvolunteerJoinedActivity)
                {
                    result.NeededForBeeBadge = maxActivityJoined - currentvolunteerJoinedActivity;
                }


                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<VolunteerBadgeDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<ProjectManagerBadgeDTO> GetProjectManagerBadge(long projectManagerId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            ProjectManagerBadgeDTO result = null;
            try
            {
                result = new ProjectManagerBadgeDTO
                {
                    ProjectExperienceBadgeList = new List<ProjectExperienceBadgeDTO>()
                };

                var currentProjectManager = _projectManagerRepository.GetById(projectManagerId);

                if (currentProjectManager == null)
                    throw new Exception("Proje yöneticisi bilgisi bulunamadı.");

                var projectDetailIdList = _projectDetailRepository.Entities.Where(p => p.StatusId == (int)EnumProjectSubDetailStatusType.Tamamlandi).Select(s => s.Id).Distinct().ToList();
                var filteredProjectDetailVoteList = _projectDetailVoteRepository.Entities.Where(p => projectDetailIdList.Contains(p.ProjectDetailId)).ToList();
                var averageProjectVote = filteredProjectDetailVoteList.Average(p => p.Vote); //tüm ortalama
                var groupedprojectDetailVoteList = filteredProjectDetailVoteList.GroupBy(g => new
                {
                    g.ProjectDetailId
                }).ToList();

                int badgeVote = 3;
                long projectDetailId = -1;
                int badgeCount = 0;

                foreach (var projectDetailVote in groupedprojectDetailVoteList)
                {
                    int totalVoteProjectDetail = projectDetailVote.Sum(s => s.Vote);
                    var averageProjectDetailVote = projectDetailVote.Average(p => p.Vote); // detay bazında ortalama

                    if (averageProjectDetailVote > badgeVote)
                    {
                        projectDetailId = projectDetailVote.Key.ProjectDetailId;

                        var bestProjectDetail = _projectDetailRepository.GetById(projectDetailId);

                        if (bestProjectDetail != null)
                        {


                            if (bestProjectDetail.Project.ProjectManagerId == projectManagerId)
                            {
                                result.IsHighestVoteBadge = true;
                            }

                            var projectManagerProjectDetailExperienceList = _projectDetailRepository.Entities.Where(p => p.Project.ProjectManagerId == projectManagerId && p.StatusId == (int)EnumProjectSubDetailStatusType.Tamamlandi).ToList();

                            foreach (var projectDetail in projectManagerProjectDetailExperienceList)
                            {
                                string projectDetailName = $"Proje: {projectDetail.Project.Name} ({projectDetail.StartDate.ToString("dd/MM/yyyy")}-{projectDetail.EndDate.ToString("dd/MM/yyyy")})";
                                var randomHexColor = CreateRandomHexColor();

                                result.ProjectExperienceBadgeList.Add(new ProjectExperienceBadgeDTO
                                {
                                    ProjectDetailId = projectDetailId,
                                    ProjectDetailName = projectDetailName,
                                    BadgeColor = randomHexColor
                                });
                            }
                        }
                    }
                }
                               

                serviceResultType = EnumServiceResultType.Success;
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<ProjectManagerBadgeDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        static Random _random = new Random();
        static string CreateRandomHexColor()
        {
            return String.Format("#{0:X6}", _random.Next(0x1000000));
        }
    }
}
