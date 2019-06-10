using Ilkyar.Business.System.Extentions;
using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.ServiceContracts.User;
using Ilkyar.Contracts.Services;
using Ilkyar.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ilkyar.Business.BusinessRules.User
{
    public class UserBusiness : IUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Contracts.Entities.EF.User> _userRepository;

        public UserBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.User>();
        }

        public ServiceResult<List<UserDTO>> GetUserList(UserFilterDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<UserDTO> result = null;
            try
            {
                Expression<Func<Contracts.Entities.EF.User, bool>> expUser = p => true;

                #region availableUserTypeIdList

                List<int> availableUserTypeIdList = new List<int>();

                if (model.CurrentUserTypeId == (int)EnumUserType.NGOHead)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                    availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                    availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                    availableUserTypeIdList.Add((int)EnumUserType.Donator);
                    availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                    availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                    availableUserTypeIdList.Add((int)EnumUserType.Student);
                    availableUserTypeIdList.Add((int)EnumUserType.Volunteer);
                    availableUserTypeIdList.Add((int)EnumUserType.YonDer);

                }

                if (model.CurrentUserTypeId == (int)EnumUserType.ProjectManager)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                    availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                    availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                    availableUserTypeIdList.Add((int)EnumUserType.Student);
                    availableUserTypeIdList.Add((int)EnumUserType.Volunteer);
                }

                if (model.CurrentUserTypeId == (int)EnumUserType.ScholarshipCommittee)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                    availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                    availableUserTypeIdList.Add((int)EnumUserType.Donator);
                    availableUserTypeIdList.Add((int)EnumUserType.YonDer);
                }

                if (model.CurrentUserTypeId == (int)EnumUserType.ScholarshipHolder)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                    availableUserTypeIdList.Add((int)EnumUserType.YonDer);
                }

                if (model.CurrentUserTypeId == (int)EnumUserType.Donator)
                {

                }

                if (model.CurrentUserTypeId == (int)EnumUserType.Schoolmaster)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                    availableUserTypeIdList.Add((int)EnumUserType.Student);
                }

                if (model.CurrentUserTypeId == (int)EnumUserType.HostSchoolTeacher)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                    availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                    availableUserTypeIdList.Add((int)EnumUserType.Student);
                }

                if (model.CurrentUserTypeId == (int)EnumUserType.Student)
                {

                }

                if (model.CurrentUserTypeId == (int)EnumUserType.Volunteer)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                }

                if (model.CurrentUserTypeId == (int)EnumUserType.YonDer)
                {
                    availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                    availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                    availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                    availableUserTypeIdList.Add((int)EnumUserType.Donator);
                }

                expUser = expUser.And(p => availableUserTypeIdList.Contains(p.UserTypeId));

                #endregion

                if (model.UserTypeId != null)
                {
                    expUser = expUser.And(p => p.UserTypeId == model.UserTypeId.Value);
                }

                if (!string.IsNullOrEmpty(model.UserName))
                {
                    expUser = expUser.And(p => p.Username.Contains(model.UserName));
                }

                if (model.Status != null)
                {
                    if (model.Status == (int)EnumUserStatus.Aktif)
                    {
                        expUser = expUser.And(p => p.IsActive == true);
                    }
                    else if (model.Status == (int)EnumUserStatus.Pasif)
                    {
                        expUser = expUser.And(p => p.IsActive == false);
                    }
                }

                if (!string.IsNullOrEmpty(model.Email))
                {
                    expUser = expUser.And(p => p.Email.Contains(model.Email));
                }

                if (!string.IsNullOrEmpty(model.FirstName))
                {
                    expUser = expUser.And(p =>
                                            p.NgoHead.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.ProjectManager.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.ScholarshipCommittee.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.ScholarshipHolder.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.Donator.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.Schoolmaster.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.HostSchoolTeacher.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.Student.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.Volunteer.Any(p2 => p2.FirstName.Contains(model.FirstName)) ||
                                            p.YonDer.Any(p2 => p2.FirstName.Contains(model.FirstName))
                                            );
                }

                if (!string.IsNullOrEmpty(model.LastName))
                {
                    expUser = expUser.And(p =>
                                            p.NgoHead.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.ProjectManager.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.ScholarshipCommittee.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.ScholarshipHolder.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.Donator.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.Schoolmaster.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.HostSchoolTeacher.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.Student.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.Volunteer.Any(p2 => p2.LastName.Contains(model.LastName)) ||
                                            p.YonDer.Any(p2 => p2.LastName.Contains(model.LastName))
                                            );
                }

                if (!string.IsNullOrEmpty(model.Phone))
                {
                    expUser = expUser.And(p =>
                                            p.NgoHead.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.ProjectManager.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.ScholarshipCommittee.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.ScholarshipHolder.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.Donator.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.Schoolmaster.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.HostSchoolTeacher.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.Student.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.Volunteer.Any(p2 => p2.PhoneNumber.Contains(model.Phone)) ||
                                            p.YonDer.Any(p2 => p2.PhoneNumber.Contains(model.Phone))
                                            );
                }

                if (model.BirthDate != null)
                {
                    expUser = expUser.And(p =>
                                            p.NgoHead.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.ProjectManager.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.ScholarshipCommittee.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.ScholarshipHolder.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.Donator.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.Schoolmaster.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.HostSchoolTeacher.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.Student.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.Volunteer.Any(p2 => p2.BirthDate == model.BirthDate) ||
                                            p.YonDer.Any(p2 => p2.BirthDate == model.BirthDate)
                                            );
                }

                var userList = _userRepository.Entities.Where(expUser).ToList();

                result = new List<UserDTO>();

                foreach (var user in userList)
                {
                    var newUser = new UserDTO();

                    newUser.UserId = user.Id;
                    newUser.UserTypeId = user.UserTypeId;
                    newUser.UserType = EnumHelper.GetEnumDescription(typeof(EnumUserType), user.UserTypeId.ToString());
                    newUser.Username = user.Username;
                    newUser.Email = user.Email;
                    newUser.IsActive = user.IsActive;

                    if (user.IsActive)
                        newUser.UserStatus = EnumHelper.GetEnumDescription(typeof(EnumUserStatus), ((int)EnumUserStatus.Aktif).ToString());
                    else
                        newUser.UserStatus = EnumHelper.GetEnumDescription(typeof(EnumUserStatus), ((int)EnumUserStatus.Pasif).ToString());

                    if (user.UserTypeId == (int)EnumUserType.NGOHead)
                    {
                        if (user.NgoHead.Any())
                        {
                            newUser.Id = user.NgoHead.Single().Id;
                            newUser.FirstName = user.NgoHead.Single().FirstName;
                            newUser.LastName = user.NgoHead.Single().LastName;
                            newUser.BirthDate = user.NgoHead.Single().BirthDate;
                            newUser.PhoneNum = user.NgoHead.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.ProjectManager)
                    {
                        if (user.ProjectManager.Any())
                        {
                            newUser.Id = user.ProjectManager.Single().Id;
                            newUser.FirstName = user.ProjectManager.Single().FirstName;
                            newUser.LastName = user.ProjectManager.Single().LastName;
                            newUser.BirthDate = user.ProjectManager.Single().BirthDate;
                            newUser.PhoneNum = user.ProjectManager.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                    {
                        if (user.ScholarshipCommittee.Any())
                        {
                            newUser.Id = user.ScholarshipCommittee.Single().Id;
                            newUser.FirstName = user.ScholarshipCommittee.Single().FirstName;
                            newUser.LastName = user.ScholarshipCommittee.Single().LastName;
                            newUser.BirthDate = user.ScholarshipCommittee.Single().BirthDate;
                            newUser.PhoneNum = user.ScholarshipCommittee.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                    {
                        if (user.ScholarshipHolder.Any())
                        {
                            newUser.Id = user.ScholarshipHolder.Single().Id;
                            newUser.FirstName = user.ScholarshipHolder.Single().FirstName;
                            newUser.LastName = user.ScholarshipHolder.Single().LastName;
                            newUser.BirthDate = user.ScholarshipHolder.Single().BirthDate;
                            newUser.PhoneNum = user.ScholarshipHolder.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Donator)
                    {
                        if (user.Donator.Any())
                        {
                            newUser.Id = user.Donator.Single().Id;
                            newUser.FirstName = user.Donator.Single().FirstName;
                            newUser.LastName = user.Donator.Single().LastName;
                            newUser.BirthDate = user.Donator.Single().BirthDate;
                            newUser.PhoneNum = user.Donator.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Schoolmaster)
                    {
                        if (user.Schoolmaster.Any())
                        {
                            newUser.Id = user.Schoolmaster.Single().Id;
                            newUser.FirstName = user.Schoolmaster.Single().FirstName;
                            newUser.LastName = user.Schoolmaster.Single().LastName;
                            newUser.BirthDate = user.Schoolmaster.Single().BirthDate;
                            newUser.PhoneNum = user.Schoolmaster.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                    {
                        if (user.HostSchoolTeacher.Any())
                        {
                            newUser.Id = user.HostSchoolTeacher.Single().Id;
                            newUser.FirstName = user.HostSchoolTeacher.Single().FirstName;
                            newUser.LastName = user.HostSchoolTeacher.Single().LastName;
                            newUser.BirthDate = user.HostSchoolTeacher.Single().BirthDate;
                            newUser.PhoneNum = user.HostSchoolTeacher.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Student)
                    {
                        if (user.Student.Any())
                        {
                            newUser.Id = user.Student.Single().Id;
                            newUser.FirstName = user.Student.Single().FirstName;
                            newUser.LastName = user.Student.Single().LastName;
                            newUser.BirthDate = user.Student.Single().BirthDate;
                            newUser.PhoneNum = user.Student.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Volunteer)
                    {
                        if (user.Volunteer.Any())
                        {
                            newUser.Id = user.Volunteer.Single().Id;
                            newUser.FirstName = user.Volunteer.Single().FirstName;
                            newUser.LastName = user.Volunteer.Single().LastName;
                            newUser.BirthDate = user.Volunteer.Single().BirthDate;
                            newUser.PhoneNum = user.Volunteer.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.YonDer)
                    {
                        if (user.YonDer.Any())
                        {
                            newUser.Id = user.YonDer.Single().Id;
                            newUser.FirstName = user.YonDer.Single().FirstName;
                            newUser.LastName = user.YonDer.Single().LastName;
                            newUser.BirthDate = user.YonDer.Single().BirthDate;
                            newUser.PhoneNum = user.YonDer.Single().PhoneNumber;
                        }
                    }

                    result.Add(newUser);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<UserDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<UserDTO>> GetUserList()
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<UserDTO> result = null;
            try
            {
                var userList = _userRepository.Entities.Where(p => p.IsActive == true).ToList();

                result = new List<UserDTO>();

                foreach (var user in userList)
                {
                    var newUser = new UserDTO();

                    newUser.UserId = user.Id;
                    newUser.UserTypeId = user.UserTypeId;
                    newUser.UserType = EnumHelper.GetEnumDescription(typeof(EnumUserType), user.UserTypeId.ToString());
                    newUser.Username = user.Username;
                    newUser.Email = user.Email;
                    newUser.IsActive = user.IsActive;

                    if (user.UserTypeId == (int)EnumUserType.NGOHead)
                    {
                        if (user.NgoHead.Any())
                        {
                            newUser.Id = user.NgoHead.Single().Id;
                            newUser.FirstName = user.NgoHead.Single().FirstName;
                            newUser.LastName = user.NgoHead.Single().LastName;
                            newUser.BirthDate = user.NgoHead.Single().BirthDate;
                            newUser.PhoneNum = user.NgoHead.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.ProjectManager)
                    {
                        if (user.ProjectManager.Any())
                        {
                            newUser.Id = user.ProjectManager.Single().Id;
                            newUser.FirstName = user.ProjectManager.Single().FirstName;
                            newUser.LastName = user.ProjectManager.Single().LastName;
                            newUser.BirthDate = user.ProjectManager.Single().BirthDate;
                            newUser.PhoneNum = user.ProjectManager.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                    {
                        if (user.ScholarshipCommittee.Any())
                        {
                            newUser.Id = user.ScholarshipCommittee.Single().Id;
                            newUser.FirstName = user.ScholarshipCommittee.Single().FirstName;
                            newUser.LastName = user.ScholarshipCommittee.Single().LastName;
                            newUser.BirthDate = user.ScholarshipCommittee.Single().BirthDate;
                            newUser.PhoneNum = user.ScholarshipCommittee.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                    {
                        if (user.ScholarshipHolder.Any())
                        {
                            newUser.Id = user.ScholarshipHolder.Single().Id;
                            newUser.FirstName = user.ScholarshipHolder.Single().FirstName;
                            newUser.LastName = user.ScholarshipHolder.Single().LastName;
                            newUser.BirthDate = user.ScholarshipHolder.Single().BirthDate;
                            newUser.PhoneNum = user.ScholarshipHolder.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Donator)
                    {
                        if (user.Donator.Any())
                        {
                            newUser.Id = user.Donator.Single().Id;
                            newUser.FirstName = user.Donator.Single().FirstName;
                            newUser.LastName = user.Donator.Single().LastName;
                            newUser.BirthDate = user.Donator.Single().BirthDate;
                            newUser.PhoneNum = user.Donator.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Schoolmaster)
                    {
                        if (user.Schoolmaster.Any())
                        {
                            newUser.Id = user.Schoolmaster.Single().Id;
                            newUser.FirstName = user.Schoolmaster.Single().FirstName;
                            newUser.LastName = user.Schoolmaster.Single().LastName;
                            newUser.BirthDate = user.Schoolmaster.Single().BirthDate;
                            newUser.PhoneNum = user.Schoolmaster.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                    {
                        if (user.HostSchoolTeacher.Any())
                        {
                            newUser.Id = user.HostSchoolTeacher.Single().Id;
                            newUser.FirstName = user.HostSchoolTeacher.Single().FirstName;
                            newUser.LastName = user.HostSchoolTeacher.Single().LastName;
                            newUser.BirthDate = user.HostSchoolTeacher.Single().BirthDate;
                            newUser.PhoneNum = user.HostSchoolTeacher.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Student)
                    {
                        if (user.Student.Any())
                        {
                            newUser.Id = user.Student.Single().Id;
                            newUser.FirstName = user.Student.Single().FirstName;
                            newUser.LastName = user.Student.Single().LastName;
                            newUser.BirthDate = user.Student.Single().BirthDate;
                            newUser.PhoneNum = user.Student.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.Volunteer)
                    {
                        if (user.Volunteer.Any())
                        {
                            newUser.Id = user.Volunteer.Single().Id;
                            newUser.FirstName = user.Volunteer.Single().FirstName;
                            newUser.LastName = user.Volunteer.Single().LastName;
                            newUser.BirthDate = user.Volunteer.Single().BirthDate;
                            newUser.PhoneNum = user.Volunteer.Single().PhoneNumber;
                        }
                    }

                    if (user.UserTypeId == (int)EnumUserType.YonDer)
                    {
                        if (user.YonDer.Any())
                        {
                            newUser.Id = user.YonDer.Single().Id;
                            newUser.FirstName = user.YonDer.Single().FirstName;
                            newUser.LastName = user.YonDer.Single().LastName;
                            newUser.BirthDate = user.YonDer.Single().BirthDate;
                            newUser.PhoneNum = user.YonDer.Single().PhoneNumber;
                        }
                    }

                    result.Add(newUser);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<UserDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<UserDTO> GetUser(long userId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            UserDTO result = null;
            try
            {
                var existingUser = _userRepository.Entities.Where(p => p.Id == userId).SingleOrDefault();

                if (existingUser == null)
                    throw new Exception("Kullanıcı bulunamadı.");

                result = new UserDTO
                {
                    UserId = existingUser.Id,
                    UserTypeId = existingUser.UserTypeId,
                    Username = existingUser.Username,
                    Email = existingUser.Email,
                    IsActive = existingUser.IsActive
                };

                //---NGO Head Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.NGOHead)
                {
                    if (existingUser.NgoHead.Any())
                    {
                        var selectedNgoHead = existingUser.NgoHead.Single();

                        result.Id = selectedNgoHead.Id;
                        result.FirstName = selectedNgoHead.FirstName;
                        result.LastName = selectedNgoHead.LastName;
                        result.BirthDate = selectedNgoHead.BirthDate;
                        result.PhoneNum = selectedNgoHead.PhoneNumber;
                        result.DutyStartDate = selectedNgoHead.DutyStartDate;
                        result.DutyEndDate = selectedNgoHead.DutyEndDate;
                    }
                }

                //---ProjectManager Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.ProjectManager)
                {
                    if (existingUser.ProjectManager.Any())
                    {
                        var selectedProjectManager = existingUser.ProjectManager.Single();

                        result.Id = selectedProjectManager.Id;
                        result.FirstName = selectedProjectManager.FirstName;
                        result.LastName = selectedProjectManager.LastName;
                        result.BirthDate = selectedProjectManager.BirthDate;
                        result.PhoneNum = selectedProjectManager.PhoneNumber;
                        result.DutyStartDate = selectedProjectManager.DutyStartDate;
                        result.DutyEndDate = selectedProjectManager.DutyEndDate;
                    }
                }

                //---ScholarshipCommittee Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                {
                    if (existingUser.ScholarshipCommittee.Any())
                    {
                        var selectedScholarshipCommittee = existingUser.ScholarshipCommittee.Single();

                        result.Id = selectedScholarshipCommittee.Id;
                        result.Title = selectedScholarshipCommittee.Title;
                        result.FirstName = selectedScholarshipCommittee.FirstName;
                        result.LastName = selectedScholarshipCommittee.LastName;
                        result.BirthDate = selectedScholarshipCommittee.BirthDate;
                        result.PhoneNum = selectedScholarshipCommittee.PhoneNumber;
                        result.DutyStartDate = selectedScholarshipCommittee.DutyStartDate;
                        result.DutyEndDate = selectedScholarshipCommittee.DutyEndDate;
                    }
                }

                //---ScholarshipHolder Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                {
                    if (existingUser.ScholarshipHolder.Any())
                    {
                        var selectedScholarshipHolder = existingUser.ScholarshipHolder.Single();

                        result.Id = selectedScholarshipHolder.Id;
                        result.YonDerName = selectedScholarshipHolder.YonDerId.ToString();
                        result.FirstName = selectedScholarshipHolder.FirstName;
                        result.LastName = selectedScholarshipHolder.LastName;
                        result.BirthDate = selectedScholarshipHolder.BirthDate;
                        result.PhoneNum = selectedScholarshipHolder.PhoneNumber;
                        result.ScholarshipStartDate = selectedScholarshipHolder.ScholarshipStartDate;
                        result.ScholarshipEndDate = selectedScholarshipHolder.ScholarshipEndDate;
                        result.ScholarshipAmount = selectedScholarshipHolder.ScholarshipAmount;
                        result.IbanNo = selectedScholarshipHolder.IbanNo;
                        result.School = selectedScholarshipHolder.School.ToString();
                        result.EducationLevel = selectedScholarshipHolder.EducationLevel;
                        result.Class = selectedScholarshipHolder.Class.ToString();
                        result.CumGPA = selectedScholarshipHolder.CumGPA;
                        result.MotherName = selectedScholarshipHolder.MotherName;
                        result.MotherOccupation = selectedScholarshipHolder.MotherOccupationId.ToString();
                        result.FatherName = selectedScholarshipHolder.FatherName;
                        result.FatherOccupation = selectedScholarshipHolder.FatherOccupationId.ToString();
                        result.NumberOfSiblings = selectedScholarshipHolder.NumberOfSiblings;
                        //result.SiblingFirstName = 
                        //result.SiblingLastName = selectedScholarshipHolder.
                        //result.SiblingMonthlyIncome = selectedScholarshipHolder.
                        //result.SiblingOccupation = selectedScholarshipHolder.
                        result.MonthlyIncome = selectedScholarshipHolder.MonthlyIncome;
                        result.HealthConditionInfo = selectedScholarshipHolder.HealthConditionInfo;
                    }
                }

                //---Donator Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.Donator)
                {
                    if (existingUser.Donator.Any())
                    {
                        var selectedDonator = existingUser.Donator.Single();

                        result.Id = selectedDonator.Id;
                        result.FirstName = selectedDonator.FirstName;
                        result.LastName = selectedDonator.LastName;
                        result.BirthDate = selectedDonator.BirthDate;
                        result.PhoneNum = selectedDonator.PhoneNumber;
                        result.Occupation = selectedDonator.OccupationId.ToString();
                        result.WorkPlace = selectedDonator.WorkPlace;
                    }
                }

                //---Schoolmaster Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.Schoolmaster)
                {
                    if (existingUser.Schoolmaster.Any())
                    {
                        var selectedSchoolmaster = existingUser.Schoolmaster.Single();

                        result.Id = selectedSchoolmaster.Id;
                        result.FirstName = selectedSchoolmaster.FirstName;
                        result.LastName = selectedSchoolmaster.LastName;
                        result.BirthDate = selectedSchoolmaster.BirthDate;
                        result.PhoneNum = selectedSchoolmaster.PhoneNumber;
                        result.CityId = selectedSchoolmaster.CityId;
                        result.TownId = selectedSchoolmaster.TownId;
                        result.School = selectedSchoolmaster.School;
                    }

                }

                //---HostSchoolTeacher Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                {
                    if (existingUser.HostSchoolTeacher.Any())
                    {
                        var selectedHostSchoolTeacher = existingUser.HostSchoolTeacher.Single();

                        result.Id = selectedHostSchoolTeacher.Id;
                        result.FirstName = selectedHostSchoolTeacher.FirstName;
                        result.LastName = selectedHostSchoolTeacher.LastName;
                        result.BirthDate = selectedHostSchoolTeacher.BirthDate;
                        result.PhoneNum = selectedHostSchoolTeacher.PhoneNumber;
                        //result.City =
                        //result.Town =
                        result.School = selectedHostSchoolTeacher.School;
                        result.Branch = selectedHostSchoolTeacher.BranchId.ToString();
                    }
                }

                //---Student Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.Student)
                {
                    if (existingUser.Student.Any())
                    {
                        var selectedStudent = existingUser.Student.Single();

                        result.Id = selectedStudent.Id;
                        result.FirstName = selectedStudent.FirstName;
                        result.LastName = selectedStudent.LastName;
                        result.BirthDate = selectedStudent.BirthDate;
                        result.PhoneNum = selectedStudent.PhoneNumber;
                        //result =
                        //result =
                        result.School = selectedStudent.SchoolId.ToString();
                        result.EducationLevel = selectedStudent.EducationLevel;
                        result.Class = selectedStudent.Class;
                        result.CumGPA = selectedStudent.CumGPA;
                    }
                }

                //---Volunteer Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.Volunteer)
                {
                    if (existingUser.Volunteer.Any())
                    {
                        var selectedVolunteer = existingUser.Volunteer.Single();

                        result.Id = selectedVolunteer.Id;
                        result.FirstName = selectedVolunteer.FirstName;
                        result.LastName = selectedVolunteer.LastName;
                        result.BirthDate = selectedVolunteer.BirthDate;
                        result.PhoneNum = selectedVolunteer.PhoneNumber;
                        result.IsStudent = selectedVolunteer.IsStudent;
                        result.University = selectedVolunteer.UniversityId.ToString();
                        result.Department = selectedVolunteer.DepartmentId.ToString();
                        result.Class = selectedVolunteer.Class;
                        result.Occupation = selectedVolunteer.OccupationId.ToString();
                    }
                }

                //---YonDer Info---//
                if (existingUser.UserTypeId == (int)EnumUserType.YonDer)
                {
                    if (existingUser.YonDer.Any())
                    {
                        var selectedYonDer = existingUser.YonDer.Single();

                        result.Id = selectedYonDer.Id;
                        result.FirstName = selectedYonDer.FirstName;
                        result.LastName = selectedYonDer.LastName;
                        result.BirthDate = selectedYonDer.BirthDate;
                        result.PhoneNum = selectedYonDer.PhoneNumber;
                        result.DutyStartDate = selectedYonDer.DutyStartDate;
                        result.DutyEndDate = selectedYonDer.DutyEndDate;
                    }
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<UserDTO>
            {
                ErrorMessage = errorMessage,
                Result = result,
                ServiceResultType = serviceResultType
            };
        }
    }
}

