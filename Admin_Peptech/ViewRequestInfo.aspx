<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" Debug="true" AutoEventWireup="true" Inherits="Admin_Peptech_ViewRequestInfo" Codebehind="ViewRequestInfo.aspx.cs" %>
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

<h2>Manage Catalog Requests</h2>
<asp:Label ID="lblmessage" runat="server" CssClass="msg" Visible="False"></asp:Label>
<p class="tip"><img src="../../images/tip.gif" border="0" align="absmiddle"> Click on a Request Info to View/Edit</p>
<%--<asp:Button ID="btnadd" Text="Add New" runat="server" CssClass="button" OnClick="btnadd_Click" />--%>


<asp:GridView ID="grdcms" Width="100%" CssClass="grid" DataKeyNames="id" 
        AllowPaging="True" PageSize="15"  AutoGenerateColumns="False" 
        EmptyDataText="No List available" runat="server" CellPadding="6" 
        OnRowCommand="grdcms_RowCommand" AllowSorting="True" 
        DataSourceID="srccms" >
 <HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
<Columns>
<%--<asp:TemplateField >
<HeaderTemplate>
<asp:CheckBox ID="chk_checkall" Text="All" runat="server" Checked="false" onclick="SelectAll(this);"/>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="statusCheck" runat="server" />
<asp:HiddenField ID="checkid" runat="server" Value='<%# Eval("id") %>' />
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>--%>

<asp:TemplateField HeaderText="Name" SortExpression="name">
<ItemTemplate>
<asp:HyperLink ID="lblname" runat="server" Text='<%#Eval("name") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Email" SortExpression="email">
<ItemTemplate>
<asp:HyperLink ID="lblemail" runat="server" Text='<%#Eval("email") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Company" SortExpression="Company">
<ItemTemplate>
<asp:HyperLink ID="lblPhone" runat="server" Text='<%#Eval("Company") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Enquiry Type" SortExpression="enquirytype">
<ItemTemplate>
<asp:HyperLink ID="lblEnquiry" runat="server" Text='<%#Eval("enquirytype") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="City" SortExpression="city">
<ItemTemplate>
<asp:HyperLink ID="lblservicewanted" runat="server" Text='<%#Eval("city") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="State" SortExpression="statename">
<ItemTemplate>
<asp:HyperLink ID="lbldaytime" runat="server" Text='<%#Eval("statename") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Country" SortExpression="Countryname">
<ItemTemplate>
<asp:HyperLink ID="lbldaytime1" runat="server" Text='<%#Eval("Countryname") %>'  ></asp:HyperLink>
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

</Columns>
<PagerTemplate>
<table>
<tr>
<td align="right">
<asp:LinkButton ID="lnkprev" runat="server" CausesValidation="false"  CommandName="page" CommandArgument="Prev" Text="Previous"/>
<asp:LinkButton ID="lnknext" runat="server" CausesValidation="false" CommandName="page" CssClass="text" CommandArgument="Next" Text="Next"/>
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
</asp:DropDownList>&nbsp;&nbsp;
Records per page
</td>
</tr>
</table>
</PagerTemplate>
</asp:GridView>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td colspan="2" align="center">
<br />
<%-- Content Editor --%>
<asp:Label ID="lblmsg" runat="server" ForeColor="red" Font-Bold="true" Visible="false" EnableViewState="false"></asp:Label>
<br />
<asp:FormView ID="frmdetail" runat="server" DataSourceID="srccmsdetail" 
        Width="100%" DataKeyNames="id">
<ItemTemplate>
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle">
<asp:Panel ID="Panel1" runat="server" GroupingText='<%# "Catalog Request Information of " + Eval("name") %>'>   
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
<asp:Label ID="Label1" runat="server" Text="EMail:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label2" runat="server" Text='<%#Eval("email")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label3" runat="server" Text="Company:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label4" runat="server" Text='<%#Eval("Company")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label5" runat="server" Text="Telephone:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label6" runat="server" Text='<%#Eval("Telephone")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label7" runat="server" Text="Fax:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label8" runat="server" Text='<%#Eval("Fax")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label9" runat="server" Text="street1:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label10" runat="server" Text='<%#Eval("street1")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label13" runat="server" Text="street2:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label14" runat="server" Text='<%#Eval("street2")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label15" runat="server" Text="City:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label16" runat="server" Text='<%#Eval("City")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label17" runat="server" Text="state:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label18" runat="server" Text='<%#Eval("statename")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label19" runat="server" Text="Other:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label20" runat="server" Text='<%#Eval("Other")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label21" runat="server" Text="Zip:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label22" runat="server" Text='<%#Eval("Zip")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label23" runat="server" Text="Country:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label24" runat="server" Text='<%#Eval("Countryname")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label11" runat="server" Text="Enquiry Type:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label12" runat="server" Text='<%#Eval("enquirytype")%>'></asp:Label>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label25" runat="server" Text="Comments:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">

    <asp:Label ID="Label26" runat="server" Text='<%#Eval("Comments")%>'></asp:Label>
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
<br />
<br />

<asp:SqlDataSource ID="srccms" runat="server"
ConnectionString="<%$ ConnectionStrings:dbConnect %>"

        SelectCommand="select (ri.fname+' '+ri.Lname) as name,ri.*,(select country from peptech_country where id=ri.country)countryname,isnull((select state from peptech_state where id=ri.state),other)statename from Peptech_RequestInfo ri where Enquirytype='Catalog Request' order by posteddate desc">
</asp:SqlDataSource>

<asp:SqlDataSource ID="srccmsdetail" runat="server"
ConnectionString="<%$ ConnectionStrings:dbConnect %>"

        SelectCommand="select (ri.fname+' '+ri.Lname) as name,ri.*,(select country from peptech_country where id=ri.country)countryname,isnull((select state from peptech_state where id=ri.state),other)statename from Peptech_RequestInfo ri where id=@id">
<SelectParameters>
<asp:ControlParameter Name="id" ControlID="grdcms" PropertyName="Selectedvalue" />
</SelectParameters>

</asp:SqlDataSource>

</td>
</tr>

</table>

<table>
<tr>
<td align="left" style="width:50px; height: 23px;">
<%--<asp:Button ID="active" runat="server" CausesValidation="false" Text="Activate" CommandArgument=""  OnClick="active_Click" CssClass="button" /></td>
--%><td align="left" style="height: 23px; width: 70px;">
<%--<asp:Button ID="deactive" CausesValidation="false" runat="server" Text="Deactivate" OnClick="deactive_Click" CssClass="button" /></td>
--%>
</tr>
</table>
</asp:Content>

