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
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class ListUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Init_UserType();
                Init_UserStatus();
                FilterUserList();
            }
        }

        private void Init_UserType()
        {
            List<int> availableUserTypeIdList = new List<int>();

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                availableUserTypeIdList.Add((int)EnumUserType.Donator);
                availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
                availableUserTypeIdList.Add((int)EnumUserType.Volunteer);
                availableUserTypeIdList.Add((int)EnumUserType.YonDer);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
                availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
                availableUserTypeIdList.Add((int)EnumUserType.Volunteer);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ScholarshipCommittee)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                availableUserTypeIdList.Add((int)EnumUserType.Donator);
                availableUserTypeIdList.Add((int)EnumUserType.YonDer);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ScholarshipHolder)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                availableUserTypeIdList.Add((int)EnumUserType.YonDer);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Donator)
            {

            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Schoolmaster)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.HostSchoolTeacher)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.Schoolmaster);
                availableUserTypeIdList.Add((int)EnumUserType.HostSchoolTeacher);
                availableUserTypeIdList.Add((int)EnumUserType.Student);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Student)
            {

            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Volunteer)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ProjectManager);
            }

            if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.YonDer)
            {
                availableUserTypeIdList.Add((int)EnumUserType.NGOHead);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipCommittee);
                availableUserTypeIdList.Add((int)EnumUserType.ScholarshipHolder);
                availableUserTypeIdList.Add((int)EnumUserType.Donator);
            }


            var userTypeList = EnumHelper.GetEnumAsDictionary(typeof(EnumUserType));
            userTypeList = userTypeList.Where(p => availableUserTypeIdList.Contains(p.Key)).ToDictionary(t => t.Key, t => t.Value);

            foreach (var item in userTypeList)
            {
                UserType.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

        private void Init_UserStatus()
        {
            var userStatusList = EnumHelper.GetEnumAsDictionary(typeof(EnumUserStatus));

            foreach (var item in userStatusList)
            {
                Status.Items.Add(new DropDownListItem(item.Value, item.Key.ToString()));
            }
        }

        protected void buttonFilterUserList_Click(object sender, EventArgs e)
        {
            FilterUserList();
        }

        protected void buttonClearFilter_Click(object sender, EventArgs e)
        {
            UserType.SelectedIndex = -1;
            UserName.Text = null;
            FirstName.Text = null;
            LastName.Text = null;
            Email.Text = null;
            Phone.Text = null;
            BirthDate.SelectedDate = null;
            Status.SelectedIndex = -1;

            FilterUserList();
        }

        private void FilterUserList()
        {
            try
            {
                var filter = new UserFilterDTO();

                filter.CurrentUserTypeId = UserHelper.CurrentUser.UserTypeId;

                if (UserType.SelectedIndex != -1)
                    filter.UserTypeId = Convert.ToInt32(UserType.SelectedValue);

                filter.UserName = UserName.Text;
                filter.FirstName = FirstName.Text;
                filter.LastName = LastName.Text;
                filter.Email = Email.Text;
                filter.Phone = Phone.Text;
                filter.BirthDate = BirthDate.SelectedDate;

                if (Status.SelectedIndex != -1)
                    filter.Status = Convert.ToInt32(Status.SelectedValue);

                ServiceResult<List<UserDTO>> serviceResult = new ServiceResult<List<UserDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.UserApiUrl, "GetUserList", queryString, filter);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<UserDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                UsersListGrid.DataSource = serviceResult.Result;
                UsersListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void UserListGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string userId = (e.Item as GridDataItem).GetDataKeyValue("UserId").ToString();
                Response.Redirect($"ViewUser.aspx?userId={userId}");
            }
        }

    }
}