using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.ServiceContracts.Message
{
    public interface IMessage
    {
        ServiceResult<List<ConversationDTO>> GetConversationList(long currentUserId, long userId);
        ServiceResult<long> CreateNewConversation(AddConversationDTO model);
        ServiceResult<bool> DeleteConversation(DeleteConversationDTO model);
    }
}
