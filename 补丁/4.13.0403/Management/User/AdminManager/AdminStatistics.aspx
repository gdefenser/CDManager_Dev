<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="AdminStatistics.aspx.cs" Inherits="CDManager_Dev4.Management.User.AdminManager.AdminStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    管理员管理统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>管理员管理统计</h2>
    <hr />
    <center>
        <asp:Panel ID="panelGLYTM" runat="server" Visible="false">
            <table>
                <tr>
                    <th>管理员条码:</th>
                    <td>
                        <asp:TextBox ID="txtGLYTM" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnClick" runat="server" Text="查找管理员" OnClick="btnClick_Click" /></td>
                </tr>
            </table>
            <hr />
        </asp:Panel>
        <asp:Panel ID="panelAdmin" runat="server" Visible="false">
            <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
            <asp:UpdatePanel ID="upAdmin" runat="server">
                <ContentTemplate>
                    <table class="statistics" cellspacing="0" cellpadding="4">
                        <tr>
                            <th>管理员条码</th>
                            <th>姓名</th>
                            <th>申请处理数</th>
                            <th>最近登录时间</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="linkGLYTM" runat="server"></asp:HyperLink></td>
                            <td>
                                <asp:Label ID="lblXM" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblApply" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblLogin" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr />
                    <asp:Chart ID="chartAdmin" runat="server" Width="1000">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                            <asp:Series Name="Series2"></asp:Series>
                            <asp:Series Name="Series3"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <hr />
                    <h3>光盘上传记录</h3>
                    <asp:EntityDataSource ID="edsUploadLog" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="UploadLog" Where="it.CZYTM=@CZYTM" Select="it.CD.CDMC,it.CD.Book.ZTM,it.CD.Book.ISBN,it.SCSJ,it.CZYTM,it.SCBH,it.IP" OrderBy="it.SCBH">
                        <WhereParameters>
                            <asp:QueryStringParameter DbType="String" Name="CZYTM" QueryStringField="GLYTM" />
                        </WhereParameters>
                    </asp:EntityDataSource>
                    <asp:GridView ID="gvUploadLog" runat="server" CellPadding="4" DataSourceID="edsUploadLog"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
                        BorderStyle="Solid" BorderWidth="1px" DataKeyNames="SCBH"
                        Font-Size="Small" AllowSorting="True"
                        PageSize="15" BorderColor="#999999">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="CDMC" HeaderText="光盘名称">
                                <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="ISBN" DataNavigateUrlFormatString="~/Management/BookManager/EditBook.aspx?ISBN={0}"
                                HeaderText="图书名称" DataTextField="ZTM">
                                <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="SCSJ" HeaderText="上传时间" SortExpression="SCSJ">
                                <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IP" HeaderText="上传IP" SortExpression="IP">
                                <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#99CCFF" />
                        <EmptyDataTemplate>
                            没有上传记录
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5381AC" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#5381AC" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </center>
</asp:Content>
