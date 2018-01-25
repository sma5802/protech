<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Login" Codebehind="Login.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top"><div align="justify"><span class="blueheading">Login</span><br /><br />
<asp:Label ID="lblMessage" runat="server" EnableViewState="false" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
<br /><br />
<asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButton1" Width="100%">
<table  border="0" cellpadding="2" cellspacing="0" class="text" Width="100%">
<tr>
<td width="100">Username:</td>
<td><asp:TextBox ID="txtUsername" runat="server" size="30" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtUsername" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator><%--<input name="textfield" type="text" size="30" />--%></td>
<td >
Don't have an account with us yet? <br />Please set up an account to place orders.</td>
</tr>
<tr>
<td>Password: </td>
<td><asp:TextBox ID="txtPassword" runat="server" size="30" TextMode="Password" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator></td>
    <td style="width: 415px">
    </td>
</tr>
<tr>
<td>&nbsp;</td>
<td><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/login.gif" AlternateText="Login" width="64" height="25" OnClick="ImageButton1_Click"  /></td>
    <td >
    Click <a class="bluelink" href="registration.aspx">here</a> to begin. 
    </td>
</tr>
<tr>
<td>&nbsp;</td>
<td><a class="bluelink" href="forgetpassowrd.aspx">Forgot your password?</a> </td>
    <td >
    </td>
</tr>
</table>
</asp:Panel>
<br />
<br />
<br />
<br />
<br />
<br />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="login" />

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
<div id="enlarge" style=" visibility:hidden;" >
    <div id="fade"></div>
    <div class="popup_block">
    <div class="popup">
    
<table width="100%" style="border:solid 4px black; >
<tr><td align="center" valign="middle">
 <table width="70%" style="border:solid 4px black; background-color:white; margin-bottom:200px; margin-top:200px" >
 <tr>
 <td colspan="2">
     <strong class="blueText1">For security reasons, a new password is required. Please provide a new password.</strong>
 </td>
 </tr>
 <tr>
 <td colspan="2">
     <asp:Label ID="lbler" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
 </td>
 </tr>
 <tr>
 <td align="left" width="40%">
 <strong>Username:</strong>
 </td>
 <td align="left">
     <asp:Label ID="lblUsername" runat="server" ></asp:Label>
 </td>
 </tr>
 <tr>
 <td align="left">
 <strong>Email:</strong>
 </td>
 <td align="left">
     <asp:TextBox ID="txtEmail" Width="200" MaxLength="150" runat="server"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtEmail" Display="None" 
        ErrorMessage="Please enter Email" SetFocusOnError="True" ></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" 
        Display="None" ValidationGroup="reg" ControlToValidate="txtEmail" 
        ErrorMessage="Please enter Correct Email" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
        SetFocusOnError="True"></asp:RegularExpressionValidator>
     
 </td>
 </tr>
 <tr>
 <td align="left">
 <strong>New Password:</strong>
 </td>
 <td align="left">
  <asp:TextBox ID="txtNewPassword" TextMode="Password" Width="200" MaxLength="20" runat="server"></asp:TextBox>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtNewPassword" Display="None" 
        ErrorMessage="Please enter New Password" SetFocusOnError="True" ></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
        ControlToValidate="txtNewPassword" Display="None" 
        ErrorMessage="New Password must contain atleast 4 characters" 
        SetFocusOnError="True" ValidationExpression=".{4,50}" ValidationGroup="reg"></asp:RegularExpressionValidator>
 </td>
 </tr>
 <tr>
 <td align="left">
 <strong>Confirm New Password:</strong>
 </td>
 <td align="left">
  <asp:TextBox ID="txtNewCPassword" TextMode="Password" Width="200" MaxLength="20" runat="server"></asp:TextBox>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtNewCPassword" Display="None" 
        ErrorMessage="Please enter Confirm New Password" SetFocusOnError="True" ></asp:RequiredFieldValidator>
  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
ControlToValidate="txtNewCPassword" Display="None" ErrorMessage="Confirm New Password do not match"
ValidationGroup="reg" SetFocusOnError="True"></asp:CompareValidator>
 </td>
 </tr>
 <tr>
<td>&nbsp;<asp:ValidationSummary ValidationGroup="reg" ShowMessageBox="true" ShowSummary="false" ID="ValidationSummary2" runat="server" />
</td>
<td align="left">
    <asp:ImageButton ID="imgChangePwd" ValidationGroup="reg" 
        ImageUrl="~/images/submit.gif" runat="server" onclick="imgChangePwd_Click" />
        &nbsp;
        <a href="javascript:void(0);" onclick="javascript:document.getElementById('enlarge').style.visibility='hidden';" border="0"><img border="0" src="images/cancel.gif" /></a>
</td>
</tr>
 </table>
</td></tr>

</table>

 </div>
 </div>
 </div>
</asp:Content>

