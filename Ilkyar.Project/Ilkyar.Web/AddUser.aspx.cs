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
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;
            if (!Page.IsPostBack)
            {
                Init_UserType();

                //ValidatorNGOHead_TCKN.Enabled = false;
                //ValidatorNGOHead_FirstName.Enabled = false;
                //ValidatorNGOHead_LastName.Enabled = false;
                //ValidatorNGOHead_BirthDate.Enabled = false;
                //ValidatorNGOHead_DutyStartDate.Enabled = false;
                //ValidatorNGOHead_Email.Enabled = false;
                //ValidatorNGOHead_Phone.Enabled = false;
                //ValidatorNGOHead_Password.Enabled = false;
                //ValidatorNGOHead_Password2.Enabled = false;



            }
        }

        private void Init_UserType()
        {
            List<int> availableUserTypeIdList = new List<int>();

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
                availableUserTypeIdList.Add((int)EnumUserType.YonDer);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager)
            {
                availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
            {
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                availableUserTypeIdList.Add((int)EnumUserType.YonDer);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ScholarshipHolder)
            {
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Donator)
            {
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Schoolmaster)
            { 
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
            {               
                availableUserTypeIdList.Add((int)EnumUserType.Student);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Student)
            {
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Volunteer)
            {
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.YonDer)
            {
            }

            var userTypeList = EnumHelper.GetEnumAsDictionary(typeof(EnumUserType));
            userTypeList = userTypeList.Where(p => availableUserTypeIdList.Contains(p.Key)).ToDictionary(t => t.Key, t => t.Value);

            foreach (var item in userTypeList)
            {
                UserType.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }




        protected void buttonCreateNGOHead_Click(object sender, EventArgs e)
        {
            try
            {
                //ValidatorNGOHead_TCKN.Enabled = true;
                //ValidatorNGOHead_FirstName.Enabled = true;
                //ValidatorNGOHead_LastName.Enabled = true;
                //ValidatorNGOHead_BirthDate.Enabled = true;
                //ValidatorNGOHead_DutyStartDate.Enabled = true;
                //ValidatorNGOHead_Email.Enabled = true;
                //ValidatorNGOHead_Phone.Enabled = true;
                //ValidatorNGOHead_Password.Enabled = true;
                //ValidatorNGOHead_Password2.Enabled = true;

                var newNGOHead = new CreateNewNGOHeadDTO();

                if (!string.IsNullOrWhiteSpace(NGOHead_Password.Text) && !string.IsNullOrWhiteSpace(NGOHead_Password2.Text))
                {
                    if (NGOHead_Password.Text != NGOHead_Password2.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newNGOHead.UserTypeId = (int)EnumUserType.NGOHead;
                newNGOHead.FirstName = NGOHead_FirstName.Text;
                newNGOHead.LastName = NGOHead_LastName.Text;
                newNGOHead.BirthDate = NGOHead_BirthDate.SelectedDate.Value;
                newNGOHead.PhoneNum = NGOHead_Phone.Text;
                newNGOHead.Username = NGOHead_TCKN.Text;
                newNGOHead.Email = NGOHead_Email.Text;
                newNGOHead.Password = NGOHead_Password.Text;
                newNGOHead.Password = NGOHead_Password.Text;
                newNGOHead.DutyStartDate = NGOHead_DutyStartDate.SelectedDate.Value;

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewNGOHead", queryString, newNGOHead);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Organizasyon Başkanı üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }


        }

        protected void buttonCreateProjectManager_Click(object sender, EventArgs e)
        {

            try
            {
                var newProjectManager = new CreateNewProjectManagerDTO();

                if (!string.IsNullOrWhiteSpace(ProjectManager_Password.Text) && !string.IsNullOrWhiteSpace(ProjectManager_Password2.Text))
                {
                    if (ProjectManager_Password.Text != ProjectManager_Password2.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newProjectManager.UserTypeId = (int)EnumUserType.ProjectManager;
                newProjectManager.FirstName = ProjectManager_FirstName.Text;
                newProjectManager.LastName = ProjectManager_LastName.Text;
                newProjectManager.BirthDate = ProjectManager_BirthDate.SelectedDate.Value;
                newProjectManager.PhoneNum = ProjectManager_Phone.Text;
                newProjectManager.Username = ProjectManager_TCKN.Text;
                newProjectManager.Email = ProjectManager_Email.Text;
                newProjectManager.Password = ProjectManager_Password.Text;
                newProjectManager.Password = ProjectManager_Password.Text;
                newProjectManager.DutyStartDate = ProjectManager_DutyStartDate.SelectedDate.Value;

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewProjectManager", queryString, newProjectManager);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Proje yöneticisi üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }

        }

        protected void buttonCreateScholarshipCommittee_Click(object sender, EventArgs e)
        {

            try
            {
                var newScholarshipCommittee = new CreateNewScholarshipCommitteeDTO();

                if (!string.IsNullOrWhiteSpace(ScholarshipCommittee_Password.Text) && !string.IsNullOrWhiteSpace(ScholarshipCommittee_Password2.Text))
                {
                    if (ScholarshipCommittee_Password.Text != ScholarshipCommittee_Password2.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newScholarshipCommittee.UserTypeId = (int)EnumUserType.ScholarshipCommittee;
                newScholarshipCommittee.FirstName = ScholarshipCommittee_FirstName.Text;
                newScholarshipCommittee.LastName = ScholarshipCommittee_LastName.Text;
                newScholarshipCommittee.BirthDate = ScholarshipCommittee_BirthDate.SelectedDate.Value;
                newScholarshipCommittee.PhoneNum = ScholarshipCommittee_Phone.Text;
                newScholarshipCommittee.Title = ScholarshipCommittee_Title.Text;
                newScholarshipCommittee.Username = ScholarshipCommittee_TCKN.Text;
                newScholarshipCommittee.Email = ScholarshipCommittee_Email.Text;
                newScholarshipCommittee.Password = ScholarshipCommittee_Password.Text;
                newScholarshipCommittee.Password = ScholarshipCommittee_Password.Text;
                newScholarshipCommittee.DutyStartDate = ScholarshipCommittee_DutyStartDate.SelectedDate.Value;

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewScholarshipCommittee", queryString, newScholarshipCommittee);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Burs komitesi üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {

                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            
            }
        }

        protected void buttonCreateYonDer_Click(object sender, EventArgs e)
        {

            try
            {
                var newYonDer = new CreateNewYonderDTO();

                if (!string.IsNullOrWhiteSpace(YonDer_Password.Text) && !string.IsNullOrWhiteSpace(YonDer_Password2.Text))
                {
                    if (YonDer_Password.Text != YonDer_Password2.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newYonDer.UserTypeId = (int)EnumUserType.YonDer;
                newYonDer.FirstName = YonDer_FirstName.Text;
                newYonDer.LastName = YonDer_LastName.Text;
                newYonDer.BirthDate = YonDer_BirthDate.SelectedDate.Value;
                newYonDer.PhoneNum = YonDer_Phone.Text;
                newYonDer.Username = YonDer_TCKN.Text;
                newYonDer.Email = YonDer_Email.Text;
                newYonDer.Password = YonDer_Password.Text;
                newYonDer.Password = YonDer_Password.Text;
                newYonDer.DutyStartDate = YonDer_DutyStartDate.SelectedDate.Value;

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewYonDer", queryString, newYonDer);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Yön-Der üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }

        }

        protected void buttonCreateSchoolmaster_Click(object sender, EventArgs e)
        {

            try
            {
                var newSchoolmaster = new CreateNewSchoolmasterDTO();

                if (!string.IsNullOrWhiteSpace(Schoolmaster_Password.Text) && !string.IsNullOrWhiteSpace(Schoolmaster_Password2.Text))
                {
                    if (Schoolmaster_Password.Text != Schoolmaster_Password2.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newSchoolmaster.UserTypeId = (int)EnumUserType.Schoolmaster;
                newSchoolmaster.FirstName = Schoolmaster_FirstName.Text;
                newSchoolmaster.LastName = Schoolmaster_LastName.Text;
                newSchoolmaster.BirthDate = Schoolmaster_BirthDate.SelectedDate.Value;
                newSchoolmaster.PhoneNum = Schoolmaster_Phone.Text;
                newSchoolmaster.Username = Schoolmaster_TCKN.Text;
                newSchoolmaster.Email = Schoolmaster_Email.Text;
                newSchoolmaster.Password = Schoolmaster_Password.Text;
                newSchoolmaster.Password = Schoolmaster_Password.Text;
                //newSchoolmaster.SchoolID = ;

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewSchoolmaster", queryString, newSchoolmaster);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Organizasyon Başkanı üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }

        }

        protected void buttonCreateHostSchoolTeacher_Click(object sender, EventArgs e)
        {

            try
            {
                var newHostSchoolTeacher = new CreateNewHostSchoolTeacherDTO();

                if (!string.IsNullOrWhiteSpace(HostSchoolTeacher_Password.Text) && !string.IsNullOrWhiteSpace(HostSchoolTeacher_Password2.Text))
                {
                    if (HostSchoolTeacher_Password.Text != HostSchoolTeacher_Password2.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newHostSchoolTeacher.UserTypeId = (int)EnumUserType.HostSchoolTeacher;
                newHostSchoolTeacher.FirstName = HostSchoolTeacher_FirstName.Text;
                newHostSchoolTeacher.LastName = HostSchoolTeacher_LastName.Text;
                newHostSchoolTeacher.BirthDate = HostSchoolTeacher_BirthDate.SelectedDate.Value;
                newHostSchoolTeacher.PhoneNum = HostSchoolTeacher_Phone.Text;
                newHostSchoolTeacher.Username = HostSchoolTeacher_TCKN.Text;
                newHostSchoolTeacher.Email = HostSchoolTeacher_Email.Text;
                newHostSchoolTeacher.Password = HostSchoolTeacher_Password.Text;
                newHostSchoolTeacher.Password = HostSchoolTeacher_Password.Text;
                //newHostSchoolTeacher.BranchID = ;
                //newHostSchoolTeacher.SchoolID = ;

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewHostSchoolTeacher", queryString, newHostSchoolTeacher);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Okul Öğretmeni üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }

        }

        protected void buttonCreateStudent_Click(object sender, EventArgs e)
        {

            try
            {
                var newStudent = new CreateNewStudentDTO();

                if (!string.IsNullOrWhiteSpace(Student_Password.Text) && !string.IsNullOrWhiteSpace(Student_Password2.Text))
                {
                    if (Student_Password.Text != Student_Password2.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newStudent.UserTypeId = (int)EnumUserType.Student;
                newStudent.FirstName = Student_FirstName.Text;
                newStudent.LastName = Student_LastName.Text;
                newStudent.BirthDate = Student_BirthDate.SelectedDate.Value;
                newStudent.PhoneNum = Student_Phone.Text;
                newStudent.Username = Student_TCKN.Text;
                newStudent.Email = Student_Email.Text;
                newStudent.Password = Student_Password.Text;
                newStudent.Password = Student_Password.Text;
                //newStudent.SchoolID = ;
                newStudent.EducationLevel = Student_EducationLevel.Text;
                newStudent.Class = Student_Class.Text;
                newStudent.CumGPA = Convert.ToSingle(Student_CumGPA.Text);

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewStudent", queryString, newStudent);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Organizasyon Başkanı üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }

        }
    }
}