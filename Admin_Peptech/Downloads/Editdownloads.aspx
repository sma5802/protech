<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_Downloads_Editdownloads" Codebehind="Editdownloads.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
  
   function showchars()
{
    var totalchar = 0;
    var length = document.getElementById('<%=txtTitle.ClientID %>').value.length;
    if(length >500)
    {
    document.getElementById('<%=txtTitle.ClientID %>').value = document.getElementById('<%=txtTitle.ClientID %>').value.slice(0,500);
    }
}
</script>
<h2>Edit Downloads</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg"></asp:Label>
<asp:Panel ID="pnlSubmit" runat="server" >
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="lblCounties" runat="server" Text="Download Text:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
<asp:TextBox ID="txtDownloadtext" Width="80%" runat="server" MaxLength="200"  ValidationGroup="insertcategory"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqcategory" runat="server" ControlToValidate="txtDownloadtext"
        ErrorMessage="Plaese enter Download Text" ValidationGroup="state"  Display="None"></asp:RequiredFieldValidator>
</td>
</tr>

<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label2" runat="server" Text="Title:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
<asp:TextBox ID="txtTitle" onblur="showchars()" onkeyup="showchars()" Width="80%" runat="server" MaxLength="500" TextMode="MultiLine" Rows="3" ValidationGroup="insertcategory"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
        ErrorMessage="Plaese enter Download Title" ValidationGroup="state"  Display="None"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label1" runat="server" Text="Download File:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
    <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;<%--<a id="ViewDownload" runat="server" target="_blank">Downloads</a>--%>
    <asp:LinkButton ID="lbDownload" runat="server" oncommand="lbDownload_Command">Downloads</asp:LinkButton>
    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
        ErrorMessage="Please Enter State Image" ValidationGroup="state"  Display="None"></asp:RequiredFieldValidator>--%>
</td>
</tr>
<tr>
<td align="left" valign="middle"  >
<asp:Label ID="Label3" runat="server" Text="Image:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
    
    <asp:FileUpload ID="FileUpload2" runat="server" />&nbsp;&nbsp;<asp:Image ID="Image1" runat="server" />
    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
        ErrorMessage="Please Enter State Image" ValidationGroup="state"  Display="None"></asp:RequiredFieldValidator>--%>
</td>
</tr>

<tr>
<td ></td>
<td>
<%--<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="state" />
--%><asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="state"/>
<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="state" />
</td>
</tr>
</table>
    </asp:Panel>
</asp:Content>

