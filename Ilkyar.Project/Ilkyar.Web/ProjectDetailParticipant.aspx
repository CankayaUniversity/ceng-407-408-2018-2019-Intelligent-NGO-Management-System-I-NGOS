<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDetailParticipant.aspx.cs" Inherits="Ilkyar.Web.ProjectDetailParticipant" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Katılımcı İşlemleri</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead || UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager) %>
                    <%{ %>
                    <h5 class="panel-title">Katılımcı Ekle</h5>
                    <%} %>
                    <% else %>
                    <%{ %>
                    <h5 class="panel-title">Proje Bilgisi</h5>
                    <%} %>
                    <div class="heading-elements">
                        <ul class="icons-list">
                            <li><a data-action="collapse"></a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:HiddenField ID="ProjectId" runat="server" />
                    <asp:HiddenField ID="ProjectDetailId" runat="server" />
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <label class="text-bold">Proje Bilgisi</label>
                                <telerik:RadTextBox ID="ProjectDetailSummary" runat="server" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                    <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead || UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager || UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Schoolmaster) %>
                    <%{ %>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="text-bold">Katılımcı Tipi</label>
                                <telerik:RadDropDownList ID="UserType" DefaultMessage="Seçiniz" runat="server" Width="100%" AutoPostBack="true" OnItemSelected="UserType_ItemSelected"></telerik:RadDropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="text-bold">Katılımcı Bilgisi</label>
                                <telerik:RadDropDownList ID="UserFirstLastName" DefaultMessage="Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="text-left">
                        <asp:Label ID="labelErrorMessage" runat="server" Visible="false" />
                    </div>
                    <div class="text-right">
                        <asp:LinkButton ID="buttonAddParticipant" runat="server" type="button" class="btn btn-success" Text='<i class="icon-plus3 position-left"></i>Ekle' OnClick="buttonAddParticipant_Click" />
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Katılımcı Listesi</h5>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="ParticipantListGrid" runat="server" Width="100%" OnPreRender="ParticipantListGrid_PreRender" OnItemCommand="ParticipantListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                            <telerik:GridBoundColumn DataField="UserTypeName" HeaderText="Katılımcı Tipi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Katılımcı Bilgisi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridButtonColumn UniqueName="Delete" Text="Sil" ButtonType="LinkButton" CommandName="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
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
