<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" Inherits="Admin_Peptech_Downloads_Adddownload" Codebehind="Adddownload.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<script language="javascript" type="text/javascript">

  
   function showchars()
{
    var totalchar = 0;
    var length = document.getElementById('<%=txtTitle.ClientID %>').value.length;
    if(length >500)
    {
    document.getElementById('<%=txtTitle.ClientID %>').value = document.getElementById('<%=txtTitle.ClientID %>').value.slice(0,500);
    }
}

function ImagePreview()
  {
        var a = document.getElementById ("ctl00_ContentPlaceHolder1_FileUpload1").value;
        var value = a.split(".");
        value.reverse(); 
        //alert(value[0]);           
        var ExtensionList = "doc,docx,pdf,sdf,jpg,bmp,gif,jpeg,png";        
        var Extension = ExtensionList.split(",");
        var i;
        var ImageCheck = ""; 
        for(i=0;i<9;i++)
        {            
            if(value[0] == Extension[i])
            { 
                 ImageCheck = true;
                  break;
                //return true;
            }
            else
            {
              ImageCheck = false;
            }
        }  
        if(ImageCheck==true)      
        {
            return true;
        }
        else
        {
        alert("Please enter appropriate format.")
        return false;
        }
    
}



</script>--%>

<h2>Add Downloads</h2>
<asp:Label id="lblmessage" runat="server"  Visible="true" CssClass="msg"></asp:Label>
<asp:Panel ID="pnlSubmit" runat="server" >
<table class="box" width="100%" >
<tr>
<td align="left" valign="middle" style="width: 214px"  >
<asp:Label ID="lblCounties" runat="server" Text="Download Text:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
<asp:TextBox ID="txtDownloadtext" Width="80%" runat="server" MaxLength="200"  ValidationGroup="insertcategory"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqcategory" SetFocusOnError="true" runat="server" ControlToValidate="txtDownloadtext"
        ErrorMessage="Plaese enter Download Text" ValidationGroup="vld"  
        Display="None"></asp:RequiredFieldValidator>
</td>
</tr>

<tr>
<td align="left" valign="middle" style="width: 214px"  >
<asp:Label ID="Label2" runat="server" Text="Title:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
<asp:TextBox ID="txtTitle"  runat="server" Width="80%" MaxLength="500" TextMode="MultiLine" Rows="3" ValidationGroup="insertcategory"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="txtTitle"
        ErrorMessage="Plaese enter Download Title" ValidationGroup="vld"  
        Display="None"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td align="left" valign="middle" style="width: 214px"  >
<asp:Label ID="Label1" runat="server" Text="Upload Download File:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle">
    <asp:FileUpload ID="FileUpload1" runat="server" />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="FileUpload1" Display="None" 
        ErrorMessage="Please Enter download file"  SetFocusOnError="true"
        ValidationGroup="vld"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="FileUpload1" Display="None" 
        ErrorMessage="Only pdf,doc,docx,jpg,jpeg,gif,png files are allowed." SetFocusOnError="True" 
        
        
        ValidationExpression="^.*\.((p|P)(d|D)(f|F)|(d|D)(o|O)(c|C)(x|X)|(s|S)(d|D)(f|F)?|(j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G))$" 
        ValidationGroup="vld"></asp:RegularExpressionValidator>
    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
        ErrorMessage="Please Enter State Image" ValidationGroup="state"  Display="None"></asp:RequiredFieldValidator>--%>
</td>
</tr>

<tr>

<td align="left" valign="middle" style="width: 214px" >
    <asp:Label ID="Label3" runat="server" Font-Bold="true" 
        Text="Upload Download File Image:"></asp:Label>
    </td>
<td align="left" valign="middle">
    <asp:FileUpload ID="FileUpload2" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
        ControlToValidate="FileUpload2" Display="None" SetFocusOnError="true"
        ErrorMessage="Please Enter download file type Image " 
        ValidationGroup="vld"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="revImage" runat="server" 
        ControlToValidate="FileUpload2" Display="None" 
        ErrorMessage=" ! Invalid image type" SetFocusOnError="True" 
        Text=" ! Invalid image type" 
        ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G))$" 
        ValidationGroup="vld"></asp:RegularExpressionValidator>
</td>
</tr>
    <tr>
        <td style="width: 214px">
        </td>
        <td>
            <asp:Button ID="btnsubmit" runat="server" CssClass="button" 
                OnClick="btnsubmit_Click"  Text="Save" 
                ValidationGroup="vld" style="width: 42px; height: 26px" />
            <%--<asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click" ValidationGroup="insertcategory"/>
--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vld" />
        </td>
    </tr>
</table>
    </asp:Panel>
</asp:Content>


