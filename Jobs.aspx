<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Jobs" Codebehind="Jobs.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    <td align="left" valign="top"><div align="justify"><span class="blueheading">Job Opportunities</span><br />
                        <br />     
                        <asp:DataList ID="dlsNews" runat="server" Width="100%" OnItemDataBound="dlscities_ItemDataBound" RepeatColumns="1" RepeatDirection="vertical">
<ItemTemplate>    
   Job Position: <b><%#Eval("jobposition")%></b><br />
   Location:<%#Eval("Location")%><br />
    <U>Job Description</U><br />
    <%#HttpUtility.HtmlDecode(Eval("Description").ToString())%><br />
     <U>Job Requirements</U><br />
     <%#HttpUtility.HtmlDecode(Eval("Requirement").ToString())%>
</ItemTemplate>
<SeparatorTemplate><hr color="#EAE9E9" size="1px"/></SeparatorTemplate>
</asp:DataList>                  
                   <%--   Job Position: <b>Chief Scientific Officer (CSO)</b>                      <br />
                      Location: Position located in Shanghai, China                      <br />
                      <br />
                      <U>Job Description</U><br />
                          <br />
                          The Chief Scientific Officer is responsible for the overall research and development functions in the company. Responsibilities include overseeing the creation, development, design and implementation of the organization's research and development programs and building partnerships and relationships with the scientific community.<br />
                      <br />
  The Chief Scientific Officer will oversee chemistry and manufacturing development and develop an infrastructure to support the full range of research and development activities.  <br />
  <br />
  <U>Job Requirements</U>                      <ul class="text">
                          <li>Ph.D. in organic chemistry or medicinal chemistry with &gt;7 years industry experience in US pharmaceutical companies or biotech companies </li>
                          <li>With more than 5 years of management experience, including project and budget management </li>
                          <li>Excellent oral and written communication and presentation skills </li>
                          <li>Proven track record of innovation including peer-reviewed publications and/or patents </li>
                      </ul>
                        <hr color="#EAE9E9" size="1px"/>
                        <br />
                      Job Position: <b>Director, Medicinal Chemistry</b>                      <br />
                      Location: Position located in Shanghai, China                      <br />
                      <br />
                      <U>Job Description</U><br />
                        <br />
                        The candidate will direct a team of medicinal chemists and play a significant role in the generation of lead compounds for various medicinal chemistry programs, by utilizing multiple strategies such as HTS hit validation, lead optimization and structure-based design. The qualified individual must be proficient in identifying structure activity relationships (SAR), selectivity profiles, pharmacokinetics and in vivo efficacy. The individual will collaborate and interact with a broad group of scientists and with external collaborators.                      <br />
                        <br />
                        <U>Job Requirements</U>                          <br />
                      Ph.D. in organic or medicinal chemistry 
                      <ul class="text"><li>Minimum of 4 - 6 years of relevant experience in the pharmaceutical or biotech industry </li>
                          <li>Ability to work independently and ability to interact with a multidisciplinary team of medicinal chemists, biologists, pharmacologists and external collaborators </li>
                          <li>Experience with structure-based drug design </li>
                          <li>Flexibility to work in a dynamic environment </li>
                      </ul>
                        <hr color="#EAE9E9" size="1px"/>
                        <br />
Job Position: <b>Director, Process Chemistry and Manufacturing</b> <br />
Location: Position located in Shanghai, China<br />
<br />
<U>Job Description</U><br />
                          <br />
                          The candidate will lead a team of chemists to develop chemical processes to manufacture chemical intermediates. The qualified individual will be responsible for designing and troubleshooting synthetic routes and scale up plans and help manage the pilot plant.                      <br />
                          <br />
                          <U>Job Requirements</U><br />
                      <br />
                      Ph.D. in Organic Chemistry with emphasis on synthesis; 0-4 years post doctoral experience and 4-10 years industrial experience in scaling up chemical processes<br />
                      <br />
  Thorough knowledge and experience in handling, purification and characterization of organic compounds and drug candidates on gram to kilogram scale is required <br />
  <br />
  <hr color="#EAE9E9" size="1px"/>
  <br />
  Job Position: <strong>Business Development Manager</strong><br />
  Location: Anywhere in the USA
  <br />
  <br /> 
  <U>Job Description</U><br />
      <br />
  The new Business Development Manager will report to our VP, Business Development. Key responsibilities include: (1) expand our client base and develop new clients' relationships; (2) grow our sales revenue and meet our sales goal; (3) maintain and strengthen relationships with existing customers, and identify new opportunities with these existing customers. Up to 50% travel is required. The new hire will have the option of working from home. Compensation is commensurate with experience and will consist of a base salary and commissions based on revenue generated.  <br />
      <br />
  <U>Job Requirements</U><br />
  <br />
  The new Business Development Manager must have great business development skills in the pharma CRO industry. Excellent people skills and communication skills are required. The new hire must be able to identify potential customers, know how to initiate contacts, build relationships, and successfully close deals and bring in revenue. The new Business Development Manager also needs to have a solid background in organic chemistry.<br />
  <br />--%>
  <hr color="#EAE9E9" size="1px"/>
  <%--<br />--%>
  <asp:Label ID="lblcmsjob" runat="server"></asp:Label>
  
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

