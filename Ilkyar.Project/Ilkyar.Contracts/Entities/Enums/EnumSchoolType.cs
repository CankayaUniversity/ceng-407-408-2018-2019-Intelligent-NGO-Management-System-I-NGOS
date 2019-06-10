using System.ComponentModel;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumSchoolType
    {
        [Description("Anaokul")]
        Duzenlenen = 1,
        [Description("İlköğretim Okulu")]
        IlkogretimOkulu = 2,
        [Description("Ortaöğretim Okulu")]
        OrtaogretimOkulu = 3,
        [Description("Lise")]
        Lise = 4
    }
}
