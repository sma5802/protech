<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" EnableViewState="true" EnableEventValidation="true" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" Inherits="Admin_Peptech_ManageCatalog_CatalogList" Codebehind="CatalogList.aspx.cs" %>
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
<asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
<ContentTemplate >
<asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
<asp:ListItem Value="0" Text="Select category"></asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:DropDownList ID="ddlSubcategory" runat="server" 
        AppendDataBoundItems="True">
        <asp:ListItem Value="0">Select Subcategory</asp:ListItem>
    </asp:DropDownList>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div style="position:absolute; z-index:1; left: 40%;top: 40%;">
<asp:UpdateProgress id="updateprg" runat="server" >
<progresstemplate>
<img align="left" src="../../images/loadingimg.gif" alt=""  />
</progresstemplate>
</asp:UpdateProgress>
</div>
    
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Filter" OnClick="Button1_Click" CssClass="button" /></asp:Panel>

<asp:GridView ID="grdListProperty" Width="100%"  CssClass="grid" AllowSorting="true" OnRowDataBound="grdListProperty_OnRowBound"  AllowPaging="true" PageSize="50"  AutoGenerateColumns="false" EmptyDataText="No List available" runat="server" CellPadding="6" OnPageIndexChanging="grdListProperty_PageIndexChanging" EnableViewState="true" OnRowDeleting="grdListProperty_RowDeleting" OnSorting="grdListProperty_Sorting">
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

<asp:TemplateField HeaderText="Category Name" SortExpression="categoryname">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname100" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("categoryname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="SubCategory Name" SortExpression="subcategoryname">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname101" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("subcategoryname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Product Name" SortExpression="productname">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname10" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("productname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Catalog #" SortExpression="CatalogName">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname" runat="server" Text='<%#Eval("CatalogName") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Size" SortExpression="Quantity">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname1" runat="server" Text='<%#Eval("Quantity") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Price($)" SortExpression="Price">
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
<asp:LinkButton ID="LinkButton1" runat="server" 
        OnClientClick="javascript: return confirm('Are you sure to delete this?')" 
        CommandName="Delete" CommandArgument='<%#Eval("id") %>' 
        CausesValidation="false" oncommand="LinkButton1_Command">
<img alt="delete"  src="../../images/del.gif" border="0" /></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>
<RowStyle CssClass="gridcell" Height="20px" />
<PagerStyle  HorizontalAlign="Center" />
 <HeaderStyle  CssClass="gridhead" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
</asp:GridView>
   <%-- <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" bgcolor="white">
        <tr>
            <td style="width: 33%" align="right">
            <asp:Label ID="lblpage" runat="server" Font-Bold="true" ></asp:Label>
            </td>
            <td style="width: 33%">
            Show&nbsp;&nbsp;
<asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_selected">
<asp:ListItem Text="15" Value="15"></asp:ListItem>
<asp:ListItem Text="25" Value="25"></asp:ListItem>
<asp:ListItem Text="50" Value="50"></asp:ListItem>
<asp:ListItem Text="100" Value="100"></asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;
Records per page
            </td>
        </tr>
    </table>--%>

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

