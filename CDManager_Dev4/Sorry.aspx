<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master" AutoEventWireup="true" CodeBehind="Sorry.aspx.cs" Inherits="CDManager_Dev4.Sorry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    很抱歉,服务器处理发生错误
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.setTimeout("location.href='Index.aspx'", 4000);
        function back() {
            var num = document.getElementById("hid").value;
            document.getElementById("hid").value = --num;
            var value = document.getElementById("hid").value;
            document.getElementById("num").innerHTML = value;
        }
        window.setInterval('back()', 1000)
    </script>
    <center style="margin-top: 150px; margin-bottom: 150px">
        <h2>
            无法打开目的页面</h2>
        <hr />
        <h1 style="color: #FF0000; font-weight: bold;">
            很抱歉,服务器在处理您的操作时发生错误!<br />
            我们会努力修复,感谢您的支持和理解
        </h1>
       <h4>
            <span id="num" style="color: #990000">5</span>秒后自动返回首页</h4>
        <input type="hidden" value="5" id="hid">
        
    </center>
</asp:Content>
