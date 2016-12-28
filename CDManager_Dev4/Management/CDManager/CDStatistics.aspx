<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="CDStatistics.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.CDStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘库统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>光盘库统计</h2>
    <hr />
    <center>
        <table class="statistics" cellspacing="0" cellpadding="4">
            <tr>
                <th>含光盘图书数</th>
                <th>光盘库光盘数</th>
                <th>在线光盘数</th>
                <th>光盘库更新时间</th>
                <th>申请数</th>
                <th>下载数</th>
                <th>本月申请数</th>
                <th>本月下载数</th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBookCount" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="lblCDCount" runat="server"></asp:Label></td>
                <td>
                    <asp:HyperLink ID="linkOnlineCount" runat="server" NavigateUrl=""></asp:HyperLink>
                </td>
                <td>
                    <asp:Label ID="lblTime" runat="server"></asp:Label></td>
                <td>
                    <asp:HyperLink ID="linkApply" runat="server"></asp:HyperLink></td>
                <td>
                    <asp:HyperLink ID="linkDownload" runat="server"></asp:HyperLink></td>
                <td>
                    <asp:Label ID="lblMouthApply" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="lblMouthDownload" runat="server"></asp:Label></td>
            </tr>
        </table>
        <hr />
        <asp:Chart ID="chartCD" runat="server" Width="900px">
            <Series>
                <asp:Series Name="Series1" ChartType="Line"></asp:Series>
                <asp:Series Name="Series2" ChartType="Line"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <hr />
        <h3>光盘资源申请统计</h3>
        <table class="statistics" cellspacing="0" cellpadding="4">
            <tr>
                <th colspan="2">申请处理率</th>
                <th colspan="2">申请成功率</th>
                <th colspan="2">申请忽略率</th>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblApplyedCount" runat="server"></asp:Label></td>
                <td colspan="2">
                    <asp:Label ID="lblSuccessCount" runat="server"></asp:Label></td>
                <td colspan="2">
                    <asp:Label ID="lblBanCount" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>最多申请资源:</th>
                <td>
                    <asp:HyperLink ID="linkMaxApply" runat="server"></asp:HyperLink></td>
                <th>本月最多申请资源:</th>
                <td>
                    <asp:HyperLink ID="linkMouthMaxApply" runat="server"></asp:HyperLink></td>
                <th>未处理申请数:</th>
                <td>
                    <asp:HyperLink ID="linkNoApply" runat="server"></asp:HyperLink></td>
            </tr>
            <tr>
                <th>发送申请最多读者:</th>
                <td>
                    <asp:HyperLink ID="linkMaxReader" runat="server"></asp:HyperLink></td>
                <th>本月发送申请最多读者:</th>
                <td>
                    <asp:HyperLink ID="linkMonthMaxReader" runat="server"></asp:HyperLink>
                </td>
                <th>日均申请数:</th>
                <td>
                    <asp:Label ID="lblDayCount" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>最近24小时申请数:</th>
                <td>
                    <asp:Label ID="lbl24Count" runat="server"></asp:Label></td>
                <th>处理最多申请管理员:</th>
                <td>
                    <asp:HyperLink ID="linkMaxAdmin" runat="server"></asp:HyperLink>
                </td>
                <th>最近申请资源:</th>
                <td>
                    <asp:HyperLink ID="linkNewlyApply" runat="server"></asp:HyperLink></td>
            </tr>
        </table>
        <hr />
        <h3>光盘资源下载统计</h3>
        <table class="statistics" cellspacing="0" cellpadding="4">

            <tr>
                <th>最多下载资源:</th>
                <td>
                    <asp:HyperLink ID="linkMaxDownload" runat="server"></asp:HyperLink></td>
                <th>本月最多下载资源:</th>
                <td>
                    <asp:HyperLink ID="linkMouthMaxDownload" runat="server"></asp:HyperLink></td>
                <th>最近下载资源:</th>
                <td>
                    <asp:HyperLink ID="linkNewlyDownload" runat="server"></asp:HyperLink></td>
            </tr>
            <tr>
                <th>最多下载人:</th>
                <td>
                    <asp:HyperLink ID="linkMaxDownloadReader" runat="server"></asp:HyperLink></td>
                <th>本月最多下载人:</th>
                <td>
                    <asp:HyperLink ID="linkMonthMaxDownloadReader" runat="server"></asp:HyperLink>
                </td>
                <th>日均下载数:</th>
                <td>
                    <asp:Label ID="lblDayDownloadCount" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>最近24小时下载数:</th>
                <td>
                    <asp:Label ID="lbl24DownloadCount" runat="server"></asp:Label></td>
                <th>申请/下载比:</th>
                <td>
                    <asp:Label ID="lblApplyDownload" runat="server"></asp:Label></td>
                <th>下载读者占总数:</th>
                <td>
                    <asp:Label ID="lblDownloadTotal" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
