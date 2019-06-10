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

namespace Ilkyar.Web
{
    public partial class DisplayProjectDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                long projectId = -1;
                long projectDetailId = -1;

                if (!string.IsNullOrEmpty(Request.QueryString["projectId"]))
                {
                    projectId = Convert.ToInt64(Request.QueryString["projectId"]);
                    ProjectId.Value = projectId.ToString();
                    GetProject(projectId);
                }
                else
                {
                    Response.Redirect("ListProject.aspx");
                }

                if (!string.IsNullOrEmpty(Request.QueryString["projectDetailId"]))
                {
                    projectDetailId = Convert.ToInt64(Request.QueryString["projectDetailId"]);
                    ProjectDetailId.Value = projectDetailId.ToString();
                    GetProjectSubDetail(projectId, projectDetailId);
                    GetProjectActivityList(projectId, projectDetailId);
                }



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
                TextBoxProjecStartDate.Text = project.StartDate.ToString("dd.MM.yyyy");
                TextBoxProjectEndDate.Text = project.EndDate.ToString("dd.MM.yyyy");
                TextBoxProjectStatus.Text = project.StatusName;
                ProjectManagerId.Value = Convert.ToInt64(project.ProjectManagerId).ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("ListProject.aspx");
            }
        }

        private void GetCity(int cityId)
        {
            try
            {
                ServiceResult<CityDTO> serviceResult = new ServiceResult<CityDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("cityId", cityId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetCity", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<CityDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var city = serviceResult.Result;

                RadDropDownListCity.Text = city.Name;

            }
            catch (Exception ex)
            {
                Response.Redirect("ListProject.aspx");
            }
        }

        private void GetTown(int cityId, int townId)
        {
            try
            {
                ServiceResult<TownDTO> serviceResult = new ServiceResult<TownDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("cityId", cityId.ToString());
                queryString.Add("townId", townId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetTown", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<TownDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var town = serviceResult.Result;

                RadDropDownListTown.Text = town.Name;

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
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<ProjectSubDetailDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var project = serviceResult.Result;

                GetCity(project.CityId);
                GetTown(project.CityId, project.TownId);
                TextBoxSchool.Text = project.School;
                TextBoxProjectDetailStartDate.Text = project.DetailStartDate.ToString("dd.MM.yyyy");
                TextBoxProjectDetailEndDate.Text = project.DetailEndDate.ToString("dd.MM.yyyy");
                TextBoxDepartureDate.Text = project.TrnsStartDate.ToString("dd.MM.yyyy");
                DepartureTransportationType.Text = EnumHelper.GetEnumDescription(typeof(EnumTransportationType), project.TransportationTypeId.ToString());
                DeparturePoint.Text = project.Departure;
                TextBoxMaterial.Text = project.ReqText;
                DepartureTransportationTypeInfo.Text = project.DepartureFirm;
                DepartureNumberOfPeople.Text = project.TrnsNumOfPeople.ToString();
                TextBoxArrivalDate.Text = project.TrnsEndDate.ToString("dd.MM.yyyy");
                ArrivalTransportationType.Text = EnumHelper.GetEnumDescription(typeof(EnumTransportationType), project.ArrivalTransportationTypeId.ToString());
                ArrivalPoint.Text = project.Comeback;
                ArrivalTansportationTypeInfo.Text = project.ComebackFirm;
                ArrivalNumberOfPeople.Text = project.TrnsArrNumOfPeople.ToString();
                TextBoxAccomodation.Text = project.Inn;
                TextBoxNumOfPep.Text = project.AccNumOfPeople.ToString();
                TextAreaNote.Text = project.ProjectInfo;
            }
            catch (Exception ex)
            {
                Response.Redirect("ListProject.aspx");
            }
        }

        private List<ProjectActivityDTO> GetProjectActivityList(long projectId, long projectDetailId)
        {
            List<ProjectActivityDTO> result = new List<ProjectActivityDTO>();

            try
            {
                ServiceResult<List<ProjectActivityDTO>> serviceResult = new ServiceResult<List<ProjectActivityDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectId", projectId.ToString());
                queryString.Add("projectDetailId", projectDetailId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProjectActivityList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectActivityDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                ProjectActivityGrid.DataSource = serviceResult.Result;
                ProjectActivityGrid.DataBind();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        protected void LinkButtonBack_Click(object sender, EventArgs e)
        {
            string projectId = ProjectId.Value;
            string projectManagerId = ProjectManagerId.Value;
            Response.Redirect($"DisplayProject.aspx?projectId={projectId}&projectManagerId={projectManagerId}");
        }

    }
}