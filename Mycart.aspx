<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" Inherits="Mycart" Codebehind="Mycart.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function SelectAll(obj)
{
var CheckValue =  obj.checked;

var objCheckBoxes = document.getElementsByTagName("input");
if(!objCheckBoxes)
return;
var countCheckBoxes = objCheckBoxes.length;

if(countCheckBoxes == 1)
{

if (objCheckBoxes.name.indexOf("grdListProperty")>=0 && objCheckBoxes.name.indexOf("statusCheck")>=0)
objCheckBoxes.checked = CheckValue;
}
else
{
// set the check value for all check boxes
for(var i = 0; i < countCheckBoxes; i++)
if (objCheckBoxes[i].type == "checkbox")
if (objCheckBoxes[i].name.indexOf("grdListProperty")>=0 && objCheckBoxes[i].name.indexOf("statusCheck")>=0)
objCheckBoxes[i].checked = CheckValue;
}
}
</script>
<%--<script language="javascript" type="text/javascript">
function check()
{
    var _ret=true;
    var val12=document.getElementById('<%=lblShipping.ClientID %>').innerHTML;
    if(val12=="")
    {
        alert("Please select proper shipping charge");
        _ret=false;
    }
    return _ret;
}
</script>--%>
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top">
    <br />
<div align="justify"><span class="blueheading">Shopping Cart</span><br />
<br />
Here is your shopping cart. When you are ready to place an order, please select 
"Checkout". <br />
<asp:Label ID="lblmessage" runat="server" Visible="False" EnableViewState="False" 
        Font-Bold="True"></asp:Label>
<br />
<table width="100%" border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1" class="btext">
<tr>
<td valign="top" bgcolor="#F9F8F8">
<asp:GridView ID="grdListProperty" Width="100%" 
        OnRowDataBound="grdListProperty_OnRowBound"  AutoGenerateColumns="False" 
        runat="server" CellPadding="6" EmptyDataText="No catalogs available." 
        OnPageIndexChanging="grdListProperty_PageIndexChanging" DataSourceID="sqlCart" 
        ondatabound="grdListProperty_DataBound"  >
 <HeaderStyle BackColor="#F1F0F0"   />
<RowStyle Height="20px" BackColor="White" />
<AlternatingRowStyle BackColor="#F1F0F0"  />
<PagerStyle  HorizontalAlign="Center" />
<Columns>
<asp:TemplateField >
<HeaderTemplate>
<asp:CheckBox ID="chk_checkall" Text="Remove" runat="server" Checked="false" onclick="SelectAll(this);"/>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="statusCheck" runat="server" />
<asp:HiddenField ID="checkid" runat="server" Value='<%# Eval("TempID") %>' />
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Item" >
<ItemTemplate>
<asp:Label ID="lblcountiesname" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("ProductName").ToString())%>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Size (g)">
<ItemTemplate>
<asp:Label ID="lblcountiesname1" runat="server" Text='<%# String.Format(Eval("ProductQuantity").ToString()) %>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Unit Price">
<ItemTemplate>
<asp:Label ID="lblcountiesname2" runat="server" Text='<%# Eval("price","{0:c}")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Quantity">
<ItemTemplate>
    <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("Qty") %>' MaxLength="3" Width="30"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ReqtxtQty" runat="server" ErrorMessage="Please enter valid quantity" ControlToValidate="txtQty" SetFocusOnError="true" Display="None" ValidationGroup="updateqty" ></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="comtxtQty" runat="server" ControlToValidate="txtQty" ErrorMessage="Please enter Only Integer"
                        Operator="GreaterThan" Type="Integer" SetFocusOnError="true" Display="None" ValueToCompare="0" ValidationGroup="updateqty" ></asp:CompareValidator>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>

<asp:TemplateField HeaderText="SubTotal">
<ItemTemplate>
   <asp:Label ID="lblSubTotal" runat="server" ></asp:Label>
</ItemTemplate>  
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>

</Columns>
</asp:GridView>
    <asp:SqlDataSource ID="sqlCart" runat="server" 
        ConnectionString="<%$ ConnectionStrings:dbConnect %>" 
        SelectCommand="SELECT * FROM pep$tech$corp.peptech_ShoppingBagTMP WHERE UserID=@UserID">
        <SelectParameters>
            <asp:SessionParameter Name="UserID" SessionField="UserId" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</td>
</tr>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="text">
<tr bgcolor="#F1F0F0"> 
<td height="30" align="right" ><strong>
    <asp:Label ID="lblsubtotal" runat="server" Visible="False"></asp:Label>&nbsp;Sub Total:&nbsp;&nbsp;&nbsp;</strong></td>
<td width="80" align="right" ><strong><asp:Label ID="lblsub_total" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr bgcolor="#F1F0F0" style="display:none"> 
<td height="30" align="right" ><strong>&nbsp;<asp:Label ID="lblship" runat="server" Visible="False" Text="Shipping Charge:"></asp:Label>&nbsp;&nbsp;&nbsp;</strong></td>
<td width="80"><strong>&nbsp;
<%--<asp:Label ID="lblShipping" runat="server" Visible="False">00</asp:Label>--%></strong></td>
</tr>
<tr bgcolor="#F1F0F0"> 
<td align="right" height="30px" ><strong>Total:</strong>&nbsp;&nbsp;&nbsp;</td>
<td height="30px" align="right" ><strong><asp:Label ID="lbltotal" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
</table>
<br />

<table width="100%"  border="0" cellpadding="5" cellspacing="0" class="text">
<tr>
<td width="50%">
<asp:LinkButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click" OnClientClick="javascript:return confirm('Are you sure  want to delete selected product(s) from the shopping cart?')" CommandArgument='<%#Eval("tempid") %>' BorderWidth="0px"><img src="images/remove.gif" alt="Remove" width="78" height="25" border="0" /></asp:LinkButton><a href="Categories.aspx"><img src="images/continue-shopping.gif" alt="Continue Shopping" width="150" height="25" hspace="5" border="0" /></a></td>
<td align="right">
<asp:LinkButton ID="lnkUpdate" runat="server" CommandArgument="all" CommandName="updatecart" ValidationGroup="updateqty" OnCommand="mycart_updatecommand"><img src="images/update.gif" alt="Update" width="74" height="25" hspace="5" border="0" />
</asp:LinkButton>
<asp:LinkButton ID="lnkCheckout" runat="server" OnClick="lnkCheckout_Click" ><img src="images/checkout.gif" alt="Checkout" width="92" height="25" border="0" /></asp:LinkButton>
</td>
</tr>
</table>
<br />
<br />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="updateqty" ShowMessageBox="true" ShowSummary="false" />
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

