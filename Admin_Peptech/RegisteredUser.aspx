<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_RegisteredUser" Codebehind="RegisteredUser.aspx.cs" %>
<%@ Import Namespace="UserClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function SelectAll(obj)
{
var CheckValue =  obj.checked;

var objCheckBoxes = document.getElementsByTagName("input");
if(!objCheckBoxes)
return;
var countCheckBoxes = objCheckBoxes.length;

if(countCheckBoxes == 1)
{

if (objCheckBoxes.name.indexOf("grdListProperty")>=0 && objCheckBoxes.name.indexOf("statusCheck")>=0)
objCheckBoxes.checked = CheckValue;
}
else
{
// set the check value for all check boxes
for(var i = 0; i < countCheckBoxes; i++)
if (objCheckBoxes[i].type == "checkbox")
if (objCheckBoxes[i].name.indexOf("grdListProperty")>=0 && objCheckBoxes[i].name.indexOf("statusCheck")>=0)
objCheckBoxes[i].checked = CheckValue;
}
}
</script>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="box">
<tr>
<td align="left" >
<h2>List Of Registered User</h2>
<asp:Label ID="lblmessage" runat="server" CssClass="msg" Visible="False"></asp:Label>
<p class="tip"><%--<img src="../../images/tip.gif" border="0" align="absmiddle"> Click on a counties to edit the counties name--%></p>

<%--<asp:Button ID="btnadd" Text="Add New" runat="server" CssClass="button" OnClick="btnadd_Click" />
--%>
<asp:GridView ID="grdListProperty" Width="100%" CssClass="grid" 
        DataSourceID="srcListProperty" OnRowDataBound="grdListProperty_OnRowBound" 
        OnDataBound="grdListProperty_databound" DataKeyNames="id" AllowPaging="True" 
        PageSize="15"  AutoGenerateColumns="False" runat="server" CellPadding="6" 
        EmptyDataText="No List Available" 
        OnPageIndexChanging="grdListProperty_PageIndexChanging" 
        OnRowCommand="grdListProperty_RowCommand" AllowSorting="True">
 <HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
<Columns>

<%--<asp:TemplateField HeaderText="Action" ShowHeader="False">
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript: return confirm('Are you sure to delete this?')" CommandName="Delete" CausesValidation="false">
<img alt="delete"  src="../../images/del.gif" border="0" /></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>--%><asp:TemplateField >
<HeaderTemplate>
<asp:CheckBox ID="chk_checkall" Enabled="false" Visible="false" Text="All" runat="server" Checked="false" onclick="SelectAll(this);"/>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="statusCheck" runat="server" />
<asp:HiddenField ID="checkid" runat="server" Value='<%# Eval("id") %>' />
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Name" SortExpression="name">
<ItemTemplate>
<asp:Label ID="lblname" runat="server" Text='<%#Eval("name") %>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>



<asp:TemplateField HeaderText="Email" SortExpression="Email">
<ItemTemplate>
<asp:Label ID="lblname5" runat="server" Text='<%#Eval("Email") %>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
    <asp:BoundField DataField="company" HeaderText="Company" 
        SortExpression="company" />

<asp:TemplateField HeaderText="Phone" SortExpression="Phone">
<ItemTemplate>
<asp:Label ID="lblname2" runat="server" Text='<%#Eval("Phone") %>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>


<asp:TemplateField HeaderText="City" SortExpression="BilCity">
<ItemTemplate>
<asp:Label ID="lblname4" runat="server" Text='<%#Eval("BilCity") %>'  ></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:CommandField ShowSelectButton="True" SelectImageUrl="~/images/edit.gif" ButtonType="Image" HeaderText="View">
    <HeaderStyle HorizontalAlign="Left" />
    <ItemStyle HorizontalAlign="Left" />
</asp:CommandField>


<asp:TemplateField HeaderText="Action" ShowHeader="False">
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript: return confirm('Are you sure to delete this?')" CommandName="del" CommandArgument='<%#Eval("id") %>' CausesValidation="false">
<img alt="delete"  src="../images/del.gif" border="0" /></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>

<%--<asp:TemplateField HeaderText="Status">
<ItemTemplate>
<img src="<%#string.Format("../images/{0}",Eval("status").ToString().ToLower().Replace("true","status-on.gif").Replace("false","status-off.gif")) %>" title="<%#string.Format("Profile {0}",Eval("status").ToString().ToLower().Replace("true","Active").Replace("false","Deactive")) %>" />
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>--%>


<asp:TemplateField HeaderText="PO Status">
<ItemTemplate>
<img src="<%#string.Format("../images/{0}",Eval("PurchaseOrderNo").ToString().ToLower().Replace("true","status-on.gif").Replace("false","status-off.gif")) %>" title="<%#string.Format("PO {0}",Eval("PurchaseOrderNo").ToString().ToLower().Replace("true","Active").Replace("false","Deactive")) %>" />
<%--<asp:Label ID="lblname14" runat="server" Text='<%#Eval("PurchaseOrderNo") %>'  ></asp:Label>--%>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>




