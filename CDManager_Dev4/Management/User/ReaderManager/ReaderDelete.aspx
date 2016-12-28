<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="ReaderDelete.aspx.cs" Inherits="CDManager_Dev4.Management.User.ReaderManager.ReaderDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    读者批量注销
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsReader" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Reader"
        OrderBy="it.DZTM" Select="it.[DZTM], it.[XM], it.[XB], it.[YJDW], it.[EJDW], it.[YXRQ], it.[FFSJ], it.[FFCZY]" EntityTypeFilter="">
        <%--        <WhereParameters>
            <asp:QueryStringParameter Name="DZTM" QueryStringField="DZTM" DbType="String" />
            <asp:QueryStringParameter Name="DZLX" QueryStringField="DZLX" DbType="String" />
            <asp:QueryStringParameter Name="Grade" QueryStringField="Grade" DbType="String" />
            <asp:QueryStringParameter Name="YJDW" QueryStringField="YJDW" DbType="String" />
            <asp:QueryStringParameter Name="EJDW" QueryStringField="EJDW" DbType="String" />
        </WhereParameters>--%>
    </asp:EntityDataSource>
    <asp:HiddenField ID="hideWhere" runat="server" />
    <h3>
        <asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
    <hr />
    <asp:UpdatePanel ID="upReader" runat="server">
        <ContentTemplate>
            <center>
                <asp:Label ID="lblWarning" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="#990000"></asp:Label>
                <asp:ListView ID="lvReader" runat="server" DataKeyNames="DZTM" DataSourceID="edsReader">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:HyperLink ID="linkDZTM" runat="server" NavigateUrl='<%# Eval("DZTM","~/Management/User/ReaderManager/EditReader.aspx?DZTM={0}") %>' Text='<%# Eval("DZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="XMLabel" runat="server" Text='<%# Eval("XM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="XBLabel" runat="server" Text='<%# Eval("XB") %>' />
                            </td>
                            <td>
                                <asp:Label ID="YJDWLabel" runat="server" Text='<%# Eval("YJDW") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EJDWLabel" runat="server" Text='<%# Eval("EJDW") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("FFSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFCZYLabel" runat="server" Text='<%# Eval("FFCZY") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有要删除的读者</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:HyperLink ID="linkDZTM" runat="server" NavigateUrl='<%# Eval("DZTM","~/Management/User/ReaderManager/EditReader.aspx?DZTM={0}") %>' Text='<%# Eval("DZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="XMLabel" runat="server" Text='<%# Eval("XM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="XBLabel" runat="server" Text='<%# Eval("XB") %>' />
                            </td>
                            <td>
                                <asp:Label ID="YJDWLabel" runat="server" Text='<%# Eval("YJDW") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EJDWLabel" runat="server" Text='<%# Eval("EJDW") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("FFSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFCZYLabel" runat="server" Text='<%# Eval("FFCZY") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;" width="100%">
                                        <tr id="Tr2" runat="server" class="tableheader">
                                            <th id="Th2" runat="server">
                                                <asp:LinkButton ID="headerDZTM" CommandArgument="DZTM" CommandName="Sort" runat="server" ForeColor="White">读者条码</asp:LinkButton></th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerXM" CommandArgument="XM" CommandName="Sort" runat="server" ForeColor="White">姓名</asp:LinkButton></th>
                                            <th id="Th4" runat="server">
                                                <asp:LinkButton ID="headerXB" CommandArgument="XB" CommandName="Sort" runat="server" ForeColor="White">姓别</asp:LinkButton></th>
                                            <th id="Th5" runat="server">
                                                <asp:LinkButton ID="headerYJDW" CommandArgument="YJDW" CommandName="Sort" runat="server" ForeColor="White">一级单位</asp:LinkButton>
                                            </th>
                                            <th id="Th6" runat="server">
                                                <asp:LinkButton ID="headerEJDW" CommandArgument="EJDW" CommandName="Sort" runat="server" ForeColor="White">二级单位</asp:LinkButton></th>
                                            <th id="Th7" runat="server">
                                                <asp:LinkButton ID="headerFFSJ" CommandArgument="FFSJ" CommandName="Sort" runat="server" ForeColor="White">创建时间时间</asp:LinkButton></th>
                                            <th id="Th8" runat="server">
                                                <asp:LinkButton ID="headerFFCZY" CommandArgument="FFCZY" CommandName="Sort" runat="server" ForeColor="White">创建人</asp:LinkButton></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpReader" runat="server" PageSize="25">
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
                <hr />
                <asp:Button ID="btnDelete" runat="server" Text="确定删除" OnClick="btnDelete_Click" Enabled="False" />
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upReader" DisplayAfter="0" DynamicLayout="false">
                    <ProgressTemplate>
                        <img src="../../../Resources/Images/5-120604091310-50.gif" /><br />
                        读者数据删除中...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
