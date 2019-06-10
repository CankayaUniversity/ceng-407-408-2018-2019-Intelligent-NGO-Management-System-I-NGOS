<%@ Page Title="Gönüllü Ol" Language="C#" MasterPageFile="~/Register/Register.Master" AutoEventWireup="true" CodeBehind="Volunteer.aspx.cs" Inherits="Ilkyar.Web.Register.Volunteer" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .isStudentClass td {
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!-- Registration form -->
    <div class="row">
        <div class="col-lg-6 col-lg-offset-3">
            <div class="panel registration-form">
                <div class="panel-body">
                    <div class="text-center">
                        <div>
                            <img src="../images/Ilkyarlogo.png" style="margin-left: auto; margin-right: auto; display: block; width: 50%;" />
                        </div>
                        <h4 class="content-group-lg" style="font-size: large; font-weight:bold;" >Gönüllü Ol</h4>
                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Ad</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_FirstName" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_FirstName" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label class="text-bold">Soyad</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_LastName" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_LastName" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Doğum Tarihi</label>
                            <div class="form-group has-feedback">
                                <telerik:RadDatePicker ID="Volunteer_BirthDate" class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_BirthDate" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">Telefon</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_Phone" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_Phone" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">TC Kimlik No</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_TCKN" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_TCKN" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">E-Posta</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_Email" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_Email" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label class="text-bold">İlgi Alanları</label>
                            <div class="form-group has-feedback">
                                <telerik:RadComboBox ID="RadComboBoxInterest" runat="server" CheckBoxes="true" Width="400"></telerik:RadComboBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group has-feedback">
                                <telerik:RadRadioButtonList ID="checkIsStudent" CausesValidation="false" runat="server" Direction="Horizontal" OnSelectedIndexChanged="checkIsStudent_SelectedIndexChanged">
                                    <Items>
                                        <telerik:ButtonListItem Value="1" Text="Üniversite Öğrencisiyim"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Value="2" Text="Çalışıyorum"></telerik:ButtonListItem>
                                    </Items>
                                </telerik:RadRadioButtonList>
                            </div>
                        </div>
                    </div>

                    <div class="row" id="divStudent" runat="server" visible="false">
                        <div class="col-md-4">
                            <label class="text-bold">Üniversite</label>
                            <div class="form-group has-feedback">
                                <telerik:RadDropDownList ID="universityDropDownListV" ValidationGroup="ValidationGroupStudent" AutoPostBack="true" DefaultMessage="Üniversite Seçiniz" runat="server" Width="100%" OnItemSelected="universityDropDownListV_ItemSelected"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator ID="ValidatorUniversityDDL" runat="server" ControlToValidate="universityDropDownListV" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="text-bold">Departman</label>
                            <div class="form-group has-feedback">
                                <telerik:RadDropDownList ID="departmentDropDownListV" ValidationGroup="ValidationGroupStudent" DefaultMessage="Departman Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator ID="ValidatorDepartmentDDL" runat="server" ControlToValidate="departmentDropDownListV" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="text-bold">Sınıf</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_Class" ValidationGroup="ValidationGroupStudent" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="ValidatorVolunteerClass" runat="server" ControlToValidate="Volunteer_Class" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="row" id="divNotStudent" runat="server" visible="false">
                        <div class="col-md-4">
                            <label class="text-bold">Meslek</label>
                            <div class="form-group has-feedback">
                                <telerik:RadDropDownList ID="Volunteer_Occupation" ValidationGroup="ValidationGroupNotStudent" DefaultMessage="Meslek Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator ID="ValidatorVolunteerOccupation" runat="server" ControlToValidate="Volunteer_Occupation" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Şifre</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_Password" TextMode="Password" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_Password" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label class="text-bold">Şifre(Tekrar)</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Volunteer_PasswordAgain" TextMode="Password" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_PasswordAgain" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>
                    </div>

                    <div class="text-left">
                        <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Visible="false" />
                    </div>

                    <div class="text-right">
                        <a href="../Login.aspx" class="btn btn-link"><i class="icon-arrow-left13 position-left"></i>Geri Dön</a>
                        <asp:LinkButton ID="buttonCreateNewVolunteer" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateNewVolunteer_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /registration form -->
</asp:Content>
