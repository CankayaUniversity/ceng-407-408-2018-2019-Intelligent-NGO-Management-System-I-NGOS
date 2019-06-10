using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class ListReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FilterReportList();
            }
        }

        protected void buttonFilterReportList_Click(object sender, EventArgs e)
        {
            FilterReportList();
        }

        protected void buttonClearFilter_Click(object sender, EventArgs e)
        {
            ReportDate.SelectedDate = null;
            YonderName.Text = null;
            ScholarshipHolderName.Text = null;
            Subject.Text = null;

            FilterReportList();
        }

        private void FilterReportList()
        {
            try
            {
                var filter = new ReportFilterDTO();

                filter.YonDerName = YonderName.Text;
                filter.ScholarshipHolderName = ScholarshipHolderName.Text;
                filter.Subject = Subject.Text;
                filter.ReportDate = ReportDate.SelectedDate;

                ServiceResult<List<ReportDTO>> serviceResult = new ServiceResult<List<ReportDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ReportApiUrl, "GetReportList", queryString, filter);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ReportDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                ReportListGrid.DataSource = serviceResult.Result;
                ReportListGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void ReportListGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string reportId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                Response.Redirect($"ViewReport.aspx?reportId={reportId}");
            }
        }


        //protected void ReportListGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        var filter = new ReportFilterDTO();

        //        ServiceResult<List<ReportDTO>> serviceResult = new ServiceResult<List<ReportDTO>>();
        //        var queryString = new Dictionary<string, string>();
        //        var response = ApiHelper.CallSendApiMethod(ApiKeys.ReportApiUrl, "GetReportList", queryString, filter);
        //        if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
        //        var data = response.Content.ReadAsStringAsync().Result;
        //        serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ReportDTO>>>(data);

        //        if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
        //            throw new Exception(serviceResult.ErrorMessage);

        //        if (serviceResult.Result == null)
        //            throw new Exception(serviceResult.ErrorMessage);

        //        ReportListGrid.DataSource = serviceResult.Result;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

    }
}