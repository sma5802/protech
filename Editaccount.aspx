<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="Editaccount" Codebehind="Editaccount.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function addLoadEvent(func)
 {  
 var oldonload = window.onload;
  
  if (typeof window.onload != 'function')
   {
     window.onload = func;
    
  } 
  else
   {
      window.onload = function()
     {
    
      if (oldonload)
       {
         divtest();
        oldonload();
      }
 
      func();
      if(oldonload)
      {
     divtest();
        oldonload();
      }
    }
  }
}
addLoadEvent(function() 
{
     divtest();
//drponch();
});
function divtest()
{

	var cboState = "";
	var txtoth = "";
	
	    cboState = document.getElementById("ctl00_ContentPlaceHolder1_ddlState");
	    txtoth = document.getElementById("ctl00_ContentPlaceHolder1_txtOther");
  if(document.getElementById('<%= ddlCountry.ClientID%>').value =="0"||document.getElementById('<%= ddlCountry.ClientID%>').value =="254")
  {
  
                    cboState.style.display='block';
                    txtoth.style.display  = 'none';
  }
  else
  {
                    cboState.style.display='none';
                    txtoth.style.display  = 'block';
  }
}
</script>
<script language="javascript" type="text/javascript">
function GetXmlHttpObjectctry()
{ 
	var objXMLHttp=null;
	if (window.XMLHttpRequest)
		objXMLHttp=new XMLHttpRequest();

	else if (window.ActiveXObject)
		objXMLHttp=new ActiveXObject("Microsoft.XMLHTTP");

	return objXMLHttp;
} 

function FillCbocstate(gettype)
{
//alert("dsf");
    //document.getElementById("waitingDiv").innerHTML="<img src='images/loader.gif'>";
	xmlHttp=GetXmlHttpObjectctry();
	if (xmlHttp==null)
	{
		alert ("Browser does not support HTTP Request");
		return;
	} 
	var cboCountry = "";
	var cboState = "";
	var txtoth = "";
	var url = "";
	if(gettype == "1")
	{
	    cboCountry = document.getElementById("ctl00_ContentPlaceHolder1_ddlCountry");
	    cboState = document.getElementById("ctl00_ContentPlaceHolder1_ddlState");
	    txtoth = document.getElementById("ctl00_ContentPlaceHolder1_txtOther");
	    //alert(cboCountry.value);
	    
	    url = "asp/getsate.aspx?ctryid="+cboCountry.value;	
	     divtest();
	}

	
	xmlHttp.onreadystatechange=function Styles() { 
		if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
		{ 
		 
		    var xmlResponse = xmlHttp.responseText;
		    //alert(xmlResponse);
		    var arrResponse = xmlResponse.split('|');			
			cboState.options.length = 0;			

	        var opt = document.createElement('OPTION');
		    opt.value = "";
		    if(gettype == "1")
		        { opt.value = "0";
		          opt.text = "Select State";
		        }
		    //else
		      //  opt.text = " --Select Model-- ";
		    cboState.options.add(opt,0);
		    cboState.options[0].selected = true;
            if(xmlResponse == "####")
            {
                if(gettype == "1")
                   {
                     //cboState.style.display='none';
                     //txtoth.style.display = 'block';
                    
                   }
         
            }
            else
            {    
                if(gettype == "1")
                {
                    //cboState.style.display='block';
                    //txtoth.style.display  = 'none';
                  
                }
              
			    for (var i=0; i < arrResponse.length-1; i++)
			    {
				    var opt = document.createElement('OPTION');
				      var arrRes = arrResponse[i].split(',');	
				      //alert(arrRes[0]+" "+arrRes[1]);
			        opt.value = arrRes[0];
				    opt.text = arrRes[1];
				    cboState.options.add(opt);
			    }
			}
			
			
	}
	}
	xmlHttp.open("GET",url,true);
	xmlHttp.send(null);
} 

