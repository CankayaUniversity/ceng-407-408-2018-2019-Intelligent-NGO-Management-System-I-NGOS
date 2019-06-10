using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumInvitationStatusType
    {
        [Description("Beklemede")]
        Beklemede = 0,
        [Description("Onaylandı")]
        Onaylandi = 1,
        [Description("Reddedildi")]
        Reddedildi = -1
    }
}
