<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="True" Inherits="Admin_Peptech_ManageProduct_AddBatchProduct" Codebehind="AddBatchProduct.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Add Batch Product</h2>
<asp:Label id="lblmessage" runat="server"  Visible="false" CssClass="msg" ></asp:Label>
<asp:Panel ID="Panel1" runat="server"  DefaultButton="btnsubmit">
<table class="box" width="100%" >
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
    <asp:GridView ID="GridView1" runat="server" CssClass="grid" AllowPaging="true" PageSize="15" Width="80%" OnPageIndexChanging="GridView1_PageIndexChanging">
    <HeaderStyle  CssClass="gridhead" />
<RowStyle CssClass="gridcell" Height="20px" />
<AlternatingRowStyle  CssClass="gridcellalt"/>
<PagerStyle  HorizontalAlign="Center" />
    </asp:GridView>
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

