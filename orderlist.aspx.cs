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

public partial class orderlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["mainuserid"] == null && Session["mainuserid"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["WebsitePath"].ToString() + "Login.aspx?requestpath=" + Request.Url.ToString());

        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    protected void fillgridview()
    {

        DataSet ds;

        ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "order where status=1 and userno in(select id from " + customUtility.DBPrefix + "memberlist where id='" + Session["mainuserid"] + "') order by OrderID desc");
        // Response.Write("select * from " + customUtility.DBPrefix + "order where userno in(select id from " + customUtility.DBPrefix + "user where email='" + Session["email"].ToString() + "') order by OrderID desc");
        // Response.End();
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }


    protected void OnCommand_Delete(object sender, CommandEventArgs e)
    {
        customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "order where orderno='" + e.CommandArgument + "'");
        fillgridview();
    }
    protected void GridViw1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }
}
