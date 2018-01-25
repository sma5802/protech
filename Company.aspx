<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Company" Codebehind="Company.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
              <tr align="left" valign="top">
                <td>
                <br />
                <strong class="blueheading">Mission and History</strong>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="management.aspx" class="blueheading1">Management</a>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<a href="facility.aspx" class="blueheading1">Facilities</a>
                </td>
              </tr>
            
                  <tr>
                  
                    <td align="left" valign="top"><%--<a href="default.aspx" class="bluelink">Home</a> &gt; <a href="company.aspx" class="bluelink">Company</a> &gt; Mission and History<br />--%>
                     
                        <asp:Label ID="lblContent" runat="server"></asp:Label>
                       <%-- <br /><strong>Mission</strong><strong></strong> <br />
                        Our mission is to provide high quality and competitively priced chemistry services and products to the pharmaceutical and biotech industries.                      <br />
                        <br />
                        <strong>History </strong>                      <br />
                        Headquartered in Burlington, MA with an R&amp;D center and a brand new Metric-Ton Scale Manufacturing Plant in Shanghai, China, PepTech Corporation has been providing high quality chemistry services and products to the pharmaceutical and biotech industries for over 15 years.                      <br />
                        <br />
                        We have built this world class chemistry service company purely through organic growth by providing top quality services and products to our customers. We have served over 500 customers worldwide, and are particularly known for our strengths in problem solving, fast turnaround, and good communication with our clients. We pride ourselves on high customer satisfaction and retention. <br />--%>
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br /></td>
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

