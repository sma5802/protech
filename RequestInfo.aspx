<%@ Page Language="C#" MasterPageFile="~/Content.master" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" AutoEventWireup="true" Inherits="RequestInfo" Codebehind="RequestInfo.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">
function showchars()
{

    var totalchar = 0;
    var length = document.getElementById("<%=txtComments.ClientID%>").value.length;
    if(length >5000)
    {
    document.getElementById("<%=txtComments.ClientID%>").value = document.getElementById("<%=txtComments.ClientID%>").value.slice(0,5000);
    }    
    document.getElementById('leftchar').innerHTML = 5000 - document.getElementById("<%=txtComments.ClientID%>").value.length;
}

</script>
<asp:Panel ID="Panelinfo" runat="server" DefaultButton="ImgbtnSubmit">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td>
<table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top"><div align="justify"></div></td>
<td align="left" valign="top"><div align="justify"></div></td>
</tr>
<tr>
<td align="left" valign="top">
<div align="justify">
</div>
</td>
<td align="left" valign="top">
<table border="0" cellpadding="0" cellspacing="0" width="400">
<tr>
<td align="left" width="150" ><asp:LinkButton ID="lnkquotes" CssClass="bluelink1" runat="server" OnClick="lnkquotes_Click">Quote Request</asp:LinkButton></td>
<td align="left" width="10" >|</td>
<td align="left" ><asp:LinkButton ID="lnkcatlog" CssClass="bluelink1" runat="server" OnClick="lnkcatlog_Click">Catalog Request</asp:LinkButton></td>
</tr>
</table>
<hr  style="width:100%"/>
</td>
</tr>
<tr>
<td align="left" valign="top">
<div align="justify">
</div>
</td>
<td align="left" valign="top">
<div align="justify">
<asp:Label ID="lblTitle" runat="server"  CssClass="blueheading"></asp:Label><br />
<asp:Label ID="lblContent" runat="server"></asp:Label><br />
<asp:Label ID="lblMsg" CssClass="redheading1" Font-Bold="true" runat="server" EnableViewState="false" Visible="false"></asp:Label>

<br />
<span class="blueheading">Contact Information </span><br />
Required information is marked with an asterisk (<span style="color: crimson">*</span>). <br />
<br />
<table border="0" cellspacing="0" cellpadding="2" width= "100%" class="text">
<tr>
<td width="130" rowspan="1" align="left">First Name: <span style="color: crimson"><span style="color: crimson">*</span></span></td>
<td width="150" rowspan="1"><asp:TextBox ID="txtFName" runat="server" CssClass="textForm" size="29" tabindex="1" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="Please enter First Name" ControlToValidate="txtFName" 
        ValidationGroup="req" Display="none" SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
<td width="130" rowspan="1" align="left"> Last Name: <span style="color: crimson">*</span></td>
<td rowspan="1"><asp:TextBox ID="txtLName" runat="server" CssClass="textForm" size="29" tabindex="2" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ErrorMessage="Please enter Last Name" ControlToValidate="txtLName" 
        ValidationGroup="req" Display="none" SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td align="left" rowspan="1"> Company: <span style="color: crimson">*</span></td>
<td rowspan="1"><asp:TextBox ID="txtCompany" runat="server" CssClass="textForm" size="29" tabindex="3" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ErrorMessage="Please enter Company Name" ControlToValidate="txtCompany" 
        ValidationGroup="req" Display="none" SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
<td align="left" rowspan="1">E-mail: <span style="color: crimson">*</span></td>
<td rowspan="1"><asp:TextBox ID="txtEmail" runat="server" CssClass="textForm" size="29" tabindex="4" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ErrorMessage="Please enter Email" ControlToValidate="txtEmail" 
        ValidationGroup="req" Display="none" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
Display="None" ErrorMessage="Please Enter Correct Email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
ValidationGroup="req" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
</tr>
<tr>
<td align="left" rowspan="1"> Telephone: </td>
<td rowspan="1"><asp:TextBox ID="txtTelephone" runat="server" CssClass="textForm" size="29" tabindex="5" MaxLength="25"></asp:TextBox>

</td>
<td align="left" rowspan="1"> Fax: </td>
<td rowspan="1"><asp:TextBox ID="txtFax" runat="server" CssClass="textForm" size="29" tabindex="6" MaxLength="25"></asp:TextBox>
</td>
</tr>
</table>
<br />
<span class="blueheading">Address </span><br />
<br />
<table border="0" cellspacing="0" cellpadding="2" width= "100%" class="text">
<tr>
<td width="25%" rowspan="1" align="left" class="textForm">Street 1: <span style="color: crimson">*</span></td>
<td rowspan="1"><asp:TextBox ID="txtStreet1" runat="server" CssClass="textForm" size="29" tabindex="7" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ErrorMessage="Please enter Street1" ControlToValidate="txtStreet1" 
        ValidationGroup="req" Display="none" SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td valign="middle" align="left" class="textForm"> Street 2: </td>
