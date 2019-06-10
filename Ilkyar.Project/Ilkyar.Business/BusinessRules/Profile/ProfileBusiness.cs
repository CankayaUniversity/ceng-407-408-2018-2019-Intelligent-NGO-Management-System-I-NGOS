using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.EF;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.ServiceContracts.Profile;
using Ilkyar.Contracts.Services;
using Ilkyar.Contracts.UnitOfWork;
using System;
using System.Linq;

namespace Ilkyar.Business.BusinessRules.Profile
{
    public class ProfileBusiness : IProfile
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Contracts.Entities.EF.User> _userRepository;
        private readonly IRepository<NgoHead> _ngoHeadRepository;
        private readonly IRepository<Occupation> _occupationRepository;

        public ProfileBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.User>();
            _ngoHeadRepository = _unitOfWork.GetRepository<NgoHead>();
            _occupationRepository = _unitOfWork.GetRepository<Occupation>();
        }

        public ServiceResult<UserDTO> UpdateProfile(UpdateUserDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            UserDTO result = null;
            try
            {
                var existingUser = _userRepository.Entities.Where(p => p.Id == model.UserId && p.IsActive).SingleOrDefault();

                if (existingUser == null)
                    throw new Exception("Kullanıcı bulunamadı.");

                if (existingUser.UserTypeId == (int)EnumUserType.NGOHead)
                {
                    if (!existingUser.NgoHead.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingNgoHead = existingUser.NgoHead.Single();

                    existingNgoHead.FirstName = model.FirstName;
                    existingNgoHead.LastName = model.LastName;
                    existingNgoHead.BirthDate = model.BirthDate;
                    existingNgoHead.PhoneNumber = model.PhoneNum;
                    existingNgoHead.DutyStartDate = model.DutyStartDate;
                    existingNgoHead.DutyEndDate = model.DutyEndDate;
                }
                if (existingUser.UserTypeId == (int)EnumUserType.ProjectManager)
                {
                    if (!existingUser.ProjectManager.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingProjectManager = existingUser.ProjectManager.Single();

                    existingProjectManager.FirstName = model.FirstName;
                    existingProjectManager.LastName = model.LastName;
                    existingProjectManager.BirthDate = model.BirthDate;
                    existingProjectManager.PhoneNumber = model.PhoneNum;
                    existingProjectManager.DutyStartDate = model.DutyStartDate;
                    existingProjectManager.DutyEndDate = model.DutyEndDate;
                }
                if (existingUser.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                {
                    if (!existingUser.ScholarshipCommittee.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingScholarshipCommittee = existingUser.ScholarshipCommittee.Single();

                    existingScholarshipCommittee.Title = model.Title;
                    existingScholarshipCommittee.FirstName = model.FirstName;
                    existingScholarshipCommittee.LastName = model.LastName;
                    existingScholarshipCommittee.BirthDate = model.BirthDate;
                    existingScholarshipCommittee.PhoneNumber = model.PhoneNum;
                    existingScholarshipCommittee.DutyStartDate = model.DutyStartDate;
                    existingScholarshipCommittee.DutyEndDate = model.DutyEndDate;
                }
                if (existingUser.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                {
                    if (!existingUser.ScholarshipHolder.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingScholarshipHolder = existingUser.ScholarshipHolder.Single();

                    existingScholarshipHolder.FirstName = model.FirstName;
                    existingScholarshipHolder.LastName = model.LastName;
                    existingScholarshipHolder.BirthDate = model.BirthDate;
                    existingScholarshipHolder.PhoneNumber = model.PhoneNum;
                    ///TODO:EKleme yapılacak
                }
                if (existingUser.UserTypeId == (int)EnumUserType.Donator)
                {
                    if (!existingUser.Donator.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingDonator = existingUser.Donator.Single();

                    existingDonator.FirstName = model.FirstName;
                    existingDonator.LastName = model.LastName;
                    existingDonator.BirthDate = model.BirthDate;
                    existingDonator.PhoneNumber = model.PhoneNum;
                    //existingDonator.Occupation = model.Occupation;
                    existingDonator.WorkPlace = model.WorkPlace;
                }
                if (existingUser.UserTypeId == (int)EnumUserType.Schoolmaster)
                {
                    if (!existingUser.Schoolmaster.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingSchoolmaster = existingUser.Schoolmaster.Single();

                    existingSchoolmaster.FirstName = model.FirstName;
                    existingSchoolmaster.LastName = model.LastName;
                    existingSchoolmaster.BirthDate = model.BirthDate;
                    existingSchoolmaster.PhoneNumber = model.PhoneNum;
                    existingSchoolmaster.School = model.School;
                    existingSchoolmaster.CityId = model.CityId;
                    existingSchoolmaster.TownId = model.TownId;
                }
                if (existingUser.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                {
                    if (!existingUser.HostSchoolTeacher.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingHostSchoolTeacher = existingUser.HostSchoolTeacher.Single();

                    existingHostSchoolTeacher.FirstName = model.FirstName;
                    existingHostSchoolTeacher.LastName = model.LastName;
                    existingHostSchoolTeacher.BirthDate = model.BirthDate;
                    existingHostSchoolTeacher.PhoneNumber = model.PhoneNum;
                    // existingHostSchoolTeacher.School = model.School;
                    // existingHostSchoolTeacher.Branch = model.Branch;

                }
                if (existingUser.UserTypeId == (int)EnumUserType.Student)
                {
                    if (!existingUser.Student.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingStudent = existingUser.Student.Single();

                    existingStudent.FirstName = model.FirstName;
                    existingStudent.LastName = model.LastName;
                    existingStudent.BirthDate = model.BirthDate;
                    existingStudent.PhoneNumber = model.PhoneNum;
                    //existingStudent.School = model.School;
                    existingStudent.EducationLevel = model.EducationLevel;
                    existingStudent.Class = model.Class;
                }
                if (existingUser.UserTypeId == (int)EnumUserType.Volunteer)
                {
                    if (!existingUser.Volunteer.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingVolunteer = existingUser.Volunteer.Single();

                    existingVolunteer.FirstName = model.FirstName;
                    existingVolunteer.LastName = model.LastName;
                    existingVolunteer.BirthDate = model.BirthDate;
                    existingVolunteer.PhoneNumber = model.PhoneNum;

                    existingVolunteer.IsStudent = model.Volunteer_IsStudent;

                    existingVolunteer.UniversityId = null;
                    existingVolunteer.DepartmentId = null;
                    existingVolunteer.Class = null;
                    existingVolunteer.OccupationId = null;

                    if (model.Volunteer_IsStudent == true)
                    {
                        existingVolunteer.UniversityId = model.Volunteer_UniversityId;
                        existingVolunteer.DepartmentId = model.Volunteer_DepartmentId;
                        existingVolunteer.Class = model.Volunteer_Class;
                    }
                    else
                    {
                        existingVolunteer.OccupationId = model.Volunteer_OccupationId;
                    }
                }
                if (existingUser.UserTypeId == (int)EnumUserType.YonDer)
                {
                    if (!existingUser.YonDer.Any())
                        throw new Exception("Kullanıcı bulunamadı.");

                    var existingYonDer = existingUser.YonDer.Single();

                    existingYonDer.FirstName = model.FirstName;
                    existingYonDer.LastName = model.LastName;
                    existingYonDer.BirthDate = model.BirthDate;
                    existingYonDer.PhoneNumber = model.PhoneNum;
                    existingYonDer.DutyStartDate = model.DutyStartDate;
                    existingYonDer.DutyEndDate = model.DutyEndDate;
                }

                _unitOfWork.SaveChanges();

                result = GetUser(model.UserId);
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<UserDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<UserDTO> UpdatePassword(UpdateUserDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            UserDTO result = null;
            try
            {
                var existingUser = _userRepository.Entities.Where(p => p.Username == model.Username).SingleOrDefault();

                if (existingUser == null)
                    throw new Exception("Kullanıcı bulunamadı.");

                existingUser.Password = model.Password;
            

                _unitOfWork.SaveChanges();

                result = GetUser(existingUser.Id);
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<UserDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        private UserDTO GetUser(long userId)
        {
            UserDTO result = null;
            try
            {
                var user = _userRepository.GetById(userId);

                if (user == null)
                {
                    throw new Exception("Kullanıcı bilgisi bulunamadı.");
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
                        result.CityId = Convert.ToInt32(currentSchoolmaster.CityId);
                        result.TownId = Convert.ToInt32(currentSchoolmaster.TownId);
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
                        result.CityId = Convert.ToInt32(currentHostSchoolTeacher.CityId);
                        result.TownId = Convert.ToInt32(currentHostSchoolTeacher.TownId);
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
                        result.CityId = Convert.ToInt32(currentStudent.CityId);
                        result.TownId = Convert.ToInt32(currentStudent.TownId);
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
                        if (currentVolunteer.University != null)
                        {
                            result.University = currentVolunteer.University.Name;
                        }
                        if (currentVolunteer.Department != null)
                        {
                            result.Department = currentVolunteer.Department.Name;
                        }
                        result.Class = currentVolunteer.Class;
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
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
