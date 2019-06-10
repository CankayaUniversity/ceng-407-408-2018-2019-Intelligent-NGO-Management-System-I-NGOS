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
    public partial class ViewReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int reportId = -1;

            if (!string.IsNullOrEmpty(Request.QueryString["reportId"]))
            {
                reportId = Convert.ToInt32(Request.QueryString["reportId"]);
            }
            else
                Response.Redirect("ListReport.aspx");

            GetReport(reportId);
        }

        private void GetReport(int reportId)
        {
            try
            {
                ServiceResult<ReportDTO> serviceResult = new ServiceResult<ReportDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("reportId", reportId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ReportApiUrl, "GetReport", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<ReportDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var report = serviceResult.Result;

                Report_Date.Text = report.ReportDate.ToString("dd.MM.yyyy");
                Report_Subject.Text = report.Subject;
                Report_YonDerName.Text = report.YonDerName;
                Report_ScholarshipHolder.Text = report.ScholarshipHolderName;
                Report_Text.Content = report.ReportText;
            }
            catch (Exception ex)
            {
                Response.Redirect("ListReport.aspx");
            }
        }
    }
}


