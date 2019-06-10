<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="Ilkyar.Web.ViewReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="panel panel-flat">
        <div class="panel-heading">
            <h5 class="text-semibold">Rapor Detayları</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                </ul>
            </div>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <label style="font-weight:bold">Tarih</label>
                        <telerik:RadTextBox ID="Report_Date" class="form-control" runat="server" Width="100%" Enabled="false" ></telerik:RadTextBox>
                    </div>
                    <div class="col-md-6">
                        <label style="font-weight:bold">Konu</label>
                        <telerik:RadTextBox ID="Report_Subject" class="form-control" runat="server" Width="100%" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <label style="font-weight:bold">Yön-Der</label>
                        <telerik:RadTextBox ID="Report_YonDerName" class="form-control" runat="server" Width="100%" Enabled="false"></telerik:RadTextBox>
                    </div>
                    <div class="col-md-6">
                        <label style="font-weight:bold">Bursiyer</label>
                        <telerik:RadTextBox ID="Report_ScholarshipHolder" class="form-control" runat="server" Width="100%" Enabled="false"></telerik:RadTextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <label style="font-weight:bold">Metin</label>
                        <telerik:RadEditor ID="Report_Text" class="form-control" runat="server" Width="100%" Height="100%" AutoResizeHeight="true" RenderMode="Lightweight" Enabled="false"></telerik:RadEditor>
                    </div>
                </div>
                <br />
                <div class="text-right">
                    <a href="ListReport.aspx" class="btn btn-default"><i class="icon-arrow-left8 position-left"></i>Rapor Listesine Geri Dön</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
