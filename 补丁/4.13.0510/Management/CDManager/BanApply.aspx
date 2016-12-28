<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="BanApply.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.BanApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘资源申请申请拒绝
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>光盘资源申请申请拒绝</h2>
    <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
    <hr />
    <center>
        <asp:UpdatePanel ID="upBanApply" runat="server">
            <ContentTemplate>
                <table class="statistics" cellspacing="0" cellpadding="4">
                    <tr>
                        <th>ISBN</th>
                        <th>图书名称</th>
                        <th>申请数</th>
                        <th>申请时间</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblISBN" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:HyperLink ID="linkZTM" runat="server"></asp:HyperLink></td>
                        <td>
                            <asp:HyperLink ID="linkApply" runat="server"></asp:HyperLink></td>
                        <td>
                            <asp:Label ID="lblSQSJ" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
                <table class="input">
                    <tr>
                        <th>拒绝理由</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="dropMessage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropMessage_SelectedIndexChanged">
                                <asp:ListItem>==选择拒绝理由==</asp:ListItem>
                                <asp:ListItem>资源已经上传</asp:ListItem>
                                <asp:ListItem>服务器磁盘空间已满</asp:ListItem>
                                <asp:ListItem>系统需要维护</asp:ListItem>
                                <asp:ListItem>其他</asp:ListItem>
                            </asp:DropDownList><br />
                            <asp:TextBox ID="txtMessage" runat="server" Columns="35" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnClick" runat="server" Text=" 确 定 " OnClick="btnClick_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
