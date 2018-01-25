<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_Order_OrderListing" Codebehind="OrderListing.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../../js_calendar/calendar.js"></script>
 <link href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>js_calendar/calendar.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function test1(obj)
{
alert(obj.value.length);
alert(document.getElementById('<%=TextBox1.ClientID%>').value.length);
}
function displayCalendar1(inputField,format,buttonObj)
{
alert(inputField);
}
</script>
<asp:Panel ID="Panel1" runat="server" Width="100%" >  
<table border="0" cellpadding="1" class="box" Width="100%" cellspacing="1">
<tr>
<td align="center"><asp:Label ID="lblorderstatus" CssClass="tital" runat="server" Text="Orders" Font-Bold="True"  Width="176px"></asp:Label></td>
</tr>
<tr>
<td>
<asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
<img id="tst" src="../../images/calendar_icon.gif" runat="server" visible="false" onclick="test1(document.getElementById('<%=TextBox1.ClientID%>'))" />
</td>
</tr>
<tr>
    <td style="height: 43px" align="center">
        From&nbsp;&nbsp;<asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" CssClass="inp"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqtxtstartdate" runat="server" ControlToValidate="txtFromDate"
                  Text="(Required)" Display="Dynamic" ValidationGroup="find"></asp:RequiredFieldValidator>
                <img  style="vertical-align:middle;cursor:pointer" src="../../images/calendar_icon.gif"  alt="Choose date" onclick="displayCalendar('<%=txtFromDate.ClientID%>','mm/dd/yyyy',this)"/>&nbsp;&nbsp; 
        (MM/DD/YYYY )&nbsp;&nbsp;
            
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
Display="Dynamic" ErrorMessage="Invalid Date Format"   
ValidationExpression="((0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d)|((1[012]|0[1-9])(3[01]|2\d|1\d|0[1-9])(19|20)\d\d)" ValidationGroup="find"></asp:RegularExpressionValidator>

        
        To&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" CssClass="inp"></asp:TextBox>
              <asp:RequiredFieldValidator ID="Reqtxtenddate" runat="server" ControlToValidate="txtToDate"
                  Text="(Required)" Display="Dynamic" ValidationGroup="find"></asp:RequiredFieldValidator>
                  <img  style="vertical-align:middle;cursor:pointer" src="../../images/calendar_icon.gif"  alt="Choose date" onclick="displayCalendar('<%=txtToDate.ClientID%>','mm/dd/yyyy',this)"/>&nbsp;&nbsp;
(MM/DD/YYYY )&nbsp;&nbsp;
          
             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtToDate"
Display="Dynamic" ErrorMessage="Invalid Date Format" ValidationExpression="((0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d)|((1[012]|0[1-9])(3[01]|2\d|1\d|0[1-9])(19|20)\d\d)" ValidationGroup="find"></asp:RegularExpressionValidator>
    
    
        <br />
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="Dynamic" 
            ErrorMessage="To Date Must be Grater Than From Date" Operator="LessThan" 
            Type="Date" ValidationGroup="find"></asp:CompareValidator>
    
    
        <br />
    
    
        <asp:Button ID="btnSearch" runat="server" CssClass="button" 
            OnClick="btnSearch_Click" Text="Find Order" ValidationGroup="find" />
    
    
    </td>
</tr>
</table> 

<br /><br />

<table id="tbl_Add_Category" runat="server" class="box" width="100%" >
<tr><td align="left">
&nbsp;
<br />
<table width="100%" >
<tr>
<td align="left">
<asp:Button ID="Button1" runat="server" CssClass="button" Text="Update Paid Status" OnClick="Button1_Click" Visible="False" /> <asp:Label ID="lblmessage" runat="server" ForeColor="red" Visible="false"> </asp:Label>
    </td>
<%--<td align="right">
Show Orders By &nbsp;&nbsp;<asp:DropDownList ID="DropDownListStatusSearch" runat="server">
<asp:ListItem>Pending</asp:ListItem>
<asp:ListItem>Confirmed</asp:ListItem>
<asp:ListItem>Shipped</asp:ListItem>
<asp:ListItem>Delivered</asp:ListItem>
<asp:ListItem>Payment Not Confirmed</asp:ListItem>
<asp:ListItem>Back-Order</asp:ListItem>
<asp:ListItem>Canceled</asp:ListItem>
<asp:ListItem>All Order</asp:ListItem>
</asp:DropDownList>&nbsp;<asp:Button ID="SearchByOrder" runat="server" OnClick="SearchByOrder_Click"
Text="Show" CssClass="button" /></td>--%>
</tr>
</table>
<br />
<asp:GridView ID="GridView1" PageSize="50" AllowPaging="true" ShowFooter="true" AutoGenerateColumns="false" EmptyDataText="No record found" CssClass="grid" Width="100%" runat="server" OnPageIndexChanging="GridViw1_OnPageIndexChanging" OnRowDataBound="Gridview1_rowDataBound" CellPadding="6">
 <HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox runat="server" ID="chbispaid" /> 
