<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="ManufacturingPlant" Codebehind="ManufacturingPlant.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
              <tr align="left" valign="top">
                
                <td>
                <br /><br />
                <a href="operations.aspx" class="blueheading1">R&amp;D Center</a>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<strong class="blueheading">Manufacturing Plant</strong>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="instruments.aspx" class="blueheading1">Instruments</a></td>
              </tr>
            
                  <tr>
                    <td align="left" valign="top"><div align="justify"><%--<a href="default.aspx" class="bluelink">Home</a> &gt; <a href="operations.aspx" class="bluelink">Operations</a> &gt; Manufacturing Plant<br />--%>
                      
                        <asp:Label ID="lblCmsManufacturing" runat="server"></asp:Label>
                      <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div></td>
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

