<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testimage.aspx.cs" Inherits="Admin_Peptech_ManageProduct_testimage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
function GenerateRow()
{
  var tbl=document.getElementById("tblImages");
  var lastRow = tbl.rows.length;
  var iteration = lastRow;
  var row = tbl.insertRow(lastRow);
  var cell1 = row.insertCell(0);
  var newdiv = document.createElement('Input');						
  newdiv.type="File";	
  newdiv.id="Images"+iteration;
  newdiv.name="Images"+iteration;
  newdiv.setAttribute('runat','server');
  cell1.appendChild(newdiv);
  document.getElementById("<%= hdImage.ClientID %>").value = lastRow;
}
function deleteinput()
{
var table= document.getElementById("tblImages");
var length=table.rows.length;
if(length>1)
{
table.deleteRow(length-1);
document.getElementById("<%= hdImage.ClientID %>").value = length-2;
}
}
</script>

</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
   <div>
<table width="100%" id="aheader"><tr>
<td><div id="logo">Peptech:Admin</div></td>
<td align="right">
<div style="font-size:11px;padding:3px;">

</div>
</td>
</tr></table>


<table border="0" cellspacing="0" cellpadding="0" width="100%" id="maintable">

<tr>

<td valign="top" id="sidestrip" width="10" align="center" onClick="javascript:toggleSidebar()">
<span id="togglemenu">&bull;<br>&bull;<br>&bull;<br>&bull;<br>&bull;<br>&bull;<br>&bull;<br>&bull;<br>&bull;<br>&bull;<br></span>
</td>


<td valign="top" width="130" id="sidebar" rowspan="2" >

<div class="menus">
<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="lblGeneral" runat="server" Text="General"></asp:Label></td></tr>

<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/Default.aspx" class="menulink">Admin Home</a></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ChangePassword.aspx" class="menulink">Admin Change Password</a></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ViewRequestInfo.aspx" class="menulink">View Catalog Request</a></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/RegisteredUser.aspx" class="menulink">View Registered User</a></td></tr>
<%--<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/GreekCharlist.aspx" class="menulink">View Greek Character</a></td></tr>
--%>
<tr><td class="menucell">
<asp:LinkButton   ID="lnkSignout" CssClass="menulink" runat="server" CausesValidation="false" OnClick="lnkSignout_Click" Width="93px">Signout</asp:LinkButton></td></tr>
</table>
<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label3" runat="server" Text="Manage Home"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageHome/HomeList.aspx" class="menulink">Manage Homelist</a></td></tr>
</table>

<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label5" runat="server" Text="Manage Category"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageCategory/CategoryList.aspx" class="menulink">Manage Category</a></td></tr>
</table>

<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label6" runat="server" Text="Manage Sub Category"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageSubcategory/subCategoryList.aspx" class="menulink">Manage Sub Category</a></td></tr>
</table>


<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label7" runat="server" Text="Manage Product"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageProduct/ProductList.aspx" class="menulink">Manage Product</a></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageProduct/BatchUpload.aspx?batch=1" class="menulink">Add Batch Product</a></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageImage/ImageList.aspx" class="menulink">Upload Product Image</a></td></tr>
</table>

<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label10" runat="server" Text="Manage Order"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/Order/OrderListing.aspx" class="menulink">Manage Order</a></td></tr>
</table>

<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label8" runat="server" Text="Manage Product Catalog"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageCatalog/CatalogList.aspx" class="menulink">Manage Product Catalog</a></td></tr>
</table>

<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label9" runat="server" Text="Manage Shipping Charge"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageShipping/ShippingList.aspx" class="menulink">Manage Shipping Charge</a></td></tr>
</table>
<%--<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label11" runat="server" Text="Manage Greek Character"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/GreekChar/Greekcharlist.aspx" class="menulink">Manage Greek Character</a></td></tr>
</table>--%>
<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label1" runat="server" Text="Manage Downloads"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/Downloads/DownloadList.aspx" class="menulink">Manage Downloads</a></td></tr>
</table>
<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label4" runat="server" Text="Manage Jobs"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageJobList/Joblist.aspx" class="menulink">Manage Jobs</a></td></tr>
</table>


<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label2" runat="server" Text="Manage News"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageNews/NewsList.aspx" class="menulink">Manage News</a></td></tr>
</table>

<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="Label11" runat="server" Text="Manage Mail Messages"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/ManageMail/Message.aspx" class="menulink">Manage Mail Message</a></td></tr>
</table>


<table border="0" cellspacing="1" cellpadding="2" class="menu" width="100%">
<tr><td class="menuhead"><asp:Label ID="lblManagePosts" runat="server" Text="Manage CMS"></asp:Label></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/CMS/CMSList.aspx" class="menulink">Manage CMS</a></td></tr>
<tr><td class="menucell"><a href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>Admin_Peptech/CMS/HomeList.aspx" class="menulink">Manage Home</a></td></tr>
</table>

</div>
<br/><br/>

<b>Server Time</b><br/><asp:Label ID="lblServerTime" runat="server" Text="Server Time"></asp:Label><br/><br/><br/>


</td>

<td valign="top" id="main">


<br /><br />
<div class="copy">
Copyright &copy; 2009. All Rights Reserved.<br />
</div><br /><br />
</td>
</tr>
</table>
</div>
<table class="box" width="100%" >
 <tr>
        <td align="left" valign="middle">
        </td>
        <td align="left" valign="middle">
        <table class="txt" Width="100%"><tbody ID="tblImages"><tr><td style="width: 25%"></td><td style="width: 25%"></td><td style="width: 25%"></td><td style="width: 25%"></td></tr></tbody></table><asp:HiddenField ID="hdImage" runat="server"  Value="0"/> <a onclick="javascript:GenerateRow();" href="Javascript:void(0);" class="bluelnk">Upload Product Image</a> | <a onclick="javascript:deleteinput();" href="Javascript:void(0);" class="bluelnk">Remove</a><br />
        </td>
    </tr>
   
    <tr>
<td ></td>
<td>
<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>
<%--<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>
--%></td>
</tr>
</table>
    </form>
</body>
</html>
