<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Admin_Peptech_ManageNews_AddNews" Codebehind="AddNews.aspx.cs" %>
<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
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
<h2>Add News</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg"></asp:Label>
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle"  >
<table class="box" width="100%" >
    <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label4" runat="server" Text="News Headlines:" Font-Bold="true"></asp:Label>

        
        </td>
        <td align="left" valign="middle">
           <asp:TextBox ID="txthead" Width="400" runat="server" MaxLength="200" onkeyup="return maxlength(this,200);"/>&nbsp;max length 200 characters
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter news headlines" ValidationGroup="insertcategory" Display="None" ControlToValidate="txthead" SetFocusOnError="true"/>
            </td>
    </tr>
   
<tr>
<td align="left" valign="top"  >
<asp:Label ID="Label1" runat="server" Text="News Details:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
 <fckeditorv2:fckeditor runat="server" id="FCKeditor1" SkinPath="skins/office2003/" BasePath="~/FCKeditor/" height="350px"></fckeditorv2:fckeditor>
   <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FCKeditor1"
        ErrorMessage="Please Enter Service Description" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>--%>
</td>
</tr>
<tr>
<td ></td>
<td>
<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>
<%--<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/></td>
--%></tr>
</table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="insertcategory" />
</td>
</tr>
</table>

</asp:Content>

