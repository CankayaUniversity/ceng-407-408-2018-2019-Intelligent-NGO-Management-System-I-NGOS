using Ilkyar.Contracts.Entities.Enums;

namespace Ilkyar.Contracts.Services
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
        public EnumServiceResultType ServiceResultType { get; set; }
    }
}
