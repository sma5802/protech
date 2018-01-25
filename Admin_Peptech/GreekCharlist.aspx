<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Peptech_GreekCharlist" Codebehind="GreekCharlist.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title><%=ConfigurationManager.AppSettings["title"].ToString()%></title>
     <link href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>css/apager.css" rel="stylesheet" type="text/css" />
    <link href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>css/astyle.default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function func(a)
    {
     var val=document.getElementById("a"); 
     document.getElementById("ctl00_ContentPlaceHolder1_txtCounties").focus();
     document.getElementById("ctl00_ContentPlaceHolder1_txtCounties").value=val;
    }
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataList ID="DataList1" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
        <ItemTemplate>
        <a onclick="javascript:func(<%#HttpUtility.HtmlDecode(Eval("Greekchar").ToString())%>);void(0);">
        <%#HttpUtility.HtmlDecode(Eval("Greekchar").ToString())%>
        </a>
        </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
