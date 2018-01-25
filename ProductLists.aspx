<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="ProductLists" Codebehind="ProductLists.aspx.cs" %>
<%@ Register TagPrefix="uc2" TagName="prdList" Src="~/mnuProduct.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top">
<div align="justify">
<br />
<span class="blueheading">Products</span><br />
<br />
<div align="left">
<asp:HyperLink ID="hlhome" runat="server" CssClass="bluelink" Font-Bold="true" NavigateUrl="~/default.aspx" Text="Home"></asp:HyperLink>&nbsp;<b class="bluetext">>></b>&nbsp;<asp:HyperLink
ID="hlproduct" runat="server" CssClass="bluelink" Font-Bold="true" NavigateUrl="~/categories.aspx" Text="Product"></asp:HyperLink>&nbsp;<b id="a2" runat="server" class="bluetext"><asp:Label ID="Label1" runat="server" Text=">>"></asp:Label></b>&nbsp;<asp:HyperLink ID="hlcat" Font-Bold="true" CssClass="bluelink" runat="server" ></asp:HyperLink>&nbsp;<b id="a1" runat="server" class="bluetext"><asp:Label ID="Label2" runat="server" Text=">>"></asp:Label></b>&nbsp;<asp:Label ID="lblSubCat" Font-Bold="true" CssClass="bluetext" runat="server" ></asp:Label>
</div>
<table width="100%"  border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1" class="text">
<tr>
<td align="left" valign="top" bgcolor="#F9F8F8" width="30%">
 <uc2:prdList id="prdList" runat="server" />  
</td>

<td align="left" valign="top" bgcolor="#F9F8F8">
<asp:DataList ID="dlsprod" runat="server" Width="100%" OnItemDataBound="dlsprod_ItemDataBound" RepeatColumns="1" RepeatDirection="horizontal">
<HeaderTemplate>
<table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#E1E1E1" class="text">
<tr bgcolor="#F1F0F0" class="bluetext" valign="top">
<td  align="left" valign="top" width="52%">&nbsp;<strong><asp:LinkButton ID="lbProd"
    runat="server" onclick="lbProd_Click">Product Name</asp:LinkButton></strong></td>
<td align="left" width="16%" ><strong>
    <asp:LinkButton ID="lbCAS"
    runat="server" onclick="lbCAS_Click">CAS #</asp:LinkButton></strong></td>
<td align="left" width="16%"><strong>
    <asp:LinkButton ID="lbFormula"
    runat="server" onclick="lbFormula_Click">Formula</asp:LinkButton></strong></td>
<td align="left" width="16%"><strong>
    <asp:LinkButton ID="lbMweight"
    runat="server" onclick="lbMweight_Click">M.W.</asp:LinkButton></strong></td>
</tr>    
</table>
</HeaderTemplate>
<ItemTemplate>
<table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#E1E1E1" class="text">
<tr bgcolor="#FFFFFF" valign="top">
<td  bgcolor="#FFFFFF" width="52%">
<asp:HyperLink ID="hlProdut" runat="server" CssClass="bluelink" Text='<%#HttpUtility.HtmlDecode(Eval("ProductName").ToString())%>' NavigateUrl='<%# "Product.aspx?pid="+Eval("id") %>'></asp:HyperLink><%--<a href="products-detail.asp" class="bluelink">L-2-Chlorophe</a> --%></td>
<td  align="left" width="16%"><asp:Label ID="lblCAS" runat="server" Text='<%#Eval("CAS") %>'></asp:Label></td>
<td align="left" width="16%">
<asp:Label ID="lblformula" runat="server" Text='<%#Eval("formula") %>'></asp:Label></td>
<td align="left" width="16%"> <asp:Label ID="lblWeight" runat="server"></asp:Label></td>
</tr>
</table>
</ItemTemplate>
<AlternatingItemTemplate>
<table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#E1E1E1" class="text">
<tr bgcolor="#F1F0F0" class="bluetext" valign="top">
<td  width="52%">
<asp:HyperLink ID="hlProdut" runat="server" CssClass="bluelink" Text='<%#HttpUtility.HtmlDecode(Eval("ProductName").ToString())%>' NavigateUrl='<%# "Product.aspx?pid="+Eval("id") %>'></asp:HyperLink><%--<a href="products-detail.asp" class="bluelink">L-2-Chlorophe</a> --%></td>
<%-- <td  align="left" ><asp:HyperLink ID="hlImage" runat="server" NavigateUrl='<%# "Product.aspx?pid="+Eval("id") %>'><asp:Image ID="imgProd" runat="server" ImageUrl='<%#Eval("ProductImage","~/ProductImage/{0}")%>' /></asp:HyperLink></td>--%>
<td  align="left" width="16%"><asp:Label ID="lblCAS" runat="server" Text='<%#Eval("CAS") %>'></asp:Label></td>
<td align="left" width="16%">
<asp:Label ID="lblformula" runat="server" Text='<%#Eval("formula") %>'></asp:Label></td>
<td align="left" width="16%"> <asp:Label ID="lblWeight" runat="server"></asp:Label></td>
</tr>    
</table>
</AlternatingItemTemplate>
</asp:DataList> 
<div align="center"><asp:Panel ID="pnlPager" runat="server" EnableViewState="False"/></div>  

<asp:Panel ID="pnlmsg" runat="server" Visible="false">
<center>
<strong class="redheading1">No Product Exists</strong>
</center>
</asp:Panel>
</td>
</tr>
</table>
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

