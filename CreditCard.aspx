<%@ Page Language="C#" AutoEventWireup="true" Inherits="CreditCard" Codebehind="CreditCard.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="links" Src="~/MenuControl2l.ascx" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=ConfigurationManager.AppSettings["title"].ToString() %></title>
    <link href="<%=ConfigurationManager.AppSettings["WebSitePath1"].ToString()%>css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="<%=ConfigurationManager.AppSettings["WebSitePath1"].ToString() %>resources/css1/rcom-ticker-tr.css" rel="stylesheet" type="text/css" />
    <script language="jscript" type="text/jscript">
function card()
{

	// visa - len=13, 16; first digit=4
    if( document.theForm.payment[1].checked ) 
    {
			if(( document.theForm.ccNumber.value.length != 13 && document.theForm.ccNumber.value.length != 16 ) || document.theForm.ccNumber.value.charAt( 0 ) != '4')
			 {
				window.alert( "Please enter a valid VISA number." );
				return( false );
			}
		}
		// mc - len=16; first digits=51...55
		if( document.theForm.payment[2].checked )
		 {
			if( document.theForm.ccNumber.value.length != 16 || document.theForm.ccNumber.value.charAt( 0 ) != '5') {
				window.alert( "Please enter a valid MasterCard number." );
				return( false );
			}
		}
		// amex - len=15; first digits=34, 37
		if( document.theForm.payment[3].checked ) {
			if( document.theForm.ccNumber.value.length != 15 || ( document.theForm.ccNumber.value.substr( 0, 2 ) != "34" && document.theForm.ccNumber.value.substr( 0, 2 ) != "37" ))
			{
				window.alert( "Please enter a valid American Express number." );
				return( false );
			}
		}
}

</script>

    <script type="text/javascript">
function agreed(sender,args)
{
args.IsValid =false;
if(document.getElementById('<%=chkterm.ClientID %>').checked==false)
{
    document.getElementById('<%=chkterm.ClientID %>').focus();
    args.IsValid =false;
}
else
    args.IsValid =true;    
}
</script>
</head>
<body>
<%--<%=ConfigurationManager.AppSettings["websitepath1"] %>--%>
    <form id="form1" runat="server">
    <div align="center">
        <div id="topDiv">
            <table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" height="18" align="left" valign="top"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/main-tlcr.gif" alt="corner" width="17" height="18" /></td>
    <td height="18" align="left" valign="top" style="background-image:url(images/main-tbg.gif)"><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td width="17" height="18" align="left" valign="top"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/main-trcr.gif" alt="corner" width="17" height="18" /></td>
  </tr>
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="topwait"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
    <td width="152"><a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>Default.aspx"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/peptech-logo.gif" alt="Peptech" width="152" height="104" border="0" /></a></td>
    <td align="right">
    <table cellpadding="3" cellspacing="0">
    <tr>
    <td style="height: 54px">
    <a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>ProductSearch.aspx?search=direct"><strong class="bluetext">Search Product:</strong></a>
   
    &nbsp;</td>
    <td style="height: 108px">
    <asp:TextBox ID="txtsearch" runat="server" Width="150" ValidationGroup="ss"></asp:TextBox></td>
    <td style="height: 54px">
    <a href="javascript:search();" ><img border="0" src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/GO1.jpg" /></a>
    
    </td>
    </tr>
    </table>
    
    </td>
    </tr>
    <tr><td colspan="2">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td align="right" valign="top" style="height: 18px">
