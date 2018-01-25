<%@ Page Language="C#" AutoEventWireup="true" Inherits="FileNotFound" Codebehind="error.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=ConfigurationManager.AppSettings["title"] %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
     <table cellspacing="0" cellpadding="0" border="0" width="780" style="font-size: 12px; color: rgb(0, 0, 0); line-height: 15px; font-family: verdana,Arial,Helvetica,sans-serif; text-decoration: none;">
    <tbody>
      <tr valign="top">
      <td align="center" valign="middle">
          <br />
          <br />
          <br />
          <img src="images/peptech-logo.gif" /></td>
        
      </tr>
        <tr valign="top">
            <td align="center" valign="middle"><strong class="text">
                <br />
                <br />
                Due to communications or connection problem <br />
        we can not process your request. </strong><br />          <strong>Please try again later.<br />
                <br />
                <br />
                </strong>&nbsp;<asp:ImageButton ID="btnRedirect" runat="server" 
                    ImageUrl="~/images/submit.gif" OnClick="btnRedirect_Click" />
        
        </td>            
        </tr>
    </tbody>
  </table>
    </div>
    </form>
</body>
</html>
