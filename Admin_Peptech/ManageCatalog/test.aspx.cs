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

public partial class Admin_Peptech_ManageCatalog_test : System.Web.UI.Page
{
    string direction;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"catalog where status=1");
        if (dt.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        Session["ds"] = dt;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    

  


    protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
    {
        DataSet ds = new DataSet();

        ds = (DataSet)Session["ds"];

        DataTable dtable = ds.Tables[0];
        direction = "ASC";
        if (dtable != null)
        {
            DataView m_DataView = new DataView(dtable);
            if (Session["sorting"] != null)
            {
                direction = Session["sorting"].ToString();
                if (direction.Equals("ASC"))
                    direction = "DESC";
                else
                    direction = "ASC";
            }
            m_DataView.Sort = e.SortExpression + " " + direction;
            GridView1.DataSource = m_DataView;
            GridView1.DataBind();
            Session.Add("sorting", direction);
        }
    }
}