</Columns>
<PagerTemplate>
<table>
<tr>
<td align="right">
<asp:LinkButton ID="lnkprev" runat="server" CausesValidation="false"  CommandName="page" CommandArgument="Prev" Text="Previous"/>
<asp:LinkButton ID="lnknext" runat="server" CausesValidation="false" CommandName="page" CommandArgument="Next" Text="Next"/>
</td>
<td width="10"></td>
<td>
<asp:Label ID="lblpage" runat="server" Font-Bold="true" ></asp:Label>
</td>
<td width="10"></td>
<td><font >
Show&nbsp;&nbsp;
<asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_selected">
<asp:ListItem Text="5" Value="5"></asp:ListItem>
<asp:ListItem Text="10" Value="10"></asp:ListItem>
<asp:ListItem Text="15" Value="15"></asp:ListItem>
<asp:ListItem Text="20" Value="20"></asp:ListItem>
<asp:ListItem Text="50" Value="50"></asp:ListItem>
<asp:ListItem Text="100" Value="100"></asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;
Records per page
</td>
</tr>
</table>
</PagerTemplate>
</asp:GridView>

<br />
<%--<asp:Button ID="active" runat="server" CausesValidation="false" Text="Activate Status" CommandArgument=""  OnClick="active_Click" CssClass="button" />
<&nbsp;
asp:Button ID="deactive" CausesValidation="false" runat="server" Text="Deactivate Status" OnClick="deactive_Click" CssClass="button" />
&nbsp;--%>
<asp:Button ID="btnAPO" CausesValidation="false" runat="server" 
        Text="Activate Purchase Order" CssClass="button" onclick="btnAPO_Click" />
&nbsp;
<asp:Button ID="btnDPO" CausesValidation="false" runat="server" 
        Text="Deactivate Purchase Order" CssClass="button" onclick="btnDPO_Click" />
</td>
</tr>
</table>
<br />
<asp:FormView ID="frmdetail" runat="server" DataSourceID="srccmsdetail" DefaultMode="ReadOnly" Width="100%" DataKeyNames="id">
<ItemTemplate>
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle" >
<asp:Panel ID="Panel1" runat="server" GroupingText='<%#Eval("name")+" Registered User"%>'>
<table width="100%" >
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="lblCounties" runat="server" Text="Name:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="lblname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label9" runat="server" Text="username:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label10" runat="server" Text='<%#Eval("username")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label11" runat="server" Text="Password:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label12" runat="server" Text='<%#customUtility.DecryptData(Eval("Password").ToString())%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label19" runat="server" Text="Email:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label20" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label17" runat="server" Text="Company:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label18" runat="server" Text='<%#Eval("Company")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label21" runat="server" Text="Phone:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label22" runat="server" Text='<%#Eval("Phone")%>'></asp:Label>
</td>
</tr>


<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label23" runat="server" Text="Fax:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label24" runat="server" Text='<%#Eval("Fax")%>'></asp:Label>
</td>
</tr>

<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label1" runat="server" Text="Street1:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label2" runat="server" Text='<%#Eval("BilStreet1")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label13" runat="server" Text="Street2:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label14" runat="server" Text='<%#Eval("BilStreet2")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label7" runat="server" Text="City:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label8" runat="server" Text='<%#Eval("BilCity")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label15" runat="server" Text="State:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label16" runat="server" Text='<%#Eval("statename")%>'></asp:Label>
</td>
</tr>
<%--<tr >
<td align="left" valign="middle"  >
<asp:Label ID="Label17" runat="server" Text="Other:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label18" runat="server" Text='<%#Eval("Bilother")%>'></asp:Label>
</td>
</tr>
<tr>--%>
<td align="left" valign="middle"  >
<asp:Label ID="Label3" runat="server" Text="Zip/Postal Code:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label4" runat="server" Text='<%#Eval("BilZip")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label5" runat="server" Text="Country:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label6" runat="server" Text='<%#Eval("countryname")%>'></asp:Label>
</td>
</tr>
<tr>
<td ></td>
<td>
<%--<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>
<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>--%>
</td>
</tr>
</table>
  </asp:Panel>
</td>
</tr>
</table>
</ItemTemplate>
</asp:FormView>


<asp:SqlDataSource ID="srcListProperty" runat="server"
ConnectionString="<%$ ConnectionStrings:dbConnect %>"
SelectCommand="Select ml.*,(ml.fname+' '+ml.Lname)name,(select country from pep$tech$corp.peptech_country where id=ml.Bilcountry)countryname,(select state from pep$tech$corp.peptech_state where id=ml.Bilstate)statename  from pep$tech$corp.peptech_Memberlist ml ORDER BY id desc"
 >
</asp:SqlDataSource>


<asp:SqlDataSource ID="srccmsdetail" runat="server"
ConnectionString="<%$ connectionStrings:dbConnect %>"
SelectCommand="select ml.*,(ml.fname+' '+ml.Lname)name,(select country from pep$tech$corp.peptech_country where id=ml.Bilcountry)countryname,isnull((select state from pep$tech$corp.peptech_state where id=ml.Bilstate),bilother)statename from pep$tech$corp.peptech_Memberlist ml where ml.id=@id">
<SelectParameters>
<asp:ControlParameter Name="id" ControlID="grdListProperty" PropertyName="Selectedvalue" />
</SelectParameters>

<%--<UpdateParameters>
<asp:ControlParameter Name="id" ControlID="srcListProperty" PropertyName="Selectedvalue" />
</UpdateParameters>--%>
</asp:SqlDataSource>
<table>
<%--<tr>
<td align="left" style="width:50px; height: 23px;">
<asp:Button ID="active" runat="server" CausesValidation="false" Text="Activate" CommandArgument=""  OnClick="active_Click" CssClass="button" /></td>
<td align="left" style="height: 23px; width: 70px;">
<asp:Button ID="deactive" CausesValidation="false" runat="server" Text="Deactivate" OnClick="deactive_Click" CssClass="button" /></td>
</tr>
--%></table>
</asp:Content>


