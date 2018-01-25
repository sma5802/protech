using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UserClass;
public partial class checkmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if (customUtility.CheckDuplicateFieldValue(Request.QueryString["EMail"].ToString(), "MemberList", "username", customUtility.CompareType.text, ""))
      {
          Response.Write("user exist");
      }
      else
      {
          Response.Write("not");
      }
    }
}
