using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.EF;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.ServiceContracts.Message;
using Ilkyar.Contracts.Services;
using Ilkyar.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Business.BusinessRules.Message
{
    public class MessageBusiness : IMessage
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Contracts.Entities.EF.User> _userRepository;
        private readonly IRepository<Contracts.Entities.EF.Conversation> _conversationRepository;

        public MessageBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.User>();
            _conversationRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.Conversation>();
        }

        public ServiceResult<List<ConversationDTO>> GetConversationList(long currentUserId, long userId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ConversationDTO> result = null;
            try
            {
                var selectedUser = _userRepository.GetById(userId);

                if (selectedUser == null)
                    throw new Exception("Kullanıcı bulunamadı.");

                var conversationList = _conversationRepository.Entities.Where(p => (p.SenderId == currentUserId && p.ReceiverId == userId) || (p.SenderId == userId && p.ReceiverId == currentUserId)).ToList().OrderBy(o => o.Date).ToList();

                result = new List<ConversationDTO>();

                foreach (var conversation in conversationList)
                {
                    var newConversation = new ConversationDTO();

                    #region selectedUserFullName

                    string selectedUserFullName = string.Empty;

                    if (selectedUser.UserTypeId == (int)EnumUserType.NGOHead)
                    {
                        if (selectedUser.NgoHead.Any())
                        {
                            selectedUserFullName = $"{selectedUser.NgoHead.Single().FirstName} {selectedUser.NgoHead.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.ProjectManager)
                    {
                        if (selectedUser.ProjectManager.Any())
                        {
                            selectedUserFullName = $"{selectedUser.ProjectManager.Single().FirstName} {selectedUser.ProjectManager.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                    {
                        if (selectedUser.ScholarshipCommittee.Any())
                        {
                            selectedUserFullName = $"{selectedUser.ScholarshipCommittee.Single().FirstName} {selectedUser.ScholarshipCommittee.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                    {
                        if (selectedUser.ScholarshipHolder.Any())
                        {
                            selectedUserFullName = $"{selectedUser.ScholarshipHolder.Single().FirstName} {selectedUser.ScholarshipHolder.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.Donator)
                    {
                        if (selectedUser.Donator.Any())
                        {
                            selectedUserFullName = $"{selectedUser.Donator.Single().FirstName} {selectedUser.Donator.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.Schoolmaster)
                    {
                        if (selectedUser.Schoolmaster.Any())
                        {
                            selectedUserFullName = $"{selectedUser.Schoolmaster.Single().FirstName} {selectedUser.Schoolmaster.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                    {
                        if (selectedUser.HostSchoolTeacher.Any())
                        {
                            selectedUserFullName = $"{selectedUser.HostSchoolTeacher.Single().FirstName} {selectedUser.HostSchoolTeacher.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.Student)
                    {
                        if (selectedUser.Student.Any())
                        {
                            selectedUserFullName = $"{selectedUser.Student.Single().FirstName} {selectedUser.Student.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.Volunteer)
                    {
                        if (selectedUser.Volunteer.Any())
                        {
                            selectedUserFullName = $"{selectedUser.Volunteer.Single().FirstName} {selectedUser.Volunteer.Single().LastName}";
                        }
                    }

                    if (selectedUser.UserTypeId == (int)EnumUserType.YonDer)
                    {
                        if (selectedUser.YonDer.Any())
                        {
                            selectedUserFullName = $"{selectedUser.YonDer.Single().FirstName} {selectedUser.YonDer.Single().LastName}";
                        }
                    }

                    #endregion

                    if (conversation.SenderId == currentUserId)
                    {
                        newConversation.MessageTypeId = (int)EnumMessageType.Gonderilen;
                    }
                    else
                    {
                        newConversation.MessageTypeId = (int)EnumMessageType.Gelen;
                    }

                    newConversation.Id = conversation.Id;
                    newConversation.SenderId = conversation.SenderId;
                    newConversation.ReceiverId = conversation.ReceiverId;
                    newConversation.UserFullName = selectedUserFullName;

                    newConversation.Message = conversation.Message;
                    newConversation.Date = conversation.Date;

                    result.Add(newConversation);
                }

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<ConversationDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<long> CreateNewConversation(AddConversationDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var newConversation = new Conversation
                {
                    SenderId = model.SenderId,
                    ReceiverId = model.ReceiverId,
                    Message = model.Message,
                    Date = DateTime.Now
                };

                var conversationResult = _conversationRepository.Add(newConversation);
                _unitOfWork.SaveChanges();

                result = conversationResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<bool> DeleteConversation(DeleteConversationDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            bool result = false;
            try
            {
                var existingConversation = _conversationRepository.GetById(model.ConversationId);

                if (existingConversation == null)
                    throw new Exception("Sohbet bilgisi bulunamadı.");

                _conversationRepository.Delete(existingConversation);
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
    }
}
