using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Services;

namespace Ilkyar.Contracts.ServiceContracts.Profile
{
    public interface IProfile
    {
        ServiceResult<UserDTO> UpdateProfile(UpdateUserDTO model);
        ServiceResult<UserDTO> UpdatePassword(UpdateUserDTO model);
    }
}
