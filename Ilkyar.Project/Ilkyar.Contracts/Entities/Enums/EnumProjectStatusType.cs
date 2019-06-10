using System.ComponentModel;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumProjectStatusType
    {
        [Description("Aktif")]
        Aktif = 1,
        [Description("İptal edildi")]
        IptalEdildi = 2,
        [Description("Tamamlandı")]
        Tamamlandi = 3
    }
}
