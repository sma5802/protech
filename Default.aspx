<%@ Page Language="C#" MasterPageFile="~/Content.master"  AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">

/*** set the width and height to match your images **/

#slideshow {
    position:relative;
    height:242px;
}

#slideshow DIV {
    position:absolute;
    top:0;
    left:0;
    z-index:8;
    opacity:0.0;
    height: 242px;
    background-color: #FFF;
}

#slideshow DIV.active {
    z-index:10;
    opacity:1.0;
}

#slideshow DIV.last-active {
    z-index:9;
}

#slideshow DIV IMG {
    height: 242px;
    display: block;
    border: 0;
    margin-bottom: 10px;
}

</style>
<script type="text/javascript" src="jquery-1.2.6.min.js"></script>

<script type="text/javascript">

    /*** 
    Simple jQuery Slideshow Script
    Released by Jon Raasch (jonraasch.com) under FreeBSD license: free to use or modify, not responsible for anything, etc.  Please link out to me if you like it :)
    ***/

    function slideSwitch() {
        var $active = $('#slideshow DIV.active');

        if ($active.length == 0) $active = $('#slideshow DIV:last');

        // use this to pull the divs in the order they appear in the markup
        var $next = $active.next().length ? $active.next()
        : $('#slideshow DIV:first');

        // uncomment below to pull the divs randomly
        // var $sibs  = $active.siblings();
        // var rndNum = Math.floor(Math.random() * $sibs.length );
        // var $next  = $( $sibs[ rndNum ] );


        $active.addClass('last-active');

        $next.css({ opacity: 0.0 })
        .addClass('active')
        .animate({ opacity: 1.0 }, 1000, function () {
            $active.removeClass('active last-active');
        });
    }

    $(function () {
        setInterval("slideSwitch()", 5000);
    });

</script>
  <%--  <script type="text/javascript" language="javascript" src="slideshow.js"></script>--%>
<%--<body onload="runSlideShow()"></body>--%>
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
      <tr>
      <td width="489"><img src="images/peptech-buliding.jpg" width="489" height="242" /></td>
        <td height="6" align="center" style="background-image:url(images/blue-bg.gif); background-repeat:repeat-y;" valign="middle">
        <div id="slideshow">
    <div class="active">
        <img src="images/main-img1.gif" alt="Slideshow Image 1" />
       
    </div>
    
    <div>
        <img src="images/main-img2.gif" alt="Slideshow Image 2" />
        
    </div>
    
    <div>
        <img src="images/main-img3.gif" alt="Slideshow Image 3" />
        
    </div>
    
    <div>
        <img src="images/main-img4.gif" alt="Slideshow Image 4" />
       
    </div>
    
