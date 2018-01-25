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

public partial class Contents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request.QueryString["page"] != null && Request.QueryString["page"].ToString() != "")
        {
            //Response.Write("Select * from " + customUtility.DBPrefix + "Edit where pageName='" + Server.UrlDecode(Request.QueryString["page"]) + "'");
            //Response.End();
            DataSet dsGetContent = customUtility.GetTableData("Select * from " + customUtility.DBPrefix + "Edit where home=0 and pageName='" + Server.UrlDecode(Request.QueryString["page"].ToString().Replace("'","''")) + "'");

            if (dsGetContent.Tables[0].Rows.Count > 0)
            {
                //lblPath.Text = dsGetContent.Tables[0].Rows[0]["PageName"].ToString().Replace("''", "'");
                lblTitle.Text = dsGetContent.Tables[0].Rows[0]["PageName"].ToString().Replace("''", "'");
                lblContent.Text = HttpUtility.HtmlDecode(dsGetContent.Tables[0].Rows[0]["pagedata"].ToString().Replace("''", "'"));
            }
            else
                Response.Redirect("default.aspx");
        }
    }
}
