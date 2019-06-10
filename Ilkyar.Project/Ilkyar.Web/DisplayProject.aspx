<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayProject.aspx.cs" Inherits="Ilkyar.Web.DisplayProject" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Proje Detayları</span></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Temel Detaylar</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                            <li><a data-action="collapse"></a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="well" style="margin-bottom: 10px;">
                        <asp:HiddenField ID="ProjectId" runat="server" />
                        <asp:HiddenField ID="ProjectManagerId" runat="server" />
                        <div class="row">
                            <div class="col-md-4">
                                <label class="text-bold">Proje Tipi</label>
                                <telerik:RadDropDownList runat="server" ID="ProjectType" DefaultMessage="Seçiniz.." class="form-control" Width="100%"></telerik:RadDropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Proje Adı</label>
                                <telerik:RadTextBox ID="TextBoxProject" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Proje Yöneticisi</label>
                                <telerik:RadDropDownList runat="server" DefaultMessage="Seçiniz.." ID="ProjectManager" class="form-control" Width="100%"></telerik:RadDropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <label class="text-bold">Başlangıç Tarihi</label>
                                <telerik:RadDatePicker ID="RadDatePickerProjectStartDate" Class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Bitiş Tarihi</label>
                                <telerik:RadDatePicker ID="RadDatePickerProjectEndDate" Class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Proje Durumu</label>
                                <telerik:RadDropDownList runat="server" ID="ProjectStatus" DefaultMessage="Seçiniz.." class="form-control" Width="100%"></telerik:RadDropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="text-left">
                        <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                    <div class="text-right">
                        <a href="ListProject.aspx" class="btn btn-default"><i class="icon-arrow-left8 position-left"></i>Proje Listesine Geri Dön</a>
                        <asp:LinkButton ID="createProjectDetail" runat="server" type="button" class="btn btn-info" Text='<i class="icon-plus2 position-left"></i>Proje Detayı Ekle' OnClick="createProjectDetail_Click" />
                        <asp:LinkButton ID="updateProject" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Projeyi Güncelle' OnClick="updateProject_Click" />
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
                                <telerik:RadGrid ID="ProjectSubDetailListGrid" runat="server" Width="100%" OnPreRender="ProjectSubDetailListGrid_PreRender" OnItemCommand="ProjectSubDetailListGrid_ItemCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                            <telerik:GridDateTimeColumn DataField="DetailStartDate" HeaderText="Başlangıç Tarihi" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridDateTimeColumn DataField="DetailEndDate" HeaderText="Bitiş Tarihi" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="City" HeaderText="İl" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="School" HeaderText="Okul" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Durum" HeaderStyle-Font-Bold="true" />
                                            <telerik:GridButtonColumn Text="Görüntüle" ButtonType="LinkButton" CommandName="Detail" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
                                            <%--<telerik:GridButtonColumn UniqueName="Edit" Text="Düzenle" ButtonType="LinkButton"  CommandName="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />--%>
                                            <telerik:GridButtonColumn UniqueName="Participants" Text="Katılımcılar" ButtonType="LinkButton" CommandName="Participants" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
                                            <telerik:GridButtonColumn UniqueName="ActivityOperations" Text="Aktivite Başvuru İşlemleri" ButtonType="LinkButton" CommandName="ActivityOperations" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
                                            <telerik:GridButtonColumn UniqueName="ActivityConfirmationOperations" Text="Aktivite Başvuru Onay İşlemleri" ButtonType="LinkButton" CommandName="ActivityConfirmationOperations" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
                                            <telerik:GridButtonColumn UniqueName="SurveyProjectDetailResult" Text="Proje Değerlendirmeleri" ButtonType="LinkButton" CommandName="SurveyProjectDetailResult" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0b7bde" />
                                            <telerik:GridTemplateColumn UniqueName="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" Text="Düzenle" Font-Bold="true" Visible='<%#Eval("StatusId").ToString() == ((int)EnumProjectSubDetailStatusType.Aktif).ToString() ? true:false %>' CommandName="Edit" />
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
