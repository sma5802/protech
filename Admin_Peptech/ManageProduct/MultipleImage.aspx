<%@ Page Language="C#" AutoEventWireup="True" Inherits="Admin_Peptech_ManageProduct_MultipleImage" Codebehind="MultipleImage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
function GenerateRow()
{
  var tbl=document.getElementById("tblImages");
  var lastRow = tbl.rows.length;
  var iteration = lastRow;
  var row = tbl.insertRow(lastRow);
  var cell1 = row.insertCell(0);
  var newdiv = document.createElement('Input');						
  newdiv.type="File";	
  newdiv.id="Images"+iteration;
  newdiv.name="Images"+iteration;
  newdiv.setAttribute('runat','server');
  cell1.appendChild(newdiv);
  document.getElementById("<%= hdImage.ClientID %>").value = lastRow;
}
function deleteinput()
{
var table= document.getElementById("tblImages");
var length=table.rows.length;
if(length>1)
{
table.deleteRow(length-1);
document.getElementById("<%= hdImage.ClientID %>").value = length-2;
}
}
</script>
</head>
<body>
    <form id="form1" runat="server" encType="multipart/form-data" method="post">
    <div>
    <table class="box" width="100%" >
 <tr>
        <td align="left" valign="middle">
        </td>
        <td align="left" valign="middle">
        <table class="txt" Width="100%"><tbody ID="tblImages"><tr><td style="width: 25%"></td><td style="width: 25%"></td><td style="width: 25%"></td><td style="width: 25%"></td></tr></tbody></table><asp:HiddenField ID="hdImage" runat="server"  Value="0"/> <a onclick="javascript:GenerateRow();" href="Javascript:void(0);" class="bluelnk">Upload Product Image</a> | <a onclick="javascript:deleteinput();" href="Javascript:void(0);" class="bluelnk">Remove</a><br />
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

    </div>
    </form>
</body>
</html>
