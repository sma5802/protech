<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="True" Inherits="Contents" Codebehind="Contents.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
a {
    color:blue;
    text-decoration: underline;
  }
</style> 
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    <td align="left" valign="top"><div align="justify"><%--<a href="Default.aspx" class="bluelink">Home</a> &gt; <asp:Label ID="lblPath" runat="server"></asp:Label><br />--%>
                      <br />
                        <asp:Label ID="lblTitle" runat="server"  CssClass="blueheading"></asp:Label><br />
                        <asp:Label ID="lblContent" runat="server"></asp:Label>
                      <%--<span class="blueheading">Privacy Policy</span><br />
                        <br />                      
                        This Policy is designed to assist you in understanding how this site collects, uses and safeguards the personal information you provide to PepTech Corp. and to assist you in placing orders.                      <br />
                        <br />
                        <strong>What Information Do We Collect and How Do We Use it?                      </strong><br />
                        When you visit this site you may provide PepTech Corp. with two types of information: personal and business information you actively choose to disclose ("Active Information") through placing an order or contacting PepTech Corp., and use information collected, in a way not visible to you, on an aggregate anonymous basis as you and others browse this site ("Passive Information").<br />
                        <br />
                        <strong>Active Information You Disclose</strong><br />
                        If you place an order, PepTech Corp. collects your name, your company name, billing and shipping addresses, email, purchase order and/or credit card information. PepTech Corp. uses such information only for processing your order and to notify you of your order status.<br />
                        <br />
                        <strong>Passive Information Collected</strong><br />
                        Our site utilizes a standard technology called "cookies" to collect information about how this site is used. Passive Information gathered might include the date and time of visits, the site pages viewed, time spent at this site. Passive Information is collected on an aggregate basis without any association to your personal information so that you remain anonymous. If you do not wish to transmit "cookie" information about yourself, you may turn off the cookie function in your web browser.<br />
                        <br />
                        <strong>Sharing Information with the Government or As Otherwise Required by Law</strong><br />
                        We may be required by law or government agency to disclose both Active and Passive Information you have provided to PepTech Corp. and will do so if required by law or subpoena.<br />
                        <br />
                        <strong>How Do We Secure Active Information and Passive Information?</strong><br />
                        We secure your information submitted by you by using reasonable efforts to prevent unauthorized access or disclosure, or accidental loss or destruction, anathema to us, of Active Information and Passive Information.                      <br />
                        <br />
                        <i>We reserve the right to revise this policy. Please check back on this site as this policy may be revised from time to time without notice to you.</i> <br />
                      <br />--%>
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

