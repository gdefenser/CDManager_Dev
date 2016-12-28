<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="ApplyAndDownload.aspx.cs" Inherits="CDManager_Dev4.Management.User.ReaderManager.ApplyAndDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    读者申请与下载统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>读者申请与下载统计</h2>
    <hr />
    <center>
        <asp:Panel ID="panelDZTM" runat="server" Visible="false">
            <table>
                <tr>
                    <th>读者条码:</th>
                    <td>
                        <asp:TextBox ID="txtDZTM" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnClick" runat="server" Text="查找读者" OnClick="btnClick_Click" /></td>
                </tr>
            </table>
            <hr />
        </asp:Panel>
        <asp:Panel ID="panelReader" runat="server" Visible="false">
            <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
            <asp:UpdatePanel ID="upReader" runat="server">
                <ContentTemplate>
                    <table class="statistics" cellspacing="0" cellpadding="4">
                        <tr>
                            <th>操作</th>
                            <th>读者条码</th>
                            <th>姓名</th>
                            <th>申请数/下载数</th>
                            <th>本月申请数/本月下载数</th>
                            <th>最近申请时间</th>
                            <th>最近下载时间</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnBan" runat="server" Text="禁止登录" OnClick="btnBan_Click" /></td>
                            <td>
                                <asp:HyperLink ID="linkDZTM" runat="server"></asp:HyperLink></td>
                            <td>
                                <asp:Label ID="lblXM" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblApplyDownload" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblMouthApplyDownload" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblApply" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblDownload" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr />
                    <asp:DropDownList ID="dropType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                        <asp:ListItem>申请/下载趋势图</asp:ListItem>
                        <asp:ListItem>申请/下载比例图</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:Chart ID="chartTrend" runat="server" Visible="False" Width="900px">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Line"></asp:Series>
                            <asp:Series Name="Series2" ChartType="Line"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="chartPercent" runat="server" Visible="false">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
                            <asp:Series Name="Series2" ChartType="Pie"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <hr />
                    <asp:DropDownList ID="dropLogType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropLogType_SelectedIndexChanged">
                        <asp:ListItem>申请记录明细</asp:ListItem>
                        <asp:ListItem>下载记录明细</asp:ListItem>
                    </asp:DropDownList><br />
                    <br />
                    <asp:EntityDataSource ID="edsApply" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="ApplyLog" EntityTypeFilter="" OrderBy="it.SQSJ" Select="it.Book.ZTM,it.Book.CBS,it.Book.ZRZ,it.Book.ISBN,it.SQSJ,it.SQBH,it.IP" Where="it.DZTM=@DZTM">
                        <WhereParameters>
                            <asp:QueryStringParameter DbType="String" Name="DZTM" QueryStringField="DZTM" />
                        </WhereParameters>
                    </asp:EntityDataSource>
                    <asp:EntityDataSource ID="edsDownload" runat="server" ConnectionString="name=CDManagerDevEntities"
                        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="DownloadLog"
                        Where="it.CZYTM=@CZYTM" Select="it.CD.CDMC,it.CD.Book.ZTM,it.CD.Book.CBS,it.CD.Book.ZRZ,it.CDID,it.CD.Book.ISBN,it.XZSJ,it.CZYTM,it.XZBH,it.IP"
                        OrderBy="it.XZSJ">
                        <WhereParameters>
                            <asp:QueryStringParameter DbType="String" Name="CZYTM" QueryStringField="DZTM" />
                        </WhereParameters>
                    </asp:EntityDataSource>
                    <asp:ListView ID="lvApply" runat="server" DataSourceID="edsApply" OnItemDataBound="lvApply_ItemDataBound" Visible="false">
                        <AlternatingItemTemplate>
                            <tr class="alternating">
                                <td>
                                    <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                                </td>
                                <td>
                                    <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Label ID="SQSJLabel" runat="server" Text='<%# Eval("SQSJ") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="IPLabel" runat="server" Text='<%# Eval("IP") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnBanIP" runat="server" Text="禁止IP" CommandArgument='<%# Eval("IP") %>' OnCommand="btnBanIP_Command" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                                <tr>
                                    <td>没有申请记录</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td>
                                    <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                                </td>
                                <td>
                                    <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Label ID="SQSJLabel" runat="server" Text='<%# Eval("SQSJ") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="IPLabel" runat="server" Text='<%# Eval("IP") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnBanIP" runat="server" Text="禁止IP" CommandArgument='<%# Eval("IP") %>' OnCommand="btnBanIP_Command" />
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
                                                    <asp:LinkButton ID="headerISBN" CommandArgument="ISBN" CommandName="Sort" runat="server" ForeColor="White">ISBN</asp:LinkButton></th>
                                                <th id="Th2" runat="server">
                                                    <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                                <th id="Th3" runat="server">
                                                    <asp:LinkButton ID="headerSQSJ" CommandArgument="SQSJ" CommandName="Sort" runat="server" ForeColor="White">申请时间</asp:LinkButton></th>
                                                <th id="Th4" runat="server">申请IP</th>
                                                <th id="Th5" runat="server">禁止IP</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server">
                                    <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                        <asp:DataPager ID="dpApply" runat="server" PageSize="20">
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
                                                        跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnApplyGo" runat="server" Text="GO" OnCommand="btnApplyGo_Command" />
                                                    </PagerTemplate>
                                                </asp:TemplatePagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                    <asp:ListView ID="lvDownload" runat="server" DataSourceID="edsDownload" Visible="false" OnItemDataBound="lvDownload_ItemDataBound">
                        <AlternatingItemTemplate>
                            <tr class="alternating">
                                <td>
                                    <asp:Label ID="CDMCLabel" runat="server" Text='<%# Eval("CDMC") %>' />
                                </td>
                                <td>
                                    <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Label ID="XZSJLabel" runat="server" Text='<%# Eval("XZSJ") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="IPLabel" runat="server" Text='<%# Eval("IP") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnBanIP" runat="server" Text="禁止IP" CommandArgument='<%# Eval("IP") %>' OnCommand="btnBanIP_Command" />
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
                                    <asp:Label ID="CDMCLabel" runat="server" Text='<%# Eval("CDMC") %>' />
                                </td>
                                <td>
                                    <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Label ID="XZSJLabel" runat="server" Text='<%# Eval("XZSJ") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="IPLabel" runat="server" Text='<%# Eval("IP") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnBanIP" runat="server" Text="禁止IP" CommandArgument='<%# Eval("IP") %>' OnCommand="btnBanIP_Command" />
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
                                                    <asp:LinkButton ID="headerCDMC" CommandArgument="CDMC" CommandName="Sort" runat="server" ForeColor="White">光盘名称</asp:LinkButton></th>
                                                <th id="Th2" runat="server">
                                                    <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                                <th id="Th3" runat="server">
                                                    <asp:LinkButton ID="headerXZSJ" CommandArgument="XZSJ" CommandName="Sort" runat="server" ForeColor="White">下载时间</asp:LinkButton></th>
                                                <th id="Th4" runat="server">下载IP</th>
                                                <th id="Th5" runat="server">禁止IP</th>
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
                                                        跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnDownloadGo" runat="server" Text="GO" OnCommand="btnDownloadGo_Command" />
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
        </asp:Panel>
    </center>
</asp:Content>
