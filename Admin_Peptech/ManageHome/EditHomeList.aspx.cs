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

public partial class Admin_Peptech_ManageHome_EditHomeList : System.Web.UI.Page
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
        //bool check = customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "homelist where id!=" + Request.QueryString["id"].ToString() + " and Title='" + ddlTitle.SelectedValue + "'");
        //if (check != true)
        //{
        //    lblmessage.Text = "Downloads Title already exists ! Try another Downloads Title.";
        //}
        bool editnews = customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "homelist set Title='" + ddlTitle.SelectedValue + "',description='" + HttpUtility.HtmlDecode(FCKeditor1.Value.Replace("'", "''").Trim()) + "' where id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
        if (editnews == true)
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageHome/HomeList.aspx?Upd=1");
    }


    protected void getnews()
    {
        try
        {
            DataTable dtgetnews = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "homelist where id=" + Convert.ToInt32(Request.QueryString["id"].ToString())).Tables[0];
            if (dtgetnews.Rows.Count > 0)
            {
                ddlTitle.SelectedValue = dtgetnews.Rows[0]["title"].ToString().Replace("''", "'");
                FCKeditor1.Value = HttpUtility.HtmlDecode(dtgetnews.Rows[0]["description"].ToString().Replace("''", "'"));

            }
            else
            {
                Response.Redirect("HomeList.aspx");
            }
        }
        catch { Response.Redirect("HomeList.aspx"); }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeList.aspx");
        //ddlTitle.SelectedValue ="Please Select";
        //FCKeditor1.Value = "";
    }
}
