<%@ Page Title="Gelen Kutusu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="Ilkyar.Web.Inbox" %>

<%@ Import Namespace="Ilkyar.Web.Helpers" %>
<%@ Import Namespace="Ilkyar.Contracts.Entities.Enums" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    <h4><span class="text-semibold">Gelen Kutusu</span></h4>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-3">
            <div class="panel panel-flat">
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <label class="text-bold">Kullanıcı</label>
                                <telerik:RadDropDownList ID="UserList" DefaultMessage="Seçiniz" runat="server" Width="100%" AutoPostBack="true" OnItemSelected="UserList_ItemSelected"></telerik:RadDropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-9" id="divConversation" runat="server" visible="false">
            <div class="panel panel-flat">
                <div class="panel-body">
                    <ul class="media-list chat-list content-group">
                        <asp:ListView ID="ConversationList" runat="server">
                            <EmptyDataTemplate>
                                <h5 class="panel-title">Henüz mesajlaşma yok!</h5>
                            </EmptyDataTemplate>
                            <ItemTemplate>

                                <li class="media reversed" runat="server" visible='<%# Eval("MessageTypeId").ToString() == ((int)EnumMessageType.Gonderilen).ToString() ? true : false %>'>
                                    <div class="media-body">
                                        <div class="media-content">
                                            <asp:Label runat="server"><%# Eval("Message") %></asp:Label>
                                        </div>
                                        <span class="media-annotation display-block mt-10"><%# Eval("Date") %> - <%# UserHelper.FirstLastName %></span>
                                        <asp:LinkButton ID="deleteMessage" AccessKey='<%# Eval("Id") %>' runat="server" OnClick="deleteMessage_Click">Mesajı Sil</asp:LinkButton>
                                    </div>

                                    <div class="media-right">
                                        <a href="assets/images/placeholder.jpg">
                                            <img src="assets/images/placeholder.jpg" class="img-circle img-md" alt="">
                                        </a>
                                    </div>
                                </li>

                                <li class="media" runat="server" visible='<%# Eval("MessageTypeId").ToString() == ((int)EnumMessageType.Gelen).ToString() ? true : false %>'>
                                    <div class="media-left">
                                        <a href="assets/images/placeholder.jpg">
                                            <img src="assets/images/placeholder.jpg" class="img-circle img-md" alt="">
                                        </a>
                                    </div>

                                    <div class="media-body">
                                        <div class="media-content">
                                            <asp:Label runat="server"><%# Eval("Message") %></asp:Label>
                                        </div>
                                        <span class="media-annotation display-block mt-10"><%# Eval("Date") %> - <%# Eval("UserFullName") %></span>
                                    </div>
                                </li>

                            </ItemTemplate>
                        </asp:ListView>
                    </ul>

                    <div class="form-group">
                        <telerik:RadTextBox ID="Message" runat="server" class="form-control content-group" TextMode="MultiLine" Rows="3" Columns="1" Width="100%"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Message" ValidationGroup="ConversationGroup" ErrorMessage="Zorunlu alan!" />
                    </div>

                    <div class="text-right">
                        <asp:LinkButton ID="buttonSendMessage" runat="server" OnClick="buttonSendMessage_Click" ValidationGroup="ConversationGroup" type="button" class="btn bg-teal-400 btn-labeled btn-labeled-right"><b><i class="icon-circle-right2"></i></b>Gönder</asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
