<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="True" Inherits="Admin_Peptech_ManageProduct_ProductList" Codebehind="ProductList.aspx.cs" %>
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

<h2>Manage Products</h2>
<asp:Label ID="lblmessage" runat="server" CssClass="msg" Visible="False"></asp:Label>
<p class="tip"><img src="../../images/tip.gif" border="0" align="absmiddle"> Click on a Products to edit the Products name</p>
<asp:Panel ID="Panel1" runat="server" GroupingText="Filter Record" CssClass="bluetext">

<asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
<ContentTemplate >
<asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
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
    <asp:Button ID="Button1" runat="server" Text="Filter" OnClick="Button1_Click" CssClass="button" />

    </asp:Panel>
<asp:Button ID="btnadd" Text="Add New" runat="server" CssClass="button" OnClick="btnadd_Click" />

<asp:GridView ID="grdListProperty" Width="100%" CssClass="grid" AllowSorting="True" 
        OnRowDataBound="grdListProperty_OnRowBound" 
        OnDataBound="grdListProperty_databound" DataKeyNames="id" AllowPaging="True" 
        PageSize="50"  AutoGenerateColumns="False" runat="server" CellPadding="6" 
        EmptyDataText="No List Available" 
        OnPageIndexChanging="grdListProperty_PageIndexChanging" 
        OnRowCommand="grdListProperty_RowCommand" 
        OnSorting="grdListProperty_Sorting" onrowdeleting="grdListProperty_RowDeleting">
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

 <asp:TemplateField HeaderText="Sort">
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="text">
                        <tr>
                            <td align="center">
                                <asp:LinkButton ID="lnkUp" runat="server" ToolTip="Up"
                                    CommandArgument='<%#Eval("ID") %>' CommandName="upsort"><img 
                                    alt="Up" border="0" height="7" src="../../images/ascending.gif" vspace="1" width="7" /></asp:LinkButton>
                            </td>
                            <td align="center">
                                <asp:LinkButton ID="lnkDown" runat="server" ToolTip="Down"
                                    CommandArgument='<%#Eval("ID") %>' CommandName="downsort"><img 
                                    alt="Down" border="0" height="7" src="../../images/descending.gif" vspace="1" 
                                    width="7" /></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="40px" />
            </asp:TemplateField>
            

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
<asp:HyperLink ID="lblcountiesname2" runat="server" Text='<%#HttpUtility.HtmlDecode(Eval("productname").ToString()) %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Catalog #" SortExpression="catalog" Visible="False">
<ItemTemplate>
<asp:HyperLink ID="hlcatalog" runat="server" ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="CAS" SortExpression="CAS">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname3" runat="server" Text='<%#Eval("CAS") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Formula" SortExpression="formula">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname4" runat="server"   ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Molecular Weight" SortExpression="MWeight">
<ItemTemplate>
<asp:HyperLink ID="lblcountiesname5" runat="server" ></asp:HyperLink>
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
<asp:HyperLink ID="hypedit" runat="server"  NavigateUrl='<%# "EditProduct.aspx?id="+ Eval("id")%>' ImageUrl="../../images/edit.gif" BorderStyle="None"></asp:HyperLink>
</ItemTemplate>  
</asp:TemplateField>

<asp:TemplateField HeaderText="Action" ShowHeader="False">
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("id") %>' OnClientClick="javascript: return confirm('Are you sure to delete this?')" CommandName="Delete" CausesValidation="false">
<img alt="delete"  src="../../images/del.gif" border="0" /></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>
<RowStyle CssClass="gridcell" Height="20px" />
<PagerStyle  HorizontalAlign="Center" />
<HeaderStyle  CssClass="gridhead" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
</asp:GridView>
<%--<asp:SqlDataSource ID="srcListProperty" runat="server" ConnectionString="<%$ connectionStrings:dbConnect %>"
DeleteCommand="delete Peptech_Product where id=@id">
<SelectParameters>
<asp:Parameter Name="id" />
</SelectParameters>
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


