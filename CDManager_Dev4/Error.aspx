﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CDManager_Dev4.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    操作失败
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.setTimeout("location.href='Index.aspx'", 3000);
        function back() {
            var num = document.getElementById("hid").value;
            document.getElementById("hid").value = --num;
            var value = document.getElementById("hid").value;
            document.getElementById("num").innerHTML = value;
        }
        window.setInterval('back()', 1000)
    </script>
    <center style="padding-top: 100px; padding-bottom: 100px">
        <h2 style="color: #990000">操作失败<br />
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </h2>
        <hr style="width: 90%" />
        <h4><span id="num" style="color: #990000">3</span>秒后自动返回首页</h4>
        <input type="hidden" value="3" id="hid">
    </center>
</asp:Content>
