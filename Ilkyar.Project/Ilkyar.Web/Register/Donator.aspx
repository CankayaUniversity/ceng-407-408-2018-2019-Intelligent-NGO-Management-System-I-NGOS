<%@ Page Title="Bağış Yap" Language="C#" MasterPageFile="~/Register/Register.Master" AutoEventWireup="true" CodeBehind="Donator.aspx.cs" Inherits="Ilkyar.Web.Register.Donator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <h5 class="content-group-lg" style="font-size: large;">Bağış Yap</h5>
                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Ad</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_FirstName" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_FirstName" ErrorMessage="Zorunlu alan!" ForeColor="Red" />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label class="text-bold">Soyad</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_LastName" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_LastName" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Doğum Tarihi</label>
                            <div class="form-group has-feedback">
                                <telerik:RadDatePicker ID="Donator_BirthDate" class="form-control" runat="server" Width="100%"></telerik:RadDatePicker>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_BirthDate" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">Telefon</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_Phone" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_Phone" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">TC Kimlik No</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_TCKN" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_TCKN" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">E-Posta</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_Email" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_Email" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Meslek</label>
                            <div class="form-group has-feedback">
                                <telerik:RadDropDownList ID="Donator_Occupation" DefaultMessage="Meslek Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator ID="ValidatorDonatorOccupation" runat="server" ControlToValidate="Donator_Occupation" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">İş Yeri</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_WorkPlace" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Şifre</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_Password" TextMode="Password" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_Password" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label class="text-bold">Şifre(Tekrar)</label>
                            <div class="form-group has-feedback">
                                <telerik:RadTextBox ID="Donator_PasswordAgain" TextMode="Password" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_PasswordAgain" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                    </div>

                    <div class="text-left">
                        <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Visible="false" />
                    </div>

                    <div class="text-right">
                        <a href="../Login.aspx" class="btn btn-link"><i class="icon-arrow-left13 position-left"></i>Geri Dön</a>
                        <asp:LinkButton ID="buttonCreateNewDonator" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateNewDonator_Click" />
                    </div>




                </div>
            </div>
        </div>
    </div>
    <!-- /registration form -->
</asp:Content>
