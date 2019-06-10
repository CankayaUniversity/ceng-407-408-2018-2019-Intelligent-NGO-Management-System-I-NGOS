using System.ComponentModel;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumMessageType
    {
        [Description("Gelen")]
        Gelen = 1,
        [Description("Gönderilen")]
        Gonderilen = 2
    }
}
