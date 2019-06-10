using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class EditProfile : System.Web.UI.Page
    {
        private readonly static string webApiBaseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];

        protected void Page_Load(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                Init_CityDropDownList();
                Init_UniversityDropDownList();
                Init_OccupationDropDownList();

                if (UserHelper.UserTypeId == (int)EnumUserType.NGOHead)
                {
                    Init_NGOHead();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.ProjectManager)
                {
                    Init_ProjectManager();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                {
                    Init_ScholarshipCommittee();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                {
                    Init_ScholarshipHolder();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Donator)
                {
                    Init_Donator();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
                {
                    Init_Schoolmaster();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                {
                    Init_HostSchoolTeacher();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Student)
                {
                    Init_Student();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Volunteer)
                {
                    Init_Volunteer();
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.YonDer)
                {
                    Init_YonDer();
                }
            }
        }

        private void Init_YonDer()
        {
            YonDer_FirstName.Text = UserHelper.CurrentUser.FirstName;
            YonDer_LastName.Text = UserHelper.CurrentUser.LastName;
            YonDer_TCKN.Text = UserHelper.CurrentUser.Username;
            YonDer_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            YonDer_Email.Text = UserHelper.CurrentUser.Email;
            YonDer_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            YonDer_DutyStartDate.SelectedDate = UserHelper.CurrentUser.DutyStartDate;
            YonDer_DutyEndDate.MinDate = new DateTime(1900, 1, 1);
            YonDer_DutyEndDate.MaxDate = DateTime.Today;
            YonDer_DutyEndDate.SelectedDate = UserHelper.CurrentUser.DutyEndDate;
        }

        private void Init_Volunteer()
        {
            Volunteer_FirstName.Text = UserHelper.CurrentUser.FirstName;
            Volunteer_LastName.Text = UserHelper.CurrentUser.LastName;
            Volunteer_TCKN.Text = UserHelper.CurrentUser.Username;
            Volunteer_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            Volunteer_Email.Text = UserHelper.CurrentUser.Email;
            Volunteer_Phone.Text = UserHelper.CurrentUser.PhoneNum;

            divStudent.Visible = false;
            divNotStudent.Visible = false;

            if (UserHelper.CurrentUser.IsStudent)
            {
                checkIsStudent.SelectedValue = "1";
                divStudent.Visible = true;

                Volunteer_University.SelectedValue = UserHelper.CurrentUser.UniversityId?.ToString();
                Init_DepartmentDropDownList(Volunteer_University.SelectedValue);
                Volunteer_Department.SelectedValue = UserHelper.CurrentUser.DepartmentId?.ToString();
                Volunteer_Class.Text = UserHelper.CurrentUser.Class;
            }
            else
            {
                checkIsStudent.SelectedValue = "2";
                divNotStudent.Visible = true;

                Volunteer_Occupation.SelectedValue = UserHelper.CurrentUser.OccupationId?.ToString();
            }
        }

        private void Init_Student()
        {
            Student_FirstName.Text = UserHelper.CurrentUser.FirstName;
            Student_LastName.Text = UserHelper.CurrentUser.LastName;
            Student_TCKN.Text = UserHelper.CurrentUser.Username;
            Student_BirthDate.MinDate = new DateTime(1900, 1, 1);
            Student_BirthDate.MaxDate = DateTime.Today;
            Student_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            Student_Email.Text = UserHelper.CurrentUser.Email;
            Student_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            Student_School.Text = UserHelper.CurrentUser.School;
            Student_EducationLevel.Text = UserHelper.CurrentUser.EducationLevel;
            Student_Class.Text = UserHelper.CurrentUser.Class.ToString();
            Student_CumGPA.Text = UserHelper.CurrentUser.CumGPA.ToString();
        }

        private void Init_HostSchoolTeacher()
        {
            HostSchoolTeacher_FirstName.Text = UserHelper.CurrentUser.FirstName;
            HostSchoolTeacher_LastName.Text = UserHelper.CurrentUser.LastName;
            HostSchoolTeacher_TCKN.Text = UserHelper.CurrentUser.Username;
            HostSchoolTeacher_BirthDate.MinDate = new DateTime(1900, 1, 1);
            HostSchoolTeacher_BirthDate.MaxDate = DateTime.Today;
            HostSchoolTeacher_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            HostSchoolTeacher_Email.Text = UserHelper.CurrentUser.Email;
            HostSchoolTeacher_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            HostSchoolTeacher_School.Text = UserHelper.CurrentUser.School;
            //branş
        }

        private void Init_Schoolmaster()
        {
            Schoolmaster_FirstName.Text = UserHelper.CurrentUser.FirstName;
            Schoolmaster_LastName.Text = UserHelper.CurrentUser.LastName;
            Schoolmaster_TCKN.Text = UserHelper.CurrentUser.Username;
            Schoolmaster_BirthDate.MinDate = new DateTime(1900, 1, 1);
            Schoolmaster_BirthDate.MaxDate = DateTime.Today;
            Schoolmaster_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            Schoolmaster_Email.Text = UserHelper.CurrentUser.Email;
            Schoolmaster_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            Schoolmaster_School.Text = UserHelper.CurrentUser.School;
            cityDropDownListSM.SelectedValue = UserHelper.CurrentUser.CityId.ToString();
        }

        private void Init_Donator()
        {
            //TODO: Düzenleme yapılacak.
            Donator_FirstName.Text = UserHelper.CurrentUser.FirstName;
            Donator_LastName.Text = UserHelper.CurrentUser.LastName;
            Donator_TCKN.Text = UserHelper.CurrentUser.Username;
            Donator_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            Donator_Email.Text = UserHelper.CurrentUser.Email;
            Donator_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            Donator_Occupation.Text = UserHelper.CurrentUser.Occupation.ToString();
            Donator_WorkPlaceFalse.Text = UserHelper.CurrentUser.WorkPlace.ToString();

            if (UserHelper.CurrentUser.WorkPlace == null)
            {
                Donator_WorkPlaceTrue.Text = "Çalışmıyor.";
            }
        }

        private void Init_ScholarshipHolder()
        {
            ScholarshipHolder_YonDer.Text = UserHelper.CurrentUser.YonDerName;
            ScholarshipHolder_FirstName.Text = UserHelper.CurrentUser.FirstName;
            ScholarshipHolder_LastName.Text = UserHelper.CurrentUser.LastName;
            ScholarshipHolder_TCKN.Text = UserHelper.CurrentUser.Username;
            ScholarshipHolder_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            ScholarshipHolder_Email.Text = UserHelper.CurrentUser.Email;
            ScholarshipHolder_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            ScholarshipHolder_ScholarshipStartDate.SelectedDate = UserHelper.CurrentUser.ScholarshipStartDate;
            ScholarshipHolder_ScholarshipEndDateTrue.SelectedDate = UserHelper.CurrentUser.ScholarshipEndDate;

            if (UserHelper.CurrentUser.ScholarshipEndDate == null)
            {
                ScholarshipHolder_ScholarshipEndDateFalse.Text = "Halen burs almaya devam ediyor.";
            }
            ScholarshipHolder_ScholarshipAmount.Text = UserHelper.CurrentUser.ScholarshipAmount.ToString();
            ScholarshipHolder_IBANNo.Text = UserHelper.CurrentUser.IbanNo.ToString();
            ScholarshipHolder_School.Text = UserHelper.CurrentUser.School;
            ScholarshipHolder_EducationLevel.Text = UserHelper.CurrentUser.EducationLevel;
            ScholarshipHolder_Class.Text = UserHelper.CurrentUser.Class.ToString();
            ScholarshipHolder_cumGPA.Text = UserHelper.CurrentUser.CumGPA.ToString();
            //Radiobutton kontrolleri yapılacak mı?
            ScholarshipHolder_MotherName.Text = UserHelper.CurrentUser.MotherName;
            ScholarshipHolder_MotherOccupationId.Text = UserHelper.CurrentUser.MotherOccupation.ToString();
            ScholarshipHolder_FatherName.Text = UserHelper.CurrentUser.FatherName;
            ScholarshipHolder_FatherOccupationId.Text = UserHelper.CurrentUser.FatherOccupation.ToString();

            ScholarshipHolder_NumberOfSiblings.Text = UserHelper.CurrentUser.NumberOfSiblings.ToString();
            //ScholarshipHolder_SiblingFirstName.Text = 
            //ScholarshipHolder_SiblingLastName.Text = UserHelper.CurrentUser.
            //ScholarshipHolder_SiblingMonthlyIncome.Text = UserHelper.CurrentUser.
            //ScholarshipHolder_SiblingOccupation.Text = UserHelper.CurrentUser.

            ScholarshipHolder_MontlyIncome.Text = UserHelper.CurrentUser.MonthlyIncome.ToString();
            ScholarshipHolder_HealthCondition.Text = UserHelper.CurrentUser.HealthConditionInfo;
        }

        private void Init_ScholarshipCommittee()
        {
            ScholarshipCommittee_Title.Text = UserHelper.CurrentUser.Title;
            ScholarshipCommittee_FirstName.Text = UserHelper.CurrentUser.FirstName;
            ScholarshipCommittee_LastName.Text = UserHelper.CurrentUser.LastName;
            ScholarshipCommittee_TCKN.Text = UserHelper.CurrentUser.Username;
            ScholarshipCommittee_BirthDate.MinDate = new DateTime(1900, 1, 1);
            ScholarshipCommittee_BirthDate.MaxDate = DateTime.Today;
            ScholarshipCommittee_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            ScholarshipCommittee_Email.Text = UserHelper.CurrentUser.Email;
            ScholarshipCommittee_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            ScholarshipCommittee_DutyStartDate.MinDate = new DateTime(1900, 1, 1);
            ScholarshipCommittee_DutyStartDate.MaxDate = DateTime.Today;
            ScholarshipCommittee_DutyStartDate.SelectedDate = UserHelper.CurrentUser.DutyStartDate;
            ScholarshipCommittee_DutyEndDate.MinDate = new DateTime(1900, 1, 1);
            ScholarshipCommittee_DutyEndDate.MaxDate = DateTime.Today;
            ScholarshipCommittee_DutyEndDate.SelectedDate = UserHelper.CurrentUser.DutyEndDate;
        }

        private void Init_ProjectManager()
        {
            ProjectManager_FirstName.Text = UserHelper.CurrentUser.FirstName;
            ProjectManager_LastName.Text = UserHelper.CurrentUser.LastName;
            ProjectManager_TCKN.Text = UserHelper.CurrentUser.Username;
            ProjectManager_BirthDate.MinDate = new DateTime(1900, 1, 1);
            ProjectManager_BirthDate.MaxDate = DateTime.Today;
            ProjectManager_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            ProjectManager_Email.Text = UserHelper.CurrentUser.Email;
            ProjectManager_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            ProjectManager_DutyStartDate.MinDate = new DateTime(1900, 1, 1);
            ProjectManager_DutyStartDate.MaxDate = DateTime.Today;
            ProjectManager_DutyStartDate.SelectedDate = UserHelper.CurrentUser.DutyStartDate;
            ProjectManager_DutyEndDate.MinDate = new DateTime(1900, 1, 1);
            ProjectManager_DutyEndDate.MaxDate = DateTime.Today;
            ProjectManager_DutyEndDate.SelectedDate = UserHelper.CurrentUser.DutyEndDate;
        }

        private void Init_NGOHead()
        {
            NGOHead_FirstName.Text = UserHelper.CurrentUser.FirstName;
            NGOHead_LastName.Text = UserHelper.CurrentUser.LastName;
            NGOHead_TCKN.Text = UserHelper.CurrentUser.Username;
            NGOHead_BirthDate.MinDate = new DateTime(1900, 1, 1);
            NGOHead_BirthDate.MaxDate = DateTime.Today;
            NGOHead_BirthDate.SelectedDate = UserHelper.CurrentUser.BirthDate;
            NGOHead_Email.Text = UserHelper.CurrentUser.Email;
            NGOHead_Phone.Text = UserHelper.CurrentUser.PhoneNum;
            NGOHead_DutyStartDate.MinDate = new DateTime(1900, 1, 1);
            NGOHead_DutyStartDate.MaxDate = DateTime.Today;
            NGOHead_DutyStartDate.SelectedDate = UserHelper.CurrentUser.DutyStartDate;
            NGOHead_DutyEndDate.MinDate = new DateTime(1900, 1, 1);
            NGOHead_DutyEndDate.MaxDate = DateTime.Today;
            NGOHead_DutyEndDate.SelectedDate = UserHelper.CurrentUser.DutyEndDate;
        }

        private void Init_CityDropDownList()
        {
            try
            {
                ServiceResult<List<CityDTO>> serviceResult = new ServiceResult<List<CityDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetCityList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<CityDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
                        {
                            foreach (var item in serviceResult.Result)
                            {
                                cityDropDownListSM.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                            }
                        }

                        if (UserHelper.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                        {
                            foreach (var item in serviceResult.Result)
                            {
                                cityDropDownListHST.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                            }
                        }
                        if (UserHelper.UserTypeId == (int)EnumUserType.Student)
                        {
                            foreach (var item in serviceResult.Result)
                            {
                                cityDropDownListS.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });

                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        private void Init_UniversityDropDownList()
        {
            try
            {
                ServiceResult<List<UniversityDTO>> serviceResult = new ServiceResult<List<UniversityDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetUniversityList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<UniversityDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            if (UserHelper.UserTypeId == (int)EnumUserType.Volunteer)
                            {
                                Volunteer_University.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Init_DepartmentDropDownList(string selectedUniversityId)
        {
            Volunteer_Department.Items.Clear();
            Volunteer_Department.SelectedIndex = -1;

            try
            {
                ServiceResult<List<DepartmentDTO>> serviceResult = new ServiceResult<List<DepartmentDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetDepartmentList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<DepartmentDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        var filteredDepartmentList = serviceResult.Result.Where(p => p.UniversityId == Convert.ToInt32(selectedUniversityId)).ToList();

                        foreach (var item in filteredDepartmentList)
                        {
                            Volunteer_Department.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Init_OccupationDropDownList()
        {
            try
            {
                ServiceResult<List<OccupationDTO>> serviceResult = new ServiceResult<List<OccupationDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetOccupationList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<OccupationDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            Volunteer_Occupation.Items.Add(new DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void cityDropDownListSM_ItemSelected(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            townDropDownListSM.Items.Clear();
            var selectedCityId = cityDropDownListSM.SelectedValue;

            try
            {
                ServiceResult<List<TownDTO>> serviceResult = new ServiceResult<List<TownDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetTownList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<TownDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        var filteredTownList = serviceResult.Result.Where(p => p.CityId == Convert.ToInt32(selectedCityId)).ToList();

                        foreach (var item in filteredTownList)
                        {
                            townDropDownListSM.Items.Add(new DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }          
        }

        protected void cityDropDownListHST_ItemSelected(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            townDropDownListHST.Items.Clear();
            var selectedCityId = cityDropDownListHST.SelectedValue;

            ///TODO: veritabanından çekilen veriye göre filtreleme yapılacak

            if (selectedCityId == "6")
            {
                townDropDownListHST.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = "İlçe #1", Value = "İlçe #1" });
            }
            else
            {
                townDropDownListHST.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = "İlçe #2", Value = "İlçe #2" });
                townDropDownListHST.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = "İlçe #3", Value = "İlçe #3" });
            }
        }

        protected void cityDropDownListS_ItemSelected(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            townDropDownListS.Items.Clear();
            var selectedCityId = cityDropDownListS.SelectedValue;

            ///TODO: veritabanından çekilen veriye göre filtreleme yapılacak

            if (selectedCityId == "6")
            {
                townDropDownListS.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = "İlçe #1", Value = "İlçe #1" });
            }
            else
            {
                townDropDownListS.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = "İlçe #2", Value = "İlçe #2" });
                townDropDownListS.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = "İlçe #3", Value = "İlçe #3" });
            }
        }

        protected void buttonUpdateProfile_Click(object sender, EventArgs e)
        {
            try
            {
                var updatedUser = new UpdateUserDTO();

                if (!string.IsNullOrWhiteSpace(User_Password.Text) && !string.IsNullOrWhiteSpace(User_PasswordAgain.Text))
                {
                    if (User_Password.Text != User_PasswordAgain.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");

                    updatedUser.Password = User_Password.Text;
                }

                updatedUser.Id = UserHelper.CurrentUser.Id;
                updatedUser.UserId = UserHelper.CurrentUser.UserId;
                updatedUser.UserTypeId = UserHelper.CurrentUser.UserTypeId;
                updatedUser.IsActive = UserHelper.CurrentUser.IsActive;
                updatedUser.Password = User_Password.Text;


                if (UserHelper.UserTypeId == (int)EnumUserType.NGOHead)
                {
                    updatedUser.FirstName = NGOHead_FirstName.Text;
                    updatedUser.LastName = NGOHead_LastName.Text;
                    updatedUser.Email = NGOHead_Email.Text;
                    updatedUser.BirthDate = NGOHead_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = NGOHead_Phone.Text;
                    updatedUser.DutyStartDate = NGOHead_DutyStartDate.SelectedDate.Value;
                    updatedUser.DutyEndDate = NGOHead_DutyEndDate.SelectedDate;
                    if (updatedUser.DutyEndDate != null)
                    {
                        updatedUser.IsActive = false;
                    }
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.ProjectManager)
                {
                    updatedUser.FirstName = ProjectManager_FirstName.Text;
                    updatedUser.LastName = ProjectManager_LastName.Text;
                    updatedUser.Email = ProjectManager_Email.Text;
                    updatedUser.BirthDate = ProjectManager_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = ProjectManager_Phone.Text;
                    updatedUser.DutyStartDate = ProjectManager_DutyStartDate.SelectedDate.Value;
                    updatedUser.DutyEndDate = ProjectManager_DutyEndDate.SelectedDate;
                    if (updatedUser.DutyEndDate != null)
                    {
                        updatedUser.IsActive = false;
                    }
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
                {
                    updatedUser.Title = ScholarshipCommittee_Title.Text;
                    updatedUser.FirstName = ScholarshipCommittee_FirstName.Text;
                    updatedUser.LastName = ScholarshipCommittee_LastName.Text;
                    updatedUser.Email = ScholarshipCommittee_Email.Text;
                    updatedUser.BirthDate = ScholarshipCommittee_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = ScholarshipCommittee_Phone.Text;
                    updatedUser.DutyStartDate = ScholarshipCommittee_DutyStartDate.SelectedDate.Value;
                    updatedUser.DutyEndDate = ScholarshipCommittee_DutyEndDate.SelectedDate;

                    if (updatedUser.DutyEndDate != null)
                    {
                        updatedUser.IsActive = false;
                    }
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipHolder)
                {
                    updatedUser.FirstName = ScholarshipHolder_FirstName.Text;
                    updatedUser.LastName = ScholarshipHolder_LastName.Text;
                    updatedUser.Email = ScholarshipHolder_Email.Text;
                    updatedUser.BirthDate = ScholarshipHolder_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = ScholarshipHolder_Phone.Text;
                    updatedUser.IbanNo = ScholarshipHolder_IBANNo.Text;
                    updatedUser.EducationLevel = ScholarshipHolder_EducationLevel.Text;
                    updatedUser.School = ScholarshipHolder_School.Text;
                    updatedUser.Class = ScholarshipHolder_Class.Text;
                    updatedUser.CumGPA = Convert.ToInt64(ScholarshipHolder_cumGPA.Text);
                    updatedUser.HealthConditionInfo = ScholarshipHolder_HealthCondition.Text;
                    updatedUser.NumberOfSiblings = Convert.ToInt32(ScholarshipHolder_NumberOfSiblings.Text);
                    ///TODO: Mother-Father-Sibling bilgileri sorulacak.....
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Donator)
                {
                    updatedUser.FirstName = Donator_FirstName.Text;
                    updatedUser.LastName = Donator_LastName.Text;
                    updatedUser.Email = Donator_Email.Text;
                    updatedUser.BirthDate = Donator_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = Donator_Phone.Text;
                    updatedUser.WorkPlace = Donator_WorkPlaceFalse.Text;
                   // updatedUser.OccupationId = Donator_Occupation.Text;
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
                {
                    updatedUser.FirstName = Schoolmaster_FirstName.Text;
                    updatedUser.LastName = Schoolmaster_LastName.Text;
                    updatedUser.Email = Schoolmaster_Email.Text;
                    updatedUser.BirthDate = Schoolmaster_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = Schoolmaster_Phone.Text;
                    updatedUser.School = Schoolmaster_School.Text;
                    updatedUser.CityId = Convert.ToInt32(cityDropDownListSM.SelectedItem.Value);
                    updatedUser.TownId = Convert.ToInt32(townDropDownListSM.SelectedItem.Value);
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
                {
                    updatedUser.FirstName = HostSchoolTeacher_FirstName.Text;
                    updatedUser.LastName = HostSchoolTeacher_LastName.Text;
                    updatedUser.Email = HostSchoolTeacher_Email.Text;
                    updatedUser.BirthDate = HostSchoolTeacher_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = HostSchoolTeacher_Phone.Text;
                    updatedUser.School = HostSchoolTeacher_School.Text;
                    updatedUser.CityId = Convert.ToInt32(cityDropDownListHST.SelectedValue);
                    updatedUser.TownId = Convert.ToInt32(townDropDownListHST.SelectedValue);
                    //updatedUser.Branch = HostSchoolTeacher_Branch.Text;
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Student)
                {
                    updatedUser.FirstName = Student_FirstName.Text;
                    updatedUser.LastName = Student_LastName.Text;
                    updatedUser.Email = Student_Email.Text;
                    updatedUser.BirthDate = Student_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = Student_Phone.Text;
                    updatedUser.School = Student_School.Text;
                    updatedUser.EducationLevel = Student_EducationLevel.Text;
                    updatedUser.CityId = Convert.ToInt32(cityDropDownListS.SelectedValue);
                    updatedUser.TownId = Convert.ToInt32(townDropDownListS.SelectedValue);
                    updatedUser.Class = Student_Class.Text;
                    updatedUser.CumGPA = Convert.ToInt64(Student_CumGPA.Text);
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.Volunteer)
                {
                    updatedUser.FirstName = Volunteer_FirstName.Text;
                    updatedUser.LastName = Volunteer_LastName.Text;
                    updatedUser.Email = Volunteer_Email.Text;
                    updatedUser.BirthDate = Volunteer_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = Volunteer_Phone.Text;

                    if (checkIsStudent.SelectedValue == "1") //Student
                    {
                        updatedUser.Volunteer_IsStudent = true;
                        updatedUser.Volunteer_UniversityId = Convert.ToInt32(Volunteer_University.SelectedValue);
                        updatedUser.Volunteer_DepartmentId = Convert.ToInt32(Volunteer_Department.SelectedValue);
                        updatedUser.Volunteer_Class = Volunteer_Class.Text;
                    }
                    else if (checkIsStudent.SelectedValue == "2") //Not student
                    {
                        updatedUser.Volunteer_IsStudent = false;
                        updatedUser.Volunteer_OccupationId = Convert.ToInt32(Volunteer_Occupation.SelectedValue);
                    }
                }
                if (UserHelper.UserTypeId == (int)EnumUserType.YonDer)
                {
                    updatedUser.FirstName = YonDer_FirstName.Text;
                    updatedUser.LastName = YonDer_LastName.Text;
                    updatedUser.Email = YonDer_Email.Text;
                    updatedUser.BirthDate = YonDer_BirthDate.SelectedDate.Value;
                    updatedUser.PhoneNum = YonDer_Phone.Text;
                    updatedUser.DutyStartDate = YonDer_DutyStartDate.SelectedDate.Value;
                    updatedUser.DutyEndDate = YonDer_DutyEndDate.SelectedDate;
                    if (updatedUser.DutyEndDate != null)
                    {
                        updatedUser.IsActive = false;
                    }
                }

                ServiceResult<UserDTO> serviceResult = new ServiceResult<UserDTO>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProfileApiUrl, "UpdateProfile", queryString, updatedUser);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<UserDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Profil bilgileri güncellendi.";
                labelErrorMessage.Visible = true;

                Session["CurrentUser"] = serviceResult.Result;

                ///TODO: guncellenecekUser DTO bilgisi, WebApi ile Server tarafına gönderilecek ve ilgili Business'ta veri tabanına güncelleme işlemleri yapılacak.
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }

        protected void checkIsStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            divStudent.Visible = false;
            divNotStudent.Visible = false;

            ValidatorUniversityDDL.Enabled = false;
            ValidatorDepartmentDDL.Enabled = false;
            ValidatorVolunteerClass.Enabled = false;
            ValidatorVolunteerOccupation.Enabled = false;

            if (checkIsStudent.SelectedValue == "1") //Student
            {
                divStudent.Visible = true;
                ValidatorUniversityDDL.Enabled = true;
                ValidatorDepartmentDDL.Enabled = true;
                ValidatorVolunteerClass.Enabled = true;
            }
            else if (checkIsStudent.SelectedValue == "2") //Not student
            {
                divNotStudent.Visible = true;
                ValidatorVolunteerOccupation.Enabled = true;
            }
        }

        protected void universityDropDownListV_ItemSelected(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            var selectedUniversityId = Volunteer_University.SelectedValue;
            Init_DepartmentDropDownList(selectedUniversityId);
        }


    }
}