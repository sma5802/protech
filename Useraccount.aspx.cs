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

public partial class Useraccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["upd"] != null && Request.QueryString["upd"].ToString() != "")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Member Details updated successfully ";
        }
        Session["npath"] = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        Session["npath"] += "/" + HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
        Session["npath"] += "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        if (Session["mainuserid"] == null || Session["mainuserid"] == "")
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Login.aspx");
    }
}
