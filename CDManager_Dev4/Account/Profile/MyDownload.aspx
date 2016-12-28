<%@ Page Title="我的下载" Language="C#" MasterPageFile="~/Resources/Masters/Profile.master"
    AutoEventWireup="true" CodeBehind="MyDownload.aspx.cs" Inherits="CDManager_Dev4.Account.Profile.MyDownload" %>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    我的下载
</asp:Content>  
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsMyDownload" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="DownloadLog"
        Where="it.CZYTM=@CZYTM" Select="it.CD.CDMC,it.CD.Book.ZTM,it.CD.Book.CBS,it.CD.Book.ZRZ,it.CD.Book.FLTM,it.CD.ISBN,it.XZSJ,it.CZYTM"
        OrderBy="it.XZSJ desc">
        <WhereParameters>
            <asp:SessionParameter DbType="String" Name="CZYTM" SessionField="CZYTM" />
        </WhereParameters>
    </asp:EntityDataSource>
    <h3>
        我的下载</h3>
    <hr />
        <center>
                   <asp:UpdatePanel ID="upMyDownload" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvMyDownload" runat="server" DataKeyNames="ISBN" DataSourceID="edsMyDownload" OnItemDataBound="lvMyDownload_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/BookDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="CBSLabel" runat="server" Text='<%# Eval("CBS") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ZRZLabel" runat="server" Text='<%# Eval("ZRZ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FLTMLabel" runat="server" Text='<%# Eval("FLTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("XZSJ") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有你的下载记录</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                           <td>
                                <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/BookDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="CBSLabel" runat="server" Text='<%# Eval("CBS") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ZRZLabel" runat="server" Text='<%# Eval("ZRZ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FLTMLabel" runat="server" Text='<%# Eval("FLTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("XZSJ") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader" >
                                            <th id="Th1" runat="server">
                                                <asp:LinkButton ID="headerISBN" CommandArgument="ISBN" CommandName="Sort" runat="server" ForeColor="White">ISBN</asp:LinkButton></th>
                                            <th id="Th2" runat="server">
                                                <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                            <th id="Th3" runat="server">出版社</th>
                                            <th id="Th4" runat="server">作者</th>
                                            <th id="Th5" runat="server">分类</th>
                                            <th id="Th7" runat="server">光盘资源状态</th>
                                            <th id="Th8" runat="server">
                                                <asp:LinkButton ID="headerFFSJ" CommandArgument="XZSJ" CommandName="Sort" runat="server" ForeColor="White">下载时间</asp:LinkButton></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; font-family: Verdana, Arial, Helvetica, sans-serif; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpMyDownload" runat="server" PageSize="5">
                                        <Fields>
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    当前
                                            <asp:Label ID="lblPage" runat="server" ForeColor="Red" Text="<%# Container.StartRowIndex/Container.PageSize+1%>"></asp:Label>
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
            </ContentTemplate> 
        </asp:UpdatePanel>
        </center>
</asp:Content>
