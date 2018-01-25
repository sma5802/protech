<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" CodeFile="CatalogListold.aspx.cs" Inherits="Admin_Peptech_ManageCatalog_CatalogList"%>
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
<h2>Manage Catalog</h2>
<asp:Label ID="lblmessage" runat="server" CssClass="msg" Visible="False"></asp:Label>
<p class="tip"><img src="../../images/tip.gif" border="0" align="absmiddle"> Click on a Catalog to edit the Catalog name</p>

<asp:Button ID="btnadd" Text="Add New" runat="server" CssClass="button" OnClick="btnadd_Click" />
<asp:Panel ID="Panel1" runat="server" GroupingText="Filter Record" CssClass="bluetext">
    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:DropDownList ID="ddlSubcategory" runat="server">
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Filter" OnClick="Button1_Click" CssClass="button" /></asp:Panel>

<asp:GridView ID="grdListProperty" Width="100%" CssClass="grid" OnRowDataBound="grdListProperty_OnRowBound" OnDataBound="grdListProperty_databound" DataKeyNames="id" AllowPaging="true" PageSize="50"  AutoGenerateColumns="false" runat="server" CellPadding="6" EmptyDataText="No List Available" OnPageIndexChanging="grdListProperty_PageIndexChanging" OnRowCommand="grdListProperty_RowCommand" OnRowDeleting="grdListProperty_RowDeleting" OnSorting="grdListProperty_Sorting">
    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
        NextPageText="Next" PreviousPageText="Previous" />

<Columns>
<asp:TemplateField >
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

<asp:TemplateField HeaderText="Category Name">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname100" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("categoryname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="SubCategory Name">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname101" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("subcategoryname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Product Name">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname10" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("productname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Catalog #">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname" runat="server" Text='<%#Eval("CatalogName") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Quantity">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname1" runat="server" Text='<%#Eval("Quantity") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Price($)">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname2" runat="server" Text='<%#Eval("Price") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Status">
<ItemTemplate>
<asp:Image runat="server" ID="ImageStatus" />
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Edit">
<ItemStyle  HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:HyperLink ID="hypedit" runat="server"  NavigateUrl='<%# "EditCatalog.aspx?id="+ Eval("id")%>' ImageUrl="../../images/edit.gif" BorderStyle="None"></asp:HyperLink>
</ItemTemplate>  
</asp:TemplateField>

<asp:TemplateField HeaderText="Action" ShowHeader="False">
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript: return confirm('Are you sure to delete this?')" CommandName="Delete" CommandArgument='<%#Eval("id") %>' CausesValidation="false">
<img alt="delete"  src="../../images/del.gif" border="0" /></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>
<RowStyle CssClass="gridcell" Height="20px" />
<PagerStyle  HorizontalAlign="Center" />
 <HeaderStyle  CssClass="gridhead" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
</asp:GridView>
<%--<asp:SqlDataSource ID="srcListProperty" runat="server"
ConnectionString="<%$ connectionStrings:dbConnect %>"
DeleteCommand="delete Peptech_catalog where id=@id" >
</asp:SqlDataSource>--%>
<table>
<tr>
<td align="left" style="width:50px; height: 23px;">
<asp:Button ID="active" runat="server" CausesValidation="false" Text="Activate" CommandArgument=""  OnClick="active_Click" CssClass="button" /></td>
<td align="left" style="height: 23px; width: 70px;">
<asp:Button ID="deactive" CausesValidation="false" runat="server" Text="Deactivate" OnClick="deactive_Click" CssClass="button" /></td>
<td align="left" style="height: 23px; width: 70px;">
<asp:Button ID="btnDelete" CausesValidation="false" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="button" OnClientClick="javascript: return confirm('Are you sure to delete this?')" /></td>
</tr>
</table>
</asp:Content>


