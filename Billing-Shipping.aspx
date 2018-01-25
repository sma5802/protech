<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" Inherits="Billing_Shipping" Codebehind="Billing-Shipping.aspx.cs" %>
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
        oldonload();
      }
 
      func();
      if(oldonload)
      {divtest();
        oldonload();
      }
    }
  }
}
addLoadEvent(function() 
{divtest();

//drponch();
});
function divtest()
{
   var pnl = document.getElementById('<%= pnlFedexAccountnumber.ClientID %>');

               if(saveref.pr=="0"||saveref.pr=="0.0"||saveref.pr==null)
                   {
                     pnl.style.display = 'none';
                   }
               else
                  {
                          pnl.style.display = 'none';
                        }
}
</script>
    <script language="javascript" type="text/javascript" >
var x = null;
var pr = null;
saveref.x=null;
saveref.pr = -1;
function saveref(old,price)
          {
          
          var pnl = document.getElementById('<%= pnlFedexAccountnumber.ClientID %>');
             if(saveref.x!=null) 
                  { 
                    if(saveref.x!=old)
                      { 
                     
                        saveref.x.checked = false;
                        saveref.x = old;
                        saveref.pr = price;
                 
                        if(saveref.pr=="0"||saveref.pr=="0.0")
                        {
                       
                          pnl.style.display = 'block';
                        }
                        else
                        {
                        
                          pnl.style.display = 'none';
                        }
                      }
                  }
                 else
                  {
                       saveref.x = old;
                       saveref.pr = price;
                    
                        if(saveref.pr=="0"||saveref.pr=="0.0")
                        {
                          pnl.style.display = 'block';
                        }
                        else
                        {
                          pnl.style.display = 'none';
                        }
                  }
                      
}

function toggleMenu(e1,price)
{
  //alert(price);
  
    e1.checked = true;
    
           // var val12=document.getElementById('<%=lblShipping.ClientID %>');
           //     val12.innerHTML = price;
	saveref(e1,price);
	 
  
}
function checkship()
{
    if(saveref.x==null)
    {
        alert("Check the shipping charge Option above")
        var panl = document.getElementById('<%= pnlFedexAccountnumber.ClientID %>');
        panl.focus();
        return false;
    }
    else
    {
        if(saveref.pr==0)
        {
            var fed=document.getElementById('<%= txtfedex.ClientID %>');
            if(fed.value.length==0)
            {
                alert('We charge a flat shipping fee regardless of package size and order value. We will not charge any shipping fees if you can provide us with your FedEx account number.');
                fed.focus();
                return false;
            }
            else
            {
                digitCheck = /^\d+$/; 
                if(digitCheck.test(fed.value) && fed.value.length==9)
                {
                    return true;                    
                } 
                else 
                {
                    alert('Please enter a valid FedEx account number. It is nine-digit number with no letters, special characters, or spaces.');
                    fed.focus();
                    return false;
                }
            }
        }
    }
}
</script>
<script language="javascript" type="text/javascript">
function chkBilling(sender,args)
{
    args.IsValid=true;
    var dcountry=document.getElementById('<%=ddlcountry.ClientID %>');
    var dstate=document.getElementById('<%=ddlstate.ClientID %>');
    var tstate=document.getElementById('<%=txtBillotherstate.ClientID %>');
    if(dcountry.value=="0")
    {
        sender.errormessage='Please Select State';
        dstate.focus();
        args.IsValid=false;
    }
    else if(dcountry.value=="254")
    {
        if(dstate.value=="Select State")
        {
             sender.errormessage='Please Select State';
             dstate.focus();
             args.IsValid=false;
        }
    }
    else
    {
        if(tstate.value=="")
        {
            sender.errormessage='Please Enter State';
            tstate.focus();
            args.IsValid=false;
        }        
    }
}



