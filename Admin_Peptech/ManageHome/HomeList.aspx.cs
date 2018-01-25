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

public partial class Admin_Peptech_ManageHome_HomeList : System.Web.UI.Page
{
    private CheckBox cb;
    private HiddenField hddid;
    private DataSet ds;
    private int hddval;
    private string hidAllValue = "";
    private int rowcnt = 0;
    private int rowindex;
    private int order;
    private int id;
    private string cat;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Upd"] != null)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "HomeList Updated Successfully";
        }
        else if (Request.QueryString["add"] != null)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "HomeList Added Successfully";
        }
        if (!IsPostBack)
        {
            // Bind();
        }

    }
    protected void grdListProperty_OnRowBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            //Image img = (Image)e.Row.FindControl("ImageStatus");
            //if (dv["status"].ToString().ToLower().Equals("true"))
            //{
            //    img.ImageUrl = "../../images/status-on.gif";
            //}
            //else
            //{
            //    img.ImageUrl = "../../images/status-off.gif";
            //}

        }
    }

    //Paging

    protected void grdListProperty_databound(object sender, EventArgs e)
    {
        if (grdListProperty.Rows.Count > 0)
        {
            rowcnt = int.Parse(grdListProperty.Rows.Count.ToString());
            Label lblpage = (Label)grdListProperty.BottomPagerRow.FindControl("lblpage");
            DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
            lblpage.Text = "Page : " + Convert.ToString(grdListProperty.PageIndex + 1);
            ddlpage.SelectedValue = grdListProperty.PageSize.ToString();
        }
    }
    protected void ddlpage_selected(object sender, EventArgs e)
    {
        DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
        grdListProperty.PageSize = int.Parse(ddlpage.SelectedValue);
        grdListProperty.DataBind();
    }

    protected void grdListProperty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin_Peptech/ManageHome/AddHomelist.aspx?add=1");
    }
    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex > 0)
        {

            grdListProperty.PageIndex = e.NewPageIndex;
            grdListProperty.DataBind();
        }
    }




    protected void grdListProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower() == "delete")
        {
            //bool del = customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "services where id=" + e.CommandArgument.ToString());
            //if (del.ToString().ToLower().Equals("true"))
            //{
            lblmessage.Visible = true;
            lblmessage.Text = "Record deleted successfully";
            grdListProperty.DataBind();
            //Bind();
            // }
        }
    }
}
