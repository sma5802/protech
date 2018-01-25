<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Download" Codebehind="Download.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    <td align="left" valign="top"><div align="justify"><%--<a href="default.aspx" class="bluelink">Home</a> &gt; Download<br />--%>
                      <br />
                      <span class="blueheading">Download</span><br />
                        <br />
                        <br /> 
                         <asp:DataList ID="dlscities" runat="server" Width="100%" OnItemDataBound="dlscities_ItemDataBound" RepeatColumns="1" RepeatDirection="vertical">
<ItemTemplate>                     
                        <table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
                          <tr align="left" valign="top">
                            <td width="60">
                                
                            <%--<a target="_blank" href="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString()%>Downloads/<%# String.Format(Eval("downloadfile").ToString())%>">--%>
                            <asp:Image ID="ImgDownloadtype" runat="server" width="44" height="51" border="0" ImageUrl='<%# String.Format(Eval("image","~/DownloadsImage/{0}")) %>'/>
                            <%--</a>--%></td>
                            <td>
                            <asp:LinkButton ID="lb" CssClass="redlnk" CommandArgument='<%#Eval("downloadfile") %>' CommandName="download" runat="server" oncommand="lb_Command"><strong><%#Eval("downloadtext")%></strong></asp:LinkButton>
                            <%--<br /><br>--%>
                            <%--<a id="dd" runat="server" visible="false" class="redlnk"><strong><%#Eval("downloadtext")%></strong></a>--%><br />
                           <%#Eval("title") %></td>
                          </tr>
                         <%-- <tr align="left" valign="top">
                            <td><br />
                              <br />
                              <a href="PepTech2006_Version2.zip" target="_blank"><img src="images/img_sdf.gif" width="44" height="51" border="0" /></a></td>
                            <td><br />
                            <br />
                            <a href="PepTech2006_Version2.zip" target="_blank" class="redlnk"><strong>Please click here
to download the
SDF file.</strong></a><br />
Our catalog is also available in SDF format. The SDF file can be imported to your own databases for structure searches. </td>
                          </tr>--%>
                        </table>
                        </ItemTemplate>
                        <SeparatorTemplate><br /></SeparatorTemplate>
                        </asp:DataList>
                      <br />
                        <br />
                        <br />
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

