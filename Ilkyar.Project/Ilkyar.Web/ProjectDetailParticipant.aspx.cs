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
    public partial class ProjectDetailParticipant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                Init_UserType();
                GetParticipantList(projectId, projectDetailId);
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

        private void Init_UserType()
        {
            UserType.Items.Clear();

            List<int> availableUserTypeIdList = new List<int>();

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead || UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
                availableUserTypeIdList.Add((int)EnumUserType.Volunteer);
            }


            var userTypeList = EnumHelper.GetEnumAsDictionary(typeof(EnumUserType));
            userTypeList = userTypeList.Where(p => availableUserTypeIdList.Contains(p.Key)).ToDictionary(t => t.Key, t => t.Value);

            foreach (var item in userTypeList)
            {
                UserType.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

        protected void UserType_ItemSelected(object sender, DropDownListEventArgs e)
        {
            UserFirstLastName.Items.Clear();

            try
            {
                var selectedUserTypeId = UserType.SelectedValue;

                var filter = new UserFilterDTO();

                filter.CurrentUserTypeId = UserHelper.CurrentUser.UserTypeId;

                if (UserType.SelectedIndex != -1)
                    filter.UserTypeId = Convert.ToInt32(selectedUserTypeId);

                filter.Status = Convert.ToInt32(EnumUserStatus.Aktif);

                ServiceResult<List<UserDTO>> serviceResult = new ServiceResult<List<UserDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.UserApiUrl, "GetUserList", queryString, filter);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<UserDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        var filteredUserFirstLastNameList = serviceResult.Result.Where(p => p.UserTypeId == Convert.ToInt32(selectedUserTypeId)).ToList();

                        foreach (var item in filteredUserFirstLastNameList)
                        {
                            UserFirstLastName.Items.Add(new DropDownListItem { Text = $"{item.FirstName} {item.LastName}", Value = item.UserId.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void buttonAddParticipant_Click(object sender, EventArgs e)
        {
            var projectId = ProjectId.Value;
            var projectDetailId = ProjectDetailId.Value;

            try
            {
                AddParticipantDTO newParticipant = new AddParticipantDTO()
                {
                    ProjectId = Convert.ToInt64(ProjectId.Value),
                    ProjectDetailId = Convert.ToInt64(ProjectDetailId.Value),
                    UserTypeId = Convert.ToInt32(UserType.SelectedValue),
                    UserId = Convert.ToInt32(UserFirstLastName.SelectedValue)
                };

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "AddNewParticipant", queryString, newParticipant);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Katılımcı eklendi.";
                labelErrorMessage.Visible = true;
                GetParticipantList(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }

        protected void ParticipantListGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string participantId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                var projectId = ProjectId.Value;
                var projectDetailId = ProjectDetailId.Value;

                try
                {
                    DeleteParticipantDTO deleteParticipant = new DeleteParticipantDTO()
                    {
                        ProjectDetailParticipantId = Convert.ToInt64(participantId),
                        ProjectId = Convert.ToInt64(ProjectId.Value),
                        ProjectDetailId = Convert.ToInt64(ProjectDetailId.Value)
                    };

                    ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                    var queryString = new Dictionary<string, string>();
                    var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "DeleteParticipant", queryString, deleteParticipant);
                    if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                    var data = response.Content.ReadAsStringAsync().Result;
                    serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                    if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                        throw new Exception(serviceResult.ErrorMessage);

                    labelErrorMessage.Text = "Katılımcı silindi.";
                    labelErrorMessage.Visible = true;
                    GetParticipantList(Convert.ToInt64(projectId), Convert.ToInt64(projectDetailId));
                }
                catch (Exception ex)
                {
                    labelErrorMessage.Text = ex.Message;
                    labelErrorMessage.Visible = true;
                }
            }
        }

        private void GetParticipantList(long projectId, long projectDetailId)
        {
            try
            {
                ServiceResult<List<ParticipantDTO>> serviceResult = new ServiceResult<List<ParticipantDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectId", projectId.ToString());
                queryString.Add("projectDetailId", projectDetailId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetParticipantList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ParticipantDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                ParticipantListGrid.DataSource = serviceResult.Result;
                ParticipantListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void ParticipantListGrid_PreRender(object sender, EventArgs e)
        {
            foreach (GridColumn col in ParticipantListGrid.MasterTableView.RenderColumns)
            {
                if (col.UniqueName == "Delete")
                {
                    GridButtonColumn boundColumn = (GridButtonColumn)col;
                    if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead || UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager || UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Schoolmaster)
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