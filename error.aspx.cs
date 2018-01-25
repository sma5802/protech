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

public partial class FileNotFound : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }
    protected void btnRedirect_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["aspxerrorpath"] != null && Request.QueryString["aspxerrorpath"].ToString() != "")
        {
            string strWebSitePath = Request.QueryString["aspxerrorpath"].ToString();
            Response.Write(strWebSitePath);
                if (!strWebSitePath.ToLower().Contains("admin_peptech"))
                    Response.Redirect("default.aspx");
                else
                    Response.Redirect("admin_peptech/default.aspx");
           
        }
    }
}