function chkShipping(sender,args)
{
    args.IsValid=true;
    var ddcountry=document.getElementById('<%=ddlcountry1.ClientID %>');
    var ddstate=document.getElementById('<%=ddlstate1.ClientID %>');
    var txstate=document.getElementById('<%=txtshipotherstate.ClientID %>');
    if(ddcountry.value=="0")
    {
        sender.errormessage='Please Select State in Shipping Address';
        ddstate.focus();
        args.IsValid=false;
    }
    else if(ddcountry.value=="254")
    {
        if(ddstate.value=="Select State")
        {
             sender.errormessage='Please Select State in Shipping Address';
             ddstate.focus();
             args.IsValid=false;
        }
    }
    else
    {
        if(txstate.value=="")
        {
            sender.errormessage='Please Enter State in Shipping Address';
            txstate.focus();
            args.IsValid=false;
        }        
    }
}
</script>


    <asp:Panel ID="Panelbill" runat="server" DefaultButton="lnkCheckout">
        <table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
            <tr>
                <td width="17" align="left" valign="top" style="background-image: url(images/main-lbg.gif)">
                    <img src="images/space.gif" alt="space" width="1" height="1" />
                </td>
                <td align="left" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="text">
                        <tr align="left" valign="top">
                            <td>
                                <table width="100%" border="0" cellpadding="10" cellspacing="0" class="text">
                                    <tr>
                                        <td align="left" valign="top">
                                            <table width="100%" border="0" cellpadding="10" cellspacing="0" class="text">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <strong class="title">BILLING & SHIPPING ADDRESS</strong><br />
                                                        <br />
                                                        <strong>BILLING ADDRESS</strong><br />
                                                        <br />
                                                        <table width="100%" border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1"
                                                            class="text">
                                                            <tr>
                                                                <td bgcolor="#F9F8F8">
                                                                    <table width="100%" border="0" cellpadding="3" cellspacing="0" class="text">
                                                                        <tr bgcolor="#F1F0F0">
                                                                            <td colspan="2">
                                                                                <strong>Please enter your name and address exactly as they appear on your credit card
                                                                                    statement. </strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="180">
                                                                                &nbsp;&nbsp;First Name:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqfirstname" ValidationGroup="reg" runat="server"
                                                                                    ControlToValidate="txtFirstName" Display="None" ErrorMessage="Please Enter First name"
                                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr bgcolor="#F1F0F0">
                                                                            <td>
                                                                                &nbsp;&nbsp;Last Name:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqlastname" ValidationGroup="reg" runat="server"
                                                                                    ControlToValidate="txtLastName" Display="None" ErrorMessage="Please Enter Last name"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;Street Address1:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtStreetaddress" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqaddree" ValidationGroup="reg" runat="server" ControlToValidate="txtStreetaddress"
                                                                                    Display="None" ErrorMessage="Please Enter Address" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;Street Address2
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtStreetaddress2" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;Country:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <%--<asp:UpdatePanel ID="uplbilling" runat="server">
                                                                                    <contenttemplate>--%>
                                                                                        <asp:DropDownList ID="ddlcountry" runat="server" AppendDataBoundItems="True"  Width="150px" OnDataBound="ddlcountry_DataBound" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" DataSourceID="srccountry" DataTextField="Country" DataValueField="ID">
                                                                                            <asp:ListItem Text="Select Country" Value="0"></asp:ListItem>
                                                                                        </asp:DropDownList>

                                                                                        <asp:SqlDataSource ID="srccountry" runat="server" ConnectionString="<%$ ConnectionStrings:dbConnect %>"
                                                                                            SelectCommand = "SELECT id, Country, isactive FROM pep$tech$corp.peptech_country where isactive=1 order by country">
                                                                                        </asp:SqlDataSource>
                                                                                   <%-- </contenttemplate>
                                                                                </asp:UpdatePanel>--%>
                                                                                <asp:RequiredFieldValidator ID="reqcounrty" ValidationGroup="reg" runat="server"
                                                                                    ControlToValidate="ddlcountry" Display="None" InitialValue="0" ErrorMessage="Please Select Country  name"
                                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr bgcolor="#F1F0F0">
                                                                            <td>
                                                                                &nbsp;&nbsp;State:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <%--<asp:UpdatePanel ID="uplbilling2" runat="server">
                                                                                    <contenttemplate>--%>
                                                                                        <asp:DropDownList id="ddlstate" runat="server" Width="150px"   AppendDataBoundItems="True" DataTextField="State" DataValueField="id">
                                                                                            <asp:ListItem>Select State</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox id="txtBillotherstate" MaxLength="50"  runat="server" Visible="False"></asp:TextBox>  
                                                                                    <%--</contenttemplate>
                                                                                    <triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="ddlcountry" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                                                    </triggers>
                                                                                </asp:UpdatePanel>
                                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplbilling">
                                                                                    <progresstemplate>
                                                                                        <div style="position:absolute; z-index:1; left: 45%;top: 70%;">
                                                                                        <img src="images/ajax-loader.gif" alt="Load Progress" border="0" />
                                                                                        </div>
                                                                                    </progresstemplate>
                                                                                </asp:UpdateProgress>--%>
                                                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="reg" Display="None"
                                                                                    ClientValidationFunction="chkBilling" ErrorMessage=""></asp:CustomValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;City:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtcity" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqcity" ValidationGroup="reg" runat="server" ControlToValidate="txtcity"
                                                                                    Display="None" ErrorMessage="Please Enter City name" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr bgcolor="#F1F0F0">
                                                                            <td>
                                                                                &nbsp;&nbsp;Zip Code:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtpostal" runat="server" MaxLength="10"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqpostal" ValidationGroup="reg" runat="server" ControlToValidate="txtpostal"
                                                                                    Display="None" ErrorMessage="Please Enter Zip Code" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtpostal"
                                                                                    Display="None" ErrorMessage="Zip contain atleast 5 characters and only alphanumeric characters are allowed"
                                                                                    SetFocusOnError="True" ValidationExpression="[0-9A-Za-z]{5,}" ValidationGroup="reg"></asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;Phone:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtphone1" runat="server" MaxLength="15" Width="196px" onkeyup="selectbox1(this)"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqphone1" ValidationGroup="reg" runat="server" ControlToValidate="txtphone1"
                                                                                    Display="None" ErrorMessage="Please Enter Phone" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr bgcolor="#F1F0F0">
                                                                            <td>
                                                                                &nbsp;&nbsp;Email Address:<span style="color: crimson">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtemail" runat="server" MaxLength="100" Width="250px" ValidationGroup="reg"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqemail" ValidationGroup="reg" runat="server" ControlToValidate="txtemail"
                                                                                    Display="None" ErrorMessage="Please Enter email."></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="Regvalidemail" runat="server" ValidationGroup="reg"
                                                                                    SetFocusOnError="True" ErrorMessage="The Email Address you entered is wrong .Please enter a valid Email Address"
                                                                                    Display="None" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#E1E1E1"
                                                            class="text">
                                                            <tr>
                                                                <td width="25" align="center" bgcolor="#F9F8F8">
                                                                    <asp:CheckBox ID="chkshipping" runat="server" AutoPostBack="True" OnCheckedChanged="chkshipping_CheckedChanged" />
                                                                </td>
                                                                <td bgcolor="#F9F8F8">
                                                                    &nbsp;&nbsp;<strong>Please click here if Shipping Address is the same as the Billing
                                                                        Address </strong>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <strong>SHIPPING ADDRESS</strong><br />
                                                        <br />
                                                        <table width="100%" border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1"
                                                            class="text">
                                                            <tr>
                                                                <td bgcolor="#F9F8F8">
                                                                    <table width="100%" border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1"
                                                                        class="text">
                                                                        <tr>
                                                                            <td bgcolor="#F9F8F8">
                                                                                <table width="100%" border="0" cellpadding="3" cellspacing="0" class="text">
                                                                                    <tr bgcolor="#F1F0F0">
                                                                                        <td colspan="2">
                                                                                            <strong>Please enter your name and address exactly as they appear on your credit card
                                                                                                statement. </strong>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="180">
                                                                                            &nbsp;&nbsp;First Name:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtFirstName1" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="reg" runat="server"
                                                                                                ControlToValidate="txtFirstName1" Display="None" ErrorMessage="Please Enter First name in Shipping Address"
                                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr bgcolor="#F1F0F0">
                                                                                        <td>
                                                                                            &nbsp;&nbsp;Last Name:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtLastName1" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="reg" runat="server"
                                                                                                ControlToValidate="txtLastName1" Display="None" ErrorMessage="Please Enter Last name  in Shipping Address"
                                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            &nbsp;&nbsp;Street Address1:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtStreetaddress1" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="reg" runat="server"
                                                                                                ControlToValidate="txtStreetaddress1" Display="None" ErrorMessage="Please Enter Address  in Shipping Address"
                                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            &nbsp;&nbsp;Street Address2
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtStreetaddress3" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            &nbsp;&nbsp;Country:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%--<asp:UpdatePanel ID="upshipping" runat="server">
                                                                                                <contenttemplate>--%>
                                                                                                    <asp:DropDownList ID="ddlcountry1" runat="server" AppendDataBoundItems="True" Width="150px" OnDataBound="ddlcountry_DataBound" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry1_SelectedIndexChanged" DataSourceID="srccountry" DataTextField="Country" DataValueField="Id">
                                                                                                        <asp:ListItem Text="Select Country" Value="0"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                <%--</contenttemplate>
                                                                                            </asp:UpdatePanel>--%>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="reg" InitialValue="0"
                                                                                                runat="server" ControlToValidate="ddlcountry1" Display="None" ErrorMessage="Please Select Country  name  in Shipping Address"
                                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr bgcolor="#F1F0F0">
                                                                                        <td>
                                                                                            &nbsp;&nbsp;State:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%--<asp:UpdatePanel ID="upshipping2" runat="server">
                                                                                                <contenttemplate>--%>
                                                                                                    <asp:DropDownList id="ddlstate1" runat="server" Width="150px" AppendDataBoundItems="True" DataTextField="State" DataValueField="id">
                                                                                                        <asp:ListItem>Select State</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:TextBox id="txtshipotherstate" MaxLength="50"  runat="server" Visible="False"></asp:TextBox>
                                                                                                <%--</contenttemplate>
                                                                                                <triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcountry1" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                                                                </triggers>
                                                                                            </asp:UpdatePanel>
                                                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upshipping">
                                                                                                <progresstemplate>
                                                                                                    <div style="position: absolute; z-index: 1; left: 45%; top: 138%;">
                                                                                                        <img src="images/ajax-loader.gif" alt="Load Progress" border="0" />
                                                                                                    </div>
                                                                                                </progresstemplate>
                                                                                            </asp:UpdateProgress>--%>
                                                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="reg" Display="None"
                                                                                                ClientValidationFunction="chkShipping" ErrorMessage=""></asp:CustomValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            &nbsp;&nbsp;City:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtcity1" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="reg" runat="server"
                                                                                                ControlToValidate="txtcity1" Display="None" ErrorMessage="Please Enter City name  in Shipping Address"
                                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr bgcolor="#F1F0F0">
                                                                                        <td>
                                                                                            &nbsp;&nbsp;Zip Code:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtpostal1" runat="server" MaxLength="10"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="reg" runat="server"
                                                                                                ControlToValidate="txtpostal1" Display="None" ErrorMessage="Please Enter Zip Code  in Shipping Address"
                                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtpostal1"
                                                                                                Display="None" ErrorMessage="Shipping Address Zip contain atleast 5 characters and only alphanumeric characters are allowed"
                                                                                                SetFocusOnError="True" ValidationExpression="[0-9A-Za-z]{5,}" ValidationGroup="reg"></asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            &nbsp;&nbsp;Phone:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtphone2" runat="server" MaxLength="15" Width="196px" onkeyup="selectbox1(this)"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="reg" runat="server"
                                                                                                ControlToValidate="txtphone2" Display="None" ErrorMessage="Please Fill Day Phone no.  in Shipping Address"
                                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr bgcolor="#F1F0F0">
                                                                                        <td>
                                                                                            &nbsp;&nbsp;Email Address:<span style="color: crimson">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtemail1" runat="server" MaxLength="100" Width="250px" ValidationGroup="reg"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="reg" runat="server"
                                                                                                ControlToValidate="txtemail1" Display="None" ErrorMessage="Please Enter email.  in Shipping Address"></asp:RequiredFieldValidator>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="reg"
                                                                                                SetFocusOnError="True" ErrorMessage="The Email Address you entered is wrong .Please enter a valid Email Address  in Shipping Address"
                                                                                                Display="None" ControlToValidate="txtemail1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <b>Shipping Charges:<span style="color: crimson">*</span></b>
                                                        <br />
                                                        <br />
                                                        We charge a flat shipping fee regardless of package size and order value. We will
                                                        not charge any shipping fees if you can provide us with your FedEx account number.
                                                        <br />
                                                        <br />
                                                        <table width="100%" border="0" cellpadding="10" cellspacing="1" bgcolor="#E1E1E1"
                                                            class="text">
                                                            <tr>
                                                                <td align="left" valign="top" bgcolor="#F9F8F8">
                                                                    <asp:GridView ID="grdShipping" Width="100%" CssClass="text" OnRowDataBound="grdShipping_OnRowBound"
                                                                        AutoGenerateColumns="false" runat="server" CellPadding="6" EmptyDataText="No Shipping Charges available."
                                                                        Font-Bold="True">
                                                                        <HeaderStyle BackColor="#F1F0F0" />
                                                                        <RowStyle Height="20px" BackColor="#F1F0F0" CssClass="bluetext" />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <PagerStyle HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLocation" runat="server" Font-Bold="true"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Service">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblService" runat="server" Font-Bold="true" Text='<%# String.Format(Eval("Service").ToString()) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Freight">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPrice" runat="server" Font-Bold="true"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ShowHeader="False" HeaderText="Shipping Charge">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdnid" runat="server" Value='<%#Eval("id")%>' />
                                                                                    <asp:CheckBox ID="chkshipping" runat="server" onclick="Check(this)" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <table width="100%" id="tbshiping" runat="server" visible="false" border="0" cellpadding="2"
                                                            cellspacing="1" bgcolor="#E1E1E1" class="text">
                                                            <tr>
                                                                <td align="left" bgcolor="#F9F8F8" style="width: 460px">
                                                                    <strong>
                                                                        <asp:Label ID="lblship" runat="server" Text="Shipping Charge:" Width="133px"></asp:Label></strong>
                                                                </td>
                                                                <td bgcolor="#F9F8F8" align="left">
                                                                    <asp:Label ID="lblShipping" runat="server" Font-Bold="True" ForeColor="Red" Width="174px">00</asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:Panel ID="pnlFedexAccountnumber" runat="server" Width="100%" Font-Bold="true"
                                                            GroupingText="FedEx Account Number">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" height="50">
                                                                <tr>
                                                                    <td width="100%" colspan="3">
                                                                        <asp:Label ID="lblfedmsg" runat="server" EnableViewState="false" ForeColor="red"
                                                                            Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="180">
                                                                        <strong>Enter Your FedEx Account Number :</strong>
                                                                    </td>
                                                                    <td width="120">
                                                                        <asp:TextBox ID="txtfedex" runat="server" MaxLength="9"></asp:TextBox>&nbsp;
                                                                    </td>
                                                                    <td width="400">
                                                                        <strong></strong>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <br />
                                                        <asp:LinkButton ID="lnkCheckout" runat="server" OnClientClick="javascript:return checkship();"
                                                            OnClick="lnkCheckout_Click" ValidationGroup="reg"><img src="images/checkout.gif" ValidationGroup="reg"  alt="Checkout" width="92" height="25" border="0"  /></asp:LinkButton>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="reg" />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="5">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="17" align="left" valign="top" style="background-image: url(images/main-rbg.gif)">
                    <img src="images/space.gif" alt="space" width="1" height="1" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

