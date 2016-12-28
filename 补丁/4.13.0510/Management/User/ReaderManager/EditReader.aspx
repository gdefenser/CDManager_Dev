﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="EditReader.aspx.cs" Inherits="CDManager_Dev4.Management.User.ReaderManager.EditReader" %>

<%@ Register Src="~/Resources/UserControl/CalendarExtender.ascx" TagPrefix="uc1" TagName="CalendarExtender" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑读者
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>编辑读者</h2>
    <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
    <hr />
    <center>
        <table class="input">
            <tr>
                <th class="decol">登录读者:
                </th>
                <td>
                    <asp:Label ID="lblDZTM" runat="server"></asp:Label>
                </td>
                <th class="decol">*姓名:
                </th>
                <td>
                    <asp:TextBox ID="txtXM" runat="server" MaxLength="20"></asp:TextBox><br />
                    <asp:CustomValidator ID="valXM" runat="server" Font-Bold="True" ForeColor="Maroon"
                        ErrorMessage="请输入姓名" Display="Dynamic"></asp:CustomValidator>
                </td>
                <th class="decol">密码:
                </th>
                <td>
                    <asp:TextBox ID="txtMM" runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol">性别:
                </th>
                <td align="left">
                    <asp:DropDownList ID="dropXB" runat="server">
                        <asp:ListItem>=选择性别=</asp:ListItem>
                        <asp:ListItem>男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th class="decol">*有效日期:
                </th>
                <td>
                    <uc1:CalendarExtender runat="server" ID="CalendarExtenderYXRQ" />
                    <br />
                    <asp:CustomValidator ID="valYXRQ" runat="server" Font-Bold="True" ForeColor="Maroon"
                        ErrorMessage="请选择正确有效日期" Display="Dynamic"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="left">
                    <table>
                        <tr>
                            <th class="decol">*读者类型:
                            </th>
                            <td>
                                <asp:DropDownList ID="dropDZLX" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropDZLX_SelectedIndexChanged"></asp:DropDownList><br />
                                <asp:TextBox ID="txtDZLX" runat="server" Visible="false"></asp:TextBox>
                                <asp:CustomValidator ID="valDZLX" runat="server" Font-Bold="True" ForeColor="Maroon"
                                    ErrorMessage="读者类型错误" Display="Dynamic"></asp:CustomValidator>
                            </td>
                            <th class="decol">*一级单位:
                            </th>
                            <td>
                                <asp:DropDownList ID="dropYJDW" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropYJDW_SelectedIndexChanged"></asp:DropDownList><br />
                                <asp:TextBox ID="txtYJDW" runat="server" Visible="false"></asp:TextBox>
                                <asp:CustomValidator ID="valYJDW" runat="server" Font-Bold="True" ForeColor="Maroon"
                                    ErrorMessage="一级单位错误" Display="Dynamic"></asp:CustomValidator>
                            </td>
                            <th class="decol">*二级单位:
                            </th>
                            <td>
                                <asp:DropDownList ID="dropEJDW" runat="server" OnSelectedIndexChanged="dropEJDW_SelectedIndexChanged"></asp:DropDownList><br />
                                <asp:TextBox ID="txtEJDW" runat="server" Visible="false"></asp:TextBox>
                                <asp:CustomValidator ID="valEJDW" runat="server" Font-Bold="True" ForeColor="Maroon"
                                    ErrorMessage="二级单位错误" Display="Dynamic"></asp:CustomValidator>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnApplyDownload" runat="server" Text="用户申请与下载记录" OnClick="btnApplyDownload_Click" /><br />
                    <asp:Button ID="btnBan" runat="server" Text="禁止用户登录" OnClick="btnBan_Click" />
                    <asp:Button ID="btnMessage" runat="server" Text="发送消息" OnClick="btnMessage_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnEdit" runat="server" Text="  确  定  "
                        OnClick="btnEdit_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="  重  置  "
                        OnClick="btnReset_Click" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