<asp:Label ID="lblUser" EnableViewState="false" runat="server" CssClass="bluelink1" Font-Bold="True"></asp:Label>
<span id="s1" runat="server" >&nbsp;|&nbsp;</span>
<asp:LinkButton ID="lnkmyaccount" EnableViewState="false" runat="server" CssClass ="bluelink1" OnClick="lnkmyaccount_Click" Font-Bold="true" Visible="false">My Account</asp:LinkButton>
<span id="s2" runat="server" >&nbsp;|&nbsp;</span>
<asp:LinkButton ID="lnkbasket" EnableViewState="false" runat="server" CssClass="bluelink1" PostBackUrl='Mycart.aspx' Font-Bold="true" CausesValidation="false"></asp:LinkButton>
<span id="s3" runat="server" >&nbsp;|&nbsp;</span>
<asp:HyperLink ID="hypsignin" EnableViewState="false" runat ="server" CssClass="blueheading" Font-Bold="True">[hypsignin]</asp:HyperLink>
<span id="s4" runat="server" >&nbsp;|&nbsp;</span>
<asp:LinkButton ID="lnkstatus" EnableViewState="false" runat="server" CausesValidation="false" CssClass="blueheading" Font-Bold="true" OnClick="lnkstatus_click" />
</td>
</tr>
</table>
</td></tr>
</table>
     
     
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td align="left" valign="top" bgcolor="#10316B" >

<uc1:links id="links1" runat="server" />
</td>
</tr>
</table>
</td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
</table>
        </div>
        <div id="middleDiv">
        <table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" class="tdbg" ><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    
                <td align="left" valign="top">
               <table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr align="left" valign="top"> 
<td width="15"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/dote.gif" alt="" width="1" height="1" /></td>
<td><br />
<strong class="tital">CREDIT CARD</strong>

<br />
<br />
<asp:GridView ID="GridView_cart" CssClass="text" runat="server" AutoGenerateColumns="False" width="100%"  cellpadding="5" OnRowCommand="GridView_cart_RowCommand" OnRowDataBound="GridView_cart_RowDataBound" GridLines="Both">
 <HeaderStyle BackColor="#F1F0F0"  Font-Bold="True"  CssClass="text" /> 
 <AlternatingRowStyle BackColor ="#F1F0F0"/>
<Columns>
<asp:TemplateField HeaderText="Product" >
<ItemTemplate>
<%--<asp:HiddenField ID="hdTempID" runat="server" Value='<%#Eval("TempID")%>' />--%>
<asp:Label ID="lblp_name" CssClass="text" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("ProductName").ToString())%>'></asp:Label><br />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%">
<ItemTemplate>
<%# Eval("Qty") %>
<%--<asp:TextBox ID="txtquantity" ReadOnly="true" runat="server" Text='<%# Eval("Qty") %>' width="30px" MaxLength="3"></asp:TextBox>--%>
<%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtquantity"
ErrorMessage="(Invalid)" Operator="GreaterThan" ValueToCompare="0" Type="Integer"></asp:CompareValidator>--%>
</ItemTemplate>
<ItemStyle HorizontalAlign="right" />
<HeaderStyle  HorizontalAlign="Right"/>
</asp:TemplateField>
<asp:TemplateField HeaderText="Unit Price" HeaderStyle-Width="20%">
<ItemTemplate>
<asp:Label ID="lblprice" runat="server" Text='<%#String.Format("{0:c}",Eval("price"))%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="right" />
<HeaderStyle  HorizontalAlign="Right"/>
</asp:TemplateField>

<asp:TemplateField HeaderText="Total Price" HeaderStyle-Width="20%">
<ItemTemplate>
<asp:Label ID="Label5" runat="server" ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="right" />
<HeaderStyle  HorizontalAlign="Right"/>
</asp:TemplateField>
<%--<asp:TemplateField>
<FooterTemplate>
<asp:Label ID="lblSubTotal" runat="server" Text="Sub Total:&nbsp;"/><br />
<asp:Label ID="lblcoupondiscount" Visible="false" runat="server" Text="Discount:&nbsp;"/><br />
<asp:Label ID="lbltotal" runat="server" Visible="false" Text="Total:&nbsp;"/>

<asp:Label ID="lblSubTotalAmount" runat="server" Text=""/><br />
<br />
<asp:Label ID="lblTotalAmount" runat="server"  Visible="false" />
</FooterTemplate>
</asp:TemplateField>--%>
<%--<asp:TemplateField HeaderText="TotalPrice" ItemStyle-BackColor="#F7F7F7">
<ItemTemplate>
<asp:Label ID="Label5" runat="server" Text='<%#String.Format("{0:c}",Eval("total"))%>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>--%>
</Columns>
</asp:GridView>




