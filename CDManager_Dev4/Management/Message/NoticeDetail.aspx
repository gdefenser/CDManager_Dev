<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="NoticeDetail.aspx.cs" Inherits="CDManager_Dev4.Management.Message.NoticeDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    公告明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div style="margin-top: 10px; margin-right: 20px; margin-left: 20px">
        <input id="btnBack" type="button" value="返回" onclick="javascript: window.history.back(-1);" />
        <hr />
        <center>
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h2>
            <asp:Label ID="lblLKSJTitle" runat="server" Text="Label" Font-Size="Small" ForeColor="#999999"></asp:Label></center>
        <hr />
        <asp:Panel ID="panelDetail" runat="server" Font-Size="Small">
            <div style="margin-right: 20%; margin-left: 20%">
                <asp:Label ID="lblGGDX" runat="server" Text="Label"></asp:Label>:<br />
                <p style="text-indent: 2em;">
                    <asp:Label ID="lblGGNR" runat="server" Text="Label" Height="300px"></asp:Label>
                </p>
                <asp:Panel ID="panelDownload" runat="server" Visible="false">
                    附件:<asp:HyperLink ID="linkDownload" runat="server"></asp:HyperLink>
                </asp:Panel>
                <div style="text-align: right">
                    <asp:Label ID="lblLKR" runat="server" Text="Label"></asp:Label><br />
                    <asp:Label ID="lblLKSJ" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <hr />
            上一篇:<asp:HyperLink ID="linkPrev" runat="server"></asp:HyperLink><br />
            下一篇:<asp:HyperLink ID="linkNext" runat="server"></asp:HyperLink>
        </asp:Panel>
    </div>
</asp:Content>
