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

public partial class Admin_Peptech_ManageNews_EditNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                getnews();
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        bool editnews = customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "news set headlines='" + txthead.Text.Replace("'", "''").Trim() + "',details='" + HttpUtility.HtmlDecode(FCKeditor1.Value.Replace("'", "''").Trim()) + "' where id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
        if (editnews == true)
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageNews/NewsList.aspx?Upd=1");
    }


    protected void getnews()
    {
        DataTable dtgetnews = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "news where id=" + Convert.ToInt32(Request.QueryString["id"].ToString())).Tables[0];
        if (dtgetnews.Rows.Count > 0)
        {
            txthead.Text = dtgetnews.Rows[0]["headlines"].ToString().Replace("''", "'");
            FCKeditor1.Value = HttpUtility.HtmlDecode(dtgetnews.Rows[0]["details"].ToString().Replace("''", "'"));
         
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txthead.Text ="";
        FCKeditor1.Value = "";
    }
}
