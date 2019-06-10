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
    public partial class VoteProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.Schoolmaster && UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.HostSchoolTeacher)
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
                ServiceResult<List<VoteProjectProjectDetailDTO>> serviceResult = new ServiceResult<List<VoteProjectProjectDetailDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("userId", UserHelper.CurrentUser.UserId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetVoteProjectProjectDetailList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<VoteProjectProjectDetailDTO>>>(data);

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
            GetSurveyProjectDetailQuestionList();
        }

        private void GetSurveyProjectDetailQuestionList()
        {
            long projectDetailId = Convert.ToInt64(ProjectDetailName.SelectedValue);

            try
            {
                ServiceResult<List<SurveyProjectDetailQuestionDTO>> serviceResult = new ServiceResult<List<SurveyProjectDetailQuestionDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectDetailId", projectDetailId.ToString());
                queryString.Add("userId", UserHelper.CurrentUser.UserId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetSurveyProjectDetailQuestionList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<SurveyProjectDetailQuestionDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                SurveyProjectDetailQuestionList.DataSource = serviceResult.Result;
                SurveyProjectDetailQuestionList.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void RateProjectDetail_Rate(object sender, EventArgs e)
        {
            RadRating oRating = (RadRating)sender;

            int questionId = Convert.ToInt32(oRating.AccessKey);
            long projectDetailId = Convert.ToInt64(ProjectDetailName.SelectedValue);
            long userId = UserHelper.CurrentUser.UserId;
            short vote = Convert.ToInt16(oRating.Value);

            var newSurveyProjectDetailQuestion = new AddSurveyProjectDetailQuestionDTO
            {
                ProjectDetailId = projectDetailId,
                UserId = userId,
                SurveyProjectDetailQuestionId = questionId,
                Vote = vote
            };

            try
            {
                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "AddNewSurveyProjectDetailQuestion", queryString, newSurveyProjectDetailQuestion);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                GetSurveyProjectDetailQuestionList();
            }
            catch (Exception ex)
            {
            }
        }
    }
}