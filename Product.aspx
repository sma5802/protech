<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Product" Codebehind="Product.aspx.cs" %>
<%@ Register TagPrefix="uc2" TagName="prdList" Src="~/mnuProduct.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top"><div align="justify">

<%-- <a href="default.aspx" class="bluelink">Home</a> &gt; Product &gt; <asp:Label runat="server" id="lblcat" visible="false"></asp:Label> &gt; <asp:Label runat="server" id="lblsubcat" visible="false"></asp:Label>&gt;<asp:Label runat="server" id="lblProd" visible="false"></asp:Label><br />--%>
<br />
<table width="100%"  border="0" cellspacing="0" cellpadding="0">
<tr>
<td><span class="blueheading">
<asp:Label runat="server" id="lblProduct" ></asp:Label></span></td>
<td align="right"><%--<a class="bluelink" href="#">Next &gt;&gt;</a>--%></td>
</tr>
</table>
<br />
<br />
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="230"><asp:Image ID="imgProduct" runat="server"  />
</td>
<td valign="top"><ul style="MARGIN-LEFT: 15pt">
<li>CAS #&nbsp; :&nbsp;<asp:Label runat="server" id="lblCAS" ></asp:Label></li><li>Formula&nbsp;:&nbsp; <asp:Label runat="server" id="lblFormula" CssClass="text"></asp:Label></li>
<li>Molecular Weight&nbsp;:&nbsp;<asp:Label runat="server" id="lblMWeight" ></asp:Label></li></ul></td>
</tr>
</table>
<br />
<asp:Label ID="lblErrorMessage" runat="server" Visible="false" EnableViewState="false" ForeColor="Red" Font-Bold="true"></asp:Label>
<table width="100%"  border="0" cellpadding="10" cellspacing="1" bgcolor="#E1E1E1" class="text">
<tr>
<td align="left" valign="top" bgcolor="#F9F8F8" width="30%">
<uc2:prdList id="prdList" runat="server" />  
</td>
<td align="left" valign="top" bgcolor="#F9F8F8">
<asp:GridView ID="grdListProperty" Width="100%" OnRowDataBound="grdListProperty_OnRowBound" AllowPaging="true" PageSize="15"  AutoGenerateColumns="false" runat="server" CellPadding="6" EmptyDataText="No catalogs available." OnPageIndexChanging="grdListProperty_PageIndexChanging" OnRowCommand="grdListProperty_RowCommand" >
<HeaderStyle BackColor="#F1F0F0"   />
<RowStyle Height="20px" BackColor="#FFFFFF" />
<AlternatingRowStyle BackColor="#F1F0F0"  />
<PagerStyle  HorizontalAlign="Center" />
<Columns>
<asp:TemplateField HeaderText="Catalog #" >
<ItemTemplate>
<asp:Label ID="lblcountiesname" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("CatalogName").ToString())%>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Size (g)">
<ItemTemplate>
<asp:Label ID="lblcountiesname1" runat="server" Text='<%# String.Format(Eval("Quantity").ToString()) %>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Price">
<ItemStyle HorizontalAlign="Right" />
<ItemTemplate>
<asp:Label ID="lblcountiesname2" runat="server" Text='<%# Eval("price","{0:c}")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Quantity">
<ItemTemplate >
<asp:TextBox ID="txtQty" runat="server" Width="30" Text='<%#Eval("unit") %>' MaxLength="3"></asp:TextBox><br />
<asp:RequiredFieldValidator ID="ReqtxtQty" runat="server" ErrorMessage="Please enter valid quantity" ControlToValidate="txtQty" SetFocusOnError="true" Display="Dynamic" ValidationGroup="updateqty" ></asp:RequiredFieldValidator>
<asp:CompareValidator ID="comtxtQty" runat="server" ControlToValidate="txtQty" ErrorMessage="Please enter Only Integer"
Operator="GreaterThan" Type="Integer" SetFocusOnError="true" Display="Dynamic" ValueToCompare="0" ValidationGroup="updateqty" ></asp:CompareValidator>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>

<asp:TemplateField  ShowHeader="False">
<ItemStyle HorizontalAlign="Center" />
<HeaderStyle HorizontalAlign="Center" />
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server"  ValidationGroup="updateqty"  CommandArgument='<%#Eval("id")%>' >
<img src="images/add-to-cart.gif" alt="Add To Cart" width="106" height="25" border="0" /></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<%--<asp:TemplateField  ShowHeader="False">
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton ID="lnkUpdate" runat="server" CommandArgument="all" CommandName="updatecart" ValidationGroup="updateqty" OnCommand="mycart_updatecommand"><img src="images/update.gif" alt="Update" width="74" height="25" hspace="5" border="0" />
</asp:LinkButton></ItemTemplate>
</asp:TemplateField>
--%>
</Columns>
</asp:GridView>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="updateqty" ShowMessageBox="false" ShowSummary="false" />
</td>
</tr>
</table>
<br />
</div>
</td>
</tr>
</table>
</td>
<td width="5"><img src="images/space.gif" width="1" height="1" /></td>
</tr>
</table>
</td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
</table>
</asp:Content>
