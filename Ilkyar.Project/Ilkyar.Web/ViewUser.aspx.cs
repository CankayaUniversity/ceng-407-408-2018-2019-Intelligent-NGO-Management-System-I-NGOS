using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilkyar.Web
{
    public partial class ViewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ViewUser_UserId"] = null;
            Session["ViewUser_UserTypeId"] = null;
            Session["ViewUser_DutyEndDate"] = null;
            Session["ViewUser_ScholarshipEndDate"] = null;
            Session["ViewUser_NumberOfSiblings"] = null;
            Session["ViewUser_WorkPlace"] = null;
            Session["ViewUser_isStudent"] = null;

            int userId = -1;

            if (!string.IsNullOrEmpty(Request.QueryString["userId"]))
            {
                userId = Convert.ToInt32(Request.QueryString["userId"]);
            }
            else
                Response.Redirect("ListUsers.aspx");

            GetUser(userId);
        }

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

                FirstLastName.InnerText = $"{user.FirstName} {user.LastName}";
                UserType.InnerText = EnumHelper.GetEnumDescription(typeof(EnumUserType), user.UserTypeId.ToString());

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
                //Schoolmaster_City.Text =
                //Schoolmaster_Town.Text =
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
    }
}