<asp:HiddenField ID="hidorderid" runat="server" Value='<%#Eval("OrderID") %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:BoundField HeaderText="OrderNo" DataField="Orderno" />
<%--<asp:TemplateField HeaderText="Order Status">
<ItemTemplate>
<asp:DropDownList ID="DropDownListStatus" runat="server">
<asp:ListItem>Pending</asp:ListItem>
<asp:ListItem>Confirmed</asp:ListItem>
<asp:ListItem>Shipped</asp:ListItem>
<asp:ListItem>Delivered</asp:ListItem>
<asp:ListItem>Payment Not Confirmed</asp:ListItem>
<asp:ListItem>Back-Order</asp:ListItem>
<asp:ListItem>Canceled</asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>   --%>

<asp:BoundField HeaderText="Net Amount" DataField="Netamount" DataFormatString="{0:c}" HtmlEncode="False">
    <ItemStyle HorizontalAlign="Right" />
    <HeaderStyle HorizontalAlign="Right"/>
</asp:BoundField>
<asp:BoundField HeaderText="Order Date" DataField ="Orderdate" DataFormatString='{0:M-dd-yyyy}' HtmlEncode="False">
 <ItemStyle HorizontalAlign="Right" />
    <HeaderStyle HorizontalAlign="Right"/>
</asp:BoundField>
<asp:TemplateField HeaderText="Ispaid">
<ItemTemplate>
<asp:Image ID="ImageStatus" runat="server" />
</ItemTemplate>
 <ItemStyle HorizontalAlign="Center" />
    <HeaderStyle HorizontalAlign="Center"/>
</asp:TemplateField>

<asp:TemplateField HeaderText="View">
<ItemTemplate >      
<a href="vieworder.aspx?ID=<%# Eval("OrderNo") %>&userid=<%# Eval("UserID") %>">
<asp:Image ID="Image1" runat="server" ImageUrl="~/images/copy.gif" /></a>


</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
    <HeaderStyle HorizontalAlign="Center"/>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete">
<ItemTemplate>      
<asp:LinkButton ID="lnkdelete" runat="server"  OnCommand="OnCommand_Delete" CommandArgument='<%#Eval("orderNo") %>' OnClientClick="javascript:return confirm('Are you sure want to delete this Order? Details will also be deleted.')"><asp:Image
ID="Image2" runat="server" ImageUrl="../../images/del.gif" /></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
<HeaderStyle HorizontalAlign="Center"/>
</asp:TemplateField>

</Columns>

</asp:GridView>
</td></tr>
</table>
&nbsp;
<table width="100%">
<tr>
<td align="right">
<asp:Button ID="Button2" runat="server" CssClass="button" OnClick="Button2_Click" Text="Confirm Orders" /></td>
</tr>
</table>
</asp:Panel><br /><br />
<table width="100%" class="box">
<tr>
<td align="center"><br /><u>
DownLoad Orders in Excel format</u>
</td>
</tr>
<tr>
<td style="height: 43px"><br />
From&nbsp;&nbsp;<asp:TextBox ID="txtstdate" runat="server" ReadOnly="false" ValidationGroup="vgSearch1"></asp:TextBox>(mm/dd/yyyy)
<asp:RequiredFieldValidator ControlToValidate="txtstdate" ID="RequiredFieldValidator3" ValidationGroup="vgSearch1" runat="server" Text="(Required)" Display="Dynamic" ForeColor="red"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="vgSearch1" ErrorMessage="(invalid)" ForeColor="red" ControlToValidate="txtstdate" Operator="DataTypeCheck" Type="Date" Text="(invalid)" Display="Dynamic"></asp:CompareValidator>

To&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtenddate" ValidationGroup="vgSearch1" runat="server" ReadOnly="false" ></asp:TextBox> (mm/dd/yyyy)
<asp:RequiredFieldValidator ValidationGroup="vgSearch1" runat="server" ID="RequiredFieldValidator4"  Text="(Required)" ForeColor="red" Display="Dynamic" ControlToValidate="txtenddate"></asp:RequiredFieldValidator>
<asp:CompareValidator ValidationGroup="vgSearch1" ID="CompareValidator4" runat="server" ErrorMessage="(invalid)" Text="(invalid)" Operator="DataTypeCheck" ControlToValidate="txtenddate" Type="Date" ForeColor="red" Display="Dynamic"></asp:CompareValidator>
<%-- <asp:Button ID="Button3" runat="server" Text="Find Order" OnClick="btnSearch_Click" ValidationGroup="vgSearch"/>--%>
</td>
</tr>
<tr>
<td>
<asp:LinkButton ID="lnkdownload" runat="server" ValidationGroup="vgSearch1" OnClick="lnkdownload_click">
<asp:Label ID="lbldownload" runat="server" Text="Download in excel"/>
</asp:LinkButton>
</td>
</tr>
</table>

</asp:Content>

