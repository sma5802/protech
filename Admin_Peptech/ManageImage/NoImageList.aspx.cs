using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using UserClass;

public partial class Admin_Peptech_ManageImage_NoImageList : System.Web.UI.Page
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
            lblmessage.Text = "Product Image Updated Successfully";
        }
    }
    protected void grdListProperty_OnRowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            ((Label)e.Row.FindControl("lblFormula")).Text = writeFormula(dv["formula"].ToString());
            if (dv["productimage"].ToString() != "")
                ((HtmlImage)e.Row.FindControl("imgProd")).Src = "../../ProductImage/" + dv["productImage"].ToString();
            else
                ((HtmlImage)e.Row.FindControl("imgProd")).Visible = false;

            if (dv["subcategoryname"].ToString() != "" && dv["subcategoryname"] != null)
                ((HyperLink)e.Row.FindControl("hlSubcategory")).Text = HttpUtility.HtmlDecode(dv["subcategoryname"].ToString());
            else
                ((HyperLink)e.Row.FindControl("hlSubcategory")).Text = "N/A";
        }
    }
    protected string writeFormula(string numberString)
    {
        string strbig = "";
        string strsmal = "";

        char[] ca = numberString.ToCharArray();
        for (int i = 0; i < ca.Length; i++)
        {
            if (ca[i] > 57 || ca[i] < 48)
            {
                strbig += ca[i];
            }
            else
            {
                strbig += "<span style='FONT-SIZE: 7pt; VERTICAL-ALIGN: sub; LINE-HEIGHT: 16pt; FONT-FAMILY: verdana'>" + ca[i] + "</span>";
            }

        }
        return strbig;
    }

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
            lblmessage.Visible = true;
            lblmessage.Text = "Record deleted successfully";
            grdListProperty.DataBind();
        }
    }
}