using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumTransportationType
    {
        [Description("Belirtilmedi")]
        None = 0,
        [Description("Otobüs")]
        Bus = 1,
        [Description("Uçak")]
        Airplane = 2,
        [Description("Tren")]
        Train = 3,
        [Description("Bireysel Ulaşım")]
        Individual = 4,
        [Description("Diğer")]
        Other = 5
    }
}
