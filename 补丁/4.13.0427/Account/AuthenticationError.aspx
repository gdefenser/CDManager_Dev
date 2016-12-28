<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master"
    AutoEventWireup="true" CodeBehind="AuthenticationError.aspx.cs" Inherits="CDManager_Dev4.Account.AuthenticationError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    认证错误
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.setTimeout("location.href='../Index.aspx'", 4000);
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
            对不起,你不能打开该页面!</h1>
        <br />
        <h3>
            错误原因:</h3>
        1.超时或非法登录<br />
        2.你无权限打开该页面<br />
        3.服务器已关闭非管理员登录权限<br />
        4.登录缓存已被管理员清空<br />
        <hr /><h4>
            <span id="num" style="color: #990000">5</span>秒后自动返回首页</h4>
        <input type="hidden" value="5" id="hid">
        
    </center>
</asp:Content>