function validatestate(sender,args)
{
    args.IsValid =  true;
    if(document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').value=='254')
    {
        if(document.getElementById('ctl00_ContentPlaceHolder1_ddlState').value== "0")
        {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlState').focus();
            sender.errormessage="Please Select State";
            args.IsValid =  false;
        }
    }
    else
    {
        if(document.getElementById('ctl00_ContentPlaceHolder1_txtOther').value== "")
        {
            document.getElementById('ctl00_ContentPlaceHolder1_txtOther').focus();
            sender.errormessage="Please Enter State";
            args.IsValid =  false;
        }
    }
}
</script>
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top">
<table width="100%" align="left" border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td width="10" style="height: 10px"></td>
<td style="width: 11px; height: 10px"></td>
</tr>
<tr>
<td height="108" valign="top">
<table border="0" cellspacing="0" cellpadding="2" width= "100%" class="text">
<tr>
<td colspan="4">
<strong class="title">
VIEW / EDIT DETAILS</strong>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href="UserAccount.aspx" class="bluelink">Back</a><br /><br />
<asp:Label ID="lblmsg" runat="server" ForeColor="red" Font-Bold="true" Visible="false" EnableViewState="false"></asp:Label>

</td>
</tr>
<tr>
<td width="130" rowspan="1" align="left">First Name: <span style="color: crimson">*</span></td>
<td width="150" rowspan="1">
<asp:TextBox ID="txtFName" runat="server" size="29" TabIndex="4" MaxLength="50" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqfirstname" ValidationGroup="reg" runat="server" ControlToValidate="txtFName" Display="None" ErrorMessage="Please Enter First name" ></asp:RequiredFieldValidator>

</td>
<td width="130" rowspan="1" align="left"></td>
<td rowspan="1">
</td>
</tr>
<tr>
<td width="130" rowspan="1" align="left"> Last Name: <span style="color: crimson">*</span></td>
<td width="150" rowspan="1">
<asp:TextBox ID="txtLName" runat="server" size="29" TabIndex="5" MaxLength="50" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="reg" runat="server" ControlToValidate="txtLName" Display="None" ErrorMessage="Please Enter Last name" ></asp:RequiredFieldValidator>
</td>
<td width="130" rowspan="1" align="left"></td>
<td rowspan="1">
</td>
</tr>
<tr>
<td align="left" rowspan="1" width="130">
Company: <span style="color: crimson">*</span></td>
<td rowspan="1" width="150">
<asp:TextBox ID="txtCompany" runat="server" size="29" TabIndex="6" MaxLength="200" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqComp" ValidationGroup="reg" runat="server" ControlToValidate="txtCompany" Display="None" ErrorMessage="Please Enter Company" ></asp:RequiredFieldValidator></td>
<td align="left" rowspan="1" width="130">
</td>
<td rowspan="1">
&nbsp;
</td>
</tr>
<tr>
<td align="left" rowspan="1"> E-mail: <span style="color: crimson">*</span></td>
<td rowspan="1"><asp:TextBox ID="txtEmail" runat="server" size="29" TabIndex="7" MaxLength="100" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="reg" runat="server" ControlToValidate="txtEmail" Display="None" ErrorMessage="Please Enter EMail Address" ></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="None" ValidationGroup="regs" ControlToValidate="txtEmail" ErrorMessage="Please Enter Correct Email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
<td align="left" rowspan="1"><span style="color: crimson"></span>
</td>
<td rowspan="1">
&nbsp;&nbsp;
<%--<asp:RegularExpressionValidator id="Regvalidemail" runat="server" ValidationGroup="reg" SetFocusOnError="True" ErrorMessage="The Email Address you entered is wrong .Please enter a valid Email Address" Display="None" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)<span style="color: crimson">*</span>@\w+([-.]\w+)<span style="color: crimson">*</span>\.\w+([-.]\w+)<span style="color: crimson">*</span>"></asp:RegularExpressionValidator>--%>

</td>
</tr>
<tr>
<td align="left" rowspan="1">
Phone:<span style="color: crimson">*</span>
</td>
<td rowspan="1">
<asp:TextBox ID="txtPhone" runat="server" size="29" TabIndex="8" MaxLength="15" CssClass="textForm"></asp:TextBox>
 <asp:RequiredFieldValidator ID="reph" runat="server" ControlToValidate="txtPhone"
                  Display="None" ErrorMessage="Please Enter Phone No" SetFocusOnError="True" ValidationGroup="reg"></asp:RequiredFieldValidator>
        <%-- <asp:RegularExpressionValidator ID="cuph" runat="server" ControlToValidate="txtPhone"
                  Display="None" ErrorMessage="Enter Phoneno in Proper format" SetFocusOnError="True"
                  ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ValidationGroup="reg"></asp:RegularExpressionValidator>--%> 
