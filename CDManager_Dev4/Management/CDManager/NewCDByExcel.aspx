﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="NewCDByExcel.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.NewCDByExcel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘信息批量导入
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2>
       光盘信息批量导入</h2>
    <hr />
     <div style="margin-left: 100px">
    <h3>
        Excel文件导入</h3>
     <ul>
        <li>系统只能识别Excel2000-2003(.xls)或者Excel2007以上(.xlsx)的数据文件 </li>
        <li>数据文件请确保严格按照以下格式排版及编辑字段名,否则数据导入将会出错
            <ul>
                <li>
                    <table id="excelexample">
                        <tr>
                            <th>
                                CDXH
                            </th>
                            <th>
                                ISBN
                            </th>
                            <th>
                                CDMC
                            </th>                         
                        </tr>
                        <tr>
                            <th>光盘序号</th>
                            <th>图书ISBN</th>
                            <th>光盘名称</th>
                        </tr>
                    </table>
                </li>
            </ul>
        </li>
    </ul>
    <asp:Panel ID="panelUpload" runat="server" Visible="false">
            <asp:FileUpload ID="FileUpload" runat="server" /><asp:Button ID="btnUpload" runat="server" Text="上传待导入光盘数据" OnClick="btnUpload_Click" />
        </asp:Panel>
        <br />
        <asp:UpdatePanel ID="upMessage" runat="server" Visible="false">
            <ContentTemplate>
                <table>
                    <tr>
                        <th>待上传数据:</th>
                        <td>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>【<asp:HyperLink ID="linkCheck" runat="server">查看待导入数据</asp:HyperLink>】</td>

                    </tr>
                    <tr>
                        <th>上传方式:</th>
                        <td>
                            <asp:RadioButton ID="rdoContinue" runat="server" GroupName="type" Text="重复跳过" Checked="true" /><asp:RadioButton ID="rdoReset" runat="server" GroupName="type" Text="重复覆盖" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnReUplod" runat="server" Text="重新上传" OnClick="btnReUplod_Click" /><asp:Button ID="btnInsert" runat="server" Text="开始导入数据到数据库" OnClick="btnInsert_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblUploading" runat="server"></asp:Label>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upMessage" DisplayAfter="0" DynamicLayout="false">
                    <ProgressTemplate>
                        <img src="../../Resources/Images/5-120604091310-50.gif" /><br />
                        光盘数据处理中...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
         </div>
</asp:Content>
