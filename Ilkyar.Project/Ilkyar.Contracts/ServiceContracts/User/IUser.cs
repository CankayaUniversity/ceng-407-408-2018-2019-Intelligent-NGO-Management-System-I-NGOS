using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Services;
using System.Collections.Generic;

namespace Ilkyar.Contracts.ServiceContracts.User
{
    public interface IUser
    {
        ServiceResult<List<UserDTO>> GetUserList(UserFilterDTO filter);
        ServiceResult<List<UserDTO>> GetUserList();
        ServiceResult<UserDTO> GetUser(long userId);
    }
}