<table width="100%" border="0" cellpadding="0" cellspacing="0" class="text">
<tr> 
<td height="30" align="right" style="width: 794px"><strong>Sub Total:&nbsp;&nbsp;&nbsp;</strong></td>
<td width="80" align="right"><strong>&nbsp;
<asp:Label ID="lblsub_total" runat="server" Text=""></asp:Label>&nbsp;&nbsp; </strong></td>
</tr>
<tr> 
<td height="30" align="right" style="width: 794px" ><strong>&nbsp;<asp:Label ID="lblship" runat="server" Visible="false" Text="Shipping Charge:"></asp:Label>&nbsp;&nbsp;&nbsp;</strong></td>
<td width="80" align="right"><strong>&nbsp;
<asp:Label ID="lblShipping" runat="server"></asp:Label>&nbsp;&nbsp; </strong></td>
</tr>

<tr> 
<td height="30" align="right" style="width: 794px"><strong>Total:</strong>&nbsp;&nbsp;&nbsp;</td>
<td align="right"><strong>&nbsp;
<asp:Label ID="lbltotal" runat="server" Text=""></asp:Label>&nbsp;&nbsp; </strong></td>
</tr>
<tr> 
<td height="1" align="right" bgcolor="#E6E6E6" style="width: 794px"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/dote.gif" width="1" height="1" /></td>
<td height="1" bgcolor="#E6E6E6"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/dote.gif" width="1" height="1" /></td>
</tr>
</table>
<br />
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td>
<strong>Place Order</strong>&nbsp;&nbsp;
</td>
</tr>
<tr>
<td>
<asp:RadioButtonList ID="rblorder" CellPadding="0" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblorder_SelectedIndexChanged">
<asp:ListItem Text="Credit Card" Value="0" ></asp:ListItem>
<asp:ListItem Text="Purchase Order" Value="1"></asp:ListItem>
</asp:RadioButtonList>
   
    <br />
</td>
</tr>
<tr id="trorder" runat="server" visible="false">
<td>
<strong>Enter your Purchase Order Number : </strong>
<%--</td>
</tr>
<tr>
<td>--%>
<asp:TextBox ID="txtPurchaseorderNumber" runat="server" MaxLength="25" Width="155px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPurchaseorderNumber"
        Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Purchase order Number" ValidationGroup="pus"></asp:RequiredFieldValidator>
        <br />
        <br />
         <asp:CheckBox ID="chkterm" runat="server" />I agree to PepTech Corp.'s <a  href="javascript:void(0);" onclick="window.open('Terms_Sale.aspx','Terms','width=700,height=550,scrollbars=yes,top=10,toolbar=no,left=200px,top=80px')"  >Terms of Sale.</a>
    <asp:CustomValidator ID="CustomValidator1" Display="None" ValidationGroup="pus" runat="server" ClientValidationFunction="agreed" ErrorMessage="Please Agree Terms of Sale."></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="pus" ShowMessageBox="true" ShowSummary="false" runat="server" />
    <br /><br />
