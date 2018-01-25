<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_ManageImage_ImageList" Codebehind="ImageList.aspx.cs" %>
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

<h2>Manage Image</h2>
<asp:Label ID="lblmessage" runat="server" CssClass="msg" Visible="False"></asp:Label>
<p class="tip"><img src="../../images/tip.gif" border="0" align="absmiddle"> Click on a Image to edit the Image name</p>
<div align="right"><a href="NoImageList.aspx"><strong>Show Products have no image</strong></a></div><br />
<asp:GridView ID="grdListProperty" AllowPaging="true" PageSize="50" Width="100%" CssClass="grid" DataSourceID="srcListProperty" OnRowDataBound="grdListProperty_OnRowBound" OnDataBound="grdListProperty_databound" DataKeyNames="id" AllowSorting="true"  AutoGenerateColumns="false" runat="server" CellPadding="6" EmptyDataText="No List Available" OnPageIndexChanging="grdListProperty_PageIndexChanging" OnRowCommand="grdListProperty_RowCommand">
<HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
<Columns>
<asp:TemplateField HeaderText="Category Name" SortExpression="categoryname">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("categoryname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Sub Category Name" SortExpression="subcategoryname">
<ItemTemplate>
<asp:HyperLink ID="hlSubcategory" runat="server" ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Product Name" SortExpression="productname">
<ItemTemplate>
<img id="imgProd" runat="server" src='<%#string.Format("../../ProductImage/{0}",Eval("ProductImage")) %>' alt='<%#HttpUtility.HtmlDecode(Eval("productname").ToString()) %>' width="100" />
<asp:HyperLink ID="lblcountiesname2" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("productname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Formula" SortExpression="formula">
<ItemTemplate>
<asp:Label ID="lblFormula" runat="server"></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Cata Log Name" SortExpression="catalogname">
<ItemTemplate>
<asp:Label ID="lblcatlog" runat="server"><%#Eval("catalogname")%></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Edit">
<ItemStyle  HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:HyperLink ID="hypedit" runat="server"  NavigateUrl='<%# string.Format("EditImage.aspx?id={0}", Eval("id"))%>' ImageUrl="../../images/edit.gif" BorderStyle="None"></asp:HyperLink>
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
<asp:ListItem Text="50" Value="50"></asp:ListItem>
<asp:ListItem Text="100" Value="100"></asp:ListItem>
<asp:ListItem Text="200" Value="200"></asp:ListItem>
<asp:ListItem Text="500" Value="500"></asp:ListItem>
<asp:ListItem Text="1000" Value="1000"></asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;
Records per page
</td>
</tr>
</table>
</PagerTemplate>
</asp:GridView>
<asp:SqlDataSource ID="srcListProperty" runat="server"
ConnectionString="<%$ connectionStrings:dbConnect %>"
SelectCommand="Select p.*,(select categoryname from pep$tech$corp.peptech_category where id=p.categoryid)categoryname,
(select subcategoryname from pep$tech$corp.peptech_subcategory where id=p.subcategoryid)subcategoryname,  
(select top 1 catalogname from pep$tech$corp.Peptech_Catalog where Productid=p.id)catalogname
from pep$tech$corp.Peptech_Product p ORDER BY categoryname,subcategoryname,productname asc"
DeleteCommand="delete pep$tech$corp.Peptech_Product where id=@id" >
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

