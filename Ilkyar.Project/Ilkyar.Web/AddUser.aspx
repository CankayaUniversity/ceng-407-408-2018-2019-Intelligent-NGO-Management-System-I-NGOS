<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Ilkyar.Web.AddUser" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Yeni Kullanıcı Ekle</span></h4>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Yeni Kullanıcı Yarat</h5>
                    <div class="heading-elements">
                        <ul class="icons-list">
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">Kullanıcı Tipi</label>
                                <div>
                                    <telerik:RadDropDownList runat="server" ID="UserType" class="form-control" Width="100%" AutoPostBack="True"></telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserType" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                            <% if (Convert.ToInt32(UserType.SelectedValue) == (int)EnumUserType.NGOHead) %>
                            <%{ %>
                            <div class="col-md-6">
                                <label class="text-bold">TC Kimlik No</label>
                                <asp:TextBox ID="NGOHead_TCKN" class="form-control" ValidationGroup="NGOHead" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ValidatorNGOHead_TCKN" runat="server" ControlToValidate="NGOHead_TCKN" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row" id="divNGOName" runat="server" >
                            <div class="col-md-6">
                                <label class="text-bold">Ad</label>
                                <div>
                                    <asp:TextBox ID="NGOHead_FirstName" ValidationGroup="NGOHead" Class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValidatorNGOHead_FirstName" runat="server" ControlToValidate="NGOHead_FirstName" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="text-bold">Soyad</label>
                                <div>
                                    <asp:TextBox ID="NGOHead_LastName" ValidationGroup="NGOHead" Class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValidatorNGOHead_LastName" runat="server" ControlToValidate="NGOHead_LastName" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divNGODate" runat="server" >
                            <div class="col-md-6">
                                <label class="text-bold">Doğum Tarihi</label>
                                <div>
                                    <telerik:RadDatePicker ID="NGOHead_BirthDate" ValidationGroup="NGOHead" class="form-control" runat="server" AutoPostBack="True" MinDate="1940-01-01" Culture="en-US" MaxDate="1999-12-31"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorNGOHead_BirthDate" runat="server" ControlToValidate="NGOHead_BirthDate" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="text-bold">Görev Başlangıç Tarihi</label>
                                <div>
                                    <telerik:RadDatePicker ID="NGOHead_DutyStartDate" ValidationGroup="NGOHead" class="form-control" runat="server" AutoPostBack="True" MinDate="1940-01-01" Culture="en-US"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorNGOHead_DutyStartDate" runat="server" ControlToValidate="NGOHead_DutyStartDate" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">E-posta</label>

                                <div>
                                    <asp:TextBox ID="NGOHead_Email" class="form-control" ValidationGroup="NGOHead" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValidatorNGOHead_Email" runat="server" ControlToValidate="NGOHead_Email" ErrorMessage="Zorunlu alan!" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="text-bold">Telefon</label>
                                <asp:TextBox ID="NGOHead_Phone" class="form-control" ValidationGroup="NGOHead" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="ValidatorNGOHead_Phone" runat="server" ControlToValidate="NGOHead_Phone" ErrorMessage="Zorunlu alan!" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Şifre</label>
                            <asp:TextBox ID="NGOHead_Password" TextMode="Password" ValidationGroup="NGOHead" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ValidatorNGOHead_Password" runat="server" ControlToValidate="NGOHead_Password" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">Şifre Tekrar</label>
                            <asp:TextBox ID="NGOHead_Password2" TextMode="Password" ValidationGroup="NGOHead" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ValidatorNGOHead_Password2" runat="server" ControlToValidate="NGOHead_Password2" ErrorMessage="Zorunlu alan!"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <br />

                    <div class="text-right">
                        <asp:LinkButton ID="buttonCreateNGOHead" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateNGOHead_Click" />
                    </div>

                    <%} %>

                    <% if (Convert.ToInt32(UserType.SelectedValue) == (int)EnumUserType.ProjectManager) %>
                    <%{ %>
                    <div class="col-md-6">
                        <label class="text-bold">TC Kimlik No</label>

                        <asp:TextBox ID="ProjectManager_TCKN" class="form-control" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_TCKN" ErrorMessage="Zorunlu alan!" />--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold">Ad</label>
                        <div>
                            <asp:TextBox ID="ProjectManager_FirstName" Class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label class="text-bold">Soyad</label>
                        <div>
                            <asp:TextBox ID="ProjectManager_LastName" Class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold">Doğum Tarihi</label>
                        <div>
                            <telerik:RadDatePicker ID="ProjectManager_BirthDate" class="form-control" runat="server" AutoPostBack="True" MinDate="1940-01-01">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

                                <DateInput DisplayDateFormat="d.MM.yyyy" DateFormat="d.MM.yyyy" LabelWidth="40%" AutoPostBack="True">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                                    <FocusedStyle Resize="None"></FocusedStyle>

                                    <DisabledStyle Resize="None"></DisabledStyle>

                                    <InvalidStyle Resize="None"></InvalidStyle>

                                    <HoveredStyle Resize="None"></HoveredStyle>

                                    <EnabledStyle Resize="None"></EnabledStyle>
                                </DateInput>

                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                            <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_BirthDate" ErrorMessage="Zorunlu alan!" />--%>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <label class="text-bold">Görev Başlangıç Tarihi</label>
                        <div>
                            <telerik:RadDatePicker ID="ProjectManager_DutyStartDate" class="form-control" runat="server" AutoPostBack="True" SelectedDate="2019-04-12"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_DutyStartDate" ErrorMessage="Zorunlu alan!" />
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label class="text-bold">E-posta</label>

                        <div>
                            <asp:TextBox ID="ProjectManager_Email" class="form-control" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_Email" ErrorMessage="Zorunlu alan!" />--%>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label class="text-bold">Telefon</label>
                        <asp:TextBox ID="ProjectManager_Phone" class="form-control" runat="server"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectManager_Phone" ErrorMessage="Zorunlu alan!" />--%>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label class="text-bold">Şifre</label>
                    <asp:TextBox ID="ProjectManager_Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="text-bold">Şifre Tekrar</label>
                    <asp:TextBox ID="ProjectManager_Password2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <br />

            <div class="text-right">
                <asp:LinkButton ID="buttonCreateProjectManager" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateProjectManager_Click" />

            </div>

            <%} %>
            <% if (Convert.ToInt32(UserType.SelectedValue) == (int)EnumUserType.ScholarshipCommittee) %>
            <%{ %>
            <div class="col-md-6">
                <label class="text-bold">TC Kimlik No</label>

                <asp:TextBox ID="ScholarshipCommittee_TCKN" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_TCKN" ErrorMessage="Zorunlu alan!" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label class="text-bold">Ad</label>
                <div>
                    <asp:TextBox ID="ScholarshipCommittee_FirstName" Class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <label class="text-bold">Soyad</label>
                <div>
                    <asp:TextBox ID="ScholarshipCommittee_LastName" Class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <label class="text-bold">Doğum Tarihi</label>
                <div>
                    <telerik:RadDatePicker ID="ScholarshipCommittee_BirthDate" class="form-control" runat="server" AutoPostBack="True" MinDate="1940-01-01"></telerik:RadDatePicker>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_BirthDate" ErrorMessage="Zorunlu alan!" />
                </div>
            </div>

            <div class="col-md-3">
                <label class="text-bold">Görev Başlangıç Tarihi</label>
                <div>
                    <telerik:RadDatePicker ID="ScholarshipCommittee_DutyStartDate" class="form-control" runat="server" AutoPostBack="True" SelectedDate="2019-04-12"></telerik:RadDatePicker>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_DutyStartDate" ErrorMessage="Zorunlu alan!" />
                </div>
            </div>
            <div class="col-md-6">
                <label class="text-bold">Ünvan</label>
                <div>
                    <asp:TextBox ID="ScholarshipCommittee_Title" Class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-md-6">
                <label class="text-bold">E-posta</label>

                <div>
                    <asp:TextBox ID="ScholarshipCommittee_Email" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_Email" ErrorMessage="Zorunlu alan!" />
                </div>
            </div>
            <div class="col-md-6">
                <label class="text-bold">Telefon</label>
                <asp:TextBox ID="ScholarshipCommittee_Phone" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ScholarshipCommittee_Phone" ErrorMessage="Zorunlu alan!" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Şifre</label>
            <asp:TextBox ID="ScholarshipCommittee_Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Şifre Tekrar</label>
            <asp:TextBox ID="ScholarshipCommittee_Password2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>

    <br />

    <div class="text-right">
        <asp:LinkButton ID="buttonCreateScholarshipCommittee" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateScholarshipCommittee_Click" />

    </div>

    <%} %>

    <% if (Convert.ToInt32(UserType.SelectedValue) == (int)EnumUserType.YonDer) %>
    <%{ %>
    <div class="col-md-6">
        <label class="text-bold">TC Kimlik No</label>

        <asp:TextBox ID="YonDer_TCKN" class="form-control" runat="server"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_TCKN" ErrorMessage="Zorunlu alan!" />--%>
    </div>
    </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">Ad</label>
                                <div>
                                    <asp:TextBox ID="YonDer_FirstName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="text-bold">Soyad</label>
                                <div>
                                    <asp:TextBox ID="YonDer_LastName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Doğum Tarihi</label>
            <div>
                <telerik:RadDatePicker ID="YonDer_BirthDate" class="form-control" runat="server" AutoPostBack="True" MinDate="1940-01-01"></telerik:RadDatePicker>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_BirthDate" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>

        <div class="col-md-6">
            <label class="text-bold">Görev Başlangıç Tarihi</label>
            <div>
                <telerik:RadDatePicker ID="YonDer_DutyStartDate" class="form-control" runat="server"></telerik:RadDatePicker>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_DutyStartDate" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">E-posta</label>

            <div>
                <asp:TextBox ID="YonDer_Email" class="form-control" runat="server"></asp:TextBox>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_Email" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Telefon</label>
            <asp:TextBox ID="YonDer_Phone" class="form-control" runat="server"></asp:TextBox>
            <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="YonDer_Phone" ErrorMessage="Zorunlu alan!" />--%>
        </div>
    </div>
    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Şifre</label>
                            <asp:TextBox ID="YonDer_Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">Şifre Tekrar</label>
                            <asp:TextBox ID="YonDer_Password2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

    <br />

    <div class="text-right">
        <asp:LinkButton ID="buttonCreateYonDer" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateYonDer_Click" />

    </div>

    <%} %>
    <% if (Convert.ToInt32(UserType.SelectedValue) == (int)EnumUserType.Schoolmaster) %>
    <%{ %>
    <div class="col-md-6">
        <label class="text-bold">TC Kimlik No</label>

        <asp:TextBox ID="Schoolmaster_TCKN" class="form-control" runat="server"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_TCKN" ErrorMessage="Zorunlu alan!" />--%>
    </div>
    </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">Ad</label>
                                <div>
                                    <asp:TextBox ID="Schoolmaster_FirstName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="text-bold">Soyad</label>
                                <div>
                                    <asp:TextBox ID="Schoolmaster_LastName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
    <div class="row">
        <div class="col-md-3">
            <label class="text-bold">Doğum Tarihi</label>
            <div>
                <telerik:RadDatePicker ID="Schoolmaster_BirthDate" class="form-control" runat="server" AutoPostBack="True" MinDate="1940-01-01"></telerik:RadDatePicker>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_BirthDate" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>

        <div class="col-md-3">
            <label class="text-bold">Görev Başlangıç Tarihi</label>
            <div>
                <telerik:RadDatePicker ID="Schoolmaster_DutyStartDate" class="form-control" runat="server" AutoPostBack="True" SelectedDate="2019-04-12"></telerik:RadDatePicker>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_DutyStartDate" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Okul Adı</label>
            <div>
                <telerik:RadDropDownList runat="server" ID="Schoolmaster_School" class="form-control" Width="100%">
                </telerik:RadDropDownList>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">E-posta</label>

            <div>
                <asp:TextBox ID="Schoolmaster_Email" class="form-control" runat="server"></asp:TextBox>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_Email" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Telefon</label>
            <asp:TextBox ID="Schoolmaster_Phone" class="form-control" runat="server"></asp:TextBox>
            <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="Schoolmaster_Phone" ErrorMessage="Zorunlu alan!" />--%>
        </div>
    </div>
    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="text-bold">Şifre</label>
                            <asp:TextBox ID="Schoolmaster_Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="text-bold">Şifre Tekrar</label>
                            <asp:TextBox ID="Schoolmaster_Password2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

    <br />

    <div class="text-right">
        <asp:LinkButton ID="buttonCreateSchoolmaster" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateSchoolmaster_Click" />

    </div>

    <%} %>
    <% if (Convert.ToInt32(UserType.SelectedValue) == (int)EnumUserType.HostSchoolTeacher) %>
    <%{ %>
    <div class="col-md-6">
        <label class="text-bold">TC Kimlik No</label>

        <asp:TextBox ID="HostSchoolTeacher_TCKN" class="form-control" runat="server"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_TCKN" ErrorMessage="Zorunlu alan!" />--%>
    </div>
    </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">Ad</label>
                                <div>
                                    <asp:TextBox ID="HostSchoolTeacher_FirstName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="text-bold">Soyad</label>
                                <div>
                                    <asp:TextBox ID="HostSchoolTeacher_LastName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Doğum Tarihi</label>
            <div>
                <telerik:RadDatePicker ID="HostSchoolTeacher_BirthDate" class="form-control" runat="server" AutoPostBack="True" MinDate="1940-01-01"></telerik:RadDatePicker>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_BirthDate" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>

        <div class="col-md-6">
            <label class="text-bold">Görev Başlangıç Tarihi</label>
            <div>
                <telerik:RadDatePicker ID="HostSchoolTeacher_DutyStartDate" class="form-control" runat="server" AutoPostBack="True" SelectedDate="2019-04-12"></telerik:RadDatePicker>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_DutyStartDate" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Okul Adı</label>
            <div>
                <telerik:RadDropDownList runat="server" ID="HostSchoolTeacher_School" class="form-control" Width="100%">
                </telerik:RadDropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Branşı</label>
            <div>
                <telerik:RadDropDownList runat="server" ID="HostSchoolTeacher_Branch" class="form-control" Width="100%">
                </telerik:RadDropDownList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">E-posta</label>

            <div>
                <asp:TextBox ID="HostSchoolTeacher_Email" class="form-control" runat="server"></asp:TextBox>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_Email" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Telefon</label>
            <asp:TextBox ID="HostSchoolTeacher_Phone" class="form-control" runat="server"></asp:TextBox>
            <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="HostSchoolTeacher_Phone" ErrorMessage="Zorunlu alan!" />--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Şifre</label>
            <asp:TextBox ID="HostSchoolTeacher_Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Şifre Tekrar</label>
            <asp:TextBox ID="HostSchoolTeacher_Password2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>

    <br />

    <div class="text-right">
        <asp:LinkButton ID="buttonCreateHostSchoolTeacher" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateHostSchoolTeacher_Click" />

    </div>

    <%} %>
    <% if (Convert.ToInt32(UserType.SelectedValue) == (int)EnumUserType.Student) %>
    <%{ %>
    <div class="col-md-6">
        <label class="text-bold">TC Kimlik No</label>

        <asp:TextBox ID="Student_TCKN" class="form-control" runat="server"></asp:TextBox>
        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="Student_TCKN" ErrorMessage="Zorunlu alan!" />--%>
    </div>
    </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-bold">Ad</label>
                                <div>
                                    <asp:TextBox ID="Student_FirstName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="text-bold">Soyad</label>
                                <div>
                                    <asp:TextBox ID="Student_LastName" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Doğum Tarihi</label>
            <div>
                <telerik:RadDatePicker ID="Student_BirthDate" class="form-control" runat="server" AutoPostBack="True"></telerik:RadDatePicker>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_BirthDate" ErrorMessage="Zorunlu alan!" />
            </div>
        </div>

        <div class="col-md-3">
            <label class="text-bold">Sınıf</label>
            <asp:TextBox ID="Student_Class" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_Class" ErrorMessage="Zorunlu alan!" />
        </div>
        <div class="col-md-3">
            <label class="text-bold">Not Ortalaması</label>
            <asp:TextBox ID="Student_CumGPA" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_CumGPA" ErrorMessage="Zorunlu alan!" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Okul Adı</label>
            <asp:TextBox ID="TextBoxSchool" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Eğitim Seviyesi</label>
            <asp:TextBox ID="Student_EducationLevel" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_EducationLevel" ErrorMessage="Zorunlu alan!" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">E-posta</label>

            <div>
                <asp:TextBox ID="Student_Email" class="form-control" runat="server"></asp:TextBox>
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="Student_Email" ErrorMessage="Zorunlu alan!" />--%>
            </div>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Telefon</label>
            <asp:TextBox ID="Student_Phone" class="form-control" runat="server"></asp:TextBox>
            <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="Student_Phone" ErrorMessage="Zorunlu alan!" />--%>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label class="text-bold">Şifre</label>
            <asp:TextBox ID="Student_Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label class="text-bold">Şifre Tekrar</label>
            <asp:TextBox ID="Student_Password2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>

    <br />

    <div class="text-right">
        <asp:LinkButton ID="buttonCreateStudent" runat="server" type="button" class="btn btn-success" Text='<i class="icon-checkmark position-left"></i>Kaydet' OnClick="buttonCreateStudent_Click" />

    </div>

    <%} %>

    <div class="text-left">
        <asp:Label ID="labelErrorMessage" class="text-semibold" runat="server" Visible="false" />
    </div>

    </div>
            </div>
        </div>
    </div>



</asp:Content>
