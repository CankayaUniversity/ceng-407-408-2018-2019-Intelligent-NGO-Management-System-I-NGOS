using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class CreateUpdateProjectDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                long projectId = -1;
                long projectDetailId = -1;
                bool isProjectDetailEditMode = false;

                //ValidatorRadDropDownListTown.Enabled = false;
                ValidatorTextBoxSchool.Enabled = false;
                ValidatorRadDatePickerProjecStartDate.Enabled = false;
                ValidatorRadDatePickerProjectEndDate.Enabled = false;
                ValidatorDepartureDate.Enabled = false;
                ValidatorArrivalDate.Enabled = false;
                ValidatorTextBoxAccomodation.Enabled = false;
                ValidatorTextBoxNumOfPep.Enabled = false;
                ValidatorActivity.Enabled = false;
                ValidatorDeparturePoint.Enabled = false;
                ValidatorDepartureTransportationTypeInfo.Enabled = false;
                ValidatorDepartureNumberOfPeople.Enabled = false;
                ValidatorArrivalPoint.Enabled = false;
                ValidatorArrivalTansportationTypeInfo.Enabled = false;
                ValidatorArrivalNumberOfPeople.Enabled = false;
                ValidatorArrivalTransportationType.Enabled = false;
                ValidatorTransportationType.Enabled = false;

                if (!string.IsNullOrEmpty(Request.QueryString["projectId"]))
                {
                    projectId = Convert.ToInt64(Request.QueryString["projectId"]);
                    ProjectId.Value = projectId.ToString();
                }
                else
                {
                    Response.Redirect("ListProject.aspx");
                }

                if (!string.IsNullOrEmpty(Request.QueryString["projectDetailId"]))
                {
                    projectDetailId = Convert.ToInt64(Request.QueryString["projectDetailId"]);
                    ProjectDetailId.Value = projectDetailId.ToString();
                    isProjectDetailEditMode = true;
                }

                if (isProjectDetailEditMode) //Update
                {
                    //GetProjectDetail servisi çağırılacak
                    buttonUpdateProjectDetail.Visible = true;
                }
                else //Create
                {
                    buttonCreateProjectDetail.Visible = true;
                    //Kullanıcıdan proje detayına ait bilgiler alınıp veritabanına kayıt yapılacak.
                }

                Init_RadDropDownListCity();
                Init_TransportationTypeList();
                Init_ActivityDropDownList();
                Init_RadDropDownListSchoolType();
                GetProject(projectId);

                if (isProjectDetailEditMode)
                {
                    GetProjectSubDetail(projectId, projectDetailId);
                    //ValidatorRadDropDownListTown.Enabled = true;
                }
            }
        }

        private void Init_RadDropDownListCity()
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
                        foreach (var item in serviceResult.Result)
                        {
                            RadDropDownListCity.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RadDropDownListCity_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            var selectedCityId = RadDropDownListCity.SelectedValue;
            Init_RadDropDownListTown(selectedCityId);
        }

        private void Init_RadDropDownListTown(string selectedCityId)
        {
            RadDropDownListTown.Items.Clear();
            RadDropDownListTown.SelectedIndex = -1;

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
                            RadDropDownListTown.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Init_RadDropDownListSchoolType()
        {
            var SchoolTypeList = EnumHelper.GetEnumAsDictionary(typeof(EnumSchoolType));

            foreach (var item in SchoolTypeList)
            {
                RadDropDownListSchoolType.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Value, Value = item.Key.ToString() });
            }
        }

        private void Init_TransportationTypeList()
        {
            var TransportationTypeList = EnumHelper.GetEnumAsDictionary(typeof(EnumTransportationType));

            foreach (var item in TransportationTypeList)
            {
                ArrivalTransportationType.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
                DepartureTransportationType.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

        private void Init_ActivityDropDownList()
        {
            try
            {
                ServiceResult<List<ActivityDTO>> serviceResult = new ServiceResult<List<ActivityDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetActivityList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ActivityDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            RadComboBoxActivity.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetProject(long projectId)
        {
            try
            {
                ServiceResult<ProjectDTO> serviceResult = new ServiceResult<ProjectDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectId", projectId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProject", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<ProjectDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var project = serviceResult.Result;

                TextBoxProjectType.Text = project.ProjectTypeName;
                TextBoxProject.Text = project.Name;
                TextBoxProjectManager.Text = project.ProjectManagerName;
                TextBoxProjectStartDate.Text = project.StartDate.ToString("dd.MM.yyyy");
                TextBoxProjectEndDate.Text = project.EndDate.ToString("dd.MM.yyyy");
                TextBoxProjectStatus.Text = project.StatusName;
                RadDatePickerProjectStartDate.MinDate = project.StartDate;
                RadDatePickerProjectStartDate.MaxDate = project.EndDate;
                DepartureDate.MinDate = project.StartDate;
                DepartureDate.MaxDate = project.EndDate;
                ArrivalDate.MinDate = project.StartDate;
                ArrivalDate.MaxDate = project.EndDate;
            }
            catch (Exception ex)
            {
                Response.Redirect("ListProject.aspx");
            }
        }

        private void GetProjectSubDetail(long projectId, long projectDetailId)
        {
            try
            {
                ServiceResult<ProjectSubDetailDTO> serviceResult = new ServiceResult<ProjectSubDetailDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectId", projectId.ToString());
                queryString.Add("projectDetailId", projectDetailId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProjectSubDetail", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                //var data = response.Content.ReadAsStringAsync();
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<ProjectSubDetailDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var project = serviceResult.Result;

                RadDropDownListCity.SelectedValue = project.CityId.ToString();
                Init_RadDropDownListTown(project.CityId.ToString());
                RadDropDownListTown.SelectedValue = project.TownId.ToString();
                RadDropDownListSchoolType.SelectedValue = project.SchoolTypeId.ToString();
                TextBoxSchool.Text = project.School;
                TextBoxMaterial.Text = project.ReqText;
                RadDatePickerProjectStartDate.SelectedDate = project.DetailStartDate;
                RadDatePickerProjectEndDate.SelectedDate = project.DetailEndDate;
                DepartureDate.SelectedDate = project.TrnsStartDate;
                DepartureTransportationType.SelectedValue = project.TransportationTypeId.ToString();
                DeparturePoint.Text = project.Departure;
                DepartureTransportationTypeInfo.Text = project.DepartureFirm;
                DepartureNumberOfPeople.Text = project.TrnsNumOfPeople.ToString();
                ArrivalDate.SelectedDate = project.TrnsEndDate;
                ArrivalTransportationType.SelectedValue = project.ArrivalTransportationTypeId.ToString();
                ArrivalPoint.Text = project.Comeback;
                ArrivalTansportationTypeInfo.Text = project.ComebackFirm;
                ArrivalNumberOfPeople.Text = project.TrnsArrNumOfPeople.ToString();
                TextBoxAccomodation.Text = project.Inn;
                TextBoxNumOfPep.Text = project.AccNumOfPeople.ToString();
                TextAreaNote.Text = project.ProjectInfo;

                string Name;
                RadComboBoxActivity.ClearCheckedItems();

                foreach (var activity in project.ActivityList)
                {
                    Name = activity.Name;
                    RadComboBoxActivity.Text = string.Empty;
                    RadComboBoxActivity.Text = Name;
                    RadComboBoxActivity.Items.FindItemByText(Name).Checked = true;
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("ListProject.aspx");
            }
        }

        protected void LinkButtonBack_Click(object sender, EventArgs e)
        {
            string projectId = ProjectId.Value;
            Response.Redirect($"DisplayProject.aspx?projectId={projectId}");
        }

        protected void buttonCreateProjectDetail_Click(object sender, EventArgs e)
        {
            try
            {
                var newProjectDetail = new CreateProjectDetailDTO();


                newProjectDetail.ProjectId = Convert.ToInt64(ProjectId.Value);
                newProjectDetail.CityId = Convert.ToInt32(RadDropDownListCity.SelectedValue);
                newProjectDetail.TownId = Convert.ToInt32(RadDropDownListTown.SelectedValue);
                newProjectDetail.SchoolTypeId = Convert.ToInt32(RadDropDownListSchoolType.SelectedValue);
                newProjectDetail.School = TextBoxSchool.Text;
                newProjectDetail.DetailStartDate = RadDatePickerProjectStartDate.SelectedDate.Value;
                newProjectDetail.ReqText = TextBoxMaterial.Text;
                newProjectDetail.DetailEndDate = RadDatePickerProjectEndDate.SelectedDate.Value;
                newProjectDetail.TrnsStartDate = DepartureDate.SelectedDate.Value;
                newProjectDetail.TransportationTypeId = Convert.ToInt32(DepartureTransportationType.SelectedValue);
                newProjectDetail.Departure = DeparturePoint.Text;
                newProjectDetail.DepartureFirm = DepartureTransportationTypeInfo.Text;
                newProjectDetail.TrnsNumOfPeople = Convert.ToInt32(DepartureNumberOfPeople.Text);
                newProjectDetail.TrnsEndDate = ArrivalDate.SelectedDate.Value;
                newProjectDetail.ArrivalTransportationTypeId = Convert.ToInt32(ArrivalTransportationType.SelectedValue);
                newProjectDetail.Comeback = ArrivalPoint.Text;
                newProjectDetail.ComebackFirm = ArrivalTansportationTypeInfo.Text;
                newProjectDetail.TrnsArrNumOfPeople = Convert.ToInt32(ArrivalNumberOfPeople.Text);
                newProjectDetail.Inn = TextBoxAccomodation.Text;
                newProjectDetail.AccNumOfPeople = Convert.ToInt32(TextBoxNumOfPep.Text);
                newProjectDetail.ProjectInfo = TextAreaNote.Text;
                newProjectDetail.StatusId = (int)EnumProjectStatusType.Aktif;

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "CreateProjectDetail", queryString, newProjectDetail);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Proje detayları eklendi.";
                labelErrorMessage.Visible = true;

                var collection = RadComboBoxActivity.CheckedItems;

                if (collection.Count != 0)
                {
                    foreach (var item in collection)
                    {
                        try
                        {
                            AddProjectActivityDTO newProjectActivity = new AddProjectActivityDTO()
                            {
                                ActivityId = Convert.ToInt32(item.Value),
                                ProjectId = Convert.ToInt64(ProjectId.Value),
                                ProjectDetailId = Convert.ToInt64(serviceResult.Result)
                            };

                            ServiceResult<long> serviceResultActivity = new ServiceResult<long>();
                            var queryStringActivity = new Dictionary<string, string>();
                            var responseActivity = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "AddNewProjectActivity", queryStringActivity, newProjectActivity);
                            if (!responseActivity.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                            var dataActivity = responseActivity.Content.ReadAsStringAsync().Result;
                            serviceResultActivity = JsonConvert.DeserializeObject<ServiceResult<long>>(dataActivity);

                            if (serviceResultActivity.ServiceResultType != EnumServiceResultType.Success)
                                throw new Exception(serviceResultActivity.ErrorMessage);

                        }
                        catch (Exception ex)
                        {
                            labelErrorMessage.Text = ex.Message;
                            labelErrorMessage.Visible = true;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }

        }

        protected void buttonUpdateProjectDetail_Click(object sender, EventArgs e)
        {
            try
            {
                var updatedProjectSubDetail = new UpdateProjectSubDetailDTO();

                updatedProjectSubDetail.ProjectId = Convert.ToInt64(ProjectId.Value);
                updatedProjectSubDetail.Id = Convert.ToInt64(ProjectDetailId.Value);
                updatedProjectSubDetail.CityId = Convert.ToInt32(RadDropDownListCity.SelectedValue);
                updatedProjectSubDetail.TownId = Convert.ToInt32(RadDropDownListTown.SelectedValue);
                updatedProjectSubDetail.SchoolTypeId = Convert.ToInt32(RadDropDownListSchoolType.SelectedValue);
                updatedProjectSubDetail.School = TextBoxSchool.Text;
                updatedProjectSubDetail.ReqText = TextBoxMaterial.Text;
                updatedProjectSubDetail.DetailStartDate = RadDatePickerProjectStartDate.SelectedDate.Value;
                updatedProjectSubDetail.DetailEndDate = RadDatePickerProjectEndDate.SelectedDate.Value;
                updatedProjectSubDetail.TrnsStartDate = DepartureDate.SelectedDate.Value;
                updatedProjectSubDetail.TrnsEndDate = ArrivalDate.SelectedDate.Value;
                updatedProjectSubDetail.TransportationTypeId = Convert.ToInt32(DepartureTransportationType.SelectedValue);
                updatedProjectSubDetail.ArrivalTransportationTypeId = Convert.ToInt32(ArrivalTransportationType.SelectedValue);
                updatedProjectSubDetail.Departure = DeparturePoint.Text;
                updatedProjectSubDetail.DepartureFirm = DepartureTransportationTypeInfo.Text;
                updatedProjectSubDetail.TrnsNumOfPeople = Convert.ToInt32(DepartureNumberOfPeople.Text);
                updatedProjectSubDetail.Comeback = ArrivalPoint.Text;
                updatedProjectSubDetail.ComebackFirm = ArrivalTansportationTypeInfo.Text;
                updatedProjectSubDetail.TrnsArrNumOfPeople = Convert.ToInt32(ArrivalNumberOfPeople.Text);
                updatedProjectSubDetail.Inn = TextBoxAccomodation.Text;
                updatedProjectSubDetail.AccNumOfPeople = Convert.ToInt32(TextBoxNumOfPep.Text);
                updatedProjectSubDetail.ProjectInfo = TextAreaNote.Text;
                //updatedProjectSubDetail.StatusId = 

                ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "UpdateProjectSubDetail", queryString, updatedProjectSubDetail);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Proje detayları güncellendi.";
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

