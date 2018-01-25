<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true"  EnableEventValidation="false" MaintainScrollPositionOnPostback="true" Inherits="Registration" Codebehind="Registration.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function chkusername()
{
var txtusrname=document.getElementById('<%= txtUsername.ClientID%>'); 
if(txtusrname.value.length==0)
{
alert('Please enter your Username');   
return false;
}
}




//*****************Email*********
function ExecuteCall(objEmail)
{
tempEmail = objEmail.value;
if (tempEmail=='')
{ 
//alert("Please enter your Email-ID");
//objEmail.focus();
return;
}

xmlHttp=GetXmlHttpObject();
if (xmlHttp==null)
{
alert ("Browser does not support HTTP Request");
return;
}

var url="checkmail.aspx";
url=url+"?Email=" + tempEmail;
xmlHttp.onreadystatechange=function CEmail()
{
if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
{ 
var xmlResponse = xmlHttp.responseText;
//alert(xmlResponse);
if(xmlResponse.indexOf("user exist")!=-1)
{
//objEmail.focus();
document.getElementById("lblDuplicateAccountCheck").innerHTML= "<span style=\"color: red; font-weight: bold;\"><br>Username already exists.</span>";
}
else
document.getElementById("lblDuplicateAccountCheck").innerHTML= "<span style=\"color: blue; font-weight: bold;\"><br>Username available for you.</span>";
} 
}

xmlHttp.open("GET",url,true);
xmlHttp.send(null);
}
</script>
<script language="javascript" type="text/javascript">

