<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VoteVolunteer.aspx.cs" Inherits="Ilkyar.Web.VoteVolunteer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Etkinlikleri Değerlendir</span></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Etkinlik Değerlendirme Formu</h5>
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
                                <telerik:RadGrid ID="EvaluationVolunteerListGrid" runat="server" Width="100%">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="ActivityId">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ActivityName" HeaderText="Aktivite Tipi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="VolunteerName" HeaderText="Gönüllü Bilgisi" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridTemplateColumn ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <telerik:RadRating ID="RateVolunteer" runat="server" DataModelID='<%# Eval("ActivityId") %>' AccessKey='<%# Eval("VolunteerId") %>' AutoPostBack="true" ItemCount="5" OnRate="RateVolunteer_Rate" SelectionMode="Continuous" Precision="Item" Orientation="Horizontal" Value='<%# Convert.ToInt32(Eval("Vote")) %>'></telerik:RadRating>
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
</asp:Content>
