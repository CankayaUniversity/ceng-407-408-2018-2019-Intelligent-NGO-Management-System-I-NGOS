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
    public partial class ListProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Init_ProjectType();
                Init_UserStatus();
                FilterProjectList();
            }
        }

        protected void buttonFilterProjectList_Click(object sender, EventArgs e)
        {
            FilterProjectList();
        }

        protected void buttonClearFilter_Click(object sender, EventArgs e)
        {
            ProjectType.SelectedIndex = -1;
            ProjectManager.Text = null;
            ProjectName.Text = null;
            ProjectStartDate.SelectedDate = null;
            ProjectEndDate.SelectedDate = null;
            ProjectStatus.SelectedIndex = -1;

            FilterProjectList();
        }

        private void FilterProjectList()
        {
            try
            {
                var filter = new ProjectFilterDTO();

                if (ProjectType.SelectedIndex != -1)
                    filter.ProjectTypeId = Convert.ToInt32(ProjectType.SelectedValue);

                filter.ProjectManagerName = ProjectManager.Text;
                filter.ProjectName = ProjectName.Text;
                filter.StartDate = ProjectStartDate.SelectedDate;
                filter.EndDate = ProjectEndDate.SelectedDate;

                if (ProjectStatus.SelectedIndex != -1)
                    filter.Status = Convert.ToInt32(ProjectStatus.SelectedValue);

                ServiceResult<List<ProjectDTO>> serviceResult = new ServiceResult<List<ProjectDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "GetProjectList", queryString, filter);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                ProjectListGrid.DataSource = serviceResult.Result.OrderByDescending(o => o.StartDate);
                ProjectListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void ProjectListGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                string projectId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                string projectManagerId = (e.Item as GridDataItem).GetDataKeyValue("ProjectManagerId").ToString();
                Response.Redirect($"DisplayProject.aspx?projectId={projectId}&projectManagerId={projectManagerId}");
            }
        }

        private void Init_ProjectType()
        {
            List<int> ProjectTypeIdList = new List<int>();

            ProjectTypeIdList.Add((int)EnumProjectType.Duzenlenen);
            ProjectTypeIdList.Add((int)EnumProjectType.Gidilen);

            var projectTypeList = EnumHelper.GetEnumAsDictionary(typeof(EnumProjectType));
            projectTypeList = projectTypeList.Where(p => ProjectTypeIdList.Contains(p.Key)).ToDictionary(t => t.Key, t => t.Value);

            foreach (var item in projectTypeList)
            {
                ProjectType.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

        private void Init_UserStatus()
        {
            var projectStatusList = EnumHelper.GetEnumAsDictionary(typeof(EnumProjectStatusType));

            foreach (var item in projectStatusList)
            {
                ProjectStatus.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

    }
}