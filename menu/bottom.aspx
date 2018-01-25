<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bottom.aspx.cs" Inherits="bottom" %>
<%@ Register TagPrefix="Menu" TagName="Bottom" Src="menucontrolbottom.ascx" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Control</title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="maindv"><a href="top.aspx">TOP (Vertical Sub) MENU</a>&nbsp;|&nbsp;<a class="selected" href="bottom.aspx">TOP (Horizontal Sub) MENU</a>&nbsp;|&nbsp;<a href="left.aspx">LEFT MENU</a>&nbsp;|&nbsp;<a href="Default.aspx">RIGHT MENU</a></div>
    <div>
    <Menu:Bottom id="bottomMenu" runat="server" />
    </div>
    <div style="clear:both"></div>
    </div>
    </form>
</body>
</html>
