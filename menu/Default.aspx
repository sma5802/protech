<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="Menu" TagName="Right" Src="menucontrolright.ascx" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Control</title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="maindv"><a href="top.aspx">TOP (Vertical Sub) MENU</a>&nbsp;|&nbsp;<a href="bottom.aspx">TOP (Horizontal Sub) MENU</a>&nbsp;|&nbsp;<a href="left.aspx">LEFT MENU</a>&nbsp;|&nbsp;<a class="selected" href="Default.aspx">RIGHT MENU</a></div>
    <div style="float:right">
    <Menu:Right id="right" runat="server" />
    </div>
    <div style="clear:both"></div>
    
    </div>
    </form>
</body>
</html>
