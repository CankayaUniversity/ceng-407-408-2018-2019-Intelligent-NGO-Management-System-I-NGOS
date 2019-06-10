using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Ilkyar.Web
{
    public partial class ProjectSchedule : System.Web.UI.Page
    {
        private static List<AppointmentInfo> AppointmentList { get; set; }
        private static List<ProjectScheduleProjectDetailDTO> ProjectScheduleProjectDetailList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitProjectDetailList();
            }

            if (ProjectDetailName.SelectedIndex != -1)
                ScheduleList.Visible = true;
            else
                ScheduleList.Visible = false;


        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        private void InitProjectDetailList()
        {
            ProjectDetailName.Items.Clear();

            try
            {
                ServiceResult<List<ProjectScheduleProjectDetailDTO>> serviceResult = new ServiceResult<List<ProjectScheduleProjectDetailDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("userId", UserHelper.CurrentUser.UserId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProjectScheduleProjectDetailList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectScheduleProjectDetailDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                ProjectScheduleProjectDetailList = new List<ProjectScheduleProjectDetailDTO>();
                ProjectScheduleProjectDetailList.AddRange(serviceResult.Result);

                foreach (var item in serviceResult.Result)
                {
                    ProjectDetailName.Items.Add(new DropDownListItem { Text = item.ProjectDetailName, Value = item.ProjectDetailId.ToString() });
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void InitializeAppointments()
        {
            AppointmentList = new List<AppointmentInfo>();

            try
            {
                ServiceResult<List<ProjectDetailActivityScheduleDTO>> serviceResult = new ServiceResult<List<ProjectDetailActivityScheduleDTO>>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("projectDetailId", ProjectDetailName.SelectedValue);
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ProjectApiUrl, "GetProjectDetailScheduleList", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectDetailActivityScheduleDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                foreach (var item in serviceResult.Result)
                {
                    var newAppointment = new AppointmentInfo();
                    newAppointment.ID = item.ProjectDetailActivityScheduleId.ToString();
                    newAppointment.Start = item.StartDate;
                    newAppointment.End = item.EndDate;
                    newAppointment.Subject = item.SummaryInfo;
                    newAppointment.Resources = new ResourceCollection();
                    newAppointment.Resources.Add(new Resource("AppointmentInfo", "ProjectDetailActivityScheduleId", item.ProjectDetailActivityScheduleId.ToString()));
                    newAppointment.Resources.Add(new Resource("AppointmentInfo", "ProjectDetailActivityId", item.ProjectDetailActivityId.ToString()));
                    AppointmentList.Add(newAppointment);
                }
            }
            catch (Exception ex)
            {
            }

            ScheduleList.DataSource = AppointmentList;
        }

        private List<ProjectDetailActivityDTO> GetActivityList()
        {
            List<ProjectDetailActivityDTO> result = new List<ProjectDetailActivityDTO>();

            try
            {
                var filter = new ProjectDetailActivityFilterDTO();
                filter.ProjectDetailId = Convert.ToInt64(ProjectDetailName.SelectedValue);

                ServiceResult<List<ProjectDetailActivityDTO>> serviceResult = new ServiceResult<List<ProjectDetailActivityDTO>>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "GetProjectDetailActivityList", queryString, filter);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<List<ProjectDetailActivityDTO>>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                result = serviceResult.Result.Where(p => p.StatusId == (int)EnumActivityStatusType.Onaylandi).ToList();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        protected void ProjectDetailName_ItemSelected(object sender, DropDownListEventArgs e)
        {
            SelectedProjectDetailId.Value = ProjectDetailName.SelectedValue;

            InitializeAppointments();
        }

        protected void ScheduleList_FormCreated(object sender, SchedulerFormCreatedEventArgs e)
        {
            if (e.Container.Mode == SchedulerFormMode.Insert)
            {
                RadDropDownList tempActivityList = (RadDropDownList)e.Container.FindControl("ActivityList");
                tempActivityList.Items.Clear();

                var activitylist = GetActivityList();

                foreach (var item in activitylist)
                {
                    tempActivityList.Items.Add(new DropDownListItem { Text = $"{item.VolunteerFullName} ({item.ActivityName})", Value = item.Id.ToString() });
                }
            }

            if (e.Container.Mode == SchedulerFormMode.Edit)
            {
                RadDropDownList tempActivityList = (RadDropDownList)e.Container.FindControl("ActivityList");
                tempActivityList.Items.Clear();

                var activitylist = GetActivityList();

                foreach (var item in activitylist)
                {
                    tempActivityList.Items.Add(new DropDownListItem { Text = $"{item.VolunteerFullName} ({item.ActivityName})", Value = item.Id.ToString() });
                }

                var selectedAppointment = AppointmentList.Single(p => p.ID == e.Appointment.ID.ToString());

                if (selectedAppointment != null)
                {
                    var projectDetailActivity = selectedAppointment.Resources.SingleOrDefault(p => p.Key.ToString() == "ProjectDetailActivityId");

                    if (projectDetailActivity != null)
                    {
                        tempActivityList.SelectedValue = projectDetailActivity.Text;
                    }
                }

                RadTimePicker tempStartTime = (RadTimePicker)e.Container.FindControl("StartTime");
                tempStartTime.SelectedTime = new TimeSpan(e.Container.Appointment.Start.Hour, e.Container.Appointment.Start.Minute, e.Container.Appointment.Start.Second);

                RadTimePicker tempEndTime = (RadTimePicker)e.Container.FindControl("EndTime");
                tempEndTime.SelectedTime = new TimeSpan(e.Container.Appointment.End.Hour, e.Container.Appointment.End.Minute, e.Container.Appointment.End.Second);
            }
        }

        protected void ScheduleList_AppointmentCommand(object sender, AppointmentCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                RadDropDownList activityDropDownList = (RadDropDownList)e.Container.FindControl("ActivityList");

                RadTimePicker startTime = (RadTimePicker)e.Container.FindControl("StartTime");
                RadTimePicker endTime = (RadTimePicker)e.Container.FindControl("EndTime");

                var selectedDate = e.Container.Appointment.Start;

                DateTime startDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, startTime.SelectedTime.Value.Hours, startTime.SelectedTime.Value.Minutes, startTime.SelectedTime.Value.Seconds);
                DateTime endDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, endTime.SelectedTime.Value.Hours, endTime.SelectedTime.Value.Minutes, endTime.SelectedTime.Value.Seconds);

                var newProjectDetailActivitySchedule = new UpdateProjectDetailActivityScheduleDTO();
                newProjectDetailActivitySchedule.ProjectDetailId = Convert.ToInt64(ProjectDetailName.SelectedValue);
                newProjectDetailActivitySchedule.ProjectDetailActivityId = Convert.ToInt64(activityDropDownList.SelectedValue);
                newProjectDetailActivitySchedule.StartDate = startDateTime;
                newProjectDetailActivitySchedule.EndDate = endDateTime;
                newProjectDetailActivitySchedule.OperationTypeId = (int)EnumProjectDetailActivityScheduleOperationType.Insert;

                try
                {
                    ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                    var queryString = new Dictionary<string, string>();
                    var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "UpdateProjectDetailActivitySchedule", queryString, newProjectDetailActivitySchedule);
                    if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                    var data = response.Content.ReadAsStringAsync().Result;
                    serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                    if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                        throw new Exception(serviceResult.ErrorMessage);
                }
                catch (Exception ex)
                {
                }
            }

            if (e.CommandName == "Update")
            {
                RadDropDownList activityDropDownList = (RadDropDownList)e.Container.FindControl("ActivityList");

                RadTimePicker startTime = (RadTimePicker)e.Container.FindControl("StartTime");
                RadTimePicker endTime = (RadTimePicker)e.Container.FindControl("EndTime");

                var selectedDate = e.Container.Appointment.Start;

                DateTime startDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, startTime.SelectedTime.Value.Hours, startTime.SelectedTime.Value.Minutes, startTime.SelectedTime.Value.Seconds);
                DateTime endDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, endTime.SelectedTime.Value.Hours, endTime.SelectedTime.Value.Minutes, endTime.SelectedTime.Value.Seconds);

                var existingProjectDetailActivitySchedule = new UpdateProjectDetailActivityScheduleDTO();
                existingProjectDetailActivitySchedule.ProjectDetailActivityScheduleId = Convert.ToInt64(e.Container.Appointment.ID.ToString());
                existingProjectDetailActivitySchedule.ProjectDetailId = Convert.ToInt64(ProjectDetailName.SelectedValue);
                existingProjectDetailActivitySchedule.ProjectDetailActivityId = Convert.ToInt64(activityDropDownList.SelectedValue);
                existingProjectDetailActivitySchedule.StartDate = startDateTime;
                existingProjectDetailActivitySchedule.EndDate = endDateTime;
                existingProjectDetailActivitySchedule.OperationTypeId = (int)EnumProjectDetailActivityScheduleOperationType.Update;

                try
                {
                    ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                    var queryString = new Dictionary<string, string>();
                    var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "UpdateProjectDetailActivitySchedule", queryString, existingProjectDetailActivitySchedule);
                    if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                    var data = response.Content.ReadAsStringAsync().Result;
                    serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                    if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                        throw new Exception(serviceResult.ErrorMessage);
                }
                catch (Exception ex)
                {
                }
            }

            InitializeAppointments();
        }

        protected void ScheduleList_AppointmentDelete(object sender, AppointmentDeleteEventArgs e)
        {
            var existingProjectDetailActivitySchedule = new UpdateProjectDetailActivityScheduleDTO();
            existingProjectDetailActivitySchedule.ProjectDetailActivityScheduleId = Convert.ToInt64(e.Appointment.ID.ToString());
            existingProjectDetailActivitySchedule.OperationTypeId = (int)EnumProjectDetailActivityScheduleOperationType.Delete;

            try
            {
                ServiceResult<bool> serviceResult = new ServiceResult<bool>();
                var queryString = new Dictionary<string, string>();
                var response = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "UpdateProjectDetailActivitySchedule", queryString, existingProjectDetailActivitySchedule);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<bool>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);
            }
            catch (Exception ex)
            {
            }
        }

        protected void ScheduleList_FormCreating(object sender, SchedulerFormCreatingEventArgs e)
        {
            var selectedProjectDetailId = Convert.ToInt64(SelectedProjectDetailId.Value);
            var selectedProjectDetail = ProjectScheduleProjectDetailList.SingleOrDefault(p => p.ProjectDetailId == selectedProjectDetailId);

            if (selectedProjectDetail != null)
            {
                if (e.Appointment.Start < selectedProjectDetail.StartDate || e.Appointment.Start > selectedProjectDetail.EndDate)
                {
                    e.Cancel = true;
                }
            }
        }
    }

    class AppointmentInfo
    {
        public string ID { get; set; }

        public string Subject { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public ResourceCollection Resources { get; set; }
    }
}