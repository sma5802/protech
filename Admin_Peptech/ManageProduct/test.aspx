<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Admin_Peptech_ManageProduct_test"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Add Product</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg" ></asp:Label>
<asp:Panel ID="Panel1" runat="server"  DefaultButton="btnsubmit">
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
 <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ValidationGroup="insertcategory" ErrorMessage="Please Choose Category Name" ControlToValidate="ddlCategory" InitialValue="<--Please Select-->" Display="None"></asp:RequiredFieldValidator> </TD></TR>
 <TR><TD style="WIDTH: 17%" vAlign=middle align=left>
 <asp:Label id="Label5" runat="server" Text="SubCategory Name:" Font-Bold="true"></asp:Label> </TD><TD style="WIDTH: 83%" vAlign=middle align=left><asp:DropDownList id="ddlSubCategory" runat="server" ValidationGroup="insertcategory"></asp:DropDownList> 
 <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ValidationGroup="insertcategory" ErrorMessage="Please Choose SubCategory Name" ControlToValidate="ddlSubCategory" InitialValue="<--Please Select-->" Display="None"></asp:RequiredFieldValidator> </TD></TR></TBODY></TABLE>
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
<tr>
<td align="left" valign="middle" >
<TABLE class="box" width="100%">
     <tr>
        <td align="left" valign="top">
        <asp:Label ID="Label7" runat="server" Text="Upload Product Excel:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
            <asp:FileUpload ID="fuPImage" runat="server" />
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="fuPImage"
        ErrorMessage="Please Enter Product Image" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
<tr>
<td ></td>
<td>
<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" />
<%--<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>
--%></td>
</tr>
</table>
</td>
</tr>
</table>
 </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="insertcategory" />

<div style="position:absolute; z-index:1; left: 40%;top: 40%;">
<asp:UpdateProgress id="updateprg" runat="server" >
<progresstemplate>
<img align="left" src="../../images/loadingimg.gif" alt=""  />
</progresstemplate>
</asp:UpdateProgress>
</div>

</asp:Content>

