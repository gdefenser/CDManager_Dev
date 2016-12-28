<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="ApplyStatistics.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.ApplyStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘资源申请统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>光盘资源申请统计</h2>
    <hr />
    <center>
        <table class="statistics" cellspacing="0" cellpadding="4">
            <tr>
                <th>总申请数</th>
                <th>本月申请数</th>
                <th>申请处理率</th>
                <th>申请成功率</th>
                <th>申请忽略率</th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCount" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="lblMouthCount" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="lblApplyedCount" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="lblSuccessCount" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="lblBanCount" runat="server"></asp:Label></td>
            </tr>
        </table>
        <hr />
        <table class="statistics" cellspacing="0" cellpadding="4">
            <tr>
                <th>最多申请资源:</th>
                <td>
                    <asp:HyperLink ID="linkMaxBook" runat="server"></asp:HyperLink></td>
                <th>发送申请最多读者:</th>
                <td>
                    <asp:Label ID="lblMaxReader" runat="server"></asp:Label></td>
                <th>未处理申请数:</th>
                <td><asp:HyperLink ID="linkNoApply" runat="server"></asp:HyperLink></td>
            </tr>
            <tr>
                <th>本月最多申请资源:</th>
                <td><asp:HyperLink ID="linkMouthMaxBook" runat="server"></asp:HyperLink></td>
                <th>本月发送申请最多读者:</th>
                <td><asp:HyperLink ID="linkMouthMaxReader" runat="server"></asp:HyperLink></td>
                <th>日均申请数:</th>
                <td><asp:Label ID="lblDayCount" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>最近24小时申请数:</th>
                <td><asp:Label ID="lbl24Count" runat="server"></asp:Label></td>
                <th>处理最多申请管理员:</th>
                <td><asp:Label ID="lblMaxAdmin" runat="server"></asp:Label></td>
                <th>最近申请资源:</th>
                <td><asp:HyperLink ID="linkNewApply" runat="server"></asp:HyperLink></td>
            </tr>
        </table>
    </center>
</asp:Content>
