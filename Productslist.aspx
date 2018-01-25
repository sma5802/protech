<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Productslist" Codebehind="Productslist.aspx.cs" %>
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
                   <%-- <a href="peptech-home.asp" class="bluelink">Home</a> &gt; <a class="bluelink" 
href="product-categories.asp">Categories</a> &gt; <a 
class="bluelink" href="sub-product-categories.asp">&alpha;-amino acid analogs</a> &gt; Aromatic Amino Acid Analogs<br />--%>
                      <br />
                      <span class="blueheading">Products</span><br />
                        <br />
                        <table width="100%"  border="0" cellpadding="5" cellspacing="1" bgcolor="#CCCCCC" class="text">
                          <tr>
                            <td align="left" valign="top" bgcolor="#FAFAFA"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
                             <%-- <tr>
                                <td><b>Total # of Products: 307&nbsp;&nbsp;&nbsp;&nbsp;</b> Showing Products 1 to 20 </td>
                                <td><input type="submit" name="Submit" value=" Prev " /></td>
                                <td>Page</td>
                                <td>
                             </td>
                                <td>of&nbsp;16</td>
                                <td><input type="submit" name="Submit" value=" Next " /></td>
                              </tr>--%>
                            </table></td>
                          </tr>
                        </table>
                        <br />
                        <table width="100%"  border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1" class="text">
                          <tr>
                            <td align="left" valign="top" bgcolor="#F9F8F8" width="100%">
<asp:DataList ID="dlsprod" runat="server" Width="100%" OnItemDataBound="dlsprod_ItemDataBound" RepeatColumns="2" RepeatDirection="horizontal">
<ItemTemplate>
<table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#E1E1E1" class="text">
  <tr bgcolor="#F1F0F0" class="bluetext" valign="top">
                                  <td width="30%" align="left" valign="top">&nbsp;<strong>Product Name</strong></td>
                                  <td align="left" width="70%"><strong>Image</strong></td>
                                 <%-- <td align="center"><strong>Formula</strong></td>
                                  <td align="center"><strong>M.W.</strong></td>--%>
  </tr>    
     <tr bgcolor="#FFFFFF" valign="top">
       <td width="30%" bgcolor="#FFFFFF">
       <asp:HyperLink ID="hlProdut" runat="server" CssClass="bluelink" Text='<%#HttpUtility.HtmlDecode(Eval("ProductName").ToString())%>' NavigateUrl='<%# "Product.aspx?pid="+Eval("id") %>'></asp:HyperLink><%--<a href="products-detail.asp" class="bluelink">L-2-Chlorophe</a> --%></td>
       <td  align="left" width="70%"><asp:HyperLink ID="hlImage" runat="server" NavigateUrl='<%# "Product.aspx?pid="+Eval("id") %>'><asp:Image ID="imgProd" runat="server" ImageUrl='<%#Eval("ProductImage","~/ProductImage/{0}")%>' /></asp:HyperLink></td>
      <%-- <td align="center">C<span style="font-size:7pt;vertical-align:sub;font-family:verdana;line-height:16pt">9</span>H<span style="font-size:7pt;vertical-align:sub;font-family:verdana;line-height:16pt">1</span><span style="font-size:7pt;vertical-align:sub;font-family:verdana;line-height:16pt">0</span>ClNO<span style="font-size:7pt;vertical-align:sub;font-family:verdana;line-height:16pt">2</span></td>
       <td align="center">199.63</td>--%>
    </tr>
    </table>
</ItemTemplate>
</asp:DataList> 
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

