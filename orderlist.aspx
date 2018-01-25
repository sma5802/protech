<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="orderlist" Codebehind="orderlist.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    
                <td align="left" valign="top">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr align="left" valign="top"> 
<td width="15">
<img src="images/dote.gif" width="4" height="1" />
</td>
<td><br /><strong class="title">
ORDER HISTORY</strong>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href="UserAccount.aspx" class="bluelink">Back</a>
<br /> <br /> 

<asp:GridView ID="GridView1" ShowFooter="false"  AllowPaging="true" PageSize="50"  AutoGenerateColumns="false" CssClass="text" Width="100%" BorderWidth="1px" BorderColor="#f3f3f3" cellpadding="5" cellspacing="1"  runat="server" OnPageIndexChanging="GridViw1_OnPageIndexChanging" EmptyDataRowStyle-ForeColor="red" EmptyDataRowStyle-BorderWidth="1" EmptyDataRowStyle-BorderColor="#F6F6F6" EmptyDataText="No order found." GridLines="Both" >
<HeaderStyle Font-Bold="True" HorizontalAlign="Left" BackColor="WhiteSmoke" />
<RowStyle Height="20px" BackColor="White" />

<Columns>
<asp:BoundField HeaderText="OrderNo" DataField="Orderno" >
<ItemStyle HorizontalAlign="Left" BackColor="White" />
</asp:BoundField>
<%--<asp:TemplateField HeaderText="Order Status">
<ItemTemplate>
<%# Eval("OrderStatus") %>
</ItemTemplate>
<ItemStyle BackColor="White" HorizontalAlign="Left" />
</asp:TemplateField>--%>  
<asp:BoundField HeaderText="Net Amount" DataField="Netamount" DataFormatString="{0:c}" HtmlEncode="False" >
<ItemStyle HorizontalAlign="Right" BackColor="White" />
<HeaderStyle HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField HeaderText="Order Date" DataField ="Orderdate" DataFormatString='{0:M-dd-yyyy}' HtmlEncode="False">
<ItemStyle HorizontalAlign="Right" BackColor="White" />
<HeaderStyle HorizontalAlign="Right" />

</asp:BoundField>

<%--<asp:BoundField HeaderText="IsPaid" DataField="Ispaid" HeaderStyle-HorizontalAlign="Right" >
<ItemStyle HorizontalAlign="Right" BackColor="White" />
</asp:BoundField>--%>

<asp:TemplateField HeaderText="Action">
<ItemTemplate >
<a href="Vieworder.aspx?ID=<%# Eval("OrderNo") %>&userid=<%# Eval("UserID") %>" class="bluelink">View</a></ItemTemplate>
<ItemStyle HorizontalAlign="Right" BackColor="White" />
<HeaderStyle HorizontalAlign="Right" />
</asp:TemplateField>
</Columns>
<SelectedRowStyle BackColor="#EFEFEF" Font-Bold="True" ForeColor="Black" />
<PagerStyle Font-Bold="True" BackColor="WhiteSmoke" HorizontalAlign="Center" />
    <EmptyDataRowStyle BorderColor="#F6F6F6" BorderWidth="1px" ForeColor="Red" />
</asp:GridView>            
<br />
<br />
<br />
<br />
</td>
<td width="20">&nbsp;</td>
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

