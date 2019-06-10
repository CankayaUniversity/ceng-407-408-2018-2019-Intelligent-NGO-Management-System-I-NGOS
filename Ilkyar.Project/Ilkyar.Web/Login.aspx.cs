using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;

namespace Ilkyar.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly static string webApiBaseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];

        protected void Page_Load(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                if (Request.Cookies["Username"] != null && Request.Cookies["Password"] != null)
                {
                    textBoxUsername.Text = Request.Cookies["Username"].Value;
                    textBoxPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                }

                if (Session["CurrentUser"] != null)
                {
                    var currentUser = Session["CurrentUser"].ToString();

                    if (!string.IsNullOrEmpty(currentUser))
                    {
                        Response.Redirect("Home.aspx");
                    }
                }
            }
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceResult<UserDTO> serviceResult = new ServiceResult<UserDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("username", textBoxUsername.Text);
                queryString.Add("password", textBoxPassword.Text);
                var response = ApiHelper.CallGetApiMethod(ApiKeys.AccountApiUrl, "Login", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<UserDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                if (RememberMe.Checked == true)
                {
                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies["Username"].Value = textBoxUsername.Text.Trim();
                Response.Cookies["Password"].Value = textBoxPassword.Text.Trim();
                Session["CurrentUser"] = serviceResult.Result;
                Response.Redirect("Home.aspx");
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }
    }
}