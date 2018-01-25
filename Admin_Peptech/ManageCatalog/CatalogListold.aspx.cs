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

public partial class Admin_Peptech_ManageCatalog_CatalogList : System.Web.UI.Page
{
    private string sSortDirection = "ASC";
    private string sSortExpression = "categoryname,subcategoryname";
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
            lblmessage.Text = "Catalog Updated Successfully";
        }
        else if (Request.QueryString["add"] != null)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Catalog Added Successfully";
        }
        if (!IsPostBack)
        {
            BindGrid();
            DataTable dtcat = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"category where status=1").Tables[0];
            if (dtcat.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtcat;
                ddlCategory.DataTextField = "categoryname";
                ddlCategory.DataValueField = "id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
                ddlSubcategory.Items.Insert(0,new ListItem("Select Subcategory",""));
            }
        }

    }
    protected void BindGrid()
    {
        DataTable dtfillgrid = customUtility.GetTableData("Select c.*,cat.categoryname as categoryname,subcat.subcategoryname as subcategoryname,p.ProductName as ProductName from Peptech_catalog c inner join peptech_product p on p.id=c.productid inner join peptech_category cat on p.categoryid=cat.id inner join peptech_subcategory subcat on subcat.id=p.subcategoryid ORDER BY c.catalogname asc").Tables[0];
        if (dtfillgrid.Rows.Count > 0)
        {
            grdListProperty.DataSource = dtfillgrid;
            grdListProperty.DataBind();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
        {
            cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
            hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

            hddval = Convert.ToInt32(hddid.Value);

            if (cb.Checked)
            {
                hidAllValue = hidAllValue + hddval + ",";
            }
        }
        try
        {
            hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
            Delete(hidAllValue, 1);
            lblmessage.Text = "Deleted successfully.";

            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.Visible = true;
        }

    }
    protected void Delete(string val, int status_value)
    {
        customUtility.ExecuteNonQuery("Delete from " + customUtility.DBPrefix + "catalog where id in (" + val + ")");
        grdListProperty.DataBind();
    }
    protected void active_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
        {
            cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
            hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

            hddval = Convert.ToInt32(hddid.Value);

            if (cb.Checked)
            {
                hidAllValue = hidAllValue + hddval + ",";
            }
        }
        try
        {
            hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
            setActive(hidAllValue, 1);
            lblmessage.Text = "Activate successfully.";

            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.Visible = true;
        }

    }
    //Deactivating ListProperty
    protected void deactive_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
        {
            cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
            hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

            hddval = Convert.ToInt32(hddid.Value);

            if (cb.Checked)
            {
                hidAllValue = hidAllValue + hddval + ",";
            }
        }
        try
        {
            hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
            setActive(hidAllValue, 0);
            lblmessage.Text = "Deactivate successfully.";
            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.Visible = true;
        }

    }

    protected void setActive(string val, int status_value)
    {
        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "catalog set status = " + status_value + " where id in (" + val + ")");
        grdListProperty.DataBind();
    }

    protected void grdListProperty_OnRowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            Image img = (Image)e.Row.FindControl("ImageStatus");
            if (dv["status"].ToString().ToLower().Equals("true"))
            {
                img.ImageUrl = "../../images/status-on.gif";
            }
            else
            {
                img.ImageUrl = "../../images/status-off.gif";
            }

        }
    }

    //Paging

    protected void grdListProperty_databound(object sender, EventArgs e)
    {
        if (grdListProperty.Rows.Count > 0)
        {
            //rowcnt = int.Parse(grdListProperty.Rows.Count.ToString());
            //Label lblpage = (Label)grdListProperty.BottomPagerRow.FindControl("lblpage");
            //DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
            //lblpage.Text = "Page : " + Convert.ToString(grdListProperty.PageIndex + 1);
            //ddlpage.SelectedValue = grdListProperty.PageSize.ToString();
            //Menu menuPager = (Menu)grdListProperty.BottomPagerRow.FindControl("menuPager");
            //for (int i = 1; i <= grdListProperty.PageCount; i++)
            //{
            //    MenuItem item = new MenuItem();
            //    item.Text = String.Format(" ",i);
               
            //    item.Value = i.ToString();
            //    //Response.Write(item.Value);
            //    //Response.End();
            //    if (grdListProperty.PageIndex == i)
            //    {
            //        item.Selected = true;

            //    }
            //    menuPager.Items.Add(item);
            //}
        }

       
    }
    protected void ddlpage_selected(object sender, EventArgs e)
    {
        //DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
        //grdListProperty.PageSize = int.Parse(ddlpage.SelectedValue);
        //grdListProperty.DataBind();
    }

    protected void grdListProperty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin_Peptech/ManageCatalog/AddCatalog.aspx?add=1");
    }
    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (e.NewPageIndex > 0)
        //{
        Response.Write(e.NewPageIndex);
        Response.End();
            grdListProperty.PageIndex = e.NewPageIndex;
            grdListProperty.DataBind();
        //}
    }




    protected void grdListProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower() == "delete")
        {
            bool del = customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "catalog where id="+ e.CommandArgument.ToString());
            if (del.ToString().ToLower().Equals("true"))
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Record deleted successfully";
                grdListProperty.DataBind();
                BindGrid();
            }
        }
    }
    protected void menuPager_MenuItemClick(object sender, MenuEventArgs e)
    {
        //grdListProperty.PageIndex = Int32.Parse(e.Item.Value);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //select * from Peptech_product p inner join Peptech_catalog c on p.id=c.productid where p.categoryid=54 and p.subcategoryid=71 and p.status=1
        //string strFilter = "select *,cat.categoryname as categoryname,subcat.subcategoryname as subcategoryname from Peptech_product p inner join Peptech_catalog c on p.id=c.productid inner join peptech_category cat on cat.id=p.categoryid inner join peptech_subcategory subcat on subcat.id=p.subcategoryid where ";
        //if (ddlCategory.SelectedIndex != 0)
        //{
        //    strFilter += "p.categoryid=" + ddlCategory.SelectedValue + " and ";
        //}
        //if (ddlSubcategory.SelectedIndex != 0)
        //{
        //    strFilter += "p.subcategoryid=" + ddlSubcategory.SelectedValue + " and ";
        //}
        //strFilter += "p.status=1";
        
        //DataTable dtFilter = customUtility.GetTableData(strFilter).Tables[0];
        //if (dtFilter.Rows.Count > 0)
        //{
        //    grdListProperty.DataSource = dtFilter;
        //    grdListProperty.DataBind();
        //}
        searchbind();
    }
    protected void searchbind()
    {
        string strFilter = "select *,cat.categoryname as categoryname,subcat.subcategoryname as subcategoryname from Peptech_product p inner join Peptech_catalog c on p.id=c.productid inner join peptech_category cat on cat.id=p.categoryid inner join peptech_subcategory subcat on subcat.id=p.subcategoryid where ";
        if (ddlCategory.SelectedIndex != 0)
        {
            strFilter += "p.categoryid=" + ddlCategory.SelectedValue + " and ";
        }
        if (ddlSubcategory.SelectedIndex != 0)
        {
            strFilter += "p.subcategoryid=" + ddlSubcategory.SelectedValue + " and ";
        }
        strFilter += "p.status=1";

        DataTable dtFilter = customUtility.GetTableData(strFilter).Tables[0];
        if (dtFilter.Rows.Count > 0)
        {
            grdListProperty.DataSource = dtFilter;
            grdListProperty.DataBind();
        }
        
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex != 0)
        {
            DataTable dtsubcat = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid=" + ddlCategory.SelectedValue.ToString() + " and status=1").Tables[0];
            if (dtsubcat.Rows.Count > 0)
            {
                ddlSubcategory.DataSource = dtsubcat;
                ddlSubcategory.DataTextField = "subcategoryname";
                ddlSubcategory.DataValueField = "id";
                ddlSubcategory.DataBind();
                ddlSubcategory.Items.Insert(0, new ListItem("Select Subcategory", ""));
            }
        }
    }

    protected void grdListProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdListProperty_Sorting(object sender, GridViewSortEventArgs e)
    {
        //grdListProperty.SortExpression = e.SortExpression;
        //sSortDirection = GetSortDirection(e.SortDirection.ToString());
        //sSortExpression = e.SortExpression;
        ////gvEmployeeList.DataSource = ds_gvEmployeeList();
        ////gvEmployeeList.DataBind();
        ////grdListProperty.SortExpression[2].;
        //BindGrid();
    }
    //private string GetSortDirection(string sSortDirection)
    //{
    //    switch (sSortDirection)
    //    {
    //        case "ASC":
    //            return "DESC";
    //        case "DESC":
    //            return "ASC";
    //        default:
    //            return "ASC";
    //    }
    //}
}
