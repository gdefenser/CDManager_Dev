<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="DownloadLogList.aspx.cs" Inherits="CDManager_Dev4.Management.User.ReaderManager.DownloadLogList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    读者下载日志
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsBook" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False"
         CommandText="select b.BookID,b.ISBN,b.ZTM,b.ZRZ,b.CBS,c.ZXZT from Book as b left join CD as c on b.BookID=c.BookID 
where c.CDID in (select value d.CDID from DownloadLog as d) group by b.BookID,b.ISBN,b.ZTM,b.ZRZ,b.CBS,c.ZXZT" OrderBy="it.ISBN">
    </asp:EntityDataSource>
    <h3>读者下载日志</h3>
    <hr />
    <asp:UpdatePanel ID="upDownloadLog" runat="server">
        <ContentTemplate>
            <center>
                ISBN:<asp:TextBox ID="txtSelect" runat="server"></asp:TextBox>
                <asp:Button ID="btnSelect" runat="server" Text="查找记录" OnClick="btnSelect_Click" /><br />
                <hr />
                 <asp:ListView ID="lvBook" runat="server"  DataSourceID="edsBook" OnItemDataBound="lvBook_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                     <asp:Button ID="btnBookDetail" runat="server" Text="下载明细" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnBookDetail_Command" />
                            </td>
                            <td>
                                <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblDownloadApply" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server"/>
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
                                    <asp:Button ID="btnBookDetail" runat="server" Text="下载明细" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnBookDetail_Command" />
                            </td>
                            <td>
                                <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblDownloadApply" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader">
                                            <th id="Th1" runat="server"></th>
                                            <th id="Th2" runat="server">
                                                <asp:LinkButton ID="headerISBN" CommandArgument="ISBN" CommandName="Sort" runat="server" ForeColor="White">ISBN</asp:LinkButton></th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                            <th id="Th7" runat="server">
                                                下载数</th>
                                            <th id="Th8" runat="server"><asp:LinkButton ID="headerZXZT" CommandArgument="ZXZT" CommandName="Sort" runat="server" ForeColor="White">光盘资源状态</asp:LinkButton></th>
                                            <th id="Th9" runat="server">
                                                <asp:LinkButton ID="headerFFSJ" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">光盘操作时间</asp:LinkButton></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpBook" runat="server" PageSize="15">
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
