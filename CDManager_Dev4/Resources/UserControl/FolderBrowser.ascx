﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FolderBrowser.ascx.cs" Inherits="CDManager_Dev4.Resources.UserControl.FolderBrowser" %>
<script src="<%=ResolveClientUrl("~/Resources/Scripts/browserFolder.js")%>" type="text/javascript"></script>
<asp:TextBox ID="txtFTPFolder" runat="server" Columns="35" Enabled="False" AutoPostBack="True"></asp:TextBox><input id="btnSelectFolder" type="button" value="浏览" onclick="BrowseFolder()" />