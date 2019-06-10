<%@ Page Title="Yeni Proje Oluştur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="Ilkyar.Web.CreateProject" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="Server">
    <h4><span class="text-semibold">Yeni Proje Oluştur</span></h4>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Proje Oluştur</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label>Proje Tipi:</label>
                                <div>
                                    <telerik:RadDropDownList runat="server" ID="ProjectType" class="form-control" Width="100%"></telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectType" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label>Proje Adı:</label>
                                <div>
                                    <telerik:RadTextBox ID="ProjectName" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectName" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Proje Yöneticisi</label>
                                <div>
                                    <telerik:RadDropDownList runat="server" ID="ProjectManager" class="form-control" Width="100%"></telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label>Başlangıç Tarihi:</label>
                                <div>
                                    <telerik:RadDatePicker ID="StartDate" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDate" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Bitiş Tarihi</label>
                                <div>
                                    <telerik:RadDatePicker ID="EndDate" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="EndDate" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="text-left">
                        <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Visible="false" />
                    </div>

                    <div class="text-right">
                        <a href="CreateProject.aspx" class="btn btn-default"><i class="icon-blocked position-left"></i>İptal</a>
                        <asp:LinkButton ID="buttonCreateNewProject" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateNewProject_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
