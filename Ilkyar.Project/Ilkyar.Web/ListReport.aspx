<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListReport.aspx.cs" Inherits="Ilkyar.Web.ListReport" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="Server">
    <h4><span class="text-semibold">Raporları Listele</span></h4>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
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
                            <div class="col-md-3">
                                <label class="text-bold">Rapor Tarihi</label>
                                <telerik:RadDatePicker ID="ReportDate" runat="server" Width="100%"></telerik:RadDatePicker>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Yön-Der</label>
                                <telerik:RadTextBox ID="YonderName" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Bursiyer</label>
                                <telerik:RadTextBox ID="ScholarshipHolderName" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Konu</label>
                                <telerik:RadTextBox ID="Subject" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="text-right">
                        <asp:LinkButton ID="buttonClearFilter" runat="server" type="button" class="btn btn-default" Text='<i class="icon-sync position-left"></i>Temizle' OnClick="buttonClearFilter_Click" />
                        <asp:LinkButton ID="buttonFilterReportList" runat="server" type="button" class="btn btn-success" Text='<i class="icon-search4 position-left"></i>Sorgula' OnClick="buttonFilterReportList_Click" />
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
                                <telerik:RadGrid ID="ReportListGrid" runat="server" Width="100%" OnItemCommand="ReportListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                            <telerik:GridDateTimeColumn DataField="ReportDate" HeaderText="Rapor Tarihi" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="YonDerId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="YonderName" HeaderText="Yön-Der" HeaderStyle-Font-Bold="true"/>
                                            <telerik:GridBoundColumn DataField="ScholarshipHolderId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ScholarshipHolderName" HeaderText="Bursiyer" HeaderStyle-Font-Bold="true"/>
                                            <telerik:GridBoundColumn DataField="Subject" HeaderText="Konu" HeaderStyle-Font-Bold="true"/>
                                            <telerik:GridButtonColumn Text="Detay" ButtonType="LinkButton" CommandName="Select" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
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
