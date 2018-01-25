<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="True" Inherits="Admin_Peptech_ManageProduct_EditProduct" Codebehind="EditProduct.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
function doGetCaretPosition(symbol) {
    var oField=document.getElementById('<%=txtProduct.ClientID %>');

    // Initialize
    var iCaretPos = 0;

    // IE Support
    if (document.selection) 
    { 
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

    <h2>
        Edit Product</h2>
    <asp:Label ID="lblmessage" runat="server" Visible="true" CssClass="msg"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnupdate">
        <table class="box" width="100%">
            <tr>
                <td align="left" valign="middle" style="width: 100%">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                        <contenttemplate>
                            <table class="box" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 17%" valign="middle" align="left">
                                            <asp:Label ID="Label4" runat="server" Text="Category Name:" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 83%" valign="middle" align="left">
                                            <asp:DropDownList ID="ddlCategory" runat="server" ValidationGroup="insertcategory"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="insertcategory"
                                                ErrorMessage="Please Choose Category Name" ControlToValidate="ddlCategory" Display="None"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 17%" valign="middle" align="left">
                                            <asp:Label ID="Label5" runat="server" Text="SubCategory Name:" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 83%" valign="middle" align="left">
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" ValidationGroup="insertcategory">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" valign="middle">
                    <table class="box" width="100%">
                        <tr>
                            <td align="left" valign="middle">
                                <asp:Label ID="Label8" runat="server" Text="Select Symbol:" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" valign="middle">
                                <asp:DataList ID="DataList1" runat="server" Width="20%" RepeatColumns="5" RepeatDirection="Horizontal"
                                    OnItemCommand="DataList1_ItemCommand">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="doGetCaretPosition (this); return false;"
                                            CommandArgument='<%#HttpUtility.HtmlDecode(Eval("Greekchar").ToString())%>'>
                                                <%#HttpUtility.HtmlDecode(Eval("Greekchar").ToString())%>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <asp:Label ID="Label6" runat="server" Text="Product Name:" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" valign="middle" style="width: 83%">
                                <asp:TextBox ID="txtProduct" runat="server" ValidationGroup="insertcategory" Width="200"
                                    MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProduct"
                                    ErrorMessage="Please Enter Product Name" ValidationGroup="insertcategory" Display="None"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <asp:Label ID="Label1" runat="server" Text="CAS:" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" style="width: 83%" valign="middle">
                                <asp:TextBox ID="txtCAS" runat="server" MaxLength="50" ValidationGroup="insertcategory"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <asp:Label ID="Label2" runat="server" Text="Formula:" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" style="width: 83%" valign="middle">
                                <asp:TextBox ID="txtFormula" runat="server" MaxLength="15" ValidationGroup="insertcategory"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFormula"
                                    ErrorMessage="Please Enter Formula" ValidationGroup="insertcategory" Display="None"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                <asp:Label ID="Label3" runat="server" Text="Molecular Weight:" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" style="width: 83%" valign="middle">
                                <asp:TextBox ID="txtWeight" runat="server" MaxLength="200" ValidationGroup="insertcategory"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtWeight"
                                    ErrorMessage="Please Enter Molecular Weight" ValidationGroup="insertcategory"
                                    Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtWeight"
                                    Display="None" ErrorMessage="Molecular weight must be in numeric form" Operator="DataTypeCheck"
                                    SetFocusOnError="True" Type="Double" ValidationGroup="insertcategory"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="Label7" runat="server" Text="Product Image:" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left" style="width: 83%" valign="middle">
                                <asp:FileUpload ID="fuPImage" runat="server" />&nbsp;<asp:Image ID="Image1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnupdate" runat="server" CssClass="button" Text="Update" OnClick="btnupdate_Click"
                                    ValidationGroup="insertcategory" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="insertcategory" />
    <div style="position: absolute; z-index: 1; left: 40%; top: 40%;">
        <asp:UpdateProgress id="updateprg" runat="server">
            <progresstemplate>
                <img align="left" src="../../images/loadingimg.gif" alt=""  />
            </progresstemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
