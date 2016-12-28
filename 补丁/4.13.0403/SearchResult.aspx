<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master"
    AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="CDManager_Dev4.SearchResult" %>

<%@ Register Src="Resources/UserControl/SearchBook.ascx" TagName="SearchBook" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    图书搜索结果
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsBook" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Book" OnQueryCreated="edsBook_QueryCreated">
        <WhereParameters>
            <asp:QueryStringParameter Name="Key" QueryStringField="Key" DbType="String" />
            <asp:QueryStringParameter Name="ISBN" QueryStringField="ISBN" DbType="String" />
            <asp:QueryStringParameter Name="ZTM" QueryStringField="ZTM" DbType="String" />
            <asp:QueryStringParameter Name="CBS" QueryStringField="CBS" DbType="String" />
            <asp:QueryStringParameter Name="ZRZ" QueryStringField="ZRZ" DbType="String" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="edsCD" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="CD"
        OnQueryCreated="edsCD_QueryCreated">
        <WhereParameters>
            <asp:QueryStringParameter Name="Key" QueryStringField="Key" DbType="String" />
            <asp:QueryStringParameter Name="CDXH" QueryStringField="CDXH" DbType="String" />
            <asp:QueryStringParameter Name="CDMC" QueryStringField="CDMC" DbType="String" />
            <asp:QueryStringParameter Name="ZXZT" QueryStringField="ZXZT" DbType="Int16" />
        </WhereParameters>
    </asp:EntityDataSource>
    <table>
        <tr>
            <th style="font-size: large">广州大学华软软件学院图书馆<br />
                光盘查询系统图书搜索
            </th>
            <td>
                <uc1:SearchBook ID="SearchBook1" runat="server" />
            </td>
        </tr>
    </table>
    <hr width="96%" />
    <div id="search_result">
        <asp:UpdatePanel ID="upSearchResult" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panelLastTime" runat="server" Visible="false">
                    <center></center>
                </asp:Panel>
                <asp:ListView ID="listBook" runat="server" DataKeyNames="BookID" DataSourceID="edsBook" Visible="false"
                    OnPagePropertiesChanged="listBook_PagePropertiesChanged" OnItemDataBound="listBook_ItemDataBound">
                    <EmptyDataTemplate>
                        <h2>
                            <center>没有找到符合搜索条件的图书,请尝试更改其他的搜索关键字</center>
                        </h2>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td width="100%">
                                    <div>
                                        <asp:HyperLink ID="linkCDMC" runat="server" NavigateUrl='<%# Eval("ISBN","~/BookDetail.aspx?ISBN={0}") %>' Font-Size="12pt" Font-Bold="True"><%# Eval("ZTM") %></asp:HyperLink>

                                    </div>
                                    <br />
                                    <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>' />
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblZTM" runat="server" Text='<%# Eval("ZTM") %>' />
                                    </div>
                                    <div style="float: right; margin-top: -15px">
                                        光盘在线状态:<asp:Label ID="lblCDStatus" runat="server"></asp:Label>
                                        (光盘数量:<asp:Label ID="lblCDCount" runat="server"></asp:Label>
                                        &nbsp;&nbsp;在线数量:<asp:Label ID="lblCDOnline" runat="server"></asp:Label>
                                        )
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <hr />
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div style="">
                            搜索结果共:<asp:Label ID="lblCount" runat="server" ForeColor="Red"></asp:Label>
                            条,当前<asp:Label ID="lblPage" runat="server" ForeColor="Red"></asp:Label>
                            /<asp:Label ID="lblPageCount" runat="server"></asp:Label>
                            页                                                     
                            <div style="float: right; margin-top: -5px">
                                <asp:DataPager ID="dpBookTop" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField PreviousPageText="上十页" ButtonCount="10" NextPageText="下十页" />
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnBTGo" runat="server" Text="GO" OnCommand="btnBTGo_Command" />
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>
                            </div>
                            <hr />
                        </div>
                        <div id="itemPlaceholderContainer" runat="server" style="">
                            <span runat="server" id="itemPlaceholder" />
                        </div>
                        <div style="">
                            <center>
                                <asp:DataPager ID="dpBookBottom" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField PreviousPageText="上十页" ButtonCount="10" NextPageText="下十页" />
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnBBGo" runat="server" Text="GO" OnCommand="btnBBGo_Command"  />
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>
                            </center>
                        </div>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="listCD" runat="server" DataKeyNames="CDXH" DataSourceID="edsCD" Visible="false" OnItemDataBound="listCD_ItemDataBound" OnPagePropertiesChanged="listCD_PagePropertiesChanged">
                    <EmptyDataTemplate>
                        <h2>
                            <center>没有找到符合搜索条件的光盘,请尝试更改其他的搜索关键字</center>
                        </h2>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td width="100%">
                                    <div>
                                        <asp:HiddenField ID="hideBookID" runat="server" Value='<%# Eval("BookID") %>' />
                                        <asp:HyperLink ID="linkCDMC" runat="server" Font-Size="12pt" Font-Bold="True"><%# Eval("CDMC") %></asp:HyperLink>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCDXH" runat="server" Text='<%# Eval("CDXH") %>' Font-Bold="True" />
                                    </div>
                                    <br />
                                    [<asp:Label ID="lblISBN" runat="server" Text='<%# Eval("BookID") %>' />]
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblBook" runat="server" />

                                    </div>

                                    <div style="float: right; margin-top: -38px">
                                        光盘在线状态:<asp:Label ID="lblCDStatus" runat="server" Text='<%# Eval("ZXZT") %>'></asp:Label>
                                        <center style="padding: 2px; border: 1px solid #999999; margin-top: 5px;">
                                            <asp:Panel ID="panelApply" runat="server" Visible="False">
                                                <asp:Button ID="btnApplySubmit" runat="server" Text="提交申请" Visible="false" CommandArgument='<%# Eval("BookID") %>' OnCommand="btnApplySubmit_Command" />
                                                <asp:Label ID="lblApplyed" runat="server" Text="已发送申请" Visible="False" Font-Size="Medium" ForeColor="Maroon"></asp:Label><br />
                                            </asp:Panel>
                                            <asp:Panel ID="panelDownload" runat="server" Visible="False">
                                                <asp:Button ID="btnDownload" runat="server" Text="点击下载" CommandArgument='<%# Eval("CDXH") %>' OnCommand="btnDownload_Command" />
                                            </asp:Panel>
                                            <asp:Panel ID="panelUpload" runat="server" Visible="False" Font-Bold="True">
                                                还没有上传该光盘资源,<asp:HyperLink ID="linkUpload" runat="server">点击上传</asp:HyperLink>
                                            </asp:Panel>
                                            <asp:Panel ID="panelNoLogin" runat="server" Visible="False" Font-Bold="True">
                                                登录后可直接下载,<asp:HyperLink ID="linkLogin" runat="server" NavigateUrl="~/Account/Login.aspx">登录</asp:HyperLink>
                                            </asp:Panel>
                                        </center>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <hr />
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div style="">
                            搜索结果共:<asp:Label ID="lblCount" runat="server" ForeColor="Red"></asp:Label>
                            条,当前<asp:Label ID="lblPage" runat="server" ForeColor="Red"></asp:Label>
                            /<asp:Label ID="lblPageCount" runat="server"></asp:Label>
                            页
                            <div style="float: right; margin-top: -5px">
                                <asp:DataPager ID="dpCDTop" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField PreviousPageText="上十页" ButtonCount="10" NextPageText="下十页" />
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnCTGo" runat="server" Text="GO" OnCommand="btnCTGo_Command"/>
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>
                            </div>
                            <hr />
                        </div>
                        <div id="itemPlaceholderContainer" runat="server" style="">
                            <span runat="server" id="itemPlaceholder" />
                        </div>
                        <div style="">
                            <center>
                                <asp:DataPager ID="dpCDBottom" runat="server">
                                    <Fields>                                      
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField PreviousPageText="上十页" ButtonCount="10" NextPageText="下十页" />
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnCBGo" runat="server" Text="GO" OnCommand="btnCBGo_Command"  />
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>
                            </center>
                        </div>
                    </LayoutTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
