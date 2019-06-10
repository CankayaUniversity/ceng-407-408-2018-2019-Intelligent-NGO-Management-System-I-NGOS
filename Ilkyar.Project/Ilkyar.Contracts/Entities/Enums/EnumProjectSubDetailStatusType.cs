using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumProjectSubDetailStatusType
    {
        [Description("Aktif")]
        Aktif = 1,
        [Description("İptal edildi")]
        IptalEdildi = 2,
        [Description("Tamamlandı")]
        Tamamlandi = 3
    }
}
