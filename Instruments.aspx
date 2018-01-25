<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Instruments" Codebehind="Instruments.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top">
<table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr align="left" valign="top">
<td>
<br /><br />
<a href="operations.aspx" class="blueheading1">R&amp;D Center</a>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="manufacturingPlant.aspx" class="blueheading1">Manufacturing Plant</a>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<strong class="blueheading">Instruments</strong>
</td>
</tr>
<tr><td align="left" valign="top">
<div align="justify">
<asp:Label ID="lblcmsInstruments" runat="server"></asp:Label>
<br />
<br />
</div>
</td>
</tr>
</table>
</td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
  </tr>
</table>
 </asp:Content> 
