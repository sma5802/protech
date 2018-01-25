<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" EnableEventValidation="true" AutoEventWireup="true" Inherits="Admin_Peptech_ManageImage_EditImage" Codebehind="EditImage.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

<h2>Add Product Image</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg" ></asp:Label>
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle" style="width:100%">
<asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
<ContentTemplate >
<TABLE class="box" width="100%">
<TBODY><TR>
<TD style="WIDTH: 17%" vAlign=middle align=left>
<asp:Label id="Label4" runat="server" Text="Category Name:" Font-Bold="true"></asp:Label> 
</TD>
<TD style="WIDTH: 83%" vAlign=middle align=left>
<asp:DropDownList id="ddlCategory" runat="server" ValidationGroup="insertcategory" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
 <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ValidationGroup="insertcategory" ErrorMessage="Please Choose Category Name" ControlToValidate="ddlCategory" Display="None"></asp:RequiredFieldValidator> </TD></TR>
 <TR><TD style="WIDTH: 17%" vAlign=middle align=left>
 <asp:Label id="Label5" runat="server" Text="SubCategory Name:" Font-Bold="true"></asp:Label> </TD><TD style="WIDTH: 83%" vAlign=middle align=left><asp:DropDownList id="ddlSubCategory" runat="server" ValidationGroup="insertcategory" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList> 
 <%--<asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ValidationGroup="insertcategory" ErrorMessage="Please Choose SubCategory Name" ControlToValidate="ddlSubCategory" InitialValue="<--Please Select-->" Display="None"></asp:RequiredFieldValidator>--%> </TD></TR>
 <TR><TD style="WIDTH: 17%" vAlign=middle align=left>
 <asp:Label id="lblSelectProductname" runat="server" Text="Product Name:" Font-Bold="true"></asp:Label> </TD><TD style="WIDTH: 83%" vAlign=middle align=left><asp:DropDownList id="ddlProduct" runat="server" ValidationGroup="insertcategory"></asp:DropDownList> 
 <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ValidationGroup="insertcategory" ErrorMessage="Please Choose Product Name" ControlToValidate="ddlProduct" Display="None"></asp:RequiredFieldValidator> </TD></TR>
 </TBODY></TABLE>
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>

<tr>
<td align="left" valign="middle" >
<TABLE class="box" width="100%">     
     <tr>
        <td align="left" valign="top">
        <asp:Label ID="Label7" runat="server" Text="Product Image:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
            <asp:FileUpload ID="fuPImage" runat="server"  />&nbsp;<asp:Image ID="Image1" runat="server" />
       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="fuPImage"
        ErrorMessage="Please Enter Product Image" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    
<tr>
<td ></td>
<td>
<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>
<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />

</td>
</tr>
</table>
</td>
</tr>
</table>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="insertcategory" />

<div style="position:absolute; z-index:1; left: 40%;top: 40%;">
<asp:UpdateProgress id="updateprg" runat="server" >
<progresstemplate>
<img align="left" src="../../images/loadingimg.gif" alt=""  />
</progresstemplate>
</asp:UpdateProgress>
</div>
</asp:Content>
