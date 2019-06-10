using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilkyar.Web
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int userId = Convert.ToInt32(UserHelper.CurrentUser.UserId);
            GetUser(userId);


            //Init_NGOHead();
            //Init_ProjectManager();
            //Init_ScholarshipCommittee();
            //Init_ScholarshipHolder();
            //Init_Donator();
            //Init_Schoolmaster();
            //Init_HostSchoolTeacher();
            //Init_Student();
            //Init_Volunteer();
            //Init_YonDer();
        }

        //private void Init_YonDer()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.YonDer)
        //    {
        //        YonDer_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        YonDer_LastName.Text = UserHelper.CurrentUser.LastName;
        //        YonDer_TCKN.Text = UserHelper.CurrentUser.Username;
        //        YonDer_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        YonDer_Email.Text = UserHelper.CurrentUser.Email;
        //        YonDer_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        YonDer_DutyStartDate.Text = UserHelper.CurrentUser.DutyStartDate.ToString("dd.MM.yyyy");
        //        YonDer_DutyEndDateIsActive.Text = UserHelper.CurrentUser.DutyEndDate.HasValue ? UserHelper.CurrentUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

        //        if (UserHelper.CurrentUser.DutyEndDate == null)
        //        {
        //            YonDer_DutyEndDate.Text = "Halen görevine devam ediyor.";
        //        }
        //    }
        //}

        //private void Init_Volunteer()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.Volunteer)
        //    {
        //        Volunteer_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        Volunteer_LastName.Text = UserHelper.CurrentUser.LastName;
        //        Volunteer_TCKN.Text = UserHelper.CurrentUser.Username;
        //        Volunteer_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        Volunteer_Email.Text = UserHelper.CurrentUser.Email;
        //        Volunteer_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        GetVolunteerInterestList(UserHelper.CurrentUser.Id);
        //        Volunteer_Interest1.Text = Interest1Id.Value;
        //        Volunteer_Interest2.Text = Interest2Id.Value;
        //        Volunteer_Interest3.Text = Interest3Id.Value;

        //        if (UserHelper.CurrentUser.IsStudent == true)
        //        {
        //            Volunteer_IsStudent.Text = "Öğrenci";
        //            Volunteer_University.Text = UserHelper.CurrentUser.University;
        //            Volunteer_Department.Text = UserHelper.CurrentUser.Department;
        //            Volunteer_Class.Text = UserHelper.CurrentUser.Class;
        //        }
        //        else
        //        {
        //            Volunteer_IsStudent.Text = "Çalışan";
        //            Volunteer_Occupation.Text = UserHelper.CurrentUser.Occupation;
        //        }
        //    }
        //}

        //private void Init_Student()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.Student)
        //    {
        //        Student_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        Student_LastName.Text = UserHelper.CurrentUser.LastName;
        //        Student_TCKN.Text = UserHelper.CurrentUser.Username;
        //        Student_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        Student_Email.Text = UserHelper.CurrentUser.Email;
        //        Student_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        Student_City.Text =
        //        Student_Town.Text =
        //        Student_School.Text = UserHelper.CurrentUser.School;
        //        Student_EducationLevel.Text = UserHelper.CurrentUser.EducationLevel;
        //        Student_Class.Text = UserHelper.CurrentUser.Class.ToString();
        //        Student_CumGPA.Text = UserHelper.CurrentUser.CumGPA.ToString();
        //    }
        //}

        //private void Init_HostSchoolTeacher()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
        //    {
        //        HostSchoolTeacher_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        HostSchoolTeacher_LastName.Text = UserHelper.CurrentUser.LastName;
        //        HostSchoolTeacher_TCKN.Text = UserHelper.CurrentUser.Username;
        //        HostSchoolTeacher_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        HostSchoolTeacher_Email.Text = UserHelper.CurrentUser.Email;
        //        HostSchoolTeacher_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        HostSchoolTeacher_City.Text =
        //        HostSchoolTeacher_Town.Text =
        //        HostSchoolTeacher_School.Text = UserHelper.CurrentUser.School;
        //        HostSchoolTeacher_Branch.Text = UserHelper.CurrentUser.Branch;
        //    }
        //}

        //private void Init_Schoolmaster()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
        //    {
        //        Schoolmaster_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        Schoolmaster_LastName.Text = UserHelper.CurrentUser.LastName;
        //        Schoolmaster_TCKN.Text = UserHelper.CurrentUser.Username;
        //        Schoolmaster_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        Schoolmaster_Email.Text = UserHelper.CurrentUser.Email;
        //        Schoolmaster_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        Schoolmaster_School.Text = UserHelper.CurrentUser.School;
        //        var cityId = UserHelper.CurrentUser.
        //        GetCity(cityId);
        //        GetTown(UserHelper.CurrentUser.CityId, UserHelper.CurrentUser.TownId);
        //    }
        //}

        //private void Init_Donator()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.Donator)
        //    {
        //        Donator_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        Donator_LastName.Text = UserHelper.CurrentUser.LastName;
        //        Donator_TCKN.Text = UserHelper.CurrentUser.Username;
        //        Donator_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        Donator_Email.Text = UserHelper.CurrentUser.Email;
        //        Donator_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        Donator_Occupation.Text = UserHelper.CurrentUser.Occupation.ToString();
        //        Donator_WorkPlaceFalse.Text = UserHelper.CurrentUser.WorkPlace.ToString();

        //        if (UserHelper.CurrentUser.WorkPlace == null)
        //        {
        //            Donator_WorkPlaceTrue.Text = "Çalışmıyor.";
        //        }
        //    }
        //}

        //private void Init_ScholarshipHolder()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipHolder)
        //    {
        //        ScholarshipHolder_YonDer.Text = UserHelper.CurrentUser.YonDerName;
        //        ScholarshipHolder_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        ScholarshipHolder_LastName.Text = UserHelper.CurrentUser.LastName;
        //        ScholarshipHolder_TCKN.Text = UserHelper.CurrentUser.Username;
        //        ScholarshipHolder_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        ScholarshipHolder_Email.Text = UserHelper.CurrentUser.Email;
        //        ScholarshipHolder_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        ScholarshipHolder_ScholarshipStartDate.Text = UserHelper.CurrentUser.ScholarshipStartDate.ToString("dd.MM.yyyy");
        //        ScholarshipHolder_ScholarshipEndDateTrue.Text = UserHelper.CurrentUser.ScholarshipEndDate.HasValue ? UserHelper.CurrentUser.ScholarshipEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

        //        if (UserHelper.CurrentUser.ScholarshipEndDate == null)
        //        {
        //            ScholarshipHolder_ScholarshipEndDateFalse.Text = "Halen burs almaya devam ediyor.";
        //        }
        //        ScholarshipHolder_ScholarshipAmount.Text = UserHelper.CurrentUser.ScholarshipAmount.ToString();
        //        ScholarshipHolder_IBANNo.Text = UserHelper.CurrentUser.IbanNo.ToString();
        //        ScholarshipHolder_School.Text = UserHelper.CurrentUser.School;
        //        ScholarshipHolder_EducationLevel.Text = UserHelper.CurrentUser.EducationLevel;
        //        ScholarshipHolder_Class.Text = UserHelper.CurrentUser.Class.ToString();
        //        ScholarshipHolder_cumGPA.Text = UserHelper.CurrentUser.CumGPA.ToString();

        //        ScholarshipHolder_MotherName.Text = UserHelper.CurrentUser.MotherName;
        //        ScholarshipHolder_MotherOccupationId.Text = UserHelper.CurrentUser.MotherOccupation.ToString();
        //        ScholarshipHolder_FatherName.Text = UserHelper.CurrentUser.FatherName;
        //        ScholarshipHolder_FatherOccupationId.Text = UserHelper.CurrentUser.FatherOccupation.ToString();

        //        ScholarshipHolder_NumberOfSiblings.Text = UserHelper.CurrentUser.NumberOfSiblings.ToString();
        //        ScholarshipHolder_SiblingFirstName.Text =
        //        ScholarshipHolder_SiblingLastName.Text = UserHelper.CurrentUser.
        //        ScholarshipHolder_SiblingMonthlyIncome.Text = UserHelper.CurrentUser.
        //        ScholarshipHolder_SiblingOccupation.Text = UserHelper.CurrentUser.

        //        ScholarshipHolder_MontlyIncome.Text = UserHelper.CurrentUser.MonthlyIncome.ToString();
        //        ScholarshipHolder_HealthCondition.Text = UserHelper.CurrentUser.HealthConditionInfo;

        //    }
        //}

        //private void Init_ScholarshipCommittee()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
        //    {
        //        ScholarshipCommittee_Title.Text = UserHelper.CurrentUser.Title;
        //        ScholarshipCommittee_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        ScholarshipCommittee_LastName.Text = UserHelper.CurrentUser.LastName;
        //        ScholarshipCommittee_TCKN.Text = UserHelper.CurrentUser.Username;
        //        ScholarshipCommittee_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        ScholarshipCommittee_Email.Text = UserHelper.CurrentUser.Email;
        //        ScholarshipCommittee_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        ScholarshipCommittee_DutyStartDate.Text = UserHelper.CurrentUser.DutyStartDate.ToString("dd.MM.yyyy");
        //        ScholarshipCommittee_DutyEndDateIsActive.Text = UserHelper.CurrentUser.DutyEndDate.HasValue ? UserHelper.CurrentUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

        //        if (UserHelper.CurrentUser.DutyEndDate == null)
        //        {
        //            ScholarshipCommittee_DutyEndDate.Text = "Halen görevine devam ediyor.";
        //        }
        //    }
        //}

        //private void Init_ProjectManager()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.ProjectManager)
        //    {
        //        ProjectManager_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        ProjectManager_LastName.Text = UserHelper.CurrentUser.LastName;
        //        ProjectManager_TCKN.Text = UserHelper.CurrentUser.Username;
        //        ProjectManager_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        ProjectManager_Email.Text = UserHelper.CurrentUser.Email;
        //        ProjectManager_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        ProjectManager_DutyStartDate.Text = UserHelper.CurrentUser.DutyStartDate.ToString("dd.MM.yyyy");
        //        ProjectManager_DutyEndDateIsActive.Text = UserHelper.CurrentUser.DutyEndDate.HasValue ? UserHelper.CurrentUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;
        //        GetVolunteerInterestList(UserHelper.CurrentUser.UserId); Project Managera ilgi alanları eklendiğinde açılacak
        //        ProjectManager_Interest1.Text = Interest1Id.Value;
        //        ProjectManager_Interest2.Text = Interest2Id.Value;
        //        ProjectManager_Interest3.Text = Interest3Id.Value;

        //        if (UserHelper.CurrentUser.DutyEndDate == null)
        //        {
        //            ProjectManager_DutyEndDate.Text = "Halen görevine devam ediyor.";
        //        }
        //    }
        //}

        //private void Init_NGOHead()
        //{
        //    if (UserHelper.UserTypeId == (int)EnumUserType.NGOHead)
        //    {
        //        NGOHead_FirstName.Text = UserHelper.CurrentUser.FirstName;
        //        NGOHead_LastName.Text = UserHelper.CurrentUser.LastName;
        //        NGOHead_TCKN.Text = UserHelper.CurrentUser.Username;
        //        NGOHead_BirthDate.Text = UserHelper.CurrentUser.BirthDate.ToString("dd.MM.yyyy");
        //        NGOHead_Email.Text = UserHelper.CurrentUser.Email;
        //        NGOHead_Phone.Text = UserHelper.CurrentUser.PhoneNum;
        //        NGOHead_DutyStartDate.Text = UserHelper.CurrentUser.DutyStartDate.ToString("dd.MM.yyyy");
        //        NGOHead_DutyEndDateIsActive.Text = UserHelper.CurrentUser.DutyEndDate.HasValue ? UserHelper.CurrentUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

        //        if (UserHelper.CurrentUser.DutyEndDate == null)
        //        {
        //            NGOHead_DutyEndDate.Text = "Halen görevine devam ediyor.";
        //        }

        //    }
        //}

        private void GetUser(int userId)
        {
            try
            {
                ServiceResult<UserDTO> serviceResult = new ServiceResult<UserDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("userId", userId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.UserApiUrl, "GetUser", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<UserDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var user = serviceResult.Result;

                //FirstLastName.InnerText = $"{user.FirstName} {user.LastName}";
                //UserType.InnerText = EnumHelper.GetEnumDescription(typeof(EnumUserType), user.UserTypeId.ToString());

                Session["ViewUser_UserId"] = user.UserId;
                Session["ViewUser_UserTypeId"] = user.UserTypeId;

                Init_NGOHead(user);
                Init_ProjectManager(user);
                Init_ScholarshipCommittee(user);
                Init_ScholarshipHolder(user);
                Init_Donator(user);
                Init_Schoolmaster(user);
                Init_HostSchoolTeacher(user);
                Init_Student(user);
                Init_Volunteer(user);
                Init_YonDer(user);
            }
            catch (Exception ex)
            {
                Response.Redirect("ListUsers.aspx");
            }
        }

        private void Init_NGOHead(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.NGOHead)
            {
                NGOHead_FirstName.Text = selectedUser.FirstName;
                NGOHead_LastName.Text = selectedUser.LastName;
                NGOHead_TCKN.Text = selectedUser.Username;
                NGOHead_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                NGOHead_Email.Text = selectedUser.Email;
                NGOHead_Phone.Text = selectedUser.PhoneNum;
                NGOHead_DutyStartDate.Text = selectedUser.DutyStartDate.ToString("dd.MM.yyyy");
                NGOHead_DutyEndDateIsActive.Text = selectedUser.DutyEndDate.HasValue ? selectedUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

                if (selectedUser.DutyEndDate == null)
                {
                    NGOHead_DutyEndDate.Text = "Halen görevine devam ediyor.";
                }

                Session["ViewUser_DutyEndDate"] = selectedUser.DutyEndDate;
            }
        }

        private void Init_ProjectManager(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.ProjectManager)
            {
                ProjectManager_FirstName.Text = selectedUser.FirstName;
                ProjectManager_LastName.Text = selectedUser.LastName;
                ProjectManager_TCKN.Text = selectedUser.Username;
                ProjectManager_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                ProjectManager_Email.Text = selectedUser.Email;
                ProjectManager_Phone.Text = selectedUser.PhoneNum;
                ProjectManager_DutyStartDate.Text = selectedUser.DutyStartDate.ToString("dd.MM.yyyy");
                ProjectManager_DutyEndDateIsActive.Text = selectedUser.DutyEndDate.HasValue ? selectedUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

                if (selectedUser.DutyEndDate == null)
                {
                    ProjectManager_DutyEndDate.Text = "Halen görevine devam ediyor.";
                }

                Session["ViewUser_DutyEndDate"] = selectedUser.DutyEndDate;
            }
        }

        private void Init_ScholarshipCommittee(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
            {
                ScholarshipCommittee_Title.Text = selectedUser.Title;
                ScholarshipCommittee_FirstName.Text = selectedUser.FirstName;
                ScholarshipCommittee_LastName.Text = selectedUser.LastName;
                ScholarshipCommittee_TCKN.Text = selectedUser.Username;
                ScholarshipCommittee_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                ScholarshipCommittee_Email.Text = selectedUser.Email;
                ScholarshipCommittee_Phone.Text = selectedUser.PhoneNum;
                ScholarshipCommittee_DutyStartDate.Text = selectedUser.DutyStartDate.ToString("dd.MM.yyyy");
                ScholarshipCommittee_DutyEndDateIsActive.Text = selectedUser.DutyEndDate.HasValue ? selectedUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

                if (selectedUser.DutyEndDate == null)
                {
                    ScholarshipCommittee_DutyEndDate.Text = "Halen görevine devam ediyor.";
                }
                Session["ViewUser_DutyEndDate"] = selectedUser.DutyEndDate;
            }
        }

        private void Init_ScholarshipHolder(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.ScholarshipHolder)
            {
                ScholarshipHolder_YonDer.Text = selectedUser.YonDerName;
                ScholarshipHolder_FirstName.Text = selectedUser.FirstName;
                ScholarshipHolder_LastName.Text = selectedUser.LastName;
                ScholarshipHolder_TCKN.Text = selectedUser.Username;
                ScholarshipHolder_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                ScholarshipHolder_Email.Text = selectedUser.Email;
                ScholarshipHolder_Phone.Text = selectedUser.PhoneNum;
                ScholarshipHolder_ScholarshipStartDate.Text = selectedUser.ScholarshipStartDate.ToString("dd.MM.yyyy");
                ScholarshipHolder_ScholarshipEndDateTrue.Text = selectedUser.ScholarshipEndDate.HasValue ? selectedUser.ScholarshipEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

                if (selectedUser.ScholarshipEndDate == null)
                {
                    ScholarshipHolder_ScholarshipEndDateFalse.Text = "Halen burs almaya devam ediyor.";
                }
                ScholarshipHolder_ScholarshipAmount.Text = selectedUser.ScholarshipAmount.ToString();
                ScholarshipHolder_IBANNo.Text = selectedUser.IbanNo.ToString();
                ScholarshipHolder_School.Text = selectedUser.School;
                ScholarshipHolder_EducationLevel.Text = selectedUser.EducationLevel;
                ScholarshipHolder_Class.Text = selectedUser.Class.ToString();
                ScholarshipHolder_cumGPA.Text = selectedUser.CumGPA.ToString();

                ScholarshipHolder_MotherName.Text = selectedUser.MotherName;
                ScholarshipHolder_MotherOccupationId.Text = selectedUser.MotherOccupation.ToString();
                ScholarshipHolder_FatherName.Text = selectedUser.FatherName;
                ScholarshipHolder_FatherOccupationId.Text = selectedUser.FatherOccupation.ToString();

                ScholarshipHolder_NumberOfSiblings.Text = selectedUser.NumberOfSiblings.ToString();
                //ScholarshipHolder_SiblingFirstName.Text = 
                //ScholarshipHolder_SiblingLastName.Text = selectedUser.
                //ScholarshipHolder_SiblingMonthlyIncome.Text = selectedUser.
                //ScholarshipHolder_SiblingOccupation.Text = selectedUser.

                ScholarshipHolder_MontlyIncome.Text = selectedUser.MonthlyIncome.ToString();
                ScholarshipHolder_HealthCondition.Text = selectedUser.HealthConditionInfo;
                Session["ViewUser_ScholarshipEndDate"] = selectedUser.ScholarshipEndDate;
                Session["ViewUser_NumberOfSiblings"] = selectedUser.NumberOfSiblings;
            }
        }

        private void Init_Donator(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.Donator)
            {
                Donator_FirstName.Text = selectedUser.FirstName;
                Donator_LastName.Text = selectedUser.LastName;
                Donator_TCKN.Text = selectedUser.Username;
                Donator_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                Donator_Email.Text = selectedUser.Email;
                Donator_Phone.Text = selectedUser.PhoneNum;
                Donator_Occupation.Text = selectedUser.Occupation.ToString();
                Donator_WorkPlaceFalse.Text = selectedUser.WorkPlace.ToString();

                if (selectedUser.WorkPlace == null)
                {
                    Donator_WorkPlaceTrue.Text = "Çalışmıyor.";
                }

                Session["ViewUser_WorkPlace"] = selectedUser.WorkPlace;
            }
        }

        private void Init_Schoolmaster(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.Schoolmaster)
            {
                Schoolmaster_FirstName.Text = selectedUser.FirstName;
                Schoolmaster_LastName.Text = selectedUser.LastName;
                Schoolmaster_TCKN.Text = selectedUser.Username;
                Schoolmaster_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                Schoolmaster_Email.Text = selectedUser.Email;
                Schoolmaster_Phone.Text = selectedUser.PhoneNum;
                var cityId = selectedUser.CityId;
                GetCity(cityId);
                var townId = selectedUser.TownId;
                GetTown(cityId,townId);

                Schoolmaster_School.Text = selectedUser.School;
            }
        }

        private void Init_HostSchoolTeacher(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
            {
                HostSchoolTeacher_FirstName.Text = selectedUser.FirstName;
                HostSchoolTeacher_LastName.Text = selectedUser.LastName;
                HostSchoolTeacher_TCKN.Text = selectedUser.Username;
                HostSchoolTeacher_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                HostSchoolTeacher_Email.Text = selectedUser.Email;
                HostSchoolTeacher_Phone.Text = selectedUser.PhoneNum;
                //HostSchoolTeacher_City.Text =
                //HostSchoolTeacher_Town.Text =
                HostSchoolTeacher_School.Text = selectedUser.School;
                HostSchoolTeacher_Branch.Text = selectedUser.Branch;
            }
        }

        private void Init_Student(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.Student)
            {
                Student_FirstName.Text = selectedUser.FirstName;
                Student_LastName.Text = selectedUser.LastName;
                Student_TCKN.Text = selectedUser.Username;
                Student_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                Student_Email.Text = selectedUser.Email;
                Student_Phone.Text = selectedUser.PhoneNum;
                //Student_City.Text =
                //Student_Town.Text =
                Student_School.Text = selectedUser.School;
                Student_EducationLevel.Text = selectedUser.EducationLevel;
                Student_Class.Text = selectedUser.Class.ToString();
                Student_CumGPA.Text = selectedUser.CumGPA.ToString();
            }
        }

        private void Init_Volunteer(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.Volunteer)
            {
                Volunteer_FirstName.Text = selectedUser.FirstName;
                Volunteer_LastName.Text = selectedUser.LastName;
                Volunteer_TCKN.Text = selectedUser.Username;
                Volunteer_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                Volunteer_Email.Text = selectedUser.Email;
                Volunteer_Phone.Text = selectedUser.PhoneNum;
                GetVolunteerInterestList(UserHelper.CurrentUser.Id);
                Volunteer_Interest1.Text = Interest1Id.Value;
                Volunteer_Interest2.Text = Interest2Id.Value;
                Volunteer_Interest3.Text = Interest3Id.Value;

                if (selectedUser.IsStudent == true)
                {
                    Volunteer_IsStudent.Text = "Öğrenci";
                    Volunteer_University.Text = selectedUser.University;
                    Volunteer_Department.Text = selectedUser.Department;
                    Volunteer_Class.Text = selectedUser.Class;
                }
                else
                {
                    Volunteer_IsStudent.Text = "Çalışan";
                    Volunteer_Occupation.Text = selectedUser.Occupation;
                }

                Session["ViewUser_isStudent"] = selectedUser.IsStudent;

            }
        }

        private void Init_YonDer(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.YonDer)
            {
                YonDer_FirstName.Text = selectedUser.FirstName;
                YonDer_LastName.Text = selectedUser.LastName;
                YonDer_TCKN.Text = selectedUser.Username;
                YonDer_BirthDate.Text = selectedUser.BirthDate.ToString("dd.MM.yyyy");
                YonDer_Email.Text = selectedUser.Email;
                YonDer_Phone.Text = selectedUser.PhoneNum;
                YonDer_DutyStartDate.Text = selectedUser.DutyStartDate.ToString("dd.MM.yyyy");
                YonDer_DutyEndDateIsActive.Text = selectedUser.DutyEndDate.HasValue ? selectedUser.DutyEndDate.Value.ToString("dd.MM.yyyy") : string.Empty;

                if (selectedUser.DutyEndDate == null)
                {
                    YonDer_DutyEndDate.Text = "Halen görevine devam ediyor.";
                }

                Session["ViewUser_DutyEndDate"] = selectedUser.DutyEndDate;
            }
        }
    

        private void GetCity(int? cityId)
        {
            try
            {
                ServiceResult<CityDTO> serviceResult = new ServiceResult<CityDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("cityId", cityId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetCity", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<CityDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var city = serviceResult.Result;

                if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
                    Schoolmaster_City.Text = city.Name;
                if (UserHelper.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                    HostSchoolTeacher_City.Text = city.Name;
                if (UserHelper.UserTypeId == (int)EnumUserType.Student)
                    Student_City.Text = city.Name;

                    

            }
            catch (Exception ex)
            {
                Response.Redirect("Home.aspx");
            }
        }

        private void GetTown(int? cityId, int? townId)
        {
            try
            {
                ServiceResult<TownDTO> serviceResult = new ServiceResult<TownDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("cityId", cityId.ToString());
                queryString.Add("townId", townId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetTown", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<TownDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var town = serviceResult.Result;

                if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
                    Schoolmaster_Town.Text = town.Name;
                if (UserHelper.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                    HostSchoolTeacher_Town.Text = town.Name;
                if (UserHelper.UserTypeId == (int)EnumUserType.Student)
                    Student_Town.Text = town.Name;

            }
            catch (Exception ex)
            {
                Response.Redirect("Home.aspx");
            }
        }

        private void GetVolunteerInterestList(long volunteerId)
        {
            try
            {
                ServiceResult<List<InterestVolunteerDTO>> serviceResult = new ServiceResult<List<InterestVolunteerDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("volunteerId", volunteerId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetVolunteerInterestList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<InterestVolunteerDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var temp = 3;

                        foreach (var item in serviceResult.Result)
                        {
                            if (temp == 3)
                            {
                                Interest3Id.Value = item.InterestName;
                                temp--;
                            }
                            else if (temp == 2)
                            {
                                Interest2Id.Value = item.InterestName;
                                temp--;
                            }
                            else
                            {
                                Interest1Id.Value = item.InterestName;
                    }
                                  
                           }

            }
            catch (Exception ex)
            {
            }
        }


    }
}