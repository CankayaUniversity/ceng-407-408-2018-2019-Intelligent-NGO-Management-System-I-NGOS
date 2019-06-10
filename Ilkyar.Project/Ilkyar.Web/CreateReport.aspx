<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateReport.aspx.cs" Inherits="Ilkyar.Web.CreateReport" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="Server">
    <h4><span class="text-semibold">Rapor Yaz</span></h4>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="panel panel-flat">
        <div class="panel-heading">
            <h5 class="text-semibold">Rapor Bilgileri</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                </ul>
            </div>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-4">
                        <label>Bursiyer Seçiniz</label>
                        <telerik:RadDropDownList runat="server" ID="Report_ScholarshipHolder" AutoPostBack="true" DefaultMessage="Seçiniz" class="form-control" Width="100%" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Report_ScholarshipHolder" ErrorMessage="Zorunlu alan!" />
                    </div>
                    <div class="col-md-4">
                        <label>Tarih</label>
                        <div>
                            <telerik:RadDatePicker ID="Report_Date" class="form-control" runat="server"></telerik:RadDatePicker>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Report_Date" ErrorMessage="Zorunlu alan!" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <label>Konu</label>
                        <telerik:RadTextBox ID="Report_Subject" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Report_Subject" ErrorMessage="Zorunlu alan!" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <label>Metin</label>
                        <telerik:RadEditor ID="Report_Text" runat="server" Width="100%" EditModes="Design" ContentAreaMode="Div" StripFormattingOptions="NoneSupressCleanMessage">
                            <Tools>
                                <telerik:EditorToolGroup>
                                    <telerik:EditorTool Name="Bold" />
                                    <telerik:EditorTool Name="Italic" />
                                    <telerik:EditorTool Name="Underline" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="ForeColor" />
                                    <telerik:EditorTool Name="BackColor" />
                                    <telerik:EditorSeparator />
                                    <telerik:EditorTool Name="FontName" />
                                    <telerik:EditorTool Name="RealFontSize" />
                                </telerik:EditorToolGroup>
                            </Tools>
                        </telerik:RadEditor>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Report_Text" ErrorMessage="Zorunlu alan!" />
                    </div>
                </div>
            </div>

            <div class="text-left">
                <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Visible="false" />
            </div>
            <div class="text-right">
                <a href="CreateReport.aspx" class="btn btn-default"><i class="icon-blocked position-left"></i>İptal</a>
                <asp:LinkButton ID="buttonSaveReport" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonSaveReport_Click" />
            </div>
        </div>
    </div>

</asp:Content>
