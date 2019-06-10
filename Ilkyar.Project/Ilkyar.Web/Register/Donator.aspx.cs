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

namespace Ilkyar.Web.Register
{
    public partial class Donator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                Init_OccupationDropDownList();
            }
        }

        protected void buttonCreateNewDonator_Click(object sender, EventArgs e)
        {
            try
            {
                var newDonator = new CreateNewDonatorDTO();

                if (!string.IsNullOrWhiteSpace(Donator_Password.Text) && !string.IsNullOrWhiteSpace(Donator_PasswordAgain.Text))
                {
                    if (Donator_Password.Text != Donator_PasswordAgain.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");
                }

                newDonator.UserTypeId = (int)EnumUserType.Donator;
                newDonator.FirstName = Donator_FirstName.Text;
                newDonator.LastName = Donator_LastName.Text;
                newDonator.BirthDate = Donator_BirthDate.SelectedDate.Value;
                newDonator.PhoneNum = Donator_Phone.Text;
                newDonator.Username = Donator_TCKN.Text;
                newDonator.Email = Donator_Email.Text;
                newDonator.Password = Donator_Password.Text;
                newDonator.OccupationId = Convert.ToInt32(Donator_Occupation.SelectedValue);

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.AccountApiUrl, "CreateNewDonator", queryString, newDonator);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Bağışçı üyeliği oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }

        private void Init_OccupationDropDownList()
        {
            try
            {
                ServiceResult<List<OccupationDTO>> serviceResult = new ServiceResult<List<OccupationDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetOccupationList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<OccupationDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            Donator_Occupation.Items.Add(new Telerik.Web.UI.DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}