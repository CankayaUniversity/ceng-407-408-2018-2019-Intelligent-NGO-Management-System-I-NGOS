<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayProjectDetail.aspx.cs" Inherits="Ilkyar.Web.DisplayProjectDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Projeleri Görüntüle</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <%-- Project Header --%>
    <div class="row">
        <div class="panel panel-flat">
            <div class="panel-heading">
                <h5 class="panel-title">Proje Bilgileri</h5>
                <div class="heading-elements">
                    <li><a data-action="collapse"></a></li>
                    <ul class="icons-list">
                    </ul>
                </div>
            </div>
            <div class="panel-body">
                <div class="well" style="margin-bottom: 10px;">
                    <asp:HiddenField ID="ProjectId" runat="server" />
                    <asp:HiddenField ID="ProjectDetailId" runat="server" />
                    <asp:HiddenField ID="ProjectManagerId" runat="server" />
                    <div class="row">
                        <div class="col-md-4">
                            <label class="text-bold">Proje Tipi</label>
                            <asp:TextBox ID="TextBoxProjectType" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="text-bold">Proje Adı</label>
                            <asp:TextBox ID="TextBoxProject" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="text-bold">Proje Yöneticisi</label>
                            <asp:TextBox ID="TextBoxProjectManager" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <label class="text-bold">Başlangıç Tarihi</label>
                            <asp:TextBox ID="TextBoxProjecStartDate" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="text-bold">Bitiş Tarihi</label>
                            <asp:TextBox ID="TextBoxProjectEndDate" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="text-bold">Proje Durumu</label>
                            <asp:TextBox ID="TextBoxProjectStatus" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="text-left">
                    <asp:Label ID="label1" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
                <div class="text-right">
                    <asp:LinkButton ID="LinkButtonBack"  OnClick="LinkButtonBack_Click" runat="server"><i class="icon-arrow-left8 position-left"></i>Detay Listesine Geri Dön</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <%-- Project Header --%>

    <%-- Project Detail --%>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Diğer Detaylar</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                            <li><a data-action="collapse"></a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="well" style="margin-bottom: 10px;">
                        <div class="row">
                            <div class="col-md-4">
                                <label class="text-bold">İl</label>
                                <asp:TextBox ID="RadDropDownListCity" ReadOnly="true" Class="form-control" AutoPostBack="true" runat="server" Width="100%" OnItemSelected="RadDropDownListCity_ItemSelected"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">İlçe</label>
                                <asp:TextBox ID="RadDropDownListTown" ReadOnly="true" AutoPostBack="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Okul</label>
                                <asp:TextBox ID="TextBoxSchool" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <label class="text-bold">Başlangıç Tarihi</label>
                                <asp:TextBox ID="TextBoxProjectDetailStartDate" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Bitiş Tarihi</label>
                                <asp:TextBox ID="TextBoxProjectDetailEndDate" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="panel panel-flat">
                                <div class="panel-heading">
                                    <h5 class="panel-title">Etkinlik Detayları</h5>
                                </div>
                                <div class="panel-body">
                                    <div class="well">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <telerik:RadGrid ID="ProjectActivityGrid" runat="server" Width="100%" >
                                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Id">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="Id" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="ActivityName" HeaderText="Etkinlikler" HeaderStyle-Font-Bold="true" />
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="panel panel-flat">
                                <div class="panel-heading">
                                    <h5 class="panel-title">Malzeme Detayları</h5>
                                </div>
                                <div class="panel-body">
                                    <div class="well">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="text-bold">Gerekli Malzemeler</label>
                                                <asp:TextBox ID="TextBoxMaterial" ReadOnly="true" Class="form-control" runat="server" TextMode="MultiLine" Width="100%" Height="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>






                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-flat">
                                <div class="panel-heading">
                                    <h5 class="panel-title">Ulaşım Detayları</h5>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h6 class="panel-title">Gidiş Detayları</h6>
                                            <div class="well">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Gidiş Tarihi</label>
                                                        <asp:TextBox ID="TextBoxDepartureDate" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Ulaşım Tipi</label>
                                                        <asp:TextBox ID="DepartureTransportationType" ReadOnly="true" Width="100%" Class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Gidiş - Hareket Noktası</label>
                                                        <asp:TextBox ID="DeparturePoint" Class="form-control" ReadOnly="true" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Otobüs Plakası / Uçuş Firması</label>
                                                        <asp:TextBox ID="DepartureTransportationTypeInfo" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Kişi Sayısı</label>
                                                        <asp:TextBox ID="DepartureNumberOfPeople" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <h6 class="panel-title">Dönüş Detayları</h6>
                                            <div class="well">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Dönüş Tarihi</label>
                                                        <asp:TextBox ID="TextBoxArrivalDate" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label class="text-bold">Ulaşım Tipi</label>
                                                        <asp:TextBox ID="ArrivalTransportationType" ReadOnly="true" Class="form-control" DefaultMessage="Seçiniz" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Dönüş - Hareket Noktası</label>
                                                        <asp:TextBox ID="ArrivalPoint" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Otobüs Plakası / Uçuş Firması</label>
                                                        <asp:TextBox ID="ArrivalTansportationTypeInfo" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Kişi Sayısı</label>
                                                        <asp:TextBox ID="ArrivalNumberOfPeople" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="panel panel-flat">
                                <div class="panel-heading">
                                    <h5 class="panel-title">Konaklama Detayları</h5>
                                </div>
                                <div class="panel-body">
                                    <div class="well">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="text-bold">Konaklanacak Yer</label>
                                                <asp:TextBox ID="TextBoxAccomodation" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="text-bold">Kişi Sayısı</label>
                                                <asp:TextBox ID="TextBoxNumOfPep" ReadOnly="true" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="panel panel-flat">
                                <div class="panel-heading">
                                    <h5 class="panel-title">Not Ekle</h5>

                                </div>
                                <div class="panel-body">
                                    <div class="well">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="text-bold">Not</label>
                                                <asp:TextBox ID="TextAreaNote" ReadOnly="true" Class="form-control" runat="server" TextMode="MultiLine" Width="100%" Height="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="text-left">
                        <asp:Label ID="labelErrorMessage" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>

                </div>

            </div>
        </div>
    </div>

</asp:Content>
