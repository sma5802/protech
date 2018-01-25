<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Vieworder" Codebehind="Vieworder.aspx.cs" %>
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
<td style="width: 11px"></td>
<td style="background-image:url(<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/gt-bg.gif) "><img src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/space.gif" alt="Space" width="1" height="1" /></td>
<td style="width: 11px"></td>
</tr>
<tr>
<td style="background-image:url(<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/gl-bg.gif) ; width: 11px;"><img src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/space.gif" alt="Space" width="1" height="1" /></td>
<td height="108" valign="top"><br />
<strong class="title">
ORDER DETAILS</strong><br /> <br /> 
<asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="text">
<br />
<asp:Label ID="Label1" runat="server" Text="View Order Detail" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href='javascript:window.history.back(-1)' class="bluelink">Back</a>



<br />
<br />

<asp:FormView ID="FormView1" runat="server"  EmptyDataText="Record not found!" Width="100%" >
<ItemTemplate>
<table width="100%" align="left" border="0" cellpadding="0" cellspacing="0" class="btxt1">
<tr><td align="center"><hr style="height:1px; width:100%" /></td></tr>
<tr>
<td width="100%"> 

<table border="0" width="100%" align="left" class="btxt1"  >

<tr > 
<td width="40%" align="left">&nbsp;Order Number:</td>
<td align="left">&nbsp;<%# Eval("orderNo")%></td>
</tr>
<tr > 
<td align="left">&nbsp;Email:</td>
<td align="left">
<asp:Label ID="lblemail" runat="server"></asp:Label></td>
</tr>

<tr > 
<td align="left">&nbsp;Subtotal:</td>
<td align="left" >&nbsp;<%# Eval("OrderAmount","{0:c}")%></td>
</tr>
<tr > 
<td align="left">&nbsp;Shipping Charge:</td>
<td align="left" >&nbsp;<%# Eval("shippingcharge","{0:c}")%></td>
</tr>

<%-- <tr> 
<td align="left"  >&nbsp;Sales Tax:</td>

<td align="left"  >&nbsp;<%# Eval("SalesTax","{0:c}")%></td>

</tr>

<tr> 
<td align="left"  >&nbsp;Shipping :</td>

<td align="left"  >&nbsp;<%# Eval("shippingcharge", "{0:c}")%></td>

</tr>--%>
<tr > 
<td align="left" >&nbsp;Total:</td>
<td align="left" >&nbsp;<%# Eval("NetAmount","{0:c}")%></td>
</tr>

<tr   > 
<td align="left"   >&nbsp;Payment:</td>
<td align="left"  >&nbsp;<asp:Label ID="lblispaid" runat="server"><%#Eval("ispaid").ToString().ToLower().Replace("true","Done").Replace("false","Not Done") %></asp:Label> </td>
</tr>

<tr   > 
<td align="left"   >&nbsp;Purchase Order No.:</td>
<td align="left"  >&nbsp;<asp:Label ID="lblPurchaseOrder" runat="server"></asp:Label> </td>
</tr>

</table>

</td>
</tr>
<%-- <tr><td align="center"><hr style="height:1px; width:100%" /></td></tr>--%>
<!--  Card Info -->
<tr><td width="100%">

<%--<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1" >
<tr > 
<td align="left"   ><strong>Credit Card 
Information :</strong></td></tr>
<tr > 
<td align="left"   >
<asp:Label ID="lblcouponwithsameamount" runat="server" Text="Used Coupon with the same amount of total amount." Visible="false" ForeColor="red"></asp:Label></td></tr>
</table>--%>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1" >

<tr> 
<td width="100%" > 

<asp:FormView ID="FormView_criditcard" runat="server" Width="100%">
<ItemTemplate>
<table width="100%" >
<tr > 
<td align="left" width="40%" >&nbsp;Name:</td>
<td align="left" ><%# Eval("name") %></td>
</tr>
<tr > 
<td align="left">&nbsp;Card Number:</td>
<td align="left" >
<asp:Label ID="lblcno" runat="server"></asp:Label></td>
</tr>

<tr> 
<td  align="left">&nbsp;Cart Type:</td>
<td align="left" ><%# Eval("Type") %></td>
</tr>
<tr > 
<td align="left"  >&nbsp;Exp Date:</td>
<td align="left" ><%# Eval("expdate","{0:d}") %></td>
</tr>
<tr > 
<td align="left"  >CVV/CID Number</td>
<td align="left" ><%# Eval("CVVNo") %></td>
</tr>
</table>
</ItemTemplate>
</asp:FormView>


</td>
</tr>
</table>
</td></tr>
<tr runat="server" id="trhrrol"><td align="center"><hr style="height:1px; width:100%" /></td></tr>

