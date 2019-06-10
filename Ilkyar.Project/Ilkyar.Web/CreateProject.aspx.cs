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
    public partial class CreateProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.NGOHead && UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.ProjectManager)
                Response.Redirect("Home.aspx");

            labelErrorMessage.Visible = false;
            StartDate.MinDate = DateTime.Today;
            EndDate.MinDate = DateTime.Today;
            
            if (!Page.IsPostBack)
            {
                Init_ProjectTypeList();
                Init_ProjectManagerList();
            }
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

        protected void buttonCreateNewProject_Click(object sender, EventArgs e)
        {
            try
            {
                CreateNewProjectDTO newProject = new CreateNewProjectDTO()
                {
                    ProjectTypeId = Convert.ToInt32(ProjectType.SelectedValue),
                    ProjectManagerId = Convert.ToInt32(ProjectManager.SelectedValue),
                    Name = ProjectName.Text,
                    StartDate = StartDate.SelectedDate.HasValue ? StartDate.SelectedDate.Value : DateTime.Today,
                    EndDate = EndDate.SelectedDate.HasValue ? EndDate.SelectedDate.Value : DateTime.Today,
                    StatusId = (int)EnumProjectStatusType.Aktif
                };

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "CreateNewProject", queryString, newProject);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Proje oluşturuldu.";
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