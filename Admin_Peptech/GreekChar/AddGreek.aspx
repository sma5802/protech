<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_GreekChar_AddGreek" Codebehind="AddGreek.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Add Greek Character</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg"></asp:Label>
<asp:Panel ID="pnlSubmit" runat="server" >
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="lblCounties" runat="server" Text="Enter Greek Character:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
<asp:TextBox ID="txtCounties" runat="server" MaxLength="200"  ValidationGroup="insertcategory" Width="250"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqcategory" runat="server" ControlToValidate="txtCounties"
        ErrorMessage="Required" ValidationGroup="insertcategory"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td ></td>
<td>
<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>
</td>
</tr>
</table>
    </asp:Panel>
</asp:Content>

