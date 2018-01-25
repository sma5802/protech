<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Forgetpassowrd" Codebehind="Forgetpassowrd.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><br />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmsg" runat="server" EnableViewState="false" Visible="false" ForeColor="Red" Font-Bold="True"></asp:Label>
<table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top"><div align="justify"><span class="blueheading">Retrieve Your Password</span><br />
<br />
<strong>&nbsp;Enter the e-mail address associated with your peptechcorp.com account, then click Submit.<br />
&nbsp;Your password will be emailed to you shortly. </strong><br /><br />
<table width="100%"  border="0" cellpadding="2" cellspacing="0" class="text">
<tr>
<td width="210"> E-mail address:</td>
<td><asp:TextBox ID="txtemail" runat="server" size="30" MaxLength="200"></asp:TextBox><%--<input name="textfield" type="text" size="30" />--%></td>
<asp:RequiredFieldValidator ID="reqemail" ValidationGroup="reg" runat="server" ControlToValidate="txtemail" Display="None" ErrorMessage="Please Enter email." ></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator id="Regvalidemail" runat="server" ValidationGroup="reg" SetFocusOnError="True" ErrorMessage="The Email Address you entered is wrong .Please enter a valid Email Address" Display="None" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
</tr>
<tr>
<td>Please re-confirm your email address: </td>
<td><asp:TextBox ID="txtreemail" runat="server" size="30" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqremail" ValidationGroup="reg" runat="server" ControlToValidate="txtreemail" Display="None" ErrorMessage="Please Enter Confirm Email" ></asp:RequiredFieldValidator>
<asp:CompareValidator ID="comemail" runat="server" ControlToCompare="txtemail" ControlToValidate="txtreemail" Display="None" ErrorMessage="Email address invalid" ValidationGroup="reg"  ></asp:CompareValidator>
</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><asp:ImageButton id="imgsubmit" runat="server" ImageUrl="~/images/submit.gif" AlternateText="Forgot Password" width="73" height="25" hspace="5" OnClick="imgsubmit_onclick" ValidationGroup="reg"/>
</td>
</tr>

</table>
<br />
<br />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
ShowSummary="False" ValidationGroup="reg" />
<br />
<br />
<br />
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