</td>
</tr>
<tr>
<td></td>
</tr>
</table>
<%--<table  id="tblcoupon" runat="server" border="0" width="100%" cellpadding="0" cellspacing="0" class="text" >
<tr id="tblrow1" runat="server" visible="false" valign="top"> <td  colspan="2" style="height: 19px"><asp:Label runat="server" ID="lblcouponerrormessage" ForeColor="red"></asp:Label>  </td></tr>
<tr valign="top" id="tblrow2" runat="server" >
<td align="left" style="width:170px">Got a coupon code? Enter it here:
</td><td align="left">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Couponcode" />
<asp:RequiredFieldValidator ID="reqtxtcoupon" runat="server" ControlToValidate="txtcoupon" ErrorMessage="Please enter Coupon code." Display="None" ValidationGroup="Couponcode" SetFocusOnError="true"></asp:RequiredFieldValidator>
<asp:TextBox ID="txtcoupon" runat="server" Columns="12" CssClass="inp" MaxLength="25"></asp:TextBox>
<asp:ImageButton ID="imgcoupon" runat="server" imageurl="~/images/apply.gif"  OnClick="btncoupon_Click" CssClass="inp" ValidationGroup="Couponcode"  /></td>
</tr>
</table>--%>
<asp:Panel ID="Panel1" runat="server" DefaultButton="imgPlaceOrder">
<asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
<br /> <strong class="tital">CREDIT CARD INFO:</strong><br /> <br />
<table width="100%" border="0" cellpadding="2" cellspacing="0" class="text">
<tr>
<td style="width: 207px">
Card Type:</td>
<td>
<asp:DropDownList ID="cboCardType" runat="server">
<asp:ListItem Selected="True" Value="MasterCard">MasterCard</asp:ListItem>
<asp:ListItem Value="VisaCard">Visa</asp:ListItem>
<asp:ListItem Value="AmExCard">American Express</asp:ListItem>
<%--<asp:ListItem Value="DinersClubCard">Diners Club</asp:ListItem>--%>
<%--<asp:ListItem Value="DiscoverCard">Discover</asp:ListItem>--%>
<%-- <asp:ListItem Value="enRouteCard">enRoute</asp:ListItem>
<asp:ListItem Value="JCBCard">JCB</asp:ListItem>--%>
</asp:DropDownList>
</td>
</tr>
<tr> 
<td style="width: 207px">Name as appears in your card:</td>
<td>
<asp:TextBox ID="txtCardHolderName" runat="server" CssClass="inp" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCardHolderName"
        ErrorMessage="Please Enter Card Holder's Name" ValidationGroup="card" Display="None"></asp:RequiredFieldValidator>
</td>
</tr>
<tr> 
<td style="width: 207px">Card Number:</td>
<td>
<asp:TextBox ID="txtCreditCardNumber" autocomplete="off" runat="server" CssClass="inp" MaxLength="16"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCreditCardNumber"
        ErrorMessage="Please Enter Card Number" ValidationGroup="card" Display="None"></asp:RequiredFieldValidator>
</td>
</tr>
    <tr>
        <td style="width: 207px">
            Exp. Date:</td>
        <td>
<asp:DropDownList ID="cboExpMonth" runat="server">
</asp:DropDownList>
<asp:DropDownList ID="cboExpYear" runat="server">
</asp:DropDownList></td>
    </tr>
<tr> 
<td style="width: 207px">CVV/CVC Number:</td>
<td>
<asp:TextBox ID="txtCSVNumber" runat="server" CssClass="inp" MaxLength="5" Width="59px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCSVNumber"
        ErrorMessage="Please enter CSV Number" ValidationGroup="card" Display="None"></asp:RequiredFieldValidator></td>
</tr>
<tr>
<td colspan="2">
 <asp:CheckBox ID="chkTerms" runat="server" />I agree to PepTech Corp.'s <a  href="javascript:void(0);" onclick="window.open('Terms_Sale.aspx','Terms','width=700,height=550,scrollbars=yes,top=10,toolbar=no,left=200px,top=80px')"  >Terms of Sale.</a>
</td>
</tr>
<tr>
<td colspan="2">
<br />
<asp:ImageButton ID="imgPlaceOrder" runat="server" ImageUrl="images/place-order.gif"
OnClick="imgPlaceOrder_Click" OnClientClick="return CheckCardNumber()" AlternateText="Place Order" ValidationGroup="card" /></td>
</tr>
</table>
</asp:Panel>
<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/place-order.gif"
OnClick="ImageButton1_Click" AlternateText="Place Order" ValidationGroup="pus" Visible="False" />
    &nbsp;<br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="card" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;
<br /> 
<br />
<asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Size="Large" />
<br /> 
<br />
    <asp:HiddenField ID="hidorderno" runat="server" />
