<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Useraccount" Title="Untitled Page" Codebehind="Useraccount.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top">
<table width="100%" align="left" border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td width="10" style="height: 10px"></td>
<td style="width: 11px; height: 10px"></td>
</tr>
<tr>
<td height="108" valign="top">
   <asp:Label ID="lblmsg" runat="server" ForeColor="red" Font-Bold="true" Visible="false" EnableViewState="false"></asp:Label>
<br />
<strong class="title">MY ACCOUNT</strong><br />
<table width="70%" border="0" cellpadding="10" cellspacing="0" class="text">
<tr> 
<td valign="top"> <br />
<strong><a href="editaccount.aspx" class="bluelink">Edit Profile</a></strong><br />
Add or update your billing address .<br /><br />
<strong><a href="orderlist.aspx" class="bluelink">Order History</a></strong><br />
View the details of your previous orders. <strong></strong> <br /><br /><br />
</td>
</tr>
</table>
</td>
</tr>
<tr align="left" valign="top">
<td></td>
<td style="width: 11px"></td>
</tr>
</table>
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

