<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" ValidateRequest="false" Inherits="Admin_Peptech_CMS_HomeList" Codebehind="HomeList.aspx.cs" %>
<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
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

<h2>Manage Home</h2>
<asp:Label ID="lblmessage" runat="server" CssClass="msg" Visible="False"></asp:Label>
<p class="tip"><img src="../../images/tip.gif" border="0" align="absmiddle"> Click on the Home Text to View/Edit.</p>

<%--<asp:Button ID="btnadd" Text="Add New" runat="server" CssClass="button" OnClick="btnadd_Click" />--%>



<asp:GridView ID="grdcms" Width="100%" CssClass="grid" DataSourceID="srccms" DataKeyNames="id" AllowPaging="true" PageSize="30"  AutoGenerateColumns="false" EmptyDataText="No List available" runat="server" CellPadding="6" >
 <HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
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

<asp:TemplateField HeaderText="PageName">
<ItemTemplate>
<asp:HyperLink ID="lblPagename" runat="server" Text='<%#Eval("pagename") %>'  ></asp:HyperLink>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:CommandField ShowSelectButton="true" SelectImageUrl="../../images/edit.gif" ButtonType="Image" HeaderText="View/Edit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
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
<asp:FormView ID="frmdetail" runat="server" DataSourceID="srccmsdetail" DefaultMode="Edit" Width="100%" DataKeyNames="id">
<EditItemTemplate>
<hr />
<strong><center>Edit Content Pages(<%#Eval("pagename")%>)</center></strong>
<br />
<fckeditorv2:fckeditor runat="server" id="FCKeditor1" Value='<%# Bind("pagedata") %>'  SkinPath="skins/office2003/" BasePath="~/FCKeditor/" height="350px"></fckeditorv2:fckeditor>
 <br />
 <asp:LinkButton id="lnkUpdate" Text="Update Contents" CommandName="Update" Runat="server" />  
 |      
 <asp:LinkButton id="lnkCancel" Text="Cancel Update" CommandName="Cancel" Runat="server" />   
</EditItemTemplate>
</asp:FormView>
<br />
<br />

<asp:SqlDataSource ID="srccms" runat="server"
ConnectionString="<%$ connectionStrings:dbConnect %>"
SelectCommand="select id,pagename from Peptech_edit where home=1">
</asp:SqlDataSource>

<asp:SqlDataSource ID="srccmsdetail" runat="server"
ConnectionString="<%$ connectionStrings:dbConnect %>"
SelectCommand="select id,pagename,pagedata from Peptech_edit where id=@id"
UpdateCommand="update Peptech_edit set pagedata=@pagedata where id=@id"
OnUpdated="srccmsdetail_updated">
<SelectParameters>
<asp:ControlParameter Name="id" ControlID="grdcms" PropertyName="Selectedvalue" />
</SelectParameters>

<UpdateParameters>
<asp:ControlParameter Name="id" ControlID="grdcms" PropertyName="Selectedvalue" />
</UpdateParameters>
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

