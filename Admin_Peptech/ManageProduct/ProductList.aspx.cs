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

public partial class Admin_Peptech_ManageProduct_ProductList : System.Web.UI.Page
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
        try
        {
            if (Request.QueryString["Upd"] != null)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Product Updated Successfully";
            }
            else if (Request.QueryString["add"] != null)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Product Added Successfully";
            }
            else if (Request.QueryString["batch"] != null)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Batch Product Added Successfully";
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
                    if (Session["pcatid"] != null && Session["pcatid"].ToString() != "")
                    {
                        catid = Convert.ToInt32(Session["pcatid"]);
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
                    if (Session["pSubcatid"] != null && Session["pSubcatid"].ToString() != "0")
                    {
                        Subcatid = Convert.ToInt32(Session["pSubcatid"]);
                        ddlSubcategory.SelectedValue = Subcatid.ToString();
                        ddlSubcategory.DataBind();
                    }
                    searchbind();
                }
            }
        }
        catch(Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message;
        }
    }
    protected void BindGrid()
    {
        //ViewState["indexid"] = 0;
        //DataSet dtfillgrid = customUtility.GetTableData("Select p.*,(select categoryname from pep$tech$corp.peptech_category where id=p.categoryid)categoryname,(select subcategoryname from pep$tech$corp.peptech_subcategory where id=p.subcategoryid)subcategoryname  from pep$tech$corp.Peptech_Product p ORDER BY isnull(p.sort,0)");
        DataSet dtfillgrid = customUtility.GetTableData("Select p.*,(select categoryname from pep$tech$corp.peptech_category where id=p.categoryid)categoryname,(select subcategoryname from pep$tech$corp.peptech_subcategory where id=p.subcategoryid)subcategoryname  from pep$tech$corp.Peptech_Product p ");
        if (dtfillgrid.Tables[0].Rows.Count > 0)
        {
            grdListProperty.DataSource = dtfillgrid;
            grdListProperty.DataBind();
        }
        ViewState["sorting"] = 0;
        Session["pdsproduct"] = dtfillgrid;
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
        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Product set status = " + status_value + " where id in (" + val + ")");
        //BindGrid();
        searchbind();
        //grdListProperty.DataBind();
        
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
       // customUtility.ExecuteNonQuery("Delete from " + customUtility.DBPrefix + "Product where id in (" + val + ")");
        //grdListProperty.DataBind();
        //BindGrid();
        for (int i = 0; i < grdListProperty.Rows.Count; i++)
        {
            cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
            hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");
            hddval = Convert.ToInt32(hddid.Value);

            if (cb.Checked)
            {
                //hidAllValue = hidAllValue + hddval + ",";
                try
                {
                    string sort = customUtility.GetAField("Select sort from " + customUtility.DBPrefix + "Product where ID='" + hddval.ToString() + "'");
                    if (customUtility.ExecuteNonQuery("Delete from " + customUtility.DBPrefix + "Product where ID='" + hddval.ToString() + "'"))
                    {
                        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Product set sort=sort-1 where ID in(select   ID from " + customUtility.DBPrefix + "Product where sort>" + sort.ToString() + ")");
                    }
                }
                catch
                {
                }
            }
        }
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
            ((HyperLink)e.Row.FindControl("lblcountiesname4")).Text = writeFormula(dv["formula"].ToString());
            if (dv["subcategoryname"].ToString() != "" && dv["subcategoryname"]!= null)
            {
                ((HyperLink)e.Row.FindControl("hlSubcategory")).Text = HttpUtility.HtmlDecode(dv["subcategoryname"].ToString());
            }
            else
            {
                ((HyperLink)e.Row.FindControl("hlSubcategory")).Text ="N/A";
            }
            //if (dv["catalog"].ToString() != "" && dv["catalog"] != null)
            //{
            //    ((HyperLink)e.Row.FindControl("hlcatalog")).Text = HttpUtility.HtmlDecode(dv["catalog"].ToString());
            //}
            //else
            //{
            //    ((HyperLink)e.Row.FindControl("hlcatalog")).Text= "N/A";
            //}
            HyperLink lblcountiesname5 = (HyperLink)e.Row.FindControl("lblcountiesname5");
            string mw =dv["mweight"].ToString();
            double MW = Convert.ToDouble(mw);
            lblcountiesname5.Text = string.Format("{0:0.00}", MW);

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
        //Response.Write(strbig);
        return strbig;
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
        Response.Redirect("~/Admin_Peptech/ManageProduct/AddProduct.aspx?add=1");
    }
    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Session["pindex"] = e.NewPageIndex;
        //Response.Write(Session["pindex"]);
        //if (ViewState["indexid"].ToString() == "0")
        //{
        //    grdListProperty.PageIndex = e.NewPageIndex;
        //    BindGrid();
        //}
        //else
        //{
            grdListProperty.PageIndex = e.NewPageIndex;
            BindGrid();
        //}
    }




    protected void grdListProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sort = "";
        int newsort = 0;
        //switch (e.CommandName.ToString().ToLower())
        //{
        //    case "upsort":
        //        sort = customUtility.GetAField("Select sort from " + customUtility.DBPrefix + "product where ID='" + e.CommandArgument.ToString() + "'");
        //        newsort = Convert.ToInt32(sort) - 1;
        //        if (customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "product where sort='" + newsort + "'"))
        //        {
        //            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "product set sort='" + sort + "' where sort='" + newsort + "'");
        //            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "product set sort='" + newsort + "' where ID='" + e.CommandArgument.ToString() + "'");
        //            BindGrid();   
        //        }
        //        break;
        //    case "downsort":
        //        sort = customUtility.GetAField("Select sort from " + customUtility.DBPrefix + "product where ID='" + e.CommandArgument.ToString() + "'");
        //        newsort = Convert.ToInt32(sort) + 1;
        //        if (customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "product where sort='" + newsort + "'"))
        //        {
        //            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "product set sort='" + sort + "' where sort='" + newsort + "'");
        //            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "product set sort='" + newsort + "' where ID='" + e.CommandArgument.ToString() + "'");
        //            BindGrid();
        //        }
        //        break;
        //}

        if (e.CommandName.ToString().ToLower() == "delete")
        {
            //bool del = customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "product where id=" + e.CommandArgument.ToString());
            //if (del.ToString().ToLower().Equals("true"))
            //{
            //    lblmessage.Visible = true;
            //    lblmessage.Text = "Record deleted successfully";
            //    searchbind();
            //    //BindGrid();
            //    //grdListProperty.DataBind();
            //    //Bind();
            //}
            //string deletesort = customUtility.GetAField("Select sort from " + customUtility.DBPrefix + "Product where ID='" + e.CommandArgument.ToString()+"'");
            if (customUtility.ExecuteNonQuery("Delete from " + customUtility.DBPrefix + "Product where ID='" +  e.CommandArgument.ToString()+"'"))
            {
                //customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Product set sort=sort-1 where ID in(select   ID from " + customUtility.DBPrefix + "Product where sort>" + deletesort.ToString() + ")");
                lblmessage.Visible = true;
                lblmessage.Text = "Record deleted successfully";
                BindGrid();
            }
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubcategory.Items.Clear();
        ddlSubcategory.Items.Insert(0, new ListItem("Select Subcategory", "0"));
        if (ddlCategory.SelectedIndex != 0)
        {
           // GridView grdListProperty;
            grdListProperty.Columns[1].Visible = false;
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
        else
            grdListProperty.Columns[1].Visible = true;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["pindex"] = null;
        Session["psort"] = null;
        //ViewState["indexid"] = 1;
        searchbind();
    }
    protected void searchbind()
    {
        Int32 index = 0;
        Session["pcatid"] = ddlCategory.SelectedValue;
        Session["pSubcatid"] = ddlSubcategory.SelectedValue;
        if (Session["pindex"] != null && Session["pindex"].ToString() != "")
            index =Convert.ToInt32(Session["pindex"]);
        string str = "select p.*,c.categoryname as categoryname,(select subcategoryname from " + customUtility.DBPrefix + "subcategory sc where sc.id=p.subcategoryid) as subcategoryname from " + customUtility.DBPrefix + "product p inner join " + customUtility.DBPrefix + "category c on p.categoryid=c.id where ";
        if (ddlCategory.SelectedValue != "0")
        {
            str += "p.categoryid=" + ddlCategory.SelectedValue + " and ";
        }
        if (ddlSubcategory.SelectedValue != "0")
        {
            str += "p.subcategoryid=" + ddlSubcategory.SelectedValue + " and ";
        }
        str += " p.id!=0 ";
        if (Session["psort"] != null && Session["psort"].ToString() != null)
            str += "order by " + Session["psort"].ToString();
        else
            str += "order by p.id desc";
        //Response.Write(str);
        //Response.End();
        DataSet dtFilter = customUtility.GetTableData(str);
        if (dtFilter.Tables[0].Rows.Count <= 0)
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
            Session["pdssearchproduct"] = dtFilter;
        }
        Session["pdsproduct"] = dtFilter;
        //Response.Write(Session["pindex"]);
        //GridView dd;
       
        //grdListProperty.PageIndex = index;
    }
    protected void grdListProperty_Sorting(object sender, GridViewSortEventArgs e)
    {
        //Response.Write(e.SortExpression);
        DataSet ds = new DataSet();
        if (ViewState["sorting"].ToString() == "0")
        {
            ds = (DataSet)Session["pdsproduct"];

            DataTable dtable = ds.Tables[0];
            direction = "ASC";
            if (dtable != null)
            {
                DataView m_DataView = new DataView(dtable);
                if (Session["psorting"] != null)
                {
                    direction = Session["psorting"].ToString();
                    if (direction.Equals("ASC"))
                        direction = "DESC";
                    else
                        direction = "ASC";
                }
                m_DataView.Sort = e.SortExpression + " " + direction;
                grdListProperty.DataSource = m_DataView;
                grdListProperty.DataBind();
                Session.Add("psorting", direction);
            }
        }
        else
        {
            ds = (DataSet)Session["pdssearchproduct"];

            DataTable dtable = ds.Tables[0];
            direction = "ASC";
            if (dtable != null)
            {
                DataView m_DataView = new DataView(dtable);
                if (Session["psorting"] != null)
                {
                    direction = Session["psorting"].ToString();
                    if (direction.Equals("ASC"))
                        direction = "DESC";
                    else
                        direction = "ASC";
                }
                m_DataView.Sort = e.SortExpression + " " + direction;
                grdListProperty.DataSource = m_DataView;
                grdListProperty.DataBind();
                Session.Add("psorting", direction);
            }
        }
        //Response.Write(e.SortExpression + " " + direction);
        Session["psort"] = e.SortExpression + " " + direction;
        
    }
    protected void grdListProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
    }
}
