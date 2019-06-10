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
    public partial class CreateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.YonDer)
                Response.Redirect("Home.aspx");

            labelErrorMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                Init_ScholarshipHolderList();
            }
        }

        private void Init_ScholarshipHolderList()
        {
            try
            {
                ServiceResult<List<ScholarshipHolderDTO>> serviceResult = new ServiceResult<List<ScholarshipHolderDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetScholarshipHolderList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ScholarshipHolderDTO>>>(data);

                if (serviceResult.ServiceResultType == EnumServiceResultType.Success)
                {
                    if (serviceResult.Result.Any())
                    {
                        foreach (var item in serviceResult.Result)
                        {
                            Report_ScholarshipHolder.Items.Add(new DropDownListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void buttonSaveReport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateNewReportDTO newReport = new CreateNewReportDTO()
                {
                    YonDerId = UserHelper.CurrentUser.Id,
                    ScholarshipHolderId = Convert.ToInt32(Report_ScholarshipHolder.SelectedValue),
                    Subject = Report_Subject.Text,
                    ReportDate = Report_Date.SelectedDate.HasValue ? Report_Date.SelectedDate.Value : DateTime.Today,
                    ReportText = HttpUtility.HtmlDecode(Report_Text.Content)
                };

                ServiceResult<long> serviceResult = new ServiceResult<long>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ReportApiUrl, "CreateNewReport", queryString, newReport);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                labelErrorMessage.Text = "Rapor oluşturuldu.";
                labelErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                labelErrorMessage.Text = ex.Message;
                labelErrorMessage.Visible = true;
            }
        }
    }
}