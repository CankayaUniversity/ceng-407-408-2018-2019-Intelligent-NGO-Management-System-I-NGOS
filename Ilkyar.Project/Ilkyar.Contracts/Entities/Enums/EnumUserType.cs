using System.ComponentModel;

namespace Ilkyar.Contracts.Entities.Enums
{
    public enum EnumUserType
    {
        [Description("Organizasyon Başkanı")]
        NGOHead = 1,
        [Description("Proje Yöneticisi")]
        ProjectManager = 2,
        [Description("Burs Komitesi")]
        ScholarshipCommittee = 3,
        [Description("Bursiyer")]
        ScholarshipHolder = 4,
        [Description("Bağışçı")]
        Donator = 5,
        [Description("Okul Müdürü")]
        Schoolmaster = 6,
        [Description("Okul Öğretmeni")]
        HostSchoolTeacher = 7,
        [Description("Öğrenci")]
        Student = 8,
        [Description("Gönüllü")]
        Volunteer = 9,
        [Description("Yön-Der")]
        YonDer = 10
    }
}
