using Ilkyar.Contracts.Entities;
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
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class Inbox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Init_UserList();
            }
        }

        private void Init_UserList()
        {
            UserList.Items.Clear();

            try
            {
                ServiceResult<List<UserDTO>> serviceResult = new ServiceResult<List<UserDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.UserApiUrl, "GetUserList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<UserDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                foreach (var item in serviceResult.Result.OrderBy(o => o.FirstName).ThenBy(o => o.LastName).ToList())
                {
                    if (item.UserId != UserHelper.CurrentUser.UserId)
                        UserList.Items.Add(new DropDownListItem($"{item.FirstName} {item.LastName} ({item.UserType})", item.UserId.ToString()));
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void UserList_ItemSelected(object sender, DropDownListEventArgs e)
        {
            GetConversationList();
        }

        private void GetConversationList()
        {
            if (UserList.SelectedIndex != -1)
                divConversation.Visible = true;
            else
                divConversation.Visible = false;

            long currentUserId = UserHelper.CurrentUser.UserId;
            long selectedUserId = Convert.ToInt64(UserList.SelectedValue);

            try
            {
                ServiceResult<List<ConversationDTO>> serviceResult = new ServiceResult<List<ConversationDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("currentUserId", currentUserId.ToString());
                queryString.Add("userId", selectedUserId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.MessageApiUrl, "GetConversationList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ConversationDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                ConversationList.DataSource = serviceResult.Result;
                ConversationList.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void buttonSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                AddConversationDTO newConversation = new AddConversationDTO()
                {
                    SenderId = UserHelper.UserId,
                    ReceiverId = Convert.ToInt64(UserList.SelectedValue),
                    Message = Message.Text
                };

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.MessageApiUrl, "CreateNewConversation", queryString, newConversation);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);
            }
            catch (Exception ex)
            {
            }

            Message.Text = null;
            GetConversationList();

        }

        protected void deleteMessage_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            long conversationId = Convert.ToInt64(linkButton.AccessKey);

            try
            {
                DeleteConversationDTO deleteConversation = new DeleteConversationDTO()
                {
                    ConversationId = conversationId
                };

                ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.MessageApiUrl, "DeleteConversation", queryString, deleteConversation);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                GetConversationList();
            }
            catch (Exception ex)
            {
            }
        }
    }
}