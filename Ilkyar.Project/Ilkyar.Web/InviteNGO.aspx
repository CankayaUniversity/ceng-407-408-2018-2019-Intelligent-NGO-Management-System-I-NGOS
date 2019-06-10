<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InviteNGO.aspx.cs" Inherits="Ilkyar.Web.InviteNGO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div class="panel panel-flat">
        <div class="panel-heading">
            <h6 class="panel-title">Davet Bilgileri</h6>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                </ul>
            </div>
        </div>

        <div class="panel-body">
            <div class="form-group">

                <div class="row">
                    <div class="col-md-4">
                        <label class="text-bold">Ad</label>
                        <asp:TextBox ID="Schoolmaster_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="text-bold">Soyad</label>
                        <asp:TextBox ID="Schoolmaster_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                    </div>            
                    <div class="col-md-4">
                        <label class="text-bold">TC Kimlik No</label>
                        <asp:TextBox ID="Schoolmaster_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                    </div>                    
                </div>
                </div>
            

            <div class="form-group">
                <div class="row">
                    <div class="col-md-4">
                        <label class="text-bold">E-Posta</label>
                        <asp:TextBox ID="Schoolmaster_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="text-bold">Telefon</label>
                        <asp:TextBox ID="Schoolmaster_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="text-bold">İl</label>
                        <asp:TextBox ID="Schoolmaster_City" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="row">                    
                    <div class="col-md-4">
                        <label class="text-bold">İlçe</label>
                        <asp:TextBox ID="Schoolmaster_Town" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="text-bold">Okul Adı</label>
                        <asp:TextBox ID="Schoolmaster_School" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="text-bold">Öğrenci Sayısı</label>
                        <asp:TextBox ID="TextBoxNumberOfStudent" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="row">                    
                    <div class="col-md-12">
                        <label class="text-bold">İhtiyaç Listesi</label>
                        <telerik:RadUpload ID="RadUploadRequirementList" runat="server" MaxFileInputsCount="2" OverwriteExistingFiles="false" ControlObjectsVisibility="None"></telerik:RadUpload>
                        <asp:FileUpload ID="uplFileUploader" runat="server" />
                        </div>
                </div>
            </div>
            <div class="text-left">
                        <asp:Label ID="labelErrorMessage" Text="İhtiyaç Listesi Seçiniz.. " runat="server" Visible="false" />
                    </div>
                    <div class="text-right">
                        <asp:LinkButton ID="buttonInvite" runat="server" type="button" class="btn btn-success" Text='<i class="icon-plus3 position-left"></i>Ekle' OnClick="buttonInvite_Click" />
                    </div>
            </div>
        </div>
</asp:Content>
