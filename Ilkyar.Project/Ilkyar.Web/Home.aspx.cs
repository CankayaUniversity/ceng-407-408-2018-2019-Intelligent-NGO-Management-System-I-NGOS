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
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead)
                {
                    Init_DashboardInfo();
                    Init_LeadershipBoardListGrid();
                }

                if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager)
                {
                    Init_ProjectManagerBadge();
                }

                if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Volunteer)
                {
                    Init_ProjectDetailSuggestionList();
                    Init_VolunteerBadge();

                    var leadershipBoardList = GetLeadershipBoardList();

                    if (leadershipBoardList.Any(p => p.VolunteerId == UserHelper.CurrentUser.Id))
                    {
                        var leadershipBoard = leadershipBoardList.Single(p => p.VolunteerId == UserHelper.CurrentUser.Id); 

                        if (leadershipBoard.OrderNumber == 1)
                        {
                            LeadershipBoardInfo.Text = $"Liderlik sıralamasında {leadershipBoard.OrderNumber}. sıradasınız.";
                        }
                        else if (leadershipBoard.OrderNumber > 1)
                        {

                            LeadershipBoardInfo.Text = $"Liderlik sıralamasında {leadershipBoard.OrderNumber}. sıradasınız. Birinci olmak için yeni projelerimize göz atmak ister misiniz?";
                        }
                    }


                }
            }
        }

        private void Init_LeadershipBoardListGrid()
        {
            var leadershipBoardList = GetLeadershipBoardList();

            LeadershipBoardListGrid.DataSource = leadershipBoardList;
            LeadershipBoardListGrid.DataBind();
        }

        private void Init_DashboardInfo()
        {
            try
            {
                ServiceResult<DashboardDTO> serviceResult = new ServiceResult<DashboardDTO>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetDashboardInfo", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<DashboardDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                TotalProjectDetailCount.InnerText = serviceResult.Result.TotalProjectCount.ToString();
                TotalProjectDetailCompletedCount.InnerText = serviceResult.Result.TotalProjectCompletedCount.ToString();
                TotalProjectDetailActiveCount.InnerText = serviceResult.Result.TotalProjectActiveCount.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private List<LeadershipBoardDTO> GetLeadershipBoardList()
        {
            List<LeadershipBoardDTO> result = new List<LeadershipBoardDTO>();

            try
            {
                ServiceResult<List<LeadershipBoardDTO>> serviceResult = new ServiceResult<List<LeadershipBoardDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetLeadershipBoardList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<LeadershipBoardDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                result = serviceResult.Result;
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        private void Init_VolunteerBadge()
        {
            try
            {
                ActivityLeadershipBadge.Visible = false;
                GeniusBadge.Visible = false;
                BronzeLeadershipBadge.Visible = false;
                SilverLeadershipBadge.Visible = false;
                GoldLeadershipBadge.Visible = false;
                LabelNeededForBeeBadge.Visible = false;
                BeeBadgePanel.Visible = false;

                ServiceResult<VolunteerBadgeDTO> serviceResult = new ServiceResult<VolunteerBadgeDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("volunteerId", UserHelper.CurrentUser.Id.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.AccountApiUrl, "GetVolunteerBadge", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<VolunteerBadgeDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                VolunteerBadgeList.DataSource = serviceResult.Result.ActivityLeadershipBadgeList;
                VolunteerBadgeList.DataBind();
                GenuisBadgeList.DataSource = serviceResult.Result.GeniusBadgeList;
                GenuisBadgeList.DataBind();

                if (serviceResult.Result.ActivityLeadershipBadgeList.Count==0 && serviceResult.Result.GeniusBadgeList.Count == 0)
                {
                    VolunteerBadgeList.Visible = false;
                    GenuisBadgeList.Visible = false;
                    volunteerBadgeInfo.Visible = true;
                }

                if (serviceResult.Result.ActivityLeadershipBadgeList.Count != 0)
                    ActivityLeadershipBadge.Visible = true;
                if (serviceResult.Result.GeniusBadgeList.Count != 0)
                    GeniusBadge.Visible = true;
                if (serviceResult.Result.IsBronzeActivityLeadershipBadge)
                    BronzeLeadershipBadge.Visible = true;
                if (serviceResult.Result.IsSilverActivityLeadershipBadge)
                    SilverLeadershipBadge.Visible = true;
                if (serviceResult.Result.IsGoldActivityLeadershipBadge)
                    GoldLeadershipBadge.Visible = true;
                if (serviceResult.Result.IsBee)
                    BeeBadgePanel.Visible = true;
                else 
                {
                    LabelNeededForBeeBadge.Visible = true;
                    LabelNeededForBeeBadge.Text = serviceResult.Result.NeededForBeeBadge + " aktiviteye daha katılıp Arı Rozeti kazanmaya ne dersin?";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Init_ProjectManagerBadge()
        {
            try
            {
                ServiceResult<ProjectManagerBadgeDTO> serviceResult = new ServiceResult<ProjectManagerBadgeDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectManagerId", UserHelper.CurrentUser.Id.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.AccountApiUrl, "GetProjectManagerBadge", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<ProjectManagerBadgeDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                //ProjectManagerBadgeList.DataSource = serviceResult.Result.ProjectExperienceBadgeList;
                //ProjectManagerBadgeList.DataBind();
               
                IsHighestVoteBadge.Visible = false;
                if (serviceResult.Result.ProjectExperienceBadgeList.Count==0)
                {
                    BadgeInfo.Visible = true;
                }

                if (serviceResult.Result.IsHighestVoteBadge)
                    IsHighestVoteBadge.Visible = true;

                if (serviceResult.Result.ProjectExperienceBadgeList.Count < 3)
                {
                    ProjectManagerSilverBadge.Visible = false;
                    ProjectManagerBronzeBadge.Visible = false;
                    ProjectManagerGoldenBadge.Visible = false;
                }
                else if (serviceResult.Result.ProjectExperienceBadgeList.Count >= 3 && serviceResult.Result.ProjectExperienceBadgeList.Count < 7)
                {
                    ProjectManagerSilverBadge.Visible = false;
                    ProjectManagerBronzeBadge.Visible = true;
                    ProjectManagerGoldenBadge.Visible = false;
                }
                else if (serviceResult.Result.ProjectExperienceBadgeList.Count >= 7 && serviceResult.Result.ProjectExperienceBadgeList.Count < 12)
                {
                    ProjectManagerSilverBadge.Visible = true;
                    ProjectManagerBronzeBadge.Visible = true;
                    ProjectManagerGoldenBadge.Visible = false;
                }
                else
                {
                    ProjectManagerSilverBadge.Visible = true;
                    ProjectManagerBronzeBadge.Visible = true;
                    ProjectManagerGoldenBadge.Visible = true;
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void Init_ProjectDetailSuggestionList()
        {
            try
            {
                ServiceResult<List<ProjectDetailSuggestionDTO>> serviceResult = new ServiceResult<List<ProjectDetailSuggestionDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("volunteerId", UserHelper.CurrentUser.Id.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProjectDetailSuggestionList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectDetailSuggestionDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                SuggestedProjectDetailListGrid.DataSource = serviceResult.Result;
                SuggestedProjectDetailListGrid.DataBind();

                if (serviceResult.Result.Count==0)
                {
                    SuggestedProjectDetailListGrid.Visible = false;
                    projectSuggestionInfo.Visible = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void SuggestedProjectDetailListGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                string projectId = (e.Item as GridDataItem).GetDataKeyValue("ProjectId").ToString();
                string suggestedProjectDetailId = (e.Item as GridDataItem).GetDataKeyValue("ProjectDetailId").ToString();
                Response.Redirect($"DisplayProjectDetail.aspx?projectId={projectId}&projectDetailId={suggestedProjectDetailId}");
            }

            if (e.CommandName == "Select")
            {
                string projectId = (e.Item as GridDataItem).GetDataKeyValue("ProjectId").ToString();
                string suggestedProjectDetailId = (e.Item as GridDataItem).GetDataKeyValue("ProjectDetailId").ToString();
                Response.Redirect($"ProjectDetailActivity.aspx?projectId={projectId}&projectDetailId={suggestedProjectDetailId}");
            }
        }
    }
}