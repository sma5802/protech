using System;
using System.IO;
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

public partial class Admin_Peptech_Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblServerTime.Text = string.Format("{0:D}", System.DateTime.Now);
        if (Session["AdminID"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/login.aspx?requestpath=" + Request.ServerVariables["SCRIPT_NAME"]);

        //else
        //    lblhead.Text = "Welcome " + Session["admintitle"].ToString();
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://peptechcorp.com") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://peptechcorp.com"))
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("http://peptechcorp.com", "http://www.peptechcorp.com").Replace("https://peptechcorp.com", "https://www.peptechcorp.com"));
            }
        }
    }
    protected void lnkSignout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session["AdminID"] = "";
        Session["admintitle"] = "";
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/login.aspx");
    }
}
