<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="SessionExipred" Codebehind="SessionExipred.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top">
<asp:HyperLink ID="HyperLink1" runat="server" CssClass="bluelink" NavigateUrl="~/default.aspx">Back To Home</asp:HyperLink><br /><br /><br /><br /><br /><br />
<center>
<asp:Label ID="Label1" CssClass="blueheading" runat="server" Text="Your session has been expired."></asp:Label>
</center>
<br /><br /><br /><br /><br /><br />
</td>
</tr>
</table></td>
<td width="5"><img src="images/space.gif" width="1" height="1" /></td>
</tr>
</table>
</td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
</table>
</asp:Content>
