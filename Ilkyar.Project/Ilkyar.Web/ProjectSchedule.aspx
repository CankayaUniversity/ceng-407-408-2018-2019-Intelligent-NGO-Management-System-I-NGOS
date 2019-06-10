<%@ Page Title="Proje Çizelgeleme" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectSchedule.aspx.cs" Inherits="Ilkyar.Web.ProjectSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            padding: 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Proje Planlama</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ScheduleList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ScheduleList" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Proje Planlama</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">Proje Seçiniz</label>
                                <div>
                                    <telerik:RadDropDownList runat="server" ID="ProjectDetailName" class="form-control" Width="100%" AutoPostBack="true" DefaultMessage="Seçiniz" OnItemSelected="ProjectDetailName_ItemSelected"></telerik:RadDropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:HiddenField ID="SelectedProjectDetailId" runat="server" />

        <telerik:RadScheduler RenderMode="Lightweight" runat="server" ID="ScheduleList"
            Culture="tr-TR" Height="100%" ShowFullTime="true"
            OnFormCreated="ScheduleList_FormCreated"
            OnFormCreating="ScheduleList_FormCreating"
            OnAppointmentCommand="ScheduleList_AppointmentCommand"
            OnAppointmentDelete="ScheduleList_AppointmentDelete"
            DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End"
            EnableRecurrenceSupport="false" SelectedView="MonthView">
            <AdvancedForm Enabled="false"></AdvancedForm>
            <TimelineView UserSelectable="false"></TimelineView>
            <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
            <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>
            <InlineInsertTemplate>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <label>Aktivite</label>
                            <div>
                                <telerik:RadDropDownList ID="ActivityList" runat="server" Width="100%"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ActivityList" ValidationGroup="Insert" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <label>Başlangıç Zamanı</label>
                            <div>
                                <telerik:RadTimePicker ID="StartTime" runat="server"></telerik:RadTimePicker>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="StartTime" ValidationGroup="Insert" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <label>Bitiş Zamanı</label>
                            <div>
                                <telerik:RadTimePicker ID="EndTime" runat="server"></telerik:RadTimePicker>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="EndTime" ValidationGroup="Insert" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-right" style="padding-top: 10px;">
                    <asp:LinkButton ID="InsertButton" runat="server" CssClass="btn btn-success" ValidationGroup="Insert" CommandName="Insert">Ekle</asp:LinkButton>
                    <asp:LinkButton ID="CancelButton" runat="server" CssClass="btn btn-default" CommandName="Cancel">İptal Et</asp:LinkButton>
                </div>
            </InlineInsertTemplate>
            <InlineEditTemplate>
                <asp:HiddenField ID="ProjectDetailActivityScheduleId" runat="server" />
                <asp:HiddenField ID="ProjectDetailActivityId" runat="server" />
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <label>Aktivite</label>
                            <div>
                                <telerik:RadDropDownList ID="ActivityList" runat="server" Width="100%"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ActivityList" ValidationGroup="Insert" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <label>Başlangıç Zamanı</label>
                            <div>
                                <telerik:RadTimePicker ID="StartTime" runat="server"></telerik:RadTimePicker>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="StartTime" ValidationGroup="Insert" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <label>Bitiş Zamanı</label>
                            <div>
                                <telerik:RadTimePicker ID="EndTime" runat="server"></telerik:RadTimePicker>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="EndTime" ValidationGroup="Insert" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-right" style="padding-top: 10px;">
                    <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-success" ValidationGroup="Update" CommandName="Update">Güncelle</asp:LinkButton>
                    <asp:LinkButton ID="CancelButton" runat="server" CssClass="btn btn-default" CommandName="Cancel">İptal Et</asp:LinkButton>
                </div>
            </InlineEditTemplate>
        </telerik:RadScheduler>
    </div>
</asp:Content>
