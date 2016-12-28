<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="CDManager_Dev4.Management.Message.NewMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    发送新消息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>发送新消息</h2>
    <hr />
    <center>
        <asp:UpdatePanel ID="upNewMessage" runat="server">
            <ContentTemplate>
                <table class="input">
                    <tr>
                        <th>收信人(学号/工号):</th>
                        <td align="left">
                            <asp:TextBox ID="txtTM" runat="server"></asp:TextBox><br />
                            <asp:CustomValidator ID="valTM" runat="server" Font-Bold="True" ForeColor="Maroon"
                                Display="Dynamic"></asp:CustomValidator></td>
                    </tr>
                    <tr>
                        <th>收信人类型:</th>
                        <td align="left">
                            <asp:DropDownList ID="dropSXRLX" runat="server">
                                <asp:ListItem>=收信人类型=</asp:ListItem>
                                <asp:ListItem Value="reader">读者</asp:ListItem>
                                <asp:ListItem Value="admin">管理员</asp:ListItem>
                            </asp:DropDownList><br />
                            <asp:CustomValidator ID="valSXRLX" runat="server" Font-Bold="True" ForeColor="Maroon"
                                Display="Dynamic"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="2">消息内容</th>
                    </tr>
                   <%-- <tr>
                        <td colspan="2" align="center">输入格式:[URL]需要的URL[/URL]标记<br />
                            <asp:Button ID="btnURL" runat="server" Text="插入URL" OnClick="btnURL_Click" /></td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtXXNR" runat="server" TextMode="MultiLine" Columns="40" Rows="8"></asp:TextBox>
                            <br />
                            <asp:CustomValidator ID="valXXNR" runat="server" Font-Bold="True" ForeColor="Maroon"
                                Display="Dynamic"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnNew" runat="server" Text=" 确  定 " OnClick="btnNew_Click" />
                            <asp:Button ID="btnReset" runat="server" Text=" 重  置 " OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
