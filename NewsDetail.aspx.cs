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

public partial class NewsDetail : System.Web.UI.Page
{
    public string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "news where status=1 order by postdate desc").Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str += dt.Rows[i]["headlines"].ToString().Replace("\"", "'") + "`" + ConfigurationManager.AppSettings["WebSitePath"].ToString() + "newsdetail.aspx?id=" + dt.Rows[i]["id"].ToString() + "^";
            //str += dt.Rows[i]["headlines"].ToString().Replace("\"", "'") + "`" + ConfigurationManager.AppSettings["WebSitePath"].ToString() + dt.Rows[i]["id"].ToString() + ".html" + "^";
        }

        if (Request.QueryString["id"]!=null && Request.QueryString["id"].ToString()!="")
        {
            try
            {
                DataTable dtNews = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "news where id=" + Request.QueryString["id"]).Tables[0];
                if (dtNews.Rows.Count > 0)
                {
                    lblTitle.Text = dtNews.Rows[0]["headlines"].ToString();
                    lblNews.Text = dtNews.Rows[0]["details"].ToString();
                }
                else
                {
                    Response.Redirect("default.aspx");
                }
            }
            catch { Response.Redirect("default.aspx"); }
        }
    }
}
