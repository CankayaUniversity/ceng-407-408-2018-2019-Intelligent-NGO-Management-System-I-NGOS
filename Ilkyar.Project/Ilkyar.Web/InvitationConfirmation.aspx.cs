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
    public partial class InvitationConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.NGOHead)
                Response.Redirect("Home.aspx");

            if (!Page.IsPostBack)
            {
            }

            GetNGOInvitationList();
        }

        private List<NGOInvitationDTO> GetNGOInvitationList()
        {
            List<NGOInvitationDTO> result = new List<NGOInvitationDTO>();

            try
            {
                ServiceResult<List<NGOInvitationDTO>> serviceResult = new ServiceResult<List<NGOInvitationDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetNGOInvitationList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<NGOInvitationDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                InvitationGridList.DataSource = serviceResult.Result;
                InvitationGridList.DataBind();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        protected void InvitationGridList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                string invitationId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                UpdateNGOInvitation(Convert.ToInt64(invitationId), (int)EnumInvitationStatusType.Onaylandi);
            }
            if (e.CommandName == "Reject")
            {
                string invitationId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                UpdateNGOInvitation(Convert.ToInt64(invitationId), (int)EnumInvitationStatusType.Reddedildi);
            }
        }

        private void UpdateNGOInvitation(long invitationId, int statusId)
        {            
            try
            {
                UpdateNGOInvitationDTO updatedNGOInvitation = new UpdateNGOInvitationDTO()
                {
                    Id = Convert.ToInt64(invitationId),
                    StatusId = statusId
                };

                ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "UpdateNGOInvitation", queryString, updatedNGOInvitation);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                GetNGOInvitationList();
            }
            catch (Exception ex)
            {
            }
        }
    }
}