<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="ReaderManagement.aspx.cs" Inherits="CDManager_Dev4.Management.User.ReaderManager.ReaderManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    读者管理
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
    <h3>读者管理</h3>
    <hr />
    <asp:UpdatePanel ID="upReader" runat="server">
        <ContentTemplate>
            <center style="font-size: small">
                <asp:Button ID="btnReaderDelete" runat="server" OnClick="btnReaderDelete_Click" Visible="false" />                
                <br />
                <asp:Panel ID="panelTitle" runat="server">
                    <asp:RadioButton ID="rdoDZTM" runat="server" Text="按读者条码查询" GroupName="select" AutoPostBack="true" OnCheckedChanged="rdoDZTM_CheckedChanged" Checked="true" /><asp:RadioButton ID="rdoSelect" runat="server" Text="按筛选条件查询" GroupName="select" AutoPostBack="true" OnCheckedChanged="rdoSelect_CheckedChanged" />
                    <asp:Panel ID="panelDZTM" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="50%">
                        读者条码:<asp:TextBox ID="txtDZTM" runat="server"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel ID="panelSelect" runat="server" Visible="false" BorderStyle="Solid" BorderWidth="1px" Width="100%">
                        <asp:Table ID="tableDZLX" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>读者类型:<asp:DropDownList ID="dropDZLX" runat="server" OnSelectedIndexChanged="dropDZLX_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="trXS" Visible="false">
                                <asp:TableCell>
                                    <table>
                                        <tr>
                                            <th>按年级筛选:</th>
                                            <td>
                                                <asp:DropDownList ID="dropXSGrade" runat="server" OnSelectedIndexChanged="dropXSGrade_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>按单位筛选:</th>
                                            <td>一级单位:<asp:DropDownList ID="dropXSYJDW" runat="server" OnSelectedIndexChanged="dropXSYJDW_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>二级单位:<asp:DropDownList ID="dropXSEJDW" runat="server"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="trOther" Visible="false">
                                <asp:TableCell>
                                    一级单位:<asp:DropDownList ID="dropOtherYJDW" runat="server" OnSelectedIndexChanged="dropOtherYJDW_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    二级单位:<asp:DropDownList ID="dropOtherEJDW" runat="server"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                    <asp:Button ID="btnSelect" runat="server" Text="查找读者"
                        OnClick="btnSelect_Click" />
                </asp:Panel>
                <hr />
                <asp:ListView ID="lvReader" runat="server" DataKeyNames="DZTM" DataSourceID="edsReader" OnItemDataBound="lvReader_ItemDataBound" Visible="false">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="注销" CommandArgument='<%# Eval("DZTM") %>' OnCommand="btnDelete_Command" />
                            </td>
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
                                <td>没有读者</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="注销" CommandArgument='<%# Eval("DZTM") %>' OnCommand="btnDelete_Command" />
                            </td>
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
                                            <th id="Th1" runat="server"></th>
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

            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
