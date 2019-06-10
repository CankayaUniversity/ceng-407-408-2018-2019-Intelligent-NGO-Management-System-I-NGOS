<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDetailActivity.aspx.cs" Inherits="Ilkyar.Web.ProjectDetailActivity" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Import Namespace="Ilkyar.Contracts.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Aktivite Başvuru İşlemleri</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Aktivite Ekle</h5>
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
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="text-bold">Etkinlik</label>
                                <telerik:RadDropDownList ID="ActivityType" DefaultMessage="Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="text-left">
                        <asp:Label ID="labelErrorMessage" runat="server" Visible="false" />
                    </div>
                    <div class="text-right">
                        <asp:LinkButton ID="buttonAddActivity" runat="server" type="button" class="btn btn-success" Text='<i class="icon-plus3 position-left"></i>Ekle' OnClick="buttonAddActivity_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Aktivite Listesi</h5>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="ActivityListGrid" runat="server" Width="100%" OnItemCommand="ActivityListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ActivityName" HeaderText="Aktivite Tipi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Durum Bilgisi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridTemplateColumn ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" Text="Başvuruyu İptal Et" Font-Bold="true" Visible='<%#Eval("StatusId").ToString() == ((int)EnumActivityStatusType.Beklemede).ToString() ? true:false %>' CommandName="Delete" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
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
