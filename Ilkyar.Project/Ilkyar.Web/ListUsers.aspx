<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListUsers.aspx.cs" Inherits="Ilkyar.Web.ListUsers" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Kullanıcıları Listele</span></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
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
                                <label class="text-bold">Kullanıcı Tipi</label>
                                <telerik:RadDropDownList ID="UserType" DefaultMessage="Tümü" runat="server" Width="100%"></telerik:RadDropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Kimlik No</label>
                                <telerik:RadTextBox ID="UserName" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Ad</label>
                                <telerik:RadTextBox ID="FirstName" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Soyad</label>
                                <telerik:RadTextBox ID="LastName" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="text-bold">E-posta</label>
                                <telerik:RadTextBox ID="Email" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Telefon</label>
                                <telerik:RadTextBox ID="Phone" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Doğum Tarihi</label>
                                <telerik:RadDatePicker ID="BirthDate" runat="server" Width="100%"></telerik:RadDatePicker>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Durum</label>
                                <telerik:RadDropDownList ID="Status" DefaultMessage="Tümü" runat="server" Width="100%"></telerik:RadDropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="text-right">
                        <asp:LinkButton ID="buttonClearFilter" runat="server" type="button" class="btn btn-default" Text='<i class="icon-sync position-left"></i>Temizle' OnClick="buttonClearFilter_Click" />
                        <asp:LinkButton ID="buttonFilterUserList" runat="server" type="button" class="btn btn-success" Text='<i class="icon-search4 position-left"></i>Sorgula' OnClick="buttonFilterUserList_Click" />
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
                                <telerik:RadGrid ID="UsersListGrid" runat="server" Width="100%" OnItemCommand="UserListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="UserId">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="UserId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="UserType" HeaderText="Tip" />
                                            <telerik:GridBoundColumn DataField="Username" HeaderText="Kimlik No" />
                                            <telerik:GridBoundColumn DataField="FirstName" HeaderText="Ad" />
                                            <telerik:GridBoundColumn DataField="LastName" HeaderText="Soyad" />
                                            <telerik:GridBoundColumn DataField="Email" HeaderText="E-posta" />
                                            <telerik:GridBoundColumn DataField="PhoneNum" HeaderText="Telefon" />
                                            <telerik:GridDateTimeColumn DataField="BirthDate" HeaderText="Doğum Tarihi" DataFormatString="{0:dd/MM/yyyy}" />
                                            <telerik:GridBoundColumn DataField="UserStatus" HeaderText="Durum" />
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
