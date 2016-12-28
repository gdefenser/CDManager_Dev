<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="DownloadSearch.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.DownloadSearch" %>

<%@ Register Src="~/Resources/UserControl/CalendarExtender.ascx" TagPrefix="uc1" TagName="CalendarExtender" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    下载日志查询
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>下载日志查询</h3>
    <hr />
    <center>
        <table border="1" cellspacing="0">
            <tr align="left">
                <th>
                    <asp:checkbox id="chkDate" runat="server" text="按下载时间搜索:" />
                </th>
                <td>从<uc1:CalendarExtender runat="server" ID="CalendarExtenderFrom" />
                    到<uc1:CalendarExtender runat="server" ID="CalendarExtenderTo" />
                </td>
            </tr>
            <tr align="left">
                <th>
                    <asp:checkbox id="chkBook" runat="server" text="按图书名/ISBN搜索:" />
                </th>
                <td>
                    <asp:textbox id="txtBook" runat="server"></asp:textbox>
                </td>
            </tr>
            <tr align="left">
                <th>
                    <asp:checkbox id="chkCD" runat="server" text="按光盘名/光盘序号搜索:" />
                </th>
                <td>
                    <asp:textbox id="txtCD" runat="server"></asp:textbox>
                </td>
            </tr>
            <tr align="left">
                <th>
                    <asp:checkbox id="chkCZYTM" runat="server" text="按下载人搜索:" />
                </th>
                <td>
                    <asp:textbox id="txtCZY" runat="server"></asp:textbox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:button id="btnSelect" runat="server" text=" 确 定 " OnClick="btnSelect_Click" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
