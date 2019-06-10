<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VoteProject.aspx.cs" Inherits="Ilkyar.Web.VoteProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Projeleri Değerlendir</span></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Proje Değerlendirme Formu</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">Proje Seçiniz</label>
                                <div>
                                    <telerik:RadDropDownList runat="server" ID="ProjectDetailName" class="form-control" Width="100%" AutoPostBack="true" DefaultMessage="Seçiniz" OnItemSelected="ProjectDetailName_ItemSelected"></telerik:RadDropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:ListView ID="SurveyProjectDetailQuestionList" runat="server">
                                    <ItemTemplate>
                                        <div class="form-group">
                                            <asp:Label runat="server" CssClass="text-semibold"><%# Eval("Name") %></asp:Label>
                                            <asp:Label runat="server" Visible='<%# Eval("IsAnswered").ToString() == "True" ? false: true %>'><i>(Henüz cevaplanmadı)</i></asp:Label>
                                            <telerik:RadRating ID="RateProjectDetail" runat="server" AccessKey='<%# Eval("Id") %>' AutoPostBack="true" ItemCount="5" OnRate="RateProjectDetail_Rate" SelectionMode="Continuous" Precision="Item" Orientation="Horizontal" Value='<%# Convert.ToInt32(Eval("Vote")) %>'></telerik:RadRating>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
