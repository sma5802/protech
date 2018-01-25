<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="NewsDetail" Codebehind="NewsDetail.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
<tr>
<td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
<td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
<tr align="left" valign="top">
<td>
<table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
<tr>
<td align="left" valign="top">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="left" valign="middle" height="30px">
<div id="topContent" class="contentBand">
<span class="tickLink"><a href="javascript:void(0);">Latest News</a></span>
<img class="tickButton" id="back" src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>resources/images/buttonBackOnWht.gif" border="0" height="16" alt="" />
<img class="tickButton" id="next" src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>resources/images/buttonNextOnWht.gif" border="0" height="16" alt="" />
<span id="tick" class="tickContent"><a href="#"></a></span><span id="cursor"><img src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>resources/images/tickerCursor.gif" border="0" alt="_" /></span>

<script language="javascript" type="text/javascript">

var arrNewsItems = new Array();
var pausecontent="<%=str %>".split('^'); 

	for(var i=0;i<pausecontent.length;i++)
	{   
	  var content=pausecontent[i].split('`');
	  arrNewsItems.push(new Array(content[0],content[1]));	
    }
	
var intTickSpeed = 5000;
var intTickPos = 0;
var tickLocked = false;
var fadeTimerID;
var autoTimerID = 0;
var intTypeSpeed = 50;

var intCurrentPos = 0;
var currentText = '';
var currentLink = '';
var strText = '';
var isFirstPass = true;
</script>
<script language="javascript" type="text/javascript" src="<%=ConfigurationManager.AppSettings["WebSitePath"].ToString() %>resources/js/newsTicker2.js"></script>
</div>
</td>
</tr>
<tr>
<td>
<div align="justify"><a href="default.aspx" class="bluelink">Home</a>&nbsp;<b class="bluetext">>></b>&nbsp;News<br />
<br />
<span class="blueheading">News</span><br />
<br />
</div>
</td>
</tr>
<tr>
<td>
<asp:Label ID="lblTitle" runat="server" CssClass="blueheading"></asp:Label><br />
<asp:Label ID="lblNews" runat="server"></asp:Label>
</td>
</tr>
</table>


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


