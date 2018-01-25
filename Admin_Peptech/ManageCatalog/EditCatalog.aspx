<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" Debug="true" AutoEventWireup="true" Inherits="Admin_Peptech_ManageCatalog_EditCatalog" Codebehind="EditCatalog.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
   function doGetCaretPosition(symbol) {
var oField=document.getElementById('<%=txtProduct.ClientID %>');

     // Initialize
     var iCaretPos = 0;

     // IE Support
     if (document.selection) { 

       // Set focus on the element
       oField.focus ();
  
       // To get cursor position, get empty selection range
       var oSel = document.selection.createRange ();
  
       // Move selection start to 0 position
       oSel.moveStart ('character', -oField.value.length);
  
       // The caret position is selection length
       iCaretPos = oSel.text.length;
     }

     // Firefox support
     else if (oField.selectionStart || oField.selectionStart == '0')
       iCaretPos = oField.selectionStart;
     
     //alert(iCaretPos);
     // Return results
     var all=oField.value;
     
     var first=all.substring(0,iCaretPos);
     var middle=symbol.innerHTML;
     var last=all.substring(iCaretPos,all.length);
     
     all=first+middle+last;
     oField.value=all;
    return iCaretPos;
   }

    </script>

<script language="javascript" type="text/javascript">
function maxlength(txt,maxlength)
{
   if(txt.value.length > (maxlength-1))
   {
       txt.value = txt.value.slice(0,maxlength);
       return false;
   }
}

</script>

<h2>Edit Catalog</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg"></asp:Label>
<asp:Panel ID="Panel1" runat="server">
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle" style="width:100%">
<%--<asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
<ContentTemplate >--%>
<%--<TABLE class="box" width="100%">
<TBODY><TR>
<TD style="WIDTH: 17%" vAlign=middle align=left>
<asp:Label id="Label4" runat="server" Text="Category Name:" Font-Bold="true"></asp:Label> 
</TD>
<TD style="WIDTH: 83%" vAlign=middle align=left>
<asp:DropDownList id="ddlCategory" runat="server" ValidationGroup="insertcategory" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
 <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ValidationGroup="insertcategory" ErrorMessage="Please Choose Category Name" ControlToValidate="ddlCategory" InitialValue="<--Please Select-->" Display="None"></asp:RequiredFieldValidator> </TD></TR>
 <TR><TD style="WIDTH: 17%" vAlign=middle align=left>
 <asp:Label id="Label5" runat="server" Text="SubCategory Name:" Font-Bold="true"></asp:Label> </TD><TD style="WIDTH: 83%" vAlign=middle align=left><asp:DropDownList id="ddlSubCategory" runat="server" ValidationGroup="insertcategory"></asp:DropDownList> 
 <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ValidationGroup="insertcategory" ErrorMessage="Please Choose SubCategory Name" ControlToValidate="ddlSubCategory" InitialValue="<--Please Select-->" Display="None"></asp:RequiredFieldValidator> </TD></TR></TBODY></TABLE>
--%><%--</ContentTemplate>
</asp:UpdatePanel>--%>
</td>
</tr>
<tr>
<td align="left" valign="middle" >
<TABLE class="box" width="100%">
<tr><td align="left" valign="middle" >
<asp:Label ID="Label4" runat="server" Text="Select Product:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle" style="width:83%">
    <asp:DropDownList ID="ddlProduct" runat="server">
    </asp:DropDownList>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProduct"
        ErrorMessage="Please Select Product Name" ValidationGroup="insertcategory" Display="None" InitialValue="<--Please Select-->"></asp:RequiredFieldValidator>
   <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrice" ErrorMessage="Please Enter Service Price in Numeric Format" ValidationGroup="insertcategory" Display="None" Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>--%>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label3" runat="server" Text="Select Symbol:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
 <asp:DataList ID="DataList1" runat="server" Width="20%" RepeatColumns="5" RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand">
        <ItemTemplate>
        <%-- <a href='javascript:void(0);' onclick='funccatcher("α");'>
        <%#HttpUtility.HtmlDecode(Eval("Greekchar").ToString())%>
        </a>--%> 
        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="doGetCaretPosition (this); return false;" CommandArgument='<%#HttpUtility.HtmlDecode(Eval("Greekchar").ToString())%>'>
        <%#HttpUtility.HtmlDecode(Eval("Greekchar").ToString())%>
        </asp:LinkButton>
        </ItemTemplate>
        </asp:DataList>
</td>
</tr>

<tr><td align="left" valign="middle" >
<asp:Label ID="Label6" runat="server" Text="Catalog Name:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle" style="width:83%">
<asp:TextBox ID="txtProduct" runat="server"  ValidationGroup="insertcategory" 
        MaxLength="50"></asp:TextBox>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProduct"
        ErrorMessage="Please Enter Catalog Name" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
   <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrice" ErrorMessage="Please Enter Service Price in Numeric Format" ValidationGroup="insertcategory" Display="None" Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>--%>
</td>
</tr>
    <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label1" runat="server" Text="Quantity:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
        <asp:TextBox ID="txtQuantity" runat="server" MaxLength="5" 
                ValidationGroup="insertcategory"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity"
        ErrorMessage="Please Enter Quantity" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please Enter CAS In Correct format" ControlToValidate="txtCAS" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label2" runat="server" Text="Price:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
        <asp:TextBox ID="txtPrice" runat="server" ValidationGroup="insertcategory" 
                MaxLength="5"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice"
        ErrorMessage="Please Enter Price" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please enter price in correct format" Type="Double" ControlToValidate="txtPrice" Display="None" ValidationGroup="insertcategory"></asp:RangeValidator>
        </td>
    </tr>
<tr>
<td ></td>
<td>
<%--<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>
--%><asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>
<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />

</td>
</tr>
</table>
</td>
</tr>
</table>
 </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="insertcategory" />

<%--<div style="position:absolute; z-index:1; left: 40%;top: 40%;">
<asp:UpdateProgress id="updateprg" runat="server" >
<progresstemplate>
<img align="left" src="../../images/loadingimg.gif" alt=""  />
</progresstemplate>
</asp:UpdateProgress>
</div>
--%></asp:Content>

