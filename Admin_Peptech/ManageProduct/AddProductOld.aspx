<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" CodeFile="AddProductOld.aspx.cs" Inherits="Admin_Peptech_ManageProduct_AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function SetExpire(obj)
{
var tblRow=document.getElementById("tblRow");
    if(obj.value=="1")
        tblRow.style.display="inline";
    else
        tblRow.style.display="none";
        
}

function GenerateRow()
{
  var tbl=document.getElementById("tblImages");
  var lastRow = tbl.rows.length;
  var iteration = (lastRow-1)/3;  
  if(iteration < 1)
    iteration =0;
  var row = tbl.insertRow(lastRow);
  var row1=tbl.insertRow(lastRow);
  var row2=tbl.insertRow(lastRow)
  
  var cell0 = row.insertCell(0);
  var newdiv0 = document.createElement('Label');						
  newdiv0.innerHTML="<strong>Price:</strong>";	
  newdiv0.setAttribute("align","left");
  newdiv0.setAttribute("width","75%");
 
   
  cell0.appendChild(newdiv0);
  
  var cell1 = row.insertCell(1);
  var newdiv = document.createElement('Input');						
  newdiv.type="text";	
  newdiv.id="caption"+iteration;
  newdiv.name="caption"+iteration;
  newdiv.setAttribute('runat','server');
  cell1.appendChild(newdiv);
  
//  newdiv=document.createElement('RequiredFieldValidator');
//  newdiv.id="rf"+iterartion;
//  newdiv.setAttribute('ErrorMessage','Please enter catalog name');
//   newdiv.setAttribute('ValidationGroup','insertcategory');
//   newdiv.setAttribute('Display','None');
//    newdiv.setAttribute('ControlToValidate','caption'+itration);
  document.getElementById("<%=hdImage.ClientID %>").value = lastRow;
  
  //alert(iteration);
   //alert(document.getElementById("Images1").getAttribute('runat'));
  var cell2 = row1.insertCell(0);
  var newdiv1 = document.createElement('Label');						
  newdiv1.innerHTML="<strong>Quantity:</strong>";	
  newdiv1.setAttribute("align","left");
  newdiv1.setAttribute("width","75%");
 
   
  cell2.appendChild(newdiv1);
  
  var cell3 = row1.insertCell(1);
  var newdiv2 = document.createElement('Input');						
  newdiv2.type="text";	
  newdiv2.id="Caption1"+iteration;
  newdiv2.name="Caption1"+iteration;
  newdiv2.setAttribute('runat','server');
  cell3.appendChild(newdiv2);
 
 
  document.getElementById("<%=hdImage.ClientID %>").value = lastRow;
  
  var cell4 = row2.insertCell(0);
  var newdiv3 = document.createElement('Label');						
  newdiv3.innerHTML="<strong>Catalog Name:</strong>";	
  newdiv3.setAttribute("align","left");
  newdiv3.setAttribute("width","75%");
 
   
  cell4.appendChild(newdiv3);
 
 var cell5 = row2.insertCell(1);
  var newdiv4 = document.createElement('Input');						
  newdiv4.type="text";	
  newdiv4.id="Caption2"+iteration;
  newdiv4.name="Caption2"+iteration;
  newdiv4.setAttribute('runat','server');
  cell5.appendChild(newdiv4);
 
  document.getElementById("<%=hdImage.ClientID %>").value = tbl.rows.length-1;

}


function deleteinput()
{
var table= document.getElementById("tblImages");
var length=table.rows.length;
//alert(length);
if(length>3)
{
table.deleteRow(length-1);
table.deleteRow(length-2);
table.deleteRow(length-3);
  document.getElementById("<%=hdImage.ClientID %>").value = length-4;
 // length=length-1;
}

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
<TABLE class="box" width="100%"><tr><td align="left" valign="middle" >
<asp:Label ID="Label6" runat="server" Text="Product Name:" Font-Bold="true"></asp:Label>
</td>
<td align="left" valign="middle" style="width:83%">
<asp:TextBox ID="txtProduct" runat="server"  ValidationGroup="insertcategory"></asp:TextBox>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProduct"
        ErrorMessage="Please Enter Product Name" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
   <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrice" ErrorMessage="Please Enter Service Price in Numeric Format" ValidationGroup="insertcategory" Display="None" Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>--%>
</td>
</tr>
    <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label1" runat="server" Text="CAS:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
        <asp:TextBox ID="txtCAS" runat="server" MaxLength="200" ValidationGroup="insertcategory"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCAS"
        ErrorMessage="Please Enter CAS" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please Enter CAS In Correct format" ControlToValidate="txtCAS" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label2" runat="server" Text="Formula:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
        <asp:TextBox ID="txtFormula" runat="server" MaxLength="200" ValidationGroup="insertcategory"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFormula"
        ErrorMessage="Please Enter Formula" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label3" runat="server" Text="Molecular Weight:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
        <asp:TextBox ID="txtWeight" runat="server" MaxLength="200" ValidationGroup="insertcategory"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtWeight"
        ErrorMessage="Please Enter Molecular Weight" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please enter molecular weight in correct format" ValidationGroup="insertcategory" Display="None" ControlToValidate="txtWeight" Type="Double"></asp:RangeValidator>
        </td>
    </tr>
     <tr>
        <td align="left" valign="middle">
        <asp:Label ID="Label7" runat="server" Text="Product Image:" Font-Bold="true"></asp:Label>
        </td>
        <td align="left" style="width: 83%" valign="middle">
            <asp:FileUpload ID="fuPImage" runat="server" />
       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="fuPImage"
        ErrorMessage="Please Enter Product Image" ValidationGroup="insertcategory" Display="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
     <tr>
        <td align="left" valign="middle" colspan="2">
       <h3><br />Add Product Catalog</h3>
        </td>
        
    </tr>
     <tr>
        <td align="left" style="width: 83%" valign="middle" colspan="2">
          <table Class="txt" Width="100%">
          <tbody ID="tblImages">
          <tr>
          <td style="width: 17%"></td>
          <td style="width: 33%"></td>
          <td style="width: 25%"></td>
          <td style="width: 25%"></td>
          </tr></tbody></table><asp:HiddenField ID="hdImage" runat="server"  Value="0"/> <a onclick="javascript:GenerateRow();" href="Javascript:void(0);" class="bluelnk">Add Product Catalog</a> | <a onclick="javascript:deleteinput();" href="Javascript:void(0);" class="bluelnk">Remove</a>&nbsp;<br />
        </td>
    </tr>
<tr>
<td ></td>
<td>
<asp:Button ID="btnsubmit" runat="server" CssClass="button" Text="Save" OnClick="btnsubmit_Click" ValidationGroup="insertcategory"/>
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