</td>
<td align="left" rowspan="1">
</td>
<td rowspan="1">
</td>
</tr>
<tr>
<td align="left" rowspan="1"> Fax:</td>
<td rowspan="1">
<asp:TextBox ID="txtFax" runat="server" size="29" TabIndex="8" MaxLength="15" CssClass="textForm"></asp:TextBox><%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Phone Number"
SetFocusOnError="True" ValidationExpression="\d{10}" ValidationGroup="reg" ControlToValidate="txtPhone" Display="None"></asp:RegularExpressionValidator>--%></td>
<td align="left" rowspan="1"> &nbsp;</td>
<td rowspan="1">
&nbsp;<%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Fax Number of 10 digit"
SetFocusOnError="True" ValidationExpression="\d{10}" ValidationGroup="reg" ControlToValidate="txtFax" Display="None"></asp:RegularExpressionValidator>--%></td>
</tr>
<tr>
<td align="left" rowspan="1">Street 1: <span style="color: crimson">*</span>
</td>
<td rowspan="1"><asp:TextBox ID="txtStreet1" runat="server" size="29" TabIndex="9" MaxLength="200" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="reg" runat="server" ControlToValidate="txtStreet1" Display="None" ErrorMessage="Please Enter Street1 Address" ></asp:RequiredFieldValidator>
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
<asp:TextBox ID="txtCity" runat="server" size="29" TabIndex="11" MaxLength="50" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="reg" runat="server" ControlToValidate="txtCity" Display="None" ErrorMessage="Please Enter City Address" ></asp:RequiredFieldValidator></td>
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
<asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
<ContentTemplate >
<%--onchange="FillCbocstate(1);"  --%>
<asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="True"  Width="145px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True" DataSourceID="srccountry" DataTextField="Country" DataValueField="ID"  >
 <asp:ListItem Text="Select Country" Value="0"></asp:ListItem>
</asp:DropDownList>
<asp:SqlDataSource ID="srccountry" runat="server" ConnectionString="<%$ ConnectionStrings:dbConnect %>"
SelectCommand = "SELECT id, Country, isactive FROM pep$tech$corp.peptech_country where isactive=1 order by country">
</asp:SqlDataSource>
</ContentTemplate>

</asp:UpdatePanel>
<asp:RequiredFieldValidator ID="reqcounrty" ValidationGroup="reg" runat="server" ControlToValidate="ddlcountry" Display="None" ErrorMessage="Please Select Country  name" SetFocusOnError="True" InitialValue="0" ></asp:RequiredFieldValidator>
</td>
<td align="left" rowspan="1">
</td>
<td rowspan="1">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="reg" />
</td>
</tr>

<tr>
<td align="left" rowspan="1" valign="top">
State/Province: <span style="color: crimson">*</span>
</td>
<td><%-- OnSelectedIndexChanged="ddlState_SelectedIndexChanged1" AutoPostBack="True"--%>
<asp:UpdatePanel id="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
<ContentTemplate >
<asp:DropDownList ID="ddlState" runat="server" TabIndex="12" 
        AppendDataBoundItems="True" DataTextField="state" DataValueField="id" 
        DataSourceID="srcState"  >
<asp:ListItem Text="Select State" Value="0"></asp:ListItem>
</asp:DropDownList>
    <asp:SqlDataSource ID="srcState" runat="server" 
        ConnectionString="<%$ ConnectionStrings:dbConnect %>" 
        SelectCommand="SELECT * FROM pep$tech$corp.Peptech_State WHERE isactive=1 AND CountryID=@CountryID ORDER BY State">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCountry" Name="CountryID" 
                PropertyName="SelectedValue" Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
<asp:TextBox ID="txtOther" runat="server" CssClass="textForm" size="29" tabindex="13" MaxLength="50"></asp:TextBox>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
<div style="position:absolute; z-index:1; left: 45%;top: 138%;">
<img src="images/ajax-loader.gif" alt="Load Progress" border="0" />
</div>
</ProgressTemplate>
</asp:UpdateProgress>
<%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="reg" runat="server" ControlToValidate="ddlState" Display="None" ErrorMessage="Please Select State" ></asp:RequiredFieldValidator>--%>
<asp:CustomValidator id="cstmstate" runat="server" ValidationGroup="reg" SetFocusOnError="True" ErrorMessage="Please Enter State name" Display="None" ClientValidationFunction="validatestate"></asp:CustomValidator>
</td>
<td>
</td>
</tr>

<tr>
<td align="left" rowspan="1">Zip/Postal Code: <span style="color: crimson">*</span>
</td>
<td rowspan="1">
<asp:TextBox ID="txtZip" runat="server" size="29" TabIndex="14" MaxLength="10" CssClass="textForm"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="reg" runat="server" ControlToValidate="txtZip" Display="None" ErrorMessage="Please Enter  Zip/Postal Code" ></asp:RequiredFieldValidator>
</td>
<td align="left" rowspan="1"> 
    <asp:LinkButton ID="LinkButton2" runat="server" BorderStyle="None" OnClick="LinkButton2_Click"
        ValidationGroup="reg"><img src="images/submit.gif" alt="Submit" width="73" height="25" hspace="5" border="0" /></asp:LinkButton></td>
<td rowspan="1">
&nbsp;</td>
</tr>

</table>
</td>
</tr>
<tr align="left" valign="top">
<td></td>
<td style="width: 11px"></td>
</tr>
</table>
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