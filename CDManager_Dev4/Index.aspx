<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/NoticeShower.Master"
    AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CDManager_Dev4.Index" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register src="Resources/UserControl/SearchBook.ascx" tagname="SearchBook" tagprefix="uc1" %>

<%@ Register src="Resources/UserControl/TimePicker.ascx" tagname="TimePicker" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    广州大学华软软件学院图书馆光盘查询系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center id="index">
        <img src="Resources/Images/s.jpg" /><br />
        
&nbsp;<h2>广州大学华软软件学院图书馆<br />
            光盘查询系统
        </h2>
        
        <uc1:SearchBook ID="SearchBook1" runat="server" />    
    </center>
</asp:Content>
