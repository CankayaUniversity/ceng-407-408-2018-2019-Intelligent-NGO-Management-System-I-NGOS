<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs" Inherits="Ilkyar.Web.ViewUser" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var body = document.body;
            body.classList.add("has-detached-left");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Kullanıcı Detay</span></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!-- Detached sidebar -->
    <div class="sidebar-detached">
        <div class="sidebar sidebar-default sidebar-separate">
            <div class="sidebar-content">

                <!-- User details -->
                <div class="content-group">
                    <div class="panel-body bg-indigo-400 border-radius-top text-center" style="background-image: url(http://demo.interface.club/limitless/assets/images/bg.png); background-size: contain;">
                        <div class="content-group-sm">
                            <h6 class="text-semibold no-margin-bottom"><span id="FirstLastName" runat="server"></span></h6>

                            <span class="display-block" id="UserType" runat="server"></span>
                        </div>

                        <a href="#" class="display-inline-block content-group-sm">
                            <img src="assets/images/placeholder.jpg" class="img-circle img-responsive" alt="" style="width: 110px; height: 110px;" />
                        </a>
                    </div>
                </div>

                <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.Student) %>
                <%{ %>
                <div class="form-group">
                    <telerik:RadButton ID="StudentTranscript" runat="server" Text="Not çizelgesi"></telerik:RadButton>
                </div>
                <% }%>

                <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.ScholarshipHolder) %>
                <%{ %>
                <div class="form-group">
                    <telerik:RadButton ID="ScholarshipHolderTranscript" runat="server" Text="Not çizelgesi"></telerik:RadButton>
                </div>
                <div class="form-group">
                    <telerik:RadButton ID="ScholarshipHolderStudentDocument" runat="server" Text="Öğrenci Belgesi"></telerik:RadButton>
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
                <div class="tab-pane fade in active">

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

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.NGOHead) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="NGOHead_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="NGOHead_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="NGOHead_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="NGOHead_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="NGOHead_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="NGOHead_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="NGOHead_DutyStartDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <% if ((DateTime?)(Session["ViewUser_DutyEndDate"]) == null) %>
                                            <%{ %>
                                            <asp:TextBox ID="NGOHead_DutyEndDate" ReadOnly="true" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <asp:TextBox ID="NGOHead_DutyEndDateIsActive" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.ProjectManager) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="ProjectManager_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="ProjectManager_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="ProjectManager_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="ProjectManager_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="ProjectManager_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="ProjectManager_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="ProjectManager_DutyStartDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <% if ((DateTime?)(Session["ViewUser_DutyEndDate"]) == null) %>
                                            <%{ %>
                                            <asp:TextBox ID="ProjectManager_DutyEndDate" ReadOnly="true" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <asp:TextBox ID="ProjectManager_DutyEndDateIsActive" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.ScholarshipCommittee) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Unvan</label>
                                        <asp:TextBox ID="ScholarshipCommittee_Title" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="ScholarshipCommittee_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="ScholarshipCommittee_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="ScholarshipCommittee_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipCommittee_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="ScholarshipCommittee_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="ScholarshipCommittee_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipCommittee_DutyStartDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <% if ((DateTime?)(Session["ViewUser_DutyEndDate"]) == null) %>
                                            <%{ %>
                                            <asp:TextBox ID="ScholarshipCommittee_DutyEndDate" ReadOnly="true" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <asp:TextBox ID="ScholarshipCommittee_DutyEndDateIsActive" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.ScholarshipHolder) %>
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
                                        <asp:TextBox ID="ScholarshipHolder_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="ScholarshipHolder_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="ScholarshipHolder_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipHolder_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="ScholarshipHolder_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="ScholarshipHolder_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Burs Başlama Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipHolder_ScholarshipStartDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Burs Bitiş Tarihi</label>
                                        <div>
                                            <% if ((DateTime?)(Session["ViewUser_ScholarshipEndDate"]) == null) %>
                                            <%{ %>
                                            <asp:TextBox ID="ScholarshipHolder_ScholarshipEndDateFalse" ReadOnly="true" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <asp:TextBox ID="ScholarshipHolder_ScholarshipEndDateTrue" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
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
                                            <asp:TextBox ID="ScholarshipHolder_IBANNo" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_School" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Eğitim Seviyesi</label>
                                        <asp:TextBox ID="ScholarshipHolder_EducationLevel" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Sınıf</label>
                                        <asp:TextBox ID="ScholarshipHolder_Class" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Not Ortalaması</label>
                                        <asp:TextBox ID="ScholarshipHolder_cumGPA" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Anne Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_MotherName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Anne Çalışıyor mu?</label>
                                        <asp:TextBox ID="ScholarshipHolder_IsMotherWorking" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Anne Mesleği</label>
                                        <asp:TextBox ID="ScholarshipHolder_MotherOccupationId" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Baba Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_FatherName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Baba Çalışıyor mu?</label>
                                        <asp:TextBox ID="ScholarshipHolder_IsFatherWorking" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Baba Mesleği</label>
                                        <asp:TextBox ID="ScholarshipHolder_FatherOccupationId" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Kardeş Sayısı</label>
                                        <asp:TextBox ID="ScholarshipHolder_NumberOfSiblings" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <% if ((int?)(Session["ViewUser_NumberOfSiblings"]) > 0) %>
                            <%{ %>
                            <% for (int i = 0; i < (int?)(Session["ViewUser_NumberOfSiblings"]); i++) %>
                            <% { %>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Kardeş Adı</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingFirstName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Kardeş Soyadı</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingLastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Çalışıyor mu?</label>
                                        <asp:TextBox ID="ScholarshipHolder_IsSiblingWorking" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Kardeş Mesleği</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingOccupation" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Kardeş Aylık Geliri</label>
                                        <asp:TextBox ID="ScholarshipHolder_SiblingMonthlyIncome" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Birlikte yaşıyor mu?</label>
                                        <asp:TextBox ID="ScholarshipHolder_IslivingTogether" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <% } %>
                            <% }%>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Aylık Gelir</label>
                                        <asp:TextBox ID="ScholarshipHolder_MontlyIncome" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Herhangi bir sağlık problemi var mı?</label>
                                        <div>
                                            <asp:TextBox ID="ScholarshipHolder_HealthCondition" ReadOnly="true" TextMode="MultiLine" Rows="5" Columns="60" Style="resize: none;" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.Donator) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Donator_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Donator_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Donator_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="Donator_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Donator_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Donator_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Meslek</label>
                                        <div>
                                            <asp:TextBox ID="Donator_Occupation" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>İş Yeri</label>
                                        <div>
                                            <% if ((string)(Session["ViewUser_WorkPlace"]) == null) %>
                                            <%{ %>
                                            <asp:TextBox ID="Donator_WorkPlaceTrue" ReadOnly="true" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <asp:TextBox ID="Donator_WorkPlaceFalse" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.Schoolmaster) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Schoolmaster_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Schoolmaster_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Schoolmaster_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="Schoolmaster_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Schoolmaster_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Schoolmaster_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>İl</label>
                                        <asp:TextBox ID="Schoolmaster_City" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>İlçe</label>
                                        <asp:TextBox ID="Schoolmaster_Town" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="Schoolmaster_School" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.HostSchoolTeacher) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="HostSchoolTeacher_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="HostSchoolTeacher_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="HostSchoolTeacher_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="HostSchoolTeacher_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="HostSchoolTeacher_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="HostSchoolTeacher_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>İl</label>
                                        <asp:TextBox ID="HostSchoolTeacher_City" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>İlçe</label>
                                        <asp:TextBox ID="HostSchoolTeacher_Town" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="HostSchoolTeacher_School" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Branş</label>
                                        <asp:TextBox ID="HostSchoolTeacher_Branch" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.Student) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Student_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Student_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Student_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="Student_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Student_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Student_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>İl</label>
                                        <asp:TextBox ID="Student_City" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>İlçe</label>
                                        <asp:TextBox ID="Student_Town" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Okul Adı</label>
                                        <asp:TextBox ID="Student_School" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Eğitim Seviyesi</label>
                                        <asp:TextBox ID="Student_EducationLevel" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Sınıf</label>
                                        <asp:TextBox ID="Student_Class" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Not Ortalaması</label>
                                        <asp:TextBox ID="Student_CumGPA" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.Volunteer) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="Volunteer_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="Volunteer_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="Volunteer_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="Volunteer_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="Volunteer_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="Volunteer_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Öğrenci / Çalışan Bilgisi</label>
                                        <asp:TextBox ID="Volunteer_IsStudent" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <% if ((bool?)(Session["ViewUser_WorkPlace"]) == true) %>
                                    <%{ %>

                                    <div class="col-md-3">
                                        <label>Üniversite</label>
                                        <asp:TextBox ID="Volunteer_University" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Departman</label>
                                        <asp:TextBox ID="Volunteer_Department" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Sınıf</label>
                                        <asp:TextBox ID="Volunteer_Class" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <% }%>
                                    <%else %>
                                    <%{ %>
                                    <div class="col-md-3">
                                        <label>Meslek</label>
                                        <asp:TextBox ID="Volunteer_Occupation" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <% }%>
                                </div>
                            </div>

                            <%} %>

                            <% if (Convert.ToInt32(Session["ViewUser_UserTypeId"].ToString()) == (int)EnumUserType.YonDer) %>
                            <%{ %>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Ad</label>
                                        <asp:TextBox ID="YonDer_FirstName" ReadOnly="true" class="form-control" runat="server"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Soyad</label>
                                        <asp:TextBox ID="YonDer_LastName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>TC Kimlik No</label>
                                        <asp:TextBox ID="YonDer_TCKN" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Doğum Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="YonDer_BirthDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>E-Posta</label>
                                        <asp:TextBox ID="YonDer_Email" ReadOnly="true" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="YonDer_Phone" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Görev Başlangıç Tarihi</label>
                                        <div>
                                            <asp:TextBox ID="YonDer_DutyStartDate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Görev Bitiş Tarihi</label>
                                        <div>
                                            <% if ((DateTime?)(Session["ViewUser_DutyEndDate"]) == null) %>
                                            <%{ %>
                                            <asp:TextBox ID="YonDer_DutyEndDate" ReadOnly="true" Font-Italic="true" Style="color: darkgray;" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                            <%else %>
                                            <%{ %>
                                            <asp:TextBox ID="YonDer_DutyEndDateIsActive" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                            <% }%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%} %>

                            <div class="text-right">
                                <a href="ListUsers.aspx" class="btn btn-default"><i class="icon-arrow-left8 position-left"></i>Kullanıcı Listesine Geri Dön</a>
                            </div>
                        </div>
                    </div>
                    <!-- /profile info -->
                </div>
            </div>
            <!-- /tab content -->

        </div>
    </div>
    <!-- /detached content -->

</asp:Content>

