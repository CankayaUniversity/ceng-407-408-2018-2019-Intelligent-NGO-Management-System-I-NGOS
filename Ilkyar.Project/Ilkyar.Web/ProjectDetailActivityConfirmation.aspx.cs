using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class ProjectDetailActivityConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.ProjectManager)
                Response.Redirect("Home.aspx");

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
                GetActivityList(projectId, projectDetailId);
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
            if (e.CommandName == "Confirm")
            {
                string projectDetailActivityId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                UpdateProjectDetailActivity(Convert.ToInt64(projectDetailActivityId), (int)EnumActivityStatusType.Onaylandi);
            }
            if (e.CommandName == "Reject")
            {
                string projectDetailActivityId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                UpdateProjectDetailActivity(Convert.ToInt64(projectDetailActivityId), (int)EnumActivityStatusType.Reddedildi);
            }
            if (e.CommandName == "Select")
            {
                string userId = (e.Item as GridDataItem).GetDataKeyValue("UserId").ToString();
                Response.Redirect($"ViewUser.aspx?userId={userId}");
            }
            if (e.CommandName == "ShowPopup")
            {
                string projectDetailActivityId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                ShowVolunteerSuggestionPopup(Convert.ToInt64(projectDetailActivityId));
            }
        }

        private void ShowVolunteerSuggestionPopup(long projectDetailActivityId)
        {
            try
            {
                VolunteerSuggestionFilterDTO filter = new VolunteerSuggestionFilterDTO { ProjectDetailActivityId = projectDetailActivityId };

                ServiceResult<VolunteerSuggestionDTO> serviceResult = new ServiceResult<VolunteerSuggestionDTO>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "GetVolunteerSuggestion", queryString, filter);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<VolunteerSuggestionDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                VolunteerSuggestionPopup.Modal = true;
                VolunteerSuggestionPopup.VisibleOnPageLoad = true;

                VolunteerFullName.Text = serviceResult.Result.VolunteerFullName;
                CurrentActivityName.Text = serviceResult.Result.CurrentActivityName;
                ApprovedCityMatchPercentage.Value = serviceResult.Result.ApprovedCityMatchPercentage;
                OverallCityMatchPercentage.Value = serviceResult.Result.OverallCityMatchPercentage;
                ApprovedRegionMatchPercentage.Value = serviceResult.Result.ApprovedRegionMatchPercentage;
                OverallRegionMatchPercentage.Value = serviceResult.Result.OverallRegionMatchPercentage;
                ApprovedSchoolTypeMatchPercentage.Value = serviceResult.Result.ApprovedSchoolTypeMatchPercentage;
                OverallSchoolTypeMatchPercentage.Value = serviceResult.Result.OverallSchoolTypeMatchPercentage;
                ApprovedNumberOfPeopleMatchTolerancePercentage.InnerText = serviceResult.Result.ApprovedNumberOfPeopleMatchTolerancePercentage.ToString("#.##");
                OverallNumberOfPeopleMatchTolerancePercentage.InnerText = serviceResult.Result.OverallNumberOfPeopleMatchTolerancePercentage.ToString("#.##");
                ApprovedActivityCount.InnerText = serviceResult.Result.ApprovedActivityCount.ToString();
                RejectedActivityCount.InnerText = serviceResult.Result.RejectedActivityCount.ToString();
                OverallActivityCount.InnerText = serviceResult.Result.OverallActivityCount.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateProjectDetailActivity(long projectDetailActivityId, int statusId)
        {
            var projectId = ProjectId.Value;
            var projectDetailId = ProjectDetailId.Value;

            try
            {
                UpdateProjectDetailActivityDTO updateProjectDetailActivity = new UpdateProjectDetailActivityDTO()
                {
                    ProjectDetailActivityId = Convert.ToInt64(projectDetailActivityId),
                    ProjectId = Convert.ToInt64(ProjectId.Value),
                    ProjectDetailId = Convert.ToInt64(ProjectDetailId.Value),
                    StatusId = statusId
                };

                ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "UpdateProjectDetailActivity", queryString, updateProjectDetailActivity);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                GetActivityList(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
            }
            catch (Exception ex)
            {
            }
        }

        private void GetActivityList(long projectId, long projectDetailId)
        {
            try
            {
                var filter = new ProjectDetailActivityFilterDTO();
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
    }
}