<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUpdateProjectDetail.aspx.cs" Inherits="Ilkyar.Web.CreateUpdateProjectDetail" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Proje Detayları</span></h4>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <%-- Project Header --%>

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
                        <asp:HiddenField ID="ProjectDetailId" runat="server" />
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
                                <asp:TextBox ID="TextBoxProjectStartDate" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
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
                        <asp:LinkButton ID="LinkButtonBack" runat="server" OnClick="LinkButtonBack_Click"><i class="icon-arrow-left8 position-left"></i>Detay Listesine Geri Dön</asp:LinkButton>
                    </div>
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
                                <telerik:RadDropDownList ID="RadDropDownListCity" DefaultMessage="Seçiniz.." Class="form-control" AutoPostBack="true" runat="server" Width="100%" OnSelectedIndexChanged="RadDropDownListCity_SelectedIndexChanged"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator ID="ValidatorRadDropDownListCity" runat="server" ControlToValidate="RadDropDownListCity" ErrorMessage="Zorunlu alan!" />
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">İlçe</label>
                                <telerik:RadDropDownList ID="RadDropDownListTown" Class="form-control" runat="server" Width="100%"></telerik:RadDropDownList>
                                <%--<asp:RequiredFieldValidator ID="ValidatorRadDropDownListTown" runat="server" ControlToValidate="RadDropDownListTown" ErrorMessage="Zorunlu alan!" />--%>
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Okul Tipi</label>
                                <telerik:RadDropDownList ID="RadDropDownListSchoolType" DefaultMessage="Seçiniz..." Class="form-control" runat="server" Width="100%"></telerik:RadDropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <label class="text-bold">Okul</label>
                                <asp:TextBox ID="TextBoxSchool" Class="form-control" runat="server" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ValidatorTextBoxSchool" runat="server" ControlToValidate="TextBoxSchool" ErrorMessage="Zorunlu alan!" />
                            </div>


                            <div class="col-md-4">
                                <label class="text-bold">Başlangıç Tarihi</label>
                                <telerik:RadDatePicker ID="RadDatePickerProjectStartDate" Class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="ValidatorRadDatePickerProjecStartDate" runat="server" ControlToValidate="RadDatePickerProjectStartDate" ErrorMessage="Zorunlu alan!" />
                            </div>
                            <div class="col-md-4">
                                <label class="text-bold">Bitiş Tarihi</label>
                                <telerik:RadDatePicker ID="RadDatePickerProjectEndDate" Class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="ValidatorRadDatePickerProjectEndDate" runat="server" ControlToValidate="RadDatePickerProjectEndDate" ErrorMessage="Zorunlu alan!" />
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
                                            <div class="col-md-6">
                                                <label class="text-bold">Yapılacak Etkinlikler</label>
                                                <telerik:RadComboBox ID="RadComboBoxActivity" runat="server" CheckBoxes="true" Width="400"></telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="ValidatorActivity" ControlToValidate="RadComboBoxActivity" runat="server" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
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
                                                <asp:TextBox ID="TextBoxMaterial" Class="form-control" runat="server" TextMode="MultiLine" Width="100%" Height="60px"></asp:TextBox>
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
                                                        <telerik:RadDatePicker ID="DepartureDate" Class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="ValidatorDepartureDate" runat="server" ControlToValidate="DepartureDate" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Ulaşım Tipi</label>
                                                        <telerik:RadDropDownList ID="DepartureTransportationType" DefaultMessage="Seçiniz" Width="100%" Class="form-control" runat="server"></telerik:RadDropDownList>
                                                        <asp:RequiredFieldValidator ID="ValidatorTransportationType" runat="server" ControlToValidate="DepartureTransportationType" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Gidiş - Hareket Noktası</label>
                                                        <telerik:RadTextBox ID="DeparturePoint" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="ValidatorDeparturePoint" runat="server" ControlToValidate="DeparturePoint" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Otobüs Plakası / Uçuş Firması</label>
                                                        <telerik:RadTextBox ID="DepartureTransportationTypeInfo" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="ValidatorDepartureTransportationTypeInfo" runat="server" ControlToValidate="DepartureTransportationTypeInfo" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Kişi Sayısı</label>
                                                        <telerik:RadTextBox ID="DepartureNumberOfPeople" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="ValidatorDepartureNumberOfPeople" runat="server" ControlToValidate="DepartureNumberOfPeople" ErrorMessage="Zorunlu alan!" />
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
                                                        <telerik:RadDatePicker ID="ArrivalDate" Class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="ValidatorArrivalDate" runat="server" ControlToValidate="ArrivalDate" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Ulaşım Tipi</label>
                                                        <telerik:RadDropDownList ID="ArrivalTransportationType" Class="form-control" DefaultMessage="Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                                                        <asp:RequiredFieldValidator ID="ValidatorArrivalTransportationType" runat="server" ControlToValidate="ArrivalTransportationType" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Dönüş - Hareket Noktası</label>
                                                        <telerik:RadTextBox ID="ArrivalPoint" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="ValidatorArrivalPoint" runat="server" ControlToValidate="ArrivalPoint" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Otobüs Plakası / Uçuş Firması</label>
                                                        <telerik:RadTextBox ID="ArrivalTansportationTypeInfo" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="ValidatorArrivalTansportationTypeInfo" runat="server" ControlToValidate="ArrivalTansportationTypeInfo" ErrorMessage="Zorunlu alan!" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="text-bold">Kişi Sayısı</label>
                                                        <telerik:RadTextBox ID="ArrivalNumberOfPeople" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="ValidatorArrivalNumberOfPeople" runat="server" ControlToValidate="ArrivalNumberOfPeople" ErrorMessage="Zorunlu alan!" />
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
                                                <telerik:RadTextBox ID="TextBoxAccomodation" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="ValidatorTextBoxAccomodation" ControlToValidate="TextBoxAccomodation" runat="server" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-3">
                                                <label class="text-bold">Kişi Sayısı</label>
                                                <telerik:RadTextBox ID="TextBoxNumOfPep" Class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="ValidatorTextBoxNumOfPep" ControlToValidate="TextBoxNumOfPep" runat="server" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
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
                                                <telerik:RadTextBox ID="TextAreaNote" runat="server" TextMode="MultiLine" Width="100%" Height="130px"></telerik:RadTextBox>
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
                    <div class="text-right">
                        <asp:LinkButton ID="buttonCreateProjectDetail" runat="server" OnClick="buttonCreateProjectDetail_Click" type="button" class="btn btn-success" Visible="false" Text='<i class="icon-checkmark position-left"></i>Kaydet' />
                        <asp:LinkButton ID="buttonUpdateProjectDetail" runat="server" OnClick="buttonUpdateProjectDetail_Click" type="button" class="btn btn-success" Visible="false" Text='<i class="icon-checkmark position-left"></i>Güncelle' />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
