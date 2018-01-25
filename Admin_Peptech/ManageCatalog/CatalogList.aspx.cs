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
    
    string direction;
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
            //BindGrid();
            DataTable dtcat = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "category where status=1").Tables[0];
            if (dtcat.Rows.Count > 0)
            {
                int catid = 0;
                int Subcatid = 0;
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));
                for (int i = 0; i < dtcat.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = HttpUtility.HtmlDecode(dtcat.Rows[i]["categoryname"].ToString());
                    li.Value = dtcat.Rows[i]["id"].ToString();
                    ddlCategory.Items.Add(li);
                }
                if (Session["catid"] != null && Session["catid"].ToString() != "")
                {
                    catid = Convert.ToInt32(Session["catid"]);
                    ddlCategory.SelectedValue = catid.ToString();
                    ddlCategory.DataBind();
                }
                if (ddlCategory.SelectedValue != "0")
                {
                    ddlSubcategory.Items.Clear();
                    ddlSubcategory.Items.Insert(0, new ListItem("Select Subcategory", "0"));
                    if (ddlCategory.SelectedValue != "0")
                    {
                        DataTable dtsubcat = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid=" + ddlCategory.SelectedValue.ToString() + " and status=1").Tables[0];
                        if (dtsubcat.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtsubcat.Rows.Count; i++)
                            {
                                ListItem li = new ListItem();
                                li.Text = HttpUtility.HtmlDecode(dtsubcat.Rows[i]["subcategoryname"].ToString());
                                li.Value = dtsubcat.Rows[i]["id"].ToString();
                                ddlSubcategory.Items.Add(li);
                            }
                        }
                    }
                }
                if (Session["Subcatid"] != null && Session["Subcatid"].ToString() != "0")
                {
                    Subcatid = Convert.ToInt32(Session["Subcatid"]);
                    ddlSubcategory.SelectedValue = Subcatid.ToString();
                    ddlSubcategory.DataBind();
                }
                searchbind();
            }
        }

    }
    protected void BindGrid()
    {
        ViewState["indexid"] = 0;
        DataSet dtfillgrid = customUtility.GetTableData("Select c.*,cat.categoryname as categoryname,subcat.subcategoryname as subcategoryname,p.ProductName as ProductName from Peptech_catalog c inner join peptech_product p on p.id=c.productid inner join peptech_category cat on p.categoryid=cat.id inner join peptech_subcategory subcat on subcat.id=p.subcategoryid ORDER BY c.id desc");
        if (dtfillgrid.Tables[0].Rows.Count > 0)
        {
            grdListProperty.DataSource = dtfillgrid;
            grdListProperty.DataBind();
        }
        ViewState["sorting"] = 0;
        Session["ds"] = dtfillgrid;
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
        //grdListProperty.DataBind();
        //BindGrid();
        searchbind();
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
            lblmessage.Text = "Activated successfully.";

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
            lblmessage.Text = "Deactivated successfully.";
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
        //grdListProperty.DataBind();
        //BindGrid();
        searchbind();
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
            if (dv["subcategoryname"].ToString() != "" && dv["subcategoryname"] != null)
            {
                ((HyperLink)e.Row.FindControl("lblcountiesname101")).Text = HttpUtility.HtmlDecode(dv["subcategoryname"].ToString());
            }
            else
            {
                ((HyperLink)e.Row.FindControl("lblcountiesname101")).Text = "N/A";
            }
        }
    }

    //Paging

    //protected void ddlpage_selected(object sender, EventArgs e)
    //{
    //   //DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
    //    grdListProperty.PageSize = int.Parse(ddlpage.SelectedValue);
    //    if (ViewState["indexid"] == "0")
    //    {
    //        BindGrid();
    //    }
    //    else
    //    {
    //      searchbind();
    //    }
    //}

    //protected void grdListProperty_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin_Peptech/ManageCatalog/AddCatalog.aspx?add=1");
    }
    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Response.Write(ViewState["indexid"]);
        //Response.End();
        Session["index"] = e.NewPageIndex;
        //if (ViewState["indexid"].ToString()== "0")
        //{
        //    grdListProperty.PageIndex = e.NewPageIndex;
        //    BindGrid();
        //}
        //else
        //{
            grdListProperty.PageIndex = e.NewPageIndex;
            searchbind();
        //}
       
    }
    //protected void grdListProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName.ToString().ToLower() == "delete")
    //    {
    //        bool del = customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "catalog where id=" + e.CommandArgument.ToString());
    //        if (del.ToString().ToLower().Equals("true"))
    //        {
    //            lblmessage.Visible = true;
    //            lblmessage.Text = "Record deleted successfully";
    //            grdListProperty.DataBind();
    //            BindGrid();
    //        }
    //    }
    //}
    //protected void menuPager_MenuItemClick(object sender, MenuEventArgs e)
    //{
    //    grdListProperty.PageIndex = Int32.Parse(e.Item.Value);
    //}
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
        Session["index"] = null;
        //ViewState["indexid"] = 1;
        searchbind();
    }
    protected void searchbind()
    {
        Int32 index = 0;
        Session["catid"] = ddlCategory.SelectedValue;
        Session["Subcatid"] = ddlSubcategory.SelectedValue;
        if (Session["index"] != null && Session["index"].ToString() != "")
            index = Convert.ToInt32(Session["index"]);
        string str = "select c.*,p.productname ,cat.categoryname as categoryname,(select subcategoryname from " + customUtility.DBPrefix + "subcategory subcat where subcat.id=p.subcategoryid) as subcategoryname from Peptech_product p inner join Peptech_catalog c on p.id=c.productid inner join peptech_category cat on cat.id=p.categoryid where ";
        if (ddlCategory.SelectedValue != "0")
        {
            str += "p.categoryid=" + ddlCategory.SelectedValue + " and ";
        }
        if (ddlSubcategory.SelectedValue != "0")
        {
            str += "p.subcategoryid=" + ddlSubcategory.SelectedValue + " and ";
        }
        str += " p.id!=0 ";
        if (Session["sort"] != null && Session["sort"].ToString() != "")
            str += "order by " + Session["sort"].ToString();
        else
            str += "order by p.id desc";
        //Response.Write(str);
        //Response.End();
        
        DataSet dtFilter = customUtility.GetTableData(str);
        if (dtFilter.Tables[0].Rows.Count == 0)
        {
            grdListProperty.EmptyDataText = "No List Available";
            grdListProperty.DataBind();
        }
        else
        {
            grdListProperty.DataSource = dtFilter;
            grdListProperty.PageIndex = index;
            grdListProperty.DataBind();
            ViewState["sorting"] = 1;
            Session["dssearch"] = dtFilter;
        }




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

        //DataSet dtFilter = customUtility.GetTableData(strFilter);
        //if (dtFilter.Tables[0].Rows.Count > 0)
        //{
        //    grdListProperty.EmptyDataText = "No List Available";
        //    grdListProperty.DataBind();
        //}
        //else
        //{
        //    grdListProperty.DataSource = dtFilter;
        //    grdListProperty.DataBind();
        //    ViewState["sorting"] = 1;
        //    Session["dssearch"] = dtFilter;
        //}

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubcategory.Items.Clear();
        ddlSubcategory.Items.Insert(0, new ListItem("Select Subcategory", "0"));
        if (ddlCategory.SelectedIndex != 0)
        {
            DataTable dtsubcat = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid=" + ddlCategory.SelectedValue.ToString() + " and status=1").Tables[0];
            if (dtsubcat.Rows.Count > 0)
            {
                for (int i = 0; i < dtsubcat.Rows.Count; i++)
                {

                    ListItem li = new ListItem();
                    li.Text = HttpUtility.HtmlDecode(dtsubcat.Rows[i]["subcategoryname"].ToString());
                    li.Value = dtsubcat.Rows[i]["id"].ToString();
                    ddlSubcategory.Items.Add(li);
                }

                //ddlSubcategory.DataSource = dtsubcat;
                //ddlSubcategory.DataTextField = "subcategoryname";
                //ddlSubcategory.DataValueField = "id";
                //ddlSubcategory.DataBind();
                //ddlSubcategory.Items.Insert(0, new ListItem("Select Subcategory", ""));
            }
        }
    }

    //protected void grdListProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{

    //}

    //protected void grdListProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{

    //}
    protected void grdListProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdListProperty_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataSet ds = new DataSet();
        if (ViewState["sorting"].ToString() == "0")
        {
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
                grdListProperty.DataSource = m_DataView;
                grdListProperty.DataBind();
                Session.Add("sorting", direction);
            }
        }
        else
        {
            ds = (DataSet)Session["dssearch"];

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
                grdListProperty.DataSource = m_DataView;
                grdListProperty.DataBind();
                Session.Add("sorting", direction);
            }
        }
        Session["sort"] = e.SortExpression + " " + direction;
    }
    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        if(e.CommandName=="Delete")
        {
            customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "catalog where id=" + e.CommandArgument);
            //BindGrid();
            searchbind();
            lblmessage.Visible = true;
            lblmessage.Text = "Catalog deleted successfully!";
        }
    }
}
