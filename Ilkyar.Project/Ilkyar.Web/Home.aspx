<%@ Page Title="Anasayfa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Ilkyar.Web.Home" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>


<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <style>
        .badgeStyle {
            background-color: white;
        }

        .badgeDivStyle {
            width: 120px;
            float: left;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="Server">
    <h4><i class="icon-menu7 position-left"></i><span class="text-semibold">Anasayfa</span></h4>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead) %>
    <%{ %>
    <div class="row">
        <div class="col-md-4">
            <div class="panel bg-teal-400">
                <div class="panel-body">
                    <h4 style="font-weight: bold; text-align: center"><a href="ListActiveProject.aspx" style="font-weight: bold; text-align: center; color: white">Aktif Projeler</a></h4>
                    <h2 class="no-margin" style="text-align: center"><span id="TotalProjectDetailActiveCount" runat="server"></span></h2>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel bg-green-400">
                <div class="panel-body">
                    <h4 style="font-weight: bold; text-align: center"><a href="ListCompletedProject.aspx" style="font-weight: bold; text-align: center; color: white">Tamamlanan Projeler</a></h4>
                    <h2 class="no-margin" style="text-align: center"><span id="TotalProjectDetailCompletedCount" runat="server"></span></h2>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel bg-teal-400">
                <div class="panel-body">
                    <h4 style="font-weight: bold; text-align: center"><a href="ListProject.aspx" style="font-weight: bold; text-align: center; color: white">Toplam Projeler</a></h4>
                    <h2 class="no-margin" style="text-align: center"><span id="TotalProjectDetailCount" runat="server"></span></h2>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-body" style="background-color: lightgray;">
                    <div class="form-group">
                        <div class="row" style="text-align: center; padding: 10px; height: 200px">
                            <div>
                                <img src="images/surveypicture.png" style="margin-left: auto; margin-right: auto; display: block; width: 25%;" />
                            </div>
                            <br />
                            <a href="DisplaySurveyProjectDetailResult.aspx" style="font-weight: bold; font-size: x-large; color: #2e2f4c">Proje değerlendirmelerini görmek ister misiniz?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>

    <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Volunteer) %>
    <%{ %>

    <div class="row">
        <div class="col-md-9">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Rozetler</h5>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div id="ActivityLeadershipBadge" runat="server">
                                <asp:ListView ID="VolunteerBadgeList" runat="server">
                                    <ItemTemplate>
                                        <div class="badgeDivStyle">
                                            <button type="button" class="btn btn-lg badgeStyle" data-popup="tooltip" title="<%# Eval("ProjectDetailName") %>">
                                                <img src="images/etkinliklideri.png" style="width: 100%;" />
                                            </button>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <div id="BronzeLeadershipBadge" runat="server">
                                <div class="badgeDivStyle">
                                    <button type="button" class="btn btn-lg badgeStyle" data-popup="tooltip" title="Bronz Liderlik Rozeti">
                                        <img src="images/bronzeleader.png" style="width: 100%" /></button>
                                </div>
                            </div>
                            <div id="SilverLeadershipBadge" runat="server">
                                <div class="badgeDivStyle">
                                    <button type="button" class="btn btn-lg badgeStyle" data-popup="tooltip" title="Gümüş Liderlik Rozeti">
                                        <img src="images/silverleader.png" style="width: 100%;" /></button>
                                </div>
                            </div>
                            <div id="GoldLeadershipBadge" runat="server">
                                <div class="badgeDivStyle">
                                    <button type="button" class="btn btn-lg badgeStyle" data-popup="tooltip" title="Altın Liderlik Rozeti">
                                        <img src="images/goldleader.png" style="width: 100%;" /></button>
                                </div>
                            </div>
                            <div id="BeeBadgePanel" runat="server">
                                <div class="badgeDivStyle">
                                    <button type="button" class="btn btn-lg badgeStyle" data-popup="tooltip" title="Tebrikler! En çok aktiviteye siz katıldınız.">
                                        <img src="images/beebagde.png" style="width: 100%;" /></button>
                                </div>
                            </div>
                            <div id="GeniusBadge" runat="server">
                                <asp:ListView ID="GenuisBadgeList" runat="server">
                                    <ItemTemplate>
                                        <div class="badgeDivStyle">
                                            <button type="button" class="btn btn-lg badgeStyle" data-popup="tooltip" title="<%# Eval("ActivityName") %>">
                                                <img src="images/geniusbadge.png" style="width: 100%;" />
                                            </button>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <label runat="server" id="volunteerBadgeInfo" visible="false">Henüz bir rozetiniz yok.</label>
                            <div>
                                <asp:Label ID="LabelNeededForBeeBadge" Style="color: #0b7bde" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="alert alert-info alert-styled-left alert-bordered">
                <asp:Label ID="LeadershipBoardInfo" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-body" style="background-color: lightgray;">
                    <div class="form-group">
                        <div class="row" style="text-align: center; padding: 10px">
                            <div>
                                <img src="images/volunteer.png" style="margin-left: auto; margin-right: auto; display: block; width: 25%;" />
                            </div>
                            <br />
                            <a href="ListProject.aspx" style="font-weight: bold; font-size: x-large; color: #2e2f4c">Yeni bir projeye katılmak ister misiniz?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
    </div>

    <%--<div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-body" style="background-color: lightgray;">
                    <div class="form-group">
                        <a href="ListProject.aspx">
                            <div class="row" style="text-align: center; padding: 10px">
                                <div>
                                    <img src="images/volunteer.png" style="margin-left: auto; margin-right: auto; display: block; width: 25%;" />
                                </div>
                                <br />
                                <span style="font-size: 20px; font-weight: bold; color: #2e2f4c;">Yeni bir projeye katılmak ister misiniz?</span>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title" style="font-weight: bold; font-size: large; color: #2e2f4c; text-align: center">Sizin için seçtiğimiz bazı proje etkinlikleri</h5>
                    <label runat="server" id="projectSuggestionInfo" visible="false">Henüz size uygun bir projemiz yok.</label>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="SuggestedProjectDetailListGrid" runat="server" Width="100%" OnItemCommand="SuggestedProjectDetailListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="ProjectId,ProjectDetailId">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ProjectId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ProjectDetailId" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ProjectDetailName" HeaderText="Proje Bilgisi (Başlangıç Tarihi - Bitiş Tarihi)" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="ActivityName" HeaderText="Etkinlik" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridButtonColumn Text="Proje Bilgilerini Gör" ButtonType="LinkButton" CommandName="Detail" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
                                            <telerik:GridButtonColumn Text="Başvur" ButtonType="LinkButton" CommandName="Select" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
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

    <%} %>

    <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.ProjectManager) %>
    <%{ %>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Rozetler</h5>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <label runat="server" id="BadgeInfo" visible="false">Henüz bir rozetiniz yok.</label>
                            <div class="col-md-3" id="IsHighestVoteBadge" runat="server">
                                <button type="button" style="background-color: white; color: white; width: 60%; height: 25%; margin: 1px;" class="btn btn-lg">
                                    <img src="images/bestpm.png" style="width: 100%" /></button>
                            </div>
                            <div class="col-md-3" id="ProjectManagerBronzeBadge" visible="true" runat="server">
                                <button type="button" style="background-color: white; color: white; width: 60%; height: 25%; margin: 1px;" class="btn btn-lg">
                                    <img src="images/bronzepm.png" style="width: 100%" /></button>
                            </div>
                            <div class="col-md-3" id="ProjectManagerSilverBadge" visible="true" runat="server">
                                <button type="button" style="background-color: white; color: white; width: 60%; height: 25%; margin: 1px;" class="btn btn-lg">
                                    <img src="images/silverpm.png" style="width: 100%" /></button>
                            </div>
                            <div class="col-md-3" id="ProjectManagerGoldenBadge" visible="true" runat="server">
                                <button type="button" style="background-color: white; color: white; width: 60%; height: 25%; margin: 1px;" class="btn btn-lg">
                                    <img src="images/goldenpm.png" style="width: 100%" /></button>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-body" style="background-color: lightgray;">
                    <div class="form-group">
                        <div class="row" style="text-align: center; padding: 10px">
                            <div>
                                <img src="images/form.png" style="margin-left: auto; margin-right: auto; display: block; width: 15%;" />
                            </div>
                            <br />
                            <a href="ListProject.aspx" style="font-weight: bold; font-size: x-large; color: #2e2f4c">Bekleyen aktivite başvuruların olabilir. İncelemek ister misin?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>


    <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.NGOHead) %>
    <%{ %>
    <div class="row">
        <div class="col-md-7">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Liderlik Sıralaması</h5>
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
                                <telerik:RadGrid ID="LeadershipBoardListGrid" runat="server" Width="100%">
                                    <MasterTableView AutoGenerateColumns="false">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="VolunteerName" HeaderText="Gönüllü Bilgisi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="AvgVote" HeaderText="Alınan Ortalama Puan" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="TotalActivity" HeaderText="Etkinliklere Katılım Yüzdesi" HeaderStyle-Font-Bold="true" />
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
    <%} %>

    <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Schoolmaster || UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.HostSchoolTeacher) %>
    <%{ %>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-flat">
                        <div class="panel-body" style="background-color: lightgray;">
                            <div class="form-group">
                                <div class="row" style="text-align: center; padding: 10px; ">
                                    <div>
                                        <img src="images/form.png" style="margin-left: auto; margin-right: auto; display: block; width: 15%;" />
                                    </div>
                                    <br />
                                    <a href="VoteProject.aspx" style="font-weight: bold; font-size: x-large; color: #2e2f4c">Katıldığınız projeleri değerlendirmek ister misiniz?</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>

     <% if (UserHelper.CurrentUser.UserTypeId == (int)EnumUserType.Student) %>
    <%{ %>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-flat">
                        <div class="panel-body" style="background-color: lightgray;">
                            <div class="form-group">
                                <div class="row" style="text-align: center; padding: 10px; ">
                                    <div>
                                        <img src="images/form.png" style="margin-left: auto; margin-right: auto; display: block; width: 15%;" />
                                    </div>
                                    <br />
                                    <a href="VoteVolunteer.aspx" style="font-weight: bold; font-size: x-large; color: #2e2f4c">Katıldığınız projeleri değerlendirmek ister misiniz?</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
