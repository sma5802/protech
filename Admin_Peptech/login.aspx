<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Peptech_login" Codebehind="login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=ConfigurationManager.AppSettings["title"].ToString()%></title>
    <link href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>css/apager.css" rel="stylesheet" type="text/css" />
<link href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>css/astyle.default.css" rel="stylesheet" type="text/css" />
</head>

<body id="loginpage">
<br><br><br><br><br><br><br><br><br>


<table align="center"><tr><td align="center">
<asp:Label ID="lblErrorMsg" runat="server" CssClass="msg"></asp:Label><br />
<div id="logo">Peptech : Admin</div><br>
<form runat="server" id="loginform">
<b>Enter User name</b><br />
<asp:TextBox ID="txtUsrName" runat="server" MaxLength="50" Width="220" ValidationGroup="login" AutoCompleteType="Disabled" EnableViewState="False"></asp:TextBox> 
<asp:RequiredFieldValidator ID="requsername" SetFocusOnError="true" runat="server" ErrorMessage="Enter your User Name" ControlToValidate="txtUsrName" Display="None" ValidationGroup="login"/> <br /><br />
<b>Enter Password</b> <br />
<asp:TextBox ID="txtPwd" TextMode="Password" runat="server" MaxLength="50" Width="220" ValidationGroup="login"></asp:TextBox> 
<br><br>
<asp:RequiredFieldValidator ID="reqpassword" SetFocusOnError="true" runat="server" ErrorMessage="Enter your password" ControlToValidate="txtPwd" Display="None" ValidationGroup="login"/>
<asp:Button ID="imgSubmit"  runat="server" OnClick="imgSubmit_Click1" Text="Login" CssClass="button" ValidationGroup="login"  />

<asp:SqlDataSource ID="srcaccount" runat="server"
ConnectionString="<%$ connectionStrings:dbConnect %>"
SelectCommand="select id, username,password  from Peptech_admin where username=@username and password=@password">
<SelectParameters>
<asp:Parameter Name="username" /> 
<asp:Parameter Name="password" />
</SelectParameters> 
</asp:SqlDataSource> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="login" />
</form>

<br><br>
Copyright &copy; 2009 Peptech.com,Inc  All Rights Reserved.<br>
 
<br><br>



</td></tr></table>
</body></html>
