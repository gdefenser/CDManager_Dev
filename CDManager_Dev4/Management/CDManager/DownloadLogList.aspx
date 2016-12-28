<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="DownloadLogList.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.DownloadLogList" %>

<%@ Register Src="~/Resources/UserControl/CalendarExtender.ascx" TagPrefix="uc1" TagName="CalendarExtender" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    下载日志
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsDownload" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="DownloadLog"
        Select="it.CZYTM,it.CD.CDXH,it.CD.CDMC,it.CD.Book.ZTM,it.CD.Book.ISBN,it.XZSJ,it.IP" OrderBy="it.XZSJ" OnQueryCreated="edsDownload_QueryCreated">
    </asp:EntityDataSource>

    <h3>下载日志</h3>
    <hr />
    <asp:Button ID="btnBack" runat="server" Text="重新查询" OnClick="btnBack_Click" />
    <asp:UpdatePanel ID="upDownloadLog" runat="server">
        <ContentTemplate>

            <center>

                <table border="1" cellspacing="0">
                    <tr>
                        <th>日期关键字:
                        </th>
                        <td>
                            <asp:Label ID="lblDate" runat="server" Text="无"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>图书关键字:
                        </th>
                        <td>
                            <asp:Label ID="lblBook" runat="server" Text="无"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>光盘关键字:
                        </th>
                        <td>
                            <asp:Label ID="lblCD" runat="server" Text="无"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>下载账号关键字:
                        </th>
                        <td>
                            <asp:Label ID="lblCZY" runat="server" Text="无"></asp:Label></td>
                    </tr>
                </table>
                <iframe id="I1" frameborder="0" name="I1" height="25px" width="180px" src="iframe_DownloadLog.aspx"></iframe>
                <hr />
                <asp:ListView ID="lvDownload" runat="server" DataSourceID="edsDownload" OnItemDataBound="lvDownload_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:HyperLink ID="linkCZYTM" runat="server" Text='<%# Eval("CZYTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblXM" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink ID="linkCDMC" runat="server" NavigateUrl='<%# Eval("CDXH","~/Management/CDManager/CDDownloadDetail.aspx?CDXH={0}") %>' Text='<%# Eval("CDMC") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/CDManager/CDDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblXZSJ" runat="server" Text='<%# Eval("XZSJ") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIP" runat="server" Text='<%# Eval("IP") %>'></asp:Label>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有下载记录</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:HyperLink ID="linkCZYTM" runat="server" Text='<%# Eval("CZYTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblXM" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink ID="linkCDMC" runat="server" NavigateUrl='<%# Eval("CDXH","~/Management/CDManager/CDDownloadDetail.aspx?CDXH={0}") %>' Text='<%# Eval("CDMC") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/CDManager/CDDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblXZSJ" runat="server" Text='<%# Eval("XZSJ") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIP" runat="server" Text='<%# Eval("IP") %>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader">
                                            <th id="Th1" runat="server">
                                                <asp:LinkButton ID="headerCZYTM" CommandArgument="CZYTM" CommandName="Sort" runat="server" ForeColor="White">下载账号</asp:LinkButton></th>
                                            <th id="Th2" runat="server">下载人</th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerCDMC" CommandArgument="CDMC" CommandName="Sort" runat="server" ForeColor="White">光盘名称</asp:LinkButton></th>
                                            <th id="Th4" runat="server">
                                                <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                            <th id="Th5" runat="server">
                                                <asp:LinkButton ID="headerXZSJ" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">下载时间</asp:LinkButton></th>
                                            <th id="Th6" runat="server">下载IP</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpDownload" runat="server" PageSize="20">
                                        <Fields>
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    当前
                                            <asp:Label ID="lblPage" runat="server" ForeColor="Red" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>"></asp:Label>
                                                    /<asp:Label ID="lblPageCount" runat="server" Text="<%# Container.TotalRowCount<Container.PageSize?1:Container.TotalRowCount/Container.PageSize+1%>"></asp:Label>
                                                    页
                                                </PagerTemplate>
                                            </asp:TemplatePagerField>
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="False" />
                                            <asp:NumericPagerField PreviousPageText="上十页" ButtonCount="10" NextPageText="下十页" />
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="False" />
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnGo" runat="server" Text="GO" OnCommand="btnGo_Command" />
                                                </PagerTemplate>
                                            </asp:TemplatePagerField>
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
