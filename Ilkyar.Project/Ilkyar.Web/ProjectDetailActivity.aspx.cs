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
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class ProjectDetailActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.Volunteer)
                Response.Redirect("Home.aspx");

            labelErrorMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                long projectId = -1;
                long projectDetailId = -1;

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
                }
                else
                {
                    Response.Redirect("ListProject.aspx");
                }

                Init_ProjectDetailSummary(projectId, projectDetailId);
                Init_ActivityList(projectDetailId);
                GetActivityList(projectId, projectDetailId);
            }
        }

        private void Init_ActivityList(long projectDetailId)
        {
            try
            {
                ServiceResult<List<ActivityDTO>> serviceResult = new ServiceResult<List<ActivityDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectDetailId", projectDetailId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetActivityList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ActivityDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            ActivityType.Items.Add(new DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Init_ProjectDetailSummary(long projectId, long projectDetailId)
        {
            try
            {
                ServiceResult<ProjectDetailSummaryDTO> serviceResult = new ServiceResult<ProjectDetailSummaryDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectId", projectId.ToString());
                queryString.Add("projectDetailId", projectDetailId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProjectDetailSummary", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<ProjectDetailSummaryDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var projectDetailSummary = serviceResult.Result;
                ProjectDetailSummary.Text = $"{projectDetailSummary.ProjectName} ({projectDetailSummary.ProjectDetailStartDate.ToString("dd/MM/yyyy")} - {projectDetailSummary.ProjectDetailEndDate.ToString("dd/MM/yyyy")})";
            }
            catch (Exception ex)
            {
                Response.Redirect("ListProject.aspx");
            }
        }

        protected void ActivityListGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string activityId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                var projectId = ProjectId.Value;
                var projectDetailId = ProjectDetailId.Value;

                try
                {
                    DeleteProjectDetailActivityDTO deleteProjectDetailActivity = new DeleteProjectDetailActivityDTO()
                    {
                        ActivityProjectDetailId = Convert.ToInt64(activityId),
                        ProjectId = Convert.ToInt64(ProjectId.Value),
                        ProjectDetailId = Convert.ToInt64(ProjectDetailId.Value)
                    };

                    ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                    var queryString = new Dictionary<string, string>();
                    var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "DeleteProjectDetailActivity", queryString, deleteProjectDetailActivity);
                    if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                    var data = response.Content.ReadAsStringAsync().Result;
                    serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                    if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                        throw new Exception(serviceResult.ErrorMessage);

                    labelErrorMessage.Text = "Aktivite silindi.";
                    labelErrorMessage.Visible = true;
                    GetActivityList(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
                }
                catch (Exception ex)
                {
                    labelErrorMessage.Text = ex.Message;
                    labelErrorMessage.Visible = true;
                }
            }
        }

        private void GetActivityList(long projectId, long projectDetailId)
        {
            try
            {
                var filter = new ProjectDetailActivityFilterDTO();
                filter.VolunteerId = UserHelper.CurrentUser.Id;
                filter.ProjectDetailId = projectDetailId;

                ServiceResult<List<ProjectDetailActivityDTO>> serviceResult = new ServiceResult<List<ProjectDetailActivityDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "GetProjectDetailActivityList", queryString, filter);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectDetailActivityDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                ActivityListGrid.DataSource = serviceResult.Result;
                ActivityListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void buttonAddActivity_Click(object sender, EventArgs e)
        {
            var projectId = ProjectId.Value;
            var projectDetailId = ProjectDetailId.Value;

            try
            {
                AddProjectDetailActivityDTO newProjectDetailActivity = new AddProjectDetailActivityDTO()
                {
                    ActivityId = Convert.ToInt32(ActivityType.SelectedValue),
                    ProjectId = Convert.ToInt64(ProjectId.Value),
                    ProjectDetailId = Convert.ToInt64(ProjectDetailId.Value),
                    VolunteerId = UserHelper.CurrentUser.Id,
                    StatusId = (int)EnumActivityStatusType.Beklemede
                };

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "AddNewProjectDetailActivity", queryString, newProjectDetailActivity);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Aktivite eklendi.";
                labelErrorMessage.Visible = true;
                GetActivityList(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }
    }
}