<!-- Code for used coupon info -->
<%--<tr runat="server" id="trcoupon"><td>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1">
<tr > 
<td width="70%" align="left"   ><strong>Used Coupon Information:</strong></td>
</table>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1" >
<tr> 
<td> 

<asp:FormView ID="frmcoupon" runat="server" Width="100%">
<ItemTemplate>
<table width="100%" class="btxt1" >
<tr> 
<td align="left"  width="40%"  >&nbsp;Coupon No:</td>
<td  align="left"  ><%# Eval("Couponno") %></td>
</tr>
<tr > 
<td  align="left" >&nbsp;Discount Amount</td>
<td align="left"  >
<asp:Label ID="lbldiscountamount" runat="server"></asp:Label></td>
</tr>
<tr > 
<td align="left"   >&nbsp;Coupon Type</td>
<td align="left"  >
<asp:Label ID="lblcoupontype" runat="server" Text='<%# Eval("coupontype") %>'></asp:Label></td>
</tr>

</table>
</ItemTemplate>
</asp:FormView>
</td>
</tr>
</table>

</td></tr>--%>    


<!--code for Billing info -->
<%--<tr><td align="center"><hr style="height:1px; width:100%" /></td></tr>--%>
<tr><td>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1" >
<tr > 
<td width="70%" align="left"><strong>Billing Information:</strong></td>
</table>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1" >
<tr> 
<td> 

<asp:FormView ID="FormView_billing" runat="server" Width="100%">
<ItemTemplate>
<table width="100%" class="btxt1"  >
<tr> 
<td align="left"  width="40%"  >&nbsp;Name:</td>
<td  align="left"  ><%# Eval("firstname") %> <%#Eval("Lastname") %></td>
</tr>
<tr > 
<td  align="left" >&nbsp;Address 1:</td>
<td align="left"  ><%# Eval("address1") %></td>
</tr>
<tr > 
<td align="left"   >&nbsp;Address 2:</td>
<td align="left"  ><%# Eval("address2")%></td>
</tr>
<tr > 
<td align="left"  >&nbsp;City:</td>
<td  align="left" ><%# Eval("City") %></td>
</tr>   
<tr > 
<td align="left"  >&nbsp;State/Province:</td>
<td  align="left" >
<asp:label ID="lblstatebill" runat="server" Text='<%#Eval("state") %>'/>
</td>
</tr>              
<tr > 
<td align="left"  >&nbsp;Country:</td>
<td  align="left" >
<asp:label ID="lblcountrybill" runat="server"  Text='<%#Eval("Countryname") %>'/>
</td>
<tr > 
<td align="left"  >&nbsp;Zip/Postal code:</td>
<td align="left"  ><%# Eval("zip") %></td>
</tr>
<tr> 
<td  align="left"  >&nbsp;Phone:</td>
<td  align="left" ><%# Eval("Phone") %></td>
</tr>
<tr > 
<td  align="left" >&nbsp;Email:</td>
<td  align="left" ><%# Eval("Email") %></td>
</tr>
 
</table>
</ItemTemplate>
</asp:FormView>
</td>
</tr>
</table>
</td></tr>
<tr><td align="center"><hr style="height:1px; width:100%" /></td></tr>
<!-- Code for Shipping info -->
<tr><td>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1">
<tr > 
<td align="left"><strong>Shipping Information:</strong></td>

</table>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1" >
<tr> 
<td> 
<asp:FormView ID="FormView_Shipping" runat="server" Width="100%">
<ItemTemplate>

<table width="100%" class="btxt1">
<tr> 
<td  width="40%"  align="left">&nbsp;Name:</td>
<td align="left"   ><%# Eval("Firstname") %> <%#Eval("Lastname") %></td>
</tr>
<tr > 
<td   align="left">&nbsp;Address:</td>
<td  align="left" ><%# Eval("Address1") %></td>
</tr>
<%--<tr > 
<td align="left"  >&nbsp;Address 2</td>
<td align="left"  ><%# Eval("Address2") %></td>
</tr>--%>
<tr > 
<td  align="left" >&nbsp;City:</td>
<td  align="left" ><%# Eval("City") %></td>
</tr>
<tr > 
<td  align="left" >&nbsp;State/Province:</td>
<td  align="left" >
<asp:label ID="lblshipingstate" runat="server" Text='<%# Eval("State") %>'/>
<%--<%#Eval("State") %>--%>
</td>
</tr>
<tr > 
<td align="left"  >&nbsp;Country:</td>
<td align="left"  >
<asp:label ID="lblcountryship" runat="server"/>
</td>
</tr>
<tr > 
<td align="left"  >&nbsp;Zip/Postal code:</td>
<td align="left"  ><%# Eval("zip") %></td>
</tr>
<tr> 
<td align="left"  >&nbsp;Phone:</td>
<td  align="left" ><%# Eval("Phone") %></td>
</tr>
<tr > 
<td  align="left" >&nbsp;Email:</td>
<td  align="left" ><%# Eval("Email") %></td>
</tr>
<tr > 
<td align="left"  >&nbsp;<asp:Label ID="lblfedex" runat="server" Text="Fedex Account Number:"></asp:Label></td>
<td  align="left" ><%# Eval("fedexaccountnumber")%></td>
</tr>  
</table>


