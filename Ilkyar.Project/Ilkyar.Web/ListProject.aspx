<%@ Page Title="Projeleri Listele" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListProject.aspx.cs" Inherits="Ilkyar.Web.ListProject" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Projeleri Listele</span></h4>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Filtreleme Kriterleri</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                            <li><a data-action="collapse"></a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-4">
                                <label class="text-bold">Proje Tipi</label>
                                <telerik:RadDropDownList ID="ProjectType" DefaultMessage="Tümü" runat="server" Width="100%"></telerik:RadDropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Proje Yöneticisi</label>
                                <telerik:RadTextBox ID="ProjectManager" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Proje Adı</label>
                                <telerik:RadTextBox ID="ProjectName" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-4">
                                <label class="text-bold">Başlangıç Tarihi</label>
                                <telerik:RadDatePicker ID="ProjectStartDate" runat="server" Width="100%"></telerik:RadDatePicker>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Bitiş Tarihi</label>
                                <telerik:RadDatePicker ID="ProjectEndDate" runat="server" Width="100%"></telerik:RadDatePicker>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Durum</label>
                                <telerik:RadDropDownList ID="ProjectStatus" runat="server" DefaultMessage="Tümü" Width="100%"></telerik:RadDropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="text-right">
                        <asp:LinkButton ID="buttonClearFilter" runat="server" type="button" class="btn btn-default" Text='<i class="icon-sync position-left"></i>Temizle' OnClick="buttonClearFilter_Click" />
                        <asp:LinkButton ID="buttonFilterProjectList" runat="server" type="button" class="btn btn-success" Text='<i class="icon-search4 position-left"></i>Sorgula' OnClick="buttonFilterProjectList_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="ProjectListGrid" runat="server" Width="100%" OnItemCommand="ProjectListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id, ProjectManagerId" >
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ProjectTypeName" HeaderText="Proje Tipi" HeaderStyle-Font-Bold="true" />                                            
                                            <telerik:GridBoundColumn DataField="ProjectManagerId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ProjectManagerName" HeaderText="Proje Yöneticisi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Proje Adı" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridDateTimeColumn DataField="StartDate" HeaderText="Başlangıç Tarihi" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridDateTimeColumn DataField="EndDate" HeaderText="Bitiş Tarihi" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="ProjectTypeId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Durum" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridButtonColumn Text="Detay" ButtonType="LinkButton" CommandName="Detail" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
