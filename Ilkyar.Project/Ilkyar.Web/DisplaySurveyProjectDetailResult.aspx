<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplaySurveyProjectDetailResult.aspx.cs" Inherits="Ilkyar.Web.DisplaySurveyProjectDetailResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Proje Değerlendirme Sonuçları</span></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Proje Değerlendirme Sonuçları</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                        </ul>
                    </div>
                </div>
                <div class="panel-body">

                    <asp:HiddenField ID="ProjectId" runat="server" />
                    <asp:HiddenField ID="ProjectDetailId" runat="server" />

                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-flat">
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <telerik:RadGrid ID="SurveyProjectDetailResultGrid" runat="server" Width="100%">
                                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="SurveyProjectDetailQuestionId" HeaderStyle-Font-Bold="true" HeaderText="Soru Numarası" />
                                                            <telerik:GridBoundColumn DataField="SurveyProjectDetailQuestionName" HeaderStyle-Font-Bold="true" HeaderText="Soru" />
                                                            <telerik:GridBoundColumn DataField="Vote" HeaderStyle-Font-Bold="true" HeaderText="Toplam Puan" />
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