function validatestate(sender,args)
{//alert("state");
if(document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').value=="254")
 {
 //alert(document.getElementById('ctl00_ContentPlaceHolder1_ddlState').value);
   if(document.getElementById('ctl00_ContentPlaceHolder1_ddlState').value== "0")
     {
       sender.errormessage="Please Select State";
       document.getElementById('ctl00_ContentPlaceHolder1_ddlState').focus();
       args.IsValid =  false;
     }
   else
    {
       args.IsValid =  true;
    }
  }
  else
  {
  if(document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').value!="0")
  {
  //alert(document.getElementById('ctl00_ContentPlaceHolder1_txtOther').value);
  if(document.getElementById('ctl00_ContentPlaceHolder1_txtOther').value== "")
     {
        document.getElementById('ctl00_ContentPlaceHolder1_txtOther').focus();
        sender.errormessage="Please Enter State Name";
        args.IsValid =  false;
     }
   else
    {
        args.IsValid =  true;
    }
    }
  }
 
}
</script>
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><br />
<asp:Label ID="lblMsg" runat="server" Visible="false" EnableViewState="false" Font-Bold="true" ForeColor="red"></asp:Label>
<table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top"><div align="justify"><span class="blueheading">New Customer Account</span><br />
<br />
Please Fill The Below Form and become the member of the site
<br />
Required information is marked with an asterisk (<span style="color: crimson">*</span>). <br />
<br />
<span class="blueheading">Account Information </span>
<br />
<br />
<table width="100%"  border="0" cellpadding="2" cellspacing="0" class="text">
<tr><td></td><td colspan="5"><asp:Label ID="lblusercheck" runat="server" CssClass="redheading1"></asp:Label></td></tr>
<tr>
<td width="130">Username: <span style="color: crimson">*</span></td>
<td width="250"><asp:TextBox ID="txtUsername" runat="server" size="29"  
        TabIndex="1" CssClass="textForm" MaxLength="50"></asp:TextBox><label id="lblDuplicateAccountCheck"></label>
<%--<input type="text" class="textForm" name="FirstName" size="29" value="" tabindex="1" />--%></td>
<td width="130"><span style="color: crimson">
</span></td>
<td>
&nbsp;&nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtUsername" Display="None" 
        ErrorMessage="Please enter Username" SetFocusOnError="True" ></asp:RequiredFieldValidator></td>
<td></td>
<td>
&nbsp;&nbsp;
</td>

</tr>

<tr>
<td width="130">
Password: <span style="color: crimson">*</span></td>
<td width="150">
<asp:TextBox ID="txtPassword" runat="server" size="29"  TabIndex="2" 
        TextMode="Password" CssClass="textForm" MaxLength="50"></asp:TextBox></td>
<td width="130">
<asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtPassword" Display="None" 
        ErrorMessage="Please enter Password" SetFocusOnError="True" ></asp:RequiredFieldValidator></td>
<td>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
        ControlToValidate="txtPassword" Display="None" 
        ErrorMessage="Password must contain atleast 4 characters" 
        SetFocusOnError="True" ValidationExpression=".{4,50}" ValidationGroup="reg"></asp:RegularExpressionValidator>
</td>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td width="130">
Retype Password: <span style="color: crimson">*</span></td>
<td width="150">
<asp:TextBox ID="txtRePassowrd" runat="server" size="29"  TabIndex="3" 
        TextMode="Password" CssClass="textForm" MaxLength="50"></asp:TextBox></td>
<td width="130">
<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtRePassowrd" Display="None" 
        ErrorMessage="Please enter Retype Password" SetFocusOnError="True" ></asp:RequiredFieldValidator></td>
<td>
</td>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td width="130">
</td>
<td width="150">
</td>
<td width="130">
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
ControlToValidate="txtRePassowrd" Display="None" ErrorMessage="Passwords and Retype Password do not match"
ValidationGroup="reg" SetFocusOnError="True"></asp:CompareValidator></td>
<td>
</td>
<td>
</td>
<td>
</td>
</tr>
</table>
<br />
<span class="blueheading">Contact Information </span>
<br />
<br />
<table border="0" cellspacing="0" cellpadding="2" width= "100%" class="text">
<tr>
<td width="130" rowspan="1" align="left">First Name: <span style="color: crimson">*</span></td>
<td width="150" rowspan="1">
<asp:TextBox ID="txtFName" runat="server" size="29" TabIndex="4" MaxLength="50" 
        CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqfirstname" ValidationGroup="reg" runat="server" 
        ControlToValidate="txtFName" Display="None" 
        ErrorMessage="Please Enter First name" SetFocusOnError="True" ></asp:RequiredFieldValidator>

</td>
<td align="left" rowspan="1" width="130">
</td>
<td rowspan="1">
&nbsp;
</td>
</tr>
<tr>
<td width="130" rowspan="1" align="left"> Last Name: <span style="color: crimson">*</span></td>
<td rowspan="1"><asp:TextBox ID="txtLName" runat="server" size="29" TabIndex="5" 
        MaxLength="50" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtLName" Display="None" 
        ErrorMessage="Please Enter Last name" SetFocusOnError="True" ></asp:RequiredFieldValidator>
</td>
<td align="left" rowspan="1" width="130">
</td>
<td rowspan="1">
&nbsp;
</td>
</tr>
<tr>
<td align="left" rowspan="1" width="130">
Company: <span style="color: crimson">*</span></td>
<td rowspan="1" width="150">
<asp:TextBox ID="txtCompany" runat="server" size="29" TabIndex="6" MaxLength="200" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqComp" ValidationGroup="reg" runat="server" 
        ControlToValidate="txtCompany" Display="None" 
        ErrorMessage="Please Enter Company" SetFocusOnError="True" ></asp:RequiredFieldValidator></td>
<td align="left" rowspan="1" width="130">
</td>
<td rowspan="1">
&nbsp;
</td>
</tr>
<tr>
<td align="left" rowspan="1"> E-mail: <span style="color: crimson">*</span></td>
<td rowspan="1"><asp:TextBox ID="txtEmail" runat="server" size="29" TabIndex="7" MaxLength="200" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtEmail" Display="None" 
        ErrorMessage="Please Enter EMail Address" SetFocusOnError="True" ></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" 
        Display="None" ValidationGroup="reg" ControlToValidate="txtEmail" 
        ErrorMessage="Please Enter Correct Email address" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
        SetFocusOnError="True"></asp:RegularExpressionValidator></td>
<td align="left" rowspan="1"><span style="color: crimson"></span>
</td>
<td rowspan="1">
&nbsp;&nbsp;
<%--<asp:RegularExpressionValidator id="Regvalidemail" runat="server" ValidationGroup="reg" SetFocusOnError="True" ErrorMessage="The Email Address you entered is wrong .Please enter a valid Email Address" Display="None" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)<span style="color: crimson">*</span>@\w+([-.]\w+)<span style="color: crimson">*</span>\.\w+([-.]\w+)<span style="color: crimson">*</span>"></asp:RegularExpressionValidator>--%>

</td>
</tr>
<tr>
<td align="left" rowspan="1">
    Phone: <span style="color: crimson">*</span>
</td>
<td rowspan="1">
<asp:TextBox ID="txtPhone" runat="server" size="29" TabIndex="8" MaxLength="16" 
        CssClass="textForm"></asp:TextBox>
 <asp:RequiredFieldValidator ID="reph" runat="server" ControlToValidate="txtPhone"
                  Display="None" ErrorMessage="Please Enter Phone No" SetFocusOnError="True" ValidationGroup="reg"></asp:RequiredFieldValidator>
            <%--  <asp:RegularExpressionValidator ID="cuph" runat="server" ControlToValidate="txtPhone"
                  Display="None" ErrorMessage="Enter Phoneno in Proper format" SetFocusOnError="True"
                  ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ValidationGroup="reg"></asp:RegularExpressionValidator>--%>
                  <%--<asp:RegularExpressionValidator 
        ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhone" 
        Display="None" ErrorMessage="Alphabets are not allowed in Phone number" 
        SetFocusOnError="True" ValidationExpression="[^a-zA-Z]*" ValidationGroup="reg"></asp:RegularExpressionValidator>--%>
</td>
<td align="left" rowspan="1">
</td>
<td rowspan="1">
</td>
</tr>
<tr>
<td align="left" rowspan="1" style="height: 28px"> Fax:</td>
<td rowspan="1" style="height: 28px">
<asp:TextBox ID="txtFax" runat="server" size="29" TabIndex="8" MaxLength="16" 
        CssClass="textForm"></asp:TextBox><%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Phone Number"
SetFocusOnError="True" ValidationExpression="\d{10}" ValidationGroup="reg" ControlToValidate="txtPhone" Display="None"></asp:RegularExpressionValidator>--%>
<%--<asp:RegularExpressionValidator 
        ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtFax" 
        Display="None" ErrorMessage="Alphabets are not allowed in Fax" 
        SetFocusOnError="True" ValidationExpression="[^a-zA-Z]*" ValidationGroup="reg"></asp:RegularExpressionValidator>--%>
    </td>
<td align="left" rowspan="1" style="height: 28px"> &nbsp;</td>
<td rowspan="1" style="height: 28px">
&nbsp;<%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Fax Number of 10 digit"
SetFocusOnError="True" ValidationExpression="\d{10}" ValidationGroup="reg" ControlToValidate="txtFax" Display="None"></asp:RegularExpressionValidator>--%></td>
</tr>
<tr>
<td align="left" rowspan="1">Street 1: <span style="color: crimson">*</span>
</td>
<td rowspan="1"><asp:TextBox ID="txtStreet1" runat="server" size="29" TabIndex="9" MaxLength="200" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtStreet1" Display="None" 
        ErrorMessage="Please Enter Street1 Address" SetFocusOnError="True" ></asp:RequiredFieldValidator>
</td>
<td align="left" rowspan="1">
</td>
<td rowspan="1">
&nbsp;</td>
</tr>
<tr>
<td align="left" rowspan="1">
Street 2:
</td>
<td rowspan="1">
<asp:TextBox ID="txtStreet2" runat="server" size="29" TabIndex="10" MaxLength="200" CssClass="textForm"></asp:TextBox></td>
<td align="left" rowspan="1">
</td>
<td rowspan="1">
</td>
</tr>
<tr>
<td align="left" rowspan="1">
City: <span style="color: crimson">*</span>
</td>
<td rowspan="1">
<asp:TextBox ID="txtCity" runat="server" size="29" TabIndex="11" MaxLength="200" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtCity" Display="None" 
        ErrorMessage="Please Enter City Address" SetFocusOnError="True" ></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
        ControlToValidate="txtCity" Display="None" 
        ErrorMessage="Special characters &amp; numerals are not allowed for City" 
        SetFocusOnError="True" ValidationExpression="[a-zA-Z ]*" 
        ValidationGroup="reg"></asp:RegularExpressionValidator>
    </td>
<td align="left" rowspan="1">
&nbsp;</td>
<td rowspan="1">
&nbsp;
</td>
</tr>

<tr>
<td align="left" rowspan="1">
Country <span style="color: crimson">*</span></td>
<td rowspan="1">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
 <ContentTemplate>

 <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="True"  
        Width="145px" DataSourceID="srccountry" DataTextField="Country" 
        DataValueField="ID"   TabIndex="12" 
        AutoPostBack="True" onselectedindexchanged="ddlCountry_SelectedIndexChanged1">
 <asp:ListItem Text="Select Country" Value="0"></asp:ListItem>
</asp:DropDownList>

<asp:SqlDataSource ID="srccountry" runat="server" ConnectionString="<%$ ConnectionStrings:dbConnect %>"
SelectCommand = "SELECT id, Country, isactive FROM pep$tech$corp.peptech_country where isactive=1 order by country">
</asp:SqlDataSource>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
<asp:Image ID="Image11" runat="server" ImageUrl="~/images/loading.gif" />
</ProgressTemplate>
</asp:UpdateProgress>
<asp:RequiredFieldValidator ID="reqcounrty" ValidationGroup="reg" runat="server" 
        ControlToValidate="ddlcountry" Display="None" 
        ErrorMessage="Please Select Country name" SetFocusOnError="True"  
        InitialValue="0"></asp:RequiredFieldValidator>
</td>
<td align="left" rowspan="1">
</td>
<td rowspan="1">
</td>
</tr>

<tr>
<td align="left" rowspan="1" valign="top">
State/Province: <span style="color: crimson">*</span>
</td>
<td><%-- OnSelectedIndexChanged="ddlState_SelectedIndexChanged1" --%>
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
 <ContentTemplate>

<asp:DropDownList ID="ddlState" runat="server" TabIndex="13" AppendDataBoundItems="True">
<asp:ListItem Text="Select State" Value="0"></asp:ListItem>
</asp:DropDownList>
<asp:TextBox ID="txtOther" runat="server" CssClass="textForm" size="29" 
        tabindex="13" Visible="false" MaxLength="50"></asp:TextBox>
<%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="reg" runat="server" ControlToValidate="ddlState" Display="None" ErrorMessage="Please Select State"></asp:RequiredFieldValidator>--%>

</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="ddlcountry" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
<ProgressTemplate>
<asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading.gif" />
</ProgressTemplate>
</asp:UpdateProgress>
<asp:CustomValidator id="cstmstate" runat="server" ValidationGroup="reg" SetFocusOnError="True" ErrorMessage="Please Enter State name" Display="None" ClientValidationFunction="validatestate"></asp:CustomValidator>
</td>
<td>
</td>
</tr>

<tr>
<td align="left" rowspan="1">Zip/Postal Code: <span style="color: crimson">*</span>
</td>
<td rowspan="1">
<asp:TextBox ID="txtZip" runat="server" size="29" TabIndex="14" MaxLength="10" 
        CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="reg" 
        runat="server" ControlToValidate="txtZip" Display="None" 
        ErrorMessage="Please Enter  Zip/Postal Code" SetFocusOnError="True" ></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
        ControlToValidate="txtZip" Display="None" 
        ErrorMessage="Zip contain atleast 5 characters and only alphanumeric characters are allowed" 
        SetFocusOnError="True" ValidationExpression="[0-9A-Za-z]{5,}" 
        ValidationGroup="reg"></asp:RegularExpressionValidator>
</td>
<td align="left" rowspan="1"> </td>
<td rowspan="1">
&nbsp;</td>
</tr>
    <tr>
        <td align="left" rowspan="1">
          
                Purchase Orders:
        </td>
        <td rowspan="1" >
            <asp:CheckBox ID="chkorderno" runat="server" TabIndex="15" Text="I would like to be approved to use purchase orders on peptechcorp.com."  Font-Size="11px" /></td>
        <td align="left" rowspan="1">
        </td>
        <td rowspan="1">
        </td>
    </tr>

</table>
<br />

<asp:LinkButton ID="LinkButton2" runat="server" BorderStyle="None" OnClick="LinkButton2_Click" ValidationGroup="reg" TabIndex="16"><img src="images/submit.gif" alt="Submit" width="73" height="25" hspace="5" border="0" /></asp:LinkButton>
<asp:LinkButton ID="LinkButton1" runat="server" BorderStyle="None" OnClick="LinkButton1_Click" TabIndex="17"><img src="images/cancel.gif" alt="Cancel" width="75" height="25" border="0" /></asp:LinkButton><br />
                      <br />
                      <br />
                       <asp:ValidationSummary ID="ValidationSummary1" 
        runat="server" ValidationGroup="reg" ShowMessageBox="True" 
        ShowSummary="False" />
</td>
</tr>
</table></td>
<td width="5"><img src="images/space.gif" width="1" height="1" /></td>
</tr>
</table>
</td>
<td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
</tr>
</table>
</asp:Content>