<br />
</td>
<td width="15"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/dote.gif" width="1" height="1" /></td>
</tr>
</table>
</td>
</tr>
</table></td>
<td width="5"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" width="1" height="1" /></td>
</tr>
</table>
</td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
</table>
<%--<form name="formFinal" action="https://developer.skipjackic.com/scripts/EvolvCC.dll?Authorize" method="post" >
  <input type="text" name="serialnumber"         value="000100448700" >
  <input type="text" name="orderstring"          value="<%=Session["orderstring"]%>" >
  <input type="text" name="ordernumber"          value="<%=Session["orderno"]%>" >
  
  <input type="text" name="accountnumber"        value='<%=Session["accountnumber"]%>' >
  <input type="text" name="month"                value='<%=Session["month"]%>' >
  <input type="text" name="year"                 value='<%=Session["year"]%>' >
  <input type="text" name="cvv2"                 value='<%=Session["cvv2"]%>' >
  
  <input type="text" name="sjname"               value='<%=Session["sjname"]%>' >
  <input type="text" name="streetaddress"        value='<%=Session["streetaddress"]%>' >
  <input type="text" name="streetaddress2"       value='<%=Session["streetaddress2"]%>' >
  <input type="text" name="city"                 value='<%=Session["city"]%>' >
  <input type="text" name="state"                value='<%=Session["state"]%>' >
  <input type="text" name="zipcode"              value='<%=Session["zipcode"]%>' >
  <input type="text" name="country"              value='<%=Session["country"]%>' > 
  <input type="text" name="shiptophone"          value='<%=Session["shiptophone"]%>' >
  <input type="text" name="email"                value='<%=Session["email"]%>' >
  
  <input type="text" name="shiptoname"           value='<%=Session["shiptoname"]%>' >
  <input type="text" name="shiptostreetaddress"  value='<%=Session["shiptostreetaddress"]%>' >
  <input type="text" name="shiptostreetaddress2" value='<%=Session["shiptostreetaddress2"]%>' >
  <input type="text" name="shiptocity"           value='<%=Session["shiptocity"]%>' >
  <input type="text" name="shiptostate"          value='<%=Session["shiptostate"]%>' >
  <input type="text" name="shiptozipcode"        value='<%=Session["shiptozipcode"]%>' >
  <input type="text" name="shiptocountry"        value='<%=Session["shiptocountry"]%>' > 

  <input type="text" name="transactionamount"    value='<%=Session["amt"]%>' />
  
  
</form> --%>
        </div>
        <div id="bottomDiv">
        <table width="1004" border="0" cellspacing="0" cellpadding="0">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="10">
<tr>
<td height="30" bgcolor="#10316B"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="whitetext">
<tr>
<td align="left"><strong class="smallwhitetext">&copy; 2009 PepTech Corporation. All Rights Reserved. </strong></td>
<td align="right"><strong><a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>Default.aspx" class="whitetext">Home</a> | <a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>Content.aspx?page=Contact+Us" class="whitetext">Contact Info</a> | <a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>jobs.aspx" class="whitetext">Job Opportunities</a> | <a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>Content.aspx?page=Privacy+Policy" class="whitetext">Privacy Policy</a> | <a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>RequestInfo.aspx" class="whitetext">Info Request</a> | <a href="<%=ConfigurationManager.AppSettings["websitepath1"] %>Download.aspx" class="whitetext">Download</a> </strong></td>
</tr>
</table></td>
</tr>
</table></td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
<tr>
<td width="17" height="18" align="left" valign="top"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/main-blcr.gif" alt="Corner" width="17" height="18" /></td>
<td height="18" align="left" valign="top" style="background-image:url(images/main-bbg.gif) "><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/space.gif" alt="Space" width="1" height="1" /></td>
<td width="17" height="18" align="left" valign="top"><img src="<%=ConfigurationManager.AppSettings["websitepath1"] %>images/main-brcr.gif" alt="corner" width="17" height="18" /></td>
</tr>
</table>
        </div>
    </div>
    </form>
</body>
</html>
