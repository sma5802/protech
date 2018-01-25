<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_ManageHome_HomeList" Codebehind="HomeList.aspx.cs" %>
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

<h2>Manage Home Page</h2>
<asp:Label ID="lblmessage" runat="server" CssClass="msg" Visible="False"></asp:Label>
<p class="tip"><img src="../../images/tip.gif" border="0" align="absmiddle"> Click on a Title to edit the Title Detail</p>

<asp:Button ID="btnadd" Text="Add New" runat="server" Visible="false" CssClass="button" OnClick="btnadd_Click" />

<asp:GridView ID="grdListProperty" Width="100%" CssClass="grid" DataSourceID="srcListProperty" OnRowDataBound="grdListProperty_OnRowBound" OnDataBound="grdListProperty_databound" DataKeyNames="id" AllowPaging="true" PageSize="15"  AutoGenerateColumns="false" runat="server" CellPadding="6" EmptyDataText="No List Available" OnPageIndexChanging="grdListProperty_PageIndexChanging" OnRowCommand="grdListProperty_RowCommand">
 <HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
<Columns>
<asp:TemplateField Visible="false">
<HeaderTemplate>
<asp:CheckBox ID="chk_checkall" Text="All" runat="server" Checked="false" onclick="SelectAll(this);"/>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="statusCheck" runat="server" />
<asp:HiddenField ID="checkid" runat="server" Value='<%# Eval("id") %>' />
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<%--<asp:TemplateField HeaderText="Posting Date">
<ItemTemplate>
<asp:Label ID="lblPostdate" runat="server" Text='<%#Eval("postdate","{0:d}") %>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>--%>
<asp:TemplateField HeaderText="Title">
<ItemTemplate>
<asp:Label ID="lblHeadlines" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Edit">
<ItemStyle  HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:HyperLink ID="hypedit" runat="server"  NavigateUrl='<%#"EditHomeList.aspx?id="+ Eval("id")%>' ImageUrl="../../images/edit.gif" BorderStyle="None"    ></asp:HyperLink>
</ItemTemplate>  
</asp:TemplateField>

<asp:TemplateField HeaderText="Action" ShowHeader="False">
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript: return confirm('Are you sure to delete this?')" CommandName="Delete" CausesValidation="false">
<img alt="delete"  src="../../images/del.gif" border="0" /></asp:LinkButton>
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
<asp:SqlDataSource ID="srcListProperty" runat="server"
ConnectionString="<%$ connectionStrings:dbConnect %>"
SelectCommand="Select *  from Peptech_Homelist ORDER BY id asc"
DeleteCommand="delete Peptech_Homelist where id=@id" >
</asp:SqlDataSource>
<table>
<%--<tr>
<td align="left" style="width:50px; height: 23px;">
<asp:Button ID="active" runat="server" CausesValidation="false" Text="Activate" CommandArgument=""  OnClick="active_Click" CssClass="button" /></td>
<td align="left" style="height: 23px; width: 70px;">
<asp:Button ID="deactive" CausesValidation="false" runat="server" Text="Deactivate" OnClick="deactive_Click" CssClass="button" /></td>
</tr>--%>
</table>
</asp:Content>


