<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="AdminMessage.aspx.cs" Inherits="CDManager_Dev4.Management.Message.AdminMessage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    管理员消息箱
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsAdmin" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities"
        CommandText="select m.YHLX,m.SXRTM,m.FSRTM,m.FSSJ,m.XXNR from UserMessage as m where m.XXTM in (select value MAX(m1.XXTM) from UserMessage as m1 group by m1.SXRTM) limit 1" AutoGenerateOrderByClause="True"
        Where="(it.SXRTM=@SXRTM or it.FSRTM=@SXRTM) and it.YHLX='admin'">
        <WhereParameters>
            <asp:SessionParameter DbType="String" Name="SXRTM" SessionField="TM" />
        </WhereParameters>
        <OrderByParameters>
            <asp:Parameter DefaultValue="FSSJ" />
        </OrderByParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="edsMessage" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="UserMessage" Where="(it.FSRTM=@FSRTM or it.SXRTM=@FSRTM) and (it.SXRTM=@FSRTM or it.SXRTM=@SXRTM)">
        <WhereParameters>
            <asp:QueryStringParameter DbType="String" Name="FSRTM" QueryStringField="FSRTM" />
            <asp:SessionParameter DbType="String" Name="SXRTM" SessionField="TM" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:UpdatePanel ID="upAdminMessage" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panelAdmin" runat="server" Visible="false">
                <h2>管理员消息箱</h2>
                <hr />
                <asp:ListView ID="lvAdmin" runat="server" DataSourceID="edsAdmin">
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>你没有收到消息。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr style="">
                            <th>对话人:</th>
                            <td>
                                <asp:HyperLink ID="linkFSRTM" runat="server" Text='<%# Eval("FSRTM") %>' NavigateUrl='<%# Eval("FSRTM","~/Management/Message/AdminMessage.aspx?FSRTM={0}") %>'></asp:HyperLink>
                            </td>
                            <th>发送时间:</th>
                            <td>
                                <asp:Label ID="FSSJLabel" runat="server" Text='<%# Eval("FSSJ") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="XXNRLabel" runat="server" Text='<%# Eval("XXNR") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                        <tr runat="server" style="">
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </asp:Panel>

            <asp:Panel ID="panelMessage" runat="server" Visible="false">
                <h2>与<asp:Label ID="lblTitle" runat="server"></asp:Label>的对话</h2>
                <hr />
                <asp:ListView ID="lvMessage" runat="server" DataSourceID="edsMessage">
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>你没有收到消息。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr style="">
                            <th>发送人:</th>
                            <td>
                                <asp:Label ID="FSRTMLabel" runat="server" Text='<%# Eval("FSRTM") %>' />
                            </td>
                            <th>接收人:</th>
                            <td>
                                <asp:Label ID="SXRTMLabel" runat="server" Text='<%# Eval("SXRTM") %>' /></td>
                        </tr>
                        <tr>
                            <th>发送时间:</th>
                            <td colspan="3">
                                <asp:Label ID="FSSJLabel" runat="server" Text='<%# Eval("FSSJ") %>' />
                            </td>
                        </tr>
                        <tr>
                            <th colspan="6">消息内容</th>
                        </tr>
                        <tr>
                            <td colspan="6" style="border-bottom-style: dotted; border-width: 1px">
                                <asp:Label ID="XXNRLabel" runat="server" Text='<%# Eval("XXNR") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                        <tr runat="server" style="">
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <hr />
                <table>
                    <tr>
                        <th bgcolor="#5381AC" style="color: #FFFFFF">发送新消息</th>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtXXNR" runat="server" Rows="8" Columns="40" TextMode="MultiLine"></asp:TextBox>
                            <br />
                            <asp:CustomValidator ID="valXXNR" runat="server" Font-Bold="True" ForeColor="Maroon"
                                Display="Dynamic"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnNew" runat="server" Text=" 发  送 " OnClick="btnNew_Click" /><asp:Button ID="btnReset" runat="server" Text=" 重  置 " OnClick="btnReset_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
