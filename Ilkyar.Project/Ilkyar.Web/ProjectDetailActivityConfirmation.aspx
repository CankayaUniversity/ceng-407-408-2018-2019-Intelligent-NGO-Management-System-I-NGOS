<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDetailActivityConfirmation.aspx.cs" Inherits="Ilkyar.Web.ProjectDetailActivityConfirmation" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Import Namespace="Ilkyar.Contracts.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Aktivite Başvuru Onay İşlemleri</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Aktivite Başvuru Listesi</h5>
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
                            <div class="col-md-12">
                                <telerik:RadGrid ID="ActivityListGrid" runat="server" Width="100%" OnItemCommand="ActivityListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id,UserId">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                            <telerik:GridBoundColumn DataField="UserId" Visible="false" />
                                            <telerik:GridButtonColumn DataTextField="VolunteerFullName" ButtonType="LinkButton" CommandName="Select" HeaderText="Gönüllü Bilgisi" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="#161616" />
                                            <telerik:GridBoundColumn DataField="ActivityName" HeaderText="Aktivite Tipi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Durum Bilgisi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" Text="Öneri Ekranı" Font-Bold="true" CommandName="ShowPopup" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" Text="Onayla" Font-Bold="true" Visible='<%#Eval("StatusId").ToString() == ((int)EnumActivityStatusType.Beklemede).ToString() ? true:false %>' CommandName="Confirm" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" Text="Reddet" Font-Bold="true" Visible='<%#Eval("StatusId").ToString() == ((int)EnumActivityStatusType.Beklemede).ToString() ? true:false %>' CommandName="Reject" />
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

    <telerik:RadWindow ID="VolunteerSuggestionPopup" RenderMode="Lightweight" runat="server" Width="645px" Height="550px" Style="z-index: 100001;">
        <ContentTemplate>
            <div style="width: 80%;">
                <h3>Gönüllü Aktivite Eşleşme Öneri Bilgileri</h3>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Gönüllü</label>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="VolunteerFullName" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Aktivite</label>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="CurrentActivityName" runat="server" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Şehir Eşleşme Oranı (Onaylanmış)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="ApprovedCityMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Şehir Eşleşme Oranı (Tümü)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="OverallCityMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Bölge Eşleşme Oranı (Onaylanmış)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="ApprovedRegionMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Bölge Eşleşme Oranı (Tümü)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="OverallRegionMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Okul Tipi Eşleşme Oranı (Onaylanmış)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="ApprovedSchoolTypeMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Okul Tipi Eşleşme Oranı (Tümü)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="OverallSchoolTypeMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Proje Tipi Eşleşme Oranı (Onaylanmış)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="ApprovedProjectTypeMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Proje Tipi Eşleşme Oranı (Tümü)</label>
                    </div>
                    <div class="col-md-6">
                        <telerik:RadProgressBar ID="OverallProjectTypeMatchPercentage" runat="server" MinValue="0" MaxValue="100" BarType="Percent" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Kişi Sayısı Ortalaması (Onaylanmış)</label>
                    </div>
                    <div class="col-md-6">
                        <label id="ApprovedNumberOfPeopleMatchTolerancePercentage" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Kişi Sayısı Ortalaması (Tümü)</label>
                    </div>
                    <div class="col-md-6">
                        <label id="OverallNumberOfPeopleMatchTolerancePercentage" runat="server" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Aktivite Sayısı(Onaylanan)</label>
                    </div>
                    <div class="col-md-6">
                        <label id="ApprovedActivityCount" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Aktivite Sayısı(Reddedilen)</label>
                    </div>
                    <div class="col-md-6">
                        <label id="RejectedActivityCount" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold pull-right">Aktivite Sayısı(Toplam)</label>
                    </div>
                    <div class="col-md-6">
                        <label id="OverallActivityCount" runat="server" />
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>
