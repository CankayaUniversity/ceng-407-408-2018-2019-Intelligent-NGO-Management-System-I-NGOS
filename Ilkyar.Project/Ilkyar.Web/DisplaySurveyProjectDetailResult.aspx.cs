using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
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
    public partial class DisplaySurveyProjectDetailResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.NGOHead)
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

                InitProjectDetailList(projectId, projectDetailId);
            }
        }

        private void InitProjectDetailList(long projectId, long projectDetailId)
        {
           
            try
            {
                ServiceResult<List<SurveyProjectDetailResultDTO>> serviceResult = new ServiceResult<List<SurveyProjectDetailResultDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectId", projectId.ToString());
                queryString.Add("projectDetailId", projectDetailId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetSurveyProjectDetailResultList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<SurveyProjectDetailResultDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                SurveyProjectDetailResultGrid.DataSource = serviceResult.Result;
                SurveyProjectDetailResultGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

      
    }
}