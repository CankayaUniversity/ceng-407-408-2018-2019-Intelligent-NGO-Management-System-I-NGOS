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

namespace Ilkyar.Web
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            divNotResetedPassword.Visible = false;
            try
            {
                var updatedUser = new UpdateUserDTO();

                if (!string.IsNullOrWhiteSpace(textBoxPassword.Text) && !string.IsNullOrWhiteSpace(textBoxPasswordAgain.Text))
                {
                    if (textBoxPassword.Text != textBoxPasswordAgain.Text)
                        throw new Exception("Şifre bilgisi uyuşmuyor!");

                    updatedUser.Username = textBoxUsername.Text;
                    updatedUser.Password = textBoxPassword.Text;
                }
                ServiceResult<UserDTO> serviceResult = new ServiceResult<UserDTO>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProfileApiUrl, "UpdatePassword", queryString, updatedUser);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<UserDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Şifreniz güncellendi.";
                textBoxUsername.Text = "";
                textBoxPassword.Text = "";
                textBoxPasswordAgain.Text = "";
                labelErrorMessage.Visible = true;
                divNotResetedPassword.Visible = true;

            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }
    }
}