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
    public partial class DisplayProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;
            if (!Page.IsPostBack)
            {
                long projectId = -1;
                long projectManagerId = -1;

                if (!string.IsNullOrEmpty(Request.QueryString["projectId"]))
                {
                    projectId = Convert.ToInt64(Request.QueryString["projectId"]);
                    ProjectId.Value = projectId.ToString();
                }
                else
                {
                    Response.Redirect("ListProject.aspx");
                }

                if (!string.IsNullOrEmpty(Request.QueryString["projectManagerId"]))
                {
                    projectManagerId = Convert.ToInt64(Request.QueryString["projectManagerId"]);
                    ProjectManagerId.Value = projectManagerId.ToString();
                }
                else
                {
                    Response.Redirect("ListProject.aspx");
                }

                Init_ProjectTypeList();
                Init_ProjectManagerList();
                Init_ProjectStatusList();
                GetProject(projectId);
                GetProjectSubDetailList(projectId);

                UserAuthorityControls();
            }
        }

        private void UserAuthorityControls()
        {
            ProjectType.Enabled = false;
            TextBoxProject.Enabled = false;
            ProjectManager.Enabled = false;
            RadDatePickerProjectStartDate.Enabled = false;
            RadDatePickerProjectEndDate.Enabled = false;
            ProjectStatus.Enabled = false;
            createProjectDetail.Visible = false;
            updateProject.Visible = false;

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead)
            {
                ProjectType.Enabled = true;
                TextBoxProject.Enabled = true;
                ProjectManager.Enabled = true;
                RadDatePickerProjectStartDate.Enabled = true;
                RadDatePickerProjectEndDate.Enabled = true;
                ProjectStatus.Enabled = true;
                createProjectDetail.Visible = true;
                updateProject.Visible = true;
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager && UserHelper.CurrentUser.Id == Convert.ToInt64(ProjectManagerId.Value))
            {
                ProjectType.Enabled = true;
                TextBoxProject.Enabled = true;
                ProjectManager.Enabled = true;
                RadDatePickerProjectStartDate.Enabled = true;
                RadDatePickerProjectEndDate.Enabled = true;
                ProjectStatus.Enabled = true;
                createProjectDetail.Visible = true;
                updateProject.Visible = true;
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

                TextBoxProject.Text = project.Name;
                ProjectType.SelectedValue = project.ProjectTypeId.ToString();
                ProjectManager.SelectedValue = project.ProjectManagerId.ToString();
                ProjectStatus.SelectedValue = project.isActive.ToString();
                RadDatePickerProjectStartDate.SelectedDate = project.StartDate;
                RadDatePickerProjectEndDate.SelectedDate = project.EndDate;
            }
            catch (Exception ex)
            {
                Response.Redirect("ListProject.aspx");
            }
        }

        private void GetProjectSubDetailList(long projectId)
        {
            try
            {
                ServiceResult<List<ProjectSubDetailDTO>> serviceResult = new ServiceResult<List<ProjectSubDetailDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectId", projectId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProjectSubDetailList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectSubDetailDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Volunteer)
                {
                    var activityProjectDetailList = GetActivityProjectDetailList();

                    if (activityProjectDetailList != null)
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            if (!activityProjectDetailList.Any(p => p.ProjectDetailId == item.Id))
                            {
                                item.CanCurrentUserApplyToProjectDetailActivity = true;
                            }

                            if (activityProjectDetailList.Any(p => p.ProjectDetailId == item.Id && p.StatusId == (int)EnumActivityStatusType.Beklemede))
                            {
                                item.ActivityProjectDetailId = activityProjectDetailList.Single(p => p.ProjectDetailId == item.Id && p.StatusId == (int)EnumActivityStatusType.Beklemede).Id;
                                item.CanCurrentUserCancelExistingProjectDetailActivity = true;
                            }
                        }
                    }
                }

                ProjectSubDetailListGrid.DataSource = serviceResult.Result;
                ProjectSubDetailListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        private List<ProjectDetailActivityDTO> GetActivityProjectDetailList()
        {
            List<ProjectDetailActivityDTO> result = null;

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Volunteer)
            {
                try
                {
                    var filter = new ProjectDetailActivityFilterDTO();

                    filter.VolunteerId = UserHelper.CurrentUser.UserId;

                    ServiceResult<List<ProjectDetailActivityDTO>> serviceResult = new ServiceResult<List<ProjectDetailActivityDTO>>();
                    var queryString = new Dictionary<string, string>();
                    var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "GetActivityProjectDetailList", queryString, filter);
                    if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                    var data = response.Content.ReadAsStringAsync().Result;
                    serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectDetailActivityDTO>>>(data);

                    if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                        throw new Exception(serviceResult.ErrorMessage);

                    if (serviceResult.Result == null)
                        throw new Exception(serviceResult.ErrorMessage);

                    result = serviceResult.Result;
                }
                catch (Exception ex)
                {
                }
            }

            return result;
        }

        private void Init_ProjectTypeList()
        {
            var projectTypeList = EnumHelper.GetEnumAsDictionary(typeof(EnumProjectType));

            foreach (var item in projectTypeList)
            {
                ProjectType.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

        private void Init_ProjectManagerList()
        {
            try
            {
                ServiceResult<List<ProjectManagerDTO>> serviceResult = new ServiceResult<List<ProjectManagerDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetProjectManagerList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectManagerDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            ProjectManager.Items.Add(new DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Init_ProjectStatusList()
        {
            var projectStatusList = EnumHelper.GetEnumAsDictionary(typeof(EnumProjectStatusType));

            foreach (var item in projectStatusList)
            {
                ProjectStatus.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

        protected void ProjectSubDetailListGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            long projectId = Convert.ToInt64(ProjectId.Value);
           

            if (e.CommandName == "Detail")
            {
                string projectDetailId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                Response.Redirect($"DisplayProjectDetail.aspx?projectId={projectId}&projectDetailId={projectDetailId}");
            }
            if (e.CommandName == "Edit")
            {
                string projectDetailId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                Response.Redirect($"CreateUpdateProjectDetail.aspx?projectId={projectId}&projectDetailId={projectDetailId}");
            }
            if (e.CommandName == "Participants")
            {
                string projectDetailId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                Response.Redirect($"ProjectDetailParticipant.aspx?projectId={projectId}&projectDetailId={projectDetailId}");
            }
            if (e.CommandName == "ActivityOperations")
            {
                string projectDetailId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                Response.Redirect($"ProjectDetailActivity.aspx?projectId={projectId}&projectDetailId={projectDetailId}");
            }
            if (e.CommandName == "ActivityConfirmationOperations")
            {
                string projectDetailId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                Response.Redirect($"ProjectDetailActivityConfirmation.aspx?projectId={projectId}&projectDetailId={projectDetailId}");
            }
            if (e.CommandName == "SurveyProjectDetailResult")
            {
                string projectDetailId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                Response.Redirect($"DisplaySurveyProjectDetailResult.aspx?projectId={projectId}&projectDetailId={projectDetailId}");
            }
        }

        protected void updateProject_Click(object sender, EventArgs e)
        {
            long projectId = Convert.ToInt64(ProjectId.Value);

            try
            {
                var updatedProject = new UpdateProjectDTO();

                updatedProject.Id = projectId;
                updatedProject.ProjectTypeId = Convert.ToInt32(ProjectType.SelectedValue);
                updatedProject.ProjectManagerId = Convert.ToInt32(ProjectManager.SelectedValue);
                updatedProject.StatusId = Convert.ToInt32(ProjectStatus.SelectedValue);
                updatedProject.Name = TextBoxProject.Text;
                updatedProject.StartDate = RadDatePickerProjectStartDate.SelectedDate.Value;
                updatedProject.EndDate = RadDatePickerProjectEndDate.SelectedDate.Value;

                ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "UpdateProject", queryString, updatedProject);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                GetProjectSubDetailList(projectId);

                labelErrorMessage.Text = "Proje detayları güncellendi.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }

        protected void createProjectDetail_Click(object sender, EventArgs e)
        {
            long projectId = Convert.ToInt64(ProjectId.Value);
            Response.Redirect($"CreateUpdateProjectDetail.aspx?projectId={projectId}");
        }

        protected void ProjectSubDetailListGrid_PreRender(object sender, EventArgs e)
        {
            foreach (GridColumn col in ProjectSubDetailListGrid.MasterTableView.RenderColumns)
            {
                if (col.UniqueName == "Edit")
                {
                    GridTemplateColumn boundColumn = (GridTemplateColumn)col;
                    if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead)
                    {
                        boundColumn.Visible = true;
                    }
                    else if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager && UserHelper.CurrentUser.Id == Convert.ToInt64(ProjectManagerId.Value))
                    {
                        boundColumn.Visible = true;
                    }
                    
                    else
                    {
                        boundColumn.Visible = false;
                    }
                }

                if (col.UniqueName == "ActivityOperations")
                {
                    GridButtonColumn boundColumn = (GridButtonColumn)col;
                    if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Volunteer)
                    {
                        boundColumn.Visible = true;
                    }
                    else
                    {
                        boundColumn.Visible = false;
                    }
                }

                if (col.UniqueName == "ActivityConfirmationOperations" || col.UniqueName == "Participants")
                {
                    GridButtonColumn boundColumn = (GridButtonColumn)col;
                    if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager && UserHelper.CurrentUser.Id == Convert.ToInt64(ProjectManagerId.Value))
                    {
                        boundColumn.Visible = true;
                    }
                    else
                    {
                        boundColumn.Visible = false;
                    }
                }

                if (col.UniqueName == "SurveyProjectDetailResult")
                {
                    GridButtonColumn boundColumn = (GridButtonColumn)col;
                    if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead)
                    {
                        boundColumn.Visible = true;
                    }
                    else
                    {
                        boundColumn.Visible = false;
                    }
                }
            }
        }
    }
}