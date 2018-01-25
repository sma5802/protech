<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Addtocart" Codebehind="Addtocart.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    <td align="left" valign="top"><div align="justify"><span class="blueheading">Shopping Cart</span><br />
                      <br />
                        Here is your shopping cart. When you are ready to place an order, please select 
"Checkout". <br />
<br />
                        <table width="100%" border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1" class="btext">
                          <tr>
                            <td valign="top" bgcolor="#F9F8F8"><table width="100%" border="0" cellpadding="5" cellspacing="0" class="text">
                                <tr bgcolor="#F1F0F0">
                                  <td><strong>Remove</strong></td>
                                  <td><strong>Item</strong></td>
                                  <td><strong>Quantity</strong></td>
                                  <td><strong>Unit Price</strong></td>
                                  <td><strong>Units</strong></td>
                                  <td><strong>Subtotal</strong></td>
                              </tr>
                                <tr>
                                  <td><input type="checkbox" name="checkbox" value="checkbox" />
                                    <br />
                                  </td>
                                  <td>L-2-Chlorophe
                                  </td>
                                  <td width="80">                                  1 g</td>
                                  <td width="80">$60.00</td>
                                  <td width="80"><input name="textfield35" type="text" value="1" size="5" /></td>
                                  <td width="80">$60.00</td>
                                </tr>
                                <tr>
                                  <td bgcolor="#F1F0F0">&nbsp;</td>
                                  <td bgcolor="#F1F0F0"><b>Subtotal</b> </td>
                                  <td bgcolor="#F1F0F0">&nbsp;</td>
                                  <td bgcolor="#F1F0F0">&nbsp;</td>
                                  <td bgcolor="#F1F0F0">&nbsp;</td>
                                  <td bgcolor="#F1F0F0">$60.00</td>
                                </tr>
                            </table></td>
                          </tr>
                        </table>
                        <br />
                      <b>Shipping Charges</b>                      <br />
                      <br />
                      We charge a flat shipping fee regardless of package size and order value. We will not charge any shipping fees if you can provide us with your FedEx account number. <br />
                      <br />
                        <table width="100%"  border="0" cellpadding="10" cellspacing="1" bgcolor="#E1E1E1" class="text">
                          <tr>
                            <td align="left" valign="top" bgcolor="#F9F8F8"><table width="100%" border="0" cellpadding="4" cellspacing="1" bgcolor="#E1E1E1" class="text">
                                <tr bgcolor="#F1F0F0" class="bluetext">
                                  <td align="left"><strong>&nbsp;Location</strong></td>
                                  <td align="center"><strong>Service</strong></td>
                                  <td align="center"><strong>Price</strong></td>
                              </tr>
                                <tr bgcolor="#FFFFFF">
                                  <td bgcolor="#FFFFFF">Any </td>
                                  <td align="center">Customer's own FedEx Account</td>
                                  <td align="center">No Charge</td>
                                </tr>
                                <tr bgcolor="#F1F0F0">
                                  <td bgcolor="#F1F0F0">USA</td>
                                  <td align="center">FedEx Priority</td>
                                  <td align="center">$30.00</td>
                              </tr>
                                <tr bgcolor="#FFFFFF">
                                  <td bgcolor="#FFFFFF">USA</td>
                                  <td align="center">FedEx Standard</td>
                                  <td align="center">$25.00 </td>
                                </tr>
                                <tr bgcolor="#F1F0F0">
                                  <td bgcolor="#F1F0F0">USA</td>
                                  <td align="center">FedEx 2 Day</td>
                                  <td align="center">$20.00 </td>
                              </tr>
                                <tr bgcolor="#F1F0F0">
                                  <td bgcolor="#F1F0F0">All other locations</td>
                                  <td align="center">FedEx International Priority</td>
                                  <td align="center">$75.00</td>
                                </tr>
                            </table></td>
                          </tr>
                        </table>
                        <br />
                        <table width="100%"  border="0" cellpadding="5" cellspacing="0" class="text">
                          <tr>
                            <td width="50%"><img src="images/remove.gif" alt="Remove" width="78" height="25" /><a href="products-detail.asp"><img src="images/continue-shopping.gif" alt="Continue Shopping" width="150" height="25" hspace="5" border="0" /></a></td>
                            <td align="right"><img src="images/update.gif" alt="Update" width="74" height="25" hspace="5" /><a href="login.asp"><img src="images/checkout.gif" alt="Checkout" width="92" height="25" border="0" /></a></td>
                          </tr>
                        </table>
                        <br />
                        <br />
                    </div></td>
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

