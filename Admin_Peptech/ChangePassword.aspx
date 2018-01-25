<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_ChangePassword" Codebehind="ChangePassword.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Admin Change Passowrd</h2>
<asp:Label ID="lblMsg"  ForeColor="Red" Font-Bold="False" runat="server" Visible="False"></asp:Label>
<asp:Panel ID="pnlSubmit" runat="server" >
<table class="box" width="100%" >
<tr> 
<td style="width: 140px;" ><b> Enter Old Password</b></td>
<td> <asp:TextBox ID="txtPwd" TextMode="Password" Width="200" runat="server"></asp:TextBox> 
<asp:RequiredFieldValidator ID="rqvPwd" runat="server" ErrorMessage="Please Enter Old Password" ValidationGroup="password" ControlToValidate="txtPwd" Display="None"></asp:RequiredFieldValidator></td>
</tr>
<tr style="text-align:left"> 
<td style="width: 140px;" ><b> Enter New Password</b></td>
<td> <asp:TextBox ID="txtNewPwd" runat="server" Width="200" TextMode="Password"></asp:TextBox> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="password" ErrorMessage="Please Enter New Password" ControlToValidate="txtNewPwd" Display="None"></asp:RequiredFieldValidator></td>
</tr>
<tr style="text-align:left"> 
<td style="width: 140px;"><b> Re-Enter New Password</b></td>
<td> <asp:TextBox ID="txtReEnterNewPwd" runat="server" TextMode="Password" Width="200"></asp:TextBox> 
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPwd"
        ControlToValidate="txtReEnterNewPwd" Display="None" ErrorMessage="Passwords do not match"
        Width="148px" ValidationGroup="password"></asp:CompareValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReEnterNewPwd"
        Display="None" ErrorMessage="Re-Enter New Password" ValidationGroup="password"></asp:RequiredFieldValidator></td>
</tr>

<tr>
<td ></td>
<td>
<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="password" />
<%--<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>
--%> <asp:ValidationSummary ID="valuser" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="password"/> 
</td>
</tr>
</table>
    </asp:Panel>
</asp:Content>