<td><asp:TextBox ID="txtStreet2" runat="server" CssClass="textForm" size="29" tabindex="8" MaxLength="200"></asp:TextBox>
</td>
</tr>

<tr>
<td>Country: <span style="color: crimson">*</span></td>
<td>
<%--<asp:UpdatePanel ID="uplbilling" runat="server">
<ContentTemplate>--%>
<asp:DropDownList ID="ddlcountry" runat="server" AppendDataBoundItems="True"  Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" DataSourceID="srccountry" DataTextField="Country" DataValueField="ID" TabIndex="9">
 <asp:ListItem Value="0" Text="Select Country"></asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="reqcounrty" ValidationGroup="req" runat="server" InitialValue="0" ControlToValidate="ddlcountry" Display="None" ErrorMessage="Please Select Country" SetFocusOnError="True" ></asp:RequiredFieldValidator>
<asp:SqlDataSource ID="srccountry" runat="server" ConnectionString="<%$ ConnectionStrings:dbConnect %>"
SelectCommand = "SELECT [id], [Country], [isactive] FROM pep$tech$corp.peptech_country where isactive=1">
</asp:SqlDataSource>
<%--</ContentTemplate>
</asp:UpdatePanel>
<div style="position:absolute; z-index:1; left: 45%;top: 70%;">
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplbilling">
<ProgressTemplate>
<img src="images/ajax-loader.gif" alt="Load Progress" border="0" />
</ProgressTemplate>
</asp:UpdateProgress>
</div>  --%>
</td>
</tr>

<tr >
<td>State: <span style="color: crimson">*</span> </td>

<td>
<asp:DropDownList id="ddlstate" runat="server" Width="150px" AppendDataBoundItems="True" DataTextField="State" DataValueField="id" TabIndex="10">
<asp:ListItem Value="0">Select State</asp:ListItem>
</asp:DropDownList><asp:TextBox id="txtOther" runat="server" Visible="False" 
        TabIndex="10" MaxLength="50"></asp:TextBox> 
    <asp:RequiredFieldValidator id="reqstate" runat="server" ValidationGroup="req" 
        ControlToValidate="ddlstate" SetFocusOnError="True" 
        ErrorMessage="Please Select State  " Display="None" InitialValue="0"></asp:RequiredFieldValidator> 
</td>
</tr>

<tr>
<td valign="middle" align="left" class="textForm"> City: <span style="color: crimson">*</span></td>
<td><asp:TextBox ID="txtCity" runat="server" CssClass="textForm" size="29" tabindex="11" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ErrorMessage="Please enter City" ControlToValidate="txtCity" 
        ValidationGroup="req" Display="none" SetFocusOnError="True"></asp:RequiredFieldValidator>
</td>
</tr>

<%--<tr>
<td valign="middle" align="left" class="textForm"> State/Province: <span style="color: crimson">*</span></td>
<td><table border="0" cellspacing="0" cellpadding="0">
<tr>
<td class="text">
<asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" TabIndex="10">
</asp:DropDownList>

<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select State" ControlToValidate="ddlState" ValidationGroup="req" Display="none"></asp:RequiredFieldValidator>
</td>
<td class="tiny" width="10">&nbsp;</td>
<td class="text">
<asp:Label ID="lblOther" runat="server" Text="Other:" Visible="false"></asp:Label></td>
<td class="text"><asp:TextBox ID="txtOther" runat="server" CssClass="textForm" size="29" tabindex="11" MaxLength="200" Visible="false"></asp:TextBox></td>
</tr>
</table></td>
</tr>--%>

<tr>
<td valign="middle" align="left" class="textForm"> Zip/Postal Code: <span style="color: crimson">*</span></td>
<td><asp:TextBox ID="txtZip" runat="server" CssClass="textForm" size="29" tabindex="12" MaxLength="10"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
        ErrorMessage="Please enter Zip/Postal Code" ControlToValidate="txtZip" 
        ValidationGroup="req" Display="none" SetFocusOnError="True"></asp:RequiredFieldValidator>

</td>
</tr>

