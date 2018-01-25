<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="emptyshopbag" Codebehind="emptyshopbag.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    
                <td align="center" valign="top">
                <div class="text">
    
   Your Shopping cart is currently empty<br /><br />
   To add an item to your Cart , simply click "Continue Shopping" to add more items.
   <br /><br /><br />
  <asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="images/continue-shopping.gif" hspace="5" border="0" OnClick="ImageButton2_Click" />

     </div>
     <br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
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

