<%@ Page Language="C#" AutoEventWireup="true" Inherits="SkipPayment" Codebehind="SkipPayment.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Peptech Payment Process</title>
</head>
<body>
<%--    <form name="formFinal" action="https://developer.skipjackic.com/scripts/EvolvCC.dll?Authorize" method="post" >--%>
    <form id="formFinal" runat="server" name="formFinal" action="https://www.skipjackic.com/scripts/evolvcc.dll?Authorize" method="post">
    
    <div align="center"><h2>
    <span style="font-family: Arial, Helvetica, sans-serif;	color: #0068B3;text-decoration: none;">
    <img src="images/space.gif" height="60px" /><br />Please wait while your payment is being processed.<br />
Pressing your browser’s BACK or REFRESH buttons could result in duplication of your order.
<br /><br />
Processing....

   <%-- Please wait while your payment is being processed & don't press Browser's back button while processing......--%></span>
    	</h2>
  <input type="hidden" name="serialnumber"         value="000010185525" /> <%--Serial number of peptech clients skipjack account --%>
  <%--<input type="hidden" name="serialnumber"         value="000100448700" >--%>
  <input type="hidden" name="orderstring"          value="<%=Session["orderstring"]%>" />
  <input type="hidden" name="ordernumber"          value="<%=Session["orderno"]%>" />

  <input type="hidden" name="accountnumber"        value='<%=Session["accountnumber"]%>' />
  <input type="hidden" name="month"                value='<%=Session["month"]%>' />
  <input type="hidden" name="year"                 value='<%=Session["year"]%>' />
  <input type="hidden" name="cvv2"                 value='<%=Session["cvv2"]%>' />
  
  <input type="hidden" name="sjname"               value='<%=Session["sjname"]%>' />
  <input type="hidden" name="streetaddress"        value='<%=Session["streetaddress"]%>' />
  <input type="hidden" name="streetaddress2"       value='<%=Session["streetaddress2"]%>' />
  <input type="hidden" name="city"                 value='<%=Session["city"]%>' />
  <input type="hidden" name="state"                value='<%=Session["state"]%>' />
  <input type="hidden" name="zipcode"              value='<%=Session["zipcode"]%>' />
  <input type="hidden" name="country"              value='<%=Session["country"]%>' />
  <input type="hidden" name="shiptophone"          value='<%=Session["shiptophone"]%>' />
  <input type="hidden" name="email"                value='<%=Session["email"]%>' />
  
  <input type="hidden" name="shiptoname"           value='<%=Session["shiptoname"]%>' />
  <input type="hidden" name="shiptostreetaddress"  value='<%=Session["shiptostreetaddress"]%>' />
  <input type="hidden" name="shiptostreetaddress2" value='<%=Session["shiptostreetaddress2"]%>' />
  <input type="hidden" name="shiptocity"           value='<%=Session["shiptocity"]%>' />
  <input type="hidden" name="shiptostate"          value='<%=Session["shiptostate"]%>' />
  <input type="hidden" name="shiptozipcode"        value='<%=Session["shiptozipcode"]%>' />
  <input type="hidden" name="shiptocountry"        value='<%=Session["shiptocountry"]%>' />
  <input type="hidden" name="ShippingAmount"       value='<%=Session["shippamt"]%>' />
  <input type="hidden" name="transactionamount"    value='<%=Session["amt"]%>' />
  <input type="hidden" name="comment"              value='<%=Session["comment"]%>' />
  
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
 document.forms["formFinal"].submit();
 </script>
</html>
