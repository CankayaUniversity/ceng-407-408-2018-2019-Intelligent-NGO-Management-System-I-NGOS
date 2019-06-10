<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvitationConfirmation.aspx.cs" Inherits="Ilkyar.Web.InvitationConfirmation" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Import Namespace="Ilkyar.Contracts.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Davet Onay İşlemleri</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="panel panel-flat">
        <div class="panel-heading">
            <h6 class="panel-title">Davet Bilgileri</h6>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>

        <div class="panel-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <telerik:RadGrid ID="InvitationGridList" runat="server" Width="100%" OnItemCommand="InvitationGridList_ItemCommand">
                            <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                            <telerik:GridBoundColumn DataField="SchoolmasterId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="SchoolName" HeaderText="Okul Adı" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="City" HeaderText="İl" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="Town" HeaderText="İlçe" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="NumberOfStudent" HeaderText="Öğrenci Sayısı" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="RequirementList"  HeaderText="İhtiyaç Listesi" HeaderStyle-Font-Bold="true"/>                                           
                                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Durum Bilgisi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" Text="Onayla" Font-Bold="true" Visible='<%#Eval("StatusId").ToString() == ((int)EnumInvitationStatusType.Beklemede).ToString() ? true:false %>' CommandName="Confirm" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" Text="Reddet" Font-Bold="true" Visible='<%#Eval("StatusId").ToString() == ((int)EnumInvitationStatusType.Beklemede).ToString() ? true:false %>' CommandName="Reject" />
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
</asp:Content>
