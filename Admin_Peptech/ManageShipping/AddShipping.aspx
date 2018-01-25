<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_ManageShipping_AddShipping" Codebehind="AddShipping.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function maxlength(txt,maxlength)
{
   if(txt.value.length > (maxlength-1))
   {
       txt.value = txt.value.slice(0,maxlength);
       return false;
   }
}

</script>
<h2>Add Shipping Charge</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg"></asp:Label>
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle"  >
<table class="box" width="100%" >
    <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label4" runat="server" Text="Location:" Font-Bold="true"></asp:Label>

        
        </td>
        <td align="left" valign="middle">
            <asp:DropDownList ID="ddlcountry1" runat="server" Width="150px">
                <asp:ListItem Selected="True" Text="United States" Value="254"></asp:ListItem>
                <asp:ListItem Value="255">Outside United States</asp:ListItem>
                <asp:ListItem Value="256">International</asp:ListItem>
                	
            </asp:DropDownList></td>
    </tr>
   <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label2" runat="server" Text="Service:" Font-Bold="true"></asp:Label>

        
        </td>
        <td align="left" valign="middle">
           <asp:TextBox ID="txtService" Width="250" runat="server" MaxLength="200" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Service" ValidationGroup="insertcategory" Display="None" ControlToValidate="txtService" SetFocusOnError="true"/>
            </td>
    </tr>
<tr>
<td align="left" valign="top"  >
<asp:Label ID="Label1" runat="server" Text="Price:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
  <asp:TextBox ID="txtPrice" Width="250" runat="server" MaxLength="200" />
  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPrice"
        ErrorMessage="Please Enter Price" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtPrice" Display="None" 
        ErrorMessage="Correct Price Required" MaximumValue="999999" MinimumValue="1" 
        Type="Double" ValidationGroup="insertcategory"></asp:RangeValidator>
</td>
</tr>
<tr>
<td ></td>
<td>
<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>
<%--<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/></td>
--%></tr>
</table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="insertcategory" />
</td>
</tr>
</table>

</asp:Content>