<%--<tr>
<td align="left" valign="middle" colspan="1"> Country <span style="color: crimson">*</span></td>
<td align="">
<asp:DropDownList ID="ddlCountry" runat="server">
<asp:ListItem Text="United States" Value="254"></asp:ListItem>
</asp:DropDownList>

<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please select Country" ControlToValidate="ddlCountry" ValidationGroup="req" Display="none"></asp:RequiredFieldValidator>
</td>
</tr>--%>

<tr id="trproduct" runat="server" >
<td align="left" valign="middle" colspan="1"> 
  Product Name<span style="color: crimson"></span></td>
<td >
<asp:TextBox ID="txtproduct" runat="server" Width="200px" MaxLength="50" TabIndex="13"></asp:TextBox>
</td>
</tr>

<tr id="trcatalog" runat="server">
<td align="left" valign="middle" colspan="1">Catalog Number</td>
<td>
<asp:TextBox ID="txtcatalognumber" Width="200px" runat="server" MaxLength="20" TabIndex="14"></asp:TextBox>( if Available)
</td>
</tr>

<tr id="trcas" runat="server">
<td align="left" valign="middle" colspan="1"> 
 CAS Number<span style="color: crimson"></span></td>
<td align="">
    <asp:TextBox ID="txtcas" Width="200px" runat="server" MaxLength="30" TabIndex="15"></asp:TextBox>
</td>
</tr>

<tr id="trquantity" runat="server">
<td align="left" valign="middle" colspan="1"> 
  Quantity</td>
<td>
    <asp:TextBox ID="txtquantity" Width="200px" runat="server" MaxLength="10" TabIndex="16"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
        ControlToValidate="txtquantity" Display="None" 
        ErrorMessage="Please enter Quantity in numeric form" SetFocusOnError="True" 
        ValidationExpression="[1-9]{1}[0-9]*" ValidationGroup="req"></asp:RegularExpressionValidator>
</td>
</tr>

<tr id="trpurity" runat="server">
<td align="left" valign="middle" colspan="1"> 
  Purity</td>
<td align="">
    <asp:TextBox ID="txtpurity" Width="200px" runat="server" MaxLength="5" TabIndex="17"></asp:TextBox>
</td>
</tr>

<tr id="trdate" runat="server">
<td align="left" valign="middle" colspan="1"> 
 Date</td>
<td >
<asp:TextBox ID="txtdate" Width="200px" runat="server" MaxLength="10" TabIndex="18"></asp:TextBox>
    (MM/DD/YYYY)<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtdate"
        Display="None" ErrorMessage="Please enter date correct format" Operator="DataTypeCheck"
        SetFocusOnError="True" Type="Date" ValidationGroup="req"></asp:CompareValidator></td>
</tr>

<tr>
<td align="left" valign="top" colspan="1">Additional Comments<br />(<span id="leftchar">5000</span>&nbsp;Char)</td>
<td align="" valign="top"><asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="15" Columns="80" TabIndex="19"></asp:TextBox><%--<textarea class="textForm" rows="8" cols="60" name="Comment" tabindex="13"></textarea>--%></td>
</tr>
<tr>
<td align="left" colspan="1" valign="top">
Verification Code <span style="color: crimson">*<br>
</span><span class="text">(Verification Code is Case Sensitive)</span>
</td>
<td valign="top">
<img alt="Verification Image" src="JpegImage.aspx" />
</td>
</tr>
<tr>
<td align="left" colspan="1" valign="top">
</td>
<td valign="top">
<asp:TextBox ID="txtAccessCode" runat="server" Width="160" TabIndex="20"></asp:TextBox>&nbsp;<asp:Label
ID="lblMes" runat="server" Visible="false" CssClass="redheading1" Font-Bold="true" EnableViewState="false"></asp:Label>
<asp:RequiredFieldValidator ID="reqaccess" runat="server" ControlToValidate="txtAccessCode"
Display="None" ErrorMessage="Access Code : Access Code is a required field.Please enter Access Code as shown in the box above."
SetFocusOnError="True" ValidationGroup="req"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td align="left" valign="top" colspan="1">&nbsp;</td>
<td align="" valign="top"><asp:ImageButton ID="ImgbtnSubmit" runat="server" width="73" height="25" AlternateText="Submit" ValidationGroup="req" OnClick="ImgbtnSubmit_Click" ImageUrl="~/images/submit.gif" TabIndex="21" /></td>
</tr>
</table>
  <br />
</div>
</td>
</tr>
</table></td>
<td width="5"><img src="images/space.gif" width="1" height="1" /></td>
</tr>
</table>
</td>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="req" />
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
</table>
</asp:Panel>
</asp:Content>

