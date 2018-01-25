<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Admin_Peptech_ManageCategory_EditCategory" Codebehind="EditCategory.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">
   function doGetCaretPosition(symbol) {
var oField=document.getElementById('<%=txtCounties.ClientID %>');

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
<h2>Edit Category Name</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg"></asp:Label>
<asp:Panel ID="pnlSubmit" runat="server" >
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label1" runat="server" Text="Select Symbol:" Font-Bold="true"></asp:Label>
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
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="lblCounties" runat="server" Text="Category Name:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
<asp:TextBox ID="txtCounties" runat="server" MaxLength="200"  ValidationGroup="insertcategory" Width="250px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqcategory" runat="server" ControlToValidate="txtCounties"
        ErrorMessage="Required" ValidationGroup="insertcategory"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td style="height: 26px" ></td>
<td style="height: 26px">
<%--<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>--%>
<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>
<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />

</td>
</tr>
</table>
    </asp:Panel>
</asp:Content>