</ItemTemplate>
</asp:FormView>

</td>
</tr>
</table>

</td></tr>
<tr><td>
<tr><td align="center"><hr style="height:1px; width:100%" /></td></tr>
<!-- Code for purchase product-->
<tr><td>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1">
<tr > 
<td align="left"   ><strong>Purchased Products :</strong></td>

</table>
<table width="100%" border="0" cellpadding="4" cellspacing="1" class="btxt1">
<tr  > 
<td  align="left"   >
<asp:GridView ID="GridView1" runat="server" CssClass="grid" AutoGenerateColumns="false"  Width="100%" GridLines="both" CellPadding="6">
 <HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
<Columns>
<asp:TemplateField HeaderText="Product Name">
<ItemTemplate>
<a href='<%#string.Format("Product.aspx?pid={0}",Eval("prodid"))%>'>
<asp:Label ID="lblproductname" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("productname").ToString()) %>'></asp:Label><br /><b><%#HttpUtility.HtmlDecode(Eval("prodname").ToString()) %></b></a>
</ItemTemplate>
<ItemStyle Width="50%" />
</asp:TemplateField>

<asp:BoundField HeaderText="Qty" DataField="Qty" />

<asp:TemplateField>
<HeaderTemplate>Unit Price</HeaderTemplate>
<ItemTemplate>
<asp:Label ID="lblprice" runat="server" Text='<%#Eval("Price","{0:c}")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right"/>
<FooterTemplate>
<asp:Label ID="lblTotallabel" runat="server" Text="Total:"/>
</FooterTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right"/>
</asp:TemplateField>

<asp:TemplateField>
<HeaderTemplate>Extended Price</HeaderTemplate>
<ItemTemplate>
<asp:Label ID="lbltotalproduct" runat="server" Text='<%#Eval("Total","{0:c}")%>' ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right"/>
</asp:TemplateField>                      
</Columns>                         

</asp:GridView>
</td>
</tr>        
<tr><td align="right">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="text">
<tr> 
<td height="30" align="right" valign="top"><strong>Sub Total:&nbsp;&nbsp;&nbsp;</strong>
<%--<br /><font color="red">(Including shipping)</font>--%>
</td>
<td width="80" valign="top"  align="right"><strong>
<asp:Label ID="lblsub_total" runat="server" Text='<%# Eval("orderamount","{0:c}") %>'></asp:Label>&nbsp;&nbsp;</strong></td>
</tr>

<tr > 
<td height="30" align="right"><strong>Shipping Charge:</strong>&nbsp;&nbsp;&nbsp;</td>
<td align="right">
<asp:Label ID="lbldiscount" Text='<%# Eval("shippingcharge","{0:c}") %>' runat="server" Font-Bold="true" ></asp:Label>&nbsp;&nbsp;</strong></td>

<tr> 
<td height="1" align="right" bgcolor="#E6E6E6"><img src="../../images/dote.gif" width="1" height="1" /></td>
<td height="1" bgcolor="#E6E6E6"><img src="../../images/dote.gif" width="1" height="1" /></td>
</tr>
<tr> 
<td height="30" align="right"><strong>Total:</strong>&nbsp;&nbsp;&nbsp;</td>
<td align="right"><strong>&nbsp;
<asp:Label ID="lbltotal" runat="server" Text='<%# Eval("netamount","{0:c}") %>'></asp:Label>&nbsp;&nbsp;</strong></td>
</tr>
<tr> 
<td height="1" align="right" bgcolor="#E6E6E6"><img src="../../images/dote.gif" width="1" height="1" /></td>
<td height="1" bgcolor="#E6E6E6"><img src="../../images/dote.gif" width="1" height="1" /></td>
</tr>
</td>
</tr>
</table> 
</td></tr>
</table>

</td></tr>
</table>


</ItemTemplate>
</asp:FormView>
</asp:Panel>
</td>
<td style="background-image:url(<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/gr-bg.gif) ; width: 11px;"><img src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/space.gif" alt="Space" width="1" height="1" /></td>
</tr>
<tr align="left" valign="top">
<td style="width: 11px"></td>
<td style="background-image:url(<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/gb-bg.gif) "><img src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>images/space.gif" alt="Space" width="1" height="1" /></td>
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

