<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListCompletedProject.aspx.cs" Inherits="Ilkyar.Web.ListCompletedProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
     <h4><span class="text-semibold">Tamamlanan Projeleri Listele</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="CompletedProjectListGrid" runat="server" Width="100%" OnItemCommand="CompletedProjectListGrid_ItemCommand">
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
