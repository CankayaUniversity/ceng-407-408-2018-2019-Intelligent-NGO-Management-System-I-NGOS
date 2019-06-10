using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.EF;
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
    public partial class ListCompletedProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FilterProjectList();
            }
        }
        private void FilterProjectList()
        {
            try
            {
                var filter = new ProjectFilterDTO();

                filter.Status = Convert.ToInt32(EnumProjectStatusType.Tamamlandi);

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

                CompletedProjectListGrid.DataSource = serviceResult.Result.OrderByDescending(o => o.StartDate);
                CompletedProjectListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }


        protected void CompletedProjectListGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
         if (e.CommandName == "Detail")
                    {
                        string projectId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                        string projectManagerId = (e.Item as GridDataItem).GetDataKeyValue("ProjectManagerId").ToString();
                        Response.Redirect($"DisplayProject.aspx?projectId={projectId}&projectManagerId={projectManagerId}");
                    }
                }
    }

}