<%@ Page Language="C#" MasterPageFile="~/Content.master" ValidateRequest="false" AutoEventWireup="true" Inherits="ProductSearch" Title="Untitled Page" Codebehind="ProductSearch.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top">
<div align="justify">
<br />
<span class="blueheading">Products Search</span><br /><br />
<strong class="text">You can search by Catalog Number, Product Name, Molecular Formula or CAS Number. </strong>
<br />
<br />
<%--<div align="left">
<asp:HyperLink ID="hlhome" runat="server" CssClass="bluelink" Font-Bold="true" NavigateUrl="~/default.aspx" Text="Home"></asp:HyperLink>&nbsp;<b class="bluetext">>></b>&nbsp;<asp:HyperLink
ID="hlproduct" runat="server" CssClass="bluelink" Font-Bold="true" NavigateUrl="~/categories.aspx" Text="Product"></asp:HyperLink>&nbsp;<b id="a2" runat="server" class="bluetext">>></b>&nbsp;<asp:HyperLink ID="hlcat" Font-Bold="true" CssClass="bluelink" runat="server" ></asp:HyperLink>&nbsp;<b id="a1" runat="server" class="bluetext">>></b>&nbsp;<asp:Label ID="lblSubCat" Font-Bold="true" CssClass="bluetext" runat="server" ></asp:Label>
</div>--%>
<table width="100%"  border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1" class="text">
<tr>
<td> <asp:Panel ID="pnlsearch1" runat="server" DefaultButton="imgsearchgo1">
<table cellpadding="3" cellspacing="0">
<tr>
<td><strong>Search :</strong>&nbsp;</td>
<td><asp:TextBox ID="txtsearch1" runat="server" Width="179px" ValidationGroup="sea"></asp:TextBox></td>
<td align="left" valign="bottom">
<asp:ImageButton ID="imgsearchgo1" runat="server" ImageUrl="~/images/GO.jpg" OnClick="imgsearchgo_Click" ValidationGroup="sea"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
ErrorMessage="Please enter a valid search string." SetFocusOnError="True" ValidationGroup="sea" ControlToValidate="txtsearch1"></asp:RequiredFieldValidator>
</td>
<td width="20"></td>
<td>
<strong>Search by Structure or Substructure:</strong>&nbsp;
</td>
<td><asp:ImageButton ID="imgstrcuture" runat="server" ImageUrl="~/images/GO.jpg" OnClick="imgstrcuture_Click"/>
</td>
</tr>
</table></asp:Panel>
</td>
</tr>
<%--<tr>
<td>
<iframe align="top" src ="http://newsearch.chemexper.com/search/structure.shtml?forGroupNames=peptech&target=entry&format=peptech&options=structure" frameborder="0" width="100%" height="500px">
</iframe>
</td>
</tr>--%>
<%--<tr>
<td align="left" valign="top" bgcolor="#F9F8F8" width="100%">
<%if (Request.QueryString["query"] != null && Request.QueryString["query"].ToString() != "")
  {
%>
<iframe src='http://newsearch.chemexper.com/misc/hosted/peptech/center.shtml?query=<%=Request.QueryString["query"].ToString() %>' frameborder="0" width="100%" height="500px">
</iframe>
<%}
  else
  { %>
<iframe src='http://newsearch.chemexper.com/misc/hosted/peptech/center.shtml?query=acid' frameborder="0" width="100%" height="800">
</iframe>
<%} %>
</td>
</tr>--%>
<tr>
<td>
    <asp:GridView ID="grdSearchProducts" BackColor="#E1E1E1" BorderStyle="None" 
        BorderWidth="0px" AllowPaging="True" AllowSorting="True" 
    PageSize="50" Width="100%" EmptyDataText="No Result present for your Search" 
        CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" 
        DataSourceID="sqlSearchProducts" runat="server" 
        onpageindexchanging="grdSearchProducts_PageIndexChanging" 
        ondatabound="grdSearchProducts_DataBound" 
        onrowdatabound="grdSearchProducts_RowDataBound" 
        onsorting="grdSearchProducts_Sorting" >
    <HeaderStyle CssClass="bluetext" />
    <EmptyDataRowStyle Font-Bold="true" BackColor="#cacaca" HorizontalAlign="Center" />
        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" 
            PreviousPageText="Previous" />
    <RowStyle  BorderWidth="2px" BorderStyle="Solid" BorderColor="#E1E1E1" BackColor="White" CssClass="text" />
    <AlternatingRowStyle BorderWidth="2px" BorderColor="#E1E1E1" BorderStyle="None" CssClass="bluetext" BackColor="#F1F0F0" />
        <PagerTemplate>
            <div ID="pager">
            <asp:LinkButton ID="imgPrev" runat="server" CommandArgument="Prev" 
                    CommandName="page">Previous</asp:LinkButton>&nbsp;|&nbsp;
                <asp:LinkButton ID="imgNext" runat="server" CommandArgument="Next" 
                    CommandName="page">Next</asp:LinkButton>&nbsp;
                Page&nbsp;<asp:Label ID="lblPage" runat="server">1</asp:Label>&nbsp;of&nbsp;
                <asp:Label ID="lblPages" runat="server"></asp:Label>&nbsp;pages | View
                <asp:DropDownList ID="ddlPage" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlPage_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                </asp:DropDownList>
                &nbsp;records per page | Total <strong>
                <asp:Label ID="lblrecord" runat="server"></asp:Label>
                </strong>records found
            </div>
        </PagerTemplate>
    <PagerStyle CssClass="link" BorderWidth="2px" BorderStyle="None" HorizontalAlign="Center" />
    <Columns>
    <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName"> 
    <ItemTemplate>
    <asp:HyperLink ID="hlProdut" runat="server" CssClass="bluelink" Text='<%#HttpUtility.HtmlDecode(Eval("ProductName").ToString())%>' NavigateUrl='<%# "Product.aspx?pid="+Eval("id") %>'></asp:HyperLink>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="cas" SortExpression="cas" HeaderText="CAS#" 
            HeaderStyle-Width="16%" >
<HeaderStyle Width="16%"></HeaderStyle>
        </asp:BoundField>
        <asp:TemplateField HeaderText="Formula" SortExpression="formula">
            <ItemTemplate>
                <asp:Label ID="lblformula" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="M.W." SortExpression="mweight">
            <ItemTemplate>
                <asp:Label ID="lblWeight" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sqlSearchProducts" runat="server" 
        ConnectionString="<%$ ConnectionStrings:dbConnect %>"></asp:SqlDataSource>
</td>
</tr>

</table>
<br />
</div></td>
</tr>
</table></td>
<td width="5"><img src="images/space.gif" width="1" height="1" /></td>
</tr>
</table>
</td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
</table>
</asp:Content>

