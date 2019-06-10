using System.ComponentModel;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumActivityStatusType
    {
        [Description("Beklemede")]
        Beklemede = 0,
        [Description("Onaylandı")]
        Onaylandi = 1,
        [Description("Reddedildi")]
        Reddedildi = -1
    }
}
