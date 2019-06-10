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
    public partial class VoteVolunteer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.Student)
                Response.Redirect("Home.aspx");

            if (!Page.IsPostBack)
            {
                InitProjectDetailList();
            }
        }

        private void InitProjectDetailList()
        {
            ProjectDetailName.Items.Clear();

            try
            {
                ServiceResult<List<VoteVolunteerProjectDetailDTO>> serviceResult = new ServiceResult<List<VoteVolunteerProjectDetailDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("userId", UserHelper.CurrentUser.UserId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetVoteVolunteerProjectDetailList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<VoteVolunteerProjectDetailDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                foreach (var item in serviceResult.Result)
                {
                    ProjectDetailName.Items.Add(new DropDownListItem { Text = item.ProjectDetailName, Value = item.ProjectDetailId.ToString() });
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ProjectDetailName_ItemSelected(object sender, DropDownListEventArgs e)
        {
            GetVolunteerEvaluationList();
        }

        private void GetVolunteerEvaluationList()
        {
            long projectDetailId = Convert.ToInt64(ProjectDetailName.SelectedValue);

            try
            {
                ServiceResult<List<EvaluateVolunteerDTO>> serviceResult = new ServiceResult<List<EvaluateVolunteerDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectDetailId", projectDetailId.ToString());
                queryString.Add("userId", UserHelper.CurrentUser.UserId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetEvaluateVolunteerList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<EvaluateVolunteerDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                EvaluationVolunteerListGrid.DataSource = serviceResult.Result;
                EvaluationVolunteerListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void RateVolunteer_Rate(object sender, EventArgs e)
        {
            RadRating oRating = (RadRating)sender;

            int activityId = Convert.ToInt32(oRating.DataModelID);
            long volunteerId = Convert.ToInt64(oRating.AccessKey);
            long projectDetailId = Convert.ToInt64(ProjectDetailName.SelectedValue);
            long userId = UserHelper.CurrentUser.UserId;
            short vote = Convert.ToInt16(oRating.Value);

            var newAddVolunteerVoteDTO = new AddVolunteerVoteDTO
            {
                ProjectDetailId = projectDetailId,
                UserId = userId,
                VolunteerId = volunteerId,
                ActivityId = activityId,
                Vote = vote
            };

            try
            {
                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "AddNewVolunteerVote", queryString, newAddVolunteerVoteDTO);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                GetVolunteerEvaluationList();
            }
            catch (Exception ex)
            {
            }
        }
    }
}