</div>
        
       <%-- <img src="images/main-img1.gif" width="428" height="242" name='SlideShow'/>--%></td>
      </tr>
      <tr>
        <td colspan="2" height="7"><img src="images/space.gif" width="1" height="1" /></td>
      </tr>
    </table>
    <table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr align="left" valign="top">
                  <td width="336"><table width="100%"  border="0" cellpadding="8" cellspacing="0" class="whiteheading">
                      <tr>
                        <td bgcolor="#10316B">Services and Products:</td>
                      </tr>
                    </table>
                      <img src="images/space.gif" alt="Space" width="1" height="7" /><br />
                      <table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#EAE9E9">
                        <tr>
                          <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                              <tr>
                                <td align="left" valign="top" class="greybg">
                                    <asp:Label ID="lblServices" EnableViewState="false" runat="server"></asp:Label>
                               <%-- <table width="100%"  border="0" cellpadding="3" cellspacing="0" class="bluetext">
                                    <tr>
                                      <td width="10" align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td>Custom Synthesis</td>
                                    </tr>
                                    <tr>
                                      <td align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td>Medicinal Chemistry &amp; Focused Libraries</td>
                                    </tr>
                                    <tr>
                                      <td align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td>Process R&amp;D &amp; Large Scale Manufacturing</td>
                                    </tr>
                                    <tr>
                                      <td align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td> 2200 Products Available</td>
                                    </tr>
                                </table>--%>
                                </td>
                              </tr>
                          </table></td>
                        </tr>
                    </table></td>
                  <td width="7"><img src="images/space.gif" width="1" height="1" /></td>
                  <td><table width="100%"  border="0" cellpadding="8" cellspacing="0" class="whiteheading">
                      <tr>
                        <td bgcolor="#10316B">Quality and Value:</td>
                      </tr>
                    </table>
                      <img src="images/space.gif" alt="Space" width="1" height="7" /><br />
                      <table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#EAE9E9">
                        <tr>
                          <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                              <tr>
                                <td align="left" valign="top" class="greybg">
                                <asp:Label ID="lblQuality" EnableViewState="false" runat="server"></asp:Label>
                               <%-- <table width="100%"  border="0" cellpadding="3" cellspacing="0" class="bluetext">
                                    <tr>
                                      <td width="10" align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td> 15-Year Track Record</td>
                                    </tr>
                                    <tr>
                                      <td align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td>Experienced Project Management</td>
                                    </tr>
                                    <tr>
                                      <td align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td>Competitive Pricing (FTE or project-based fees)</td>
                                    </tr>
                                    <tr>
                                      <td align="left"><img src="images/black-square.gif" width="5" height="5" /></td>
                                      <td>IP Security</td>
                                    </tr>
                                </table>--%>
                                </td>
                              </tr>
                          </table></td>
                        </tr>
                    </table></td>
                </tr>
              </table>
                <div align="justify"><asp:Label ID="lblContent" EnableViewState="false" runat="server" ></asp:Label></div>
               </td>
            <td width="7"><img src="images/space.gif" width="1" height="1" /></td>
            <td width="232">
            <table width="100%"  border="0" cellpadding="8" cellspacing="0" bgcolor="#EAE9E9" class="text">
                <tr>
                        <td bgcolor="#10316B" class="whiteheading">News Releases:</td>
                </tr>
            </table>  
            <img src="images/space.gif" alt="Space" width="1" height="7" /><br />
                  <table width="100%"  border="0" cellpadding="8" cellspacing="0" class="text">
                      <tr>
                        <td align="left" valign="top" class="newsbg" style="border:solid 1px #EAE9E9" bgcolor="#FFFFFF">
<asp:DataList ID="dlsNews" EnableViewState="false" runat="server" Width="96%" OnItemDataBound="dlscities_ItemDataBound" 
                                RepeatColumns="1" DataSourceID="sqlNews">
<ItemTemplate>    
    <div><strong class="bluetext"><%#Eval("headlines")%></strong></div>
    <asp:Label ID="lblDesc" EnableViewState="false" runat="server"></asp:Label>
    <asp:HyperLink ID="hlMore" EnableViewState="false" runat="server" NavigateUrl='<%#string.Format("NewsDetail.aspx?id={0}",Eval("id")) %>' CssClass="redlnk">more&raquo;</asp:HyperLink>
</ItemTemplate>
<SeparatorTemplate><br /></SeparatorTemplate>
</asp:DataList> 
                            <asp:SqlDataSource ID="sqlNews" EnableViewState="false" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:dbConnect %>" 
                                SelectCommand="SELECT top 5 id, details, headlines FROM pep$tech$corp.Peptech_news WHERE status=1 order by newid()">
                            </asp:SqlDataSource>
</td>
</tr>
</table>
     
            <%--<table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#EAE9E9" class="text">
                <tr>
                  <td height="6" bgcolor="#10316B"><img src="images/space.gif" alt="Space" width="1" height="1" /></td>
                </tr>
                <tr>
                  <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                      <tr>
                        <td align="left" valign="top" class="newsbg"><div class="blueheading">News Releases: </div>
                            <br />
                            <strong class="bluetext">CAS-MI Launches Innovation Center</strong> <br />
                        Industrial and consumer products incubator <br />
                        <a href="#" class="redlnk"><strong>more&raquo;</strong></a> <br />
                        <br />
                        <strong class="bluetext">CAS-MI Extends Polymer and Coatings Testing Capabilities </strong> <br />
                        Product characterization and failure investigations <br />
                        <a href="#" class="redlnk"><strong>more&raquo;</strong></a><br />
                        <br />
                        <strong class="bluetext">CAS-MI Launches Innovation Center</strong> <br />
                        Industrial and consumer products incubator <br />
                        <a href="#" class="redlnk"><strong>more&raquo;</strong></a><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        </td>
                      </tr>
                  </table></td>
                </tr>
            </table>--%></td>
          </tr>
        </table>
      </td>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
  </tr>
</table>
 </asp:Content> 
