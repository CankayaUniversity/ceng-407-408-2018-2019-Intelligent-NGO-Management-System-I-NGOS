<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Ilkyar.Web.EditProfile" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            var body = document.body;
            body.classList.add("has-detached-left");
        });
    </script>
    <style type="text/css">
        .ruBrowse {
            background-position: 0 -46px !important;
            height: 30px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="Server">
    <h4><span class="text-semibold">Profil</span></h4>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!-- Detached sidebar -->
    <div class="sidebar-detached">
        <div class="sidebar sidebar-default sidebar-separate">
            <div class="sidebar-content">

                <!-- User details -->
                <div class="content-group">
                    <div class="panel-body bg-indigo-400 border-radius-top text-center" style="background-image: url(http://demo.interface.club/limitless/assets/images/bg.png); background-size: contain;">
                        <div class="content-group-sm">
                            <h6 class="text-semibold no-margin-bottom"><%= UserHelper.FirstLastName %></h6>

                            <span class="display-block"><%=EnumHelper.GetEnumDescription(typeof(EnumUserType), UserHelper.CurrentUser.UserTypeId.ToString()) %></span>
                        </div>

                        <a href="#" class="display-inline-block content-group-sm">
                            <img src="" class="img-circle img-responsive" alt="" style="width: 110px; height: 110px;" />
                        </a>
                        <%--<div>
                            <telerik:RadAsyncUpload RenderMode="Lightweight" Localization-Select="Seç" runat="server" TargetFolder="/images" CssClass="async-attachment" ID="AsyncUploadProfilePicture" on
                                HideFileInput="true" AllowedFileExtensions=".jpeg,.jpg,.png,.doc,.docx,.xls,.xlsx" />
                            <span class="allowed-attachments">Dosya seçiniz <span class="allowed-attachments-list">(<%= String.Join( ",", AsyncUploadProfilePicture.AllowedFileExtensions ) %>)</span>
                            </span>
                        </div>--%>

                    </div>

                </div>

                <% if (UserHelper.UserTypeId == (int)EnumUserType.Student || UserHelper.UserTypeId == (int)EnumUserType.ScholarshipHolder) %>
                <%{ %>
                <div class="form-group">
                    <label class="col-lg-12 control-label">Transcript</label>
                    <div class="col-lg-12">
                        <input type="file" class="file-styled" style="height: 10px;" />
                        <%--<asp:FileUpload ID="FileUploadTranscript" runat="server" />--%>
                        <p style="font-size: 12px">(Uygun formatlar: gif, png, jpg. Max dosya boyutu 2Mb)</p>
                    </div>
                </div>
                <% }%>
                <!-- /user details -->

            </div>
        </div>
    </div>
    <!-- /detached sidebar -->

    <!-- Detached content -->
    <div class="container-detached">
        <div class="content-detached">

            <!-- Tab content -->
            <div class="tab-content">
                <div class="tab-pane fade in active" id="profileNGOHead">

                    <!-- Profile info -->
                    <div class="panel panel-flat">
                        <div class="panel-heading">
                            <h6 class="panel-title">Profil Bilgileri</h6>
                            <div class="heading-elements">
                                <ul class="icons-list">
                                    <li><a data-action="collapse"></a></li>
                                </ul>
                            </div>
                        </div>

                        <div class="panel-body">

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.NGOHead) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="NGOHead_FirstName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="NGOHead_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="NGOHead_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="NGOHead_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="NGOHead_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="NGOHead_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="NGOHead_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NGOHead_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="NGOHead_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="NGOHead_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="NGOHead_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="NGOHead_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="NGOHead_DutyStartDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="NGOHead_DutyStartDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="NGOHead_DutyEndDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.ProjectManager) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="ProjectManager_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_FirstName" ErrorMessage="Zorunlu alan!" />

                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="ProjectManager_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="ProjectManager_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="ProjectManager_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="ProjectManager_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="ProjectManager_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="ProjectManager_DutyStartDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_DutyStartDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="ProjectManager_DutyEndDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipCommittee) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Unvan</label>
                                        <asp:TextBox ID="ScholarshipCommittee_Title" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_Title" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="ScholarshipCommittee_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="ScholarshipCommittee_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="ScholarshipCommittee_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="ScholarshipCommittee_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="ScholarshipCommittee_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="ScholarshipCommittee_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="ScholarshipCommittee_DutyStartDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_DutyStartDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="ScholarshipCommittee_DutyEndDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.ScholarshipHolder) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Yön-Der</label>
                                        <asp:TextBox ID="ScholarshipHolder_YonDer" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="ScholarshipHolder_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="ScholarshipHolder_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="ScholarshipHolder_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <div>
                                            <telerik:RadDatePicker ID="ScholarshipHolder_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="ScholarshipHolder_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="ScholarshipHolder_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Burs Başlama Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="ScholarshipHolder_ScholarshipStartDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_ScholarshipStartDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Burs Bitiş Tarihi</label>
                                        <div>
                                            <% if (UserHelper.CurrentUser.ScholarshipEndDate == null) %>  <%--Bu ne olacak test et!--%>
                                            <%{ %>
                                            <asp:TextBox ID="ScholarshipHolder_ScholarshipEndDateFalse" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <telerik:RadDatePicker ID="ScholarshipHolder_ScholarshipEndDateTrue" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <% }%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Burs Miktarı</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipHolder_ScholarshipAmount" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>IBAN No</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipHolder_IBANNo" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_IBANNo" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_School" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_School" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Eğitim Seviyesi</label>
                                        <asp:TextBox ID="ScholarshipHolder_EducationLevel" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_EducationLevel" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Sınıf</label>
                                        <asp:TextBox ID="ScholarshipHolder_Class" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_Class" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Not Ortalaması</label>
                                        <asp:TextBox ID="ScholarshipHolder_cumGPA" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_cumGPA" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Anne Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_MotherName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_MotherName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-4">
                                        <label>Anne Çalışıyor mu?</label>
                                        <asp:RadioButton ID="ScholarshipHolder_IsMotherWorkingYes" Text="Evet" class="form-control" runat="server" />
                                        <asp:RadioButton ID="ScholarshipHolder_IsMotherWorkingNo" Text="No" class="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <label>Anne Mesleği</label>
                                        <asp:TextBox ID="ScholarshipHolder_MotherOccupationId" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Baba Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_FatherName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_FatherName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-4">
                                        <label>Baba Çalışıyor mu?</label>
                                        <asp:RadioButton ID="ScholarshipHolder_IsFatherWorkingYes" Text="Evet" class="form-control" runat="server" />
                                        <asp:RadioButton ID="ScholarshipHolder_IsFatherWorkingNo" Text="No" class="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <label>Baba Mesleği</label>
                                        <asp:TextBox ID="ScholarshipHolder_FatherOccupationId" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Kardeş Sayısı</label>
                                        <asp:TextBox ID="ScholarshipHolder_NumberOfSiblings" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_NumberOfSiblings" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <% if (UserHelper.CurrentUser.NumberOfSiblings > 0) %>
                            <%{ %>
                            <% for (int i = 0; i < UserHelper.CurrentUser.NumberOfSiblings; i++) %>
                            <% { %>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Kardeş Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingFirstName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_SiblingFirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Kardeş Soyadı</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingLastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_SiblingLastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Çalışıyor mu?</label>
                                        <asp:RadioButton ID="ScholarshipHolder_IsSiblingWorkingYes" Text="Evet" class="form-control" runat="server" />
                                        <asp:RadioButton ID="ScholarshipHolder_IsSiblingWorkingNo" Text="No" class="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Kardeş Mesleği</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingOccupation" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_SiblingOccupation" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Kardeş Aylık Geliri</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingMonthlyIncome" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_SiblingMonthlyIncome" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-3">
                                        <label>Birlikte yaşıyor mu?</label>
                                        <asp:RadioButton ID="ScholarshipHolder_IslivingTogetherYes" Text="Evet" class="form-control" runat="server" />
                                        <asp:RadioButton ID="ScholarshipHolder_IslivingTogetherNo" Text="No" class="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <% } %>
                            <% }%>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Aylık Gelir</label>
                                        <asp:TextBox ID="ScholarshipHolder_MontlyIncome" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_MontlyIncome" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Herhangi bir sağlık problemi var mı?</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipHolder_HealthCondition" TextMode="MultiLine" Rows="5" Columns="60" Style="resize: none;" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipHolder_HealthCondition" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.Donator) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Donator_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Donator_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Donator_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="Donator_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Donator_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Donator_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Meslek</label>
                                        <div>
                                            <asp:TextBox ID="Donator_Occupation" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Donator_Occupation" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>İş Yeri</label>
                                        <div>
                                            <% if (UserHelper.CurrentUser.WorkPlace == null) %>
                                            <%{ %>
                                            <asp:TextBox ID="Donator_WorkPlaceTrue" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <asp:TextBox ID="Donator_WorkPlaceFalse" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Schoolmaster_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Schoolmaster_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Schoolmaster_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="Schoolmaster_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Schoolmaster_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Schoolmaster_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>İl</label>
                                        <telerik:RadDropDownList runat="server" ID="cityDropDownListSM" AutoPostBack="true" DefaultMessage="Seçiniz" OnItemSelected="cityDropDownListSM_ItemSelected" class="form-control" Width="100%">
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="cityDropDownListSM" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-4">
                                        <label>İlçe</label>
                                        <telerik:RadDropDownList runat="server" ID="townDropDownListSM" class="form-control" Width="100%">
                                        </telerik:RadDropDownList>
                                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="townDropDownListSM" ErrorMessage="Zorunlu alan!" />--%>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="Schoolmaster_School" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_School" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.HostSchoolTeacher) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="HostSchoolTeacher_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="HostSchoolTeacher_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="HostSchoolTeacher_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="HostSchoolTeacher_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="HostSchoolTeacher_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="HostSchoolTeacher_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>İl</label>
                                        <telerik:RadDropDownList runat="server" ID="cityDropDownListHST" AutoPostBack="true" DefaultMessage="Seçiniz" OnItemSelected="cityDropDownListHST_ItemSelected" class="form-control" Width="100%">
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="cityDropDownListHST" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>İlçe</label>
                                        <telerik:RadDropDownList runat="server" ID="townDropDownListHST" class="form-control" Width="100%">
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="townDropDownListHST" ErrorMessage="Zorunlu alan!" />
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="HostSchoolTeacher_School" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_School" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <%--<div class="col-md-6">
                                            <label>Branş</label>
                                            <telerik:RadDropDownList runat="server" class="form-control" Width="100%">
                                                <Items>
                                                    <telerik:DropDownListItem Value="1" Text="Value #1" />
                                                    <telerik:DropDownListItem Value="2" Text="Value #2" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </div>--%>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.Student) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Student_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Student_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Student_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="Student_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Student_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Student_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>İl</label>
                                        <telerik:RadDropDownList runat="server" ID="cityDropDownListS" AutoPostBack="true" DefaultMessage="Seçiniz" OnItemSelected="cityDropDownListS_ItemSelected" class="form-control" Width="100%">
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="cityDropDownListS" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>İlçe</label>
                                        <telerik:RadDropDownList runat="server" ID="townDropDownListS" class="form-control" Width="100%">
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="townDropDownListS" ErrorMessage="Zorunlu alan!" />
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="Student_School" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_School" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Eğitim Seviyesi</label>
                                        <asp:TextBox ID="Student_EducationLevel" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_EducationLevel" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Sınıf</label>
                                        <asp:TextBox ID="Student_Class" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_Class" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Not Ortalaması</label>
                                        <asp:TextBox ID="Student_CumGPA" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_CumGPA" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.Volunteer) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Volunteer_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Volunteer_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Volunteer_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="Volunteer_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Volunteer_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Volunteer_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Volunteer_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <telerik:RadRadioButtonList ID="checkIsStudent" CausesValidation="false" runat="server" Direction="Horizontal" OnSelectedIndexChanged="checkIsStudent_SelectedIndexChanged">
                                            <Items>
                                                <telerik:ButtonListItem Value="1" Text="Üniversite Öğrencisiyim"></telerik:ButtonListItem>
                                                <telerik:ButtonListItem Value="2" Text="Çalışıyorum"></telerik:ButtonListItem>
                                            </Items>
                                        </telerik:RadRadioButtonList>
                                    </div>
                                </div>
                                <div class="row" id="divStudent" runat="server" visible="false" style="padding-top: 20px;">
                                    <div class="col-md-4">
                                        <label>Üniversite</label>
                                        <div class="form-group has-feedback">
                                            <telerik:RadDropDownList ID="Volunteer_University" ValidationGroup="ValidationGroupStudent" AutoPostBack="true" DefaultMessage="Üniversite Seçiniz" runat="server" Width="100%" OnItemSelected="universityDropDownListV_ItemSelected"></telerik:RadDropDownList>
                                            <asp:RequiredFieldValidator ID="ValidatorUniversityDDL" runat="server" ControlToValidate="Volunteer_University" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Departman</label>
                                        <div class="form-group has-feedback">
                                            <telerik:RadDropDownList ID="Volunteer_Department" ValidationGroup="ValidationGroupStudent" DefaultMessage="Departman Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                                            <asp:RequiredFieldValidator ID="ValidatorDepartmentDDL" runat="server" ControlToValidate="Volunteer_Department" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Sınıf</label>
                                        <div class="form-group has-feedback">
                                            <telerik:RadTextBox ID="Volunteer_Class" ValidationGroup="ValidationGroupStudent" class="form-control" runat="server" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="ValidatorVolunteerClass" runat="server" ControlToValidate="Volunteer_Class" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="divNotStudent" runat="server" visible="false" style="padding-top: 20px;">
                                    <div class="col-md-4">
                                        <label>Meslek</label>
                                        <div class="form-group has-feedback">
                                            <telerik:RadDropDownList ID="Volunteer_Occupation" ValidationGroup="ValidationGroupNotStudent" DefaultMessage="Meslek Seçiniz" runat="server" Width="100%"></telerik:RadDropDownList>
                                            <asp:RequiredFieldValidator ID="ValidatorVolunteerOccupation" runat="server" ControlToValidate="Volunteer_Occupation" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (UserHelper.UserTypeId == (int)EnumUserType.YonDer) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="YonDer_FirstName" class="form-control" runat="server"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_FirstName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="YonDer_LastName" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_LastName" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="YonDer_TCKN" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_TCKN" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="YonDer_BirthDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_BirthDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="YonDer_Email" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_Email" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="YonDer_Phone" class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_Phone" ErrorMessage="Zorunlu alan!" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="YonDer_DutyStartDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_DutyStartDate" ErrorMessage="Zorunlu alan!" />
                                        </div>
                                    </div>

                                     <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <telerik:RadDatePicker ID="YonDer_DutyEndDate" class="form-control" runat="server"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Yeni Şifre</label>
                                        <asp:TextBox ID="User_Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Yeni Şifre (Tekrar)</label>
                                        <asp:TextBox ID="User_PasswordAgain" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="text-left">
                                <div class="alert alert-success alert-styled-left alert-arrow-left alert-bordered">
                                    <button type="button" class="close" data-dismiss="alert"><span>&times;</span><span class="sr-only">Close</span></button>
                                    <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Visible="false" />
                                </div>
                            </div>--%>
                            <div class="text-left">
                                <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Visible="false" />
                            </div>

                            <div class="text-right">
                                <a href="Profile.aspx" class="btn btn-default"><i class="icon-arrow-left8 position-left"></i>Profile Geri Dön</a>
                                <asp:LinkButton ID="buttonUpdateProfile" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonUpdateProfile_Click" />
                            </div>
                        </div>
                        <!-- /profile info -->
                    </div>

                </div>
                <!-- /tab content -->

            </div>
        </div>
    </div>
    <!-- /detached content -->
</asp:Content>
