<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="UploadedCD.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.UploadedCD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    未确认光盘文件管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>未确认光盘文件管理</h3>
    <hr />
    <center>
        <asp:UpdatePanel ID="upCD" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvUploadedCD" runat="server" OnItemDataBound="lvUploadedCD_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="删除文件" OnCommand="btnDelete_Command" />
                                <asp:Button ID="btnConfirm" runat="server" Text="确认上传" OnCommand="btnConfirm_Command" />
                                <asp:Button ID="btnEdit" runat="server" Text="更改文件名" OnCommand="btnEdit_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblCDMC" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblCDGS" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCDDX" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有未确认光盘</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="删除文件" OnCommand="btnDelete_Command" />
                                <asp:Button ID="btnConfirm" runat="server" Text="确认上传" OnCommand="btnConfirm_Command" />
                                <asp:Button ID="btnEdit" runat="server" Text="更改文件名" OnCommand="btnEdit_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblCDMC" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblCDGS" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCDDX" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader">
                                            <th id="Th1" runat="server">操作</th>
                                            <th id="Th2" runat="server">光盘名称</th>
                                            <th id="Th3" runat="server">光盘格式</th>
                                            <th id="Th4" runat="server">光盘大小</th>
                                            <th id="Th5" runat="server">操作时间</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpUploadedCD" runat="server" PageSize="25">
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
                <asp:Panel ID="panelEdit" runat="server" Visible="false">
                    <table>
                        <tr>
                            <th>原文件名:</th>
                            <td align="left"><asp:Label ID="lblCDMC" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>新文件名:</th>
                            <td>
                                <asp:TextBox ID="txtNewCDMC" runat="server" Columns="30"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>文件格式:</th>
                            <td align="left"><asp:Label ID="lblCDGS" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>文件大小:</th>
                            <td align="left"><asp:Label ID="lblCDDX" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>操作时间:</th>
                            <td align="left"> <asp:Label ID="lblTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnClick" runat="server" Text="确定" OnClick="btnClick_Click" /><asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                 <asp:Label ID="lblFTP" runat="server" Text="FTP已关闭,无法获取未确认光盘列表"  ForeColor="#990000" Font-Bold="True" Font-Size="Medium" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
