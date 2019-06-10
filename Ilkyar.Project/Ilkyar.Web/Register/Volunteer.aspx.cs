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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Ilkyar.Web.Register
{
    public partial class Volunteer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                Init_UniversityDropDownList();
                Init_InterestDropDownList();
            }
        }

        private void Init_InterestDropDownList()
        {
            try
            {
                ServiceResult<List<InterestDTO>> serviceResult = new ServiceResult<List<InterestDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetInterestList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<InterestDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            RadComboBoxInterest.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = item.Name, Value = item.Id.ToString() });
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
                            universityDropDownListV.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void universityDropDownListV_ItemSelected(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            departmentDropDownListV.Items.Clear();
            var selectedUniversityId = universityDropDownListV.SelectedValue;

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
                            departmentDropDownListV.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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

        protected void buttonCreateNewVolunteer_Click(object sender, EventArgs e)
        {
            try
            {
                var newVolunteer = new CreateNewVolunteerDTO();

                if (!string.IsNullOrWhiteSpace(Volunteer_Password.Text) && !string.IsNullOrWhiteSpace(Volunteer_PasswordAgain.Text))
                {
                    if (Volunteer_Password.Text != Volunteer_PasswordAgain.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newVolunteer.UserTypeId = (int)EnumUserType.Volunteer;
                newVolunteer.FirstName = Volunteer_FirstName.Text;
                newVolunteer.LastName = Volunteer_LastName.Text;
                newVolunteer.BirthDate = Volunteer_BirthDate.SelectedDate.Value;
                newVolunteer.PhoneNum = Volunteer_Phone.Text;
                newVolunteer.Username = Volunteer_TCKN.Text;
                newVolunteer.Email = Volunteer_Email.Text;
                newVolunteer.Password = Volunteer_Password.Text;

                var collection = RadComboBoxInterest.CheckedItems;

                if(collection.Count != 3) throw new Exception("Lütfen üç ilgi alanı seçiniz!");

                var temp = collection.Count;

                if (collection.Count != 0)
                {
                    foreach (var item in collection)
                    {
                        if (temp == 3)
                        {
                            newVolunteer.Interest3Id = Convert.ToInt32(item.Value);
                            temp--;
                        }
                        else if (temp == 2)
                        {
                            newVolunteer.Interest2Id = Convert.ToInt32(item.Value);
                            temp--;
                        }
                        else
                        {
                            newVolunteer.Interest1Id = Convert.ToInt32(item.Value);
                        }
                            
                    }

                }

                if (checkIsStudent.SelectedValue == "1") //Student
                {
                    newVolunteer.IsStudent = true;
                    newVolunteer.UniversityId = Convert.ToInt32(universityDropDownListV.SelectedValue);
                    newVolunteer.DepartmentId = Convert.ToInt32(departmentDropDownListV.SelectedValue);
                    newVolunteer.Class = Volunteer_Class.Text;
                }
                else if (checkIsStudent.SelectedValue == "2") //Not student
                {
                    newVolunteer.OccupationId = Convert.ToInt32(Volunteer_Occupation.SelectedValue);
                }

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewVolunteer", queryString, newVolunteer);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Gönüllü üyeliği oluşturuldu.";
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